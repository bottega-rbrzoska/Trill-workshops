using System;
using Shouldly;
using Xunit;

namespace Trill.Tests.Unit
{
    public class DummyTests : IDisposable
    {
        [Fact]
        public void dummy_test()
        {
            true.ShouldBeTrue();
        }

        // Test setup
        public DummyTests()
        {
        }

        // Test teardown
        public void Dispose()
        {
        }
    }
}