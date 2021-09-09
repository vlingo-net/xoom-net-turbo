//// Copyright © 2012-2021 VLINGO LABS. All rights reserved.
////
//// This Source Code Form is subject to the terms of the
//// Mozilla Public License, v. 2.0. If a copy of the MPL
//// was not distributed with this file, You can obtain
//// one at https://mozilla.org/MPL/2.0/.

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Vlingo.Xoom.Turbo.actors;
using Vlingo.Xoom.Turbo.Storage;
using Xunit;
using static Vlingo.Xoom.Turbo.EnvironmentVariables;

namespace Vlingo.Xoom.Turbo.Tests.Storage
{
  public class DatabaseParametersTest : IDisposable
  {
    private readonly MockEnvironmentVariables _mockEnvironmentVariables;

    public DatabaseParametersTest()
    {
      var dict = new Dictionary<string, string>();
      dict.Add("VLINGO_XOOM_QUERY_DATABASE", "MYSQL");
      dict.Add("VLINGO_XOOM_QUERY_DATABASE_NAME", "12F");
      dict.Add("VLINGO_XOOM_QUERY_DATABASE_URL", "jdbc:mysql://localhost:9001/");
      dict.Add("VLINGO_XOOM_QUERY_DATABASE_DRIVER", "com.mysql.cj.jdbc.Driver");
      dict.Add("VLINGO_XOOM_QUERY_DATABASE_USERNAME", "12FDB");
      dict.Add("VLINGO_XOOM_QUERY_DATABASE_PASSWORD", "vlingo12F");
      dict.Add("VLINGO_XOOM_QUERY_DATABASE_ORIGINATOR", "FTI");
      dict.Add("VLINGO_XOOM_QUERY_DATABASE_CONNECTION_ATTEMPTS", "2");

      _mockEnvironmentVariables = new MockEnvironmentVariables(dict);
      ComponentRegistry.Register(typeof(EnvironmentVariablesRetriever), _mockEnvironmentVariables);
    }

    [Fact]
    public void TestThatDomainParametersAreLoaded()
    {
      var parameters =
        new DatabaseParameters(new Model(ModelType.Domain.ToString()),
          new ReadOnlyDictionary<string, string>(Settings.Properties()));

      Assert.Equal("IN_MEMORY", parameters.Database);
      Assert.True(parameters.AutoCreate);
    }

    public void Dispose()
    {
      ComponentRegistry.Clear();
    }
  }
}