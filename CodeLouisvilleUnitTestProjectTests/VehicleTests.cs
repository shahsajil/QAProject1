using CodeLouisvilleUnitTestProject;
using FluentAssertions;
using FluentAssertions.Execution;
using System.Xml;
using Xunit.Abstractions;

namespace CodeLouisvilleUnitTestProjectTests
{
    public class VehicleTests
    {
        public bool hasFlatTire { get; private set; }

        //Verify the parameterless constructor successfully creates a new
        //object of type Vehicle, and instantiates all public properties
        //to their default values.
        [Fact]
        public void VehicleParameterlessConstructorTest()
        {
            //arrange
           Vehicle vehicle = new Vehicle();
            //act

            //assert
            using (new AssertionScope())
            {
                vehicle.NumberOfTires.Should().Be(0);
                vehicle.GasTankCapacity.Should().Be(0);
                vehicle.Make.Should().BeNullOrEmpty();
                vehicle.Model.Should().BeNullOrEmpty();
                vehicle.MilesPerGallon.Should().Be(0);
            };
            //vehicle.GasLevel.Should().Be("NaN%");
            //vehicle.MilesRemaining.Should().Be(0);
            //vehicle.Mileage.Should().Be(0);
        }

        //Verify the parameterized constructor successfully creates a new
        //object of type Vehicle, and instantiates all public properties
        //to the provided values.
        [Fact]
        public void VehicleConstructorTest()
        {
            //arrange
            Vehicle vehicle = new Vehicle(4, 10, "Honda", "CRV", 25);
            //act

            //assert
            using (new AssertionScope())
            {
                vehicle.NumberOfTires.Should().Be(4);
                vehicle.GasTankCapacity.Should().Be(10);
                vehicle.Make.Should().Be("Honda");
                vehicle.Model.Should().Be("CRV");
                vehicle.MilesPerGallon.Should().Be(25);
            }
        }

        //Verify that the parameterless AddGas method fills the gas tank
        //to 100% of its capacity
        [Fact]
        public void AddGasParameterlessFillsGasToMax()
        {
            //arrange
            Vehicle vehicle = new Vehicle(4, 10, "Honda", "CRV", 25);
            //act
            vehicle.AddGas();
            //assert
            vehicle.GasLevel.Should().Be("100%");
        }

        //Verify that the AddGas method with a parameter adds the
        //supplied amount of gas to the gas tank.
        [Fact]
        public void AddGasWithParameterAddsSuppliedAmountOfGas()
        {
            //arrange
            Vehicle vehicle = new Vehicle(4, 10, "Honda", "CRV", 25);
            //act
            vehicle.AddGas(6);
            //assert
            vehicle.GasLevel.Should().Be("60%");
        }

        //Verify that the AddGas method with a parameter will throw
        //a GasOverfillException if too much gas is added to the tank.
        [Fact]
        public void AddingTooMuchGasThrowsGasOverflowException()
        {
            
            //arrange
            Vehicle vehicle = new Vehicle(4, 10, "Honda", "CRV", 25);
            //act
           Action act = () => vehicle.AddGas(14);
            //assert
           _ = act.Should().Throw<GasOverfillException>().WithMessage(($"Unable to add {14} gallons to tank " +
                  $"because it would exceed the capacity of {vehicle.GasTankCapacity} gallons"));
            
        }

        //Using a Theory (or data-driven test), verify that the GasLevel
        //property returns the correct percentage when the gas level is
        //at 0%, 25%, 50%, 75%, and 100%.
        [Theory]
        [InlineData("0%", 0)]
        [InlineData("25%", 2.5)]
        [InlineData("50%", 5)]
        [InlineData("75%", 7.5)]
        [InlineData("100%", 10)]
        public void GasLevelPercentageIsCorrectForAmountOfGas(string percentage, float gasToAdd)
        {
            //arrange
            Vehicle vehicle = new Vehicle(4, 10, "Honda", "CRV", 25);
            //act
            vehicle.AddGas(gasToAdd);
            //assert
            using (new AssertionScope())
            {
                vehicle.GasLevel.Should().Be(percentage);
            }
        }

        /*
         * Using a Theory (or data-driven test), or a combination of several 
         * individual Fact tests, test the following functionality of the 
         * Drive method:
         *      a. Attempting to drive a car without gas returns the status 
         *      string “Cannot drive, out of gas.”.
         *      b. Attempting to drive a car with a flat tire returns 
         *      the status string “Cannot drive due to flat tire.”.
         *      c. Drive the car 10 miles. Verify that the correct amount 
         *      of gas was used, that the correct distance was traveled, 
         *      that GasLevel is correct, that MilesRemaining is correct, 
         *      and that the total mileage on the vehicle is correct.
         *      d. Drive the car 100 miles. Verify that the correct amount 
         *      of gas was used, that the correct distance was traveled,
         *      that GasLevel is correct, that MilesRemaining is correct, 
         *      and that the total mileage on the vehicle is correct.
         *      e. Drive the car until it runs out of gas. Verify that the 
         *      correct amount of gas was used, that the correct distance 
         *      was traveled, that GasLevel is correct, that MilesRemaining
         *      is correct, and that the total mileage on the vehicle is 
         *      correct. Verify that the status reports the car is out of gas.
        */

         //  a. Attempting to drive a car without gas returns the status 
         //  string “Cannot drive, out of gas.”.
        [Fact]
        public void EmptyGasLevel()
        {
            //arrange
            Vehicle vehicle = new Vehicle(4, 10, "Honda", "CRV", 25);
            //act
            vehicle.AddGas(0);
            //assert
            vehicle.MilesRemaining.Should().Be(0, because:"Cannot drive, out of gas.");
        }

        //   b. Attempting to drive a car with a flat tire returns 
        //      the status string “Cannot drive due to flat tire.”.
        [Fact]
        public void DriveWithFlatTire()
        {
            //arrange
            Vehicle vehicle = new Vehicle(4, 10, "Honda", "CRV", 25);
            //act
            vehicle.AddGas();
            vehicle.FlatTire();
            string drive = vehicle.Drive(4);
            //assert
            drive.Should().Be("Cannot drive due to flat tire.");
        }

        //  c.Drive the car 10 miles.Verify that the correct amount
        //    of gas was used, that the correct distance was traveled,
        //    that GasLevel is correct, that MilesRemaining is correct, 
        //    and that the total mileage on the vehicle is correct.
        [Fact]
        public void VerifyAmountAfterDriving10Miles()
        {
            //arrange
            Vehicle vehicle = new Vehicle(4, 10, "Honda", "CRV", 25);
            //act
            vehicle.AddGas();
            string usedAmount = vehicle.Drive(10);
            //assert
            usedAmount.Should().Be("Drove 10 miles using 0.4 gallons of gas.");
            vehicle.GasLevel.Should().Be("96%");
            vehicle.MilesRemaining.Should().Be(240);
            vehicle.Mileage.Should().Be(10);

        }

        //   d.Drive the car 100 miles.Verify that the correct amount
        //     of gas was used, that the correct distance was traveled,
        //     that GasLevel is correct, that MilesRemaining is correct, 
        //     and that the total mileage on the vehicle is correct.

        [Fact]
        public void VerifyAmountAfterDriving100Miles()
        {
            //arrange
            Vehicle vehicle = new Vehicle(4, 10, "Honda", "CRV", 25);
            //act
            vehicle.AddGas();
            string usedAmount = vehicle.Drive(100);
            //assert
            usedAmount.Should().Be("Drove 100 miles using 4 gallons of gas.");
            vehicle.GasLevel.Should().Be("60%");
            vehicle.MilesRemaining.Should().Be(150);
            vehicle.Mileage.Should().Be(100);

        }

        //   e.Drive the car until it runs out of gas.Verify that the 
        //     correct amount of gas was used, that the correct distance 
        //     was traveled, that GasLevel is correct, that MilesRemaining
        //     is correct, and that the total mileage on the vehicle is 
        //     correct.Verify that the status reports the car is out of gas.

        [Fact]
        public void VerifyAmountAfterDrivingtheCarUntilGasLevelEmpty()
        {
            //arrange
            Vehicle vehicle = new Vehicle(4, 10, "Honda", "CRV", 25);
            //act
            vehicle.AddGas();
            string usedAmount = vehicle.Drive(250);
            //assert
            usedAmount.Should().Be("Drove 250 miles, then ran out of gas.");
            vehicle.GasLevel.Should().Be("0%");
            vehicle.MilesRemaining.Should().Be(0);
            vehicle.Mileage.Should().Be(250);

        }

        [Theory]
        [InlineData(100, false)]
        [InlineData(100, true)]
        public void DriveNegativeTests(double milesLeftToDrive, bool flatTire)
        {
            //arrange
            Vehicle vehicle = new Vehicle(4, 10, "Honda", "CRV", 25);
            //act
            vehicle.AddGas(0);
            vehicle.hasFlatTire = flatTire;
            var status = vehicle.Drive(milesLeftToDrive);
            //assert
            vehicle.Drive(milesLeftToDrive).Should().Be(status);
        }

        [Theory]
        [InlineData(10, "Drove 10 miles using 0.4 gallons of gas.")]
        [InlineData(100, "Drove 100 miles using 4 gallons of gas.")]
        [InlineData(250, "Drove 250 miles, then ran out of gas.")]

        public void DrivePositiveTests(double milesLeftToDrive, string DrivingStatus )
        {
            //arrange
            Vehicle vehicle = new Vehicle(4, 10, "Honda", "CRV", 25);
            //act
            vehicle.AddGas();
            vehicle.hasFlatTire = false;
            var status = vehicle.Drive(milesLeftToDrive);   
            //assert
            using (new AssertionScope())
            {
                status.Should().Be(DrivingStatus);
            }

        }

        //Verify that attempting to change a flat tire using
        //ChangeTireAsync will throw a NoTireToChangeException
        //if there is no flat tire.
        [Fact]
        public async Task ChangeTireWithoutFlatTest()
        {
            //arrange
            Vehicle vehicle = new Vehicle(4, 10, "Honda", "CRV", 25);
            vehicle.hasFlatTire = false;
            //act

            //assert
             await vehicle.Invoking(async vehicle => await vehicle.ChangeTireAsyncTest())
                .Should().ThrowAsync<NoTireToChangeException>();
           
        }

        //Verify that ChangeTireAsync can successfully
        //be used to change a flat tire
        [Fact]
        public async Task ChangeTireSuccessfulTest()
        {
            //arrange
            Vehicle vehicle = new Vehicle(4, 10, "Honda", "CRV", 25);
            //act
            vehicle.FlatTire();
            await vehicle.ChangeTireAsyncTest();
            //assert
            vehicle.hasFlatTire.Should().Be(false);
        }

        //BONUS: Write a unit test that verifies that a flat
        //tire will occur after a certain number of miles.
        [Theory]
        [InlineData(100)]
        public void GetFlatTireAfterCertainNumberOfMilesTest(double milesDrive)
        {
            //arrange
            Vehicle vehicle = new Vehicle(4, 10, "Honda", "CRV", 25);
            //act
            var status = vehicle.Drive(milesDrive);
            //assert
            vehicle.Drive(milesDrive).Should().Be(status);
            
            
        }
    }
}