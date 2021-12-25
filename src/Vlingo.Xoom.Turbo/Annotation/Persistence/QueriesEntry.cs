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
	public class QueriesEntry : Attribute
	{
		Type _protocol;
		Type _actor;

		public Type Actor
		{
			get => _actor;
			set => _actor = value;
		}

		public Type Protocol
		{
			get => _protocol;
			set => _protocol = value;
		}
	}
}