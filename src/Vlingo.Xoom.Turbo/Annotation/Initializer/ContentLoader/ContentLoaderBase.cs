// Copyright © 2012-2023 VLINGO LABS. All rights reserved.
//
// This Source Code Form is subject to the terms of the
// Mozilla Public License, v. 2.0. If a copy of the MPL
// was not distributed with this file, You can obtain
// one at https://mozilla.org/MPL/2.0/.

using System;
using Vlingo.Xoom.Turbo.Codegen;

namespace Vlingo.Xoom.Turbo.Annotation.Initializer.ContentLoader;

public abstract class ContentLoaderBase<T>
{
    protected readonly Type AnnotatedClass;
    protected readonly TypeRetriever TypeRetriever;
    protected readonly ProcessingEnvironment Environment;

    protected ContentLoaderBase(Type annotatedClass, ProcessingEnvironment environment)
    {
        Environment = environment;
        AnnotatedClass = annotatedClass;
        TypeRetriever = TypeRetriever.With(environment);
    }

    public abstract void Load(CodeGenerationContext context);

    public bool ShouldLoad => AnnotatedClass != null;

    protected abstract T RetrieveContentSource();
}