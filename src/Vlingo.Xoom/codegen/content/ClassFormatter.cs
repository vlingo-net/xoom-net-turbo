// Copyright © 2012-2020 VLINGO LABS. All rights reserved.
//
// This Source Code Form is subject to the terms of the
// Mozilla Public License, v. 2.0. If a copy of the MPL
// was not distributed with this file, You can obtain
// one at https://mozilla.org/MPL/2.0/.

using System;
using System.Linq;

namespace Vlingo.Xoom.Codegen.Content
{
    public class ClassFormatter
    {
        public static string QualifiedNameOf(string packageName, string className) => string.Concat(packageName, ".", className);

        public static string SimpleNameToAttribute(string simpleName) => simpleName.Length == 1 ? simpleName.ToLowerInvariant() : simpleName.All(x => Char.IsUpper(x)) ? simpleName : string.Concat(simpleName.Substring(0, 1).ToLowerInvariant(), simpleName.Substring(1, simpleName.Length - 1));

        public static string QualifiedNameToAttribute(string qualifiedName) => SimpleNameToAttribute(SimpleNameOf(qualifiedName));

        public static string SimpleNameOf(string qualifiedName) => qualifiedName.Substring(qualifiedName.LastIndexOf(".") + 1);

        public static string PackageOf(string qualifiedName) => qualifiedName.Substring(0, qualifiedName.LastIndexOf("."));
    }
}
