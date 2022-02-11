// Copyright © 2012-2022 VLINGO LABS. All rights reserved.
//
// This Source Code Form is subject to the terms of the
// Mozilla Public License, v. 2.0. If a copy of the MPL
// was not distributed with this file, You can obtain
// one at https://mozilla.org/MPL/2.0/.

namespace Vlingo.Xoom.Turbo.Tests.Scooter.Model.Stateful;

public class Entity1State
{
	public string Id { get; }
	public string Name { get; }
	public int Age { get; }

	public Entity1State(string id, string name = "", int age = 0)
	{
		Id = id;
		Name = name;
		Age = age;
	}

	public Entity1State WithName(string name) => new Entity1State(Id, name, Age);

	public Entity1State WithAge(int age) => new Entity1State(Id, Name, age);
}