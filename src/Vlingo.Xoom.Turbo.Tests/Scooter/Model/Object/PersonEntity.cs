using Vlingo.Xoom.Lattice.Model;
using Vlingo.Xoom.Turbo.Scooter.Model.Object;

namespace Vlingo.Xoom.Turbo.Tests.Scooter.Model.Object
{
  public class PersonEntity : ObjectEntity<PersonState, DomainEvent>, Person
  {
    private PersonState _person;

    public PersonEntity()
    {
      _person = new PersonState();
    }

    public PersonEntity(int id)
    {
      _person = new PersonState(id, "", 0);
    }

    public void Identify(string name, int age)
    {
      Apply(new PersonState(name, age));
    }

    public void Change(string name)
    {
      Apply(_person.With(name));
    }

    public void IncreaseAge()
    {
      throw new System.NotImplementedException();
    }

    public override string Id()
    {
      throw new System.NotImplementedException();
    }

    protected override void StateObject(PersonState stateObject)
    {
      _person = stateObject;
    }
  }
}