// Copyright © 2012-2021 VLINGO LABS. All rights reserved.
//
// This Source Code Form is subject to the terms of the
// Mozilla Public License, v. 2.0. If a copy of the MPL
// was not distributed with this file, You can obtain
// one at https://mozilla.org/MPL/2.0/.

using System.Globalization;

namespace Vlingo.Xoom.Turbo.Codegen.Template.Autodispatch
{
    public class AutoDispatchMappingValueFormatter
    {
        public static string Format(string signature)
        {
            var titleCase = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(signature);
            titleCase.Replace(" ", "");
            return string.Concat(titleCase.Substring(0, 1).ToLower(), titleCase.Substring(1, titleCase.Length - 1));
        }
    }
}
