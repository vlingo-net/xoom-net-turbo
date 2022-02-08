// Copyright © 2012-2021 VLINGO LABS. All rights reserved.
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
    public class CodeGenerationParameter
    {
        public readonly Label label;
        public readonly string value;
        private CodeGenerationParameter? _parent;
        private readonly CodeGenerationParameters _relatedParameters;

        public static CodeGenerationParameter Of(Label label) => Of(label, label.ToString());

        public static CodeGenerationParameter Of(Label label, object value) => Of(label, value.ToString()!);

        public static CodeGenerationParameter Of(Label label, string value) => new CodeGenerationParameter(label, value);

        private CodeGenerationParameter(Label label, string value)
        {
            this.label = label;
            this.value = value;
            _relatedParameters = CodeGenerationParameters.Empty();
        }

        public CodeGenerationParameter Relate(Label label, object value)
        {
            return Relate(label, value.ToString()!);
        }

        public CodeGenerationParameter Relate(Label label, string value)
        {
            return Relate(Of(label, value));
        }

        public CodeGenerationParameter Relate(params CodeGenerationParameter[] relatedParameters)
        {
            relatedParameters.ToList().ForEach(relatedParameter =>
            {
                relatedParameter.OwnedBy(this);
                _relatedParameters.Add(relatedParameter);
            });
            return this;
        }

        public CodeGenerationParameter RetrieveOneRelated(Label label)
        {
            return _relatedParameters.RetrieveOne(this.label);
        }

        public IEnumerable<CodeGenerationParameter> RetrieveAllRelated(Label label)
        {
            return _relatedParameters.RetrieveAll(this.label);
        }

        public CodeGenerationParameter Parent() => _parent!;

        public CodeGenerationParameter Parent(Label label)
        {
            if (!HasParent())
            {
                throw new NotSupportedException("Orphan parameter");
            }

            CodeGenerationParameter matchedParent = _parent!;

            while (matchedParent != null)
            {
                if (matchedParent.IsLabeled(this.label))
                {
                    return matchedParent;
                }
                matchedParent = matchedParent.Parent();
            }

            throw new NotSupportedException("Orphan parameter");
        }

        public bool HasParent() => _parent != null;

        private void OwnedBy(CodeGenerationParameter parent) => _parent = parent;

        public string RetrieveRelatedValue(Label label) => RetrieveRelatedValue(this.label, value => value);

        public T RetrieveRelatedValue<T>(Label label, Func<string, T> mapper) => mapper(RetrieveOneRelated(this.label).value);

        public bool IsLabeled(Label label) => this.label == label;

        public bool HasAny(Label label) => IsLabeled(this.label) || _relatedParameters.List().Any(parameter => parameter.value != null && parameter.IsLabeled(this.label) && parameter.value != string.Empty);
    }
}
