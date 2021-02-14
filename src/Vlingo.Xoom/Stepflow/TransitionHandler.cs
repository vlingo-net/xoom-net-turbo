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
    public class TransitionHandler<TState, TRawState> where TState : State<object> where TRawState : State<object>
    {
        private string address;
        private Type aggregateType = typeof(object);
        private StateTransition<TState, TRawState, object> stateTransition;

        private TransitionHandler(StateTransition<TState, TRawState, object> stateTransition)
        {
            this.stateTransition = stateTransition;
            this.address = string.Concat(stateTransition.GetSourceName(), "::", stateTransition.GetTargetName());
        }

        public TransitionHandler<TState, TRawState> WithAddress(string address)
        {
            this.address = string.Concat(this.address, "::", address);
            return this;
        }

        public TransitionHandler<TState, TRawState> WithAggregate(Type type)
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

        public StateTransition<TState, TRawState, object> GetStateTransition()
        {
            return stateTransition;
        }

        public static TransitionHandler<TState, TRawState> Handle(StateTransition<TState, TRawState, object> stateTransition)
        {
            return new TransitionHandler<TState, TRawState>(stateTransition);
        }

        public static TransitionHandler<TState, TRawState>[] Transitions(params TransitionHandler<TState, TRawState>[] handlers)
        {
            return handlers;
        }
    }
}
