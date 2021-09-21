// Copyright © 2012-2021 VLINGO LABS. All rights reserved.
//
// This Source Code Form is subject to the terms of the
// Mozilla Public License, v. 2.0. If a copy of the MPL
// was not distributed with this file, You can obtain
// one at https://mozilla.org/MPL/2.0/.

using System;
using System.Collections.Generic;
using Vlingo.Xoom.Turbo.Annotation.Codegen;
using Vlingo.Xoom.Turbo.Annotation.Codegen.AutoDispatch;
using Vlingo.Xoom.Turbo.Codegen;
using Vlingo.Xoom.Turbo.Codegen.Content;
using Vlingo.Xoom.Turbo.Codegen.Parameter;
using Vlingo.Xoom.Turbo.Codegen.Template;
using Xunit;
using static Vlingo.Xoom.Turbo.Codegen.Dialect.Dialect;

namespace Vlingo.Xoom.Turbo.Tests.Annotation.Codegen.AutoDispatch
{
	public class AutoDispatchResourceHandlerGenerationStepTest
	{
		public AutoDispatchResourceHandlerGenerationStepTest()
		{
			ComponentRegistry.Register(typeof(CodeElementFormatter), 
				CodeElementFormatter.With(C_SHARP));
		}
		
		[Fact(Skip = "WIP")]
		public void TestThatAutoDispatchResourceHandlersAreGenerated()
		{
			var context = CodeGenerationContext
				.With(LoadParameters())
				.AddContent(AnnotationBasedTemplateStandard.StoreProvider,
					new OutputFile(PERSISTENCE_PACKAGE_PATH, "QueryModelStateStoreProvider.cs"),
					QUERY_MODEL_STORE_PROVIDER_CONTENT);

			new AutoDispatchResourceHandlerGenerationStep().Process(context);
			
			Assert.Equal(3, context.Contents().Count);
		}

		private const string QUERY_MODEL_STORE_PROVIDER_CONTENT = "namespace Io.Vlingo.XoomApp.Infrastructure.Persistence " +
		                                                          "{" +
		                                                          "public class QueryModelStateStoreProvider " +
		                                                          "{" +
		                                                          "... " +
		                                                          "}" +
		                                                          "}";

		private const string PERSISTENCE_PACKAGE_PATH = "Io.Vlingo.XoomApp.Infrastructure.Persistence";

		private CodeGenerationParameters LoadParameters()
		{
			var useAutoDispatch = CodeGenerationParameter.Of(Label.UseAutoDispatch, true);
			var cqrs = CodeGenerationParameter.Of(Label.Cqrs, true);

			var firstAuthorRouteParameter = CodeGenerationParameter
				.Of(Label.RouteSignature, "ChangeAuthorName(string id, AuthorData authorData)")
				.Relate(Label.RouteHandlerInvocation, "ChangeAuthorNameHandler.Handler.Handle(author, authorData)")
				.Relate(Label.UseCustomRouteHandlerParam, "true")
				.Relate(Label.RoutePath, "/{id}/name")
				.Relate(Label.RouteMethod, "PATCH")
				.Relate(Label.InternalRouteHandler, "false")
				.Relate(Label.Id, "authorId")
				.Relate(Label.IdType, "string")
				.Relate(Label.Body, "authorData")
				.Relate(Label.BodyType, "Io.Vlingo.XoomApp.Infrastructure.AuthorData")
				.Relate(Label.AdapterHandlerInvocation, "AdapterStateHandler.Handler.Handle")
				.Relate(Label.UseCustomAdapterHandlerParam, "false");

			var authorResourceParameter = CodeGenerationParameter
				.Of(Label.AutoDispatchName, "Io.Vlingo.XoomApp.Resources.AuthorResource")
				.Relate(Label.HandlersConfigName, "io.vlingo.xoomapp.resources.AuthorHandlers")
				.Relate(Label.UriRoot, "/authers")
				.Relate(Label.ModelProtocol, "Io.Vlingo.XoomApp.Model.Author")
				.Relate(Label.ModelActor, "Io.Vlingo.XoomApp.Model.AuthorEntity")
				.Relate(Label.ModelData, "Io.Vlingo.XoomApp.Model.AuthorData")
				.Relate(Label.QueriesProtocol, "Io.Vlingo.XoomApp.Infrastructure.Persistence.AuthorQueries")
				.Relate(Label.QueriesActor, "Io.Vlingo.XoomApp.Infrastructure.Persistence.AuthorQueriesActor")
				.Relate(firstAuthorRouteParameter);
				
			return CodeGenerationParameters.From(authorResourceParameter, useAutoDispatch, cqrs);
		}
	}
}