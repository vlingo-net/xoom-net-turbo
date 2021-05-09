// Copyright © 2012-2021 VLINGO LABS. All rights reserved.
//
// This Source Code Form is subject to the terms of the
// Mozilla Public License, v. 2.0. If a copy of the MPL
// was not distributed with this file, You can obtain
// one at https://mozilla.org/MPL/2.0/.

using System.Collections.Generic;
using Vlingo.Xoom.Lattice.Model;
using Vlingo.Xoom.Symbio;

namespace Vlingo.Xoom.Turbo.Scooter.Model.Sourced
{
    public abstract class EventSourcedEntity : SourcedEntity<DomainEvent>
    {
        public EventSourcedEntity() : base()
        {
        }

        public EventSourcedEntity(List<Source<DomainEvent>> stream, int currentVersion) : base(stream, currentVersion)
        {
        }
    }
}
