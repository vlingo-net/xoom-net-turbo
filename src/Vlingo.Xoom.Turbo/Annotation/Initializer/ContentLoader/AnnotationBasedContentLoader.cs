// Copyright © 2012-2022 VLINGO LABS. All rights reserved.
//
// This Source Code Form is subject to the terms of the
// Mozilla Public License, v. 2.0. If a copy of the MPL
// was not distributed with this file, You can obtain
// one at https://mozilla.org/MPL/2.0/.

using System;
using Vlingo.Xoom.Turbo.Codegen;
using Vlingo.Xoom.Turbo.Codegen.Content;

namespace Vlingo.Xoom.Turbo.Annotation.Initializer.ContentLoader;

public abstract class AnnotationBasedContentLoader<T> : IContentLoader
{
    protected readonly Type? AnnotatedClass;
    protected readonly ProcessingEnvironment Environment;
    protected readonly TypeRetriever TypeRetriever;

    protected AnnotationBasedContentLoader(Type annotatedClass, ProcessingEnvironment environment)
    {
        AnnotatedClass = annotatedClass;
        Environment = environment;
        TypeRetriever = TypeRetriever.With(environment);
    }

    public abstract void Load(CodeGenerationContext context);

    public bool ShouldLoad() => AnnotatedClass != null;

    protected abstract T RetrieveContentSource();
}