using System.Collections.Generic;

namespace Vlingo.Xoom.Turbo.Tests
{
  public class MockEnvironmentVariables : EnvironmentVariables.EnvironmentVariablesRetriever
  {
    private readonly Dictionary<string, string> _values;

    public MockEnvironmentVariables(Dictionary<string, string> values)
    {
      _values = values;
    }

    public string Retrieve(string key) => _values.GetValueOrDefault(key);

    public bool ContainsKey(string key) => _values.ContainsKey(key);
  }
}