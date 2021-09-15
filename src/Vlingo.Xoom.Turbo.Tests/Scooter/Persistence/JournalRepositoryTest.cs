using Vlingo.Xoom.Actors;
using Vlingo.Xoom.Symbio;
using Vlingo.Xoom.Symbio.Store.Journal;
using Vlingo.Xoom.Symbio.Store.Journal.InMemory;
using Vlingo.Xoom.Turbo.Scooter.Model.Sourced;
using Xunit;

namespace Vlingo.Xoom.Turbo.Tests.Scooter.Persistence
{
	public class JournalRepositoryTest
	{
		private readonly EntryAdapter _adapter;
		private readonly EntryAdapterProvider _adapterProvider;
		private readonly World _world;
		private readonly MockDispatcher<string, SnapshotState> _dispatcher;
		private readonly IJournal<string> _journal;

		public JournalRepositoryTest()
		{
			_world = World.StartWithDefaults("repo-test");
			_dispatcher = new MockDispatcher<string, SnapshotState>(new MockConfirmDispatchedResultInterest());

			_journal = Journal<string>.Using<InMemoryJournalActor<string>>(_world.Stage, _dispatcher);
		}

		[Fact]
		public void TestThatAppendWaits()
		{
			var repository = new TestEntityRepository(_journal, _adapterProvider);
			var id = "123";
			var entity1 = new TestEntity(id);

			Assert.False(entity1.Test1);
			Assert.False(entity1.Test2);
		}

		public class TestEntity : EventSourcedEntity
		{
			public bool Test1 { get; set; }
			public bool Test2 { get; set; }
			private readonly string _id;

			public TestEntity(string id)
			{
				_id = id;
			}

			public override string Id() => StreamName();

			protected override string StreamName() => _id;
		}
	}
}