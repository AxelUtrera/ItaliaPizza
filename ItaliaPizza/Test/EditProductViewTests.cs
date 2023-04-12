using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text.RegularExpressions;

namespace View.Tests
{
    [TestClass()]
    public class EditProductViewTests
    {
        Regex nameFormat = new Regex("^[A-Za-z0-9 ]+$");
        Regex onlyPriceFormat = new Regex("^([1-9]\\d*|0)(\\.\\d+)?$");


        [TestMethod()]
        public void Test01_ValidateRegexName_SuccessfulTest()
        {
            bool result = nameFormat.IsMatch("CocaCola");
            Assert.IsTrue(result);
        }


        [TestMethod()]
        public void Test02_ValidateRegexName_SuccessfulTest()
        {
            bool result = nameFormat.IsMatch("CocaCola 600ml");
            Assert.IsTrue(result);
        }


        [TestMethod()]
        public void Test03_ValidateRegexName_SuccessfulTest()
        {
            bool result = false;
            string nameToTest = "CocaCola 600ml";
            if (nameFormat.IsMatch(nameToTest) && nameToTest.Length <= 50)
            {
                result = true;
            }
            Assert.IsTrue(result);
        }


        [TestMethod()]
        public void Test04_ValidateRegexName_FailedTest()
        {
            bool result = false;
            string nameToTest = "CocaCola 600ml con algunas cosas extras como lo son papas y algunas cosas mas";
            if (nameFormat.IsMatch(nameToTest) && nameToTest.Length <= 50)
            {
                result = true;
            }
            Assert.IsFalse(result);
        }


        [TestMethod()]
        public void Test05_ValidateRegexName_FailedTest()
        {
            bool result = false;
            string nameToTest = "-CocaCola";
            if (nameFormat.IsMatch(nameToTest) && nameToTest.Length <= 50)
            {
                result = true;
            }
            Assert.IsFalse(result);
        }


        [TestMethod()]
        public void Test06_ValidateRegexName_FailedTest()
        {
            bool result = false;
            string nameToTest = "{CocaCola}";
            if (nameFormat.IsMatch(nameToTest) && nameToTest.Length <= 50)
            {
                result = true;
            }
            Assert.IsFalse(result);
        }


        [TestMethod()]
        public void Test07_ValidateRegexPrice_SuccessfulTest()
        {
            bool result = false;
            string numberToTest = "20";
            if (onlyPriceFormat.IsMatch(numberToTest))
            {
                result = true;
            }
            Assert.IsTrue(result);
        }


        [TestMethod()]
        public void Test08_ValidateRegexPrice_SuccessfulTest()
        {
            bool result = false;
            string numberToTest = "20.5";
            if (onlyPriceFormat.IsMatch(numberToTest))
            {
                result = true;
            }
            Assert.IsTrue(result);
        }


        [TestMethod()]
        public void Test09_ValidateRegexPrice_SuccessfulTest()
        {
            bool result = false;
            string numberToTest = "200000";
            if (onlyPriceFormat.IsMatch(numberToTest))
            {
                result = true;
            }
            Assert.IsTrue(result);
        }


        [TestMethod()]
        public void Test10_ValidateRegexPrice_SuccessfulTest()
        {
            bool result = false;
            string numberToTest = "1.5";
            if (onlyPriceFormat.IsMatch(numberToTest))
            {
                result = true;
            }
            Assert.IsTrue(result);
        }


        [TestMethod()]
        public void Test11_ValidateRegexPrice_FailedTest()
        {
            bool result = false;
            string numberToTest = "-";
            if (onlyPriceFormat.IsMatch(numberToTest))
            {
                result = true;
            }
            Assert.IsFalse(result);
        }


        [TestMethod()]
        public void Test12_ValidateRegexPrice_FailedTest()
        {
            bool result = false;
            string numberToTest = ".5";
            if (onlyPriceFormat.IsMatch(numberToTest))
            {
                result = true;
            }
            Assert.IsFalse(result);
        }


        [TestMethod()]
        public void Test13_ValidateRegexPrice_FailedTest()
        {
            bool result = false;
            string numberToTest = "250..5";
            if (onlyPriceFormat.IsMatch(numberToTest))
            {
                result = true;
            }
            Assert.IsFalse(result);
        }


        [TestMethod()]
        public void Test14_ValidateRegexPrice_FailedTest()
        {
            bool result = false;
            string numberToTest = "$150";
            if (onlyPriceFormat.IsMatch(numberToTest))
            {
                result = true;
            }
            Assert.IsFalse(result);
        }
    }
}