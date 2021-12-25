// Copyright © 2012-2021 VLINGO LABS. All rights reserved.
//
// This Source Code Form is subject to the terms of the
// Mozilla Public License, v. 2.0. If a copy of the MPL
// was not distributed with this file, You can obtain
// one at https://mozilla.org/MPL/2.0/.

using Vlingo.Xoom.Common;
using Vlingo.Xoom.Lattice.Model.Stateful;
using Vlingo.Xoom.Turbo.Tests.Annotation.Model;

namespace Vlingo.Xoom.Turbo.Tests.Annotation.Model
{
	public class DummyEntity : StatefulEntity<DummyState>, IDummy
	{
		private DummyState _state;

		protected override void State(DummyState state)
		{
			_state = state;
		}

		public ICompletes<DummyState> DefineWith(string name)
		{
			if (_state == null)
			{
				return Apply(new DummyState(Id, name), () => _state);
			}
			else
			{
				return Completes().With(_state);
			}
		}
	}
}