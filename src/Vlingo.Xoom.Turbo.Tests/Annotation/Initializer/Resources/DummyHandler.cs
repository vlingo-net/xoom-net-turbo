// Copyright © 2012-2022 VLINGO LABS. All rights reserved.
//
// This Source Code Form is subject to the terms of the
// Mozilla Public License, v. 2.0. If a copy of the MPL
// was not distributed with this file, You can obtain
// one at https://mozilla.org/MPL/2.0/.

using Vlingo.Xoom.Actors;
using Vlingo.Xoom.Common;
using Vlingo.Xoom.Turbo.Annotation.AutoDispatch;
using Vlingo.Xoom.Turbo.Tests.Annotation.Model;
using Vlingo.Xoom.Turbo.Tests.Annotation.Persistence;

namespace Vlingo.Xoom.Turbo.Tests.Annotation.Initializer.Resources;

public class DummyHandlers
{
    public const int DefineWith = 0;
    public const int ChangeName = 1;
    public const int QueryAll = 2;
    public const int AdaptState = 3;

    public static HandlerEntry DefineWithHandler =
        HandlerEntry.Of<Stage, DummyData, ICompletes<DummyState>>(
            DefineWith,
            (stage,
                dummyData) => Dummy.DefineWith(stage,
                dummyData.Name));
        
    public static HandlerEntry ChangeNameHandler =
        HandlerEntry.Of<IDummy, DummyData, ICompletes<DummyState>>(
            ChangeName,
            (dummy,
                dummyData) => dummy.WithName(dummyData.Name));

    public static HandlerEntry QueryAllHandler = 
        HandlerEntry.Of<IDummyQueries, ICompletes<DummyData>>(QueryAll, queries => queries.AllDummies());

    public static HandlerEntry AdaptStateHandler = HandlerEntry.Of<DummyState, DummyData>(AdaptState, DummyData.From);
}