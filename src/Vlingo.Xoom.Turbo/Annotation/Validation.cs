// Copyright © 2012-2021 VLINGO LABS. All rights reserved.
//
// This Source Code Form is subject to the terms of the
// Mozilla Public License, v. 2.0. If a copy of the MPL
// was not distributed with this file, You can obtain
// one at https://mozilla.org/MPL/2.0/.

using System;

namespace Vlingo.Xoom.Turbo.Annotation
{
	public abstract class Validation
	{
		public abstract void Validate(ProcessingEnvironment processingEnvironment, Type annotation,
			AnnotatedElements annotatedElements);

		public static Action<ProcessingEnvironment, Type, AnnotatedElements> SingularityValidation() => (ProcessingEnvironment processingEnvironment, Type annotation, AnnotatedElements annotatedElements) =>
		{
			if (annotatedElements.Count(annotation) > 1)
				throw new ProcessingAnnotationException($"Only one class should be annotated with {annotation.FullName}");
		};
	};
}

public class ProcessingAnnotationException : Exception
{
	public ProcessingAnnotationException(string message)
	{
		throw new NotImplementedException();
	}
}