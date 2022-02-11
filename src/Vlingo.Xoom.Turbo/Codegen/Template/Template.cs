// Copyright © 2012-2022 VLINGO LABS. All rights reserved.
//
// This Source Code Form is subject to the terms of the
// Mozilla Public License, v. 2.0. If a copy of the MPL
// was not distributed with this file, You can obtain
// one at https://mozilla.org/MPL/2.0/.

namespace Vlingo.Xoom.Turbo.Codegen.Template;

public enum Template
{
    AnnotatedBootstrap,
    DefaultBootstrap,
    AggregateProtocol,
    AggregateProtocolInstanceMethod,
    AggregateProtocolStaticMethod,
    AggregateStateMethod,
    ObjectEntity,
    StatefulEntity,
    StatefulEntityMethod,
    EventSourceEntity,
    EventSourceEntityMethod,
    AggregateState,
    DomainEvent,
    JournalProvider,
    PersistenceSetup,
    StateStoreProvider,
    ObjectStoreProvider,
    EntryAdapter,
    StateAdapter,
    RestResource,
    AutoDispatchMapping,
    AutoDispatchRoute,
    AutoDispatchHandlersMapping,
    AutoDispatchHandlerEntry,
    XoomInitializer,
    ProjectionDispatcherProvider,
    OperationBasedProjection,
    EventBasedProjection,
    DataObject,
    ProjectionSourceTypes,
    DatabaseProperties,
    Queries,
    RestResourceCreationMethod,
    RestResourceRetrieveMethod,
    RestResourceUpdateMethod,
    QueriesActor,
    ConsumerExchangeAdapter,
    ProducerExchangeAdapter,
    ExchangeReceiverHolder,
    ConsumerExchangeMapper,
    ProducerExchangeMapper,
    ExchangeProperties,
    ExchangeBootstrap,
    ExchangeDispatcher,
    SchemataSpecification,
    SchemataPlugin
}