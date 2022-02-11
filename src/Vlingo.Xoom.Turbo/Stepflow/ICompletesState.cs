// Copyright © 2012-2022 VLINGO LABS. All rights reserved.
//
// This Source Code Form is subject to the terms of the
// Mozilla Public License, v. 2.0. If a copy of the MPL
// was not distributed with this file, You can obtain
// one at https://mozilla.org/MPL/2.0/.

using System;
using Vlingo.Xoom.Common;

namespace Vlingo.Xoom.Turbo.Stepflow;

/// <summary>
/// A functional interface that transforms a <see cref="StateTransition{TState, TRawState, TA}"/> into a <see cref="Completes"/>.
/// </summary>
/// <typeparam name="TState"> is the target <see cref="State{T}"/></typeparam>
/// <typeparam name="TRawState"> is the target <see cref="State{T}"/></typeparam>
public interface ICompletesState<TState, TRawState> where TState : State<object> where TRawState : State<object>
{
    void Apply<TTypeState>(StateTransition<TState, TRawState, TTypeState> transition, TRawState state) where TTypeState : Type;
}