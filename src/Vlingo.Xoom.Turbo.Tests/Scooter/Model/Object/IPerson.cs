namespace Vlingo.Xoom.Turbo.Tests.Scooter.Model.Object
{
  public interface IPerson
  {
    void Identify(string name, int age);
    void Change(string name);
    void IncreaseAge();
  }
}