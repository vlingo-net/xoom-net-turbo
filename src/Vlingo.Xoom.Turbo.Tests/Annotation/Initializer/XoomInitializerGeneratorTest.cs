// Copyright © 2012-2021 VLINGO LABS. All rights reserved.
//
// This Source Code Form is subject to the terms of the
// Mozilla Public License, v. 2.0. If a copy of the MPL
// was not distributed with this file, You can obtain
// one at https://mozilla.org/MPL/2.0/.

using System;
using System.IO;
using Microsoft.Win32.SafeHandles;
using Moq;
using Vlingo.Xoom.Turbo.Annotation;
using Vlingo.Xoom.Turbo.Annotation.Initializer;
using Xunit;

namespace Vlingo.Xoom.Turbo.Tests.Annotation.Initializer
{
	public class XoomInitializerGeneratorTest
	{
		[Fact(Skip = "Work on MacOS, Windows need valid IntPtr handle")]
		public void TestThatXoomInitializerGeneratorGenerates()
		{
			var mockAnnotatedElements = new Mock<AnnotatedElements>();
			var mockProcessingEnvironment = new Mock<ProcessingEnvironment>();

			mockProcessingEnvironment
				.Setup(s => s.GetFiler())
				.Returns(new Mock<FileStream>(It.IsAny<SafeFileHandle>(), FileAccess.Read, false).Object);
			
			XoomInitializerGenerator.Instance().GenerateForm(mockProcessingEnvironment.Object
				, mockAnnotatedElements.Object);
			
			
		}
	}
}