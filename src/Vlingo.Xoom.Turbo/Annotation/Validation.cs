// Copyright © 2012-2022 VLINGO LABS. All rights reserved.
//
// This Source Code Form is subject to the terms of the
// Mozilla Public License, v. 2.0. If a copy of the MPL
// was not distributed with this file, You can obtain
// one at https://mozilla.org/MPL/2.0/.

using System;

namespace Vlingo.Xoom.Turbo.Annotation
{
	public abstract class Validation : IValidation
	{
		public abstract void Validate(ProcessingEnvironment processingEnvironment, Type annotation,
			AnnotatedElements annotatedElements);

		public static Action<ProcessingEnvironment, Type, AnnotatedElements> SingularityValidation() => (
			ProcessingEnvironment processingEnvironment, Type annotation, AnnotatedElements annotatedElements) =>
		{
			if (annotatedElements.Count(annotation) > 1)
			{
				throw new ProcessingAnnotationException($"Only one class should be annotated with {annotation.FullName}");
			}
		};

		public static Action<ProcessingEnvironment, Type, AnnotatedElements> TargetValidation() => (
			ProcessingEnvironment processingEnvironment, Type annotation, AnnotatedElements annotatedElements) =>
		{
			foreach (var rootElement in annotatedElements.ElementsWith(annotation))
			{
				if (!rootElement.IsClass)
				{
					throw new ProcessingAnnotationException($"The {annotation.FullName}");
				}
			}
		};

		public static Action<ProcessingEnvironment, Type, AnnotatedElements> ClassVisibilityValidation() => (
			ProcessingEnvironment processingEnvironment, Type annotation, AnnotatedElements annotatedElements) =>
		{
			foreach (var rootElement in annotatedElements.ElementsWith(annotation))
			{
				if (rootElement.IsNotPublic)
				{
					throw new ProcessingAnnotationException($"The class {annotation.FullName} is not public");
				}
			}
		};

		public static Action<ProcessingEnvironment, Type, AnnotatedElements> IsInterface() => (
			ProcessingEnvironment processingEnvironment, Type annotation, AnnotatedElements annotatedElements) =>
		{
			foreach (var rootElement in annotatedElements.ElementsWith(annotation))
			{
				if (!rootElement.IsInterface)
				{
					throw new ProcessingAnnotationException(
						$"The {annotation.FullName} annotation is only allowed at interface level");
				}
			}
		};
	}
}