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