// Copyright © 2012-2021 VLINGO LABS. All rights reserved.
//
// This Source Code Form is subject to the terms of the
// Mozilla Public License, v. 2.0. If a copy of the MPL
// was not distributed with this file, You can obtain
// one at https://mozilla.org/MPL/2.0/.

using System.Collections.Generic;
using System.Linq;
using Vlingo.Xoom.Turbo.Codegen.Content;
using Vlingo.Xoom.Turbo.Codegen.Parameter;

namespace Vlingo.Xoom.Turbo.Codegen.Template.Autodispatch
{
    public class AutoDispatchMappingTemplateDataFactory
    {
        public static List<TemplateData> Build(CodeGenerationParameters parameters, List<TemplateData> queriesData, List<ContentBase> contents)
        {
            var basePackage = parameters.RetrieveValue(Label.Package);
            var useCQRS = parameters.RetrieveValue(Label.Cqrs, x => bool.TrueString.ToLower() == x);
            return parameters.RetrieveAll(Label.Aggregate).SelectMany(x => new List<TemplateData>() { new AutoDispatchMappingTemplateData(basePackage, x, useCQRS, contents),
                            new AutoDispatchHandlersMappingTemplateData(basePackage, x, queriesData, contents, useCQRS) }).ToList();
        }
    }
}
