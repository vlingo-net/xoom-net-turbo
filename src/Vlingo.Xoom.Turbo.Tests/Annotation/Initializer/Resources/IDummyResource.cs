// Copyright © 2012-2021 VLINGO LABS. All rights reserved.
//
// This Source Code Form is subject to the terms of the
// Mozilla Public License, v. 2.0. If a copy of the MPL
// was not distributed with this file, You can obtain
// one at https://mozilla.org/MPL/2.0/.

using Vlingo.Xoom.Common;
using Vlingo.Xoom.Http;
using Vlingo.Xoom.Turbo.Annotation.AutoDispatch;
using Vlingo.Xoom.Turbo.Tests.Annotation.Model;
using Vlingo.Xoom.Turbo.Tests.Annotation.Persistence;

namespace Vlingo.Xoom.Turbo.Tests.Annotation.Initializer.Resources
{
	[Turbo.Annotation.AutoDispatch.AutoDispatch(Path = "/dummies", Handlers = typeof(DummyHandlers))]
	[Queries(Protocol = typeof(IDummyQueries), Actor = typeof(DummyQueriesActor))]
	[Turbo.Annotation.AutoDispatch.Model(Protocols = typeof(IDummy), Actor = typeof(DummyEntity), Data = typeof(DummyData))]
	public interface IDummyResource
	{

		[Route(Method = "POST", Handler = 1)]
		[ResponseAdapter(Handler = 3)]
		ICompletes<Response> DefineDummy([Turbo.Annotation.AutoDispatch.Body] DummyData dummyData);
		
	}
}