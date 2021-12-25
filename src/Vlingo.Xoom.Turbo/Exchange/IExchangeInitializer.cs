using Vlingo.Xoom.Symbio.Store.Dispatch;
using IDispatcher = Vlingo.Xoom.Symbio.Store.Dispatch.IDispatcher;

namespace Vlingo.Xoom.Turbo.Exchange
{
	public abstract class ExchangeInitializer
	{
		public abstract void Init(object grid);
		public IDispatcher Dispatcher { get; } = new NoOpDispatcher();
	}
}