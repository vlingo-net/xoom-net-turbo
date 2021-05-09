// Copyright © 2012-2021 VLINGO LABS. All rights reserved.
//
// This Source Code Form is subject to the terms of the
// Mozilla Public License, v. 2.0. If a copy of the MPL
// was not distributed with this file, You can obtain
// one at https://mozilla.org/MPL/2.0/.

using System;
using Vlingo.Xoom.Codegen.Parameter;
using Vlingo.Xoom.Codegen.Template;

namespace Vlingo.Xoom.Codegen.File
{
    public class FileLocationResolver
    {
        public static string From(CodeGenerationContext context, TemplateData templateData)
        {
            var location = context.ParameterOf<CodeGenerationLocationType>(Label.GenerationLocation);
            if (CodeGenerationLocation.IsInternal(location))
            {
                throw new NotSupportedException("Unable to resolve internal file location");
            }

            return new ExternalFileLocationResolver().Resolve(context, templateData);
        }
    }
}
