// Copyright © 2012-2021 VLINGO LABS. All rights reserved.
//
// This Source Code Form is subject to the terms of the
// Mozilla Public License, v. 2.0. If a copy of the MPL
// was not distributed with this file, You can obtain
// one at https://mozilla.org/MPL/2.0/.

using Vlingo.Xoom.Turbo.Annotation.Codegen.Projections;
using Vlingo.Xoom.Turbo.Annotation.Codegen.Storage;
using Vlingo.Xoom.Turbo.Codegen;
using Vlingo.Xoom.Turbo.Codegen.Content;
using Vlingo.Xoom.Turbo.Codegen.Parameter;
using Vlingo.Xoom.Turbo.Codegen.Template;

namespace Vlingo.Xoom.Turbo.Annotation.Codegen.Initializer
{
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
				StorageType.TryParse(x, out StorageType value);
				return value;
			});
			var projectionType = context.ParameterOf(Label.ProjectionType, x =>
			{
				ProjectionType.TryParse(x, out ProjectionType value);
				return value;
			});
			var blockingMessaging = context.ParameterOf(Label.BlockingMessaging, x => bool.TrueString.ToLower() == x);
			var customInitialization =
				!xoomInitializerClass.Equals(AnnotationBasedTemplateStandard.XoomInitializer.ResolveClassname());

			_parameters = TemplateParameters.With(TemplateParameter.BlockingMessaging, blockingMessaging)
					.And(TemplateParameter.ApplicationName, context.ParameterOf<string>(Label.ApplicationName))
					.And(TemplateParameter.Providers,
						StoreProvider.From(storageType, useCqrs, projectionType.IsProjectionEnabled(), hasExchange))
					.And(TemplateParameter.TypeRegistries, TypeRegistry.From(storageType, useCqrs))
					.And(TemplateParameter.RestResources, RestResource.From(contents))
				;
		}

		public override TemplateParameters Parameters() => _parameters;

		public override TemplateStandard Standard() => AnnotationBasedTemplateStandard.XoomInitializer;
	}
}