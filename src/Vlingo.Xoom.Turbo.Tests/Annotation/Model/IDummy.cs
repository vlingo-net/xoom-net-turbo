// Copyright © 2012-2021 VLINGO LABS. All rights reserved.
//
// This Source Code Form is subject to the terms of the
// Mozilla Public License, v. 2.0. If a copy of the MPL
// was not distributed with this file, You can obtain
// one at https://mozilla.org/MPL/2.0/.

using Vlingo.Xoom.Actors;
using Vlingo.Xoom.Common;

namespace Vlingo.Xoom.Turbo.Tests.Annotation.Model
{
    public interface IDummy
    {
        ICompletes<DummyState> DefineWith(string name);
    }

    public abstract class Dummy : IDummy
    {
        public static ICompletes<DummyState> DefineWith(Stage stage, string name)
        {
            var address = stage.World.AddressFactory.UniquePrefixedWith("g-");

            var dummy = stage.ActorFor<IDummy>(Definition.Has<DummyEntity>(Definition.Parameters(address.IdString)));
            return dummy.DefineWith(name);
        }

        public abstract ICompletes<DummyState> DefineWith(string name);
    }
}