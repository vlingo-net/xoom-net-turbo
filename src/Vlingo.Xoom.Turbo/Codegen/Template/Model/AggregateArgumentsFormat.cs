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
    public class AggregateArgumentsFormat
    {
        AggregateArgumentsFormat _methodInvocation = new MethodInvocation("stage");
        AggregateArgumentsFormat _signatureDeclaration = new SignatureDeclaration();

        public virtual string Format(CodeGenerationParameter parameter)
        {
            return Format(parameter, MethodScopeType.Instance);
        }

        public virtual string Format(CodeGenerationParameter parameter, MethodScopeType scope) => throw new NotImplementedException();

        public class SignatureDeclaration : AggregateArgumentsFormat
        {
            private static readonly string SignaturePatttern = "final {0} {1}";
            private static readonly string StateArgument = string.Format(StateArgument!, "Stage", "stage");

            public override string Format(CodeGenerationParameter parameter, MethodScopeType scope)
            {
                var args = scope == MethodScopeType.Static ? new List<string> { StateArgument } : new List<string>();
                return string.Join(", ", new List<List<string>>() { args, FormatMethodParameters(parameter) }.SelectMany(x => x));
            }

            private List<string> FormatMethodParameters(CodeGenerationParameter parameter)
            {
                return parameter.RetrieveAllRelated(ResolveFieldsLabel(parameter)).Select(param =>
                {
                    var paramType = FieldDetail.TypeOf(param.Parent(Label.Aggregate), param.Value);
                    return string.Format(SignaturePatttern, paramType, param.Value);
                }).ToList();
            }

            private Label ResolveFieldsLabel(CodeGenerationParameter parameter) => parameter.IsLabeled(Label.Aggregate) ? Label.StateField : Label.MethodParameter;
        }

        public class MethodInvocation : AggregateArgumentsFormat
        {
            private readonly string _carrier;
            private readonly string _stageVariableName;
            private static readonly string FiledAccessPattern = "{0}.{1}";

            public MethodInvocation(string stageVariableName) : this(stageVariableName, "")
            {
            }

            public MethodInvocation(string stageVariableName, string carrier)
            {
                _carrier = carrier;
                _stageVariableName = stageVariableName;
            }

            public override string Format(CodeGenerationParameter method, MethodScopeType scope)
            {
                var args = scope == MethodScopeType.Static ? new List<string>() { _stageVariableName } : new List<string>();
                return string.Join(", ", new List<List<string>>() { args, FormatMethodParameters(method) }.SelectMany(x => x));
            }

            private List<string> FormatMethodParameters(CodeGenerationParameter method) => method.RetrieveAllRelated(Label.MethodParameter).Select(param => string.IsNullOrEmpty(_carrier) ? param.Value : string.Format(FiledAccessPattern, _carrier, param.Value)).ToList();
        }
    }
}
