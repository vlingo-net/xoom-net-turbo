// Copyright © 2012-2020 VLINGO LABS. All rights reserved.
//
// This Source Code Form is subject to the terms of the
// Mozilla Public License, v. 2.0. If a copy of the MPL
// was not distributed with this file, You can obtain
// one at https://mozilla.org/MPL/2.0/.

using System;
using Vlingo.Symbio;

namespace Vlingo.Xoom.Stepflow
{
    /// <summary>
    /// A <see cref="TransitionHandler"/> subscribes to a <see cref="StateTransition"/> and is used to perform a transaction in response
    /// <see cref="StateTransition"/>.
    /// <param name=T"> T is the source state</param>
    /// <param name=R"> R is the source state</param>
    public class TransitionHandler<T, R> where T : IState where R : IState
    {
        private string address;
        private Type aggregateType = typeof(object);
        private StateTransition<T, R, object> stateTransition;

        private TransitionHandler(StateTransition<T, R, object> stateTransition)
        {
            this.stateTransition = stateTransition;
            this.address = string.Concat(stateTransition.GetSourceName(), "::", stateTransition.GetTargetName());
        }

        public TransitionHandler<T, R> WithAddress(string address)
        {
            this.address = string.Concat(this.address, "::", address);
            return this;
        }

        public TransitionHandler<T, R> WithAggregate(Type type)
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

        public StateTransition<T, R, object> GetStateTransition()
        {
            return stateTransition;
        }

        public static TransitionHandler<T1, R1> Handle<T1, R1>(StateTransition<T1, R1, object> stateTransition) where T1 : IState where R1 : IState
        {
            return new TransitionHandler<T1, R1>(stateTransition);
        }

        public static TransitionHandler<T1, R1>[] Transitions<T1, R1>(params TransitionHandler<T1, R1>[] handlers) where T1 : IState where R1 : IState
        {
            return handlers;
        }
    }
}
