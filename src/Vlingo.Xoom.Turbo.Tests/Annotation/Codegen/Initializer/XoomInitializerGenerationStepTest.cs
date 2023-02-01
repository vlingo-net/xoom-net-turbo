// Copyright © 2012-2023 VLINGO LABS. All rights reserved.
//
// This Source Code Form is subject to the terms of the
// Mozilla Public License, v. 2.0. If a copy of the MPL
// was not distributed with this file, You can obtain
// one at https://mozilla.org/MPL/2.0/.

using System;
using Moq;
using Vlingo.Xoom.Turbo.Annotation.Codegen;
using Vlingo.Xoom.Turbo.Annotation.Codegen.Initializer;
using Vlingo.Xoom.Turbo.Annotation.Codegen.Projections;
using Vlingo.Xoom.Turbo.Codegen;
using Vlingo.Xoom.Turbo.Codegen.Content;
using Vlingo.Xoom.Turbo.Codegen.Dialect;
using Vlingo.Xoom.Turbo.Codegen.Parameter;
using Vlingo.Xoom.Turbo.Codegen.Template;
using Xunit;

namespace Vlingo.Xoom.Turbo.Tests.Annotation.Codegen.Initializer;

using System.IO;

public class XoomInitializerGenerationStepTest : IDisposable
{
    private readonly MockRepository _mockRepository;

    public XoomInitializerGenerationStepTest()
    {
        ComponentRegistry.Register<CodeElementFormatter>(CodeElementFormatter.With(Dialect.CSharp));

        _mockRepository = new MockRepository(MockBehavior.Default);
    }

    [Fact(Skip = "WIP")]
    public void TestThatXoomInitializerIsGenerated()
    {
        var context = CodeGenerationContext
            .Using(_mockRepository.Create<FileStream>(It.IsAny<IntPtr>(), FileAccess.Read).Object,
                _mockRepository.Create<Type>().Object)
            .With(Label.ProjectionType, ProjectionType.None.Name()!)
            .With(Label.XoomInitializerName, "AnnotatedBootstrap");
        LoadParametersWithoutAnnotation(context);
        LoadContents(context);

        new XoomInitializerGenerationStep().Process(context);

        var xoomInitializer =
            context.FindContent(AnnotationBasedTemplateStandard.XoomInitializer, "XoomInitializer");
        Assert.Equal(7, context.Contents().Count);
        Assert.True(xoomInitializer!.Contains(TextExpectation.OnCSharp.Read("xoom-initializer")));
    }

    private void LoadParametersWithoutAnnotation(CodeGenerationContext context) =>
        context.With(Label.Package, "IO.Vlingo.XoomApp").With(Label.ApplicationName, "XoomApp")
            .With(Label.TargetFolder, HomeDirectory).With(Label.StorageType, "StateStore")
            .With(Label.Cqrs, "true").With(Label.BlockingMessaging, bool.TrueString)
            .With(Label.UseAnnotations, bool.FalseString);

    private void LoadContents(CodeGenerationContext context)
    {
        context.AddContent(new TemplateStandard(TemplateStandardType.RestResource),
            new OutputFile(ResourcePackagePath, "AuthorResource.cs"),
            AuthorResourceContent);
        context.AddContent(AnnotationBasedTemplateStandard.StoreProvider,
            new OutputFile(PersistencePackagePath, "CommandModelStateStoreProvide.cs"),
            CommandModelStateStoreProvideContent);
        context.AddContent(AnnotationBasedTemplateStandard.StoreProvider,
            new OutputFile(PersistencePackagePath, "QueryModelStateStoreProvide.cs"),
            QueryModelStateStoreProvideContent);
        context.AddContent(new TemplateStandard(TemplateStandardType.ExchangeBootstrap),
            new OutputFile(ExchangePackagePath, "ExchangeBootstrap.cs"),
            ExchangeBootstrapContent);
        context.AddContent(new TemplateStandard(TemplateStandardType.ProjectionDispatcherProvider),
            new OutputFile(PersistencePackagePath, "ProjectionDispatcherProvider.cs"),
            ProjectionDispatcherProviderContent);
    }

    private static readonly string HomeDirectory = Path.Combine(Directory.GetCurrentDirectory(), "xoom-app");

    private static string InfrastructurePackagePath =
        Path.Combine(HomeDirectory, "src", "IO.Vlingo.XoomApp", "Infrastructure");

    private static string ResourcePackagePath = Path.Combine(InfrastructurePackagePath, "Resource");
    private static string PersistencePackagePath = Path.Combine(InfrastructurePackagePath, "Persistence");
    private static string ExchangePackagePath = Path.Combine(InfrastructurePackagePath, "Exchange");

    private const string AuthorResourceContent =
        "namespace IO.Vlingo.XoomApp.Infrastructure.Resource " +
        "{ " +
        "public class AuthorResource " +
        "{ " +
        "... " +
        "}" +
        "}";

    private const string CommandModelStateStoreProvideContent =
        "namespace IO.Vlingo.XoomApp.Infrastructure.Persistence " +
        "{ " +
        "public class CommandModelStateStoreProvider " +
        "{ " +
        "... " +
        "}" +
        "}";

    private const string QueryModelStateStoreProvideContent =
        "namespace IO.Vlingo.XoomApp.Infrastructure.Persistence " +
        "{ " +
        "public class QueryModelStateStoreProvider " +
        "{ " +
        "... " +
        "}" +
        "}";

    private const string ProjectionDispatcherProviderContent =
        "namespace IO.Vlingo.XoomApp.Infrastructure.Persistence " +
        "{ " +
        "public class ProjectionDispatcherProvider " +
        "{ " +
        "... " +
        "}" +
        "}";

    private const string ExchangeBootstrapContent =
        "namespace IO.Vlingo.XoomApp.Infrastructure.Exchange " +
        "{ " +
        "public class ExchangeBootstrap " +
        "{ " +
        "... " +
        "}" +
        "}";

    public void Dispose() => _mockRepository.VerifyAll();
}