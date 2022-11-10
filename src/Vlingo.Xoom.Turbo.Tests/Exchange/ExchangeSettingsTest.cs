// Copyright © 2012-2022 VLINGO LABS. All rights reserved.
//
// This Source Code Form is subject to the terms of the
// Mozilla Public License, v. 2.0. If a copy of the MPL
// was not distributed with this file, You can obtain
// one at https://mozilla.org/MPL/2.0/.

using System.Collections.ObjectModel;
using Vlingo.Xoom.Turbo.Actors;
using Vlingo.Xoom.Turbo.Exchange;
using Xunit;

namespace Vlingo.Xoom.Turbo.Tests.Exchange;

public class ExchangeSettingsTest
{
    public ExchangeSettingsTest() => ExchangeSettings.Load(Settings.Properties());

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