// Copyright © 2012-2021 VLINGO LABS. All rights reserved.
//
// This Source Code Form is subject to the terms of the
// Mozilla Public License, v. 2.0. If a copy of the MPL
// was not distributed with this file, You can obtain
// one at https://mozilla.org/MPL/2.0/.

using System.Linq;

namespace Vlingo.Xoom.Codegen.Content
{
    public class ContentCreationStep : ICodeGenerationStep
    {
        public void Process(CodeGenerationContext context)
        {
            context.Contents().Where(x => x.CanWrite()).ToList();//forEach(ContentBase::create);
        }

        public bool ShouldProcess(CodeGenerationContext context) => true;
    }
}
