// Copyright © 2012-2020 VLINGO LABS. All rights reserved.
//
// This Source Code Form is subject to the terms of the
// Mozilla Public License, v. 2.0. If a copy of the MPL
// was not distributed with this file, You can obtain
// one at https://mozilla.org/MPL/2.0/.

using System;
using System.Collections.Generic;
using System.Linq;
using Vlingo.Actors;
using Vlingo.Common;
using Vlingo.Symbio;

namespace Vlingo.Xoom.Stepflow
{
    /// <summary>
    /// The default <see cref="IKernel"/> <see cref="Actor"/> implementation.
    /// 
    ///  <see cref="Actor"/>
    ///  <see cref="IKernel"/>
    ///  <see cref="IStepFlow"/>
    /// </summary>
    public class KernelActor<T, R> : Actor, IKernel where T : IState where R : IState
    {
        private Dictionary<string, TransitionHandler<T, R>> transitionHandlerMap;
        private Dictionary<string, State<T>> stateMap;
        private string kernelName = "DefaultProcessorKernel";

        public KernelActor()
        {
            transitionHandlerMap = new Dictionary<string, TransitionHandler<T, R>>();
            stateMap = new Dictionary<string, State<T>>();
        }

        public ICompletes<string> GetName()
        {
            return Completes().With(this.kernelName);
        }

        public void SetName(string name)
        {
            this.kernelName = name;
        }

        public void RegisterStates(params State<T>[] states)
        {
            states.ToList().ForEach(s =>
            {
                if (stateMap.ContainsKey(s.GetName()))
                {
                    throw new InvalidOperationException(string.Concat("The state with the name ", s.GetName(), " has already been registered"));
                }

                s.GetTransitionHandlers().ToList().ForEach(transitionHandler =>
                {
                    if (transitionHandlerMap.TryGetValue(transitionHandler.GetAddress(), out var value))
                    {
                        throw new InvalidOperationException(string.Concat("The state transition for " + value + " is already registered"));
                    }
                });

                stateMap.Add(s.GetName(), s);
            });

        }


        public ICompletes<List<State<T>>> GetStates()
        {
            return Completes().With(stateMap.Values.ToList());
        }

        public ICompletes<List<StateTransition<T, R, object>>> getStateTransitions()
        {
            return Completes().With(transitionHandlerMap.Values.Select(x => x.GetStateTransition()).ToList());
        }

        public ICompletes<StateTransition<T, R, object>> ApplyEvent<N>(N @event) where N : Event
        {
            TransitionHandler<T, R> handler = transitionHandlerMap.First(x => x.Key == @event.GetEventType()).Value;
            try
            {
                if (handler == null)
                    throw new SystemException(string.Concat("The event with type [", @event.GetEventType(), "] does not match a" +
                            " valid transition handler in the processor kernel."));
                return Completes().With(handler.GetStateTransition());
            }
            catch (Exception ex)
            {
                //TODO:
                //logger().debug(ex.getMessage(), ex);
                return Completes().With<StateTransition<T, R, object>>(null);
            }
        }

        public ICompletes<Dictionary<string, TransitionHandler<T,R>>> GetTransitionMap()
        {
            return Completes().With(transitionHandlerMap);
        }

        public void RegisterStates<T1>(params State<T1>[] states) where T1 : IState
        {
            throw new NotImplementedException();
        }

        public ICompletes<List<State<T1>>> GetStates<T1>() where T1 : IState
        {
            throw new NotImplementedException();
        }

        public ICompletes<List<StateTransition<T1, R1, A>>> GetStateTransitions<T1, R1, A>()
            where T1 : IState
            where R1 : IState
        {
            throw new NotImplementedException();
        }

        public ICompletes<Dictionary<string, TransitionHandler<T1, R1>>> GetTransitionMap<T1, R1>()
            where T1 : IState
            where R1 : IState
        {
            throw new NotImplementedException();
        }

        public ICompletes<StateTransition<T1, R1, A>> ApplyEvent<T1, R1, A, N>(N @event)
            where T1 : IState
            where R1 : IState
            where N : Event
        {
            throw new NotImplementedException();
        }
    }
}
