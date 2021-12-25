// Copyright © 2012-2021 VLINGO LABS. All rights reserved.
//
// This Source Code Form is subject to the terms of the
// Mozilla Public License, v. 2.0. If a copy of the MPL
// was not distributed with this file, You can obtain
// one at https://mozilla.org/MPL/2.0/.

namespace Vlingo.Xoom.Turbo.Tests.Annotation.Initializer
{
	[Turbo.Annotation.Initializer.Xoom(Name ="annotated-boot")]
	[Turbo.Annotation.Initializer.ResourceHandlers(Packages =new[] { "Vlingo.Xoom.Turbo.Tests.Annotation.Initializer.Resources" })]
	public class AnnotatedBootTest
	{
	}
}