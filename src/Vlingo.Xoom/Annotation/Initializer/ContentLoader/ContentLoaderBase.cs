// Copyright © 2012-2020 VLINGO LABS. All rights reserved.
//
// This Source Code Form is subject to the terms of the
// Mozilla Public License, v. 2.0. If a copy of the MPL
// was not distributed with this file, You can obtain
// one at https://mozilla.org/MPL/2.0/.

using System;
using Vlingo.Xoom.Codegen;

namespace Vlingo.Xoom.Annotation.Initializer.ContentLoader
{
    public abstract class ContentLoaderBase<T>
    {
        protected readonly Type _annotatedClass;
        protected readonly TypeRetriever _typeRetriever;
        protected readonly ProcessingEnvironment _environment;

        protected ContentLoaderBase(Type annotatedClass, ProcessingEnvironment environment)
        {
            _environment = environment;
            _annotatedClass = annotatedClass;
            _typeRetriever = TypeRetriever.With(environment);
        }

        public abstract void Load(CodeGenerationContext context);

        public bool ShouldLoad => _annotatedClass != null;

        protected abstract T RetrieveContentSource();
    }
}
