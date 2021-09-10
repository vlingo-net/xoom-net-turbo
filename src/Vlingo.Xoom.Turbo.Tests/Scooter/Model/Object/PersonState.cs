using System.Collections.Generic;
using Vlingo.Xoom.Common;
using Vlingo.Xoom.Symbio.Store.Object;

namespace Vlingo.Xoom.Turbo.Tests.Scooter.Model.Object
{
  public class PersonState : StateObject, IEqualityComparer<PersonState>
  {
    private static readonly AtomicLong _identityGenerator = new AtomicLong(0);

    public readonly string Name;
    public readonly int Age;

    public PersonState(long id, string name, int age) : base(id)
    {
      Name = name;
      Age = age;
    }

    public PersonState()
    {
      Name = "";
      Age = 0;
    }

    public PersonState(string name, int age) : base(_identityGenerator.IncrementAndGet())
    {
      Name = name;
      Age = age;
    }

    public PersonState With(string name) => new PersonState(PersistenceId, name, Age);

    public PersonState With(int age) => new PersonState(PersistenceId, Name, age);

    public bool Equals(PersonState x, PersonState y)
    {
      throw new System.NotImplementedException();
    }

    public int GetHashCode(PersonState obj)
    {
      throw new System.NotImplementedException();
    }
  }
}