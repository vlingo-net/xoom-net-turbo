// Copyright © 2012-2020 VLINGO LABS. All rights reserved.
//
// This Source Code Form is subject to the terms of the
// Mozilla Public License, v. 2.0. If a copy of the MPL
// was not distributed with this file, You can obtain
// one at https://mozilla.org/MPL/2.0/.

using System;
using System.Collections.Generic;
using Vlingo.Actors;
using Vlingo.Symbio;
using Vlingo.Symbio.Store.Dispatch;
using Vlingo.Xoom.Annotation.Persistence;

namespace Vlingo.Xoom.Storage
{
    public class DefaultJournalActorBuilder : StoreActorBuilder, IStoreActorBuilder
    {
        public T Build<T>(Stage stage, IEnumerable<IDispatcher<Dispatchable<IEntry, IState>>> dispatchers, Configuration configuration) => throw new NotImplementedException();

        private IEnumerable<IDispatcher<Dispatchable<IEntry, IState>>> Typed(List<Type> dispatchers) => (IEnumerable<IDispatcher<Dispatchable<IEntry, IState>>>)dispatchers;

        public bool Support(StorageType storageType, DatabaseCategory databaseType) => new Persistence(storageType, string.Empty).IsJournal() && databaseType == DatabaseCategory.InMemory;
    }
}
