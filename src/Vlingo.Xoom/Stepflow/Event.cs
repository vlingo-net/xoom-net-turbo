// Copyright © 2012-2020 VLINGO LABS. All rights reserved.
//
// This Source Code Form is subject to the terms of the
// Mozilla Public License, v. 2.0. If a copy of the MPL
// was not distributed with this file, You can obtain
// one at https://mozilla.org/MPL/2.0/.

using Vlingo.Actors.Plugin.Logging.Console;
using Vlingo.Symbio;

namespace Vlingo.Xoom.Stepflow
{
    public abstract class Event : Source<Event>, ITransition
    {
        private string sourceName = string.Empty;
        private string targetName = string.Empty;

        public Event()
        {
        }

        public Event(string source, string target)
        {
            this.sourceName = source;
            this.targetName = target;
        }

        public string GetSourceName()
        {
            return sourceName;
        }

        public void SetSourceName(string sourceName)
        {
            this.sourceName = sourceName;
        }

        public string GetTargetName()
        {
            return targetName;
        }

        public void SetTargetName(string targetName)
        {
            this.targetName = targetName;
        }

        public string GetEventType()
        {
            return string.Concat(GetSourceName(), "::", GetTargetName());
        }

        public void LogResult<TState, TRawState>(TState s, TRawState t) where TState : State<object> where TRawState : State<object>
        {
            ConsoleLogger.BasicInstance().Info(string.Concat(s.GetVersion(), ": [", s.GetName(), "] to [", t.GetName(), "]"));
        }
    }
}
