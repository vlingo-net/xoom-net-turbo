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

namespace io.vlingo.xoom.actors {
    public class Settings {

        private static Dictionary<string, string> PROPERTIES = new Dictionary<string, string>();
        private static readonly string PROPERTIES_FILENAME = "/vlingo-xoom.properties";
        private static readonly long serialVersionUID = 1L;
        private static readonly Dictionary<object, object> DEFAULT_DATABASE_PROPERTIES = new Dictionary<object, object>() {
            { "database", "IN_MEMORY" },
            { "query.database", "IN_MEMORY" }
        };

        static Settings() {
            loadProperties();
        }

        public static void loadProperties() {
            try {
                Assembly assembly = Assembly.GetExecutingAssembly();
                TextReader stream = new StreamReader(assembly.GetManifestResourceStream(PROPERTIES_FILENAME));
                if (stream == null) {
                    Console.WriteLine("Unable to read properties. VLINGO/XOOM will set the default mailbox and logger");
                    PROPERTIES = DEFAULT_DATABASE_PROPERTIES.ToDictionary(entry => (string)entry.Key, entry => (string)entry.Value);
                }
                else {
                    string line;
                    while ((line = stream.ReadLine()) != null) {
                        string[] keyValuePair = line.Split('=');
                        PROPERTIES.Add(keyValuePair[0], keyValuePair[1]);

                    }
                }
            }
            catch (IOException e) {
                throw new PropertiesLoadingException(e.Message, e);
            }
        }

        public static Dictionary<string, string> properties() {
            return PROPERTIES;
        }
    }
}
