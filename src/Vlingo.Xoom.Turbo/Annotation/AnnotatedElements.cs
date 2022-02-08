// Copyright © 2012-2021 VLINGO LABS. All rights reserved.
//
// This Source Code Form is subject to the terms of the
// Mozilla Public License, v. 2.0. If a copy of the MPL
// was not distributed with this file, You can obtain
// one at https://mozilla.org/MPL/2.0/.

using System;
using System.Collections.Generic;
using System.Linq;

namespace Vlingo.Xoom.Turbo.Annotation
{
    public class AnnotatedElements
    {
        private readonly IDictionary<object, HashSet<Type>> _elements = new Dictionary<object, HashSet<Type>>();

        public static AnnotatedElements From(IEnumerable<object> supportedAnnotations)
        {
            //Func<object, AbstractMap.SimpleEntry<Class, Set<Element>>> mapper =
            //        annotationClass->
            //                new AbstractMap.SimpleEntry<Class, Set<Element>>(
            //                    annotationClass, environment.getElementsAnnotatedWith(annotationClass));

            //return new AnnotatedElements(supportedAnnotations.map(mapper)
            //        .collect(Collectors.toMap(Map.Entry::getKey, Map.Entry::getValue)));
            return null!;
        }

        private AnnotatedElements(IReadOnlyDictionary<object, HashSet<Type>> elements) => elements.ToList().ForEach(element => _elements.Add(element));

        public bool Exists => _elements != null && _elements.Count != 0;

        public bool HasElementsWith(object annotation) => _elements.ContainsKey(annotation) && ElementsWith(annotation) != null && ElementsWith(annotation).Count != 0;

        public virtual HashSet<Type> ElementsWith(params object[] annotations) => new HashSet<Type>(annotations.Where(annotation => _elements.ContainsKey(annotation)).SelectMany(annotation => _elements[annotation]));

        public Type ElementWith(object annotation) => ElementsWith(annotation).FirstOrDefault();

        public virtual int Count(object annotation) => HasElementsWith(annotation) ? 0 : ElementsWith(annotation).Count;
    }
}
