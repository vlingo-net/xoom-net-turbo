// Copyright © 2012-2020 VLINGO LABS. All rights reserved.
//
// This Source Code Form is subject to the terms of the
// Mozilla Public License, v. 2.0. If a copy of the MPL
// was not distributed with this file, You can obtain
// one at https://mozilla.org/MPL/2.0/.

using System;
using System.Collections.Generic;
using System.Linq;

namespace Vlingo.Xoom.Storage
{
    public class DatabaseParameters
    {
        private static string xoomPrefix = "VLINGO_XOOM";
        private static string queryModelPrefix = "query";
        private static string combinationPattern = "{0}.{1}";
        private static List<string> propertiesKeys = new List<string>() { "database", "database.name", "database.driver", "database.url",
                    "database.username", "database.password", "database.originator" };

        public Model model;
        public string? database;
        public string? name;
        public string? driver;
        public string? url;
        public string? username;
        public string? password;
        public string? originator;
        public List<String> keys;
        public bool autoCreate;

        public DatabaseParameters(Model model, IReadOnlyDictionary<string, string> properties) : this(model, properties, true)
        {
        }

        public DatabaseParameters(Model model, IReadOnlyDictionary<string, string> properties, bool autoCreate)
        {
            this.model = model;
            this.keys = PrepareKeys();
            this.database = ValueFromIndex(0, properties);
            this.name = ValueFromIndex(1, properties);
            this.driver = ValueFromIndex(2, properties);
            this.url = ValueFromIndex(3, properties);
            this.username = ValueFromIndex(4, properties);
            this.password = ValueFromIndex(5, properties);
            this.originator = ValueFromIndex(6, properties);
            this.autoCreate = autoCreate;
        }

        private string? ValueFromIndex(int index, IReadOnlyDictionary<string, string> properties)
        {
            if (keys.Count <= index)
            {
                return null;
            }
            return ApplicationProperty.ReadValue(keys[index], properties);
        }

        private void Validate()
        {
            if (database == null)
            {
                throw new DatabaseParameterNotFoundException(model);
            }
            if (!string.Equals(database, DatabaseCategory.IN_MEMORY.ToString(), StringComparison.OrdinalIgnoreCase))
            {
                if (name == null)
                {
                    throw new DatabaseParameterNotFoundException(model, "name");
                }
                if (driver == null)
                {
                    throw new DatabaseParameterNotFoundException(model, "driver");
                }
                if (url == null)
                {
                    throw new DatabaseParameterNotFoundException(model, "url");
                }
                if (username == null)
                {
                    throw new DatabaseParameterNotFoundException(model, "username");
                }
                if (originator == null)
                {
                    throw new DatabaseParameterNotFoundException(model, "originator");
                }
            }
        }

        private List<string> PrepareKeys()
        {
            return propertiesKeys.Where(x => model.IsQueryModel()).Select(key => string.Format(combinationPattern, queryModelPrefix, key)).ToList();
        }

        public void MapToConfiguration()
        {
            Validate();
        }
    }
}
