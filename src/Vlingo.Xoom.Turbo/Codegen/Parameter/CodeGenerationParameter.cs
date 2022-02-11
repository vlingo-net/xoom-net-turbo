// Copyright © 2012-2022 VLINGO LABS. All rights reserved.
//
// This Source Code Form is subject to the terms of the
// Mozilla Public License, v. 2.0. If a copy of the MPL
// was not distributed with this file, You can obtain
// one at https://mozilla.org/MPL/2.0/.

using System;
using System.Collections.Generic;
using System.Linq;

namespace Vlingo.Xoom.Turbo.Codegen.Parameter;

public class CodeGenerationParameter
{
    public readonly Label Label;
    public readonly string Value;
    private CodeGenerationParameter? _parent;
    private readonly CodeGenerationParameters _relatedParameters;

    public static CodeGenerationParameter Of(Label label) => Of(label, label.ToString());

    public static CodeGenerationParameter Of(Label label, object value) => Of(label, value.ToString()!);

    public static CodeGenerationParameter Of(Label label, string value) => new CodeGenerationParameter(label, value);

    private CodeGenerationParameter(Label label, string value)
    {
        Label = label;
        Value = value;
        _relatedParameters = CodeGenerationParameters.Empty();
    }

    public CodeGenerationParameter Relate(Label label, object value) => Relate(label, value.ToString()!);

    public CodeGenerationParameter Relate(Label label, string value) => Relate(Of(label, value));

    public CodeGenerationParameter Relate(params CodeGenerationParameter[] relatedParameters)
    {
        relatedParameters.ToList().ForEach(relatedParameter =>
        {
            relatedParameter.OwnedBy(this);
            _relatedParameters.Add(relatedParameter);
        });
        return this;
    }

    public CodeGenerationParameter RetrieveOneRelated(Label label) => _relatedParameters.RetrieveOne(Label);

    public IEnumerable<CodeGenerationParameter> RetrieveAllRelated(Label label) => _relatedParameters.RetrieveAll(Label);

    public CodeGenerationParameter Parent() => _parent!;

    public CodeGenerationParameter Parent(Label label)
    {
        if (!HasParent())
        {
            throw new NotSupportedException("Orphan parameter");
        }

        var matchedParent = _parent!;

        while (matchedParent != null)
        {
            if (matchedParent.IsLabeled(Label))
            {
                return matchedParent;
            }
            matchedParent = matchedParent.Parent();
        }

        throw new NotSupportedException("Orphan parameter");
    }

    public bool HasParent() => _parent != null;

    private void OwnedBy(CodeGenerationParameter parent) => _parent = parent;

    public string RetrieveRelatedValue(Label label) => RetrieveRelatedValue(Label, value => value);

    public T RetrieveRelatedValue<T>(Label label, Func<string, T> mapper) => mapper(RetrieveOneRelated(Label).Value);

    public bool IsLabeled(Label label) => Label == label;

    public bool HasAny(Label label) => IsLabeled(Label) || _relatedParameters.List().Any(parameter => parameter.Value != null && parameter.IsLabeled(Label) && parameter.Value != string.Empty);
}