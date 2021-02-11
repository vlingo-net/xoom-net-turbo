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
using System.Reflection;

namespace Vlingo.Xoom.Actors
{
    public class Settings
    {

        private static IDictionary<string, string> properties = new Dictionary<string, string>();
        private static string propertiesFileName = "/vlingo-xoom.properties";
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
                Assembly assembly = Assembly.GetExecutingAssembly();
                TextReader stream = new StreamReader(assembly.GetManifestResourceStream(propertiesFileName));
                if (stream == null)
                {
                    Console.WriteLine("Unable to read properties. VLINGO/XOOM will set the default mailbox and logger");
                    properties = defaultDatabaseProperties.ToDictionary(entry => (string)entry.Key, entry => (string)entry.Value);
                }
                else
                {
                    string? line;
                    while ((line = stream?.ReadLine()) != null)
                    {
                        string[] keyValuePair = line.Split('=');
                        properties.Add(keyValuePair[0], keyValuePair[1]);

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
