// Copyright © 2012-2021 VLINGO LABS. All rights reserved.
//
// This Source Code Form is subject to the terms of the
// Mozilla Public License, v. 2.0. If a copy of the MPL
// was not distributed with this file, You can obtain
// one at https://mozilla.org/MPL/2.0/.

using System.Collections.Generic;
using System.Linq;
using Vlingo.Actors;
using Vlingo.Xoom.Actors;
using Vlingo.Xoom.Symbio;
using Vlingo.Xoom.Symbio.Store.Dispatch;
using Vlingo.Xoom.Annotation.Persistence;

namespace Vlingo.Xoom.Storage
{
    public class StoreActorBuilder
    {
        // private static readonly List<IStoreActorBuilder> _buidlers = new List<IStoreActorBuilder>() { new InMemoryStateStoreActorBuilder(), new DefaultStateStoreActorBuilder(),
        //             new InMemoryJournalActorBuilder(), new DefaultJournalActorBuilder(),
        //             new ObjectStoreActorBuilder() };
        //
        // public static T From<T>(Stage stage, Model model, IDispatcher<Dispatchable<IEntry, IState>> dispatcher, StorageType storageType, IReadOnlyDictionary<string, string> properties, bool autoDatabaseCreation) => From<T>(stage, model, new List<IDispatcher<Dispatchable<IEntry, IState>>>() { dispatcher }, storageType, properties, autoDatabaseCreation);
        //
        // public static T From<T>(Stage stage, Model model, List<IDispatcher<Dispatchable<IEntry, IState>>> dispatcher, StorageType storageType, IReadOnlyDictionary<string, string> properties, bool autoDatabaseCreation)
        // {
        //     var configuration = new DatabaseParameters(model, properties, autoDatabaseCreation);
        //     configuration.MapToConfiguration();
        //
        //     var databaseType = DatabaseType.RetrieveFromConfiguration(null);
        //
        //     return _buidlers.Where(x => x.Support(storageType, databaseType)).First().Build<T>(stage, dispatcher, null);
        // }
    }
}
