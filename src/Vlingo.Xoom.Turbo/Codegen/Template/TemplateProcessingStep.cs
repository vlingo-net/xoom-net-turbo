// Copyright © 2012-2021 VLINGO LABS. All rights reserved.
//
// This Source Code Form is subject to the terms of the
// Mozilla Public License, v. 2.0. If a copy of the MPL
// was not distributed with this file, You can obtain
// one at https://mozilla.org/MPL/2.0/.

using System.Collections.Generic;
using Vlingo.Xoom.Turbo.Codegen.Dialect;

namespace Vlingo.Xoom.Turbo.Codegen.Template
{
	public abstract class TemplateProcessingStep : ICodeGenerationStep
	{
		public void Process(CodeGenerationContext context)
		{
			var dialect = ResolveDialect(context);
			dialect.ResolvePreParametersProcessing(context.Parameters());
			BuildTemplatesData(context).ForEach(templateData =>
			{
				var code = TemplateProcessor.Instance().Process(templateData);
				context.registerTemplateProcessing(templateData, code);
			});
		}
		
		private Dialect.Dialect ResolveDialect(CodeGenerationContext context) => DialectExtensions.FindDefault();

		protected abstract List<TemplateData> BuildTemplatesData(CodeGenerationContext context);
		
		public abstract bool ShouldProcess(CodeGenerationContext context);
	}
}