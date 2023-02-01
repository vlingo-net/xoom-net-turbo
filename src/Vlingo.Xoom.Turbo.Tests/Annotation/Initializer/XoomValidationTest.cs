// Copyright © 2012-2023 VLINGO LABS. All rights reserved.
//
// This Source Code Form is subject to the terms of the
// Mozilla Public License, v. 2.0. If a copy of the MPL
// was not distributed with this file, You can obtain
// one at https://mozilla.org/MPL/2.0/.

using System;
using System.Collections.Generic;
using Moq;
using Vlingo.Xoom.Turbo.Annotation;
using Xunit;

namespace Vlingo.Xoom.Turbo.Tests.Annotation.Initializer;

public class XoomValidationTest : IDisposable
{
    private readonly MockRepository _mockRepository = new MockRepository(MockBehavior.Loose);

    [Fact]
    public void TestThatSingularityValidationPasses()
    {
        var mockAnnotatedElements = _mockRepository.Create<AnnotatedElements>();
        mockAnnotatedElements.Setup(s => s.Count(typeof(Turbo.Annotation.Initializer.XoomAttribute))).Returns(1);

        Validation.SingularityValidation().Invoke(new Mock<ProcessingEnvironment>().Object,
            typeof(Turbo.Annotation.Initializer.XoomAttribute), mockAnnotatedElements.Object);
    }

    [Fact]
    public void TestThatSingularityValidationFails()
    {
        var mockAnnotatedElements = _mockRepository.Create<AnnotatedElements>();
        mockAnnotatedElements.Setup(s => s.Count(typeof(Turbo.Annotation.Initializer.XoomAttribute))).Returns(2);

        Assert.Throws<ProcessingAnnotationException>(() => Validation.SingularityValidation().Invoke(
            new Mock<ProcessingEnvironment>().Object,
            typeof(Turbo.Annotation.Initializer.XoomAttribute), mockAnnotatedElements.Object));
    }

    [Fact]
    public void TestThatTargetValidationPasses()
    {
        var mockElement = _mockRepository.Create<Type>();
        var elements = new HashSet<Type> { mockElement.Object };
        var mockAnnotatedElements = _mockRepository.Create<AnnotatedElements>();

        mockAnnotatedElements.Setup(s => s.ElementsWith(It.IsAny<object[]>()))
            .Returns(elements);
        mockElement.SetReturnsDefault(false);

        Validation.TargetValidation().Invoke(new Mock<ProcessingEnvironment>().Object,
            typeof(Turbo.Annotation.Initializer.XoomAttribute), mockAnnotatedElements.Object);
    }

    [Fact]
    public void TestThatTargetValidationFails()
    {
        var mockElement = _mockRepository.Create<Type>();
        var elements = new HashSet<Type> { mockElement.Object };
        var mockAnnotatedElements = _mockRepository.Create<AnnotatedElements>();

        mockAnnotatedElements.Setup(s => s.ElementsWith(It.IsAny<object[]>()))
            .Returns(elements);
        mockElement.SetReturnsDefault(true);

        Assert.Throws<ProcessingAnnotationException>(() => Validation.TargetValidation().Invoke(
            new Mock<ProcessingEnvironment>().Object,
            typeof(Turbo.Annotation.Initializer.XoomAttribute), mockAnnotatedElements.Object));
    }

    [Fact]
    public void TestThatClassVisibilityValidationPasses()
    {
        var elements = new HashSet<Type> { typeof(Turbo.Annotation.Initializer.XoomAttribute) };
        var mockAnnotatedElements = _mockRepository.Create<AnnotatedElements>();

        mockAnnotatedElements.Setup(s => s.ElementsWith(new[] { typeof(Turbo.Annotation.Initializer.XoomAttribute) }))
            .Returns(elements);

        Validation.ClassVisibilityValidation().Invoke(new Mock<ProcessingEnvironment>().Object,
            typeof(Turbo.Annotation.Initializer.XoomAttribute), mockAnnotatedElements.Object);
    }

    [Fact]
    public void TestThatClassVisibilityValidationFails()
    {
        var mockElement = _mockRepository.Create<Type>();
        var elements = new HashSet<Type> { mockElement.Object };
        var mockAnnotatedElements = _mockRepository.Create<AnnotatedElements>();

        mockAnnotatedElements.Setup(s => s.ElementsWith(It.IsAny<object[]>()))
            .Returns(elements);

        Assert.Throws<ProcessingAnnotationException>(() => Validation.ClassVisibilityValidation().Invoke(
            new Mock<ProcessingEnvironment>().Object,
            typeof(Turbo.Annotation.Initializer.XoomAttribute), mockAnnotatedElements.Object));
    }

    public void Dispose() => _mockRepository.VerifyAll();
}