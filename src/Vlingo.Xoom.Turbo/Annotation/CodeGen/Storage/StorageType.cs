// Copyright © 2012-2021 VLINGO LABS. All rights reserved.
//
// This Source Code Form is subject to the terms of the
// Mozilla Public License, v. 2.0. If a copy of the MPL
// was not distributed with this file, You can obtain
// one at https://mozilla.org/MPL/2.0/.

using System.Collections.Generic;
using Vlingo.Xoom.Turbo.Codegen.Content;

namespace Vlingo.Xoom.Turbo.Annotation.Codegen.Storage
{
	public enum StorageType
	{
		None,
		StateStore,
		ObjectStore,
		Journal
	}

	public static class StorageTypeExtensions
	{
		public static string ResolveTypeRegistryObjectName(this StorageType storageType, ModelType modelType)
		{
			if (!modelType.IsQueryModel())
				return storageType.TypeRegistryObjectName();
			return "StatefulTypeRegistry";
		}

		public static IEnumerable<StorageType> FindRelatedStorageTypes(this StorageType storageType, bool useCqrs)
		{
			if (!storageType.IsEnabled())
				return new List<StorageType>();
			if (useCqrs || storageType.IsStateful())
				return new List<StorageType> { storageType };
			return new List<StorageType> { storageType, StorageType.StateStore };
		}

		private static bool IsEnabled(this StorageType storageType) => !storageType.Equals(StorageType.None);
		
		private static bool IsStateful(this StorageType storageType) => storageType.Equals(StorageType.StateStore);

		public static string TypeRegistryClassName(this StorageType storageType) => storageType.Equals(StorageType.Journal)
			? "SourcedTypeRegistry"
			: storageType.Equals(StorageType.StateStore)
				? "StatefulTypeRegistry"
				: "";

		public static string TypeRegistryObjectName(this StorageType storageType) =>
			ComponentRegistry.WithType<CodeElementFormatter>().SimpleNameToAttribute(storageType.TypeRegistryClassName());
	}
}