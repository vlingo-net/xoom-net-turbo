// Copyright © 2012-2021 VLINGO LABS. All rights reserved.
//
// This Source Code Form is subject to the terms of the
// Mozilla Public License, v. 2.0. If a copy of the MPL
// was not distributed with this file, You can obtain
// one at https://mozilla.org/MPL/2.0/.

using System.Collections.Generic;
using Vlingo.Xoom.Symbio;
using Vlingo.Xoom.Turbo.Scooter.Model.Sourced;

namespace Vlingo.Xoom.Turbo.Tests.Scooter.Persistence
{
	public class TestEntity : EventSourcedEntity
	{
		public bool Test1 { get; set; }
		public bool Test2 { get; set; }
		private readonly string _id;

		public TestEntity(string id)
		{
			_id = id;
		}

		public override string Id() => StreamName();

		protected override string StreamName() => _id;

		public void DoTest1()
		{
			Apply(new Test1Happened(_id));
		}

		static TestEntity()
		{
			RegisterConsumer<TestEntity, Test1Happened>(typeof(TestEntity), typeof(Test1Happened), WhenDoTest1);
		}

		public TestEntity(IEnumerable<ISource> sources, int streamStreamVersion) : base()
		{
		}

		static void WhenDoTest1(TestEntity entity, Test1Happened @event)
		{
		}

		public override object? ObjectContainer => this;
	}
}