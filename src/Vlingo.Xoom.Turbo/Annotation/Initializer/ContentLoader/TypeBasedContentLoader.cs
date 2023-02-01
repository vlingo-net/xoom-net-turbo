// Copyright © 2012-2023 VLINGO LABS. All rights reserved.
//
// This Source Code Form is subject to the terms of the
// Mozilla Public License, v. 2.0. If a copy of the MPL
// was not distributed with this file, You can obtain
// one at https://mozilla.org/MPL/2.0/.

using System;
using System.Collections.Generic;
using Vlingo.Xoom.Turbo.Codegen;
using Vlingo.Xoom.Turbo.Codegen.Template;

namespace Vlingo.Xoom.Turbo.Annotation.Initializer.ContentLoader;

public abstract class TypeBasedContentLoader : AnnotationBasedContentLoader<List<Type>>
{
    public TypeBasedContentLoader(Type annotatedClass, ProcessingEnvironment environment) : base(annotatedClass,
        environment)
    {
    }

    public override void Load(CodeGenerationContext context) => RetrieveContentSource()
        .ForEach(typeElement => context.AddContent(Standard(), typeElement));

    protected abstract TemplateStandard Standard();

    protected abstract override List<Type> RetrieveContentSource();
}