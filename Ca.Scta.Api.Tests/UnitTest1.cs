using System;
using Ca.Scta.Api.AppStart;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ca.Scta.Api.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var startup = new Startup();
            startup.GetIocContainer();
        }
    }
}
