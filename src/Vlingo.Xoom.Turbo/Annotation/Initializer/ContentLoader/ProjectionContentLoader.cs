// Copyright © 2012-2021 VLINGO LABS. All rights reserved.
//
// This Source Code Form is subject to the terms of the
// Mozilla Public License, v. 2.0. If a copy of the MPL
// was not distributed with this file, You can obtain
// one at https://mozilla.org/MPL/2.0/.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Vlingo.Xoom.Turbo.Annotation.Persistence;
using Vlingo.Xoom.Turbo.Codegen.Template;

namespace Vlingo.Xoom.Turbo.Annotation.Initializer.ContentLoader
{
	public class ProjectionContentLoader : TypeBasedContentLoader
	{
		public ProjectionContentLoader(Type annotatedClass, ProcessingEnvironment environment) : base(annotatedClass, environment)
		{
		}

		protected override TemplateStandard Standard() => new TemplateStandard(TemplateStandardType.Projection);

		protected override List<Type> RetrieveContentSource()
		{
			var projections = AnnotatedClass!.GetCustomAttribute<ProjectionsAttribute>();

			if (projections == null)
				return new List<Type>();

			return new[] { projections.Value }
				.Select(projection => TypeRetriever.From(projections, projection => (projection as ProjectionAttribute)!.Actor))
				.ToList();
		}
	}
}