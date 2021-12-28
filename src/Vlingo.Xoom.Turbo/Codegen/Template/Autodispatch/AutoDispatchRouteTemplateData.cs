// Copyright © 2012-2021 VLINGO LABS. All rights reserved.
//
// This Source Code Form is subject to the terms of the
// Mozilla Public License, v. 2.0. If a copy of the MPL
// was not distributed with this file, You can obtain
// one at https://mozilla.org/MPL/2.0/.

using System.Collections.Generic;
using System.Linq;
using Vlingo.Xoom.Http;
using Vlingo.Xoom.Turbo.Annotation.Codegen.AutoDispatch;
using Vlingo.Xoom.Turbo.Codegen.Parameter;
using Vlingo.Xoom.Turbo.Codegen.Template.Model;
using Vlingo.Xoom.Turbo.Codegen.Template.Resource;

namespace Vlingo.Xoom.Turbo.Codegen.Template.Autodispatch
{
    public class AutoDispatchRouteTemplateData : TemplateData
    {

        private readonly TemplateParameters _parameters;

        public static List<TemplateData> From(IEnumerable<CodeGenerationParameter> routes) => routes.Select(x => (TemplateData)new AutoDispatchRouteTemplateData(x)).ToList();

        private AutoDispatchRouteTemplateData(CodeGenerationParameter route)
        {
            var aggregate = route.Parent(Label.Aggregate);
            _parameters =
                    TemplateParameters.With(TemplateParameter.RetrievalRoute, IsRetrievalRoute(route))
                            .And(TemplateParameter.IdType, FieldDetail.TypeOf(aggregate, "id"))
                            .And(TemplateParameter.RouteMethod, route.RetrieveRelatedValue(Label.RouteMethod))
                            .And(TemplateParameter.RoutePath, PathFormatter.FormatRelativeRoutePath(route))
                            .And(TemplateParameter.DataObjectName, new TemplateStandard(TemplateStandardType.DataObject).ResolveClassname(aggregate.value))
                            .And(TemplateParameter.RouteMappingValue, AutoDispatchMappingValueFormatter.Format(route.value))
                            .And(TemplateParameter.RequireEntityLoading, route.RetrieveRelatedValue(Label.RequireEntityLoading, x => bool.TrueString.ToLower() == x))
                            .And(TemplateParameter.AutoDispatchHandlersMappingName, new TemplateStandard(TemplateStandardType.AutoDispatchHandlersMapping).ResolveClassname(aggregate.value))
                            .And(TemplateParameter.MethodName, route.value);
        }

        private bool IsRetrievalRoute(CodeGenerationParameter route)
        {
            var method = route.RetrieveRelatedValue(Label.RouteMethod, MethodExtensions.ToMethod);
            return method.IsGet() || method.IsOptions();
        }

        public override TemplateStandard Standard() => new TemplateStandard(TemplateStandardType.AutoDispatchRoute);

        public override TemplateParameters Parameters() => _parameters;
    }
}
