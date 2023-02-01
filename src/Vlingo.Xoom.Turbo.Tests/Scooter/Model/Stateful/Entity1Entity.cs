// Copyright © 2012-2023 VLINGO LABS. All rights reserved.
//
// This Source Code Form is subject to the terms of the
// Mozilla Public License, v. 2.0. If a copy of the MPL
// was not distributed with this file, You can obtain
// one at https://mozilla.org/MPL/2.0/.

using Vlingo.Xoom.Lattice.Model;
using Vlingo.Xoom.Turbo.Scooter.Model.Stateful;

namespace Vlingo.Xoom.Turbo.Tests.Scooter.Model.Stateful;

public class Entity1Entity : StatefulEntity<Entity1State, DomainEvent>, IEntity1
{
	private Entity1State _state;

	public Entity1Entity(Entity1State state) => _state = state;

	public override string Id() => _state.Id;

	protected override void State(Entity1State state) => _state = state;

	public void ChangeName(string name) => Apply(_state.WithName(name));

	public void IncreaseAge() => Apply(_state.WithAge(_state.Age + 1));

	public Entity1State State() => _state;
}