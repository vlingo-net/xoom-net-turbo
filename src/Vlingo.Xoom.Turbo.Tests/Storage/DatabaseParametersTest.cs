// Copyright © 2012-2022 VLINGO LABS. All rights reserved.
//
// This Source Code Form is subject to the terms of the
// Mozilla Public License, v. 2.0. If a copy of the MPL
// was not distributed with this file, You can obtain
// one at https://mozilla.org/MPL/2.0/.

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Vlingo.Xoom.Turbo.Actors;
using Vlingo.Xoom.Turbo.Storage;
using Xunit;
using static Vlingo.Xoom.Turbo.EnvironmentVariables;

namespace Vlingo.Xoom.Turbo.Tests.Storage
{
    public class DatabaseParametersTest : IDisposable
    {
        private readonly MockIEnvironmentVariables _mockIEnvironmentVariables;

        public DatabaseParametersTest()
        {
            var dict = new Dictionary<string, string>();
            dict.Add("VLINGO_XOOM_QUERY_DATABASE_CATEGORY", "MYSQL");
            dict.Add("VLINGO_XOOM_QUERY_DATABASE_NAME", "12F");
            dict.Add("VLINGO_XOOM_QUERY_DATABASE_URL", "jdbc:mysql://localhost:9001/");
            dict.Add("VLINGO_XOOM_QUERY_DATABASE_DRIVER", "com.mysql.cj.jdbc.Driver");
            dict.Add("VLINGO_XOOM_QUERY_DATABASE_USERNAME", "12FDB");
            dict.Add("VLINGO_XOOM_QUERY_DATABASE_PASSWORD", "vlingo12F");
            dict.Add("VLINGO_XOOM_QUERY_DATABASE_ORIGINATOR", "FTI");
            dict.Add("VLINGO_XOOM_QUERY_DATABASE_CONNECTION_ATTEMPTS", "2");

            _mockIEnvironmentVariables = new MockIEnvironmentVariables(dict);
            ComponentRegistry.Register<IEnvironmentVariablesRetriever>(_mockIEnvironmentVariables);
        }

        [Fact]
        public void TestThatDomainParametersAreLoaded()
        {
            var parameters = new DatabaseParameters(new Model(ModelType.Domain.ToString()),
                new ReadOnlyDictionary<string, string>(Settings.Properties()));

            Assert.Equal("IN_MEMORY", parameters.Database);
            Assert.True(parameters.AutoCreate);
        }

        [Fact]
        public void TestThatCommandParametersAreLoaded()
        {
            var parameters = new DatabaseParameters(new Model(ModelType.Command.ToString()),
                new ReadOnlyDictionary<string, string>(Settings.Properties()));

            Assert.Equal("IN_MEMORY", parameters.Database);
            Assert.True(parameters.AutoCreate);
        }

        [Fact]
        public void TestThatQueryParametersAreLoaded()
        {
            var parameters = new DatabaseParameters(new Model(ModelType.Query.ToString()),
                new ReadOnlyDictionary<string, string>(Settings.Properties()), true);

            Assert.Equal("MYSQL", parameters.Database);
            Assert.Equal("STORAGE_TEST", parameters.Name);
            Assert.Equal("jdbc:mysql://localhost:2215/", parameters.Url);
            Assert.Equal("com.mysql.cj.jdbc.Driver", parameters.Driver);
            Assert.Equal("vlingo_test", parameters.Username);
            Assert.Equal("vlingo123", parameters.Password);
            Assert.Equal("MAIN", parameters.Originator);
            Assert.True(parameters.AutoCreate);
        }

        [Fact]
        public void TestThatQueryParametersAreLoadedEnvVars()
        {
            var parameters = new DatabaseParameters(new Model(ModelType.Query.ToString()),
                new ReadOnlyDictionary<string, string>(new Dictionary<string, string>()), true);

            Assert.Equal("MYSQL", parameters.Database);
            Assert.Equal("12F", parameters.Name);
            Assert.Equal("jdbc:mysql://localhost:9001/", parameters.Url);
            Assert.Equal("com.mysql.cj.jdbc.Driver", parameters.Driver);
            Assert.Equal("12FDB", parameters.Username);
            Assert.Equal("vlingo12F", parameters.Password);
            Assert.Equal("FTI", parameters.Originator);
            Assert.True(parameters.AutoCreate);
        }

        public void Dispose()
        {
            ComponentRegistry.Clear();
        }
    }
}