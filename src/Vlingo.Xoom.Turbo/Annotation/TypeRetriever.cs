// Copyright © 2012-2022 VLINGO LABS. All rights reserved.
//
// This Source Code Form is subject to the terms of the
// Mozilla Public License, v. 2.0. If a copy of the MPL
// was not distributed with this file, You can obtain
// one at https://mozilla.org/MPL/2.0/.

using System;
using System.Collections.Generic;

namespace Vlingo.Xoom.Turbo.Annotation;

public class TypeRetriever
{
    //private static TypeRetriever _instance;
    private readonly Type _elements;
    private readonly ProcessingEnvironment _environment;

    private TypeRetriever(ProcessingEnvironment environment)
    {
        _environment = environment;
        _elements = environment.GetElementUtils();
    }

    public static TypeRetriever With(ProcessingEnvironment environment) => new TypeRetriever(environment);

    public bool IsValidPackage(string packageName) => _elements.Assembly.GetName().Name != null;

    public bool IsAnInterface(Attribute? attribute, Func<object, Type> retriever) =>
        GetTypeElement(attribute, retriever).IsInterface;

    private Type GetTypeElement(Attribute? attribute, Func<object, Type> retriever) =>
        From(attribute!.GetType(), retriever);

    public Type From(Attribute attribute, Func<object, Type> retriever)
    {
        var clazz = retriever.Invoke(attribute);
        return _environment.GetElementUtils().GetElementType()!;
    }

    public T From<T>(Attribute[] attribute, Func<object, Type> retriever) where T : Type
    {
        var clazz = retriever.Invoke(attribute);
        return (_environment.GetElementUtils().GetElementType() as T)!;
    }
    public T From<T>(T attribute, Func<object, Type> retriever) where T : Type
    {
        var clazz = retriever.Invoke(attribute);
        return (_environment.GetElementUtils().GetElementType() as T)!;
    }

    public T TypesFrom<T>(T attribute, Func<T, Type[]> retriever)
    {
        throw new NotImplementedException();
    }

    public List<Type> SubClassesOf<T>(string[] packages) => new List<Type>();
}