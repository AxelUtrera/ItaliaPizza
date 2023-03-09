using Microsoft.VisualStudio.TestTools.UnitTesting;
using Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Management.Instrumentation;

namespace Logic.Tests
{
    [TestClass()]
    public class UserLogicTests
    {
        [TestMethod()]
        public void Test01_AutenticateUser_SuccessfulTest()
        {
            int operationResult = UserLogic.AutenticateUser("TestUser", "a");
            int operationExpected = 200;

            Assert.AreEqual(operationExpected, operationResult);
        }

        [TestMethod()]
        public void Test02_AutenticateUser_NotValidTest()
        {
            int operationResult = UserLogic.AutenticateUser("TestUser", "asd");
            int operationExpected = 200;

            Assert.AreNotEqual(operationExpected, operationResult);
        }

        

        [TestMethod()]
        public void Test03_GetWorkerByUsername_SuccessfulTest()
        {
            Model.Worker workerExpected = new Model.Worker()
            {
                IdUser = 1,
                Username = "J1000",
                NSS = "123456",
                Password = "ca978112ca1bbdcafac231b39a23dc4da786eff8147c4e72b9807785afee48bb",
                RFC = "asdfasdfsf",
                Role = "Administrador",
                WorkerNumber = "1"
            };

            Model.Worker workerResult = UserLogic.GetWorkerByUsername("J1000");
            Assert.AreEqual(workerExpected, workerResult);
        }

        [TestMethod()]
        public void Test04_GetWorkerByUsername_FailTest()
        {
            Model.Worker workerExpected = new Model.Worker()
            {
                IdUser = 1,
                Username = "TestUser",
                NSS = "1",
                Password = "test",
                RFC = "test",
                Role = "Administrador",
                WorkerNumber = "1"
            };

            Model.Worker workerResult = UserLogic.GetWorkerByUsername("J1000");
            Assert.AreNotEqual(workerExpected, workerResult);
        }
    }
}