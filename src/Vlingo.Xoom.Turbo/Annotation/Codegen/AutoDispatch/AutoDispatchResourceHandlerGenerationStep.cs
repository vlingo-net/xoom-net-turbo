using System.Collections.Generic;
using Vlingo.Xoom.Turbo.Codegen;
using Vlingo.Xoom.Turbo.Codegen.Template;
using Vlingo.Xoom.Turbo.Codegen.Template.Autodispatch;

namespace Vlingo.Xoom.Turbo.Annotation.Codegen.AutoDispatch
{
	public class AutoDispatchResourceHandlerGenerationStep : TemplateProcessingStep
	{
		protected override List<TemplateData> BuildTemplatesData(CodeGenerationContext context) =>
			AutoDispatchResourceHandlerTemplateData.From(context);
	}
}