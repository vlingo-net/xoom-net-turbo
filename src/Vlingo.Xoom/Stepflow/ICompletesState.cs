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
    /// A functional interface that transforms a <see cref="StateTransition"/> into a <see cref="Completes"/>.
    /// </summary>
    /// <param name="T"> <T>  is the target <see cref="State"/> </param>
    /// <param name="R"> <R>  is the target <see cref="State"/> </param>
    public interface ICompletesState<TState, TRawState> where TState : State<object> where TRawState : State<object>
    {
        void Apply<TTypeState>(StateTransition<TState, TRawState, TTypeState> transition, TRawState state) where TTypeState : Type;
    }
}
