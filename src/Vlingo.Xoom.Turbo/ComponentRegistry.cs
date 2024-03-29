﻿// Copyright © 2012-2023 VLINGO LABS. All rights reserved.
//
// This Source Code Form is subject to the terms of the
// Mozilla Public License, v. 2.0. If a copy of the MPL
// was not distributed with this file, You can obtain
// one at https://mozilla.org/MPL/2.0/.

using System.Collections.Generic;

namespace Vlingo.Xoom.Turbo;

public class ComponentRegistry
{
    private static readonly IDictionary<string, object> Components = new Dictionary<string, object>();

    public static void Register(string componentName, object componentInstance) => 
        Components.Add(componentName, componentInstance);

    public static void Register<T>(object componentInstance) => Components.Add(typeof(T).FullName!, componentInstance);

    public static bool Has<T>() => Components.ContainsKey(typeof(T).FullName!);

    public static object WithName(string name) => Components[name];

    public static T WithType<T>() => (T)WithName(typeof(T).FullName!);

    public static bool Has(string componentName) => Components.ContainsKey(componentName);

    public static void Clear() => Components.Clear();
}