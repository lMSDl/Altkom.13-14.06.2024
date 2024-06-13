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
    }
}