// Copyright © 2012-2020 VLINGO LABS. All rights reserved.
//
// This Source Code Form is subject to the terms of the
// Mozilla Public License, v. 2.0. If a copy of the MPL
// was not distributed with this file, You can obtain
// one at https://mozilla.org/MPL/2.0/.

using System.Collections.Generic;
using Vlingo.Actors;
using Vlingo.Symbio.Store.Journal;
using Vlingo.Symbio.Store.Journal.InMemory;
using IDispatcher = Vlingo.Symbio.Store.Dispatch.IDispatcher;

namespace Vlingo.Xoom.Storage
{
    public class InMemoryJournalActorBuilder<T> : IStoreActorBuilder<T> where T : class
    {
        public T Build(Stage stage, IEnumerable<IDispatcher> dispatchers) => 
            (T)Journal<T>.Using<Actor>(stage, dispatchers, typeof(InMemoryJournalActor<T>));

        public bool Support(DatabaseType databaseType) => databaseType.IsInMemory;
    }
}
