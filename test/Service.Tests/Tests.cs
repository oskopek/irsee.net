using System;
using Xunit;

namespace MFF.Csharp.Project
{
    public class ServiceLibrary_ShouldAdd
    {
        private readonly ServiceLibrary serviceLibrary;
        public ServiceLibrary_ShouldAdd()
        {
            serviceLibrary = new ServiceLibrary();
        }

        [Fact]
        public void ReturnFalseGivenValueOf1()
        {
            var result = serviceLibrary.Add(1, 2);
            Assert.Equal(3, result);
        }
    }
}

