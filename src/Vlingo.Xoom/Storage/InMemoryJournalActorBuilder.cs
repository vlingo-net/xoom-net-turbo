// Copyright © 2012-2020 VLINGO LABS. All rights reserved.
//
// This Source Code Form is subject to the terms of the
// Mozilla Public License, v. 2.0. If a copy of the MPL
// was not distributed with this file, You can obtain
// one at https://mozilla.org/MPL/2.0/.

using System.Collections.Generic;
using Vlingo.Xoom.Symbio.Store.Journal;
using Vlingo.Xoom.Symbio.Store.Journal.InMemory;
using Vlingo.Xoom.Annotation.Persistence;
using Vlingo.Xoom.Actors;
using IDispatcher = Vlingo.Symbio.Store.Dispatch.IDispatcher;

namespace Vlingo.Xoom.Storage
{
    public class InMemoryJournalActorBuilder : IStoreActorBuilder
    {
        public T Build<T>(Stage stage, IEnumerable<IDispatcher<Dispatchable<IEntry, IState>>> dispatchers, Configuration configuration) =>
            (T)Journal<T>.Using<Actor, IEntry<T>, IState>(stage, (IDispatcher<Dispatchable<IEntry<T>, IState>>)dispatchers, typeof(InMemoryJournalActor<T, IEntry<T>, IState>));

        public bool Support(StorageType storageType, DatabaseCategory databaseType) => databaseType == DatabaseCategory.InMemory;
    }
}
