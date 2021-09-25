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
	public class AutoDispatch : Attribute
	{
		private string _path;
		private Type _handler;

		public AutoDispatch()
		{
		}

		public AutoDispatch(string path, Type handler)
		{
			_path = path;
			_handler = handler;
		}

		public string Path
		{
			get => _path;
			set => _path = value;
		}

		public Type Handlers
		{
			get => _handler;
			set => _handler = value;
		}
	}
}