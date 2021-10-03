// Copyright © 2012-2021 VLINGO LABS. All rights reserved.
//
// This Source Code Form is subject to the terms of the
// Mozilla Public License, v. 2.0. If a copy of the MPL
// was not distributed with this file, You can obtain
// one at https://mozilla.org/MPL/2.0/.

using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Vlingo.Xoom.Turbo.Codegen.Content;
using Vlingo.Xoom.Turbo.Codegen.Template;

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
		public static bool IsSourced(this StorageType storageType) => storageType.Equals(StorageType.Journal);

		public static string ResolveTypeRegistryObjectName(this StorageType storageType, ModelType modelType)
		{
			if (!modelType.IsQueryModel())
				return storageType.TypeRegistryObjectName();
			return "StatefulTypeRegistry";
		}

		public static ISet<string> ResolveTypeRegistryQualifiedNames(this StorageType storageType, bool useCqrs) =>
			storageType.FindRelatedStorageTypes(useCqrs)
				.Select(sType => sType.TypeRegistryQualifiedClassName())
				.ToImmutableHashSet();


		private static string TypeRegistryQualifiedClassName(this StorageType storageType)
		{
			return ComponentRegistry.WithType<CodeElementFormatter>()
				.QualifiedNameOf(storageType.TypeRegistryPackage(), storageType.TypeRegistryClassName());
		}

		public static IEnumerable<StorageType> FindRelatedStorageTypes(this StorageType storageType, bool useCqrs)
		{
			if (!storageType.IsEnabled())
				return new List<StorageType>();
			if (useCqrs || storageType.IsStateful())
				return new List<StorageType> { storageType };
			return new List<StorageType> { storageType, StorageType.StateStore };
		}

		public static bool IsEnabled(this StorageType storageType) => !storageType.Equals(StorageType.None);

		public static bool IsStateStore(this StorageType storageType) => storageType.Equals(StorageType.StateStore);
		
		public static bool IsJournal(this StorageType storageType) => storageType.Equals(StorageType.Journal);

		private static bool IsStateful(this StorageType storageType) => storageType.Equals(StorageType.StateStore);

		public static string TypeRegistryClassName(this StorageType storageType) => storageType.Equals(StorageType.Journal)
			? "SourcedTypeRegistry"
			: storageType.Equals(StorageType.StateStore)
				? "StatefulTypeRegistry"
				: "";

		public static string TypeRegistryPackage(this StorageType storageType) => storageType.Equals(StorageType.Journal)
			? "Vlingo.Xoom.Lattice.Model.Sourcing"
			: storageType.Equals(StorageType.StateStore)
				? "Vlingo.Xoom.Lattice.Model.Stateful"
				: "";

		public static TemplateStandard AdapterSourceClassStandard(this StorageType storageType) =>
			storageType.Equals(StorageType.Journal)
				? new TemplateStandard(TemplateStandardType.DomainEvent)
				: new TemplateStandard(TemplateStandardType.AggregateState);

		public static ISet<string> FindPersistentQualifiedTypes(this StorageType storageType, ModelType modelType,
			IReadOnlyList<ContentBase> contents)
		{
			if (!modelType.IsQueryModel() && !storageType.IsStateful()) return new HashSet<string>();

			var typeStandard = modelType.IsQueryModel()
				? new TemplateStandard(TemplateStandardType.DataObject)
				: new TemplateStandard(TemplateStandardType.AggregateState);

			return ContentQuery.FindFullyQualifiedClassNames(typeStandard, contents);
		}

		public static ISet<string> ResolveAdaptersQualifiedName(this StorageType storageType, ModelType modelType,
			IReadOnlyList<ContentBase> contents)
		{
			if (!modelType.RequireAdapter()) return new HashSet<string>();

			return ContentQuery.FindFullyQualifiedClassNames(storageType.AdapterSourceClassStandard(), contents);
		}

		public static string TypeRegistryObjectName(this StorageType storageType) =>
			ComponentRegistry.WithType<CodeElementFormatter>().SimpleNameToAttribute(storageType.TypeRegistryClassName());
	}
}