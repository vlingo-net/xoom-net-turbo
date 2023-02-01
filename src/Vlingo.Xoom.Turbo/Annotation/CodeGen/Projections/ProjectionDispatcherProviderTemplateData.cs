// Copyright © 2012-2023 VLINGO LABS. All rights reserved.
//
// This Source Code Form is subject to the terms of the
// Mozilla Public License, v. 2.0. If a copy of the MPL
// was not distributed with this file, You can obtain
// one at https://mozilla.org/MPL/2.0/.

using System.Collections.Generic;
using Vlingo.Xoom.Turbo.Codegen.Content;
using Vlingo.Xoom.Turbo.Codegen.Parameter;
using Vlingo.Xoom.Turbo.Codegen.Template;

namespace Vlingo.Xoom.Turbo.Annotation.Codegen.Projections;

public class ProjectionDispatcherProviderTemplateData : TemplateData
{
	private readonly TemplateParameters _parameters = null!;

	public ProjectionDispatcherProviderTemplateData(ProjectionType projectionType,
		IEnumerable<CodeGenerationParameter> projectionActors, IReadOnlyList<ContentBase> contents)
	{
	}

	public override TemplateParameters Parameters() => _parameters;

	public override TemplateStandard Standard() => AnnotationBasedTemplateStandard.ProjectionDispatcherProvider;
}