// Copyright © 2012-2020 VLINGO LABS. All rights reserved.
//
// This Source Code Form is subject to the terms of the
// Mozilla Public License, v. 2.0. If a copy of the MPL
// was not distributed with this file, You can obtain
// one at https://mozilla.org/MPL/2.0/.

using System;
using System.Collections.Generic;
using System.Linq;
using Vlingo.Symbio;

namespace Vlingo.Xoom.Stepflow
{
    /// <summary>
    /// A <see cref="IState"> is an interface definition that should describe a collection of input states and output states.
    /// </summary>
    public abstract class State<TState> where TState : class
    {
        private long createdAt;
        private Guid version;
        private string name;

        public State()
        {
            SetName(this.GetName());
            if (name == null)
            {
                throw new SystemException(string.Concat("A state must override GetName() for ", this.GetType().Name));
            }

            this.createdAt = DateTime.Now.Ticks;
            this.version = Guid.NewGuid();
        }

        public long GetCreatedAt()
        {
            return this.createdAt;
        }

        public Guid GetVersion()
        {
            return version;
        }

        private void SetName(string name)
        {
            this.name = name;
        }

        public string GetName()
        {
            return this.name;
        }

        public abstract TransitionHandler<State<object>, State<object>, TTypeState>[] GetTransitionHandlers<TTypeState>() where TTypeState : Type;

        public Dictionary<string, HashSet<string>> GetMap<TTypeState>() where TTypeState : Type
        {
            Dictionary<string, HashSet<string>> result = new Dictionary<string, HashSet<string>>();

            var values = new HashSet<string>();
            this.GetTransitionHandlers<TTypeState>().Select(t => (ITransition)t.GetStateTransition()).ToList().ForEach(x =>
            {
                if (!result.TryGetValue(x.GetSourceName(), out var value))
                {
                    result.Add(x.GetSourceName(), new HashSet<string>());
                }
                result[x.GetSourceName()].Add(x.GetTargetName());
            });

            return result;
        }

        public string ToGraph<TTypeState>() where TTypeState : Type
        {
            return string.Join("\n", this.GetMap<TTypeState>().Select(x => string.Concat("(", x.Key, ")-->(", x.Value, ")")));
        }

        public override string ToString()
        {
            return string.Concat("State{createdAt=", createdAt, ", version=", version, ", name='", name, "\'}");
        }
    }
}
