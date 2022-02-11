// Copyright © 2012-2022 VLINGO LABS. All rights reserved.
//
// This Source Code Form is subject to the terms of the
// Mozilla Public License, v. 2.0. If a copy of the MPL
// was not distributed with this file, You can obtain
// one at https://mozilla.org/MPL/2.0/.

namespace Vlingo.Xoom.Turbo.Scooter.Model;

public abstract class Repository<TS, TC> where TS : class where TC : class
{
    public abstract void FromId(string id);

    public abstract void Save(Entity<TS, TC> entity);
}