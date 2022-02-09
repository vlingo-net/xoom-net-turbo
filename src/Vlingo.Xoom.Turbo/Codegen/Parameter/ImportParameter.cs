// Copyright © 2012-2022 VLINGO LABS. All rights reserved.
//
// This Source Code Form is subject to the terms of the
// Mozilla Public License, v. 2.0. If a copy of the MPL
// was not distributed with this file, You can obtain
// one at https://mozilla.org/MPL/2.0/.

using System.Collections.Generic;
using System.Linq;

namespace Vlingo.Xoom.Turbo.Codegen.Parameter
{
    public class ImportParameter
    {

        private readonly string _qualifiedClassName;

        public ImportParameter(string qualifiedClassName) => _qualifiedClassName = qualifiedClassName;

        public static HashSet<ImportParameter> Of(params string[] qualifiedClassNames) => Of(qualifiedClassNames as IEnumerable<string>);

        public static HashSet<ImportParameter> Of(params HashSet<string>[] qualifiedNames) => Of(qualifiedNames.SelectMany(x => x));

        public static HashSet<ImportParameter> Of(IEnumerable<string> stateQualifiedNames) => new HashSet<ImportParameter>(stateQualifiedNames.Select(x => new ImportParameter(x)));

        public string GetQualifiedClassName() => _qualifiedClassName;

        public bool MatchClass(string qualifiedClassName) => _qualifiedClassName == qualifiedClassName;

        public override bool Equals(object? o)
        {
            if (this == o) return true;
            if (o == null || GetType() != o.GetType()) return false;
            var that = (ImportParameter)o;
            return _qualifiedClassName == that.GetQualifiedClassName();
        }

        public override int GetHashCode() => _qualifiedClassName.GetHashCode();
    }
}
