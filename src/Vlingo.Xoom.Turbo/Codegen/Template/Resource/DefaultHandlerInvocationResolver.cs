// Copyright © 2012-2023 VLINGO LABS. All rights reserved.
//
// This Source Code Form is subject to the terms of the
// Mozilla Public License, v. 2.0. If a copy of the MPL
// was not distributed with this file, You can obtain
// one at https://mozilla.org/MPL/2.0/.

using Vlingo.Xoom.Http;
using Vlingo.Xoom.Turbo.Annotation.Codegen.AutoDispatch;
using Vlingo.Xoom.Turbo.Codegen.Content;
using Vlingo.Xoom.Turbo.Codegen.Parameter;
using Vlingo.Xoom.Turbo.Codegen.Template.Model;

namespace Vlingo.Xoom.Turbo.Codegen.Template.Resource;

public class DefaultHandlerInvocationResolver : HandlerInvocationResolver, IHandlerInvocationResolver
{
    private readonly static string CommandPattern = "{0}.{1}({2})";
    private readonly static string QueryPattern = QueriesParameter + ".{3}()";
    private readonly static string AdapterPattern = "{0}.from(state)";

    public string ResolveRouteHandlerInvocation(CodeGenerationParameter aggregateParameter, CodeGenerationParameter routeParameter)
    {
        if (routeParameter.RetrieveRelatedValue(Label.RouteMethod, MethodExtensions.ToMethod).IsGet())
        {
            return ResolveQueryMethodInvocation(routeParameter.Value);
        }
        return ResolveCommandMethodInvocation(aggregateParameter, routeParameter);
    }

    public string ResolveAdapterHandlerInvocation(CodeGenerationParameter aggregateParameter, CodeGenerationParameter routeSignatureParameter) => string.Format(AdapterPattern, new TemplateStandard(TemplateStandardType.DataObject).ResolveClassname(aggregateParameter.Value));

    private string ResolveCommandMethodInvocation(CodeGenerationParameter aggregateParameter, CodeGenerationParameter routeParameter)
    {
        var argumentsFormat = new AggregateArgumentsFormat.MethodInvocation("stage()", "data");
        var method = AggregateDetail.MethodWithName(aggregateParameter, routeParameter.Value);
        var factoryMethod = method.RetrieveRelatedValue(Label.FactoryMethod, x => bool.TrueString.ToLower() == x);
        var scope = factoryMethod ? MethodScopeType.Static : MethodScopeType.Instance;
        var methodInvocationParameters = argumentsFormat.Format(method, scope);
        var invoker = factoryMethod ? aggregateParameter.Value : ClassFormatter.SimpleNameToAttribute(aggregateParameter.Value);
        return string.Format(CommandPattern, invoker, method.Value, methodInvocationParameters);
    }

    private string ResolveQueryMethodInvocation(string methodName) => string.Format(QueryPattern, methodName);
}