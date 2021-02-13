// Copyright © 2012-2020 VLINGO LABS. All rights reserved.
//
// This Source Code Form is subject to the terms of the
// Mozilla Public License, v. 2.0. If a copy of the MPL
// was not distributed with this file, You can obtain
// one at https://mozilla.org/MPL/2.0/.

using Vlingo.Common;
using Vlingo.Symbio;

namespace Vlingo.Xoom.Stepflow
{
    /// <summary>
    /// A functional interface that transforms a <see cref="StateTransition"/> into a <see cref="Completes"/>.
    /// </summary>
    /// <param name="T"> <T>  is the target <see cref="State"/> </param>
    /// <param name="R"> <R>  is the target <see cref="State"/> </param>
    public interface CompletesState<T,R> where T : IState where R : IState
    {
        void Apply(StateTransition<T, R, object> transition, R state);
    }
}
