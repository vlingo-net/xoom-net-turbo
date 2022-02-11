// Copyright © 2012-2022 VLINGO LABS. All rights reserved.
//
// This Source Code Form is subject to the terms of the
// Mozilla Public License, v. 2.0. If a copy of the MPL
// was not distributed with this file, You can obtain
// one at https://mozilla.org/MPL/2.0/.

using System.Collections.Generic;
using Vlingo.Xoom.Lattice.Model;
using Vlingo.Xoom.Symbio;

namespace Vlingo.Xoom.Turbo.Tests.Scooter.Model.Sourced;

public abstract class ProductParent : ProductGrandParent
{
	public string Category { get; set; }

	protected ProductParent(string type, string category) : base(type)
	{
		Category = category;

		Apply(new ProductParentCategorized(category));
	}

	public ProductParent(List<Source<DomainEvent>> eventStream, int streamVersion) : base(eventStream, streamVersion)
	{
	}
		
	static ProductParent()
	{
		RegisterConsumer<ProductParent, ProductParentCategorized>(delegate(Source<DomainEvent> source)
		{
			WhenProductParentCategorized(source as ProductParentCategorized);
		});
	}

	static void WhenProductParentCategorized(ProductParentCategorized @event)
	{
	}
}