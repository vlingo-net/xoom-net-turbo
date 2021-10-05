// Copyright © 2012-2021 VLINGO LABS. All rights reserved.
//
// This Source Code Form is subject to the terms of the
// Mozilla Public License, v. 2.0. If a copy of the MPL
// was not distributed with this file, You can obtain
// one at https://mozilla.org/MPL/2.0/.

using System;
using System.Collections.Generic;
using System.Reflection;
using Moq;
using Moq.Protected;
using Vlingo.Xoom.Turbo.Annotation;
using Vlingo.Xoom.Turbo.Annotation.AutoDispatch;
using static Vlingo.Xoom.Turbo.Annotation.Validation;
using Xunit;
using static Vlingo.Xoom.Turbo.Annotation.AutoDispatch.AutoDispatchValidations;

namespace Vlingo.Xoom.Turbo.Tests.Annotation.AutoDispatch
{
	public class AutoDispatchValidatorTest
	{
		[Fact]
		public void TestIsInterface()
		{
			var mockAnnotatedElements = new Mock<AnnotatedElements>();
			var mockRootElement = new Mock<Type>();
			var elements = new HashSet<Type> { mockRootElement.Object };

			mockAnnotatedElements
				.Setup(s => s.ElementsWith(It.IsAny<object[]>()))
				.Returns(elements);
			mockRootElement.Protected().Setup<TypeAttributes>("GetAttributeFlagsImpl").Returns(TypeAttributes.Interface);

			IsInterface().Invoke(new Mock<ProcessingEnvironment>().Object, typeof(Queries), mockAnnotatedElements.Object);
		}

		[Fact]
		public void TestClassVisibilityValidation()
		{
			var mockAnnotatedElements = new Mock<AnnotatedElements>();
			var mockRootElement = new Mock<Type>();
			var elements = new HashSet<Type> { mockRootElement.Object };

			mockAnnotatedElements
				.Setup(s => s.ElementsWith(It.IsAny<object[]>()))
				.Returns(elements);
			mockRootElement.Protected().Setup<TypeAttributes>("GetAttributeFlagsImpl").Returns(TypeAttributes.Public);

			ClassVisibilityValidation().Invoke(new Mock<ProcessingEnvironment>().Object, typeof(Queries),
				mockAnnotatedElements.Object);
		}

		[Fact]
		public void TestIsQueriesProtocolAnInterface()
		{
			var mockProcessingEnvironment = new Mock<ProcessingEnvironment>();
			var mockAnnotatedElements = new Mock<AnnotatedElements>();
			var mockRootElement = new Mock<IQueriesTest>();
			var mockElementsUtil = new Mock<Type>();
			var elements = new HashSet<Type> { mockRootElement.Object.GetType() };

			mockAnnotatedElements
				.Setup(s => s.ElementsWith(It.IsAny<object[]>()))
				.Returns(elements);

			mockElementsUtil.Setup(s => s.GetElementType()).Returns(mockElementsUtil.Object);
			mockProcessingEnvironment.Setup(s => s.GetElementUtils()).Returns(mockElementsUtil.Object);

			IsQueriesProtocolAnInterface()
				.Invoke(mockProcessingEnvironment.Object, typeof(Queries), mockAnnotatedElements.Object);
		}

		[Queries]
		public interface IQueriesTest
		{
		}
	}
}