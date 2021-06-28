using ManagerGateway.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Xunit.Sdk;

namespace ManagerGateway.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Gateway_ShouldReturnArgumentException_Address()
        {
            //Arrange

            //Act
            var gateway = new Gateway(Guid.NewGuid().ToString(), "1", "Riverdale");

            //Assert
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Gateway_ShouldReturnArgumentException_Name()
        {
            //Arrange

            //Act
            var gateway = new Gateway(Guid.NewGuid().ToString(), "127.0.0.1", "");

            //Assert
            
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Gateway_ShouldReturnArgumentException_Usn()
        {
            //Arrange

            //Act
            var gateway = new Gateway("", "127.0.0.1", "Riverdale");

            //Assert

        }
    }
}
