// Copyright © 2012-2023 VLINGO LABS. All rights reserved.
//
// This Source Code Form is subject to the terms of the
// Mozilla Public License, v. 2.0. If a copy of the MPL
// was not distributed with this file, You can obtain
// one at https://mozilla.org/MPL/2.0/.

using Vlingo.Xoom.Actors;
using Vlingo.Xoom.Common;
using Vlingo.Xoom.Lattice.Model.Stateful;

namespace Vlingo.Xoom.Turbo.Tests.Annotation.Model;

public class DummyEntity : StatefulEntity<DummyState>, IDummy
{
    private DummyState _state;

    public DummyEntity()
    {
    }

    public DummyEntity(string id) : base(id)
    {
    }

    protected override void State(DummyState state) => _state = state;

    public ICompletes<DummyState> DefineWith(Stage stage, string name) => Dummy.DefineWith(stage, name);

    public ICompletes<DummyState> DefineWith(string name)
    {
        if (_state == null)
        {
            return Apply(new DummyState(Id, name), () => _state);
        }

        return Completes().With(_state);
    }

    public ICompletes<DummyState> WithName(string name) => null;
}