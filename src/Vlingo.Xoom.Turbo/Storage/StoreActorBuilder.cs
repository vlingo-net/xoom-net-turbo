// Copyright © 2012-2021 VLINGO LABS. All rights reserved.
//
// This Source Code Form is subject to the terms of the
// Mozilla Public License, v. 2.0. If a copy of the MPL
// was not distributed with this file, You can obtain
// one at https://mozilla.org/MPL/2.0/.

using System.Collections.Generic;
using System.Linq;
using Vlingo.Xoom.Actors;
using Vlingo.Xoom.Symbio.Store;
using Vlingo.Xoom.Turbo.Annotation.Codegen.Storage;
using IDispatcher = Vlingo.Xoom.Symbio.Store.Dispatch.IDispatcher;

namespace Vlingo.Xoom.Turbo.Storage
{
	public class StoreActorBuilder
	{
		private static readonly List<IStoreActorBuilder> Builders =
			new List<IStoreActorBuilder>
			{
				new InMemoryStateStoreActorBuilder(), new DefaultStateStoreActorBuilder(),
				new InMemoryJournalActorBuilder(), new DefaultJournalActorBuilder(),
				new ObjectStoreActorBuilder()
			};

		static T From<T>(
			Stage stage,
			Model model,
			IDispatcher dispatcher,
			StorageType storageType,
			IReadOnlyDictionary<string, string> properties,
			bool autoDatabaseCreation) where T : class =>
			From<T>(stage, model, new List<IDispatcher> { dispatcher }, storageType, properties, autoDatabaseCreation);

		static T From<T>(
			Stage stage,
			Model model,
			List<IDispatcher> dispatcher,
			StorageType storageType,
			IReadOnlyDictionary<string, string> properties,
			bool autoDatabaseCreation) where T : class
		{
			try
			{
				var configuration = new DatabaseParameters(model, properties, autoDatabaseCreation)
					.MapToConfiguration();

				var databaseType = DatabaseType.RetrieveFromConfiguration(configuration);


				return Builders
					.First(resolver => resolver.Support(storageType, databaseType))
					.Build<T>(stage, dispatcher, configuration);
			}
			catch (StorageException)
			{
				return storageType.ResolveNoOpStore<T>(stage)!;
			}
		}
	}
}