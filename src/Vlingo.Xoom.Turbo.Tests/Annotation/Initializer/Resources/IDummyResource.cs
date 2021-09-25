// Copyright © 2012-2021 VLINGO LABS. All rights reserved.
//
// This Source Code Form is subject to the terms of the
// Mozilla Public License, v. 2.0. If a copy of the MPL
// was not distributed with this file, You can obtain
// one at https://mozilla.org/MPL/2.0/.

using Vlingo.Xoom.Turbo.Annotation.CodeGen.Storage;
using Vlingo.Xoom.Turbo.Tests.Annotation.Model;
using Vlingo.Xoom.Turbo.Tests.Annotation.Persistence;

namespace Vlingo.Xoom.Turbo.Tests.Annotation.Initializer.Resources
{
	[Turbo.Annotation.AutoDispatch.AutoDispatch(Path = "/dummies", Handlers = typeof(DummyHandler))]
	[Queries(Protocols = typeof(IDummyQueries), Actor = typeof(DummyQueriesActor))]
	[Turbo.Annotation.AutoDispatch.Model(Protocols = typeof(IDummy), Actor = typeof(DummyEntity), Data = typeof(DummyData))]
	public interface IDummyResource
	{
		
	}
}