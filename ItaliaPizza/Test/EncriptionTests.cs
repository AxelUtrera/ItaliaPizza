using Microsoft.VisualStudio.TestTools.UnitTesting;
using Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Tests
{
    [TestClass()]
    public class EncriptionTests
    {
        [TestMethod()]
        public void ToSHA2HashSuccessfulTest()
        {
            String passwordHashedExpected = Encription.ToSHA2Hash("pollitoDeItaliaPizza");
            String passwordHashResult = "08016164beb0990eaea42aa2063bdaca5a46c8b7df9014063b3a4ca87fa6cbf7";

            Assert.AreEqual(passwordHashedExpected, passwordHashResult);

        }

        [TestMethod()]
        public void ToSHA2HashFailedTest()
        {
            String passwordHashedExpected = "pollitoDeItaliaPizza";
            String passwordHashResult = "08016164beb0990eaea42aa2063bdaca5a46c8b7df9014063b3a4ca87fa6cbf7";

            Assert.AreNotEqual(passwordHashedExpected, passwordHashResult);

        }
    }
}