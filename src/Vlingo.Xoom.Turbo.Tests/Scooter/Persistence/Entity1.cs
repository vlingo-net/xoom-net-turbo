// Copyright © 2012-2021 VLINGO LABS. All rights reserved.
//
// This Source Code Form is subject to the terms of the
// Mozilla Public License, v. 2.0. If a copy of the MPL
// was not distributed with this file, You can obtain
// one at https://mozilla.org/MPL/2.0/.

namespace Vlingo.Xoom.Turbo.Tests.Scooter.Persistence
{
	public class Entity1
	{
		public string Id { get; }
		public int Value { get; }

		public Entity1(string id, int value)
		{
			Id = id;
			Value = value;
		}

		public override bool Equals(object obj)
		{
			if (obj == null || obj.GetType() != GetType())
			{
				return false;
			}
			return Id == ((Entity1)obj).Id;
		}
	}
}