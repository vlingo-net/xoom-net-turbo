namespace Vlingo.Xoom.Turbo.Tests.Scooter.Model.Object
{
  public interface IEmployee
  {
    void Assign(string number);
    void Adjust(int salary);
    void Hire(string number, int salary);
  }
}