// Copyright © 2012-2021 VLINGO LABS. All rights reserved.
//
// This Source Code Form is subject to the terms of the
// Mozilla Public License, v. 2.0. If a copy of the MPL
// was not distributed with this file, You can obtain
// one at https://mozilla.org/MPL/2.0/.

using Vlingo.Xoom.Actors;
using Vlingo.Xoom.Common;
using Vlingo.Xoom.Turbo.Annotation.AutoDispatch;
using Vlingo.Xoom.Turbo.Tests.Annotation.Model;
using Vlingo.Xoom.Turbo.Tests.Annotation.Persistence;

namespace Vlingo.Xoom.Turbo.Tests.Annotation.Initializer.Resources
{
	public class DummyHandlers
	{
		public static int DefineWith = 0;
		public static int AdaptState = 3;
		
		// public static HandlerEntry<Three<ICompletes<DummyState>, Stage, DummyData>> DefineDummyHandler = 
		// 	HandlerEntry<Three<ICompletes<DummyState>, Stage, DummyData>>.Of(DefineWith, (stage, dummyData) => Dummy.DefineWith(stage, dummyData.Name));

	}
}