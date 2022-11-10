// Copyright © 2012-2022 VLINGO LABS. All rights reserved.
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
using Configuration = Vlingo.Xoom.Symbio.Ado.Common.Configuration;
using IDispatcher = Vlingo.Xoom.Symbio.Store.Dispatch.IDispatcher;

namespace Vlingo.Xoom.Turbo.Storage;

public class InMemoryStateStoreActorBuilder : IStoreActorBuilder
{
	public T Build<T>(Stage stage, IEnumerable<IDispatcher> dispatchers, Configuration configuration) where T : class =>
		(stage.World.Stage.ActorFor<IStateStore>(typeof(InMemoryStateStoreActor<IState>), dispatchers, 5000L, 5000L) as T)!;

	public bool Support(StorageType storageType, DatabaseCategory databaseType) =>
		storageType.IsStateStore() && databaseType.IsInMemory();
}