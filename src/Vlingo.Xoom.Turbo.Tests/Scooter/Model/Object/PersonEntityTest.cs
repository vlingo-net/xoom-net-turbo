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
    }
  }
}