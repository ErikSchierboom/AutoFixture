﻿using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Ploeh.AutoFixture;
using Ploeh.AutoFixtureUnitTest.Kernel;
using Ploeh.AutoFixture.Kernel;
using Xunit;

namespace Ploeh.AutoFixtureUnitTest
{
    public class GuidGeneratorTest
    {
        [Fact]
        public void CreateAnonymousWillReturnNonDefaultGuid()
        {
            // Fixture setup
            var unexpectedGuid = default(Guid);
            // Exercise system
            var result = GuidGenerator.CreateAnonymous();
            // Verify outcome
            Assert.NotEqual<Guid>(unexpectedGuid, result);
            // Teardown
        }

        [Fact]
        public void CreateAnonymousTwiceWillReturnDifferentValues()
        {
            // Fixture setup
            var unexpectedGuid = GuidGenerator.CreateAnonymous();
            // Exercise system
            var result = GuidGenerator.CreateAnonymous();
            // Verify outcome
            Assert.NotEqual<Guid>(unexpectedGuid, result);
            // Teardown
        }

        [Fact]
        public void SutIsSpecimenBuilder()
        {
            // Fixture setup
            // Exercise system
            var sut = new GuidGenerator();
            // Verify outcome
            Assert.IsAssignableFrom<ISpecimenBuilder>(sut);
            // Teardown
        }

        [Fact]
        public void CreateWithNullRequestWillReturnCorrectResult()
        {
            // Fixture setup
            var sut = new GuidGenerator();
            // Exercise system
            var dummyContainer = new DelegatingSpecimenContext();
            var result = sut.Create(null, dummyContainer);
            // Verify outcome
            Assert.Equal(new NoSpecimen(), result);
            // Teardown
        }

        [Fact]
        public void CreateWithNullContextDoesNotThrow()
        {
            // Fixture setup
            var sut = new GuidGenerator();
            // Exercise system
            var dummyRequest = new object();
            sut.Create(dummyRequest, null);
            // Verify outcome (no exception indicates success)
            // Teardown
        }

        [Fact]
        public void CreateWithNonGuidRequestWillReturnCorrectResult()
        {
            // Fixture setup
            var nonGuidRequest = new object();
            var sut = new GuidGenerator();
            // Exercise system
            var dummyContext = new DelegatingSpecimenContext();
            var result = sut.Create(nonGuidRequest, dummyContext);
            // Verify outcome
            var expectedResult = new NoSpecimen(nonGuidRequest);
            Assert.Equal(expectedResult, result);
            // Teardown
        }

        [Fact]
        public void CreateWithGuidRequestWillReturnCorrectResult()
        {
            // Fixture setup
            var guidRequest = typeof(Guid);
            var sut = new GuidGenerator();
            // Exercise system
            var dummyContext = new DelegatingSpecimenContext();
            var result = sut.Create(guidRequest, dummyContext);
            // Verify outcome
            Assert.NotEqual(default(Guid), result);
            // Teardown
        }

        [Fact]
        public void CreateWithGuidRequestTwiceWillReturnDifferentResults()
        {
            // Fixture setup
            var sut = new GuidGenerator();

            var guidRequest = typeof(Guid);
            var dummyContext = new DelegatingSpecimenContext();
            var unexpectedResult = sut.Create(guidRequest, dummyContext);
            // Exercise system
            var result = sut.Create(guidRequest, dummyContext);
            // Verify outcome
            Assert.NotEqual(unexpectedResult, result);
            // Teardown
        }
    }
}