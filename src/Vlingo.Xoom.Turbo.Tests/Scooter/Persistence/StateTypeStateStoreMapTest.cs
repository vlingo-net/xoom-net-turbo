// Copyright © 2012-2022 VLINGO LABS. All rights reserved.
//
// This Source Code Form is subject to the terms of the
// Mozilla Public License, v. 2.0. If a copy of the MPL
// was not distributed with this file, You can obtain
// one at https://mozilla.org/MPL/2.0/.

using Vlingo.Xoom.Symbio.Store.State;
using Vlingo.Xoom.Turbo.Tests.Scooter.Model.Stateful;
using Xunit;

namespace Vlingo.Xoom.Turbo.Tests.Scooter.Persistence;

public class StateTypeStateStoreMapTest
{
	[Fact]
	public void TestExistingMappings()
	{
		StateTypeStateStoreMap.StateTypeToStoreName(typeof(IEntity1).FullName, typeof(IEntity1));

		Assert.Equal(typeof(IEntity1).FullName, StateTypeStateStoreMap.StoreNameFrom(typeof(IEntity1)));
		Assert.Equal(typeof(IEntity1).FullName, StateTypeStateStoreMap.StoreNameFrom(typeof(IEntity1).FullName));
	}
}