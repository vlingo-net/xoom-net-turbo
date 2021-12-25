// Copyright © 2012-2021 VLINGO LABS. All rights reserved.
//
// This Source Code Form is subject to the terms of the
// Mozilla Public License, v. 2.0. If a copy of the MPL
// was not distributed with this file, You can obtain
// one at https://mozilla.org/MPL/2.0/.

using Vlingo.Xoom.Lattice.Model;

namespace Vlingo.Xoom.Turbo.Tests.Scooter.Persistence
{
	public class Test1Happened : DomainEvent
	{
		private readonly string _id;

		public Test1Happened(string id)
		{
			_id = id;
		}
	}
}