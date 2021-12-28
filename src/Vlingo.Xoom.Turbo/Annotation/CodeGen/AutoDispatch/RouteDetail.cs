// Copyright © 2012-2021 VLINGO LABS. All rights reserved.
//
// This Source Code Form is subject to the terms of the
// Mozilla Public License, v. 2.0. If a copy of the MPL
// was not distributed with this file, You can obtain
// one at https://mozilla.org/MPL/2.0/.

using System.Collections.Generic;
using System.Linq;
using Vlingo.Xoom.Http;
using Vlingo.Xoom.Turbo.Codegen.Parameter;
using Vlingo.Xoom.Turbo.Codegen.Template;
using Vlingo.Xoom.Turbo.Codegen.Template.Autodispatch;

namespace Vlingo.Xoom.Turbo.Annotation.Codegen.AutoDispatch
{
    public class RouteDetail
    {
        private static readonly string _bodyDefaultName = "data";
        private static readonly string _methodParameterPattern = "final %s %s";
        private static readonly string _methodSignaturePattern = "%s(%s)";
        private static readonly List<Method> _bodySupportedHttpMethods = new List<Method>() { Method.Post, Method.Put, Method.Patch };

        public static string ResolveBodyName(CodeGenerationParameter route)
        {
            var httpMethod = route.RetrieveRelatedValue(Label.RouteMethod, MethodExtensions.ToMethod);

            if (!_bodySupportedHttpMethods.Contains(httpMethod))
            {
                return string.Empty;
            }

            if (route.HasAny(Label.Body))
            {
                return route.RetrieveRelatedValue(Label.Body);
            }

            return _bodyDefaultName;
        }

        public static string ResolveBodyType(CodeGenerationParameter route)
        {
            var httpMethod = route.RetrieveRelatedValue(Label.RouteMethod, MethodExtensions.ToMethod);

            if (!_bodySupportedHttpMethods.Contains(httpMethod))
            {
                return string.Empty;
            }

            if (route.Parent().IsLabeled(Label.Aggregate))
            {
                return new TemplateStandard(TemplateStandardType.DataObject).ResolveClassname(route.Parent(Label.Aggregate).value);
            }

            return route.RetrieveRelatedValue(Label.BodyType);
        }

        public static bool RequireEntityLoad(CodeGenerationParameter aggregateParameter) => aggregateParameter.RetrieveAllRelated(Label.RouteSignature).Where(route => route.HasAny(Label.RequireEntityLoading)).Any(route => route.RetrieveRelatedValue(Label.RequireEntityLoading, x => bool.TrueString.ToLower() == x));

        public static bool RequireModelFactory(CodeGenerationParameter aggregateParameter) => aggregateParameter.RetrieveAllRelated(Label.RouteSignature).Select(methodSignature => AggregateDetail.MethodWithName(aggregateParameter, methodSignature.value)).Any(method => method.RetrieveRelatedValue(Label.FactoryMethod, x => bool.TrueString.ToLower() == x));

        public static string ResolveMethodSignature(CodeGenerationParameter routeSignature)
        {
            if (HasValidMethodSignature(routeSignature.value))
            {
                return routeSignature.value;
            }

            if (routeSignature.RetrieveRelatedValue(Label.RouteMethod, MethodExtensions.ToMethod).IsGet())
            {
                return string.Format(_methodSignaturePattern, routeSignature.value, string.Empty);
            }

            return ResolveMethodSignatureWithParams(routeSignature);
        }

        private static string ResolveMethodSignatureWithParams(CodeGenerationParameter routeSignature)
        {
            var idParameter = routeSignature.RetrieveRelatedValue(Label.RequireEntityLoading, x => bool.TrueString.ToLower() == x) ? string.Format(_methodParameterPattern, "String", "id") : string.Empty;
            var method = AggregateDetail.MethodWithName(routeSignature.Parent(), routeSignature.value);
            var dataClassname = new TemplateStandard(TemplateStandardType.DataObject).ResolveClassname(routeSignature.Parent().value);
            var dataParameterDeclaration = string.Format(_methodParameterPattern, dataClassname, "data");
            var dataParameter = method.HasAny(Label.MethodParameter) ? dataParameterDeclaration : "";
            var parameters = string.Join(", ", new List<string>() { idParameter, dataParameter }.Where(param => param != string.Empty));
            return string.Format(_methodSignaturePattern, routeSignature.value, parameters);
        }

        private static bool HasValidMethodSignature(string signature) => signature.Contains("(") && signature.Contains(")");

        public static CodeGenerationParameter DefaultQueryRouteParameter(CodeGenerationParameter aggregate)
        {
            return CodeGenerationParameter.Of(Label.RouteSignature, BuildQueryAllMethodName(aggregate.value)).Relate(Label.RouteMethod, Method.Get).Relate(Label.ReadOnly, "true");
        }

        private static string BuildQueryAllMethodName(string aggregateProtocol)
        {
            var formatted = AutoDispatchMappingValueFormatter.Format(aggregateProtocol);
            return formatted.EndsWith("s") ? formatted : formatted + "s";
        }
    }
}
