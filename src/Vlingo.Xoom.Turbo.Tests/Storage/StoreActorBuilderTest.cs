// Copyright © 2012-2023 VLINGO LABS. All rights reserved.
//
// This Source Code Form is subject to the terms of the
// Mozilla Public License, v. 2.0. If a copy of the MPL
// was not distributed with this file, You can obtain
// one at https://mozilla.org/MPL/2.0/.

using System;
using Vlingo.Xoom.Actors;
using Vlingo.Xoom.Symbio.Store.Journal;
using Vlingo.Xoom.Symbio.Store.State;
using Vlingo.Xoom.Turbo.Storage;
using Vlingo.Xoom.Turbo.Annotation.Persistence;
using Xunit;

namespace Vlingo.Xoom.Turbo.Tests.Storage;

public class StoreActorBuilderTest : IDisposable
{
    private readonly World _world;

    public StoreActorBuilderTest() => _world = World.StartWithDefaults("store-actor-build-tests");

    [Fact]
    public void TestThatInMemoryJournalActorIsBuilt() {
        var journal = StoreActorBuilder.From<IJournal>(_world.Stage, new Model(ModelType.Command.ToString()),
                new MockDispatcher(), StorageType.Journal, InMemoryDatabaseProperties(), false);

        Assert.NotNull(journal);
    }

    [Fact]
    public void TestThatInMemoryStateStoreActorIsBuilt() {
        var stateStore =
            StoreActorBuilder.From<IStateStore>(_world.Stage, new Model(ModelType.Command.ToString()),
                new MockDispatcher(), StorageType.StateStore, InMemoryDatabaseProperties(), false);

        Assert.NotNull(stateStore);
    }

    private Properties InMemoryDatabaseProperties() {
        var properties = new Properties();
        properties.SetProperty("database.category", "InMemory");
        return properties;
    }

    public void Dispose() => _world.Terminate();
}