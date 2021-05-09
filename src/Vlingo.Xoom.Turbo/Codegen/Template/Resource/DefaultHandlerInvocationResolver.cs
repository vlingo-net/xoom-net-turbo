// Copyright © 2012-2021 VLINGO LABS. All rights reserved.
//
// This Source Code Form is subject to the terms of the
// Mozilla Public License, v. 2.0. If a copy of the MPL
// was not distributed with this file, You can obtain
// one at https://mozilla.org/MPL/2.0/.

using Vlingo.Xoom.Http;
using Vlingo.Xoom.Turbo.Codegen.Content;
using Vlingo.Xoom.Turbo.Codegen.Parameter;
using Vlingo.Xoom.Turbo.Codegen.Template.Model;

namespace Vlingo.Xoom.Turbo.Codegen.Template.Resource
{
    public class DefaultHandlerInvocationResolver : HandlerInvocationResolver, IHandlerInvocationResolver
    {
        private readonly static string _commandPattern = "{0}.{1}({2})";
        private readonly static string _queryPattern = _queriesParameter + ".{3}()";
        private readonly static string _adapterPattern = "{0}.from(state)";

        public string ResolveRouteHandlerInvocation(CodeGenerationParameter aggregateParameter, CodeGenerationParameter routeParameter)
        {
            if (routeParameter.RetrieveRelatedValue(Label.RouteMethod, Method.From).IsGet())
            {
                return ResolveQueryMethodInvocation(routeParameter.value);
            }
            return ResolveCommandMethodInvocation(aggregateParameter, routeParameter);
        }

        public string ResolveAdapterHandlerInvocation(CodeGenerationParameter aggregateParameter, CodeGenerationParameter routeSignatureParameter) => string.Format(_adapterPattern, new TemplateStandard(TemplateStandardType.DataObject).ResolveClassname(aggregateParameter.value));

        private string ResolveCommandMethodInvocation(CodeGenerationParameter aggregateParameter, CodeGenerationParameter routeParameter)
        {
            var argumentsFormat = new AggregateArgumentsFormat.MethodInvocation("stage()", "data");
            var method = AggregateDetail.MethodWithName(aggregateParameter, routeParameter.value);
            var factoryMethod = method.RetrieveRelatedValue(Label.FactoryMethod, x => bool.TrueString.ToLower() == x);
            var scope = factoryMethod ? MethodScopeType.Static : MethodScopeType.Instance;
            var methodInvocationParameters = argumentsFormat.Format(method, scope);
            var invoker = factoryMethod ? aggregateParameter.value : ClassFormatter.SimpleNameToAttribute(aggregateParameter.value);
            return string.Format(_commandPattern, invoker, method.value, methodInvocationParameters);
        }

        private string ResolveQueryMethodInvocation(string methodName) => string.Format(_queryPattern, methodName);
    }
}
