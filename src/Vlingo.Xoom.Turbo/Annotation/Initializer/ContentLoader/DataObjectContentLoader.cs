// Copyright © 2012-2022 VLINGO LABS. All rights reserved.
//
// This Source Code Form is subject to the terms of the
// Mozilla Public License, v. 2.0. If a copy of the MPL
// was not distributed with this file, You can obtain
// one at https://mozilla.org/MPL/2.0/.

using System;
using System.Collections.Generic;
using System.Reflection;
using Vlingo.Xoom.Turbo.Annotation.Persistence;
using Vlingo.Xoom.Turbo.Codegen.Template;

namespace Vlingo.Xoom.Turbo.Annotation.Initializer.ContentLoader;

public class DataObjectContentLoader : TypeBasedContentLoader
{
    public DataObjectContentLoader(Type annotatedClass, ProcessingEnvironment environment) : base(annotatedClass,
        environment)
    {
    }

    protected override TemplateStandard Standard() => new TemplateStandard(TemplateStandardType.DataObject);

    protected override List<Type> RetrieveContentSource()
    {
        var dataObjects = AnnotatedClass?.GetCustomAttribute<DataObjectAttribute>();
        if (dataObjects == null)
            return new List<Type>();

        return TypeRetriever.TypesFrom(new List<Type> { dataObjects.GetType() }, _ => dataObjects.Value!);
    }
}