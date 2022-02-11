// Copyright © 2012-2022 VLINGO LABS. All rights reserved.
//
// This Source Code Form is subject to the terms of the
// Mozilla Public License, v. 2.0. If a copy of the MPL
// was not distributed with this file, You can obtain
// one at https://mozilla.org/MPL/2.0/.

using System;
using System.Collections.Generic;
using Vlingo.Xoom.Common;

namespace Vlingo.Xoom.Turbo.Stepflow;

/// <summary>
/// Kernels implement state machines and validate state mutations by comparing the current state of an object to a
/// desired state.Kernels do not tightly couple themselves to event messages.Event messages do not directly inform the
/// state of an entity, rather, handlers are responsible for accepting or rejecting a state-carried transfer.The kernel
/// is a consumer and producer of state mutations that are subscribed to by a collection of handlers.
/// 
/// A kernel has a collection of input states that map to output states. When a state transfer is received by a
/// kernel, it can either accept or reject the request.A state transfer is applied to the underlying adapter during
/// validation by the kernel.If the underlying storage reflects a different state of an entity than the input state,
/// the state transfer will be rejected as a part of a transactional guarantee provided by the storage
/// implementation.
/// 
/// Kernels do not tightly couple to storage implementations. The processor must provide a transactional function to
/// the kernel.The transaction will compare the input state(stale read) to the actual state.This guarantees that any
/// stale reads are rejected.
/// 
/// Kernels implement state machines that version an entity as mutations are applied. All state mutations are
/// applied in a linear order, and are guaranteed to replicate the state of an entity.Versions of an entity are
/// incremented whenever a state mutation is accepted by the kernel. Output states can equal their input states.
/// A field update to an entity may not require a logical state transition. A valid input state could be a list or none.
/// When no input state is provided, the logical state will remain the same while the versioned state will be
/// incremented.
/// </summary>
public interface IKernel<TState, TRawState, TTypeState> where TState : State<object> where TRawState : State<object> where TTypeState : Type
{
    ICompletes<string> GetName();

    void SetName(string name);

    void RegisterStates(params State<TState>[] states);

    ICompletes<IEnumerable<State<TState>>> GetStates();

    ICompletes<IEnumerable<StateTransition<TState, TRawState, TTypeState>>> GetStateTransitions();

    ICompletes<IReadOnlyDictionary<string, TransitionHandler<TState, TRawState, TTypeState>>> GetTransitionMap();

    ICompletes<StateTransition<TState, TRawState, TTypeState>> ApplyEvent<TEventState>(TEventState @event) where TEventState : Event;
}