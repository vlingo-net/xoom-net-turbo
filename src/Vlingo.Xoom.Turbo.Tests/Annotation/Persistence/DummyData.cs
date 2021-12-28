// Copyright © 2012-2021 VLINGO LABS. All rights reserved.
//
// This Source Code Form is subject to the terms of the
// Mozilla Public License, v. 2.0. If a copy of the MPL
// was not distributed with this file, You can obtain
// one at https://mozilla.org/MPL/2.0/.

using Vlingo.Xoom.Turbo.Tests.Annotation.Model;

namespace Vlingo.Xoom.Turbo.Tests.Annotation.Persistence
{
    public class DummyData
    {
        public static DummyData From(DummyState state) => new DummyData(state.Name);

        public DummyData(string name) => Name = name;

        public string Name { get; }
    }
}