using Moq;
using Vlingo.Xoom.Turbo.Annotation;
using Vlingo.Xoom.Turbo.Annotation.CodeGen.Storage;
using Xunit;

namespace Vlingo.Xoom.Turbo.Tests.Annotation.AutoDispatch
{
	public class QueriesValidationTest
	{
		[Fact]
		public void TestThatSingularityValidationPasses()
		{
			var mockAnnotatedElements = new Mock<AnnotatedElements>();
			mockAnnotatedElements.Setup(s => s.Count(It.IsAny<Queries>())).Returns(1);

			Validation.SingularityValidation().Invoke(new Mock<ProcessingEnvironment>().Object,
				typeof(Turbo.Annotation.Initializer.Xoom), mockAnnotatedElements.Object);
		}
	}
}