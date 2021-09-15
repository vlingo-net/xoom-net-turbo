using System;
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

			entity1.ChangeName("Sally Jane");
			var newState = entity1.State();
			Assert.Equal("Sally Jane", newState.Name);

			entity1.IncreaseAge();
			newState = entity1.State();
			Assert.Equal(24, newState.Age);

			var identityState = new Entity1State(entityId);
			var restoredEntity1 = new Entity1Entity(identityState);
			var restoredEntity1State = restoredEntity1.State();
			Assert.NotNull(restoredEntity1State);
		}

		[Fact]
		public void TestThatMetadataCallbackPreservesRestores()
		{
			var entityId = "" + _idGenerator.Next(10_000);
			var state = new Entity1State(entityId, "Sally", 23);

			var entity1 = new Entity1Entity(state);
			var current1 = entity1.State();
			Assert.Equal(state, current1);

			entity1.ChangeName("Sally Jane");
			var newState = entity1.State();
			Assert.Equal("Sally Jane", newState.Name);

			entity1.IncreaseAge();
			newState = entity1.State();
			Assert.Equal(24, newState.Age);

			var identityState = new Entity1State(entityId);
			var restoredEntity1 = new Entity1Entity(identityState);
			var restoredEntity1State = restoredEntity1.State();
			Assert.NotNull(restoredEntity1State);
		}
	}
}