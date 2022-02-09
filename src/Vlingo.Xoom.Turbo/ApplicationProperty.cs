// Copyright © 2012-2022 VLINGO LABS. All rights reserved.
//
// This Source Code Form is subject to the terms of the
// Mozilla Public License, v. 2.0. If a copy of the MPL
// was not distributed with this file, You can obtain
// one at https://mozilla.org/MPL/2.0/.

using System;
using System.Collections.Generic;
using System.Linq;

namespace Vlingo.Xoom.Turbo
{
    public class ApplicationProperty
    {
        private static readonly string XoomPrefix = "VLINGO_XOOM";
        private static readonly string CombinationPattern = "{0}.{1}";

        public static string? ReadValue(string key, IReadOnlyDictionary<string, string> properties)
        {
            var propertiesValue = RetrieveFromProperties(key, properties);
            return propertiesValue != null ? propertiesValue : RetrieveFromEnvironment(key);
        }

        public static List<string> ReadMultipleValues(string key, string separator, IReadOnlyDictionary<string, string> properties)
        {
            var value = ReadValue(key, properties);
            return value == null
                ? new List<string>()
                : value.Split(new[] { separator }, StringSplitOptions.None).ToList();
        }

        private static string? RetrieveFromProperties(string key, IReadOnlyDictionary<string, string> properties)
        {
            if (!properties.ContainsKey(key))
            {
                return null;
            }

            var value = properties.FirstOrDefault(x => x.Key == key).Value.Trim();

            return string.IsNullOrEmpty(value) ? null : value;
        }

        private static string? RetrieveFromEnvironment(string key)
        {
            var envKey = ResolveEnvironmentVariable(key);

            if (!EnvironmentVariables.ContainsKey(envKey))
            {
                return null;
            }

            var value = EnvironmentVariables.Retrieve(envKey);

            if (value == null || string.IsNullOrEmpty(value.Trim()))
            {
                return null;
            }

            return value.Trim();
        }

        private static string ResolveEnvironmentVariable(string key) => string
            .Format(CombinationPattern, XoomPrefix, key)
            .Replace(".", "_").ToUpper();
    }
}