using Vlingo.Xoom.Symbio;
using Vlingo.Xoom.Symbio.Store.Journal;
using Vlingo.Xoom.Turbo.Scooter.Model.Persistence;

namespace Vlingo.Xoom.Turbo.Tests.Scooter.Persistence
{
	public class TestEntityRepository : JournalRepository
	{
		private readonly EntryAdapterProvider _adapterProvider;
		private readonly IJournal<string> _journal;
		private readonly IStreamReader? _reader;
		public TestEntityRepository(IJournal<string> journal, EntryAdapterProvider adapterProvider)
		{
			_journal = journal;
			_reader = journal.StreamReader("TestRepository").Await();
			_adapterProvider = adapterProvider;
		}
	}
}