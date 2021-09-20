// Copyright © 2012-2021 VLINGO LABS. All rights reserved.
//
// This Source Code Form is subject to the terms of the
// Mozilla Public License, v. 2.0. If a copy of the MPL
// was not distributed with this file, You can obtain
// one at https://mozilla.org/MPL/2.0/.

using Vlingo.Xoom.Actors;
using Vlingo.Xoom.Actors.TestKit;
using Vlingo.Xoom.Symbio;
using Vlingo.Xoom.Symbio.Store.State;
using Vlingo.Xoom.Symbio.Store.State.InMemory;
using Xunit;

namespace Vlingo.Xoom.Turbo.Tests.Scooter.Persistence
{
	public class StatefulRepositoryTest
	{
		private readonly static string STORE_NAME_1 = typeof(Entity1).FullName;
		private readonly static string STORE_NAME_2 = typeof(Entity2).FullName;

		private readonly MockStateStoreDispatcher _dispatcher;
		private readonly TestWorld _testWorld;
		private readonly World _world;
		private readonly MockStateStoreResultInterest _interest;
		private EntityRepository _repository;
		private readonly IStateStore _store;

		public StatefulRepositoryTest()
		{
			_testWorld = TestWorld.StartWithDefaults("test-store");
			_world = _testWorld.World;

			_interest = new MockStateStoreResultInterest();
			_dispatcher = new MockStateStoreDispatcher(_interest);

			var stateAdapterProvider = new StateAdapterProvider(_world);
			_ = new EntryAdapterProvider(_world);

			stateAdapterProvider.RegisterAdapter(new Entity1StateAdapter());

			_store = _world.ActorFor<IStateStore>(typeof(InMemoryStateStoreActor<State<string>>), new[] { _dispatcher });

			StateTypeStateStoreMap.StateTypeToStoreName(STORE_NAME_1, typeof(Entity1));
			_repository = new EntityRepository(_store);
		}

		[Fact]
		public void TestThatWriteReadAwaits()
		{
			_dispatcher.AfterCompleting(0);

			var entity1Id = "123";
			var entity1_1 = new Entity1(entity1Id, 123);

			_repository.Save(entity1_1);
			var entity1_2 = _repository.Entity1Of(entity1Id);
			Assert.Equal(entity1_1, entity1_2);
			Assert.Equal(entity1_1.Value, entity1_2.Value);
		}
	}


	public class Entity2
	{
	}
}