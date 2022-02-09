// Copyright © 2012-2022 VLINGO LABS. All rights reserved.
//
// This Source Code Form is subject to the terms of the
// Mozilla Public License, v. 2.0. If a copy of the MPL
// was not distributed with this file, You can obtain
// one at https://mozilla.org/MPL/2.0/.

using System.Collections.Concurrent;
using System.Collections.Generic;
using Vlingo.Xoom.Actors.TestKit;
using Vlingo.Xoom.Common;
using Vlingo.Xoom.Symbio;
using Vlingo.Xoom.Symbio.Store.Dispatch;

namespace Vlingo.Xoom.Turbo.Tests.Scooter.Persistence
{
	public class MockStateStoreDispatcher : IDispatcher
	{
		private AccessSafely _access;
		private readonly Dictionary<string, object> _dispatched = new Dictionary<string, object>();
		private readonly ConcurrentQueue<IEntry> _dispatchedEntries = new ConcurrentQueue<IEntry>();
		private readonly AtomicBoolean _processDispatch = new AtomicBoolean(true);
		private int _dispatchAttemptCount = 0;
		private readonly IConfirmDispatchedResultInterest _confirmDispatchedResultInterest;
		private IDispatcherControl _control;

		public MockStateStoreDispatcher(IConfirmDispatchedResultInterest confirmDispatchedResultInterest)
		{
			_confirmDispatchedResultInterest = confirmDispatchedResultInterest;
			_access = AccessSafely.AfterCompleting(0);
		}

		public void ControlWith(IDispatcherControl control) => _control = control;

		public void Dispatch(Dispatchable dispatchable)
		{
			_dispatchAttemptCount++;
			if (_processDispatch.Get())
			{
				var dispatchId = dispatchable.Id;

				_access.WriteUsing("dispatched", dispatchId,
					new Dispatch<IState, IEntry>(dispatchable.TypedState<IState>(), dispatchable.Entries));

				_control.ConfirmDispatched(dispatchId, _confirmDispatchedResultInterest);
			}
		}

		public AccessSafely AfterCompleting(int times)
		{
			_access = AccessSafely
				.AfterCompleting(times)
				.WritingWith<string, Dispatch<IState, IEntry>>("dispatched", (id, dispatch) =>
				{
					_dispatched.Add(id, dispatch.State);
					dispatch.Entries.ForEach(_dispatchedEntries.Enqueue);
				})
				.ReadingWith<string>("dispatchedState", (id) => _dispatched.GetValueOrDefault(id))
				.ReadingWith("dispatchedStateCount", () => _dispatched.Count)
				.ReadingWith("dispatchedEntries", () => _dispatchedEntries)
				.ReadingWith("dispatchedEntriesCount", () => _dispatchedEntries.Count)
				.WritingWith<bool>("processDispatch", (flag) => _processDispatch.Set(flag))
				.ReadingWith("processDispatch", () => _processDispatch.Get())
				.ReadingWith("dispatchAttemptCount", () => _dispatchAttemptCount)
				.ReadingWith("dispatched", () => _dispatched);

			return _access;
		}
	}

	public class Dispatch<S, E> where E : IEntry
	{
		public List<E> Entries;
		public S State;

		public Dispatch(S state, List<E> entries)
		{
			State = state;
			Entries = entries;
		}
	}
}