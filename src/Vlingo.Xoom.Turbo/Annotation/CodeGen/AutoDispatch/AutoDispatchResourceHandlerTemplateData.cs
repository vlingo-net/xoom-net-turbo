// Copyright © 2012-2021 VLINGO LABS. All rights reserved.
//
// This Source Code Form is subject to the terms of the
// Mozilla Public License, v. 2.0. If a copy of the MPL
// was not distributed with this file, You can obtain
// one at https://mozilla.org/MPL/2.0/.

using System.Collections.Generic;
using System.Linq;
using Vlingo.Xoom.Turbo.Annotation.Codegen;
using Vlingo.Xoom.Turbo.Annotation.Codegen.Storage;
using Vlingo.Xoom.Turbo.Annotation.Persistence;
using Vlingo.Xoom.Turbo.Codegen;
using Vlingo.Xoom.Turbo.Codegen.Content;
using Vlingo.Xoom.Turbo.Codegen.Parameter;
using Vlingo.Xoom.Turbo.Codegen.Template;
using Vlingo.Xoom.Turbo.Codegen.Template.Resource;

namespace Vlingo.Xoom.Turbo.Annotation.Codegen.AutoDispatch
{
	public class AutoDispatchResourceHandlerTemplateData : TemplateData
	{
		private readonly string _restResourceName;
		private readonly TemplateParameters _parameters;

		public static List<TemplateData> From(CodeGenerationContext context) => context.ParametersOf(Label.AutoDispatchName)
			.Select(param => (TemplateData)new AutoDispatchResourceHandlerTemplateData(context, @param)).ToList();

		private AutoDispatchResourceHandlerTemplateData(CodeGenerationContext context,
			CodeGenerationParameter autoDispatchParameter)
		{
			_restResourceName = ClassFormatter.SimpleNameOf(autoDispatchParameter.Value);

			var queryStoreProviderParameters = TemplateParameters.With(TemplateParameter.StorageType, StorageType.StateStore)
				.And(TemplateParameter.Model, ModelType.Query);
			
			var queryStoreProviderName = AnnotationBasedTemplateStandard.StoreProvider
					.ResolveClassname(queryStoreProviderParameters);
			
			var aggregateProtocolClassName = ClassFormatter
					.SimpleNameOf(autoDispatchParameter
						.RetrieveRelatedValue(Label.ModelProtocol));
			
			_parameters = TemplateParameters
				.With(TemplateParameter.PackageName, ClassFormatter.PackageOf(autoDispatchParameter.Value))
				.And(TemplateParameter.StateName, AnnotationBasedTemplateStandard.AggregateState.ResolveClassname(aggregateProtocolClassName))
				.And(TemplateParameter.Queries, Queries.From(autoDispatchParameter))
				.And(TemplateParameter.RestResourceName, Standard().ResolveClassname(_restResourceName))
				.And(TemplateParameter.UriRoot, autoDispatchParameter.RetrieveRelatedValue(Label.UriRoot))
				.And(TemplateParameter.RouteDeclarations, RouteDeclaration.From(autoDispatchParameter))
				.And(TemplateParameter.ModelProtocol, autoDispatchParameter.RetrieveRelatedValue(Label.ModelProtocol))
				.And(TemplateParameter.ModelActor, autoDispatchParameter.RetrieveRelatedValue(Label.ModelActor))
				.And(TemplateParameter.HandlersConfigName, autoDispatchParameter.RetrieveRelatedValue(Label.HandlersConfigName))
				.And(TemplateParameter.StoreProviderName, queryStoreProviderName)
				.And(TemplateParameter.RouteMethod, new List<string>())
				.And(TemplateParameter.AutoDispatchMappingName, _restResourceName).And(TemplateParameter.UseAutoDispatch, true)
				.And(TemplateParameter.DataObjectName, new TemplateStandard(TemplateStandardType.DataObject).ResolveClassname(aggregateProtocolClassName))
				.And(TemplateParameter.UseCqrs, context.ParameterOf(Label.Cqrs, x => bool.TrueString.ToLower() == x))
				.AddImports(ResolveImports(context, autoDispatchParameter, queryStoreProviderName));

			DependOn(RouteMethodTemplateData.From(autoDispatchParameter, _parameters));
		}

		private HashSet<string> ResolveImports(CodeGenerationContext context, CodeGenerationParameter autoDispatchParameter,
			string queryStoreProviderName)
		{
			var queryStoreProviderQualifiedName = ContentQuery.FindFullyQualifiedClassName(new TemplateStandard(TemplateStandardType.StoreProvider), queryStoreProviderName, context.Contents().ToList());
			var queriesProtocolQualifiedName = autoDispatchParameter.RetrieveRelatedValue(Label.QueriesProtocol);

			return new HashSet<string>(new List<string>() { queriesProtocolQualifiedName, queryStoreProviderQualifiedName });
		}

		public override void HandleDependencyOutcome(TemplateStandard standard, string outcome) =>
			_parameters.Find<List<string>>(TemplateParameter.RouteMethods).Add(outcome);

		public override TemplateParameters Parameters() => _parameters;


		public override TemplateStandard Standard() => AnnotationBasedTemplateStandard.AutoDispatchResourceHandler;

		public override string Filename() => Standard().ResolveFilename(_restResourceName, _parameters);
	}
}