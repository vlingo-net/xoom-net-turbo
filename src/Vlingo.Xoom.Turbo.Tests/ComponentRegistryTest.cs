//// Copyright © 2012-2021 VLINGO LABS. All rights reserved.
////
//// This Source Code Form is subject to the terms of the
//// Mozilla Public License, v. 2.0. If a copy of the MPL
//// was not distributed with this file, You can obtain
//// one at https://mozilla.org/MPL/2.0/.

using System;
using Xunit;

namespace Vlingo.Xoom.Turbo.Tests
{
    public class ComponentRegistryTest : IDisposable
    {
        [Fact]
        public void TestThatComponentsAreRegistered()
        {
            ComponentRegistry.Register("appName", "xoom-app");
            ComponentRegistry.Register(typeof(bool), bool.TrueString);

            Assert.True(ComponentRegistry.Has("appName"));
            Assert.True(ComponentRegistry.Has(typeof(bool)));

            Assert.False(ComponentRegistry.Has("component"));
            Assert.False(ComponentRegistry.Has(typeof(int)));

            Assert.Equal("xoom-app", ComponentRegistry.WithName("appName"));
            Assert.Equal(bool.TrueString, ComponentRegistry.WithType(typeof(bool)));
        }

        public void Dispose()
        {
            ComponentRegistry.Clear();
        }
    }
}
