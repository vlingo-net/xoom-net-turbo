using System;
using System.Reflection;
using Vlingo.Xoom.Http;

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

		public static Action<ProcessingEnvironment, Type, AnnotatedElements> ModelWithoutQueryValidator() => (
			ProcessingEnvironment processingEnvironment, Type annotation, AnnotatedElements annotatedElements) =>
		{
			foreach (var rootElement in annotatedElements.ElementsWith(annotation))
			{
				if (rootElement.GetCustomAttribute<Queries>() == null)
				{
					foreach (var enclosed in rootElement.GetMembers())
					{
						var route = enclosed.GetCustomAttribute<Route>();
						if (route != null && !route.GetType().IsInterface && !route.GetType().IsClass &&
						    route.Method == Method.Get.ToString())
							throw new ProcessingAnnotationException(
								$"The class {annotation.FullName} with {route.Method} method for Route need to have Queries annotation.");
					}
				}
			}
		};

		public static Action<ProcessingEnvironment, Type, AnnotatedElements> RouteWithoutResponseValidator() => (
			ProcessingEnvironment processingEnvironment, Type annotation, AnnotatedElements annotatedElements) =>
		{
			foreach (var rootElement in annotatedElements.ElementsWith(annotation))
			{
				if (rootElement.GetCustomAttribute<Model>() == null)
				{
					foreach (var enclosed in rootElement.GetMembers())
					{
						var routeAnnotation = enclosed.GetCustomAttribute<Route>();
						var hasMethods = !routeAnnotation.GetType().IsInterface && !routeAnnotation.GetType().IsClass &&
						                 (routeAnnotation.Method == Method.Post.ToString() ||
						                  routeAnnotation.Method == Method.Put.ToString()
						                  || routeAnnotation.Method == Method.Patch.ToString() ||
						                  routeAnnotation.Method == Method.Delete.ToString());
						if (hasMethods && enclosed.GetCustomAttribute<ResponseAdapter>() == null)
						{
							throw new ProcessingAnnotationException(
								$"The class {annotation.FullName} with {routeAnnotation.Method} method for Route need to have Response annotation.");
						}
					}
				}
			}
		};
	}
}