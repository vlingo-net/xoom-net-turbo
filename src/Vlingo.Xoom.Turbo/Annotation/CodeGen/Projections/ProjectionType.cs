// Copyright © 2012-2023 VLINGO LABS. All rights reserved.
//
// This Source Code Form is subject to the terms of the
// Mozilla Public License, v. 2.0. If a copy of the MPL
// was not distributed with this file, You can obtain
// one at https://mozilla.org/MPL/2.0/.

using System;
using System.ComponentModel;

namespace Vlingo.Xoom.Turbo.Annotation.Codegen.Projections;

public enum ProjectionType
{
	[Description("None")] None,
	[Description("Custom")] Custom,
	[Description("Event")] EventBased,
	[Description("Operation")] OperationBased
}

public static class ProjectionTypeExtensions
{
	public static string? Name(this ProjectionType projectionType) =>
		Enum.GetName(typeof(ProjectionType), projectionType);
		
	public static bool IsProjectionEnabled(this ProjectionType projectionType) =>
		!projectionType.Equals(ProjectionType.None);
	public static bool IsEventBased(this ProjectionType projectionType) =>
		!projectionType.Equals(ProjectionType.EventBased);
}