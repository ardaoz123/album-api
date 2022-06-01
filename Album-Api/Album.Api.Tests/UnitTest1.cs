using System;
using Xunit;

namespace Album.Api.Tests
{
    public class UnitTest1
    {
        //tests
        [Fact]
        public void TestNull()
        {
            Assert.Equal("Hello World", GreetingService.Greeting(null));  
        }

        [Fact]
        public void TestEmpty()
        {
            Assert.Equal("Hello World", GreetingService.Greeting(""));
        }

        [Fact]
        public void TestWhite()
        {
            Assert.Equal("Hello World", GreetingService.Greeting(" "));
        }

        [Fact]
        public void TestName()
        {
            Assert.Equal("Hello Test", GreetingService.Greeting("Test"));
        }
    }
}