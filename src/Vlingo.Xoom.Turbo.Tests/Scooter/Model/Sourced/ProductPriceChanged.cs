// Copyright © 2012-2022 VLINGO LABS. All rights reserved.
//
// This Source Code Form is subject to the terms of the
// Mozilla Public License, v. 2.0. If a copy of the MPL
// was not distributed with this file, You can obtain
// one at https://mozilla.org/MPL/2.0/.

using System;
using Vlingo.Xoom.Lattice.Model;

namespace Vlingo.Xoom.Turbo.Tests.Scooter.Model.Sourced
{
	public class ProductPriceChanged : DomainEvent
	{
		public long Price { get; }
		public DateTime OccurredOn { get; }
		public int Version { get; }
		public ProductPriceChanged(int price)
		{
			Price = price;
			OccurredOn = DateTime.Now;
			Version = 1;
		}
	}
}