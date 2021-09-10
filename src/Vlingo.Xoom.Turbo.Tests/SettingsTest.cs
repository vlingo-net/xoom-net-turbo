﻿// Copyright © 2012-2021 VLINGO LABS. All rights reserved.
//
// This Source Code Form is subject to the terms of the
// Mozilla Public License, v. 2.0. If a copy of the MPL
// was not distributed with this file, You can obtain
// one at https://mozilla.org/MPL/2.0/.

using Vlingo.Xoom.Turbo.actors;
using Xunit;

namespace Vlingo.Xoom.Turbo.Tests
{
    public class SettingsTest
    {
        [Fact]
        public void TestThatSettingsAreLoadedForBlockingMailbox()
        {
            Assert.Equal(25, Settings.Properties().Count);
        }
    }
}