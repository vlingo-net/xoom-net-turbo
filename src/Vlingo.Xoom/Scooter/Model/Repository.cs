// Copyright © 2012-2020 VLINGO LABS. All rights reserved.
//
// This Source Code Form is subject to the terms of the
// Mozilla Public License, v. 2.0. If a copy of the MPL
// was not distributed with this file, You can obtain
// one at https://mozilla.org/MPL/2.0/.

namespace Vlingo.Xoom.Scooter.Model
{
    public abstract class Repository<S, C> where S : class where C : class
    {
        public abstract void FromId(string id);

        public abstract void Save(Entity<S, C> entity);
    }
}
