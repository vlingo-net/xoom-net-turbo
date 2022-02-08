// Copyright © 2012-2021 VLINGO LABS. All rights reserved.
//
// This Source Code Form is subject to the terms of the
// Mozilla Public License, v. 2.0. If a copy of the MPL
// was not distributed with this file, You can obtain
// one at https://mozilla.org/MPL/2.0/.

using System;
using System.Collections.Generic;
using System.Linq;
using Vlingo.Xoom.Turbo.Codegen.Content;
using Vlingo.Xoom.Turbo.Codegen.Parameter;

namespace Vlingo.Xoom.Turbo.Codegen.Template.Autodispatch
{
    public class AutoDispatchHandlersMappingTemplateData : TemplateData
    {

        private readonly static string _handlerIndexPattern = "public static final int %s = %d;";
        private readonly CodeGenerationParameter _stateAdapterHandler = CodeGenerationParameter.Of(Label.RouteSignature, "adaptState");

        private readonly string _aggregateName;
        private readonly TemplateParameters _parameters;

        public AutoDispatchHandlersMappingTemplateData(string basePackage, CodeGenerationParameter aggregate, List<TemplateData> queriesTemplateData, List<ContentBase> contents, bool useCQRS)
        {
            _aggregateName = aggregate.value;
            _parameters =
                    TemplateParameters.With(TemplateParameter.PackageName, ResolvePackage(basePackage))
                            .And(TemplateParameter.AggregateProtocolName, _aggregateName)
                            .And(TemplateParameter.StateName, new TemplateStandard().ResolveClassname(TemplateStandardType.AggregateState.ToString()))
                            .And(TemplateParameter.DataObjectName, new TemplateStandard().ResolveClassname(TemplateStandardType.DataObject.ToString()))
                            .And(TemplateParameter.QueriesName, new TemplateStandard().ResolveClassname(TemplateStandardType.Queries.ToString())).And(TemplateParameter.UseCqrs, useCQRS)
                            .And(TemplateParameter.QueryAllMethodName, FindQueryMethodName(_aggregateName, queriesTemplateData))
                            .AndResolve(TemplateParameter.QueryAllIndexName, @params => AutoDispatchMappingValueFormatter.Format(@params.Find<string>(TemplateParameter.QueryAllMethodName)))
                            .And(TemplateParameter.AutoDispatchHandlersMappingName, Standard().ResolveClassname(_aggregateName))
                            .And(TemplateParameter.HandlerIndexes, ResolveHandlerIndexes(aggregate, useCQRS))
                            .And(TemplateParameter.HandlerEntries, new List<string>())
                            .AddImports(ResolveImports(_aggregateName, contents));

            DependOn(AutoDispatchHandlerEntryTemplateData.From(aggregate).ToList());
        }

        public override void HandleDependencyOutcome(TemplateStandard standard, string outcome) => _parameters.Find<List<string>>(TemplateParameter.HandlerEntries).Add(outcome);

        private List<string> ResolveHandlerIndexes(CodeGenerationParameter aggregate, bool useCQRS)
        {
            var handlers = new List<List<CodeGenerationParameter>>() { aggregate.RetrieveAllRelated(Label.RouteSignature).ToList(), new List<CodeGenerationParameter>() { _stateAdapterHandler } }.SelectMany(x => x).ToList();
            return Enumerable.Range(0, handlers.Count()).Select(index => string.Format(_handlerIndexPattern, AutoDispatchMappingValueFormatter.Format(handlers[index].value), index)).ToList();
        }

        private string FindQueryMethodName(string aggregateName, List<TemplateData> queriesTemplateData)
        {
            if (queriesTemplateData == null || queriesTemplateData.Count == 0)
            {
                return string.Empty;
            }

            string expectedQueriesName = new TemplateStandard(TemplateStandardType.Queries).ResolveClassname(aggregateName);
            return queriesTemplateData.Select(x => x.Parameters()).Where(x => x.HasValue(TemplateParameter.QueriesName, expectedQueriesName)).Select(x => x.Find<string>(TemplateParameter.QueryAllMethodName)).First();
        }

        private HashSet<string> ResolveImports(string aggregateName, List<ContentBase> contents)
        {
            var classes = MapClassesWithTemplateStandards(aggregateName);
            return new HashSet<string>(classes.Select(entry =>
            {
                try
                {
                    var className = entry.Value;
                    var standart = entry.Key;
                    return ContentQuery.FindFullyQualifiedClassName(new TemplateStandard(standart), className, contents);
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
            result.Add(TemplateStandardType.AggregateProtocol, aggregateName);
            result.Add(TemplateStandardType.AggregateState, new TemplateStandard(TemplateStandardType.AggregateState).ResolveClassname(aggregateName));
            result.Add(TemplateStandardType.Queries, new TemplateStandard(TemplateStandardType.Queries).ResolveClassname(aggregateName));
            result.Add(TemplateStandardType.DataObject, new TemplateStandard(TemplateStandardType.DataObject).ResolveClassname(aggregateName));
            return result;
        }

        private string ResolvePackage(string basePackage) => string.Format("{0}.{1}.{2}", basePackage, "infrastructure", "resource").ToLower();

        public override TemplateParameters Parameters() => _parameters;

        public override TemplateStandard Standard() => new TemplateStandard(TemplateStandardType.AutoDispatchHandlersMapping);

        public override string Filename() => Standard().ResolveFilename(_aggregateName, _parameters);

    }
}
