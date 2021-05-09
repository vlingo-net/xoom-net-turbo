// Copyright © 2012-2021 VLINGO LABS. All rights reserved.
//
// This Source Code Form is subject to the terms of the
// Mozilla Public License, v. 2.0. If a copy of the MPL
// was not distributed with this file, You can obtain
// one at https://mozilla.org/MPL/2.0/.

using System.Collections.Generic;
using System.Linq;

namespace Vlingo.Xoom.Turbo
{
    public class SystemHolder
    {
        private static readonly IDictionary<string, string> Variables = new Dictionary<string, string>();

        static SystemHolder() => LoadVariables();

        public static string GetValue(string key) => Variables.FirstOrDefault(x => x.Key == key).Value;

        public static void SetValue(string key, string value)
        {
            if (Variables.ContainsKey(key))
            {
                Variables[key] = value;
            }
            else
            {
                Variables.Add(key, value);
            }
        }

        public static bool ContainsKey(string key)
        {
            return Variables.ContainsKey(key);
        }

        private static void LoadVariables()
        {
            //variables.Add("key", "value");
        }
    }
}
