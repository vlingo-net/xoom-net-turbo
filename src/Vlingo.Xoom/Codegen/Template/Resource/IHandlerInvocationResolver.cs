// Copyright © 2012-2020 VLINGO LABS. All rights reserved.
//
// This Source Code Form is subject to the terms of the
// Mozilla Public License, v. 2.0. If a copy of the MPL
// was not distributed with this file, You can obtain
// one at https://mozilla.org/MPL/2.0/.

using Vlingo.Xoom.Codegen.Parameter;
using Vlingo.Xoom.Codegen.Template.Autodispatch;

namespace Vlingo.Xoom.Codegen.Template.Resource
{
    public interface IHandlerInvocationResolver
    {
        public abstract string ResolveRouteHandlerInvocation(CodeGenerationParameter parentParameter, CodeGenerationParameter routeSignatureParameter);

        public abstract string ResolveAdapterHandlerInvocation(CodeGenerationParameter parentParameter, CodeGenerationParameter routeSignatureParameter);
    }

    public class HandlerInvocationResolver
    {
        protected static readonly string _queriesParameter = "$queries";

        public static IHandlerInvocationResolver With(CodeGenerationParameter parentParameter)
        {
            if (parentParameter.IsLabeled(Label.AutoDispatchName))
            {
                return new AutoDispatchHandlerInvocationResolver();
            }
            return new DefaultHandlerInvocationResolver();
        }
    }
}
