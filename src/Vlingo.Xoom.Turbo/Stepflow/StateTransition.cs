// Copyright © 2012-2023 VLINGO LABS. All rights reserved.
//
// This Source Code Form is subject to the terms of the
// Mozilla Public License, v. 2.0. If a copy of the MPL
// was not distributed with this file, You can obtain
// one at https://mozilla.org/MPL/2.0/.

using System;
using Vlingo.Xoom.Actors.Plugin.Logging.Console;

namespace Vlingo.Xoom.Turbo.Stepflow;

/// <summary>
/// A <see cref="StateTransition{TState, TRawstate, TA}"/> is a resource specification that defines an input state and output state, while providing
/// a validation error if an input state cannot progress to an output state.
/// </summary>
/// <typeparam name="TState"> is the target state</typeparam>
/// <typeparam name="TRawState"> is the target state</typeparam>
/// <typeparam name="TA">this is useless</typeparam>
public class StateTransition<TState, TRawState, TA> : ITransition where TState : State<object> where TRawState : State<object> where TA : Type
{
    private readonly TState _from;
    private readonly TRawState _to;
    private Action<TState, TRawState> _action = (a, b) =>
    {
    };
        
    private Func<TA, TA> _aggregateConsumer = a => a;

    public StateTransition(TState from, TRawState to)
    {
        _from = from;
        _to = to;
    }

    public TA Apply(TA aggregate)
    {
        if (_action == null)
        {
            throw new InvalidOperationException("A state transition must define a success and error result");
        }
        var a = _aggregateConsumer(aggregate);
        _action(GetFrom(), GetTo());
        return a;
    }

    public void SetActionHandler(Action<TState, TRawState> action) => _action = action;

    public void SetAggregateConsumer(Func<TA, TA> consumer) => _aggregateConsumer = consumer;

    public TState GetFrom() => _from;

    public TRawState GetTo() => _to;

    public string GetSourceName() => GetFrom().GetType().Name;

    public string GetTargetName() => GetTo().GetType().Name;

    public override string ToString() => string.Concat("StateTransition{from=", _from, ", to=", _to, ", action=", _action, "}");

    public void LogResult<T1, TR1>(T1 s, TR1 t) where T1 : State<object> where TR1 : State<object> => ConsoleLogger.BasicInstance().Info(string.Concat(s.GetVersion(), ": [", s.GetName(), "] to [", t.GetName(), "]"));
}