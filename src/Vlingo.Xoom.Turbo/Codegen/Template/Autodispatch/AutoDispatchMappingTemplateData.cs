// Copyright © 2012-2021 VLINGO LABS. All rights reserved.
//
// This Source Code Form is subject to the terms of the
// Mozilla Public License, v. 2.0. If a copy of the MPL
// was not distributed with this file, You can obtain
// one at https://mozilla.org/MPL/2.0/.

using System;
using System.Collections.Generic;
using System.Linq;
using Vlingo.Xoom.Turbo.Annotation.Codegen.AutoDispatch;
using Vlingo.Xoom.Turbo.Codegen.Content;
using Vlingo.Xoom.Turbo.Codegen.Parameter;
using Vlingo.Xoom.Turbo.Codegen.Template.Resource;

namespace Vlingo.Xoom.Turbo.Codegen.Template.Autodispatch
{
    public class AutoDispatchMappingTemplateData : TemplateData
    {

        private readonly string _aggregateName;
        private readonly TemplateParameters _parameters;

        public AutoDispatchMappingTemplateData(string basePackage, CodeGenerationParameter aggregate, bool useCQRS, List<ContentBase> contents)
        {
            _aggregateName = aggregate.value;
            _parameters =
                    TemplateParameters.With(TemplateParameter.PackageName, ResolvePackage(basePackage))
                            .And(TemplateParameter.AggregateProtocolName, _aggregateName)
                            .And(TemplateParameter.EntityName, new TemplateStandard().ResolveClassname(TemplateStandardType.Aggregate.ToString(_aggregateName)))
                            .And(TemplateParameter.DataObjectName, new TemplateStandard().ResolveClassname(TemplateStandardType.DataObject.ToString(_aggregateName)))
                            .And(TemplateParameter.QueriesName, new TemplateStandard().ResolveClassname(TemplateStandardType.Queries.ToString(_aggregateName)))
                            .And(TemplateParameter.QueriesActorName, new TemplateStandard().ResolveClassname(TemplateStandardType.QueriesActor.ToString(_aggregateName)))
                            .And(TemplateParameter.AutoDispatchMappingName, new TemplateStandard().ResolveClassname(TemplateStandardType.AutoDispatchMapping.ToString(_aggregateName)))
                            .And(TemplateParameter.AutoDispatchHandlersMappingName, new TemplateStandard().ResolveClassname(TemplateStandardType.AutoDispatchHandlersMapping.ToString(_aggregateName)))
                            .And(TemplateParameter.UriRoot, aggregate.RetrieveRelatedValue(Label.UriRoot))
                            .AddImports(ResolveImports(_aggregateName, contents))
                            .And(TemplateParameter.RouteDeclarations, new List<string>())
                            .And(TemplateParameter.UseCqrs, useCQRS);

            LoadDependencies(aggregate, useCQRS);
        }

        private void LoadDependencies(CodeGenerationParameter aggregate, bool useCQRS)
        {
            if (useCQRS)
            {
                aggregate.Relate(RouteDetail.DefaultQueryRouteParameter(aggregate));
            }
            this.DependOn(AutoDispatchRouteTemplateData.From(aggregate.RetrieveAllRelated(Label.RouteSignature)));
        }

        public override void HandleDependencyOutcome(TemplateStandard standard, string outcome) => _parameters.Find<List<string>>(TemplateParameter.RouteDeclarations).Add(outcome);

        private HashSet<string> ResolveImports(string aggregateName, List<ContentBase> contents)
        {
            var classes = MapClassesWithTemplateStandards(aggregateName);

            return new HashSet<string>(classes.Select(entry =>
            {
                try
                {
                    var className = entry.Value;
                    var standard = entry.Key;
                    return ContentQuery.FindFullyQualifiedClassName(new TemplateStandard(standard), className, contents);
                }
                catch (ArgumentException)
                {
                    return string.Empty;
                }
            }));
        }

        private IDictionary<TemplateStandardType, string> MapClassesWithTemplateStandards(string aggregateName)
        {
            var result = new Dictionary<TemplateStandardType, string>();
            result.Add(TemplateStandardType.Aggregate, new TemplateStandard(TemplateStandardType.Aggregate).ResolveClassname(aggregateName));
            result.Add(TemplateStandardType.Queries, aggregateName);
            result.Add(TemplateStandardType.QueriesActor, new TemplateStandard(TemplateStandardType.QueriesActor).ResolveClassname(aggregateName));
            result.Add(TemplateStandardType.DataObject, new TemplateStandard(TemplateStandardType.DataObject).ResolveClassname(aggregateName));
            return result;
        }

        private string ResolvePackage(string basePackage) => string.Format("{0}.{1}.{2}", basePackage, "infrastructure", "resource").ToLower();

        public override TemplateParameters Parameters() => _parameters;

        public override TemplateStandard Standard() => new TemplateStandard(TemplateStandardType.AutoDispatchMapping);

        public override string Filename() => Standard().ResolveFilename(_aggregateName, _parameters);

    }
}
