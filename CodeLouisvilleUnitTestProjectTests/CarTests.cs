using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeLouisvilleUnitTestProject;
using FluentAssertions.Execution;
using FluentAssertions;
using System.Xml;
using Xunit;

namespace CodeLouisvilleUnitTestProjectTests
{
    public class CarTests
    {

        //Constructor: verify that newly created Car instances are also Vehicles and have 4 tires.
        [Fact]
        public void NewlyCreatedCarInstancesHave4Tires()
        {
            //arrange
            Car car = new Car();
            //act
            //assert
            using (new AssertionScope())
            {
                car.Should().BeAssignableTo<Vehicle>();
                car.NumberOfTires.Should().Be(4);
            }
        }

        //IsValidModelForMakeAsync test: Test that a Make of Honda and a Model of Civic is valid.
        //Test that a Make of Honda and a Model of Camry is not.
        //You may use two Facts or one Theory for this test.
        [Fact]
        public async void IsValidModelForMakeAsyncPositive()
        {
            //assert
            Car car = new Car(13, "Honda", "Civic", 25);
            //act
            bool validModel = await car.IsValidModelForMakeAsync();
            //assert
            validModel.Should().Be(true);

        }
        //WasModelMadeInYearAsync Positive Tests: Test that each of these values return the expected result (using a Theory would be a good idea):
        //A Make that does not exist at all returns false (regardless of model/year).
        //Make Honda, Model Camry returns false (regardless of year).
        //Make Subaru, Model WRX returns true for year 2020.
        //Make Subaru, Model WRX returns false for year 2000.
        [Theory]
        [InlineData("Honda", "Camry", 2030, false)]
        [InlineData("Subaru", "WRX", 2020, true)]
        [InlineData("Subaru", "WRX", 2000, false)]
        public async Task WasModelMadeInYearAsyncPositive(string make, string model, int year, bool returnResult)
        {
           //arrange
            Car car = new Car(13, make, model, 25);
           //act
            bool result = await car.WasModelMadeInYearAsync(year);
           //assert
           result.Should().Be(returnResult);
        }

    }
        //AddPassengers test: Test that adding passengers to the car reduces the fuel economy of the
        //car by .2 per passenger. Test that removing the passengers then adds back the fuel economy.
         [Fact]    
         public void AddPassengersReducesFuelEconomy()
         {
             //arrange
             Car car = new Car(13, "Honda", "Civic", 25);
             //act
             car.AddPassengers(5);
             //assert
             car.MilesPerGallon.Should().Be(24);
         }

}
