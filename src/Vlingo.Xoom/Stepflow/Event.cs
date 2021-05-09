// Copyright © 2012-2021 VLINGO LABS. All rights reserved.
//
// This Source Code Form is subject to the terms of the
// Mozilla Public License, v. 2.0. If a copy of the MPL
// was not distributed with this file, You can obtain
// one at https://mozilla.org/MPL/2.0/.

using Vlingo.Xoom.Symbio;
using Vlingo.Xoom.Actors.Plugin.Logging.Console;

namespace Vlingo.Xoom.Stepflow
{
    public abstract class Event : Source<Event>, ITransition
    {
        private string _sourceName = string.Empty;
        private string _targetName = string.Empty;

        public Event()
        {
        }

        public Event(string source, string target)
        {
            _sourceName = source;
            _targetName = target;
        }

        public string GetSourceName() => _sourceName;

        public void SetSourceName(string sourceName) => _sourceName = sourceName;

        public string GetTargetName() => _targetName;

        public void SetTargetName(string targetName) => _targetName = targetName;

        public string GetEventType() => string.Concat(GetSourceName(), "::", GetTargetName());

        public void LogResult<TState, TRawState>(TState s, TRawState t) where TState : State<object> where TRawState : State<object> => 
            ConsoleLogger.BasicInstance().Info(string.Concat(s.GetVersion(), ": [", s.GetName(), "] to [", t.GetName(), "]"));
    }
}
