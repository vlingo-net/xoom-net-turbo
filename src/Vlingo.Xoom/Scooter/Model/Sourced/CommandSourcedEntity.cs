// Copyright © 2012-2020 VLINGO LABS. All rights reserved.
//
// This Source Code Form is subject to the terms of the
// Mozilla Public License, v. 2.0. If a copy of the MPL
// was not distributed with this file, You can obtain
// one at https://mozilla.org/MPL/2.0/.

using System.Collections.Generic;
using Vlingo.Lattice.Model;
using Vlingo.Symbio;

namespace Vlingo.Xoom.Scooter.Model.Sourced
{
    public abstract class CommandSourcedEntity : SourcedEntity<Command>
    {
        public CommandSourcedEntity() : base()
        {
        }

        public CommandSourcedEntity(List<Source<Command>> stream, int currentVersion) : base(stream, currentVersion)
        {
        }
    }
}
