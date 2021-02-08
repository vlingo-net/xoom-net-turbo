// Copyright © 2012-2020 VLINGO LABS. All rights reserved.
//
// This Source Code Form is subject to the terms of the
// Mozilla Public License, v. 2.0. If a copy of the MPL
// was not distributed with this file, You can obtain
// one at https://mozilla.org/MPL/2.0/.

using System.ComponentModel;

namespace io.vlingo.xoom.codegen.template {
    public enum Template {
        [Description("AnnotatedBootstrap")]
        ANNOTATED_BOOTSTRAP,

        [Description("DefaultBootstrap")]
        DEFAULT_BOOTSTRAP,

        [Description("AggregateProtocol")]
        AGGREGATE_PROTOCOL,

        [Description("AggregateProtocolInstanceMethod")]
        AGGREGATE_PROTOCOL_INSTANCE_METHOD,

        [Description("AggregateProtocolStaticMethod")]
        AGGREGATE_PROTOCOL_STATIC_METHOD,

        [Description("AggregateStateMethod")]
        AGGREGATE_STATE_METHOD,

        [Description("ObjectEntity")]
        OBJECT_ENTITY,

        [Description("StatefulEntity")]
        STATEFUL_ENTITY,

        [Description("StatefulEntityMethod")]
        STATEFUL_ENTITY_METHOD,

        [Description("EventSourcedEntity")]
        EVENT_SOURCE_ENTITY,

        [Description("EventSourcedEntityMethod")]
        EVENT_SOURCE_ENTITY_METHOD,

        [Description("AggregateState")]
        AGGREGATE_STATE,

        [Description("DomainEvent")]
        DOMAIN_EVENT,

        [Description("JournalProvider")]
        JOURNAL_PROVIDER,

        [Description("PersistenceSetup")]
        PERSISTENCE_SETUP,

        [Description("StateStoreProvider")]
        STATE_STORE_PROVIDER,

        [Description("ObjectStoreProvider")]
        OBJECT_STORE_PROVIDER,

        [Description("EntryAdapter")]
        ENTRY_ADAPTER,

        [Description("StateAdapter")]
        STATE_ADAPTER,

        [Description("RestResource")]
        REST_RESOURCE,

        [Description("AutoDispatchMapping")]
        AUTO_DISPATCH_MAPPING,

        [Description("AutoDispatchRoute")]
        AUTO_DISPATCH_ROUTE,

        [Description("AutoDispatchHandlersMapping")]
        AUTO_DISPATCH_HANDLERS_MAPPING,

        [Description("AutoDispatchHandlerEntry")]
        AUTO_DISPATCH_HANDLER_ENTRY,

        [Description("XoomInitializer")]
        XOOM_INITIALIZER,

        [Description("ProjectionDispatcherProvider")]
        PROJECTION_DISPATCHER_PROVIDER,

        [Description("OperationBasedProjection")]
        OPERATION_BASED_PROJECTION,

        [Description("EventBasedProjection")]
        EVENT_BASED_PROJECTION,

        [Description("DataObject")]
        DATA_OBJECT,

        [Description("ProjectionSourceTypes")]
        PROJECTION_SOURCE_TYPES,

        [Description("DatabaseProperties")]
        DATABASE_PROPERTIES,

        [Description("Queries")]
        QUERIES,

        [Description("RestResourceCreationMethod")]
        REST_RESOURCE_CREATION_METHOD,

        [Description("RestResourceRetrieveMethod")]
        REST_RESOURCE_RETRIEVE_METHOD,

        [Description("RestResourceUpdateMethod")]
        REST_RESOURCE_UPDATE_METHOD,

        [Description("QueriesActor")]
        QUERIES_ACTOR,

        [Description("ConsumerExchangeAdapter")]
        CONSUMER_EXCHANGE_ADAPTER,

        [Description("ProducerExchangeAdapter")]
        PRODUCER_EXCHANGE_ADAPTER,

        [Description("ExchangeReceiverHolder")]
        EXCHANGE_RECEIVER_HOLDER,

        [Description("ConsumerExchangeMapper")]
        CONSUMER_EXCHANGE_MAPPER,

        [Description("ProducerExchangeMapper")]
        PRODUCER_EXCHANGE_MAPPER,

        [Description("ExchangeProperties")]
        EXCHANGE_PROPERTIES,

        [Description("ExchangeBootstrap")]
        EXCHANGE_BOOTSTRAP,

        [Description("ExchangeDispatcher")]
        EXCHANGE_DISPATCHER,

        [Description("SchemataSpecification")]
        SCHEMATA_SPECIFICATION,

        [Description("SchemataPlugin")]
        SCHEMATA_PLUGIN
    }
}
