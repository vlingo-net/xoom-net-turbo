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
        public static IDictionary<string, string> variables = new Dictionary<string, string>();

        static SystemHolder()
        {
            LoadVariables();
        }

        public static string GetValue(string key)
        {
            if (variables == null)
            {
                return string.Empty;
            }
            return variables.FirstOrDefault(x => x.Key == key).Value;
        }

        private static void LoadVariables()
        {
            //variables.Add("key", "value");
        }
    }
}
