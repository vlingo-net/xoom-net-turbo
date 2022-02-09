// Copyright © 2012-2022 VLINGO LABS. All rights reserved.
//
// This Source Code Form is subject to the terms of the
// Mozilla Public License, v. 2.0. If a copy of the MPL
// was not distributed with this file, You can obtain
// one at https://mozilla.org/MPL/2.0/.

using System;
using System.Linq;

namespace Vlingo.Xoom.Turbo.Annotation.Initializer
{
    public class XoomInitializerPackage
    {
        public static string From(ProcessingEnvironment environment, AnnotatedElements annotatedElements)
        {
            var typesWithMyAttribute =
                from a in AppDomain.CurrentDomain.GetAssemblies()
                from t in a.GetTypes()
                let attributes = t.GetCustomAttributes(typeof(XoomAttribute), true)
                where attributes != null && attributes.Length > 0
                select new { Type = t, Attributes = attributes.Cast<XoomAttribute>() };
            return typesWithMyAttribute.FirstOrDefault()!.Type.Namespace!;
        }
    }
}