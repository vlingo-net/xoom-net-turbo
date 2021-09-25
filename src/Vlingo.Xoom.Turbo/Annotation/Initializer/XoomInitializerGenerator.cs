// Copyright © 2012-2021 VLINGO LABS. All rights reserved.
//
// This Source Code Form is subject to the terms of the
// Mozilla Public License, v. 2.0. If a copy of the MPL
// was not distributed with this file, You can obtain
// one at https://mozilla.org/MPL/2.0/.

namespace Vlingo.Xoom.Turbo.Annotation.Initializer
{
	public class XoomInitializerGenerator
	{
		private static XoomInitializerGenerator _instance;

		private XoomInitializerGenerator()
		{
		}

		public static XoomInitializerGenerator Instance()
		{
			if (_instance == null)
				_instance = new XoomInitializerGenerator();
			return _instance;
		}

		public void GenerateForm(ProcessingEnvironment environment, AnnotatedElements annotatedElements)
		{
			var basePackage = XoomInitializerPackage.From(environment, annotatedElements);

			var context =
				CodeGenerationContextLoader.From(environment.GetFiler(), basePackage, annotatedElements, environment);
		}
	}
}