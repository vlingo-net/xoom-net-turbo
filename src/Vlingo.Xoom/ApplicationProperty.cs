// Copyright © 2012-2020 VLINGO LABS. All rights reserved.
//
// This Source Code Form is subject to the terms of the
// Mozilla Public License, v. 2.0. If a copy of the MPL
// was not distributed with this file, You can obtain
// one at https://mozilla.org/MPL/2.0/.

using System;
using System.Collections.Generic;
using System.Linq;

namespace Vlingo.Xoom {
    public class ApplicationProperty {
        private static string xoomPrefix = "VLINGO_XOOM";
        private static string combinationPattern = "{0}.{1}";

        public static string readValue(string key, Dictionary<string, string> properties) {
            string propertiesValue = retrieveFromProperties(key, properties);
            return propertiesValue != null ? propertiesValue : retrieveFromEnvironment(key);
        }

        public static List<string> readMultipleValues(string key, string separator, Dictionary<string, string> properties) {
            string value = readValue(key, properties);

            return value == null ? new List<string>() : value.Split(new string[] { separator }, StringSplitOptions.None).ToList();
        }

        private static string retrieveFromProperties(string key, Dictionary<string, string> properties) {
            if (!properties.ContainsKey(key)) {
                return null;
            }
            string value = properties.FirstOrDefault(x => x.Key == key).Value.Trim();

            return string.IsNullOrEmpty(value) ? null : value;
        }

        private static string retrieveFromEnvironment(string key) {
            string envKey = resolveEnvironmentVariable(key);

            if (SystemHolder.variables.ContainsKey(envKey)) {
                return null;
            }

            string value = SystemHolder.getValue(envKey);

            if (value == null || string.IsNullOrEmpty(value.Trim())) {
                return null;
            }

            return value.Trim();
        }

        private static string resolveEnvironmentVariable(string key) {
            return string.Format(combinationPattern, xoomPrefix, key).Replace("\\.", "_");
        }
    }
}
