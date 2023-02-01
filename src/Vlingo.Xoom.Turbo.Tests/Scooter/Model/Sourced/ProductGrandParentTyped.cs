// Copyright © 2012-2023 VLINGO LABS. All rights reserved.
//
// This Source Code Form is subject to the terms of the
// Mozilla Public License, v. 2.0. If a copy of the MPL
// was not distributed with this file, You can obtain
// one at https://mozilla.org/MPL/2.0/.

using System;
using Vlingo.Xoom.Lattice.Model;

namespace Vlingo.Xoom.Turbo.Tests.Scooter.Model.Sourced;

public class ProductGrandParentTyped : DomainEvent
{
	public string Type { get; }
	public DateTime OccurredOn { get; }
	public int Version { get; }

	public ProductGrandParentTyped(string type)
	{
		Type = type;
		OccurredOn = DateTime.Now;
		Version = 1;
	}
}