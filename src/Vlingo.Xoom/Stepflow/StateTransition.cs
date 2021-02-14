// Copyright © 2012-2020 VLINGO LABS. All rights reserved.
//
// This Source Code Form is subject to the terms of the
// Mozilla Public License, v. 2.0. If a copy of the MPL
// was not distributed with this file, You can obtain
// one at https://mozilla.org/MPL/2.0/.

using System;
using Vlingo.Actors.Plugin.Logging.Console;

namespace Vlingo.Xoom.Stepflow
{
    /// <summary>
    /// A <see cref="StateTransition"/> is a resource specification that defines an input state and output state, while providing
    /// a validation error if an input state cannot progress to an output state.
    /// </summary>
    /// <param name="T"> <T>  is the target state </param>
    /// <param name="R"> <R>  is the target state </param>
    public class StateTransition<TState, TRawstate, A> : ITransition where TState : State<object> where TRawstate : State<object> where A : Type
    {
        private TState from;
        private TRawstate to;
        private Action<TState, TRawstate> action = (a, b) =>
        {
        };
        private Func<A, A> aggregateConsumer = (a) => a;

        public StateTransition(TState from, TRawstate to)
        {
            this.from = from;
            this.to = to;
        }

        public A Apply(A aggregate)
        {
            if (action == null)
            {
                throw new InvalidOperationException("A state transition must define a success and error result");
            }
            A a = aggregateConsumer(aggregate);
            action(GetFrom(), GetTo());
            return a;
        }

        public void SetActionHandler(Action<TState, TRawstate> action)
        {
            this.action = action;
        }

        public void SetAggregateConsumer(Func<A, A> consumer)
        {
            this.aggregateConsumer = consumer;
        }

        public TState GetFrom()
        {
            return from;
        }

        public TRawstate GetTo()
        {
            return to;
        }

        public string GetSourceName()
        {
            return GetFrom().GetType().Name;
        }

        public string GetTargetName()
        {
            return GetTo().GetType().Name;
        }

        public override string ToString()
        {
            return string.Concat("StateTransition{from=", from, ", to=", to, ", action=", action, "}");
        }

        public void LogResult<T1, R1>(T1 s, R1 t) where T1 : State<object> where R1 : State<object>
        {
            ConsoleLogger.BasicInstance().Info(string.Concat(s.GetVersion(), ": [", s.GetName(), "] to [", t.GetName(), "]"));
        }
    }
}
