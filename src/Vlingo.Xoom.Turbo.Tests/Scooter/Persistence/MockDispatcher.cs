// Copyright © 2012-2023 VLINGO LABS. All rights reserved.
//
// This Source Code Form is subject to the terms of the
// Mozilla Public License, v. 2.0. If a copy of the MPL
// was not distributed with this file, You can obtain
// one at https://mozilla.org/MPL/2.0/.

using System;
using System.Collections.Generic;
using Vlingo.Xoom.Actors.TestKit;
using Vlingo.Xoom.Common;
using Vlingo.Xoom.Symbio.Store.Dispatch;

namespace Vlingo.Xoom.Turbo.Tests.Scooter.Persistence;

public class MockDispatcher<T, ST> : IDispatcher
{
	private AccessSafely _access;
	private readonly IConfirmDispatchedResultInterest _confirmDispatchedResultInterest;
	private IDispatcherControl _control;
	private AtomicBoolean _processDispatch = new AtomicBoolean(true);
	private List<Dispatchable> _dispatched = new List<Dispatchable>();
	private int _dispatchAttemptCount = 0;

	public MockDispatcher(IConfirmDispatchedResultInterest confirmDispatchedResultInterest)
	{
		_confirmDispatchedResultInterest = confirmDispatchedResultInterest;
		_access = AfterCompleting(0);
	}

	private AccessSafely AfterCompleting(int times)
	{
		_access = AccessSafely.AfterCompleting(times)
			.WritingWith("dispatched", (Action<Dispatchable>)_dispatched.Add)
			.ReadingWith("dispatched", () => _dispatched)
				
			.WritingWith("processDispatch", (Action<bool>)_processDispatch.Set)
			.ReadingWith("processDispatch", _processDispatch.Get)
			.ReadingWith("dispatchAttemptCount", () => _dispatchAttemptCount);

		return _access;
	}

	public void ControlWith(IDispatcherControl control) => _control = control;

	public void Dispatch(Dispatchable dispatchable)
	{
		_dispatchAttemptCount++;
		if (_processDispatch.Get())
		{
			var dispatchId = dispatchable.Id;
			_access.WriteUsing("dispatched", dispatchable);
			_control.ConfirmDispatched(dispatchId, _confirmDispatchedResultInterest);
		}
	}
}