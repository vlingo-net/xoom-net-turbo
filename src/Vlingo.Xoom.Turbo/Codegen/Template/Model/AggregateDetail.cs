// Copyright © 2012-2021 VLINGO LABS. All rights reserved.
//
// This Source Code Form is subject to the terms of the
// Mozilla Public License, v. 2.0. If a copy of the MPL
// was not distributed with this file, You can obtain
// one at https://mozilla.org/MPL/2.0/.

using System;
using System.Linq;
using Vlingo.Xoom.Turbo.Codegen.Parameter;

namespace Vlingo.Xoom.Turbo.Codegen.Template.Model
{
    public class AggregateDetail
    {
        public static CodeGenerationParameter MethodWithName(CodeGenerationParameter aggregate, string methodName) => FindMethod(aggregate, methodName) ?? throw new ArgumentException(string.Concat("Method ", methodName, " not found"));

        private static CodeGenerationParameter? FindMethod(CodeGenerationParameter aggregate, string methodName) => aggregate.RetrieveAllRelated(Label.AggregateMethod).Where(method => methodName == method.value || method.value.StartsWith(string.Concat(methodName, "("))).First();
    }
}
