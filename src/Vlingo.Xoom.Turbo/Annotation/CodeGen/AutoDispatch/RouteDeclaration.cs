// Copyright © 2012-2022 VLINGO LABS. All rights reserved.
//
// This Source Code Form is subject to the terms of the
// Mozilla Public License, v. 2.0. If a copy of the MPL
// was not distributed with this file, You can obtain
// one at https://mozilla.org/MPL/2.0/.

using System.Collections.Generic;
using System.Linq;
using Vlingo.Xoom.Http.Resource;
using Vlingo.Xoom.Turbo.Annotation.Codegen.AutoDispatch;
using Vlingo.Xoom.Turbo.Codegen.Parameter;

namespace Vlingo.Xoom.Turbo.Annotation.Codegen.AutoDispatch
{
    public class RouteDeclaration
    {
        private readonly bool _last;
        private readonly string _path;
        private readonly string _bodyType;
        private readonly string _handlerName;
        private readonly string _builderMethod;
        private readonly string _signature;
        private readonly List<string> _parameterTypes = new List<string>();

        public static List<RouteDeclaration> From(CodeGenerationParameter mainParameter)
        {
            var routeSignatures = mainParameter.RetrieveAllRelated(Label.RouteSignature).ToList();
            return Enumerable.Range(0, routeSignatures.Count()).Select(index => new RouteDeclaration(index, routeSignatures.Count(), routeSignatures[index])).ToList();
        }

        private RouteDeclaration(int routeIndex, int numberOfRoutes, CodeGenerationParameter routeSignatureParameter)
        {
            _signature = RouteDetail.ResolveMethodSignature(routeSignatureParameter);
            _handlerName = ResolveHandlerName();
            _path = PathFormatter.FormatAbsoluteRoutePath(routeSignatureParameter);
            _bodyType = RouteDetail.ResolveBodyType(routeSignatureParameter);
            _builderMethod = routeSignatureParameter.RetrieveRelatedValue(Label.RouteMethod);
            _parameterTypes.AddRange(ResolveParameterTypes(routeSignatureParameter));
            _last = routeIndex == numberOfRoutes - 1;
        }

        private string ResolveHandlerName() => _signature.Substring(0, _signature.IndexOf("(")).Trim();

        private List<string> ResolveParameterTypes(CodeGenerationParameter routeSignatureParameter)
        {
            var bodyParameterName = RouteDetail.ResolveBodyName(routeSignatureParameter);
            var parameters = _signature.Substring(_signature.IndexOf("(") + 1, _signature.LastIndexOf(")"));
            if (parameters.Trim() == string.Empty)
            {
                return new List<string>();
            }
            return parameters.Split(',').Select(parameter => parameter.Replace("readonly", string.Empty).Trim()).Where(parameter => !parameter.EndsWith(string.Concat(" ", bodyParameterName)))
            .Select(parameter => parameter.Substring(0, parameter.IndexOf(" "))).ToList();
        }

        public string GetPath() => _path;

        public string GetBodyType() => _bodyType;

        public string GetHandlerName() => _handlerName;

        public string GetBuilderMethod() => string.Concat(typeof(ResourceBuilder).Name, ".", _builderMethod.ToLower());

        public List<string> GetParameterTypes() => _parameterTypes;

        public bool IsLast() => _last;
    }
}
