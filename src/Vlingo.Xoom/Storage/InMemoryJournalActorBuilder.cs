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
using Vlingo.Symbio.Store.Journal;
using Vlingo.Symbio.Store.Journal.InMemory;

namespace Vlingo.Xoom.Storage
{
    public class InMemoryJournalActorBuilder<T> : IStoreActorBuilder<T> where T : class
    {
        public T Build(Stage stage, IEnumerable<IDispatcher<Dispatchable<IEntry<T>, IState>>> dispatchers) => 
            (T)Journal<T>.Using<Actor, IEntry<T>, IState>(stage, dispatchers, typeof(InMemoryJournalActor<T>));

        public bool Support(DatabaseType databaseType) => databaseType.IsInMemory;
    }
}
