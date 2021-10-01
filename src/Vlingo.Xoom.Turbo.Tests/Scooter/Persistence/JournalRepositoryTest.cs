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
		private readonly EntryAdapterProvider _adapterProvider;
		private readonly IJournal<string> _journal;

		public JournalRepositoryTest(ITestOutputHelper outputHelper)
		{
			var converter = new Converter(outputHelper);
			Console.SetOut(converter);
			
			var world = World.StartWithDefaults("repo-test");
			var dispatcher = new MockDispatcher<string, SnapshotState>(new MockConfirmDispatchedResultInterest());

			_journal = Journal<string>.Using<InMemoryJournalActor<string>>(world.Stage, dispatcher);
			
			var adapter = new DefaultTextEntryAdapter<DomainEvent>();

			_adapterProvider = EntryAdapterProvider.Instance(world);

			_adapterProvider.RegisterAdapter(adapter);
		}

		[Fact(Skip = "WIP")]
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
			Assert.Equal(entity1.Id(), entity2.Id());
			Assert.True(entity2.Test1);
			Assert.False(entity2.Test2);

			entity2.DoTest2();

			repository.Save(entity2);
			//
			// var entity3 = repository.TestOf(id);
			//
			// Assert.Equal(entity1.Id(), entity3.Id());
			// Assert.True(entity3.Test1);
			// Assert.True(entity3.Test2);
		}
	}
}