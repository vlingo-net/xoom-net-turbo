// Copyright © 2012-2021 VLINGO LABS. All rights reserved.
//
// This Source Code Form is subject to the terms of the
// Mozilla Public License, v. 2.0. If a copy of the MPL
// was not distributed with this file, You can obtain
// one at https://mozilla.org/MPL/2.0/.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Vlingo.Xoom.Turbo.Annotation
{
	public class TypeRetriever
	{
		private readonly AppDomain _appDomain;

		private TypeRetriever(AppDomain appDomain) => _appDomain = appDomain;

		public static TypeRetriever With(AppDomain appDomain) => new TypeRetriever(appDomain);

		public IEnumerable<Type> SubClassesOf<T>(IEnumerable<string> assemblies) =>
			assemblies
				.Where(IsValidPackage)
				.Select(p => _appDomain.GetAssemblies().SingleOrDefault(a => a.GetName().Name == p || a.FullName == p))
				.SelectMany(a => a?.GetTypes())
				.Where(t => IsSubclass(t, typeof(T)));

		public Type? From(Attribute attribute, Func<object, Type> retriever)
		{
			var clazz = retriever.Invoke(attribute);
			return _appDomain
				.GetAssemblies()
				.SelectMany(a => a.GetTypes())
				.SingleOrDefault(t => t.AssemblyQualifiedName == clazz.AssemblyQualifiedName);
		}

		public Type? GetTypeElement(Attribute attribute, Func<object, Type> retriever) => From(attribute, retriever);
		
		public IEnumerable<Type> TypesFrom<T>(T attribute, Func<T, Type[]> retriever) where T : Attribute
		{
			var types = retriever(attribute);
			return types
				.SelectMany(t => _appDomain.GetAssemblies().Select(a =>
					a.GetTypes().SingleOrDefault(at => at.AssemblyQualifiedName == t.AssemblyQualifiedName)));
		}
		
		public bool IsAnInterface(Attribute attribute, Func<object, Type> retriever) =>
			GetTypeElement(attribute, retriever)?.IsInterface ?? false;
		
		public string? TypeName(Attribute attribute, Func<object, Type> retriever) => 
			GetTypeElement(attribute, retriever)?.AssemblyQualifiedName;
		
		public IEnumerable<MethodInfo>? GetMethods(Attribute attribute, Func<object, Type> retriever) => 
			GetTypeElement(attribute, retriever)?.GetMethods();
		
		public Type? GetGenericType(Attribute attribute, Func<object, Type> retriever)
		{
			var declaredType = GetTypeElement(attribute, retriever)?.BaseType;
			if (declaredType?.GenericTypeArguments.Length == 0)
			{
				return null;
			}
			
			return declaredType?.GenericTypeArguments[0];
		}
		
		public IEnumerable<Type> GetElements(Attribute attribute, Func<object, Type> retriever) => 
			GetTypeElement(attribute, retriever)?.Assembly.GetTypes() ?? Enumerable.Empty<Type>();

		public bool IsValidPackage(string assemblyName) => 
			_appDomain
				.GetAssemblies()
				.SingleOrDefault(a => a.GetName().Name == assemblyName || a.FullName == assemblyName) != null;

		private bool IsSubclass(Type typeElement, Type superclass) => superclass.IsAssignableFrom(typeElement);
	}
}