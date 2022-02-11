// Copyright © 2012-2022 VLINGO LABS. All rights reserved.
//
// This Source Code Form is subject to the terms of the
// Mozilla Public License, v. 2.0. If a copy of the MPL
// was not distributed with this file, You can obtain
// one at https://mozilla.org/MPL/2.0/.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Vlingo.Xoom.Turbo.Annotation.Persistence;
using Vlingo.Xoom.Turbo.Codegen.Template;
using Vlingo.Xoom.Turbo.Exchange;

namespace Vlingo.Xoom.Turbo.Annotation.Initializer.ContentLoader;

public class ExchangeBootstrapLoader : TypeBasedContentLoader
{
    public ExchangeBootstrapLoader(Type annotatedClass, ProcessingEnvironment environment) : base(annotatedClass,
        environment)
    {
    }

    protected override TemplateStandard Standard() => new TemplateStandard(TemplateStandardType.ExchangeBootstrap);

    protected override List<Type> RetrieveContentSource()
    {
        var persistence = AnnotatedClass?.GetCustomAttribute<PersistenceAttribute>();

        var baseDirectory = Context.LocateBaseDirectory(Environment.GetFiler());

        var allPackages = PackageCollector.From(baseDirectory, persistence!.BasePackage).CollectAll().ToArray();

        return TypeRetriever.SubClassesOf<ExchangeInitializer>(allPackages);
    }
}