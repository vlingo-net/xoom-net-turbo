// Copyright © 2012-2022 VLINGO LABS. All rights reserved.
//
// This Source Code Form is subject to the terms of the
// Mozilla Public License, v. 2.0. If a copy of the MPL
// was not distributed with this file, You can obtain
// one at https://mozilla.org/MPL/2.0/.

using System;
using System.Collections.Generic;
using Vlingo.Xoom.Actors;
using Vlingo.Xoom.Turbo.Annotation.Codegen.Storage;
using IDispatcher = Vlingo.Xoom.Symbio.Store.Dispatch.IDispatcher;

namespace Vlingo.Xoom.Turbo.Storage
{
	public class ObjectStoreActorBuilder : IStoreActorBuilder
	{
		public T Build<T>(Stage stage, IEnumerable<IDispatcher> dispatchers, Configuration configuration) where T : class
		{
			//TODO: Implement Object Store Actor Builder
			throw new NotSupportedException("Object Store is not supported");
		}

		public bool Support(StorageType storageType, DatabaseCategory databaseType) => storageType.IsObjectStore();
	}
}