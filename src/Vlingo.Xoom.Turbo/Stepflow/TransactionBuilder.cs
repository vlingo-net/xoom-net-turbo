// Copyright © 2012-2023 VLINGO LABS. All rights reserved.
//
// This Source Code Form is subject to the terms of the
// Mozilla Public License, v. 2.0. If a copy of the MPL
// was not distributed with this file, You can obtain
// one at https://mozilla.org/MPL/2.0/.

using System;
using System.Collections.Generic;

namespace Vlingo.Xoom.Turbo.Stepflow;

public class TransitionBuilder<TState, TRawState, TA> where TState : State<object> where TRawState : State<object> where TA : Type
{
    private readonly TState _source;
    private TRawState? _target;
    private List<Action<TState, TRawState>> _actions = new List<Action<TState, TRawState>>();

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
        private readonly TState1 _source;
        private readonly TRawState1 _target;

        public TransitionBuilder2(TState1 source, TRawState1 target)
        {
            _source = source;
            _target = target;
        }

        public TransitionBuilder3<TState1, TRawState1, TA1> On(Type aggregateType) => new TransitionBuilder3<TState1, TRawState1, TA1>(_source, _target, aggregateType);

        public StateTransition<TState1, TRawState1, TA1> Then(Action<TState1, TRawState1> action)
        {
            var transition = new StateTransition<TState1, TRawState1, TA1>(_source, _target);
            transition.SetActionHandler(action);
            return transition;
        }
    }

    public class TransitionBuilder3<TState1, TRawState1, TA1> where TState1 : State<object> where TRawState1 : State<object> where TA1 : Type
    {
        public TState1 Source;
        public TRawState1 Target;
        private Type _aggregateType;
        public Func<TA1, TA1>? Action;

        public TransitionBuilder3(TState1 source, TRawState1 target, Type aggregateType)
        {
            Source = source;
            Target = target;
            _aggregateType = aggregateType;
        }

        public TransitionBuilder4<TState1, TRawState1, TA1> Then(Func<TA1, TA1> aggregateConsumer)
        {
            Action = aggregateConsumer;
            var transition = new StateTransition<TState1, TRawState1, TA1>(Source, Target);
            transition.SetAggregateConsumer(aggregateConsumer);
            return new TransitionBuilder4<TState1, TRawState1, TA1>(this);
        }

        public StateTransition<TState1, TRawState1, TA2> AndThenAccept<TA2>(Action<TState1, TRawState1> consumer) where TA2 : Type
        {
            var transition = new StateTransition<TState1, TRawState1, TA2>(Source, Target);
            transition.SetActionHandler(consumer);
            return transition;
        }
    }

    public class TransitionBuilder4<TState1, TRawState1, TA1> where TState1 : State<object> where TRawState1 : State<object> where TA1 : Type
    {
        private readonly TransitionBuilder3<TState1, TRawState1, TA1> _transitionBuilder3;

        public TransitionBuilder4(TransitionBuilder3<TState1, TRawState1, TA1> transitionBuilder3) => _transitionBuilder3 = transitionBuilder3;

        public StateTransition<TState1, TRawState1, TA1> Then(Action<TState1, TRawState1> consumer)
        {
            var transition = new StateTransition<TState1, TRawState1, TA1>(_transitionBuilder3.Source, _transitionBuilder3.Target);
            transition.SetAggregateConsumer(_transitionBuilder3.Action!);
            transition.SetActionHandler(consumer);
            return transition;
        }
    }
}