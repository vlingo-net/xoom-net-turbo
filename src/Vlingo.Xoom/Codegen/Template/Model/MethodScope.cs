// Copyright © 2012-2020 VLINGO LABS. All rights reserved.
//
// This Source Code Form is subject to the terms of the
// Mozilla Public License, v. 2.0. If a copy of the MPL
// was not distributed with this file, You can obtain
// one at https://mozilla.org/MPL/2.0/.

using System;
using System.Collections.Generic;
using Vlingo.Xoom.Codegen.Parameter;

namespace Vlingo.Xoom.Codegen.Template.Model
{
    public enum MethodScopeType
    {
        Instance,
        Static
    }
    public class MethodScope
    {
        public readonly Type[] requiredClasses;

        public MethodScope(params Type[] requiredClasses)
        {
            this.requiredClasses = requiredClasses;
        }

        public bool IsStatic(MethodScopeType methodScopeType) => methodScopeType == MethodScopeType.Static;

        public bool IsInstance(MethodScopeType methodScopeType) => methodScopeType == MethodScopeType.Instance;

        public static IEnumerable<MethodScopeType> Infer(CodeGenerationParameter method)
        {
            if (method.RetrieveRelatedValue(Label.FactoryMethod, x => x == bool.TrueString.ToLower()))
            {
                return new List<MethodScopeType>() { MethodScopeType.Instance, MethodScopeType.Static };
            }
            return new List<MethodScopeType>() { MethodScopeType.Instance };
        }
    }
}
