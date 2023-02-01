// Copyright © 2012-2023 VLINGO LABS. All rights reserved.
//
// This Source Code Form is subject to the terms of the
// Mozilla Public License, v. 2.0. If a copy of the MPL
// was not distributed with this file, You can obtain
// one at https://mozilla.org/MPL/2.0/.

using Vlingo.Xoom.Common;
using Vlingo.Xoom.Lattice.Query;
using Vlingo.Xoom.Symbio.Store.State;
using Vlingo.Xoom.Turbo.Tests.Annotation.Model;

namespace Vlingo.Xoom.Turbo.Tests.Annotation.Persistence;

public class DummyQueriesActor : StateStoreQueryActor<DummyState>, IDummyQueries
{
    public DummyQueriesActor(IStateStore stateStore) : base(stateStore)
    {
    }

    public ICompletes<DummyData> AllDummies() => Completes().With<DummyData>(null);
}