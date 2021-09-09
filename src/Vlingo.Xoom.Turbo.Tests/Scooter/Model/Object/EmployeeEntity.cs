using Vlingo.Xoom.Lattice.Model;
using Vlingo.Xoom.Turbo.Scooter.Model.Object;

namespace Vlingo.Xoom.Turbo.Tests.Scooter.Model.Object
{
  public class EmployeeEntity : ObjectEntity<EmployeeState, DomainEvent>, Employee
  {
    private EmployeeState _employee;

    public EmployeeEntity()
    {
      _employee = new EmployeeState();
    }

    public EmployeeEntity(long id)
    {
      _employee = new EmployeeState(id, "", 0);
    }

    public void Assign(string number)
    {
      throw new System.NotImplementedException();
    }

    public void Adjust(int salary)
    {
      throw new System.NotImplementedException();
    }

    public void Hire(string number, int salary)
    {
      Apply(_employee.With(number).With(salary), new Employee.EmployeeHired());
    }

    public override string Id()
    {
      throw new System.NotImplementedException();
    }

    protected override void StateObject(EmployeeState stateObject)
    {
      _employee = stateObject;
    }
  }
}