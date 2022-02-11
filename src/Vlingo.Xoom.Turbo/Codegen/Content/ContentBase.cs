// Copyright © 2012-2022 VLINGO LABS. All rights reserved.
//
// This Source Code Form is subject to the terms of the
// Mozilla Public License, v. 2.0. If a copy of the MPL
// was not distributed with this file, You can obtain
// one at https://mozilla.org/MPL/2.0/.

using System;
using System.IO;
using Vlingo.Xoom.Turbo.Codegen.Template;

namespace Vlingo.Xoom.Turbo.Codegen.Content;

public abstract class ContentBase
{
    public readonly TemplateStandard Standard;

    protected ContentBase(TemplateStandard standard) => Standard = standard;

    public static ContentBase With(TemplateStandard standard, TemplateFile templatefile, FileStream filer, Type source, string text) => new TextBasedContent(standard, templatefile, source, filer, text);

    public static ContentBase With(TemplateStandard standard, Type type) => new TypeBasedContent(standard, type);

    public static ContentBase With(TemplateStandard standard, Type protocolType, Type actorType) => new ProtocolBasedContent(standard, protocolType, actorType);

    public abstract void Create();

    public abstract string RetrieveClassName();

    public abstract string RetrievePackage();

    public abstract string RetrieveQualifiedName();

    public abstract bool CanWrite();

    public abstract bool Contains(string term);

    public virtual string RetrieveProtocolQualifiedName() => throw new NotSupportedException("Content does not have a protocol by default");

    public virtual bool IsProtocolBased => false;

    public bool Has(TemplateStandard standard) => standard.Equals(standard);

    public static ContentBase With(TemplateStandard standard, OutputFile file, FileStream filer, Type source, string text) => new TextBasedContent(standard, file, source, filer, text);

    public bool IsNamed(string name) => RetrieveClassName().Equals(name);
}