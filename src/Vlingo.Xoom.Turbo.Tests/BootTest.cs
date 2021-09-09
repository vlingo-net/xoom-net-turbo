using System;
using Xunit;

namespace Vlingo.Xoom.Turbo.Tests
{
    public class BootTest
    {

        private const string BOOT_WORLD_NAME = "test-boot";

        [Fact]
        public void TestThatWorldStarts()
        {
            var world = Boot.Start(BOOT_WORLD_NAME);

            Assert.Equal(BOOT_WORLD_NAME, world.Name);
        }
    }
}
