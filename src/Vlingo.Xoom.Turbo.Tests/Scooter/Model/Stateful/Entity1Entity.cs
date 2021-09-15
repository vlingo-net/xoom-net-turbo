using Vlingo.Xoom.Lattice.Model;
using Vlingo.Xoom.Turbo.Scooter.Model.Stateful;

namespace Vlingo.Xoom.Turbo.Tests.Scooter.Model.Stateful
{
	public class Entity1Entity : StatefulEntity<Entity1State, DomainEvent>, IEntity1
	{
		private Entity1State _state;

		public Entity1Entity(Entity1State state)
		{
			_state = state;
		}

		public override string Id() => _state.Id;

		protected override void State(Entity1State state)
		{
			_state = state;
		}

		public void ChangeName(string name)
		{
			Apply(_state.WithName(name));
		}

		public void IncreaseAge()
		{
			Apply(_state.WithAge(_state.Age + 1));
		}

		public Entity1State State() => _state;
	}
}