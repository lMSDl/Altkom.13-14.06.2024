using AutoFixture;

namespace ConsoleApp.Test.MSTest
{
    [TestClass]
    public class GardenTest
    {
        [TestMethod]
        public void Plant_ValidName_True()
        {
            //Arrange
            const int MINIMAL_VALID_SIZE = 1; 
            string validName = new Fixture().Create<string>(); 
            Garden garden = new Garden(MINIMAL_VALID_SIZE);

            //Act
            var result = garden.Plant(validName);

            //Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Plant_OverflowGarden_False()
        {
            //Arrange
            const int MINIMAL_VALID_SIZE = 1;
            string validName1 = new Fixture().Create<string>();
            //string validName2 = new Fixture().Create<string>();
            Garden garden = new Garden(MINIMAL_VALID_SIZE);
            garden.Plant(validName1);

            //Act
            var result = garden.Plant(validName1);

            //Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Plant_NullName_ArgumentNullException()
        {
            //Arrange
            const int INSIGNIFICANT_SIZE = default;
            Garden garden = new Garden(INSIGNIFICANT_SIZE);
            string? nullName = null;

            //Act
            garden.Plant(nullName);
        }

        [TestMethod]
        [DataRow("")]
        [DataRow("   ")]
        [DataRow("\n")]
        [DataRow("\t")]
        [DataRow(" \n\t")]
        public void Plant_EmptyOrWhitespaceName_ArgumentException(string name)
        {
            //Arrange
            const int INSIGNIFICANT_SIZE = default;
            Garden garden = new Garden(INSIGNIFICANT_SIZE);

            //Act
            Action action = () => garden.Plant(name);

            //Assert
            var exception = Assert.ThrowsException<ArgumentException>(action);
            Assert.AreEqual("name", exception.ParamName);
            Assert.IsTrue(exception.Message.StartsWith("Roœlina musi posiadaæ nazwê"));
        }

        [TestMethod]
        [Ignore]
        public void Plant_EmptyName_ArgumentException()
        {
        }

        [TestMethod]
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
            Assert.IsTrue(garden.GetPlants().Contains(expectedName));
        }

        [TestMethod]
        public void GetPlants_CopyOfPlantsCollection()
        {
            //Arrange
            const int INSIGNIFICANT_SIZE = default;
            Garden garden = new Garden(INSIGNIFICANT_SIZE);

            //Act
            var result1 = garden.GetPlants();
            var result2 = garden.GetPlants();

            //Assert
            Assert.AreNotSame(result1, result2);
        }

    }
}