// Copyright © 2012-2022 VLINGO LABS. All rights reserved.
//
// This Source Code Form is subject to the terms of the
// Mozilla Public License, v. 2.0. If a copy of the MPL
// was not distributed with this file, You can obtain
// one at https://mozilla.org/MPL/2.0/.

using System;
using System.Collections.Generic;
using System.Linq;
using Vlingo.Xoom.Symbio;

namespace Vlingo.Xoom.Turbo.Stepflow;

/// <summary>
/// A <see cref="IState" /> is an interface definition that should describe a collection of input states and output states.
/// </summary>
public abstract class State<TState> where TState : class
{
    private readonly long _createdAt;
    private readonly Guid _version;
    private string _name;

    public State()
    {
        SetName(GetName());
        if (_name == null)
        {
            throw new SystemException(string.Concat("A state must override GetName() for ", GetType().Name));
        }

        _createdAt = DateTime.Now.Ticks;
        _version = Guid.NewGuid();
    }

    public long GetCreatedAt() => _createdAt;

    public Guid GetVersion() => _version;

    private void SetName(string name) => _name = name;

    public string GetName() => _name;

    public abstract TransitionHandler<State<object>, State<object>, TTypeState>[] GetTransitionHandlers<TTypeState>() where TTypeState : Type;

    public Dictionary<string, HashSet<string>> GetMap<TTypeState>() where TTypeState : Type
    {
        var result = new Dictionary<string, HashSet<string>>();

        var values = new HashSet<string>();
        GetTransitionHandlers<TTypeState>().Select(t => (ITransition)t.GetStateTransition()).ToList().ForEach(x =>
        {
            if (!result.TryGetValue(x.GetSourceName(), out var value))
            {
                result.Add(x.GetSourceName(), new HashSet<string>());
            }
            result[x.GetSourceName()].Add(x.GetTargetName());
        });

        return result;
    }

    public string ToGraph<TTypeState>() where TTypeState : Type => 
        string.Join("\n", GetMap<TTypeState>().Select(x => string.Concat("(", x.Key, ")-->(", x.Value, ")")));

    public override string ToString() => 
        string.Concat("State{createdAt=", _createdAt, ", version=", _version, ", name='", _name, "\'}");
}