// Copyright © 2012-2021 VLINGO LABS. All rights reserved.
//
// This Source Code Form is subject to the terms of the
// Mozilla Public License, v. 2.0. If a copy of the MPL
// was not distributed with this file, You can obtain
// one at https://mozilla.org/MPL/2.0/.

using System;
using Vlingo.Xoom.Actors;
using Vlingo.Xoom.Lattice.Model;
using Vlingo.Xoom.Symbio;
using Vlingo.Xoom.Symbio.Store.Journal;
using Vlingo.Xoom.Symbio.Store.Journal.InMemory;
using Xunit;
using Xunit.Abstractions;

namespace Vlingo.Xoom.Turbo.Tests.Scooter.Persistence
{
	public class JournalRepositoryTest
	{
		private readonly EntryAdapter _adapter;
		private readonly EntryAdapterProvider _adapterProvider;
		private readonly World _world;
		private readonly MockDispatcher<string, SnapshotState> _dispatcher;
		private readonly IJournal<string> _journal;

		public JournalRepositoryTest()//ITestOutputHelper outputHelper)
		{
			// var converter = new Converter(outputHelper);
			// Console.SetOut(converter);
			
			_world = World.StartWithDefaults("repo-test");
			_dispatcher = new MockDispatcher<string, SnapshotState>(new MockConfirmDispatchedResultInterest());

			_journal = Journal<string>.Using<InMemoryJournalActor<string>>(_world.Stage, _dispatcher);
			
			_adapter = new DefaultTextEntryAdapter<Test1Happened>();

			_adapterProvider = EntryAdapterProvider.Instance(_world);

			_adapterProvider.RegisterAdapter(_adapter);
		}

		[Fact]
		public void TestThatAppendWaits()
		{
			var repository = new TestEntityRepository(_journal, _adapterProvider);
			var id = "123";
			var entity1 = new TestEntity(id);

			Assert.False(entity1.Test1);
			Assert.False(entity1.Test2);

			entity1.DoTest1();
			Assert.True(typeof(Test1Happened) == entity1.Applied().SourceTypeAt(0));
			Assert.True(entity1.Test1);
			Assert.False(entity1.Test2);
			
			repository.Save(entity1);

			var entity2 = repository.TestOf(id);
		}
	}
}