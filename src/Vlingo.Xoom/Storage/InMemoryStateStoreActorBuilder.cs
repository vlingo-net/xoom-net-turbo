// Copyright © 2012-2020 VLINGO LABS. All rights reserved.
//
// This Source Code Form is subject to the terms of the
// Mozilla Public License, v. 2.0. If a copy of the MPL
// was not distributed with this file, You can obtain
// one at https://mozilla.org/MPL/2.0/.

using System.Collections.Generic;
using Vlingo.Actors;
using Vlingo.Symbio;
using Vlingo.Symbio.Store.Dispatch;
using Vlingo.Symbio.Store.State;
using Vlingo.Symbio.Store.State.InMemory;

namespace Vlingo.Xoom.Storage
{
    public class InMemoryStateStoreActorBuilder<T> : IStoreActorBuilder<T> where T : class
    {
        public T Build(Stage stage, IEnumerable<IDispatcher<Dispatchable<IEntry, IState>>> dispatchers)
        {
            return stage.ActorFor<T>(typeof(IStateStore<IEntry>), typeof(InMemoryStateStoreActor<IState, IEntry>), dispatchers);
        }

        public bool Support(DatabaseType databaseType)
        {
            return databaseType.IsInMemory();
        }
    }
}
