// Copyright © 2012-2020 VLINGO LABS. All rights reserved.
//
// This Source Code Form is subject to the terms of the
// Mozilla Public License, v. 2.0. If a copy of the MPL
// was not distributed with this file, You can obtain
// one at https://mozilla.org/MPL/2.0/.

using System;
using System.Collections.Generic;
using System.Linq;
using Vlingo.Xoom.Common;
using Vlingo.Xoom.Symbio;
using Vlingo.Xoom.Actors;

namespace Vlingo.Xoom.Stepflow
{
    /// <summary>
    /// The <see cref="FlowActor{TState, TRawState, TTypeState}"/> is the default <see cref="Actor"/> implementation for a <see cref="IStepFlow{TState, TRawState, TTypeState}"/>.
    /// </summary>
    public abstract class FlowActor<TState, TRawState, TTypeState> : Actor, IStepFlow<TState, TRawState, TTypeState>, IScheduled<IMessage> where TState : State<object> where TRawState : State<object> where TTypeState : Type
    {
        private readonly List<IState> _states;
        private IKernel<TState, TRawState, TTypeState> _kernel;

        public FlowActor() => _states = new List<IState>();

        protected FlowActor(List<IState> states) => _states = states;

        public ICompletes<bool> ShutDown()
        {
            Stop();
            return Completes().With(true);
        }

        public ICompletes<bool> StartUp()
        {
            Logger.Info(string.Concat("Starting ", Definition.ActorName, "..."));
            _kernel = Stage.ActorFor<IKernel<TState, TRawState, TTypeState>>(typeof(IKernel<TState, TRawState, TTypeState>), typeof(KernelActor<TState, TRawState, TTypeState>));
            _kernel.SetName(string.Concat(Definition.ActorName, "/Kernel"));
            _kernel.RegisterStates(_states.Select(x => (State<TState>)x).ToArray());
            return Completes().With(true);
        }

        public ICompletes<IKernel<TState, TRawState, TTypeState>> GetKernel()
        {
            if (_kernel != null)
            {
                return Completes().With(_kernel);
            }

            throw new InvalidOperationException("The processor's kernel has not been initialized.");
        }

        public ICompletes<StateTransition<TState, TRawState, TTypeState>> ApplyEvent<TEventState>(TEventState @event) where TEventState : Event => 
            Completes().With(_kernel.ApplyEvent(@event).Await());

        public ICompletes<string> GetName() => 
            Completes().With("Default Processor");

        public void IntervalSignal(IScheduled<IMessage> scheduled, IMessage data)
        {
        }

        public IStepFlow<TState, TRawState, TTypeState> StartWith(Stage stage, Type clazz, string actorName) => 
            StartWith(stage, clazz, actorName, Definition.NoParameters);

        public TP StartWith<TP>(Stage stage, Type clazz, Type protocol, string actorName, IEnumerable<object> @params) where TP : IStepFlow<TState, TRawState, TTypeState>
        {
            var processor = stage.ActorFor<TP>(protocol, Definition.Has(
                    clazz,
                    @params,
                    "queueMailbox", actorName),
                    stage.World.AddressFactory.WithHighId(),
                    stage.World.DefaultLogger);

            processor.StartUp();
            return processor;
        }

        public IStepFlow<TState, TRawState, TTypeState> StartWith(Stage stage, Type clazz, string actorName, IEnumerable<object> @params)
        {
            var processor = stage.ActorFor<IStepFlow<TState, TRawState, TTypeState>>(typeof(IStepFlow<TState, TRawState, TTypeState>), Definition.Has(
                    clazz,
                    @params,
                    "queueMailbox", actorName),
                    stage.World.AddressFactory.WithHighId(),
                    stage.World.DefaultLogger);

            processor.StartUp();
            return processor;
        }
    }
}
