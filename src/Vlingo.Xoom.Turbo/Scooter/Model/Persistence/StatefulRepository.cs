// Copyright © 2012-2021 VLINGO LABS. All rights reserved.
//
// This Source Code Form is subject to the terms of the
// Mozilla Public License, v. 2.0. If a copy of the MPL
// was not distributed with this file, You can obtain
// one at https://mozilla.org/MPL/2.0/.

using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Vlingo.Xoom.Common;
using Vlingo.Xoom.Symbio;
using Vlingo.Xoom.Symbio.Store;
using Vlingo.Xoom.Symbio.Store.State;

namespace Vlingo.Xoom.Turbo.Scooter.Model.Persistence
{
	public abstract class StatefulRepository
	{
		protected StatefulRepository()
		{
		}

		/// <summary>
		/// Answer the T afer awaiting the read to be completed. The <see cref="interest"/>
		/// must be requested upon each new <see cref="Code()"/>.
		/// </summary>
		/// <param name="T">the type of object to answer</param>
		protected T Await<T>(ReadInterest interest)
		{
			while (!interest.IsRead())
			{
				interest.ThrowIfException();
			}

			return (T)interest.state.Get();
		}

		/// <summary>
		/// Await on the write to be completed. The <see cref="interest"/> must be
		/// requested upon each new <see cref="Write()"/>.
		/// </summary>
		/// <param name="interest"> the WriteInterest on which the await is based</param>
		protected void Await(WriteInterest interest)
		{
			while (!interest.IsWritten())
			{
				interest.ThrowIfException();
			}
		}

		/// <summary> 
		/// Answer a <see cref="StatefulRepository.ReadInterest"/> for each new <see cref="Read()"/>.
		/// </summary>
		/// <returns><see cref="ReadInterest"/></returns>
		protected ReadInterest CreateReadInterest() => new ReadInterest();

		/// <summary> 
		/// Answer a <see cref="StatefulRepository.WriteInterest"/> for each new <see cref="Write()"/>.
		/// </summary>
		/// <returns><see cref="WriteInterest"/></returns>
		protected WriteInterest CreateWriteInterest() => new WriteInterest();

		public class ReadInterest : Exceptional, IReadResultInterest
		{
			private readonly AtomicBoolean _read;
			public AtomicReference<object> state = new AtomicReference<object>();

			public void ReadResultedIn<TState>(IOutcome<StorageException, Result> outcome, string? id, TState state,
				int stateVersion, Metadata? metadata, object? @object) => ReadConsidering(outcome, state);

			[MethodImpl(MethodImplOptions.Synchronized)]
			private void Read(object state)
			{
				this.state.Set(state);
				_read.Set(true);
			}

			[MethodImpl(MethodImplOptions.Synchronized)]
			public bool IsRead() => _read.Get();

			private void ReadConsidering<S>(IOutcome<StorageException, Result> outcome, S state)
			{
				outcome
					.AndThen(result =>
					{
						if (result == Result.Success)
						{
							Read(state);
						}

						return result;
					})
					.Otherwise(ex =>
					{
						_exception.Set(ex);
						return outcome.GetOrNull();
					});
			}

			public void ReadResultedIn<TState>(IOutcome<StorageException, Result> outcome,
				IEnumerable<TypedStateBundle> bundles, object? @object) => throw new NotImplementedException();

			public ReadInterest()
			{
				_read = new AtomicBoolean(false);
				state = new AtomicReference<object>();
			}
		}

		public class WriteInterest : Exceptional, IWriteResultInterest
		{
			private readonly AtomicBoolean _written;

			public void WriteResultedIn<TState, TSource>(IOutcome<StorageException, Result> outcome, string id, TState state,
				int stateVersion, IEnumerable<Source<TSource>> sources, object? @object) => WrittenConsidering(outcome);

			[MethodImpl(MethodImplOptions.Synchronized)]
			private void Written() => _written.Set(true);

			[MethodImpl(MethodImplOptions.Synchronized)]
			public bool IsWritten() => _written.Get();

			public WriteInterest() => _written = new AtomicBoolean(false);

			private void WrittenConsidering(IOutcome<StorageException, Result> outcome)
			{
				outcome
					.AndThen(result =>
					{
						if (result == Result.Success)
						{
							Written();
						}

						return result;
					})
					.Otherwise(ex =>
					{
						_exception.Set(ex);
						return outcome.GetOrNull();
					});
			}

			public void WriteResultedIn<TState, TSource>(IOutcome<StorageException, Result> outcome, string id, TState state,
				int stateVersion, IEnumerable<TSource> sources,
				object? @object)
			{
				WrittenConsidering(outcome);
			}
		}

		public class Exceptional
		{
			protected readonly AtomicReference<StorageException> _exception;

			protected Exceptional()
			{
				_exception = new AtomicReference<StorageException>();
			}

			[MethodImpl(MethodImplOptions.Synchronized)]
			public void ThrowIfException()
			{
				var t = _exception.Get();
				if (t != null)
				{
					throw new InvalidOperationException(string.Concat("Append failed because: ", t.Message), t);
				}
			}
		}
	}
}