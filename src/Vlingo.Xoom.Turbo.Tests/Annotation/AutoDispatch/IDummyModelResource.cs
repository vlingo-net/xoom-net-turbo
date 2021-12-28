// Copyright © 2012-2021 VLINGO LABS. All rights reserved.
//
// This Source Code Form is subject to the terms of the
// Mozilla Public License, v. 2.0. If a copy of the MPL
// was not distributed with this file, You can obtain
// one at https://mozilla.org/MPL/2.0/.

using Vlingo.Xoom.Common;
using Vlingo.Xoom.Http;
using Vlingo.Xoom.Turbo.Annotation.AutoDispatch;
using Vlingo.Xoom.Turbo.Tests.Annotation.Initializer.Resources;
using Vlingo.Xoom.Turbo.Tests.Annotation.Model;
using Vlingo.Xoom.Turbo.Tests.Annotation.Persistence;

namespace Vlingo.Xoom.Turbo.Tests.Annotation.AutoDispatch
{
    [Model(Protocol = typeof(IDummy), Actor = typeof(DummyEntity), Data = typeof(DummyData))]
    [AutoDispatch(Path = "/dummies/", Handlers = typeof(DummyHandlers))]
    public interface IDummyModelResource
    {
        [Route(Method = Method.Put, Path = "/{dummyId}/name/", Handler = DummyHandlers.ChangeName)]
        [ResponseAdapter(Handler = DummyHandlers.AdaptState)]
        ICompletes<Response> ChangeDummyName([Id] string dummyId, [Body] DummyData dummyData);
    }   
}