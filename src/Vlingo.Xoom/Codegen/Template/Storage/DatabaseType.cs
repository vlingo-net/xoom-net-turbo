// Copyright © 2012-2021 VLINGO LABS. All rights reserved.
//
// This Source Code Form is subject to the terms of the
// Mozilla Public License, v. 2.0. If a copy of the MPL
// was not distributed with this file, You can obtain
// one at https://mozilla.org/MPL/2.0/.

using System;
using Vlingo.Xoom.Storage;

namespace Vlingo.Xoom.Codegen.Template.Storage
{
    public class DatabaseType
    {
        public readonly string label;
        public readonly string driver;
        public readonly string connectionUrl;
        public readonly bool configurable;

        public DatabaseType(string label) : this(label, "", "", false)
        {
        }

        public DatabaseType(string label, string driver, string connectionUrl) : this(label, driver, connectionUrl, true)
        {
        }

        public DatabaseType(string label, string driver, string connectionUrl, bool configurable)
        {
            this.label = label;
            this.driver = driver;
            this.connectionUrl = connectionUrl;
            this.configurable = configurable;
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
}
