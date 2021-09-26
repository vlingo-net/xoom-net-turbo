using System;
using System.Collections.Generic;
using static Vlingo.Xoom.Turbo.Annotation.AutoDispatch.AutoDispatchValidations;
using static Vlingo.Xoom.Turbo.Annotation.Validation;

namespace Vlingo.Xoom.Turbo.Annotation.AutoDispatch
{
	public class AutoDispatchValidator
	{
		private static AutoDispatchValidator _instance;

		private AutoDispatchValidator()
		{
		}

		public static AutoDispatchValidator Instance()
		{
			if (_instance == null)
				_instance = new AutoDispatchValidator();
			return _instance;
		}

		public void Validate(ProcessingEnvironment processingEnvironment, AnnotatedElements annotatedElements)
		{
			new List<Action<ProcessingEnvironment, Type, AnnotatedElements>>
					{ IsInterface(), ClassVisibilityValidation(), IsQueriesProtocolAnInterface() }
				.ForEach(validator => validator.Invoke(processingEnvironment, typeof(Queries), annotatedElements));
		}
	}
}