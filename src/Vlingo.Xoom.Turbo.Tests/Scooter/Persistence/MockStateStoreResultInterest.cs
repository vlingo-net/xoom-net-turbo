// Copyright © 2012-2021 VLINGO LABS. All rights reserved.
//
// This Source Code Form is subject to the terms of the
// Mozilla Public License, v. 2.0. If a copy of the MPL
// was not distributed with this file, You can obtain
// one at https://mozilla.org/MPL/2.0/.

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using Vlingo.Xoom.Actors.TestKit;
using Vlingo.Xoom.Common;
using Vlingo.Xoom.Symbio;
using Vlingo.Xoom.Symbio.Store;
using Vlingo.Xoom.Symbio.Store.Dispatch;
using Vlingo.Xoom.Symbio.Store.State;

namespace Vlingo.Xoom.Turbo.Tests.Scooter.Persistence
{
	public class MockStateStoreResultInterest : IReadResultInterest, IWriteResultInterest,
		IConfirmDispatchedResultInterest
	{
		private AccessSafely _access;
		private readonly AtomicInteger _confirmDispatchedResultedIn = new AtomicInteger(0);
		private readonly AtomicInteger _writeObjectResultedIn = new AtomicInteger(0);
		private readonly AtomicReference<object> _objectWriteResult = new AtomicReference<object>();
		private readonly ConcurrentQueue<Result> _objectWithAccumulatedResults = new ConcurrentQueue<Result>();
		private readonly AtomicReference<object> _objectState = new AtomicReference<object>();
		private readonly ConcurrentQueue<object> _sources = new ConcurrentQueue<object>();
		private readonly AtomicReference<Metadata> _metadaHolder = new AtomicReference<Metadata>();
		private readonly ConcurrentQueue<Exception> _errorCauses = new ConcurrentQueue<Exception>();

		public MockStateStoreResultInterest()
		{
			_access = AfterCompleting(0);
		}

		public void ReadResultedIn<TState>(IOutcome<StorageException, Result> outcome, string? id, TState state,
			int stateVersion, Metadata? metadata,
			object? @object)
		{
			throw new System.NotImplementedException();
		}

		public void ReadResultedIn<TState>(IOutcome<StorageException, Result> outcome,
			IEnumerable<TypedStateBundle> bundles, object? @object)
		{
			throw new System.NotImplementedException();
		}

		public void WriteResultedIn<TState, TSource>(IOutcome<StorageException, Result> outcome, string id, TState state,
			int stateVersion, IEnumerable<TSource> sources,
			object? @object)
		{
			outcome
				.AndThen(result =>
				{
					_access.WriteUsing("writeStoredData", new StoreData<TSource>(1, result, state, sources, null, null));
					return result;
				})
				.Otherwise(cause =>
				{
					_access.WriteUsing("writeStoreData", new StoreData<TSource>(1, cause.Result, state, sources, null, cause));
					return cause.Result;
				});
		}

		public void ConfirmDispatchedResultedIn(Result result, string dispatchId)
		{
			throw new System.NotImplementedException();
		}

		private AccessSafely AfterCompleting(int times) => AccessSafely.AfterCompleting(times)
			.WritingWith<int>("confirmDispatchedResultedIn", (increment) => _confirmDispatchedResultedIn.AddAndGet(increment))
			.ReadingWith("confirmDispatchedResultedIn", () => _confirmDispatchedResultedIn.Get())
			.WritingWith<StoreData<object>>("writeStoreData", (data) =>
			{
				_writeObjectResultedIn.AddAndGet(data.ResultedIn);
				_objectWriteResult.Set(data.Result);
				_objectWithAccumulatedResults.Enqueue(data.Result);
				_objectState.Set(data.State);
				data.Sources.ToList().ForEach(_sources.Enqueue);
				_metadaHolder.Set(data.Metadata);

				if (data.ErrorCauses != null)
					_errorCauses.Enqueue(data.ErrorCauses);
			})
			.ReadingWith<StoreData<object>>("readStoreData", (data) =>
			{
				_writeObjectResultedIn.AddAndGet(data.ResultedIn);
				_objectWriteResult.Set(data.Result);
				_objectWithAccumulatedResults.Enqueue(data.Result);
				_objectState.Set(data.State);
				data.Sources.ToList().ForEach(_sources.Enqueue);
				_metadaHolder.Set(data.Metadata);

				if (data.ErrorCauses != null)
					_errorCauses.Enqueue(data.ErrorCauses);
			});
	}

	public class StoreData<ISource>
	{
		public int ResultedIn { get; }
		public Result Result { get; }
		public object State { get; }
		public IEnumerable<ISource> Sources { get; }
		public Metadata Metadata { get; }
		public Exception ErrorCauses { get; }

		public StoreData(int resultedIn, Result objectResult, object state, IEnumerable<ISource> sources, Metadata metadata,
			Exception errorCauses)
		{
			ResultedIn = resultedIn;
			Result = objectResult;
			State = state;
			Sources = sources;
			Metadata = metadata;
			ErrorCauses = errorCauses;
		}
	}
}