// Copyright © 2012-2021 VLINGO LABS. All rights reserved.
//
// This Source Code Form is subject to the terms of the
// Mozilla Public License, v. 2.0. If a copy of the MPL
// was not distributed with this file, You can obtain
// one at https://mozilla.org/MPL/2.0/.

using System;

namespace Vlingo.Xoom.Turbo.Annotation.AutoDispatch
{
	[AttributeUsage(AttributeTargets.Interface | AttributeTargets.Class | AttributeTargets.Struct)]
	public class Model : Attribute
	{
		private Type _protocols;
		private Type _actor;
		private Type _data;

		public Model()
		{
		}
		
		public Model(Type protocols, Type actor, Type data)
		{
			_protocols = protocols;
			_actor = actor;
			_data = data;
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

		public Type Data
		{
			get => _data;
			set => _data = value;
		}
	}
}