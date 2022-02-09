// Copyright © 2012-2022 VLINGO LABS. All rights reserved.
//
// This Source Code Form is subject to the terms of the
// Mozilla Public License, v. 2.0. If a copy of the MPL
// was not distributed with this file, You can obtain
// one at https://mozilla.org/MPL/2.0/.

using System;
using Vlingo.Xoom.Turbo.Codegen.Template;

namespace Vlingo.Xoom.Turbo.Codegen.Content
{
    public class TypeBasedContent : ContentBase
    {

        public readonly Type ContentType;

        public TypeBasedContent(TemplateStandard standard, Type contentType) : base(standard) => ContentType = contentType;

        public override void Create() => throw new NotSupportedException("Type Based Content is read-only");

        public override string RetrieveClassName() => ContentType.Name;

        public override string RetrievePackage() => ClassFormatter.PackageOf(RetrieveQualifiedName());

        public override string RetrieveQualifiedName() => ContentType.FullName!;

        public override bool CanWrite() => false;

        public override bool Contains(string term) => throw new NotSupportedException("Unable to search on TypeBasedContent");
    }
}
