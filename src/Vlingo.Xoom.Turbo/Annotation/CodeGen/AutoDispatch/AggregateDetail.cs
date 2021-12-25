using System;
using System.Linq;
using Vlingo.Xoom.Turbo.Codegen.Parameter;

namespace Vlingo.Xoom.Turbo.Annotation.Codegen.AutoDispatch
{
	public class AggregateDetail
	{
		public static CodeGenerationParameter MethodWithName(CodeGenerationParameter aggregate, string methodName) =>
			FindMethod(aggregate, methodName)?? throw new ArgumentException(string.Concat("Method ", methodName, " not found"));

		private static CodeGenerationParameter? FindMethod(CodeGenerationParameter aggregate, string methodName) =>
			aggregate
				.RetrieveAllRelated(Label.AggregateMethod)
				.FirstOrDefault(method => methodName == method.value || method.value.StartsWith($"{methodName}("));
	}
}