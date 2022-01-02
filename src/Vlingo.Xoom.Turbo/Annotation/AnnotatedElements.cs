// Copyright © 2012-2021 VLINGO LABS. All rights reserved.
//
// This Source Code Form is subject to the terms of the
// Mozilla Public License, v. 2.0. If a copy of the MPL
// was not distributed with this file, You can obtain
// one at https://mozilla.org/MPL/2.0/.

using System;
using System.Collections.Generic;
using System.Linq;
using Vlingo.Xoom.Turbo.Annotation.Initializer;

namespace Vlingo.Xoom.Turbo.Annotation
{
    public class AnnotatedElements
    {
        private readonly IDictionary<Type, ISet<Type>> _elements = new Dictionary<Type, ISet<Type>>();
        
        public static AnnotatedElements From(AppDomain appDomain, IEnumerable<Type> supportedAnnotations)
        {
            Func<Type, KeyValuePair<Type, ISet<Type>>> mapper
                = annotationClass => new KeyValuePair<Type, ISet<Type>>(annotationClass,
                    new HashSet<Type>(appDomain.GetAssemblies()
                        .SelectMany(a => a.GetTypes()
                            .Where(t => t.GetCustomAttributes(typeof(XoomAttribute), true).Length > 0))));

            return new AnnotatedElements(supportedAnnotations.Select(mapper)
                .ToDictionary(k => k.Key, pair => pair.Value));
        }

        public bool Exists => _elements.Count > 0;

        public bool HasElementsWith(Type annotation) 
            => _elements.ContainsKey(annotation) && ElementsWith(annotation).Count > 0;

        public virtual ISet<Type> ElementsWith(params Type[] annotations)
            => new HashSet<Type>(annotations
                .Where(annotation => _elements.ContainsKey(annotation))
                .SelectMany(annotation => _elements[annotation]));

        public Type? ElementWith(Type annotation) => ElementsWith(annotation).FirstOrDefault();

        public virtual int Count(Type annotation) => HasElementsWith(annotation) ? 0 : ElementsWith(annotation).Count;
        
        private AnnotatedElements(IReadOnlyDictionary<Type, ISet<Type>> elements) 
            => elements.ToList().ForEach(element => _elements.Add(element));
    }
}
