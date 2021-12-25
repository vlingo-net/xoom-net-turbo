// Copyright © 2012-2021 VLINGO LABS. All rights reserved.
//
// This Source Code Form is subject to the terms of the
// Mozilla Public License, v. 2.0. If a copy of the MPL
// was not distributed with this file, You can obtain
// one at https://mozilla.org/MPL/2.0/.

using Moq;
using Vlingo.Xoom.Turbo.Annotation;
using Vlingo.Xoom.Turbo.Annotation.AutoDispatch;
using Xunit;

namespace Vlingo.Xoom.Turbo.Tests.Annotation.AutoDispatch
{
    public class QueriesValidationTest
    {
        [Fact]
        public void TestThatSingularityValidationPasses()
        {
            var mockAnnotatedElements = new Mock<AnnotatedElements>();
            mockAnnotatedElements.Setup(s => s.Count(It.IsAny<Queries>())).Returns(1);

            Validation.SingularityValidation().Invoke(new Mock<ProcessingEnvironment>().Object,
                typeof(Turbo.Annotation.Initializer.Xoom), mockAnnotatedElements.Object);
        }
    }
}