// Copyright © 2012-2021 VLINGO LABS. All rights reserved.
//
// This Source Code Form is subject to the terms of the
// Mozilla Public License, v. 2.0. If a copy of the MPL
// was not distributed with this file, You can obtain
// one at https://mozilla.org/MPL/2.0/.

using System.Collections.Generic;
using System.Linq;
using Vlingo.Xoom.Codegen.Parameter;

namespace Vlingo.Xoom.Codegen.Template.Autodispatch
{
    public class AutoDispatchMappingGenerationStep : TemplateProcessingStep
    {
        protected List<TemplateData> BuildTemplatesData(CodeGenerationContext context)
        {
            var queriesTemplateData = context.TemplateParametersOf(TemplateStandardType.QueriesActor);
            return AutoDispatchMappingTemplateDataFactory.Build(context.Parameters(), queriesTemplateData.ToList(), context.Contents().ToList());
        }

        public bool ShouldProcess(CodeGenerationContext context) => context.HasParameter(Label.UseAutoDispatch) && context.ParameterOf(Label.UseAutoDispatch, x => bool.TrueString.ToLower() == x);
    }
}
