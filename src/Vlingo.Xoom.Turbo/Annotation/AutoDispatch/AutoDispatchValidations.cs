using System;
using System.Reflection;

namespace Vlingo.Xoom.Turbo.Annotation.AutoDispatch
{
	public abstract class AutoDispatchValidations : Validation
	{
		public static Action<ProcessingEnvironment, Type, AnnotatedElements> IsQueriesProtocolAnInterface() => (
			ProcessingEnvironment processingEnvironment, Type annotation, AnnotatedElements annotatedElements) =>
		{
			foreach (var element in annotatedElements.ElementsWith(annotation))
			{
				var queries = element.GetCustomAttribute<Queries>();
				var retriever = TypeRetriever.With(processingEnvironment);
				if (retriever.IsAnInterface(queries!, Void => queries!.Protocol))
					throw new ProcessingAnnotationException(
						$"The class {annotation.FullName}. Protocol value to Queries annotation must be an interface");
			}
		};
	}
}