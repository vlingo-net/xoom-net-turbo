namespace Vlingo.Xoom.Turbo.Tests.Scooter.Model.Stateful
{
	public interface IEntity1
	{
		void ChangeName(string name);
		void IncreaseAge();
		Entity1State State();
	}
}