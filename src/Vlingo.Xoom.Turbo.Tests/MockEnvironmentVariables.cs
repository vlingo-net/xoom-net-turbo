// Copyright © 2012-2022 VLINGO LABS. All rights reserved.
//
// This Source Code Form is subject to the terms of the
// Mozilla Public License, v. 2.0. If a copy of the MPL
// was not distributed with this file, You can obtain
// one at https://mozilla.org/MPL/2.0/.

using System.Collections.Generic;

namespace Vlingo.Xoom.Turbo.Tests;

public class MockIEnvironmentVariables : EnvironmentVariables.IEnvironmentVariablesRetriever
{
    private readonly Dictionary<string, string> _values;

    public MockIEnvironmentVariables(Dictionary<string, string> values) => _values = values;

    public string Retrieve(string key) => _values.GetValueOrDefault(key);

    public bool ContainsKey(string key) => _values.ContainsKey(key);
}