// Copyright © 2012-2022 VLINGO LABS. All rights reserved.
//
// This Source Code Form is subject to the terms of the
// Mozilla Public License, v. 2.0. If a copy of the MPL
// was not distributed with this file, You can obtain
// one at https://mozilla.org/MPL/2.0/.

using System;
using Vlingo.Xoom.Actors;
using Vlingo.Xoom.Actors.TestKit;
using Vlingo.Xoom.Symbio;
using Vlingo.Xoom.Symbio.Store.State;
using Vlingo.Xoom.Symbio.Store.State.InMemory;
using Xunit;
using Xunit.Abstractions;

namespace Vlingo.Xoom.Turbo.Tests.Scooter.Persistence;

public class StatefulRepositoryTest
{
	private static readonly string _storeName1 = typeof(Entity1).FullName;
	private static readonly string _storeName2 = typeof(Entity2).FullName;

	private readonly MockStateStoreDispatcher _dispatcher;
	private readonly TestWorld _testWorld;
	private readonly World _world;
	private readonly MockStateStoreResultInterest _interest;
	private EntityRepository _repository;
	private readonly IStateStore _store;

	public StatefulRepositoryTest(ITestOutputHelper outputHelper)
	{
		var converter = new Converter(outputHelper);
		Console.SetOut(converter);
			
		_testWorld = TestWorld.StartWithDefaults("test-store");
		_world = _testWorld.World;

		_interest = new MockStateStoreResultInterest();
		_dispatcher = new MockStateStoreDispatcher(_interest);

		var stateAdapterProvider = new StateAdapterProvider(_world);
		_ = new EntryAdapterProvider(_world);

		stateAdapterProvider.RegisterAdapter(new Entity1StateAdapter());

		_store = _world.ActorFor<IStateStore>(typeof(InMemoryStateStoreActor<State<string>>), new[] { _dispatcher });

		StateTypeStateStoreMap.StateTypeToStoreName(_storeName1, typeof(Entity1));
		StateTypeStateStoreMap.StateTypeToStoreName(_storeName2, typeof(Entity2));
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
			
		var entity2Id = "456";
		var entity2_1 = new Entity2(entity2Id, "789");
			
		_repository.Save(entity2_1);
		var entity2_2 = _repository.Entity2Of(entity2Id);
		Assert.Equal(entity2_1, entity2_2);
		Assert.Equal(entity2_1.Value, entity2_2.Value);
	}
}