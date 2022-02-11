// Copyright © 2012-2022 VLINGO LABS. All rights reserved.
//
// This Source Code Form is subject to the terms of the
// Mozilla Public License, v. 2.0. If a copy of the MPL
// was not distributed with this file, You can obtain
// one at https://mozilla.org/MPL/2.0/.

using System;
using System.Collections.Generic;
using Vlingo.Xoom.Turbo.Annotation.Codegen.Projections;
using Vlingo.Xoom.Turbo.Codegen;
using Vlingo.Xoom.Turbo.Codegen.Parameter;
using Vlingo.Xoom.Turbo.Codegen.Template;

namespace Vlingo.Xoom.Turbo.Annotation.Codegen.Storage;

public class StorageGenerationStep : TemplateProcessingStep
{
	protected override List<TemplateData> BuildTemplatesData(CodeGenerationContext context)
	{
		var basePackage = context.ParameterOf<string>(Label.Package);
		var useCqrs = context.ParameterOf(Label.Cqrs, x => bool.TrueString.ToLower() == x);
		var storageType = context.ParameterOf(Label.StorageType, x =>
		{
			Enum.TryParse(x, out StorageType value);
			return value;
		});
		var projectionType = context.ParameterOf(Label.ProjectionType, x =>
		{
			Enum.TryParse(x, out ProjectionType value);
			return value;
		});

		return StorageTemplateDataFactory.Build(basePackage, context.Contents(), storageType, projectionType, useCqrs);
	}

	public override bool ShouldProcess(CodeGenerationContext context) => context.ParameterOf(Label.StorageType, x =>
	{
		Enum.TryParse(x, out StorageType value);
		return value;
	}).IsEnabled();
}