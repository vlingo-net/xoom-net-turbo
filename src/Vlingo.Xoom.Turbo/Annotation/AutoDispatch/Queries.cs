// Copyright © 2012-2021 VLINGO LABS. All rights reserved.
//
// This Source Code Form is subject to the terms of the
// Mozilla Public License, v. 2.0. If a copy of the MPL
// was not distributed with this file, You can obtain
// one at https://mozilla.org/MPL/2.0/.

using System;

namespace Vlingo.Xoom.Turbo.Annotation.AutoDispatch
{	
	[AttributeUsage(AttributeTargets.Interface | AttributeTargets.Class, Inherited = false)]
	public class Queries : Attribute
	{
		private Type _protocols;
		private Type _actor;

		public Queries()
		{
			
		}

		public Queries(Type protocols, Type actor)
		{
			_protocols = protocols;
			_actor = actor;
		}

		public Type Protocols
		{
			get => _protocols;
			set => _protocols = value;
		}

		public Type Actor
		{
			get => _actor;
			set => _actor = value;
		}
	}
}