// Copyright © 2012-2021 VLINGO LABS. All rights reserved.
//
// This Source Code Form is subject to the terms of the
// Mozilla Public License, v. 2.0. If a copy of the MPL
// was not distributed with this file, You can obtain
// one at https://mozilla.org/MPL/2.0/.

using System;
using System.Collections.Generic;

namespace Vlingo.Xoom.Turbo.Stepflow
{
    public class TransitionBuilder<TState, TRawState, TA> where TState : State<object> where TRawState : State<object> where TA : Type
    {
        private TState _source;
        private TRawState _target;
        private List<Action<TState, TRawState>> actions = new List<Action<TState, TRawState>>();

        private TransitionBuilder(TState source) => _source = source;

        private TransitionBuilder(TState source, TRawState target)
        {
            _source = source;
            _target = target;
        }

        public TransitionBuilder2<TState, TRawState1, TA1> To<TRawState1, TA1>(TRawState1 target) where TRawState1 : State<object> where TA1 : Type => new TransitionBuilder2<TState, TRawState1, TA1>(_source, target);

        public static TransitionBuilder<TState1, TRawState1, TA1> From<TState1, TRawState1, TA1>(TState1 source) where TState1 : State<object> where TRawState1 : State<object> where TA1 : Type => new TransitionBuilder<TState1, TRawState1, TA1>(source);

        public class TransitionBuilder2<TState1, TRawState1, TA1> where TState1 : State<object> where TRawState1 : State<object> where TA1 : Type
        {
            private TState1 _source;
            private TRawState1 _target;

            public TransitionBuilder2(TState1 source, TRawState1 target)
            {
                _source = source;
                _target = target;
            }

            public TransitionBuilder3<TState1, TRawState1, TA1> On(Type aggregateType) => new TransitionBuilder3<TState1, TRawState1, TA1>(_source, _target, aggregateType);

            public StateTransition<TState1, TRawState1, TA1> Then(Action<TState1, TRawState1> action)
            {
                StateTransition<TState1, TRawState1, TA1> transition = new StateTransition<TState1, TRawState1, TA1>(_source, _target);
                transition.SetActionHandler(action);
                return transition;
            }
        }

        public class TransitionBuilder3<TState1, TRawState1, TA1> where TState1 : State<object> where TRawState1 : State<object> where TA1 : Type
        {
            public TState1 source;
            public TRawState1 target;
            private Type _aggregateType;
            public Func<TA1, TA1> action;

            public TransitionBuilder3(TState1 source, TRawState1 target, Type aggregateType)
            {
                this.source = source;
                this.target = target;
                _aggregateType = aggregateType;
            }

            public TransitionBuilder4<TState1, TRawState1, TA1> Then(Func<TA1, TA1> aggregateConsumer)
            {
                action = aggregateConsumer;
                StateTransition<TState1, TRawState1, TA1> transition = new StateTransition<TState1, TRawState1, TA1>(source, target);
                transition.SetAggregateConsumer(aggregateConsumer);
                return new TransitionBuilder4<TState1, TRawState1, TA1>(this);
            }

            public StateTransition<TState1, TRawState1, TA2> AndThenAccept<TA2>(Action<TState1, TRawState1> consumer) where TA2 : Type
            {
                StateTransition<TState1, TRawState1, TA2> transition = new StateTransition<TState1, TRawState1, TA2>(source, target);
                transition.SetActionHandler(consumer);
                return transition;
            }
        }

        public class TransitionBuilder4<TState1, TRawState1, TA1> where TState1 : State<object> where TRawState1 : State<object> where TA1 : Type
        {
            private TransitionBuilder3<TState1, TRawState1, TA1> _transitionBuilder3;

            public TransitionBuilder4(TransitionBuilder3<TState1, TRawState1, TA1> transitionBuilder3) => _transitionBuilder3 = transitionBuilder3;

            public StateTransition<TState1, TRawState1, TA1> Then(Action<TState1, TRawState1> consumer)
            {
                StateTransition<TState1, TRawState1, TA1> transition = new StateTransition<TState1, TRawState1, TA1>(_transitionBuilder3.source, _transitionBuilder3.target);
                transition.SetAggregateConsumer(_transitionBuilder3.action);
                transition.SetActionHandler(consumer);
                return transition;
            }
        }
    }
}
