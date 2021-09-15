namespace Vlingo.Xoom.Turbo.Tests.Scooter.Model.Stateful
{
	public class Entity1State
	{
		public string Id { get; }
		public string Name { get; }
		public int Age { get; }

		public Entity1State(string id, string name = "", int age = 0)
		{
			Id = id;
			Name = name;
			Age = age;
		}

		public Entity1State WithName(string name) => new Entity1State(Id, name, Age);

		public Entity1State WithAge(int age) => new Entity1State(Id, Name, age);
	}
}