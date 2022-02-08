// Copyright © 2012-2021 VLINGO LABS. All rights reserved.
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

namespace Vlingo.Xoom.Turbo.Annotation.Initializer.ContentLoader
{
    public class AdapterEntriesContentLoader : TypeBasedContentLoader
    {
        public AdapterEntriesContentLoader(Type annotatedClass, ProcessingEnvironment environment) : base(
            annotatedClass, environment)
        {
        }

        protected override TemplateStandard Standard()
        {
            var persistence = AnnotatedClass?.GetCustomAttribute<PersistenceAttribute>();

            if (persistence!.IsJournal())
            {
                return new TemplateStandard(TemplateStandardType.DomainEvent);
            }

            return new TemplateStandard(TemplateStandardType.AggregateState);
        }

        protected override List<Type> RetrieveContentSource()
        {
            var adapters = AnnotatedClass?.GetCustomAttribute<AdaptersAttribute>();

            if (adapters == null)
            {
                return new List<Type>();
            }

            return TypeRetriever.TypesFrom(new List<Type> { adapters.GetType() }, _ => adapters.Value!);
        }
    }
}