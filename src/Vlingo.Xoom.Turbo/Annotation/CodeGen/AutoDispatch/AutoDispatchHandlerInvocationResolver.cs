// Copyright © 2012-2021 VLINGO LABS. All rights reserved.
//
// This Source Code Form is subject to the terms of the
// Mozilla Public License, v. 2.0. If a copy of the MPL
// was not distributed with this file, You can obtain
// one at https://mozilla.org/MPL/2.0/.

using Vlingo.Xoom.Turbo.Codegen.Content;
using Vlingo.Xoom.Turbo.Codegen.Parameter;
using Vlingo.Xoom.Turbo.Codegen.Template.Resource;

namespace Vlingo.Xoom.Turbo.Annotation.Codegen.AutoDispatch
{
    public class AutoDispatchHandlerInvocationResolver : HandlerInvocationResolver, IHandlerInvocationResolver
    {
        private static readonly string DefaultAdapterParameter = "state";
        private static readonly string HandlerInvocationPattern = "%s.%s";
        //private static readonly string _defaultFactoryMethodParameter = "$stage";
        private static readonly string HandlerInvocationWithDefaultParamsPattern = "%s.%s(%s)";
        
        //TODO: 
        public string ResolveRouteHandlerInvocation(CodeGenerationParameter parentParameter, CodeGenerationParameter routeSignatureParameter)
        {
            //var httpMethod = routeSignatureParameter.RetrieveRelatedValue(Label.RouteMethod, Method::from);

            var defaultParameter = string.Empty;//httpMethod.isGET() ? QUERIES_PARAMETER : _defaultFactoryMethodParameter;

            return Resolve(Label.RouteHandlerInvocation, Label.UseCustomRouteHandlerParam, defaultParameter, parentParameter, routeSignatureParameter);
        }

        public string ResolveAdapterHandlerInvocation(CodeGenerationParameter parentParameter, CodeGenerationParameter routeSignatureParameter) => Resolve(Label.AdapterHandlerInvocation, Label.UseCustomAdapterHandlerParam, DefaultAdapterParameter, parentParameter, routeSignatureParameter);

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
                return string.Format(HandlerInvocationPattern, handlersConfigClassName, invocation);
            }
            return string.Format(HandlerInvocationWithDefaultParamsPattern, handlersConfigClassName, invocation, defaultParameter);
        }
    }
}
