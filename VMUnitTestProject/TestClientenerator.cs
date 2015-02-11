using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VendingMachine.Simulations;
using VendingMachine.Core.Products;

namespace VMUnitTestProject
{
    [TestClass]
    public class TestClientGenerator
    {
        [TestMethod]
        public void TestConstructor()
        {
            Client c = new Client();
            Console.Out.WriteLine(c.Product+" : " + c.Wallet);
            //Assert.IsNotNull(c.Product);
            Assert.IsTrue(c.Wallet.Count < 10);
        }
    }
}
