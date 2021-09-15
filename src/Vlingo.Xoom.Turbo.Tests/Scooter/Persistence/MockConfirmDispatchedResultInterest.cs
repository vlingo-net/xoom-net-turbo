using System;
using Vlingo.Xoom.Symbio.Store;
using Vlingo.Xoom.Symbio.Store.Dispatch;

namespace Vlingo.Xoom.Turbo.Tests.Scooter.Persistence
{
	public class MockConfirmDispatchedResultInterest : IConfirmDispatchedResultInterest
	{
		public void ConfirmDispatchedResultedIn(Result result, string dispatchId)
		{
			throw new NotImplementedException();
		}
	}
}