// Copyright © 2012-2023 VLINGO LABS. All rights reserved.
//
// This Source Code Form is subject to the terms of the
// Mozilla Public License, v. 2.0. If a copy of the MPL
// was not distributed with this file, You can obtain
// one at https://mozilla.org/MPL/2.0/.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Vlingo.Xoom.Http.Resource;
using Vlingo.Xoom.Turbo.Codegen.Template;

namespace Vlingo.Xoom.Turbo.Annotation.Initializer.ContentLoader;

public class RestResourceContentLoader : TypeBasedContentLoader
{
    public RestResourceContentLoader(Type annotatedClass, ProcessingEnvironment environment) : base(annotatedClass,
        environment)
    {
    }

    protected override TemplateStandard Standard() => new TemplateStandard(TemplateStandardType.RestResource);

    protected override List<Type> RetrieveContentSource()
    {
        var resourceHandlers = AnnotatedClass?.GetCustomAttribute<ResourceHandlersAttribute>();

        if (ShouldIgnore(resourceHandlers!))
            return new List<Type>();
        if (IsPackageBased(resourceHandlers!))
        {
            return TypeRetriever.SubClassesOf<DynamicResourceHandler>(resourceHandlers?.Packages!).ToList();
        }

        return TypeRetriever.TypesFrom(new List<Type> { resourceHandlers!.GetType() },
            _ => resourceHandlers.Value!);
    }

    private bool ShouldIgnore(ResourceHandlersAttribute resourceHandlersAnnotation) =>
        resourceHandlersAnnotation.Value?.Length == 0 && !IsPackageBased(resourceHandlersAnnotation);

    private bool IsPackageBased(ResourceHandlersAttribute resourceHandlersAnnotation)
    {
        var packages = resourceHandlersAnnotation.Packages;
        return packages?.Length != 1 || !string.IsNullOrEmpty(packages[0]);
    }
}