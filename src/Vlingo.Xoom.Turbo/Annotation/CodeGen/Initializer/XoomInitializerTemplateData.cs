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
using Vlingo.Xoom.Turbo.Annotation.Codegen.Projections;
using Vlingo.Xoom.Turbo.Annotation.Codegen.Storage;
using Vlingo.Xoom.Turbo.Codegen;
using Vlingo.Xoom.Turbo.Codegen.Content;
using Vlingo.Xoom.Turbo.Codegen.Parameter;
using Vlingo.Xoom.Turbo.Codegen.Template;

namespace Vlingo.Xoom.Turbo.Annotation.Codegen.Initializer;

public class XoomInitializerTemplateData : TemplateData
{
	private readonly TemplateParameters _parameters;

	public XoomInitializerTemplateData(CodeGenerationContext context)
	{
		var contents = context.Contents();
		var packageName = context.ParametersOf(Label.Package);
		var useCqrs = context.ParameterOf(Label.Cqrs, x => bool.TrueString.ToLower() == x);
		var xoomInitializerClass = context.ParameterOf<string>(Label.XoomInitializerName);
		var hasExchange = ContentQuery.Exists(new TemplateStandard(TemplateStandardType.ExchangeBootstrap), contents);
		var storageType = context.ParameterOf(Label.StorageType, x =>
		{
			Enum.TryParse(x, out StorageType value);
			return value;
		});
		var projectionType = context.ParameterOf(Label.ProjectionType, x =>
		{
			Enum.TryParse(x, out ProjectionType value);
			return value;
		});
		var blockingMessaging = context.ParameterOf(Label.BlockingMessaging, x => bool.TrueString.ToLower() == x);
		var customInitialization =
			!xoomInitializerClass.Equals(AnnotationBasedTemplateStandard.XoomInitializer.ResolveClassname());

		_parameters = TemplateParameters.With(TemplateParameter.BlockingMessaging, blockingMessaging)
				.And(TemplateParameter.ApplicationName, context.ParameterOf<string>(Label.ApplicationName))
				.And(TemplateParameter.Providers,
					StoreProvider.From(storageType, useCqrs, projectionType.IsProjectionEnabled(), hasExchange))
				.And(TemplateParameter.UseAnnotations,
					context.ParameterOf(Label.UseAnnotations, x => bool.TrueString.ToLower() == x))
				.AndResolve(TemplateParameter.ProjectionDispatcherProviderName,
					AnnotationBasedTemplateStandard.ProjectionDispatcherProvider.ResolveClassname)
				.And(TemplateParameter.XoomInitializerClass, context.ParameterOf<string>(Label.XoomInitializerName))
				.And(TemplateParameter.ExchangeBootstrapName, ResolveExchangeBootstrapName(context))
				.And(TemplateParameter.TypeRegistries, TypeRegistry.From(storageType, useCqrs))
				.And(TemplateParameter.CustomInitialization, customInitialization)
				.And(TemplateParameter.RestResources, RestResource.From(contents))
				.AddImports(ResolveImports(context))
			;
	}

	private string ResolveExchangeBootstrapName(CodeGenerationContext context)
	{
		var exchangeBootstrapQualifiedName =
			ContentQuery
				.FindFullyQualifiedClassNames(new TemplateStandard(TemplateStandardType.ExchangeBootstrap),
					context.Contents())
				.FirstOrDefault();

		if (exchangeBootstrapQualifiedName == null)
		{
			return null!;
		}

		return exchangeBootstrapQualifiedName;
	}

	private ISet<string> ResolveImports(CodeGenerationContext context)
	{
		var useCqrs =
			context.ParameterOf(Label.Cqrs, x => bool.TrueString.ToLower() == x);

		var storageType =
			context.ParameterOf(Label.StorageType, x =>
			{
				Enum.TryParse(x, out StorageType value);
				return value;
			});

		var dependencies = new[]
		{
			AnnotationBasedTemplateStandard.StoreProvider, AnnotationBasedTemplateStandard.ProjectionDispatcherProvider,
			new TemplateStandard(TemplateStandardType.RestResource),
			AnnotationBasedTemplateStandard.AutoDispatchResourceHandler,
			new TemplateStandard(TemplateStandardType.ExchangeBootstrap)
		};

		return new[]
			{
				ContentQuery.FindFullyQualifiedClassNames(context.Contents(), dependencies),
				storageType.ResolveTypeRegistryQualifiedNames(useCqrs)
			}.SelectMany(s => s.ToList())
			.ToImmutableHashSet();
	}

	public override TemplateParameters Parameters() => _parameters;

	public override TemplateStandard Standard() => AnnotationBasedTemplateStandard.XoomInitializer;
}