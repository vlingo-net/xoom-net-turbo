// Copyright © 2012-2020 VLINGO LABS. All rights reserved.
//
// This Source Code Form is subject to the terms of the
// Mozilla Public License, v. 2.0. If a copy of the MPL
// was not distributed with this file, You can obtain
// one at https://mozilla.org/MPL/2.0/.

using System;
using Vlingo.Common;

namespace Vlingo.Xoom.Stepflow
{
    /// <summary>
    /// A <see cref="IStateHandler{TState, TRawState, TTypeState}" /> is a functional interface that describes a <see cref="StateTransition{TState, TRawState, TA}" />.
    /// </summary>
    /// <typeparam name="TState">The source <see cref="State{T}" /></typeparam>
    /// <typeparam name="TRawState">The source <see cref="State{T}" /></typeparam>
    /// <typeparam name="TTypeState">The type of the handler</typeparam>
    public interface IStateHandler<TState, TRawState, TTypeState> where TState : State<object> where TRawState : State<object> where TTypeState : Type
    {
        ICompletes<StateTransition<TState, TRawState, TTypeState>> Execute();
    }
}
