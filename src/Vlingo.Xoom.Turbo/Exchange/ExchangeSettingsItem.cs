//// Copyright © 2012-2021 VLINGO LABS. All rights reserved.
////
//// This Source Code Form is subject to the terms of the
//// Mozilla Public License, v. 2.0. If a copy of the MPL
//// was not distributed with this file, You can obtain
//// one at https://mozilla.org/MPL/2.0/.

namespace Vlingo.Xoom.Turbo.Exchange
{
    public class ExchangeSettingsItem
    {
        public readonly string key;
        public readonly string value;

        public ExchangeSettingsItem(string key, string value)
        {
            this.key = key;
            this.value = value;
        }

        public bool HasKey(string key) => this.key == key;
    }
}
