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

namespace Vlingo.Xoom.Codegen.Template
{
    public class TemplateParameters
    {
        private readonly IDictionary<string, object> _parameters = new Dictionary<string, object>();

        private TemplateParameters() { }

        public static TemplateParameters Empty() => new TemplateParameters();

        public static TemplateParameters With(TemplateParameter parameter, object value) => new TemplateParameters().And(parameter, value);

        public TemplateParameters And(TemplateParameter parameter, object value)
        {
            if (_parameters.ContainsKey(parameter.ToString()))
            {
                _parameters[parameter.ToString()] = value;
            }
            else
            {
                _parameters.Add(parameter.ToString(), value);
            }
            return this;
        }

        public TemplateParameters AndResolve(TemplateParameter parameter, Func<TemplateParameters, object> resolver)
        {
            if (_parameters.ContainsKey(parameter.ToString()))
            {
                _parameters[parameter.ToString()] = resolver(this);
            }
            else
            {
                _parameters.Add(parameter.ToString(), resolver(this));
            }
            return this;
        }

        public TemplateParameters AddImport(Type clazz) => AddImport(clazz.Name);

        public TemplateParameters AddImport(string qualifiedClassName)
        {
            if (Find<string>(TemplateParameter.Imports) == null)
            {
                And(TemplateParameter.Imports, new HashSet<ImportParameter>());
            }
            if (qualifiedClassName != null && qualifiedClassName.Trim() != string.Empty)
            {
                Find<HashSet<ImportParameter>>(TemplateParameter.Imports).Add(new ImportParameter((qualifiedClassName ?? string.Empty).Trim()));
            }
            return this;
        }

        public TemplateParameters AddImports(HashSet<string> qualifiedClassNames)
        {
            qualifiedClassNames.ToList().ForEach(x => AddImport(x));
            return this;
        }

        public bool HasImport(string qualifiedName) => Find<string>(TemplateParameter.Imports).Select(x => new ImportParameter(x.ToString())).Any(x => x.MatchClass(qualifiedName));

        public T Find<T>(TemplateParameter parameter) => (T)_parameters[parameter.ToString()];

        public T Find<T>(TemplateParameter parameter, T defaultValue)
        {
            if (!_parameters.ContainsKey(parameter.ToString()))
            {
                return defaultValue;
            }
            return (T)_parameters[parameter.ToString()];
        }

        public IReadOnlyDictionary<string, object>? Map() => _parameters as IReadOnlyDictionary<string, object>;

        public bool Has(TemplateParameter parameter) => _parameters.ContainsKey(parameter.ToString()) && _parameters[parameter.ToString()] != null && _parameters[parameter.ToString()].ToString().Trim() != string.Empty;

        public bool HasValue(TemplateParameter parameter, string value) => Has(parameter) && (string)_parameters[parameter.ToString()] == value;
    }
}
