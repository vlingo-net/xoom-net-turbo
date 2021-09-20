// Copyright © 2012-2021 VLINGO LABS. All rights reserved.
//
// This Source Code Form is subject to the terms of the
// Mozilla Public License, v. 2.0. If a copy of the MPL
// was not distributed with this file, You can obtain
// one at https://mozilla.org/MPL/2.0/.

using Vlingo.Xoom.Symbio.Store.State;
using Vlingo.Xoom.Turbo.Scooter.Model.Persistence;

namespace Vlingo.Xoom.Turbo.Tests.Scooter.Persistence
{
	public class EntityRepository : StatefulRepository
	{
		private readonly IStateStore _store;

		public EntityRepository(IStateStore store)
		{
			_store = store;
		}

		public void Save(Entity1 entity)
		{
			var interest = CreateWriteInterest();
			_store.Write(entity.Id, entity, 1, interest);
			Await(interest);
		}

		public Entity1 Entity1Of(string id)
		{
			var interst = CreateReadInterest();
			_store.Read<Entity1>(id, interst);
			return Await<Entity1>(interst);
		}
	}
}