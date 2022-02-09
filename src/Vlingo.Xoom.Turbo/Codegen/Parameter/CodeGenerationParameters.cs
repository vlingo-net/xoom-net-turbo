// Copyright © 2012-2022 VLINGO LABS. All rights reserved.
//
// This Source Code Form is subject to the terms of the
// Mozilla Public License, v. 2.0. If a copy of the MPL
// was not distributed with this file, You can obtain
// one at https://mozilla.org/MPL/2.0/.

using System;
using System.Collections.Generic;
using System.Linq;

namespace Vlingo.Xoom.Turbo.Codegen.Parameter
{
    public class CodeGenerationParameters
    {
        private readonly List<CodeGenerationParameter> _parameters = new List<CodeGenerationParameter>();

        public static CodeGenerationParameters From(Label label, object value) => From(label, value.ToString()!);

        public static CodeGenerationParameters From(Label label, string value) => From(CodeGenerationParameter.Of(label, value));

        public static CodeGenerationParameters From(params CodeGenerationParameter[] codeGenerationParameters) => 
            new CodeGenerationParameters(codeGenerationParameters.ToList());

        public static CodeGenerationParameters Empty() => new CodeGenerationParameters(new List<CodeGenerationParameter>());

        private CodeGenerationParameters(List<CodeGenerationParameter> parameters) => _parameters.AddRange(parameters);

        public CodeGenerationParameters Add(Label label, object value) => Add(label, value.ToString()!);

        public CodeGenerationParameters Add(Label label, string value) => Add(CodeGenerationParameter.Of(label, value));

        public CodeGenerationParameters Add(CodeGenerationParameter parameter)
        {
            _parameters.Add(parameter);
            return this;
        }

        public void AddAll(IReadOnlyDictionary<Label, string> parameterEntries) => AddAll(parameterEntries.Select(x => CodeGenerationParameter.Of(x.Key, x.Value)).ToList());

        public void AddAll(CodeGenerationParameters parameters) => AddAll(parameters.List());

        public CodeGenerationParameters AddAll(List<CodeGenerationParameter> parameters)
        {
            _parameters.AddRange(parameters);
            return this;
        }

        public string RetrieveValue(Label label) => RetrieveOne(label).Value;

        public T RetrieveValue<T>(Label label, Func<string, T> mapper) => mapper(RetrieveValue(label));

        public CodeGenerationParameter RetrieveOne(Label label)
        {
            var firstLabeled = _parameters.FirstOrDefault(param => param.IsLabeled(label));
            return firstLabeled != null ? firstLabeled : CodeGenerationParameter.Of(label, "");
        }

        public List<CodeGenerationParameter> List() => _parameters;

        public IEnumerable<CodeGenerationParameter> RetrieveAll(Label label) => _parameters.Where(param => param.IsLabeled(label));

        public bool IsEmpty() => _parameters == null || _parameters.Count == 0;
    }
}
