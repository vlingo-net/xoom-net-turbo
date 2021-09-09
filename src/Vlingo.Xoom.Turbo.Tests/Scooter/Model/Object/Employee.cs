using Vlingo.Xoom.Lattice.Model;

namespace Vlingo.Xoom.Turbo.Tests.Scooter.Model.Object
{
  public interface Employee
  {
    void Assign(string number);
    void Adjust(int salary);
    void Hire(string number, int salary);

    public class EmployeeHired : DomainEvent
    {
    }

    public class EmployeeSalaryAdjusted : DomainEvent
    {
    }

    public class EmployeeNumberAssigned : DomainEvent
    {
    }
  }
}