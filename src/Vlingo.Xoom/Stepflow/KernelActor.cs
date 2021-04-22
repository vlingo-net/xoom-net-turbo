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
using Vlingo.Xoom.Common;

namespace Vlingo.Xoom.Stepflow
{
    /// <summary>
    /// The default <see cref="IKernel{TState, TRawState, TTypeState}"/> <see cref="Actor"/> implementation.
    ///  <see cref="Actor"/>
    /// </summary>
    public class KernelActor<TState, TRawState, TTypeState> : Actor, IKernel<TState, TRawState, TTypeState> where TState : State<object> where TRawState : State<object> where TTypeState : Type
    {
        private readonly Dictionary<string, TransitionHandler<TState, TRawState, TTypeState>> _transitionHandlerMap;
        private readonly Dictionary<string, State<TState>> _stateMap;
        private string _kernelName = "DefaultProcessorKernel";

        public KernelActor()
        {
            _transitionHandlerMap = new Dictionary<string, TransitionHandler<TState, TRawState, TTypeState>>();
            _stateMap = new Dictionary<string, State<TState>>();
        }

        public ICompletes<string> GetName() => Completes().With(_kernelName);

        public void SetName(string name) => _kernelName = name;

        public void RegisterStates(params State<TState>[] states)
        {
            states.ToList().ForEach(s =>
            {
                if (_stateMap.ContainsKey(s.GetName()))
                {
                    throw new InvalidOperationException(string.Concat("The state with the name ", s.GetName(), " has already been registered"));
                }

                s.GetTransitionHandlers<TTypeState>().ToList().ForEach(transitionHandler =>
                {
                    if (_transitionHandlerMap.TryGetValue(transitionHandler.GetAddress(), out var value))
                    {
                        throw new InvalidOperationException(string.Concat("The state transition for " + value + " is already registered"));
                    }
                });

                _stateMap.Add(s.GetName(), s);
            });

        }

        public ICompletes<IEnumerable<State<TState>>> GetStates() => Completes().With(_stateMap.Values.AsEnumerable());

        public ICompletes<IEnumerable<StateTransition<TState, TRawState, TTypeState>>> GetStateTransitions() => 
            Completes().With(_transitionHandlerMap.Values.Select(x => x.GetStateTransition()));

        public ICompletes<StateTransition<TState, TRawState, TTypeState>> ApplyEvent<TN>(TN @event) where TN : Event
        {
            TransitionHandler<TState, TRawState, TTypeState> handler = _transitionHandlerMap.First(x => x.Key == @event.GetEventType()).Value;
            try
            {
                if (handler == null)
                    throw new SystemException(string.Concat("The event with type [", @event.GetEventType(), "] does not match a" +
                            " valid transition handler in the processor kernel."));
                return Completes().With(handler.GetStateTransition());
            }
            catch (Exception ex)
            {
                Logger.Debug(ex.Message, ex);
                return Completes().With<StateTransition<TState, TRawState, TTypeState>>(null);
            }
        }

        public ICompletes<IReadOnlyDictionary<string, TransitionHandler<TState, TRawState, TTypeState>>> GetTransitionMap() => 
            Completes().With(_transitionHandlerMap as IReadOnlyDictionary<string, TransitionHandler<TState, TRawState, TTypeState>>);
    }
}
