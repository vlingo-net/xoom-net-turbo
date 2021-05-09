// Copyright © 2012-2021 VLINGO LABS. All rights reserved.
//
// This Source Code Form is subject to the terms of the
// Mozilla Public License, v. 2.0. If a copy of the MPL
// was not distributed with this file, You can obtain
// one at https://mozilla.org/MPL/2.0/.

using System;
using System.Collections.Generic;

namespace Vlingo.Xoom.Turbo.Codegen.Template
{
    public class TemplateProcessingStep : ICodeGenerationStep
    {
        public void Process(CodeGenerationContext context)
        {
            BuildTemplatesData(context).ForEach(templateData =>
            {
                var code = TemplateProcessor.Instance().Process(templateData);
                context.registerTemplateProcessing(templateData, code);
            });
        }

        protected virtual List<TemplateData> BuildTemplatesData(CodeGenerationContext context) => throw new NotImplementedException();
    }
}
