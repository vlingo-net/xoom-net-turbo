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

namespace Vlingo.Xoom.Codegen.Template.Storage
{
    public class QueriesParameter
    {

        private static readonly string _qualifiedNamePattern = "{0}.{1}";

        private readonly string _protocolName;
        private readonly string _actorName;
        private readonly string _attributeName;
        private readonly HashSet<string> _qualifiedNames = new HashSet<string>();

        public static List<QueriesParameter> Drom(bool useCQRS, List<ContentBase> contents, List<TemplateData> templatesData)
        {
            if (!useCQRS)
            {
                return new List<QueriesParameter>();
            }

            return From(ModelType.Query, contents, templatesData);
        }

        public static List<QueriesParameter> From(ModelType model, List<ContentBase> contents, List<TemplateData> templatesData)
        {
            if (model != ModelType.Query)
            {
                return new List<QueriesParameter>();
            }

            if (ContentQuery.Exists(new TemplateStandard(TemplateStandardType.QueriesActor), contents))
            {
                return ContentQuery.FilterByStandard(new TemplateStandard(TemplateStandardType.QueriesActor), contents).Where(x => x.IsProtocolBased).Select(x => new QueriesParameter(x.RetrieveProtocolQualifiedName(), x.RetrieveQualifiedName())).ToList();
            }

            return templatesData.Where(data => data.HasStandard(TemplateStandardType.QueriesActor)).Select(data => new QueriesParameter(data.Parameters())).ToList();
        }

        public static QueriesParameter From(CodeGenerationParameter autoDispatchParameter)
        {
            if (!autoDispatchParameter.HasAny(Label.QueriesProtocol))
            {
                return QueriesParameter.Empty();
            }
            return new QueriesParameter(autoDispatchParameter.RetrieveRelatedValue(Label.QueriesProtocol), autoDispatchParameter.RetrieveRelatedValue(Label.QueriesActor));
        }

        public static QueriesParameter From(CodeGenerationParameter aggregateParameter, List<ContentBase> contents, bool useCQRS)
        {
            if (!useCQRS)
            {
                return QueriesParameter.Empty();
            }

            var queriesProtocol = new TemplateStandard(TemplateStandardType.Queries).ResolveClassname(aggregateParameter.value);
            var queriesActor = new TemplateStandard(TemplateStandardType.QueriesActor).ResolveClassname(aggregateParameter.value);
            return new QueriesParameter(ContentQuery.FindFullyQualifiedClassName(new TemplateStandard(TemplateStandardType.Queries), queriesProtocol, contents), ContentQuery.FindFullyQualifiedClassName(new TemplateStandard(TemplateStandardType.QueriesActor), queriesActor, contents));
        }

        private static QueriesParameter Empty() => new QueriesParameter(string.Empty, string.Empty, string.Empty, string.Empty);

        private QueriesParameter(TemplateParameters parameters) : this(parameters.Find<string>(TemplateParameter.PackageName), parameters.Find<string>(TemplateParameter.QueriesName), parameters.Find<string>(TemplateParameter.PackageName), parameters.Find<string>(TemplateParameter.QueriesActorName))
        {
        }

        private QueriesParameter(string protocolQualifiedName, string actorQualifiedName) : this(ClassFormatter.PackageOf(protocolQualifiedName), ClassFormatter.SimpleNameOf(protocolQualifiedName), ClassFormatter.PackageOf(actorQualifiedName), ClassFormatter.SimpleNameOf(actorQualifiedName))
        {
        }

        private QueriesParameter(string protocolPackageName, string protocolName, string actorPackageName, string actorName)
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
    }
}
