// Copyright © 2012-2021 VLINGO LABS. All rights reserved.
//
// This Source Code Form is subject to the terms of the
// Mozilla Public License, v. 2.0. If a copy of the MPL
// was not distributed with this file, You can obtain
// one at https://mozilla.org/MPL/2.0/.

using System;
using System.Linq;
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
				var queries = element.GetCustomAttribute<QueriesAttribute>();
				var retriever = TypeRetriever.With(processingEnvironment);
				if (retriever.IsAnInterface(queries, _ => queries!.Protocol!))
					throw new ProcessingAnnotationException(
						$"Class {annotation.FullName}. Protocol value to Queries annotation must be an interface");
			}
		};

		public static Action<ProcessingEnvironment, Type, AnnotatedElements> ModelWithoutQueryValidator() => (
			ProcessingEnvironment processingEnvironment, Type annotation, AnnotatedElements annotatedElements) =>
		{
			foreach (var rootElement in annotatedElements.ElementsWith(annotation))
			{
				if (rootElement.GetCustomAttribute<QueriesAttribute>() == null)
				{
					foreach (var enclosed in rootElement.GetMethods())
					{
						var route = enclosed.GetCustomAttribute<RouteAttribute>();
						if (route != null && !route.GetType().IsInterface && !route.GetType().IsClass &&
						    route.Method == Method.Get)
							throw new ProcessingAnnotationException(
								$"Class {annotation.FullName}. The class with {route.Method} method for Route need to have Queries annotation.");
					}
				}
			}
		};

		public static Action<ProcessingEnvironment, Type, AnnotatedElements> RouteWithoutResponseValidator() => (
			ProcessingEnvironment processingEnvironment, Type annotation, AnnotatedElements annotatedElements) =>
		{
			foreach (var rootElement in annotatedElements.ElementsWith(annotation))
			{
				if (rootElement.GetCustomAttribute<ModelAttribute>() == null)
				{
					foreach (var enclosed in rootElement.GetMethods())
					{
						var routeAnnotation = enclosed.GetCustomAttribute<RouteAttribute>();
						var hasMethods = !routeAnnotation!.GetType().IsInterface && !routeAnnotation.GetType().IsClass &&
						                 (routeAnnotation.Method == Method.Post ||
						                  routeAnnotation.Method == Method.Put ||
						                  routeAnnotation.Method == Method.Patch ||
						                  routeAnnotation.Method == Method.Delete);
						if (hasMethods && enclosed.GetCustomAttribute<ResponseAdapterAttribute>() == null)
						{
							throw new ProcessingAnnotationException(
								$"Class {annotation.FullName}. The class with {routeAnnotation.Method} method for Route need to have Response annotation.");
						}
					}
				}
			}
		};

		public static Action<ProcessingEnvironment, Type, AnnotatedElements> BodyForRouteValidator() => (
			ProcessingEnvironment processingEnvironment, Type annotation, AnnotatedElements annotatedElements) =>
		{
			foreach (var rootElement in annotatedElements.ElementsWith(annotation))
			{
				foreach (var enclosed in rootElement.GetMethods())
				{
					var routeAnnotation = enclosed.GetCustomAttribute<RouteAttribute>();
					if (routeAnnotation != null && !routeAnnotation.GetType().IsInterface && !routeAnnotation.GetType().IsClass &&
					    routeAnnotation.Method == Method.Get)
					{
						if (enclosed.GetParameters().Any(methodParams =>
							methodParams.IsIn && methodParams.GetCustomAttribute<BodyAttribute>() == null))
						{
							throw new ProcessingAnnotationException(
								$"Class {annotation.FullName}. Body annotation is not allowed with {routeAnnotation.Method} as method parameter for Route annotation.");
						}
					}
				}
			}
		};

		public static Action<ProcessingEnvironment, Type, AnnotatedElements> RouteHasQueryOrModelValidator() => (
			ProcessingEnvironment processingEnvironment, Type annotation, AnnotatedElements annotatedElements) =>
		{
			foreach (var rootElement in annotatedElements.ElementsWith(annotation))
			{
				var queriesAnnotation = rootElement.GetCustomAttribute<QueriesAttribute>();
				var modelAnnotation = rootElement.GetCustomAttribute<BodyAttribute>();
				if (queriesAnnotation == null && modelAnnotation == null)
					throw new ProcessingAnnotationException(
						$"Class {annotation.FullName}. To use Route annotation you need to use Queries or Model annotation on the Class level.");
			}
		};

		public static Action<ProcessingEnvironment, Type, AnnotatedElements> HandlerWithoutValidMethodValidator() => (
			ProcessingEnvironment processingEnvironment, Type annotation, AnnotatedElements annotatedElements) =>
		{
			foreach (var rootElement in annotatedElements.ElementsWith(annotation))
			{
				//TODO: Implement Handler Without Valid Method Validator 
			}
		};
	}
}