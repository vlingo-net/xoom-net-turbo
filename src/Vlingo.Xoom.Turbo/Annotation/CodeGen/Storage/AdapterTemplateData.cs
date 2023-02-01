// Copyright © 2012-2023 VLINGO LABS. All rights reserved.
//
// This Source Code Form is subject to the terms of the
// Mozilla Public License, v. 2.0. If a copy of the MPL
// was not distributed with this file, You can obtain
// one at https://mozilla.org/MPL/2.0/.

using System.Collections.Generic;
using System.Linq;
using Vlingo.Xoom.Turbo.Codegen.Content;
using Vlingo.Xoom.Turbo.Codegen.Template;
using static Vlingo.Xoom.Turbo.Codegen.Template.TemplateParameter;

namespace Vlingo.Xoom.Turbo.Annotation.Codegen.Storage;

public class AdapterTemplateData : TemplateData
{
	private readonly TemplateParameters _parameters;
	private readonly string _sourceClassName;
	private readonly TemplateStandard _sourceClassStandard;

	private AdapterTemplateData(string sourceClassName, TemplateStandard sourceClassStandard, string persistencePackage,
		StorageType storageType, IReadOnlyList<ContentBase> contents)
	{
		_sourceClassName = sourceClassName;
		_sourceClassStandard = sourceClassStandard;
		_parameters = LoadParameters(persistencePackage, storageType, contents);
	}

	private TemplateParameters LoadParameters(string packageName, StorageType storageType,
		IReadOnlyList<ContentBase> contents)
	{
		var sourceQualifiedClassName =
			ContentQuery.FindFullyQualifiedClassName(_sourceClassStandard, _sourceClassName, contents);

		return TemplateParameters.With(PackageName, packageName)
			.And(SourceName, _sourceClassName)
			.And(AdapterName, AnnotationBasedTemplateStandard.Adapter.ResolveClassname(_sourceClassName))
			.And(TemplateParameter.StorageType, storageType)
			.AddImport(sourceQualifiedClassName);
	}

	public static List<AdapterTemplateData> From(string persistencePackage, StorageType storageType,
		IReadOnlyList<ContentBase> contents) =>
		ContentQuery.FindClassNames(storageType.AdapterSourceClassStandard(), contents)
			.Select(sourceClassName => new AdapterTemplateData(sourceClassName, storageType.AdapterSourceClassStandard(),
				persistencePackage, storageType, contents))
			.ToList();

	public override TemplateParameters Parameters() => _parameters;

	public override TemplateStandard Standard() => AnnotationBasedTemplateStandard.Adapter;
}