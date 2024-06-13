using AutoFixture;

namespace ConsoleApp.Test.xUnit
{
    public class GardenTest
    {
        [Fact]
        //public void Plant_GivesTrueWhenValidName()
        //<nazwa metody>_<scenariusz>_<oczekiwany rezultat>
        //public void Plant_PassValidName_ReturnsTrue()
        public void Plant_ValidName_True()
        {
            //Arrange
            const int MINIMAL_VALID_SIZE = 1; // opisujemy swoje intencje
            //const string VALID_NAME = "a"; //piszemy testy z minimalnym przekazem (parametry o jak najmniejszym polem do interpretacji)
            string validName = new Fixture().Create<string>(); //mo¿emy wspomóc siê bibliotekami typu AutoFixture
            Garden garden = new Garden(MINIMAL_VALID_SIZE);
            
            //Act
            var result = garden.Plant(validName);

            //Assert
            //sprawdzamy tylko jedn¹ rzecz
            Assert.True(result);
        }

        [Fact]
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
            Assert.False(result);
        }

        [Fact]
        public void Plant_NullName_ArgumentNullException()
        {
            //Arrange
            const int INSIGNIFICANT_SIZE = default;
            Garden garden = new Garden(INSIGNIFICANT_SIZE);
            string? nullName = null;

            //Act
            Action action = () => garden.Plant(nullName);

            //Assert
            //ThrowsAny - uwzglêdnia dziedziczenie klas wyj¹tków
            //var exception = Assert.ThrowsAny<ArgumentException>(action);
            //Throws - sprawdza konkretny typ
            var exception = Assert.Throws<ArgumentNullException>(action);
            Assert.Equal("name", exception.ParamName);
        }

        [Theory]
        [InlineData("")]
        [InlineData("   ")]
        [InlineData("\n")]
        [InlineData("\t")]
        [InlineData(" \n\t")]
        public void Plant_EmptyOrWhitespaceName_ArgumentException(string name)
        {
            //Arrange
            const int INSIGNIFICANT_SIZE = default;
            Garden garden = new Garden(INSIGNIFICANT_SIZE);

            //Act
            var exception = Record.Exception(() => garden.Plant(name));

            //Assert
            Assert.NotNull(exception);
            var argumentException = Assert.IsType<ArgumentException>(exception);
            Assert.Equal("name", argumentException.ParamName);
            Assert.Contains("Roœlina musi posiadaæ nazwê", argumentException.Message);
        }

        [Fact(Skip = "Replaced by Plant_EmptyOrWhitespaceName_ArgumentException")]
        public void Plant_EmptyName_ArgumentException()
        {
            //Arrange
            const int INSIGNIFICANT_SIZE = default;
            Garden garden = new Garden(INSIGNIFICANT_SIZE);
            string emptyName = "";

            //Act
            var exception = Record.Exception(() => garden.Plant(emptyName));

            //Assert
            Assert.NotNull(exception);
            var argumentException = Assert.IsType<ArgumentException>(exception);
            Assert.Equal("name", argumentException.ParamName);
            Assert.Contains("Roœlina musi posiadaæ nazwê", argumentException.Message);
        }

        [Fact(Skip = "Replaced by Plant_EmptyOrWhitespaceName_ArgumentException")]
        public void Plant_WhitespaceName_ArgumentException()
        {
            //Arrange
            const int INSIGNIFICANT_SIZE = default;
            Garden garden = new Garden(INSIGNIFICANT_SIZE);
            string whitespaceName = "     \n";

            //Act
            var exception = Record.Exception(() => garden.Plant(whitespaceName));

            //Assert
            Assert.NotNull(exception);
            var argumentException = Assert.IsType<ArgumentException>(exception);
            Assert.Equal("name", argumentException.ParamName);
            Assert.Contains("Roœlina musi posiadaæ nazwê", argumentException.Message);
        }

        [Fact]
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
            Assert.Contains(expectedName, garden.GetPlants());
        }

        [Fact]
        public void GetPlants_CopyOfPlantsCollection()
        {
            //Arrange
            const int INSIGNIFICANT_SIZE = default;
            Garden garden = new Garden(INSIGNIFICANT_SIZE);

            //Act
            var result1 = garden.GetPlants();
            var result2 = garden.GetPlants();

            //Assert
            Assert.NotSame(result1, result2);
        }

        [Fact]
        public void alamakota()
        {
            Garden garden = new Garden(1);
            garden.Plant("a");

            garden.Remove("");
            garden.Remove("a");
        }

    }
}