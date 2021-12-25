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
			var mockElementsUtil = new Mock<Type>();
			var elements = new HashSet<Type> { typeof(IQueriesTest) };

			mockAnnotatedElements
				.Setup(s => s.ElementsWith(It.IsAny<object[]>()))
				.Returns(elements);

			mockElementsUtil.Setup(s => s.GetElementType()).Returns(mockElementsUtil.Object);
			mockProcessingEnvironment.Setup(s => s.GetElementUtils()).Returns(mockElementsUtil.Object);

			IsQueriesProtocolAnInterface()
				.Invoke(mockProcessingEnvironment.Object, typeof(Queries), mockAnnotatedElements.Object);
		}

		[Fact]
		public void TestModelWithoutQueryValidator()
		{
			var mockProcessingEnvironment = new Mock<ProcessingEnvironment>();
			var mockAnnotatedElements = new Mock<AnnotatedElements>();
			var mockElementsUtil = new Mock<Type>();
			var elements = new HashSet<Type> { typeof(IQueriesTest) };

			mockAnnotatedElements
				.Setup(s => s.ElementsWith(It.IsAny<object[]>()))
				.Returns(elements);
			mockProcessingEnvironment.Setup(s => s.GetElementUtils()).Returns(mockElementsUtil.Object);

			ModelWithoutQueryValidator()
				.Invoke(mockProcessingEnvironment.Object, typeof(Turbo.Annotation.AutoDispatch.Model),
					mockAnnotatedElements.Object);
		}

		[Fact]
		public void TestBodyForRouteValidator()
		{
			var mockProcessingEnvironment = new Mock<ProcessingEnvironment>();
			var mockAnnotatedElements = new Mock<AnnotatedElements>();
			var mockElementsUtil = new Mock<Type>();
			var elements = new HashSet<Type> { typeof(IQueriesTest) };

			mockAnnotatedElements
				.Setup(s => s.ElementsWith(It.IsAny<object[]>()))
				.Returns(elements);

			mockElementsUtil.Setup(s => s.GetElementType()).Returns(mockElementsUtil.Object);
			mockProcessingEnvironment.Setup(s => s.GetElementUtils()).Returns(mockElementsUtil.Object);

			BodyForRouteValidator().Invoke(mockProcessingEnvironment.Object, typeof(Queries), mockAnnotatedElements.Object);
		}

		[Fact]
		public void TestRouteWithoutResponseValidator()
		{
			var mockProcessingEnvironment = new Mock<ProcessingEnvironment>();
			var mockAnnotatedElements = new Mock<AnnotatedElements>();
			var mockElementsUtil = new Mock<Type>();
			var elements = new HashSet<Type> { typeof(IRouteResponseTest) };

			mockAnnotatedElements
				.Setup(s => s.ElementsWith(It.IsAny<object[]>()))
				.Returns(elements);
			mockProcessingEnvironment.Setup(s => s.GetElementUtils()).Returns(mockElementsUtil.Object);

			RouteWithoutResponseValidator()
				.Invoke(mockProcessingEnvironment.Object, typeof(Turbo.Annotation.AutoDispatch.Model),
					mockAnnotatedElements.Object);
		}

		[Fact]
		public void TestRouteHasQueryOrModelValidator()
		{
			var mockProcessingEnvironment = new Mock<ProcessingEnvironment>();
			var mockAnnotatedElements = new Mock<AnnotatedElements>();
			var mockElementsUtil = new Mock<Type>();
			var elements = new HashSet<Type> { typeof(IQueriesTest) };

			mockAnnotatedElements
				.Setup(s => s.ElementsWith(It.IsAny<object[]>()))
				.Returns(elements);
			mockProcessingEnvironment.Setup(s => s.GetElementUtils()).Returns(mockElementsUtil.Object);

			RouteHasQueryOrModelValidator()
				.Invoke(mockProcessingEnvironment.Object, typeof(Route), mockAnnotatedElements.Object);
		}

		[Fact]
		public void TestHandlerWithoutValidMethodValidator()
		{
			var mockProcessingEnvironment = new Mock<ProcessingEnvironment>();
			var mockAnnotatedElements = new Mock<AnnotatedElements>();
			var mockElementsUtil = new Mock<Type>();
			var elements = new HashSet<Type> { typeof(IQueriesTest) };

			mockAnnotatedElements
				.Setup(s => s.ElementsWith(It.IsAny<object[]>()))
				.Returns(elements);
			mockProcessingEnvironment.Setup(s => s.GetElementUtils()).Returns(mockElementsUtil.Object);

			HandlerWithoutValidMethodValidator().Invoke(mockProcessingEnvironment.Object,
				typeof(Turbo.Annotation.AutoDispatch.Model), mockAnnotatedElements.Object);
		}

		[Queries(Protocol = typeof(IQueriesProtocolTest))]
		public interface IQueriesTest
		{
			[Route(Method = "GET")]
			public string TestGet();
		}

		public interface IQueriesProtocolTest
		{
		}

		public interface IRouteResponseTest
		{
			[Route(Method = "POST")]
			public string TestPost(string body);
		}
	}
}