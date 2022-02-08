// Copyright © 2012-2021 VLINGO LABS. All rights reserved.
//
// This Source Code Form is subject to the terms of the
// Mozilla Public License, v. 2.0. If a copy of the MPL
// was not distributed with this file, You can obtain
// one at https://mozilla.org/MPL/2.0/.

using System;
using System.Collections.Generic;
using static Vlingo.Xoom.Turbo.Annotation.AutoDispatch.AutoDispatchValidations;
using static Vlingo.Xoom.Turbo.Annotation.Validation;

namespace Vlingo.Xoom.Turbo.Annotation.AutoDispatch
{
	public class AutoDispatchValidator
	{
		private static AutoDispatchValidator? _instance;

		private AutoDispatchValidator()
		{
		}

		public static AutoDispatchValidator Instance()
		{
			if (_instance == null)
			{
				_instance = new AutoDispatchValidator();
			}
			
			return _instance;
		}

		public void Validate(ProcessingEnvironment processingEnvironment, AnnotatedElements annotatedElements)
		{
			new List<Action<ProcessingEnvironment, Type, AnnotatedElements>>
					{ IsInterface(), ClassVisibilityValidation(), IsQueriesProtocolAnInterface() }
				.ForEach(validator => validator.Invoke(processingEnvironment, typeof(QueriesAttribute), annotatedElements));
		}
	}
}