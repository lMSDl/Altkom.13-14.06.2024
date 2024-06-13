using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Test.xUnit
{
    public class ProgramTest
    {
        [Fact]
        public void Main_ConsoleOutput_HelloWorld()
        {
            //Arrange
            var main = typeof(Program).Assembly.EntryPoint;
            TextWriter consoleWriter;
            using StringWriter writer = new StringWriter();
            SwitchConsoleOut(writer, out consoleWriter);

            //Act
            main.Invoke(null, new object[] { Array.Empty<string>() });

            //Assert
            SwitchConsoleOut(consoleWriter, out _);
            Assert.Equal("Hello, World!\r\n", writer.ToString());
        }

        private static void SwitchConsoleOut(TextWriter @int, out TextWriter @out)
        {
            @out = Console.Out;
            Console.SetOut(@int);
        }
    }
}
