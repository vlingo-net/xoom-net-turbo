// Copyright © 2012-2021 VLINGO LABS. All rights reserved.
//
// This Source Code Form is subject to the terms of the
// Mozilla Public License, v. 2.0. If a copy of the MPL
// was not distributed with this file, You can obtain
// one at https://mozilla.org/MPL/2.0/.

using System.Collections.Generic;
using Vlingo.Xoom.Actors;
using Vlingo.Xoom.Symbio;
using Vlingo.Xoom.Symbio.Store.State;
using Vlingo.Xoom.Symbio.Store.State.InMemory;
using Vlingo.Xoom.Turbo.Annotation.Persistence;
using IDispatcher = Vlingo.Xoom.Symbio.Store.Dispatch.IDispatcher;

namespace Vlingo.Xoom.Turbo.Storage
{
    public class InMemoryStateStoreActorBuilder<T> : IStoreActorBuilder<T>
    {
        public T Build(Stage stage, IEnumerable<IDispatcher> dispatchers) => 
            stage.ActorFor<T>(typeof(IStateStore), typeof(InMemoryStateStoreActor<IState>), dispatchers);

        public bool Support(DatabaseType databaseType)
        {
            throw new System.NotImplementedException();
        }

        public bool Support(StorageType storageType, DatabaseCategory databaseType)
        {
            //databaseType.IsInMemory;
            throw new System.NotImplementedException();
        }

        //public bool Support(DatabaseType databaseType) => databaseType.IsInMemory;
    }
}
