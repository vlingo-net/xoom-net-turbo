// Copyright © 2012-2021 VLINGO LABS. All rights reserved.
//
// This Source Code Form is subject to the terms of the
// Mozilla Public License, v. 2.0. If a copy of the MPL
// was not distributed with this file, You can obtain
// one at https://mozilla.org/MPL/2.0/.

using System.Collections.Generic;
using System.Linq;
using Vlingo.Xoom.Turbo.Annotation.Codegen.AutoDispatch;
using Vlingo.Xoom.Turbo.Codegen.Parameter;
using Vlingo.Xoom.Turbo.Codegen.Template.Model;

namespace Vlingo.Xoom.Turbo.Codegen.Template.Autodispatch
{
    public class AutoDispatchHandlerEntryTemplateData : TemplateData
    {
        private readonly TemplateParameters? _parameters;

        public static IEnumerable<TemplateData> From(CodeGenerationParameter aggregate) => aggregate.RetrieveAllRelated(Label.RouteSignature).Where(route => !route.HasAny(Label.ReadOnly)).Select(x => new AutoDispatchHandlerEntryTemplateData());

        public AutoDispatchHandlerEntryTemplateData()
        {
        }

        private AutoDispatchHandlerEntryTemplateData(CodeGenerationParameter route)
        {
            var aggregate = route.Parent(Label.Aggregate);
            var method = AggregateDetail.MethodWithName(aggregate, route.value);
            var factoryMethod = method.RetrieveRelatedValue(Label.FactoryMethod, x => bool.TrueString.ToLower());

            //TODO: TemplateStandartType enum methods
            _parameters =
                    TemplateParameters.With(TemplateParameter.MethodName, route.value)
                            .And(TemplateParameter.FactoryMethod, factoryMethod)
                            .And(TemplateParameter.AggregateProtocolName, aggregate.value)
                            .And(TemplateParameter.DataObjectName, TemplateStandardType.DataObject)
                            .And(TemplateParameter.AggregateProtocolVariable, Content.ClassFormatter.SimpleNameToAttribute(aggregate.value))
                            .And(TemplateParameter.StateName, TemplateStandardType.AggregateState)
                            .And(TemplateParameter.IndexName, AutoDispatchMappingValueFormatter.Format(route.value))
                            .And(TemplateParameter.MethodInvocationParameters, ResolveMethodInvocationParameters(method));
        }

        private string ResolveMethodInvocationParameters(CodeGenerationParameter method)
        {
            var factoryMethod = method.RetrieveRelatedValue(Label.FactoryMethod, x => x == bool.TrueString.ToLower());
            var methodScope = factoryMethod ? MethodScopeType.Static : MethodScopeType.Instance;
            return new AggregateArgumentsFormat.MethodInvocation("$stage", "data").Format(method, methodScope);
        }

        public override TemplateParameters Parameters() => _parameters!;

        public override TemplateStandard Standard() => new TemplateStandard(TemplateStandardType.AutoDispatchHandlerEntry);
    }
}
