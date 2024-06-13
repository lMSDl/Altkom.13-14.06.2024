using AutoFixture;

namespace ConsoleApp.Test.NUnit
{
    public class GardenTest
    {
        [Test]
        public void Plant_ValidName_True()
        {
            //Arrange
            const int MINIMAL_VALID_SIZE = 1;
            string validName = new Fixture().Create<string>();
            Garden garden = new Garden(MINIMAL_VALID_SIZE);

            //Act
            var result = garden.Plant(validName);

            //Assert
            Assert.That(result, Is.True);
        }

        [Test]
        public void Plant_OverflowGarden_False()
        {
            //Arrange
            const int MINIMAL_VALID_SIZE = 1;
            string validName1 = new Fixture().Create<string>();
            Garden garden = new Garden(MINIMAL_VALID_SIZE);
            garden.Plant(validName1);

            //Act
            var result = garden.Plant(validName1);

            //Assert
            Assert.That(result, Is.False);
        }

        [Test]
        public void Plant_NullName_ArgumentNullException()
        {
            //Arrange
            const int INSIGNIFICANT_SIZE = default;
            Garden garden = new Garden(INSIGNIFICANT_SIZE);
            string? nullName = null;

            //Act
            TestDelegate action = () => garden.Plant(nullName);

            //Assert
            var exception = Assert.Throws<ArgumentNullException>(action);
            Assert.That("name", Is.EqualTo(exception.ParamName));
        }

        //[Theory]
        [TestCase("")]
        [TestCase("   ")]
        [TestCase("\n")]
        [TestCase("\t")]
        [TestCase(" \n\t")]
        public void Plant_EmptyOrWhitespaceName_ArgumentException(string name)
        {
            //Arrange
            const int INSIGNIFICANT_SIZE = default;
            Garden garden = new Garden(INSIGNIFICANT_SIZE);

            //Act
            TestDelegate @delegate = () => garden.Plant(name);

            //Assert
            Assert.Throws(Is.InstanceOf<ArgumentException>().And
                .Property(nameof(ArgumentException.ParamName)).EqualTo("name").And
                .Property(nameof(ArgumentException.Message)).StartsWith("Roœlina musi posiadaæ nazwê"), @delegate);
        }

        [Test]
        [Ignore("Replaced by Plant_EmptyOrWhitespaceName_ArgumentException")]
        public void Plant_EmptyName_ArgumentException()
        {
        }

        [Test]
        public void Plant_ExistingName_ChangedName()
        {
            //Arrange
            const int MINIMAL_VALID_SIZE = 2;
            string validName = new Fixture().Create<string>();
            string expectedName = validName + 2;
            Garden garden = new Garden(MINIMAL_VALID_SIZE);
            garden.Plant(validName);

            //Act
            var result = garden.Plant(validName);

            //Assert
            Assert.That(garden.GetPlants(), Does.Contain(expectedName));
        }

        [Test]
        public void GetPlants_CopyOfPlantsCollection()
        {
            //Arrange
            const int INSIGNIFICANT_SIZE = default;
            Garden garden = new Garden(INSIGNIFICANT_SIZE);

            //Act
            var result1 = garden.GetPlants();
            var result2 = garden.GetPlants();

            //Assert
            Assert.That(result1, Is.Not.SameAs(result2));
        }

    }
}