// Copyright © 2012-2021 VLINGO LABS. All rights reserved.
//
// This Source Code Form is subject to the terms of the
// Mozilla Public License, v. 2.0. If a copy of the MPL
// was not distributed with this file, You can obtain
// one at https://mozilla.org/MPL/2.0/.

using System;
using System.Collections.Generic;
using System.Linq;
using Vlingo.Xoom.Stepflow;

namespace Vlingo.Xoom.Management.Endpoints
{
    public class StepFlowEndpoint
    {
        private IDictionary<string, IStepFlow<State<object>, State<object>, Type>> _stepFlow = new Dictionary<string, IStepFlow<State<object>, State<object>, Type>>();

        public IReadOnlyDictionary<string, object> Map(string flowName)
        {
            var nodes = new HashSet<string>();
            var portCount = new Dictionary<string, int>();
            var links = new HashSet<IDictionary<string, object>>();
            var results = new Dictionary<string, object>();

            var kernel = _stepFlow[flowName].GetKernel().Await();
            var states = kernel.GetStates().Await();

            states.ToList().ForEach(state =>
            {
                nodes.Add(state.GetName().Split(new string[] { "::" }, StringSplitOptions.None)[0]);
            });

            states.SelectMany(state => state.GetTransitionHandlers<Type>()).ToList().ForEach(handler => DefineGraph(portCount, links, handler));

            results.Add("nodes", nodes);
            results.Add("edges", links);
            results.Add("flow", _stepFlow[flowName].GetName().Await());

            return results;
        }

        private void DefineGraph(IReadOnlyDictionary<string, int> portCount, HashSet<IDictionary<string, object>> links, TransitionHandler<State<object>, State<object>, Type> handler)
        {
            var link = new Dictionary<string, object>();

            var address = handler.GetAddress().Split(new string[] { "::" }, StringSplitOptions.None);

            link.Add("source", handler.GetStateTransition().GetSourceName());
            link.Add("target", handler.GetStateTransition().GetTargetName());

            link.Add("label", address.Length == 3 ? address[2] : "TO");

            var sourcePortKey = handler.GetStateTransition().GetSourceName();
            var sourcePort = address.Length != 3 ? (portCount[sourcePortKey] != 0 ? portCount[sourcePortKey] + 1 : 0) : 1;

            var targetPortKey = string.Concat("TO", "::", handler.GetStateTransition().GetTargetName());
            int targetPort = address.Length != 3 ? (portCount[targetPortKey] != 0 ? portCount[targetPortKey] + 1 : 1) : 0;

            link.Add("port", sourcePort + targetPort);

            links.Add(link);
        }
    }
}
