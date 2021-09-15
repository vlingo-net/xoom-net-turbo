using System;
using Vlingo.Xoom.Lattice.Model;
using Vlingo.Xoom.Turbo.Scooter.Model.Stateful;
using Xunit;

namespace Vlingo.Xoom.Turbo.Tests.Scooter.Model.Stateful
{
	public class StatefulEntityTest
	{
		private readonly Random _idGenerator = new Random();

		[Fact]
		public void TestThatStatefulEntityPreservesRestores()
		{
			var entityId = "" + _idGenerator.Next(10_000);
			var state = new Entity1State(entityId, "Sally", 23);

			var entity1 = new Entity1Entity(state);
			Assert.Equal(state, entity1.State());
		}

		public class Entity1Entity : StatefulEntity<Entity1State, DomainEvent>, IEntity1
		{
			private Entity1State _state;

			public Entity1Entity(Entity1State state)
			{
				_state = state;
			}

			public override string Id()
			{
				throw new NotImplementedException();
			}

			protected override void State(Entity1State state)
			{
				_state = state;
			}

			public void ChangeName(string name)
			{
				throw new NotImplementedException();
			}

			public void IncreaseAge()
			{
				throw new NotImplementedException();
			}

			public Entity1State State() => _state;
		}

		public class Entity1State
		{
			public string Id { get; }
			public string Name { get; }
			public int Age { get; }

			public Entity1State(string id, string name, int age)
			{
				Id = id;
				Name = name;
				Age = age;
			}
		}
	}

	public interface IEntity1
	{
		void ChangeName(string name);
		void IncreaseAge();
		StatefulEntityTest.Entity1State State();
	}
}