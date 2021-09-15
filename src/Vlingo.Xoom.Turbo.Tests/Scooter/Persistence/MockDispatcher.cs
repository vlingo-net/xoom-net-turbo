using System;
using System.Collections.Generic;
using Vlingo.Xoom.Actors.TestKit;
using Vlingo.Xoom.Common;
using Vlingo.Xoom.Symbio.Store.Dispatch;

namespace Vlingo.Xoom.Turbo.Tests.Scooter.Persistence
{
	public class MockDispatcher<T, ST> : IDispatcher
	{
		private readonly AccessSafely _access;
		private readonly IConfirmDispatchedResultInterest _confirmDispatchedResultInterest;
		private IDispatcherControl _control;
		private AtomicBoolean _processDispatch = new AtomicBoolean(true);
		private List<Dispatchable> _dispatched = new  List<Dispatchable>();
		private int _dispatchAttemptCount = 0;

		public MockDispatcher(IConfirmDispatchedResultInterest confirmDispatchedResultInterest)
		{
			_confirmDispatchedResultInterest = confirmDispatchedResultInterest;
			_access = AfterCompleting(0);
		}

		private AccessSafely AfterCompleting(int times) => AccessSafely.AfterCompleting(times)
			.WritingWith("dispatched", (Action<Dispatchable>)_dispatched.Add)
			.ReadingWith("dispatched", () => _dispatched)
			.WritingWith("processDispatch", (Action<bool>)_processDispatch.Set)
			.ReadingWith("processDispatch", _processDispatch.Get)
			.ReadingWith("dispatchAttemptCount", () => _dispatchAttemptCount);

		public void ControlWith(IDispatcherControl control)
		{
			_control = control;
		}

		public void Dispatch(Dispatchable dispatchable)
		{
			throw new System.NotImplementedException();
		}
	}
}