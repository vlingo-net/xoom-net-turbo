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

namespace Vlingo.Xoom.Storage
{
    public class ObjectStoreActorBuilder<T> : IStoreActorBuilder<T>
    {
        public T Build(Stage stage, IEnumerable<IDispatcher<Dispatchable<IEntry<T>, IState>>> dispatchers)
        {
            //TODO: Implement Object Store Actor Builder
            throw new NotSupportedException("Object Store is not supported");
        }

        public bool Support(DatabaseType databaseType) => databaseType.IsInMemory;
    }
}
