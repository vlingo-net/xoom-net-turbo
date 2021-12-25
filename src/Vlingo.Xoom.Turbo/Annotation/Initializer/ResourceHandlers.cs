// Copyright © 2012-2021 VLINGO LABS. All rights reserved.
//
// This Source Code Form is subject to the terms of the
// Mozilla Public License, v. 2.0. If a copy of the MPL
// was not distributed with this file, You can obtain
// one at https://mozilla.org/MPL/2.0/.

using System;

namespace Vlingo.Xoom.Turbo.Annotation.Initializer
{
	[AttributeUsage(AttributeTargets.Class, Inherited = false)]
	public class ResourceHandlers : Attribute
	{
		private string[] _package;
		private Type[] _value;

		public string[] Packages
		{
			get => _package;
			set => _package = value;
		}

		public Type[] Value
		{
			get => _value;
			set => _value = value;
		}
	}
}