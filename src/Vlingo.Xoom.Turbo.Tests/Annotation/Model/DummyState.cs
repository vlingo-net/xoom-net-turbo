// Copyright © 2012-2022 VLINGO LABS. All rights reserved.
//
// This Source Code Form is subject to the terms of the
// Mozilla Public License, v. 2.0. If a copy of the MPL
// was not distributed with this file, You can obtain
// one at https://mozilla.org/MPL/2.0/.

using Vlingo.Xoom.Symbio.Store.Object;

namespace Vlingo.Xoom.Turbo.Tests.Annotation.Model
{
    public class DummyState : StateObject
    {
        public string Name { get; }
        public string Id { get; }

        public DummyState(string id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}