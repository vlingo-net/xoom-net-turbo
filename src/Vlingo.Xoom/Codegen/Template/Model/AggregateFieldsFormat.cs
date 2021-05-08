// Copyright © 2012-2020 VLINGO LABS. All rights reserved.
//
// This Source Code Form is subject to the terms of the
// Mozilla Public License, v. 2.0. If a copy of the MPL
// was not distributed with this file, You can obtain
// one at https://mozilla.org/MPL/2.0/.

using System;
using System.Collections.Generic;
using System.Linq;
using Vlingo.Xoom.Codegen.Parameter;

namespace Vlingo.Xoom.Codegen.Template.Model
{
    public class AggregateFieldsFormat<T>
    {
        AggregateFieldsFormat<List<String>> assignment = new Constructor();
        AggregateFieldsFormat<List<String>> memberDeclaration = new Member();
        AggregateFieldsFormat<List<String>> stateBasedAssignment = new Constructor("state");
        AggregateFieldsFormat<String> selfAlternateReference = AlternateReference.HandlingSelfReferencedFields();
        AggregateFieldsFormat<String> defaultValue = AlternateReference.HandlingDefaultFieldsValue();

        public T Format(CodeGenerationParameter aggregate) => Format(aggregate, aggregate.RetrieveAllRelated(Label.StateField));

        //TODO: has no body in java
        public T Format(CodeGenerationParameter parameter, IEnumerable<CodeGenerationParameter> fields) => throw new NotImplementedException();

        public class Member : AggregateFieldsFormat<List<string>>
        {
            private static readonly string _pattern = "public final {0} {1};";

            public IEnumerable<string> Format(CodeGenerationParameter aggregate, IEnumerable<CodeGenerationParameter> fields) => fields.Select(field => string.Format(_pattern, StateFieldDetail.TypeOf(aggregate, field.value), field.value));
        }

        public class Constructor : AggregateFieldsFormat<List<string>>
        {
            private readonly string _carrierName;
            private static readonly string _pattern = "this.{0} = {1};";

            public Constructor() : this(string.Empty)
            {
            }

            public Constructor(string carrierName) => _carrierName = carrierName;

            public IEnumerable<string> Format(CodeGenerationParameter aggregate, IEnumerable<CodeGenerationParameter> fields) => fields.Select(field => string.Format(_pattern, field.value, ResolveValueRetrieval(field)));

            private string ResolveValueRetrieval(CodeGenerationParameter field) => _carrierName == string.Empty ? field.value : string.Concat(_carrierName, ".", field.value);
        }

        public class AlternateReference : AggregateFieldsFormat<string>
        {
            private readonly Func<CodeGenerationParameter, string> _absenceHandler;

            private AlternateReference(Func<CodeGenerationParameter, string> absenceHandler) => _absenceHandler = absenceHandler;

            public static AlternateReference HandlingSelfReferencedFields() => new AlternateReference(field => string.Concat("this.", field.value));

            public static AlternateReference HandlingDefaultFieldsValue() => new AlternateReference(field => StateFieldDetail.ResolveDefaultValue(field.Parent(Label.Aggregate), field.value));

            public string Format(CodeGenerationParameter para, IEnumerable<CodeGenerationParameter> fields) => string.Join(", ", para.RetrieveAllRelated(Label.StateField).Select(field => IsPresent(field, fields.ToList()) ? field.value : _absenceHandler(field)));

            public static bool IsPresent(CodeGenerationParameter field, List<CodeGenerationParameter> presentFields) => presentFields.Any(present => present.value == field.value);
        }
    }
}
