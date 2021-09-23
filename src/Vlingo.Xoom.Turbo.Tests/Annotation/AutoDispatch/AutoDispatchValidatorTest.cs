// Copyright © 2012-2021 VLINGO LABS. All rights reserved.
//
// This Source Code Form is subject to the terms of the
// Mozilla Public License, v. 2.0. If a copy of the MPL
// was not distributed with this file, You can obtain
// one at https://mozilla.org/MPL/2.0/.

using System;
using System.Collections.Generic;
using Moq;
using Vlingo.Xoom.Turbo.Annotation;
using Vlingo.Xoom.Turbo.Annotation.CodeGen.Storage;
using Xunit;

namespace Vlingo.Xoom.Turbo.Tests.Annotation.AutoDispatch
{
	public class AutoDispatchValidatorTest
	{
		[Fact(Skip = "WIP")]
		public void TestIsInterface()
		{
			var mockAnnotatedElements = new Mock<AnnotatedElements>();
			var mockRootElement = new Mock<Type>();
			var elements = new HashSet<Type> { mockRootElement.Object };

			mockAnnotatedElements
				.Setup(s => s.ElementsWith(It.IsAny<Queries>()))
				.Returns(elements);

			Validation.IsInterface().Invoke(new Mock<ProcessingEnvironment>().Object, typeof(Queries), mockAnnotatedElements.Object);
		}
	}
}