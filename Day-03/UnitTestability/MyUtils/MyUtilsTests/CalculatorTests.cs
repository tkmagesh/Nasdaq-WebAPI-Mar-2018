using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyUtils;

namespace MyUtilsTests{

    [TestClass]
    public class CalculatorTests
    {
        [TestMethod]
        public void Calculator_Should_Add()
        {
            //Arrange
            var sut = new Calculator();
            var n1 = 10;
            var n2 = 20;
            var expectedResult = 30;

            //Act
            var actualResult = sut.Add(n1, n2);

            //Assert
            Assert.AreEqual(expectedResult, actualResult);
        }
    }
}
