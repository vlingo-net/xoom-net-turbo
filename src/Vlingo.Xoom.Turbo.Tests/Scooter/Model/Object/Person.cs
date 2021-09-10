namespace Vlingo.Xoom.Turbo.Tests.Scooter.Model.Object
{
  public interface Person
  {
    void Identify(string name, int age);
    void Change(string name);
    void IncreaseAge();
  }
}