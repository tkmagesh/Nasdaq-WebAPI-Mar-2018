using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyUtils;

namespace MyUtilsTests
{
    /*
    public class FakeDateTimeService : IDateTimeService
    {
        private DateTime value;

        public FakeDateTimeService(DateTime value)
        {
            this.value = value;
        }
        public DateTime GetCurrent()
        {
            return this.value;
        }
    }
    [TestClass]
    public class GreeterTests
    {
        [TestMethod]
        public void Should_Return_GoodMorning_Before_12()
        {
            //Arrange
            var dateTimeServiceForMorning = new FakeDateTimeService(new DateTime(2018, 03, 29, 9, 0, 0));
            var sut = new Greeter(dateTimeServiceForMorning);
            var userName = "Magesh";
            var expectedResult = "Hi Magesh, Good Morning!";

            //Act
            var actualResult = sut.Greet(userName);

            //Assert
            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void Should_Return_GoodAfternoon_After_12()
        {
            //Arrange
            var dateTimeServiceForAfternoon = new FakeDateTimeService(new DateTime(2018, 03, 29, 17, 0, 0));
            var sut = new Greeter(dateTimeServiceForAfternoon);
            var userName = "Magesh";
            var expectedResult = "Hi Magesh, Good Afternoon!";

            //Act
            var actualResult = sut.Greet(userName);

            //Assert
            Assert.AreEqual(expectedResult, actualResult);
        }

       
    }
     * */
    [TestClass]
    public class GreeterTests
    {
        [TestMethod]
        public void Should_Return_GoodMorning_Before_12()
        {
            //Arrange
            //var dateTimeServiceForMorning = new FakeDateTimeService(new DateTime(2018, 03, 29, 9, 0, 0));

            var mock = new Moq.Mock<IDateTimeService>();
            var dateTimeServiceForMorning = mock.Object;
            mock.Setup<DateTime>(dts => dts.GetCurrent()).Returns(new DateTime(2018, 03, 29, 9, 0, 0));

            var sut = new Greeter(dateTimeServiceForMorning);
            var userName = "Magesh";
            var expectedResult = "Hi Magesh, Good Morning!";

            //Act
            var actualResult = sut.Greet(userName);

            //Assert
            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void Should_Return_GoodAfternoon_After_12()
        {
            //Arrange
            //var dateTimeServiceForAfternoon = new FakeDateTimeService(new DateTime(2018, 03, 29, 17, 0, 0));

            var mock = new Moq.Mock<IDateTimeService>();
            var dateTimeServiceForAfternoon = mock.Object;
            mock.Setup<DateTime>(dts => dts.GetCurrent()).Returns(new DateTime(2018, 03, 29, 17, 0, 0));

            var sut = new Greeter(dateTimeServiceForAfternoon);
            var userName = "Magesh";
            var expectedResult = "Hi Magesh, Good Afternoon!";

            //Act
            var actualResult = sut.Greet(userName);

            //Assert
            Assert.AreEqual(expectedResult, actualResult);
        }


    }

}
