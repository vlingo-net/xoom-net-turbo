// Copyright © 2012-2022 VLINGO LABS. All rights reserved.
//
// This Source Code Form is subject to the terms of the
// Mozilla Public License, v. 2.0. If a copy of the MPL
// was not distributed with this file, You can obtain
// one at https://mozilla.org/MPL/2.0/.

using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Vlingo.Xoom.Turbo.Annotation.Codegen.Projections;
using Vlingo.Xoom.Turbo.Codegen.Content;
using Vlingo.Xoom.Turbo.Codegen.Template;

namespace Vlingo.Xoom.Turbo.Annotation.Codegen.Storage;

public class StorageProviderTemplateData : TemplateData
{
	private readonly TemplateParameters _parameters;

	private StorageProviderTemplateData(string persistencePackage, StorageType storageType,
		ProjectionType projectionType, List<TemplateData> stateAdapterTemplatesData, IReadOnlyList<ContentBase> contents,
		ModelType modelType) =>
		_parameters = LoadParameter(persistencePackage, storageType, projectionType, stateAdapterTemplatesData, contents, modelType);

	private TemplateParameters LoadParameter(string packageName, StorageType storageType,
		ProjectionType projectionType, List<TemplateData> templatesData, IReadOnlyList<ContentBase> contents,
		ModelType modelType)
	{
		var queries = Queries.From(modelType, contents, templatesData);
		var persistentTypes = storageType.FindPersistentQualifiedTypes(modelType, contents).ToList();
		var imports = ResolveImports(modelType, storageType, contents, queries);
		var codeElementFormatter = ComponentRegistry.WithType<CodeElementFormatter>();

		return TemplateParameters.With(TemplateParameter.StorageType, storageType)
			.And(TemplateParameter.ProjectionType, projectionType)
			.And(TemplateParameter.Model, modelType)
			.And(TemplateParameter.PackageName, packageName)
			.And(TemplateParameter.Adapters, Adapter.From(templatesData))
			.And(TemplateParameter.UseProjections, projectionType.IsProjectionEnabled())
			.And(TemplateParameter.Aggregates,
				ContentQuery.FindClassNames(new TemplateStandard(TemplateStandardType.Aggregate), contents))
			.And(TemplateParameter.PersistentTypes,
				persistentTypes.Select(codeElementFormatter.SimpleNameOf).ToImmutableHashSet())
			.AndResolve(TemplateParameter.StoreProviderName,
				@params => AnnotationBasedTemplateStandard.StoreProvider.ResolveClassname(@params))
			.AddImports(imports);
	}

	private ISet<string> ResolveImports(ModelType modelType, StorageType storageType,
		IReadOnlyList<ContentBase> contents, List<Queries> queries)
	{
		var sourceClassQualifiedNames = storageType.ResolveAdaptersQualifiedName(modelType, contents);
		var persistentTypes = storageType.FindPersistentQualifiedTypes(modelType, contents);
		var queriesQualifiedNames = queries.SelectMany(@param => @param.GetQualifiedNames())
			.ToImmutableHashSet();

		var aggregateActorQualifiedNames = storageType.IsSourced()
			? ContentQuery.FindFullyQualifiedClassNames(new TemplateStandard(TemplateStandardType.Aggregate), contents)
			: new HashSet<string>();

		return new[] { sourceClassQualifiedNames, queriesQualifiedNames, aggregateActorQualifiedNames, persistentTypes }
			.SelectMany(s => s)
			.ToImmutableHashSet();
	}

	public static List<TemplateData> From(string persistencePackage, StorageType storageType,
		ProjectionType projectionType,
		List<TemplateData> stateAdapterTemplatesData, IReadOnlyList<ContentBase> contents,
		IEnumerable<ModelType> modelTypes) =>
		modelTypes.ToImmutableSortedSet()
			.Select(modelType => new StorageProviderTemplateData(persistencePackage, storageType, projectionType,
				stateAdapterTemplatesData, contents, modelType))
			.ToList<TemplateData>();

	public override TemplateParameters Parameters() => _parameters;

	public override TemplateStandard Standard() => AnnotationBasedTemplateStandard.StoreProvider;
}