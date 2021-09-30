// Copyright © 2012-2021 VLINGO LABS. All rights reserved.
//
// This Source Code Form is subject to the terms of the
// Mozilla Public License, v. 2.0. If a copy of the MPL
// was not distributed with this file, You can obtain
// one at https://mozilla.org/MPL/2.0/.

using System;
using System.Collections.Generic;
using Vlingo.Xoom.Lattice.Model;
using Vlingo.Xoom.Symbio;
using Vlingo.Xoom.Turbo.Scooter.Model.Sourced;

namespace Vlingo.Xoom.Turbo.Tests.Scooter.Persistence
{
	public class TestEntity : EventSourcedEntity
	{
		public bool Test1
		{
			get => _test1;
			set => _test1 = value;
		}

		public bool Test2
		{
			get => _test2;
			set => _test2 = value;
		}

		private static string _id;
		private static bool _test1;
		private static bool _test2;

		public TestEntity(string id)
		{
			_id = id;
		}

		public TestEntity(IEnumerable<ISource> sources, int streamStreamVersion) : base()
		{
		}

		public override string Id() => StreamName();

		protected override string StreamName() => _id;

		public void DoTest1()
		{
			Apply(new Test1Happened(_id));
		}

		static TestEntity()
		{
			RegisterConsumer<TestEntity, Test1Happened>(delegate(Source<DomainEvent> source)
			{
				WhenDoTest1(source as Test1Happened);
			});
			RegisterConsumer<TestEntity, Test2Happened>(delegate(Source<DomainEvent> source)
			{
				WhenDoTest2(source as Test2Happened);
			});
		}

		static void WhenDoTest1(Test1Happened @event)
		{
			_id = @event.Id;
			_test1 = true;
		}
		static void WhenDoTest2(Test2Happened @event)
		{
			_id = @event.Id;
			_test2 = true;
		}

		public void DoTest2()
		{
			Apply(new Test2Happened(_id));
		}
	}
}