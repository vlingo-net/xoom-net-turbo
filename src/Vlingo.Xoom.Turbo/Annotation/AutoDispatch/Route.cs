﻿// Copyright © 2012-2021 VLINGO LABS. All rights reserved.
//
// This Source Code Form is subject to the terms of the
// Mozilla Public License, v. 2.0. If a copy of the MPL
// was not distributed with this file, You can obtain
// one at https://mozilla.org/MPL/2.0/.

using System;

namespace Vlingo.Xoom.Turbo.Annotation.AutoDispatch
{
	[AttributeUsage(AttributeTargets.Method | AttributeTargets.Struct)]
	public class Route : Attribute
	{
		private string _method;
		private int _handler;

		public Route()
		{
		}
		
		public Route(string method, int handler)
		{
			_method = method;
			_handler = handler;
		}

		public string Method
		{
			get => _method;
			set => _method = value;
		}

		public int Handler
		{
			get => _handler;
			set => _handler = value;
		}
	}
}