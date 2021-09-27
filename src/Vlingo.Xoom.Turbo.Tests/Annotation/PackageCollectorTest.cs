// Copyright © 2012-2021 VLINGO LABS. All rights reserved.
//
// This Source Code Form is subject to the terms of the
// Mozilla Public License, v. 2.0. If a copy of the MPL
// was not distributed with this file, You can obtain
// one at https://mozilla.org/MPL/2.0/.

using Vlingo.Xoom.Turbo.Annotation;
using Xunit;

namespace Vlingo.Xoom.Turbo.Tests.Annotation
{
	public class PackageCollectorTest
	{
		[Fact]
		public void TestThatPackagesAreCollected()
		{
			var projectPath =System.IO.Directory.GetParent(System.IO.Directory.GetCurrentDirectory()!).Parent!.Parent!.FullName;;

			var packages = PackageCollector.From(projectPath, "Vlingo.Xoom.Turbo.Tests.Annotation").CollectAll();
			
			// Assert.Equal(10, packages.Count); WIP collect packages/namespaces from GetCurrentDirectory
		}
	}
}