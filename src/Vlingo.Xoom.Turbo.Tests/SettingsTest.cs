using System;
using Vlingo.Xoom.Turbo.actors;
using Xunit;

namespace Vlingo.Xoom.Turbo.Tests
{
    public class SettingsTest
    {
        [Fact]
        public void TestThatSettingsAreLoadedForBlockingMailbox()
        {
            Assert.Equal(23, Settings.Properties().Count);
        }
    }
}
