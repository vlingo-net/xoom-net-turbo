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
    /// The <see cref="FlowActor"/> is the default <see cref="Actor"/> implementation for a <see cref="StepFlow"/>.
    /// </summary>
    public abstract class FlowActor : Actor, IStepFlow, IScheduled<IMessage>
    {
        private List<IState> states;
        private IKernel kernel;

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

        public ICompletes<bool> StartUp<T, R>() where T : IState where R : IState
        {
            //TODO:
            //logger().info("Starting " + this.definition().actorName() + "...");
            this.kernel = Stage.ActorFor<IKernel>(typeof(IKernel), typeof(KernelActor<T, R>));
            this.kernel.SetName(string.Concat(this.Definition.ActorName, "/Kernel"));
            this.kernel.RegisterStates(states.Select(x => (State<T>)x).ToArray());
            return Completes().With(true);
        }

        public ICompletes<IKernel> GetKernel()
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

        public ICompletes<StateTransition<T, R, A>> ApplyEvent<T, R, A, N>(N @event) where N : Event where T : IState where R : IState
        {
            return Completes().With(this.kernel.ApplyEvent<T, R, A, N>(@event).Await());
        }

        public ICompletes<string> GetName()
        {
            return Completes().With("Default Processor");
        }

        public void IntervalSignal(IScheduled<IMessage> scheduled, IMessage data)
        {
        }

        public IStepFlow StartWith<T, R>(Stage stage, Type clazz, string actorName) where T : IState where R : IState
        {
            return StartWith<T, R>(stage, clazz, actorName, Definition.NoParameters);
        }

        public P StartWith<P, T, R>(Stage stage, Type clazz, Type protocol, string actorName, List<object> @params) where P : IStepFlow where T : IState where R : IState
        {
            P processor = stage.ActorFor<P>(protocol, Definition.Has(
                    clazz,
                    @params,
                    "queueMailbox", actorName),
                    stage.World.AddressFactory.WithHighId(),
                    stage.World.DefaultLogger);

            processor.StartUp<T, R>();
            return processor;
        }

        public IStepFlow StartWith<T, R>(Stage stage, Type clazz, string actorName, List<object> @params) where T : IState where R : IState
        {
            IStepFlow processor = stage.ActorFor<IStepFlow>(typeof(IStepFlow), Definition.Has(
                    clazz,
                    @params,
                    "queueMailbox", actorName),
                    stage.World.AddressFactory.WithHighId(),
                    stage.World.DefaultLogger);

            processor.StartUp<T, R>();
            return processor;
        }
    }
}
