//// Copyright © 2012-2021 VLINGO LABS. All rights reserved.
////
//// This Source Code Form is subject to the terms of the
//// Mozilla Public License, v. 2.0. If a copy of the MPL
//// was not distributed with this file, You can obtain
//// one at https://mozilla.org/MPL/2.0/.

using System;
using System.Collections.Generic;

namespace Vlingo.Xoom.Turbo.Exchange
{
    public class ExchangeSettingsNotFoundException : SystemException
    {
        public ExchangeSettingsNotFoundException(List<string> parameters) : base(string.Concat("The following exchange parameter(s) were not informed: " + string.Join(", ", parameters)))
        {
        }
    }
}
