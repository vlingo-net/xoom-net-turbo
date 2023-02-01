// Copyright © 2012-2023 VLINGO LABS. All rights reserved.
//
// This Source Code Form is subject to the terms of the
// Mozilla Public License, v. 2.0. If a copy of the MPL
// was not distributed with this file, You can obtain
// one at https://mozilla.org/MPL/2.0/.

namespace Vlingo.Xoom.Turbo.Tests.Scooter.Persistence;

public class Entity2
{
	public string Id { get; }
	public string Value { get; }

	public Entity2(string id, string value)
	{
		Id = id;
		Value = value;
	}
		
	public override bool Equals(object obj) => Equals((Entity2)obj);

	protected bool Equals(Entity2 other)
	{
		if (other == null || other.GetType() != GetType())
		{
			return false;
		}
		return Id == other.Id;
	}

	public override int GetHashCode()
	{
		throw new System.NotImplementedException();
	}
}