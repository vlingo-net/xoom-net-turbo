// Copyright © 2012-2022 VLINGO LABS. All rights reserved.
//
// This Source Code Form is subject to the terms of the
// Mozilla Public License, v. 2.0. If a copy of the MPL
// was not distributed with this file, You can obtain
// one at https://mozilla.org/MPL/2.0/.

using System;
using Vlingo.Xoom.Http;

namespace Vlingo.Xoom.Turbo.Annotation.AutoDispatch;

[AttributeUsage(AttributeTargets.Method)]
public class RouteAttribute : Attribute
{
	public Method Method { get; set; }

	public string Path { get; set; } = string.Empty;

	public int Handler { get; set; } = -1;
}