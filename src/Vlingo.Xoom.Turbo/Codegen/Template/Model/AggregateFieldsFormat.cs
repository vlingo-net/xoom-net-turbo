// Copyright © 2012-2021 VLINGO LABS. All rights reserved.
//
// This Source Code Form is subject to the terms of the
// Mozilla Public License, v. 2.0. If a copy of the MPL
// was not distributed with this file, You can obtain
// one at https://mozilla.org/MPL/2.0/.

using System;
using System.Collections.Generic;
using System.Linq;
using Vlingo.Xoom.Turbo.Annotation.Codegen.AutoDispatch;
using Vlingo.Xoom.Turbo.Codegen.Parameter;

namespace Vlingo.Xoom.Turbo.Codegen.Template.Model
{
    public class AggregateFieldsFormat<T>
    {
        AggregateFieldsFormat<IEnumerable<String>> assignment = new Constructor();
        AggregateFieldsFormat<IEnumerable<String>> memberDeclaration = new Member();
        AggregateFieldsFormat<IEnumerable<String>> stateBasedAssignment = new Constructor("state");
        AggregateFieldsFormat<string> selfAlternateReference = AlternateReference.HandlingSelfReferencedFields();
        AggregateFieldsFormat<string> defaultValue = AlternateReference.HandlingDefaultFieldsValue();

        public virtual T Format(CodeGenerationParameter aggregate) => Format(aggregate, aggregate.RetrieveAllRelated(Label.StateField));

        //TODO: has no body in java
        public virtual T Format(CodeGenerationParameter parameter, IEnumerable<CodeGenerationParameter> fields) => throw new NotImplementedException();

        public class Member : AggregateFieldsFormat<IEnumerable<string>>
        {
            private static readonly string _pattern = "public final {0} {1};";

            public override IEnumerable<string> Format(CodeGenerationParameter aggregate, IEnumerable<CodeGenerationParameter> fields) => fields.Select(field => string.Format(_pattern, FieldDetail.TypeOf(aggregate, field.value), field.value));
        }

        public class Constructor : AggregateFieldsFormat<IEnumerable<string>>
        {
            private readonly string _carrierName;
            private static readonly string _pattern = "this.{0} = {1};";

            public Constructor() : this(string.Empty)
            {
            }

            public Constructor(string carrierName) => _carrierName = carrierName;

            public override IEnumerable<string> Format(CodeGenerationParameter aggregate, IEnumerable<CodeGenerationParameter> fields) => fields.Select(field => string.Format(_pattern, field.value, ResolveValueRetrieval(field)));

            private string ResolveValueRetrieval(CodeGenerationParameter field) => _carrierName == string.Empty ? field.value : string.Concat(_carrierName, ".", field.value);
        }

        public class AlternateReference : AggregateFieldsFormat<string>
        {
            private readonly Func<CodeGenerationParameter, string> _absenceHandler;

            private AlternateReference(Func<CodeGenerationParameter, string> absenceHandler) => _absenceHandler = absenceHandler;

            public static AlternateReference HandlingSelfReferencedFields() => new AlternateReference(field => string.Concat("this.", field.value));

            public static AlternateReference HandlingDefaultFieldsValue() => new AlternateReference(field => FieldDetail.ResolveDefaultValue(field.Parent(Label.Aggregate), field.value));

            public override string Format(CodeGenerationParameter para, IEnumerable<CodeGenerationParameter> fields) => string.Join(", ", para.RetrieveAllRelated(Label.StateField).Select(field => IsPresent(field, fields.ToList()) ? field.value : _absenceHandler(field)));

            public static bool IsPresent(CodeGenerationParameter field, List<CodeGenerationParameter> presentFields) => presentFields.Any(present => present.value == field.value);
        }
    }
}
