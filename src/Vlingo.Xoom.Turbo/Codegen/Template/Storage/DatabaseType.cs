// Copyright © 2012-2023 VLINGO LABS. All rights reserved.
//
// This Source Code Form is subject to the terms of the
// Mozilla Public License, v. 2.0. If a copy of the MPL
// was not distributed with this file, You can obtain
// one at https://mozilla.org/MPL/2.0/.

using System;
using Vlingo.Xoom.Turbo.Storage;

namespace Vlingo.Xoom.Turbo.Codegen.Template.Storage;

public class DatabaseType
{
    public readonly string Label;
    public readonly string Driver;
    public readonly string ConnectionUrl;
    public readonly bool Configurable;

    public DatabaseType(string label) : this(label, "", "", false)
    {
    }

    public DatabaseType(string label, string driver, string connectionUrl) : this(label, driver, connectionUrl, true)
    {
    }

    public DatabaseType(string label, string driver, string connectionUrl, bool configurable)
    {
        Label = label;
        Driver = driver;
        ConnectionUrl = connectionUrl;
        Configurable = configurable;
    }

    public static DatabaseCategory GetOrDefault(string name, DatabaseCategory defaultDatabase)
    {
        if (name == null || name.Trim() == string.Empty)
        {
            return defaultDatabase;
        }
        return (DatabaseCategory)Enum.Parse(typeof(DatabaseCategory), name);
    }
}