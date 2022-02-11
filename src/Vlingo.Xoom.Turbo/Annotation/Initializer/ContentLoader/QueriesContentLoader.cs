// Copyright © 2012-2022 VLINGO LABS. All rights reserved.
//
// This Source Code Form is subject to the terms of the
// Mozilla Public License, v. 2.0. If a copy of the MPL
// was not distributed with this file, You can obtain
// one at https://mozilla.org/MPL/2.0/.

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Reflection;
using Vlingo.Xoom.Turbo.Annotation.Persistence;
using Vlingo.Xoom.Turbo.Codegen;
using Vlingo.Xoom.Turbo.Codegen.Template;

namespace Vlingo.Xoom.Turbo.Annotation.Initializer.ContentLoader;

public class QueriesContentLoader : AnnotationBasedContentLoader<Dictionary<Type, Type>>
{
    public QueriesContentLoader(Type annotatedClass, ProcessingEnvironment environment) : base(annotatedClass,
        environment)
    {
    }

    public override void Load(CodeGenerationContext context)
    {
        foreach (var entry in RetrieveContentSource().ToImmutableHashSet())
        {
            context.AddContent(new TemplateStandard(TemplateStandardType.QueriesActor), entry.Key, entry.Value);
        }
    }

    protected override Dictionary<Type, Type> RetrieveContentSource()
    {
        var queries = AnnotatedClass?.GetCustomAttribute<EnableQueriesAttribute>();

        if (queries == null)
            return new Dictionary<Type, Type>();

        return new[] { queries.Value }.SelectMany(queriesEntry =>
        {
            var protocolType = TypeRetriever.From<Type>((queriesEntry as Attribute[])!,
                entry => (entry as QueriesEntryAttribute)!.Protocol!);
            var actorType = TypeRetriever.From<Type>((queriesEntry as Attribute[])!,
                entry => (entry as QueriesEntryAttribute)!.Actor!);
            return new Dictionary<Type, Type> { { protocolType, actorType } };
        }).ToDictionary(entry => entry.Key, entry => entry.Value);
    }
}