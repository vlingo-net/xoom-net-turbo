// Copyright © 2012-2020 VLINGO LABS. All rights reserved.
//
// This Source Code Form is subject to the terms of the
// Mozilla Public License, v. 2.0. If a copy of the MPL
// was not distributed with this file, You can obtain
// one at https://mozilla.org/MPL/2.0/.

using Vlingo.Xoom.Codegen.Content;
using Vlingo.Xoom.Codegen.Parameter;
using Vlingo.Xoom.Codegen.Template.Resource;

namespace Vlingo.Xoom.Codegen.Template.Autodispatch
{
    public class AutoDispatchHandlerInvocationResolver : HandlerInvocationResolver, IHandlerInvocationResolver
    {
        private static readonly string _defaultAdapterParameter = "state";
        private static readonly string _handlerInvocationPattern = "%s.%s";
        private static readonly string _defaultFactoryMethodParameter = "$stage";
        private static readonly string _handlerInvocationWithDefaultParamsPattern = "%s.%s(%s)";
        
        //TODO: 
        public string ResolveRouteHandlerInvocation(CodeGenerationParameter parentParameter, CodeGenerationParameter routeSignatureParameter)
        {
            //var httpMethod = routeSignatureParameter.RetrieveRelatedValue(Label.RouteMethod, Method::from);

            string defaultParameter = string.Empty;//httpMethod.isGET() ? QUERIES_PARAMETER : _defaultFactoryMethodParameter;

            return Resolve(Label.RouteHandlerInvocation, Label.UseCustomRouteHandlerParam, defaultParameter, parentParameter, routeSignatureParameter);
        }

        public string ResolveAdapterHandlerInvocation(CodeGenerationParameter parentParameter, CodeGenerationParameter routeSignatureParameter) => Resolve(Label.AdapterHandlerInvocation, Label.UseCustomAdapterHandlerParam, _defaultAdapterParameter, parentParameter, routeSignatureParameter);

        private string Resolve(Label invocationLabel, Label customParamsLabel, string defaultParameter, CodeGenerationParameter parentParameter, CodeGenerationParameter routeSignatureParameter)
        {
            if (!routeSignatureParameter.HasAny(customParamsLabel))
            {
                return string.Empty;
            }
            var handlersConfigQualifiedName = parentParameter.RetrieveRelatedValue(Label.HandlersConfigName);
            var handlersConfigClassName = ClassFormatter.SimpleNameOf(handlersConfigQualifiedName);
            var invocation = routeSignatureParameter.RetrieveRelatedValue(invocationLabel);
            if (routeSignatureParameter.RetrieveRelatedValue(customParamsLabel, x => bool.TrueString.ToLower() == x))
            {
                return string.Format(_handlerInvocationPattern, handlersConfigClassName, invocation);
            }
            return string.Format(_handlerInvocationWithDefaultParamsPattern, handlersConfigClassName, invocation, defaultParameter);
        }
    }
}
