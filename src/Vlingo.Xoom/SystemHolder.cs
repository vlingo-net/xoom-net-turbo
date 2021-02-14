// Copyright © 2012-2020 VLINGO LABS. All rights reserved.
//
// This Source Code Form is subject to the terms of the
// Mozilla Public License, v. 2.0. If a copy of the MPL
// was not distributed with this file, You can obtain
// one at https://mozilla.org/MPL/2.0/.

using System.Collections.Generic;
using System.Linq;

namespace Vlingo.Xoom
{
    public class SystemHolder
    {
        private static IDictionary<string, string> variables = new Dictionary<string, string>();

        static SystemHolder()
        {
            LoadVariables();
        }

        public static string GetValue(string key)
        {
            return variables.FirstOrDefault(x => x.Key == key).Value;
        }

        public static void SetValue(string key, string value)
        {
            if (variables.ContainsKey(key))
            {
                variables[key] = value;
            }
            else
            {
                variables.Add(key, value);
            }
        }

        public static bool ContainsKey(string key)
        {
            return variables.ContainsKey(key);
        }

        private static void LoadVariables()
        {
            //variables.Add("key", "value");
        }
    }
}
