// Copyright © 2012-2021 VLINGO LABS. All rights reserved.
//
// This Source Code Form is subject to the terms of the
// Mozilla Public License, v. 2.0. If a copy of the MPL
// was not distributed with this file, You can obtain
// one at https://mozilla.org/MPL/2.0/.

using System;
using Vlingo.Xoom.Turbo.Codegen.Template;

namespace Vlingo.Xoom.Turbo.Codegen.Content
{
    public class ProtocolBasedContent : TypeBasedContent
    {
        public readonly Type contentProtocolType;

        public ProtocolBasedContent(TemplateStandard standard, Type contentProtocolType, Type contentType) : base(standard, contentType)
        {
            this.contentProtocolType = contentProtocolType;
        }

        public override string RetrieveProtocolQualifiedName() => contentProtocolType.FullName;

        public override bool IsProtocolBased => true;
    }
}
