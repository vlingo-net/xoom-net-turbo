// Copyright © 2012-2020 VLINGO LABS. All rights reserved.
//
// This Source Code Form is subject to the terms of the
// Mozilla Public License, v. 2.0. If a copy of the MPL
// was not distributed with this file, You can obtain
// one at https://mozilla.org/MPL/2.0/.

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Vlingo.Cluster.Model;

namespace Vlingo.Xoom.Actors
{
    public class Settings
    {
        private static IDictionary<string, string> properties = new Dictionary<string, string>();
        private static string propertiesFileName = "/vlingo-xoom.json";
        private static IDictionary<object, object> defaultDatabaseProperties = new Dictionary<object, object>() {
            { "database", "IN_MEMORY" },
            { "query.database", "IN_MEMORY" }
        };

        static Settings()
        {
            LoadProperties();
        }

        public static void LoadProperties()
        {
            try
            {
                var props = new Properties();
                props.Load(new FileInfo(propertiesFileName));
                var keys = props.Keys;

                if (props == null || keys.Count == 0)
                {
                    Console.WriteLine("Unable to read properties. VLINGO/XOOM will set the default mailbox and logger");
                    properties = defaultDatabaseProperties.ToDictionary(entry => (string)entry.Key, entry => (string)entry.Value);
                }
                else
                {
                    foreach (var key in keys)
                    {
                        properties.Add(key, props.GetProperty(key) ?? string.Empty);
                    }
                }
            }
            catch (IOException e)
            {
                throw new PropertiesLoadingException(e.Message, e);
            }
        }

        public static IDictionary<string, string> Properties()
        {
            return properties;
        }
    }
}
