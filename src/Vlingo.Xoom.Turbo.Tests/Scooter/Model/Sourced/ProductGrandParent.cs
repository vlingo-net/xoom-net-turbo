// Copyright © 2012-2021 VLINGO LABS. All rights reserved.
//
// This Source Code Form is subject to the terms of the
// Mozilla Public License, v. 2.0. If a copy of the MPL
// was not distributed with this file, You can obtain
// one at https://mozilla.org/MPL/2.0/.

using System.Collections.Generic;
using Vlingo.Xoom.Lattice.Model;
using Vlingo.Xoom.Symbio;
using Vlingo.Xoom.Turbo.Scooter.Model.Sourced;

namespace Vlingo.Xoom.Turbo.Tests.Scooter.Model.Sourced
{
	public abstract class ProductGrandParent : EventSourcedEntity
	{
		public string Type { get; set; }

		protected ProductGrandParent(string type)
		{
			Type = type;

			Apply(new ProductGrandParentTyped(type));
		}

		public ProductGrandParent(List<Source<DomainEvent>> eventStream, int streamVersion) : base(eventStream, streamVersion)
		{
		}

		static ProductGrandParent()
		{
			RegisterConsumer<ProductGrandParent, ProductGrandParentTyped>(delegate(Source<DomainEvent> source)
			{
				WhenProductGrandParentTyped(source as ProductGrandParentTyped);
			});
		}

		static void WhenProductGrandParentTyped(ProductGrandParentTyped @event)
		{
		}
	}
}