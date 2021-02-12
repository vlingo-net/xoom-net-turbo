// Copyright © 2012-2020 VLINGO LABS. All rights reserved.
//
// This Source Code Form is subject to the terms of the
// Mozilla Public License, v. 2.0. If a copy of the MPL
// was not distributed with this file, You can obtain
// one at https://mozilla.org/MPL/2.0/.

using System;

namespace Vlingo.Xoom.Storage
{
    public class DatabaseParameterNotFoundException : SystemException
    {
        private static string exceptionMessagePattern = "{0} Database {1} not informed";

        public DatabaseParameterNotFoundException(Model model) : base(model.ToString())
        {
        }

        public DatabaseParameterNotFoundException(Model model, string attribute) : base(model.IsDomainModel() ?
                string.Format(exceptionMessagePattern, string.Empty, attribute) :
                string.Format(exceptionMessagePattern, model, attribute))
        {
        }
    }
}
