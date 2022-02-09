// Copyright © 2012-2022 VLINGO LABS. All rights reserved.
//
// This Source Code Form is subject to the terms of the
// Mozilla Public License, v. 2.0. If a copy of the MPL
// was not distributed with this file, You can obtain
// one at https://mozilla.org/MPL/2.0/.

using System.IO;
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
        public AutoDispatchResourceHandlerGenerationStepTest() =>
            ComponentRegistry.Register<CodeElementFormatter>(
                CodeElementFormatter.With(CSharp));

        [Fact(Skip = "WIP")]
        public void TestThatAutoDispatchResourceHandlersAreGenerated()
        {
            var context = CodeGenerationContext
                .With(LoadParameters())
                .AddContent(AnnotationBasedTemplateStandard.StoreProvider,
                    new OutputFile(PersistencePackagePath, "QueryModelStateStoreProvider.cs"),
                    QueryModelStoreProviderContent);

            new AutoDispatchResourceHandlerGenerationStep().Process(context);

            var authorResourceHandler = context.FindContent(AnnotationBasedTemplateStandard.AutoDispatchResourceHandler,
                "AuthorResourceHandler");
            Assert.Equal(3, context.Contents().Count);
            Assert.True(
                authorResourceHandler!.Contains(TextExpectation.OnCSharp.Read("author-dispatch-resource-handler")));
        }

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
                .Relate(Label.BodyType, "IO.Vlingo.XoomApp.Infrastructure.AuthorData")
                .Relate(Label.AdapterHandlerInvocation, "AdapterStateHandler.Handler.Handle")
                .Relate(Label.UseCustomAdapterHandlerParam, "false");

            var authorResourceParameter = CodeGenerationParameter
                .Of(Label.AutoDispatchName, "IO.Vlingo.XoomApp.Resources.AuthorResource")
                .Relate(Label.HandlersConfigName, "IO.vlingo.xoomapp.resources.AuthorHandlers")
                .Relate(Label.UriRoot, "/authers")
                .Relate(Label.ModelProtocol, "IO.Vlingo.XoomApp.Model.Author")
                .Relate(Label.ModelActor, "IO.Vlingo.XoomApp.Model.AuthorEntity")
                .Relate(Label.ModelData, "IO.Vlingo.XoomApp.Model.AuthorData")
                .Relate(Label.QueriesProtocol, "IO.Vlingo.XoomApp.Infrastructure.Persistence.AuthorQueries")
                .Relate(Label.QueriesActor, "IO.Vlingo.XoomApp.Infrastructure.Persistence.AuthorQueriesActor")
                .Relate(firstAuthorRouteParameter);

            return CodeGenerationParameters.From(authorResourceParameter, useAutoDispatch, cqrs);
        }

        private static readonly string ProjectPath = Path.Combine(Directory.GetCurrentDirectory(), "xoom-app");

        private static string InfrastructurePackagePath =
            Path.Combine(ProjectPath, "src", "IO.Vlingo.XoomApp", "Infrastructure");

        private static string PersistencePackagePath = Path.Combine(InfrastructurePackagePath, "Persistence");

        private const string QueryModelStoreProviderContent =
            "namespace IO.Vlingo.XoomApp.Infrastructure.Persistence " +
            "{ " +
            "public class QueryModelStateStoreProvider " +
            "{ " +
            "... " +
            "}" +
            "}";
    }
}