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
using Vlingo.Actors.Plugin.Logging.Console;
using Vlingo.Common;

namespace Vlingo.Xoom.Stepflow
{
    /// <summary>
    /// The default <see cref="IKernel"/> <see cref="Actor"/> implementation.
    /// 
    ///  <see cref="Actor"/>
    ///  <see cref="IKernel"/>
    ///  <see cref="IStepFlow"/>
    /// </summary>
    public class KernelActor<TState, TRawState, TTypeState> : Actor, IKernel<TState, TRawState, TTypeState> where TState : State<object> where TRawState : State<object> where TTypeState : Type
    {
        private Dictionary<string, TransitionHandler<TState, TRawState, TTypeState>> transitionHandlerMap;
        private Dictionary<string, State<TState>> stateMap;
        private string kernelName = "DefaultProcessorKernel";

        public KernelActor()
        {
            transitionHandlerMap = new Dictionary<string, TransitionHandler<TState, TRawState, TTypeState>>();
            stateMap = new Dictionary<string, State<TState>>();
        }

        public ICompletes<string> GetName()
        {
            return Completes().With(this.kernelName);
        }

        public void SetName(string name)
        {
            this.kernelName = name;
        }

        public void RegisterStates(params State<TState>[] states)
        {
            states.ToList().ForEach(s =>
            {
                if (stateMap.ContainsKey(s.GetName()))
                {
                    throw new InvalidOperationException(string.Concat("The state with the name ", s.GetName(), " has already been registered"));
                }

                s.GetTransitionHandlers<TTypeState>().ToList().ForEach(transitionHandler =>
                {
                    if (transitionHandlerMap.TryGetValue(transitionHandler.GetAddress(), out var value))
                    {
                        throw new InvalidOperationException(string.Concat("The state transition for " + value + " is already registered"));
                    }
                });

                stateMap.Add(s.GetName(), s);
            });

        }

        public ICompletes<List<State<TState>>> GetStates()
        {
            return Completes().With(stateMap.Values.ToList());
        }

        public ICompletes<List<StateTransition<TState, TRawState, TTypeState>>> GetStateTransitions()
        {
            return Completes().With(transitionHandlerMap.Values.Select(x => x.GetStateTransition()).ToList());
        }

        public ICompletes<StateTransition<TState, TRawState, TTypeState>> ApplyEvent<N>(N @event) where N : Event
        {
            TransitionHandler<TState, TRawState, TTypeState> handler = transitionHandlerMap.First(x => x.Key == @event.GetEventType()).Value;
            try
            {
                if (handler == null)
                    throw new SystemException(string.Concat("The event with type [", @event.GetEventType(), "] does not match a" +
                            " valid transition handler in the processor kernel."));
                return Completes().With(handler.GetStateTransition());
            }
            catch (Exception ex)
            {
                ConsoleLogger.BasicInstance().Debug(ex.Message, ex);
                return Completes().With<StateTransition<TState, TRawState, TTypeState>>(null);
            }
        }

        public ICompletes<Dictionary<string, TransitionHandler<TState, TRawState, TTypeState>>> GetTransitionMap()
        {
            return Completes().With(transitionHandlerMap);
        }
    }
}
