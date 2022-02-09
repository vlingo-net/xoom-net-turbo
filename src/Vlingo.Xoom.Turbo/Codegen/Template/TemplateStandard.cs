// Copyright © 2012-2022 VLINGO LABS. All rights reserved.
//
// This Source Code Form is subject to the terms of the
// Mozilla Public License, v. 2.0. If a copy of the MPL
// was not distributed with this file, You can obtain
// one at https://mozilla.org/MPL/2.0/.

using System;

namespace Vlingo.Xoom.Turbo.Codegen.Template
{
    public enum TemplateStandardType
    {
        Aggregate,
        AggregateProtocol,
        AggregateProtocolMethod,
        AggregateMethod,
        AggregateProtocolStaticMethod,
        AggregateProtocolInstanceMethod,
        AggregateState,
        AggregateStateMethod,
        Queries,
        QueriesActor,
        DataObject,
        RestResource,
        RouteMethod,
        AutoDispatchResourceHandler,
        AutoDispatchMapping,
        AutoDispatchRoute,
        AutoDispatchHandlerEntry,
        AutoDispatchHandlersMapping,
        Adapter,
        Projection,
        ProjectionDispatcherProvider,
        ProjectionSourceTypes,
        ExchangeBootstrap,
        ExchangeMapper,
        ExchangeAdapter,
        ExchangeReceiverHolder,
        ExchangeProperties,
        XoomInitializer,
        Bootstrap,
        DatabaseProperties,
        DomainEvent,
        PersistenceSetup,
        SchemataSpecification,
        SchemataPlugin,
        StoreProvider,
        ExchangeDispatcher,
    }
    public class TemplateStandard
    {
        private static readonly string DefaultFileExtension = ".cs";

        private readonly Func<TemplateParameters, string>? _templateFileRetriever;
        private readonly Func<string, TemplateParameters, string>? _nameResolver;
        private readonly TemplateStandardType _templateStandardType;

        public TemplateStandard()
        {
        }

        public TemplateStandard(TemplateStandardType templateStandardType) => _templateStandardType = templateStandardType; 

        public TemplateStandard(Func<TemplateParameters, string> templateFileRetriever) : this(templateFileRetriever, (name, parameters) => name)
        {
        }

        public TemplateStandard(Func<TemplateParameters, string> templateFileRetriever, Func<string, TemplateParameters, string> nameResolver)
        {
            _templateFileRetriever = templateFileRetriever;
            _nameResolver = nameResolver;
        }

        public string RetrieveTemplateFilename(TemplateParameters parameters) => _templateFileRetriever!(parameters);

        public string ResolveClassname() => ResolveClassname("");

        public string ResolveClassname(string name) => ResolveClassname(name, null!);

        public string ResolveClassname(TemplateParameters parameters) => ResolveClassname(null!, parameters);

        public string ResolveClassname(string name, TemplateParameters parameters) => _nameResolver!(name, parameters);

        public string ResolveFilename(TemplateParameters parameters) => ResolveFilename(null!, parameters);

        public string ResolveFilename(string name, TemplateParameters parameters)
        {
            var fileName = _nameResolver!(name, parameters);
            return fileName.Contains(".") ? fileName : fileName + DefaultFileExtension;
        }
    }
}
