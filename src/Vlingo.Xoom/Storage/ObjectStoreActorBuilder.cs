// Copyright © 2012-2021 VLINGO LABS. All rights reserved.
//
// This Source Code Form is subject to the terms of the
// Mozilla Public License, v. 2.0. If a copy of the MPL
// was not distributed with this file, You can obtain
// one at https://mozilla.org/MPL/2.0/.

using System;
using System.Collections.Generic;
using Vlingo.Actors;
using Vlingo.Xoom.Symbio;
using Vlingo.Xoom.Symbio.Store.Dispatch;
using Vlingo.Xoom.Annotation.Persistence;
using Vlingo.Xoom.Actors;
using IDispatcher = Vlingo.Xoom.Symbio.Store.Dispatch.IDispatcher;

namespace Vlingo.Xoom.Storage
{
    public class ObjectStoreActorBuilder<T> : IStoreActorBuilder<T>
    {
        public T Build(Stage stage, IEnumerable<IDispatcher> dispatchers)
        {
            //TODO: Implement Object Store Actor Builder
            throw new NotSupportedException("Object Store is not supported");
        }

        public bool Support(DatabaseType databaseType) => databaseType.IsInMemory;
    }
}
