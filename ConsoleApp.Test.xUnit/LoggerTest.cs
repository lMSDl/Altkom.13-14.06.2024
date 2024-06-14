using AutoFixture;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Test.xUnit
{
    public class LoggerTest
    {
        [Fact]
        public void Log_AnyInput_EventInvokedOnce()
        {
            //Arrange
            var log = new AutoFixture.Fixture().Create<string>();
            var logger = new Logger();
            var result = 0;
            var expectedResult = 1;
            logger.MessageLogged += (s, e) => result++;

            //Act
            logger.Log(log);

            //Assert
            Assert.Equal(expectedResult, result);
        }


        [Fact]
        public void Log_AnyInput_ValidEventInvoked()
        {
            //Arrange
            var log = new AutoFixture.Fixture().Create<string>();
            var logger = new Logger();
            object? sender = null;
            Logger.LoggerEventArgs? loggerEventArgs = null;

            logger.MessageLogged += (s, e) => { sender = s; loggerEventArgs = e as Logger.LoggerEventArgs; };

            var timeStart = DateTime.Now;
            
            //Act
            logger.Log(log);

            //Assert

            var timeStop = DateTime.Now;
            Assert.Equal(logger, sender);
            Assert.NotNull(loggerEventArgs);
            Assert.Equal(log, loggerEventArgs.Message);
            Assert.InRange(loggerEventArgs.DateTime, timeStart, timeStop);
        }

        [Fact]
        public async void GetLogsAsync_DateRange_LoggerMessage()
        {
            //Arrange
            var log = new AutoFixture.Fixture().Create<string>();
            var logger = new Logger();
            var timeStart = DateTime.Now;
            logger.Log(log);
            var timeStop = DateTime.Now;

            //Act
            var result = await logger.GetLogsAsync(timeStart, timeStop)/*.Result*/;

            //
            var splitted = result.Split(": ");
            Assert.Equal(2, splitted.Length);
            Assert.Equal(log, splitted[1]);
            Assert.True(DateTime.TryParseExact(splitted[0], "dd.MM.yyyy hh:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out _));

        }
    }
}
