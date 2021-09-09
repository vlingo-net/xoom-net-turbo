using System.Collections.ObjectModel;
using NuGet.Frameworks;
using Vlingo.Xoom.Turbo.actors;
using Vlingo.Xoom.Turbo.Exchange;
using Xunit;

namespace Vlingo.Xoom.Turbo.Tests.Exchange
{
  public class ExchangeSettingsTest
  {
    public ExchangeSettingsTest()
    {
      ExchangeSettings.Load(new ReadOnlyDictionary<string, string>(Settings.Properties()));
    }

    [Fact]
    public void TestThatConnectionSettingsAreMapped()
    {
      var firstSettings = ExchangeSettings.Of("first").MapToConnection();

      Assert.Equal("first-exchange", firstSettings.HostName);
      Assert.Equal("vlingo01", firstSettings.Username);
      Assert.Equal("vlingo-pass01", firstSettings.Password);
      Assert.Equal("virtual-first-exchange", firstSettings.VirtualHost);
      Assert.Equal(1000, firstSettings.Port);

      var secondSettings = ExchangeSettings.Of("second").MapToConnection();

      Assert.Equal("second-exchange", secondSettings.HostName);
      Assert.Equal("vlingo02", secondSettings.Username);
      Assert.Equal("vlingo-pass02", secondSettings.Password);
      Assert.Equal("virtual-second-exchange", secondSettings.VirtualHost);
      Assert.Equal(1001, secondSettings.Port);

      Assert.Equal(2, ExchangeSettings.All().Count);
    }
  }
}