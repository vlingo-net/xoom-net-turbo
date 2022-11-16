// Copyright © 2012-2022 VLINGO LABS. All rights reserved.
//
// This Source Code Form is subject to the terms of the
// Mozilla Public License, v. 2.0. If a copy of the MPL
// was not distributed with this file, You can obtain
// one at https://mozilla.org/MPL/2.0/.

using System.Collections.Generic;
using Vlingo.Xoom.Actors;
using Vlingo.Xoom.Symbio;
using Vlingo.Xoom.Symbio.Store.Journal;
using Vlingo.Xoom.Symbio.Store.Journal.InMemory;
using Vlingo.Xoom.Turbo.Annotation.Persistence;
using Configuration = Vlingo.Xoom.Symbio.Ado.Common.Configuration;
using IDispatcher = Vlingo.Xoom.Symbio.Store.Dispatch.IDispatcher;

namespace Vlingo.Xoom.Turbo.Storage;

public class InMemoryJournalActorBuilder : IStoreActorBuilder
{
	public T Build<T>(Stage stage, IEnumerable<IDispatcher> dispatchers, Configuration configuration) where T : class =>
		(stage.ActorFor<IJournal>(typeof(InMemoryJournalActor<IState>), dispatchers) as T)!;
		
	public bool Support(StorageType storageType, DatabaseCategory databaseType) =>
		storageType.IsJournal() && databaseType.IsInMemory();
}