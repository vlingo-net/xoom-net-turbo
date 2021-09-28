namespace Vlingo.Xoom.Turbo.Tests.Scooter.Persistence
{
	public class Entity2
	{
		public string Id { get; }
		public string Value { get; }

		public Entity2(string id, string value)
		{
			Id = id;
			Value = value;
		}
		
		public override bool Equals(object? obj)
		{
			if(obj ==  null || obj.GetType() != GetType())
				return false;
			return Id == ((Entity2)obj).Id;
		}
	}
}