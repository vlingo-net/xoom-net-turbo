// Copyright © 2012-2021 VLINGO LABS. All rights reserved.
//
// This Source Code Form is subject to the terms of the
// Mozilla Public License, v. 2.0. If a copy of the MPL
// was not distributed with this file, You can obtain
// one at https://mozilla.org/MPL/2.0/.

using System;
using System.Collections.Generic;
using Vlingo.Xoom.Turbo.Codegen;
using Vlingo.Xoom.Turbo.Codegen.Content;
using Vlingo.Xoom.Turbo.Codegen.Parameter;
using Vlingo.Xoom.Turbo.Codegen.Template;

namespace Vlingo.Xoom.Turbo.Annotation.Codegen.Projections
{
	public class ProjectionDispatcherProviderGenerationStep : TemplateProcessingStep
	{
		protected override List<TemplateData> BuildTemplatesData(CodeGenerationContext context)
		{
			var projectionType = context.ParameterOf(Label.ProjectionType, x =>
			{
				Enum.TryParse(x, out ProjectionType value);
				return value;
			});

			var projectionDispatcherProviderTemplateData = new ProjectionDispatcherProviderTemplateData(projectionType,
				context.ParametersOf(Label.ProjectionActor), context.Contents());
			return new List<TemplateData> { projectionDispatcherProviderTemplateData };
		}

		public override bool ShouldProcess(CodeGenerationContext context) =>
			ContentQuery.Exists(new TemplateStandard(TemplateStandardType.Projection), context.Contents());
	}
}