// Copyright © 2012-2020 VLINGO LABS. All rights reserved.
//
// This Source Code Form is subject to the terms of the
// Mozilla Public License, v. 2.0. If a copy of the MPL
// was not distributed with this file, You can obtain
// one at https://mozilla.org/MPL/2.0/.

using System;
using Vlingo.Actors;

namespace Vlingo.Xoom.Storage
{
    public class DatabaseType
    {
        private string name;

        public DatabaseType(string name)
        {
            this.name = name;
        }

        public static DatabaseCategory retrieveFromConfiguration(Configuration configuration)
        {
            if (configuration == null)
            {
                return DatabaseCategory.IN_MEMORY;
            }

            throw new ArgumentException(string.Concat("Configuration is not supported"));
        }

        public bool IsInMemory()
        {
            return this.Equals(DatabaseCategory.IN_MEMORY);
        }

        public bool HasName(string name)
        {
            return this.name == name;
        }
    }
}
