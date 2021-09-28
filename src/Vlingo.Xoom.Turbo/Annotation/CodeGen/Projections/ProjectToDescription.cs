// Copyright © 2012-2021 VLINGO LABS. All rights reserved.
//
// This Source Code Form is subject to the terms of the
// Mozilla Public License, v. 2.0. If a copy of the MPL
// was not distributed with this file, You can obtain
// one at https://mozilla.org/MPL/2.0/.

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Vlingo.Xoom.Turbo.Codegen.Parameter;

namespace Vlingo.Xoom.Turbo.Annotation.Codegen.Projections
{
	public class ProjectToDescription
	{
		private const string DefaultSourceNameInvocation = ".GetType().FullName";

		private readonly string _projectionClassName;
		private readonly bool _lastParameter;
		private readonly string _joinedTypes;

		private ProjectToDescription(int index, int numberOfProtocols, string projectionClassName,
			ProjectionType projectionType, ISet<string> sourceNames)
		{
			_projectionClassName = projectionClassName;
			_lastParameter = index == numberOfProtocols - 1;
			_joinedTypes = JoinSourceTypes(projectionType, sourceNames);
		}

		private string JoinSourceTypes(ProjectionType projectionType, ISet<string> sourceNames)
		{
			Func<string, string> mapper = (Func<string, string>) (projectionType.IsEventBased()
					? sourceName => sourceName + DefaultSourceNameInvocation
					: sourceName => $"\"{sourceName}\"");
			
			return string.Join(", ", sourceNames
				.Select(mapper)
				.ToList());
		}
		
		public static IEnumerable<ProjectToDescription> From(ProjectionType projectionType,
			List<CodeGenerationParameter> projectionActors) =>
			Enumerable.Range(0, projectionActors.Count)
				.Select(index =>
				{
					var projectionActor = projectionActors.ElementAt(index);
					var eventNames = projectionActor.RetrieveAllRelated(Label.Source)
						.Select(source => source.value)
						.Select(value => value.Trim())
						.ToImmutableHashSet();
					return new ProjectToDescription(index, projectionActors.Count, projectionActor.value, projectionType,
						eventNames);
				}).ToList();
	}
}