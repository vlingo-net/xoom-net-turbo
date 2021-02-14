// Copyright © 2012-2020 VLINGO LABS. All rights reserved.
//
// This Source Code Form is subject to the terms of the
// Mozilla Public License, v. 2.0. If a copy of the MPL
// was not distributed with this file, You can obtain
// one at https://mozilla.org/MPL/2.0/.

using System;

namespace Vlingo.Xoom.Stepflow
{
    /// <summary>
    /// A <see cref="TransitionHandler"/> subscribes to a <see cref="StateTransition"/> and is used to perform a transaction in response
    /// <see cref="StateTransition"/>.
    /// <param name=T"> T is the source state</param>
    /// <param name=R"> R is the source state</param>
    public class TransitionHandler<TState, TRawState, TTypeState> where TState : State<object> where TRawState : State<object> where TTypeState : Type
    {
        private string address;
        private Type aggregateType = typeof(TTypeState);
        private StateTransition<TState, TRawState, TTypeState> stateTransition;

        private TransitionHandler(StateTransition<TState, TRawState, TTypeState> stateTransition)
        {
            this.stateTransition = stateTransition;
            this.address = string.Concat(stateTransition.GetSourceName(), "::", stateTransition.GetTargetName());
        }

        public TransitionHandler<TState, TRawState, TTypeState> WithAddress(string address)
        {
            this.address = string.Concat(this.address, "::", address);
            return this;
        }

        public TransitionHandler<TState, TRawState, TTypeState> WithAggregate(TTypeState type)
        {
            this.aggregateType = type;
            return this;
        }

        public string GetAddress()
        {
            return address;
        }

        public Type GetAggregateType()
        {
            return aggregateType;
        }

        public StateTransition<TState, TRawState, TTypeState> GetStateTransition()
        {
            return stateTransition;
        }

        public static TransitionHandler<TState, TRawState, TTypeState> Handle(StateTransition<TState, TRawState, TTypeState> stateTransition)
        {
            return new TransitionHandler<TState, TRawState, TTypeState>(stateTransition);
        }

        public static TransitionHandler<TState, TRawState, TTypeState>[] Transitions(params TransitionHandler<TState, TRawState, TTypeState>[] handlers)
        {
            return handlers;
        }
    }
}
