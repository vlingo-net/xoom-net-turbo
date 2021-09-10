using System;
using Xunit;

namespace Vlingo.Xoom.Turbo.Tests.Scooter.Model.Object
{
  public class PersonEntityTest
  {
    [Fact]
    public void TestThatPersonIdentifiesModifiesRecovers()
    {
      var person = new PersonEntity(1);

      person.Identify("Tom Jones", 78);

      var state1 = person.Applied().state;
      Assert.True(state1.PersistenceId > 0);
      Assert.Equal("Tom Jones", state1.Name);
      Assert.Equal(78, state1.Age);

      person.Change("Tom J Jones");

      var state2 = person.Applied().state;
      Assert.Equal(state1.PersistenceId, state2.PersistenceId);
      Assert.Equal("Tom J Jones", state2.Name);
      Assert.Equal(78, state2.Age);

      person.IncreaseAge();

      var state3 = person.Applied().state;
      Assert.Equal(state1.PersistenceId, state3.PersistenceId);
      Assert.Equal("Tom J Jones", state3.Name);
      Assert.Equal(79, state3.Age);
    }
  }
}