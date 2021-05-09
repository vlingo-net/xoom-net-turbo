// Copyright © 2012-2021 VLINGO LABS. All rights reserved.
//
// This Source Code Form is subject to the terms of the
// Mozilla Public License, v. 2.0. If a copy of the MPL
// was not distributed with this file, You can obtain
// one at https://mozilla.org/MPL/2.0/.

namespace Vlingo.Xoom.Stepflow
{
    /// <summary>
    /// A <see cref="ITransition"/> is a base interface for a <see cref="StateTransition{TState, TRawState, TA}"/> and describes the identity of a source
    /// state and a target state.
    /// </summary>
    public interface ITransition
    {
        string GetSourceName();

        string GetTargetName();

        void LogResult<TState, TRawState>(TState s, TRawState t) where TState : State<object> where TRawState : State<object>;
    }
}
