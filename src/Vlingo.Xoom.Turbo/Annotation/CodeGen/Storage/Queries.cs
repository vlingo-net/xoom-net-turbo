// Copyright © 2012-2021 VLINGO LABS. All rights reserved.
//
// This Source Code Form is subject to the terms of the
// Mozilla Public License, v. 2.0. If a copy of the MPL
// was not distributed with this file, You can obtain
// one at https://mozilla.org/MPL/2.0/.

using System.Collections.Generic;
using System.Linq;
using Vlingo.Xoom.Turbo.Codegen.Content;
using Vlingo.Xoom.Turbo.Codegen.Parameter;
using Vlingo.Xoom.Turbo.Codegen.Template;

namespace Vlingo.Xoom.Turbo.Annotation.Codegen.Storage
{
    public class Queries
    {

        private static readonly string _qualifiedNamePattern = "{0}.{1}";

        private readonly string _protocolName;
        private readonly string _actorName;
        private readonly string _attributeName;
        private readonly HashSet<string> _qualifiedNames = new HashSet<string>();

        public static List<Queries> Drom(bool useCQRS, List<ContentBase> contents, List<TemplateData> templatesData)
        {
            if (!useCQRS)
            {
                return new List<Queries>();
            }

            return From(ModelType.Query, contents, templatesData);
        }

        public static List<Queries> From(ModelType model, List<ContentBase> contents, List<TemplateData> templatesData)
        {
            if (model != ModelType.Query)
            {
                return new List<Queries>();
            }

            if (ContentQuery.Exists(new TemplateStandard(TemplateStandardType.QueriesActor), contents))
            {
                return ContentQuery.FilterByStandard(new TemplateStandard(TemplateStandardType.QueriesActor), contents).Where(x => x.IsProtocolBased).Select(x => new Queries(x.RetrieveProtocolQualifiedName(), x.RetrieveQualifiedName())).ToList();
            }

            return templatesData.Where(data => data.HasStandard(TemplateStandardType.QueriesActor)).Select(data => new Queries(data.Parameters())).ToList();
        }

        public static Queries From(CodeGenerationParameter autoDispatchParameter)
        {
            if (!autoDispatchParameter.HasAny(Label.QueriesProtocol))
            {
                return Queries.Empty();
            }
            return new Queries(autoDispatchParameter.RetrieveRelatedValue(Label.QueriesProtocol), autoDispatchParameter.RetrieveRelatedValue(Label.QueriesActor));
        }

        public static Queries From(CodeGenerationParameter aggregateParameter, List<ContentBase> contents, bool useCQRS)
        {
            if (!useCQRS)
            {
                return Queries.Empty();
            }

            var queriesProtocol = new TemplateStandard(TemplateStandardType.Queries).ResolveClassname(aggregateParameter.value);
            var queriesActor = new TemplateStandard(TemplateStandardType.QueriesActor).ResolveClassname(aggregateParameter.value);
            return new Queries(ContentQuery.FindFullyQualifiedClassName(new TemplateStandard(TemplateStandardType.Queries), queriesProtocol, contents), ContentQuery.FindFullyQualifiedClassName(new TemplateStandard(TemplateStandardType.QueriesActor), queriesActor, contents));
        }

        private static Queries Empty() => new Queries(string.Empty, string.Empty, string.Empty, string.Empty);

        private Queries(TemplateParameters parameters) : this(parameters.Find<string>(TemplateParameter.PackageName), parameters.Find<string>(TemplateParameter.QueriesName), parameters.Find<string>(TemplateParameter.PackageName), parameters.Find<string>(TemplateParameter.QueriesActorName))
        {
        }

        private Queries(string protocolQualifiedName, string actorQualifiedName) : this(ClassFormatter.PackageOf(protocolQualifiedName), ClassFormatter.SimpleNameOf(protocolQualifiedName), ClassFormatter.PackageOf(actorQualifiedName), ClassFormatter.SimpleNameOf(actorQualifiedName))
        {
        }

        private Queries(string protocolPackageName, string protocolName, string actorPackageName, string actorName)
        {
            _actorName = actorName;
            _protocolName = protocolName;
            _attributeName = ClassFormatter.SimpleNameToAttribute(protocolName);

            if (!IsEmpty())
            {
                _qualifiedNames.Add(string.Format(_qualifiedNamePattern, protocolPackageName, protocolName));
                _qualifiedNames.Add(string.Format(_qualifiedNamePattern, actorPackageName, actorName));
            }
        }

        public string GetProtocolName() => _protocolName;

        public string GetActorName() => _actorName;

        public string GetAttributeName() => _attributeName;

        public HashSet<string> GetQualifiedNames() => _qualifiedNames;

        public bool IsEmpty() => _protocolName == string.Empty && _actorName == string.Empty && _attributeName == string.Empty;

        public static List<Queries> From(ModelType modelType, IReadOnlyList<ContentBase> contents,
            List<TemplateData> templatesData) => new List<Queries>();
    }
}
