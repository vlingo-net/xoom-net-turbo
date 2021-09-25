// Copyright © 2012-2021 VLINGO LABS. All rights reserved.
//
// This Source Code Form is subject to the terms of the
// Mozilla Public License, v. 2.0. If a copy of the MPL
// was not distributed with this file, You can obtain
// one at https://mozilla.org/MPL/2.0/.

using Vlingo.Xoom.Common;
using Vlingo.Xoom.Lattice.Model.Stateful;
using Vlingo.Xoom.Turbo.Tests.Annotation.Model;

namespace Vlingo.Xoom.Turbo.Tests.Annotation.Persistence
{
	public class DummyEntity : StatefulEntity<DummyState>, IDummy
	{
		protected override void State(DummyState state)
		{
			throw new System.NotImplementedException();
		}

		public ICompletes<DummyState> DefineWith(string name)
		{
			throw new System.NotImplementedException();
		}
	}
}