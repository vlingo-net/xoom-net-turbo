// Copyright © 2012-2021 VLINGO LABS. All rights reserved.
//
// This Source Code Form is subject to the terms of the
// Mozilla Public License, v. 2.0. If a copy of the MPL
// was not distributed with this file, You can obtain
// one at https://mozilla.org/MPL/2.0/.

using System;

namespace Vlingo.Xoom.Turbo.Annotation.Persistence
{
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
	public class Projection : Attribute
	{
		Type _actor;
		Type[] _becauseOf;

		public Type Actor
		{
			get => _actor;
			set => _actor = value;
		}

		public Type[] BecauseOf
		{
			get => _becauseOf;
			set => _becauseOf = value;
		}
	}
}