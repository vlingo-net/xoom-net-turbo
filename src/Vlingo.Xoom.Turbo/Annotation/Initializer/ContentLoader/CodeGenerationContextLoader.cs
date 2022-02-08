// Copyright © 2012-2021 VLINGO LABS. All rights reserved.
//
// This Source Code Form is subject to the terms of the
// Mozilla Public License, v. 2.0. If a copy of the MPL
// was not distributed with this file, You can obtain
// one at https://mozilla.org/MPL/2.0/.

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Vlingo.Xoom.Turbo.Annotation.AutoDispatch;
using Vlingo.Xoom.Turbo.Annotation.Persistence;
using Vlingo.Xoom.Turbo.Codegen;
using Vlingo.Xoom.Turbo.Codegen.Content;

namespace Vlingo.Xoom.Turbo.Annotation.Initializer.ContentLoader
{
	public class CodeGenerationContextLoader
	{
		private readonly FileStream _filer;
		private readonly string _basePackage;
		private readonly ProcessingEnvironment _environment;
		private readonly Type? _bootStrapClass;
		private readonly Type _persistenceSetupClass;
		private readonly Type _autoDispatchResourceClasses;

		private CodeGenerationContextLoader(
			FileStream filer,
			string basePackage,
			AnnotatedElements elements,
			ProcessingEnvironment environment)
		{
			_filer = filer;
			_basePackage = basePackage;
			_environment = environment;
			_bootStrapClass = ElementWith<XoomAttribute>();
			_persistenceSetupClass = ElementWith<PersistenceAttribute>();
			_autoDispatchResourceClasses = ElementWith<AutoDispatchAttribute>();
		}

		private Type ElementWith<T>()
		{
			var typesWithMyAttribute =
				from a in AppDomain.CurrentDomain.GetAssemblies()
				from t in a.GetTypes()
				let attributes = t.GetCustomAttributes(typeof(T), true)
				where attributes != null && attributes.Length > 0
				select new { Type = t, Attributes = attributes.Cast<T>() };
			return typesWithMyAttribute.FirstOrDefault()!.Type;
		}

		public static CodeGenerationContext From(
			FileStream filer,
			string basePackage,
			AnnotatedElements elements,
			ProcessingEnvironment environment) =>
			new CodeGenerationContextLoader(filer, basePackage, elements, environment).Load();

		private CodeGenerationContext Load() => CodeGenerationContext
			.Using(_filer, _bootStrapClass!)
			.On(XoomInitializerParameterResolver.From(_basePackage, _bootStrapClass!, _environment).Resolve())
			.On(AutoDispatchParameterResolver.From(_autoDispatchResourceClasses, _environment).Resolve())
			.On(PersistenceParameterResolver.From(_persistenceSetupClass, _environment).Resolve())
			.Contents(ResolveContentLoaders());

		private List<IContentLoader> ResolveContentLoaders()
		{
			if (_bootStrapClass == null)
				return new List<IContentLoader>();
			return new List<IContentLoader>
			{
				new ProjectionContentLoader(_persistenceSetupClass, _environment),
				new AdapterEntriesContentLoader(_persistenceSetupClass, _environment),
				new DataObjectContentLoader(_persistenceSetupClass, _environment),
				new QueriesContentLoader(_persistenceSetupClass, _environment),
				new AggregateContentLoader(_persistenceSetupClass, _environment),
				new RestResourceContentLoader(_bootStrapClass, _environment),
				new ExchangeBootstrapLoader(_persistenceSetupClass, _environment)
			};
		}
	}
}