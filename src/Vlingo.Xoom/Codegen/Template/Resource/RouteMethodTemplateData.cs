// Copyright © 2012-2021 VLINGO LABS. All rights reserved.
//
// This Source Code Form is subject to the terms of the
// Mozilla Public License, v. 2.0. If a copy of the MPL
// was not distributed with this file, You can obtain
// one at https://mozilla.org/MPL/2.0/.

using System.Collections.Generic;
using System.Linq;
using Vlingo.Xoom.Codegen.Content;
using Vlingo.Xoom.Codegen.Parameter;

namespace Vlingo.Xoom.Codegen.Template.Resource
{
    public class RouteMethodTemplateData : TemplateData
    {
        private static readonly string _defaultIdName = "id";

        private readonly TemplateParameters _parameters;

        public static List<TemplateData> From(CodeGenerationParameter aggregateParameter, TemplateParameters parentParameters, List<ContentBase> contents)
        {
            InferModelParameters(aggregateParameter, contents);
            return From(aggregateParameter, parentParameters);
        }

        public static List<TemplateData> From(CodeGenerationParameter mainParameter, TemplateParameters parentParameters) => mainParameter.RetrieveAllRelated(Label.RouteSignature).Where(x => !x.RetrieveRelatedValue(Label.InternalRouteHandler, a => bool.TrueString.ToLower() == a))
                .Select(routeSignatureParameter => (TemplateData)new RouteMethodTemplateData(mainParameter, routeSignatureParameter, parentParameters)).ToList();

        private static void InferModelParameters(CodeGenerationParameter aggregateParameter, List<ContentBase> contents)
        {
            var modelActor = new TemplateStandard(TemplateStandardType.Aggregate).ResolveClassname(aggregateParameter.value);
            var modelProtocolQualifiedName = ContentQuery.FindFullyQualifiedClassName(new TemplateStandard(TemplateStandardType.AggregateProtocol), aggregateParameter.value, contents);
            var modelActorQualifiedName = ContentQuery.FindFullyQualifiedClassName(new TemplateStandard(TemplateStandardType.Aggregate), modelActor, contents);
            aggregateParameter.Relate(Label.ModelProtocol, modelProtocolQualifiedName).Relate(Label.ModelActor, modelActorQualifiedName);
        }

        private RouteMethodTemplateData(CodeGenerationParameter mainParameter, CodeGenerationParameter routeSignatureParameter, TemplateParameters parentParameters)
        {
            var invocationResolver = HandlerInvocationResolver.With(mainParameter);
            var routeHandlerInvocation = invocationResolver.ResolveRouteHandlerInvocation(mainParameter, routeSignatureParameter);
            var adapterHandlerInvocation = invocationResolver.ResolveAdapterHandlerInvocation(mainParameter, routeSignatureParameter);
            _parameters = TemplateParameters.With(TemplateParameter.RouteSignature, RouteDetail.ResolveMethodSignature(routeSignatureParameter))
                            .And(TemplateParameter.ModelAttribute, ResolveModelAttributeName(mainParameter, Label.ModelProtocol))
                            .And(TemplateParameter.RouteMethod, routeSignatureParameter.RetrieveRelatedValue(Label.RouteMethod))
                            .And(TemplateParameter.RequireEntityLoading, ResolveEntityLoading(routeSignatureParameter))
                            .And(TemplateParameter.AdapterHandlerInvocation, adapterHandlerInvocation)
                            .And(TemplateParameter.RouteHandlerInvocation, routeHandlerInvocation)
                            .And(TemplateParameter.IdName, ResolveIdName(routeSignatureParameter));

            parentParameters.AddImports(ResolveImports(mainParameter, routeSignatureParameter));
        }

        private HashSet<string> ResolveImports(CodeGenerationParameter mainParameter, CodeGenerationParameter routeSignatureParameter) => new HashSet<string>(new List<string>() { RetrieveIdTypeQualifiedName(routeSignatureParameter),
                routeSignatureParameter.RetrieveRelatedValue(Label.BodyType),
                mainParameter.RetrieveRelatedValue(Label.HandlersConfigName),
                mainParameter.RetrieveRelatedValue(Label.ModelProtocol),
                mainParameter.RetrieveRelatedValue(Label.ModelActor),
                mainParameter.RetrieveRelatedValue(Label.ModelData),
            }.Where(x => x == string.Empty));

        private bool ResolveEntityLoading(CodeGenerationParameter routeSignatureParameter) => routeSignatureParameter.HasAny(Label.Id) ||
                    (routeSignatureParameter.HasAny(Label.RequireEntityLoading) &&
                            routeSignatureParameter.RetrieveRelatedValue(Label.RequireEntityLoading, x => bool.TrueString.ToLower() == x));

        private string ResolveIdName(CodeGenerationParameter routeSignatureParameter)
        {
            if (!routeSignatureParameter.HasAny(Label.Id))
            {
                return _defaultIdName;
            }
            return routeSignatureParameter.RetrieveRelatedValue(Label.Id);
        }

        private string RetrieveIdTypeQualifiedName(CodeGenerationParameter routeSignatureParameter)
        {
            var idType = routeSignatureParameter.RetrieveRelatedValue(Label.IdType);
            return idType.Contains(".") ? string.Empty : idType;
        }

        private string ResolveModelAttributeName(CodeGenerationParameter mainParameter, Label protocolLabel)
        {
            if (mainParameter.IsLabeled(Label.Aggregate))
            {
                return ClassFormatter.SimpleNameToAttribute(mainParameter.value);
            }
            var qualifiedName = mainParameter.RetrieveRelatedValue(protocolLabel);
            return ClassFormatter.QualifiedNameToAttribute(qualifiedName);
        }

        public override TemplateParameters Parameters() => _parameters;

        public override TemplateStandard Standard() => new TemplateStandard(TemplateStandardType.RouteMethod);
    }
}
