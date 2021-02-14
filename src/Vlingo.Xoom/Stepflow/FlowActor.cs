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
using Vlingo.Symbio;

namespace Vlingo.Xoom.Stepflow
{
    /// <summary>
    /// The <see cref="FlowActor"/> is the default <see cref="Actor"/> implementation for a <see cref="StepFlow"/>.
    /// </summary>
    public abstract class FlowActor<TState, TRawState, TTypeState> : Actor, IStepFlow<TState, TRawState, TTypeState>, IScheduled<IMessage> where TState : State<object> where TRawState : State<object> where TTypeState : Type
    {
        private List<IState> states;
        private IKernel<TState, TRawState, TTypeState> kernel;

        public FlowActor()
        {
            states = new List<IState>();
        }

        protected FlowActor(List<IState> states)
        {
            this.states = states;
        }

        public ICompletes<bool> ShutDown()
        {
            Stop();
            return Completes().With(true);
        }

        public ICompletes<bool> StartUp()
        {
            ConsoleLogger.BasicInstance().Info(string.Concat("Starting ", this.Definition.ActorName, "..."));
            this.kernel = Stage.ActorFor<IKernel<TState, TRawState, TTypeState>>(typeof(IKernel<TState, TRawState, TTypeState>), typeof(KernelActor<TState, TRawState, TTypeState>));
            this.kernel.SetName(string.Concat(this.Definition.ActorName, "/Kernel"));
            this.kernel.RegisterStates(states.Select(x => (State<TState>)x).ToArray());
            return Completes().With(true);
        }

        public ICompletes<IKernel<TState, TRawState, TTypeState>> GetKernel()
        {
            if (kernel != null)
            {
                return Completes().With(kernel);
            }
            else
            {
                throw new InvalidOperationException("The processor's kernel has not been initialized.");
            }
        }

        public ICompletes<StateTransition<TState, TRawState, TTypeState>> ApplyEvent<TEventState>(TEventState @event) where TEventState : Event
        {
            return Completes().With(this.kernel.ApplyEvent(@event).Await());
        }

        public ICompletes<string> GetName()
        {
            return Completes().With("Default Processor");
        }

        public void IntervalSignal(IScheduled<IMessage> scheduled, IMessage data)
        {
        }

        public IStepFlow<TState, TRawState, TTypeState> StartWith(Stage stage, Type clazz, string actorName)
        {
            return StartWith(stage, clazz, actorName, Definition.NoParameters);
        }

        public P StartWith<P>(Stage stage, Type clazz, Type protocol, string actorName, List<object> @params) where P : IStepFlow<TState, TRawState, TTypeState>
        {
            P processor = stage.ActorFor<P>(protocol, Definition.Has(
                    clazz,
                    @params,
                    "queueMailbox", actorName),
                    stage.World.AddressFactory.WithHighId(),
                    stage.World.DefaultLogger);

            processor.StartUp();
            return processor;
        }

        public IStepFlow<TState, TRawState, TTypeState> StartWith(Stage stage, Type clazz, string actorName, List<object> @params)
        {
            IStepFlow<TState, TRawState, TTypeState> processor = stage.ActorFor<IStepFlow<TState, TRawState, TTypeState>>(typeof(IStepFlow<TState, TRawState, TTypeState>), Definition.Has(
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
