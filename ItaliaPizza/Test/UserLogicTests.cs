using Microsoft.VisualStudio.TestTools.UnitTesting;
using Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Management.Instrumentation;
using Model;
using static System.Net.Mime.MediaTypeNames;
using System.Security.Cryptography;
using System.IO;

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

        [TestMethod()]
        public void Test05_RegisterNewWorker_SuccesfulTest()
        {
            User userTest = new User()
            {
                Name = "Test",
                Lastname = "Test",
                PhoneNumber = "0000000000",
                Email = "test@test.com",
                IsActive = true
            };

            Worker workerTest = new Worker()
            {
                NSS = "00000000000",
                RFC = "AAAAAAAAAAAAA",
                WorkerNumber = "00000",
                Username = "Test",
                Password = "Test",
                Role = "User test"
            };

            int expectedResult = 200;
            int obtainedResult = UserLogic.RegisterNewWorker(userTest, workerTest);

            Assert.AreEqual(expectedResult, obtainedResult);
        }

        //Cambiar
        [TestMethod()]
        public void Test06_RegisterNewWorker_FailTest()
        {
            User userTest = new User()
            {
                Name = "Test",
                Lastname = "Test",
                PhoneNumber = "0000000000",
                Email = "test@test.com",
                IsActive = true
            };

            Worker workerTest = new Worker()
            {
                NSS = "00000000000",
                RFC = "AAAAAAAAAAAAA",
                WorkerNumber = "00000",
                Username = "Test",
                Password = "Test",
                Role = "User test"
            };

            int expectedResult = 200;
            int obtainedResult = UserLogic.RegisterNewWorker(userTest, workerTest);

            Assert.AreEqual(expectedResult, obtainedResult);
        }

        [TestMethod()]
        public void Test07_RegisterNewCustomer_SuccesfullTest()
        {
            User userTest = new User()
            {
                Name = "Test 2",
                Lastname = "Test 2",
                PhoneNumber = "0000000001",
                Email = "test@test.com",
                IsActive = true
            };

            Address addressTest = new Address()
            {
                street = "Street Test",
                city = "City Test",
                number = "0",
                zipcode = "00000",
                neighborhood = "Test",
                instructions = "Instructions test"
            };

            int expectedResult = 200;
            int obtainedResult = UserLogic.RegisterNewCustomer(userTest, addressTest);

            Assert.AreEqual(expectedResult, obtainedResult);
        }

        [TestMethod()]
        public void TestDeleteUser()
        {
            int userId = 1;
            int expectedStatusCode = 200;

            int actualStatusCode = UserLogic.DeleteUser(userId);

            Assert.AreEqual(expectedStatusCode, actualStatusCode);
        }

        [TestMethod()]
        public void TestDeleteUserWithInvalidUserId()
        {
            int userId = -1;
            int expectedStatusCode = 500;

            int actualStatusCode = UserLogic.DeleteUser(userId);

            Assert.AreEqual(expectedStatusCode, actualStatusCode);
        }

        [TestMethod()]
        public void TestDeleteUserWithNonExistingUserId()
        {
            int userId = 100;
            int expectedStatusCode = 500;

            int actualStatusCode = UserLogic.DeleteUser(userId);

            Assert.AreEqual(expectedStatusCode, actualStatusCode);
        }

	}
}