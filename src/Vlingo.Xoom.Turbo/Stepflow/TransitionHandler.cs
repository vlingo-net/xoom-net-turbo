// Copyright © 2012-2022 VLINGO LABS. All rights reserved.
//
// This Source Code Form is subject to the terms of the
// Mozilla Public License, v. 2.0. If a copy of the MPL
// was not distributed with this file, You can obtain
// one at https://mozilla.org/MPL/2.0/.

using System;

namespace Vlingo.Xoom.Turbo.Stepflow;

/// <summary>
/// A <see cref="TransitionHandler{TState, TRawState, TTypeState}"/> subscribes to a <see cref="StateTransition{TState, TRawState, TA}"/> and is used to perform a transaction in response
/// <see cref="StateTransition{TState, TRawState, TA}"/>.
/// </summary>
/// <typeparam name="TState">Is the source state</typeparam>
/// <typeparam name="TRawState">Is the source state</typeparam>
/// <typeparam name="TTypeState"></typeparam>
public class TransitionHandler<TState, TRawState, TTypeState> where TState : State<object> where TRawState : State<object> where TTypeState : Type
{
    private string _address;
    private Type _aggregateType = typeof(TTypeState);
    private readonly StateTransition<TState, TRawState, TTypeState> _stateTransition;

    private TransitionHandler(StateTransition<TState, TRawState, TTypeState> stateTransition)
    {
        _stateTransition = stateTransition;
        _address = string.Concat(stateTransition.GetSourceName(), "::", stateTransition.GetTargetName());
    }

    public TransitionHandler<TState, TRawState, TTypeState> WithAddress(string address)
    {
        _address = string.Concat(_address, "::", address);
        return this;
    }

    public TransitionHandler<TState, TRawState, TTypeState> WithAggregate(TTypeState type)
    {
        _aggregateType = type;
        return this;
    }

    public string GetAddress() => _address;

    public Type GetAggregateType() => _aggregateType;

    public StateTransition<TState, TRawState, TTypeState> GetStateTransition() => _stateTransition;

    public static TransitionHandler<TState, TRawState, TTypeState> Handle(StateTransition<TState, TRawState, TTypeState> stateTransition) => 
        new TransitionHandler<TState, TRawState, TTypeState>(stateTransition);

    public static TransitionHandler<TState, TRawState, TTypeState>[] Transitions(params TransitionHandler<TState, TRawState, TTypeState>[] handlers) => 
        handlers;
}