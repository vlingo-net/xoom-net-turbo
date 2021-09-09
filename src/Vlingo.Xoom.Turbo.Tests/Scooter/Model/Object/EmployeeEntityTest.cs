using Xunit;

namespace Vlingo.Xoom.Turbo.Tests.Scooter.Model.Object
{
  public class EmployeeEntityTest
  {
    [Fact]
    public void TestThatEmployeeIdentifiesModifiesRecovers()
    {
      var employee = new EmployeeEntity();

      employee.Hire("12345", 50000);

      var state1 = employee.Applied().state;
      Assert.True(state1.PersistenceId > 0);
      Assert.Equal("12345", state1.Number);
      Assert.Equal(50000, state1.Salary);
    }
  }
}