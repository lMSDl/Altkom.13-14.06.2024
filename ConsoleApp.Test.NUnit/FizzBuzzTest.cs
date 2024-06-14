using ConsoleApp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Test.NUnit
{
    public class FizzBuzzTest
    {
        [Test]
        public void ReturnOne()
        {
            Assert.That(FizzBuzz.Compute(1),
                        Is.EqualTo(new List<string> { "1" }));
        }

        [Test]
        public void ReturnFifteen()
        {
            int n = 15;
            List<string> result = FizzBuzz.Compute(n);
            List<string> expected = new List<string> {
                "1", "2", "Fizz", "4", "Buzz",
                "Fizz", "7", "8", "Fizz", "Buzz",
                "11", "Fizz", "13", "14", "FizzBuzz"
            };

            Assert.That(result.Count,
                        Is.EqualTo(expected.Count),
                        string.Format("Your function should return {0} objects for n = {1}", n, n));

            for (int i = 0; i < n; i++)
            {
                Assert.That(result[i],
                            Is.EqualTo(expected[i]),
                            string.Format("Your function should convert the value {0} to {1} instead of {2}",
                                          i + 1, expected[i], result[i]));
            }
        }

        [TestCase(1, "1")]
        [TestCase(15, "1 2 Fizz 4 Buzz Fizz 7 8 Fizz Buzz 11 Fizz 13 14 FizzBuzz")]
        public void Compute(int count, string expectedOutput)
        {
            //Act
            var result = FizzBuzz.Compute(count);

            //Assert
            var stringResult = string.Join(" ", result);
            Assert.That(stringResult, Is.EqualTo(expectedOutput));
        }

        [TestCase(1, "Fizz", 0)]
        [TestCase(1, "Buzz", 0)]
        [TestCase(15, "Fizz", 5)]
        [TestCase(15, "Buzz", 3)]
        [TestCase(100, "Fizz", 33)]
        [TestCase(100, "Buzz", 20)]
        [TestCase(100, "FizzBuzz", 6)]
        public void Compute_AnyInt_CorrectFizzBuzzCount(int count, string fizzBuzz, int expectedResult)
        {
            //Act
            var result = FizzBuzz.Compute(count);

            //Assert
            var resultCount = result.Count(x => x.Contains(fizzBuzz));
            Assert.That(resultCount, Is.EqualTo(expectedResult));
        }

        [TestCase(1)]
        [TestCase(15)]
        [TestCase(100)]
        public void Compute_AnyInt_NonFizzBuzzValues(int count)
        {
            //Arrange
            var expected = Enumerable.Range(1, count).Select(x => x.ToString()).ToList();

            //Act
            var result = FizzBuzz.Compute(count);

            //Assert
            var zip = result.Zip(expected);

            Assert.IsTrue(zip.Where(x => int.TryParse(x.First, out _)).All(x => x.First.Equals(x.Second)));
        }
    }
}
