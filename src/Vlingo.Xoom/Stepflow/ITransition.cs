// Copyright © 2012-2020 VLINGO LABS. All rights reserved.
//
// This Source Code Form is subject to the terms of the
// Mozilla Public License, v. 2.0. If a copy of the MPL
// was not distributed with this file, You can obtain
// one at https://mozilla.org/MPL/2.0/.

using Vlingo.Symbio;

namespace Vlingo.Xoom.Stepflow
{
    /// <summary>
    /// A <see cref="Transition"/> is a base interface for a <see cref="StateTransition"/> and describes the identity of a source
    /// state and a target state.
    /// </summary>
    public interface ITransition 
    {
        string GetSourceName();

        string GetTargetName();

        void LogResult<T1, R1>(T1 s, R1 t) where T1 : IState where R1 : IState;
    }
}
