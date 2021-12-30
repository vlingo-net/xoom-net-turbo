// Copyright © 2012-2021 VLINGO LABS. All rights reserved.
//
// This Source Code Form is subject to the terms of the
// Mozilla Public License, v. 2.0. If a copy of the MPL
// was not distributed with this file, You can obtain
// one at https://mozilla.org/MPL/2.0/.

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Runtime.CompilerServices;
using Vlingo.Xoom.Turbo.Codegen.Content;
using Vlingo.Xoom.Turbo.Codegen.Dialect;

namespace Vlingo.Xoom.Turbo.Annotation
{
	public abstract class AnnotationProcessor
	{
		protected AppDomain AppDomain;

		[MethodImpl(MethodImplOptions.Synchronized)]
		public void Init(AppDomain appDomain)
		{
			AppDomain = appDomain;
			ComponentRegistry.Register<CodeElementFormatter>(CodeElementFormatter.With(Dialect.C_SHARP));
		}

		public bool Process(ISet<Type> set)
		{
			var annotatedElements = AnnotatedElements.From(SupportedAnnotationClasses());

			if (annotatedElements.Exists)
			{
				try
				{
					Generate(annotatedElements);
				}
				catch (ProcessingAnnotationException exception)
				{
					PrintError(exception);
				}
			}

			return true;
		}

		protected abstract void Generate(AnnotatedElements annotatedElements);

		public abstract IEnumerable<Type> SupportedAnnotationClasses();

		private void PrintError(ProcessingAnnotationException exception) => Console.WriteLine($"ERROR: {exception.Message}");

		public ISet<string> GetSupportedAnnotationTypes() =>
			SupportedAnnotationClasses()
				.Select(type => type.Name)
				.ToImmutableHashSet();
	}
}