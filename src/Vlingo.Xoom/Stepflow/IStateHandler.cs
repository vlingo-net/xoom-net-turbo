// Copyright © 2012-2020 VLINGO LABS. All rights reserved.
//
// This Source Code Form is subject to the terms of the
// Mozilla Public License, v. 2.0. If a copy of the MPL
// was not distributed with this file, You can obtain
// one at https://mozilla.org/MPL/2.0/.

using Vlingo.Common;

namespace Vlingo.Xoom.Stepflow
{
    /// <summary>
    /// A <see cref="IStateHandler"> is a functional interface that describes a <see cref="StateTransition">.
    /// </summary>
    /// <param name="T"> T is the source <see cref="State"></param>
    /// <param name="R"> R is the source <see cref="State"></param>
    public interface IStateHandler<TState, TRawState> where TState : State<object> where TRawState : State<object>
    {
        ICompletes<StateTransition<TState, TRawState, object>> Execute();
    }
}
