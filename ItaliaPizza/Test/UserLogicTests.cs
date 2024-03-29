﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model;
using System;
using System.Collections.Generic;

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


        [TestMethod()]
        public void Test06_RegisterNewCustomer_SuccesfullTest()
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
        public void Test07_RecoverActiveUsers_SuccesfulTest()
        {
            List<User> recoverUsers = new List<User>();
            recoverUsers = UserLogic.RecoverActiveUsers();

            Assert.IsNotNull(recoverUsers);
        }


        [TestMethod()]
        public void Test08_TestDeleteUser()
        {
            int userId = 1;
            int expectedStatusCode = 200;

            int actualStatusCode = UserLogic.DeleteUser(userId);

            Assert.AreEqual(expectedStatusCode, actualStatusCode);
        }


        [TestMethod()]
        public void Test09_TestDeleteUser_WithInvalidUserId()
        {
            int userId = -1;
            int expectedStatusCode = 500;

            int actualStatusCode = UserLogic.DeleteUser(userId);

            Assert.AreEqual(expectedStatusCode, actualStatusCode);
        }


        [TestMethod()]
        public void Test10_TestDeleteUser_WithNonExistingUserId()
        {
            int userId = 100;
            int expectedStatusCode = 500;

            int actualStatusCode = UserLogic.DeleteUser(userId);

            Assert.AreEqual(expectedStatusCode, actualStatusCode);
        }


        [TestMethod()]
        public void Test11_GetUserById_SuccesfullTest()
        {
            int userId = 7;

            User userExpected = new User()
            {
                IdUser = 7,
                Name = "Test",
                Lastname = "Test",
                PhoneNumber = "0000000000",
                Email = "test@test.com",
                IsActive = true
            };

            User userObtained = UserLogic.GetUserById(userId);

            Assert.AreEqual(userExpected, userObtained);
        }


        [TestMethod()]
        public void Test12_GetUserById_FailTest()
        {
            int userId = 0;

            User userExpected = new User()
            {
                IdUser = 7,
                Name = "Test",
                Lastname = "Test",
                PhoneNumber = "0000000000",
                Email = "test@test.com",
                IsActive = true
            };

            User userObtained = UserLogic.GetUserById(userId);

            Assert.AreNotEqual(userExpected, userObtained);
        }


        [TestMethod()]
        public void Test13_GetWorkerById_SuccesfullTest()
        {
            int idWorker = 7;

            Worker workerExpected = new Worker()
            {
                Username = "Test",
                NSS = "00000000000",
                Password = "532eaabd9574880dbf76b9b8cc00832c20a6ec113d682299550d7a6e0f345e25",
                RFC = "AAAAAAAAAAAAA",
                Role = "User test",
                WorkerNumber = "00000"
            };

            Worker workerObtained = UserLogic.GetWorkerById(idWorker);

            Assert.AreEqual(workerExpected, workerObtained);
        }


        [TestMethod()]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Test14_GetWorkerById_FailTest()
        {
            int idWorker = 0;

            Worker workerObtained = UserLogic.GetWorkerById(idWorker);

        }


        [TestMethod()]
        public void Test15_GetAddressById_SuccesfullTest()
        {
            int idUser = 6;

            Address addressExpected = new Address()
            {
                idAddress = 4,
                street = "Street Test",
                city = "City Test",
                number = "0",
                zipcode = "00000",
                neighborhood = "Test",
                instructions = "Instructions test"
            };

            Address addressObtained = UserLogic.GetAddressByIdUser(idUser);

            Assert.AreEqual (addressExpected, addressObtained);
        }


        [TestMethod()]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Test16_GetAddressById_FailTest()
        {
            int idUser = 0;

            Address addressObtained = UserLogic.GetAddressByIdUser(idUser);
        }


        [TestMethod()]
        public void Test17_ModifyWorker_SuccesfullTest()
        {
            int resultExpected = 200;

            User userTest = new User()
            {
                IdUser = 7,
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
                WorkerNumber = "00001",
                Username = "Test",
                Password = "Test",
                Role = "User test"
            };

            int resultObtained = UserLogic.ModifyWorker(userTest, workerTest);

            Assert.AreEqual(resultExpected, resultObtained);
        }


        [TestMethod()]
        public void Test18_ModifyWorker_FailTest()
        {
            int resultExpected = 500;

            User userTest = new User()
            {
                IdUser = 7,
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
                WorkerNumber = "00001",
                Username = "Test",
                Password = "Test",
                Role = "User test"
            };

            int resultObtained = UserLogic.ModifyWorker(userTest, workerTest);

            Assert.AreEqual(resultExpected, resultObtained);
        }


        [TestMethod()]
        public void Test19_ModifyCustomer_SuccesfullTest()
        {
            int resultExpected = 200;

            User userTest = new User()
            {
                IdUser = 6,
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
                zipcode = "00001",
                neighborhood = "Test",
                instructions = "Instructions test"
            };

            int resultObtained = UserLogic.ModifyCustomer(userTest, addressTest);

            Assert.AreEqual(resultExpected, resultObtained);
        }


        [TestMethod()]
        public void Test20_ModifyCustomer_FailTest()
        {
            int resultExpected = 500;

            User userTest = new User()
            {
                IdUser = 6,
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
                zipcode = "00001",
                neighborhood = "Test",
                instructions = "Instructions test"
            };

            int resultObtained = UserLogic.ModifyCustomer(userTest, addressTest);

            Assert.AreEqual(resultExpected, resultObtained);
        }
    }

    }
