// Copyright © 2012-2022 VLINGO LABS. All rights reserved.
//
// This Source Code Form is subject to the terms of the
// Mozilla Public License, v. 2.0. If a copy of the MPL
// was not distributed with this file, You can obtain
// one at https://mozilla.org/MPL/2.0/.

using System;
using Vlingo.Xoom.Actors;
using Vlingo.Xoom.Turbo.Annotation.Codegen.Storage;
using Xunit;

namespace Vlingo.Xoom.Turbo.Tests.Storage
{
    public class StoreActorBuilderTest : IDisposable
    {
        private readonly World _world;

        public StoreActorBuilderTest() => _world = World.StartWithDefaults("store-actor-build-tests");

        [Fact]
        public void TestThatMySqlJournalActorIsBuilt()
        {
            // var journal = StoreActorBuilder.From(_world.Stage, new Model(ModelType.Command.ToString()),
            //   new MockDispatcher<,>(),
            //   StorageType.Journal, DefaultDatabaseProperties(Codegen.Template.Storage.ModelType.Command), false);
        }

        private Properties DefaultDatabaseProperties(ModelType modelType)
        {
            var prefix = ModelType.Query.Equals(modelType) ? "query." : "";
            var properties = new Properties();
            properties.SetProperty(prefix + "database", "MYSQL");
            properties.SetProperty(prefix + "database.name", "STORAGE_TEST");
            properties.SetProperty(prefix + "database.url", "jdbc:mysql://localhost:2215/");
            properties.SetProperty(prefix + "database.driver", "com.mysql.cj.jdbc.Driver");
            properties.SetProperty(prefix + "database.username", "xoom_test");
            properties.SetProperty(prefix + "database.password", "vlingo123");
            properties.SetProperty(prefix + "database.originator", "MAIN");
            return properties;
        }

        public void Dispose() => _world.Terminate();
    }
}