using CodeLouisvilleUnitTestProject;
using FluentAssertions;
using FluentAssertions.Execution;

namespace CodeLouisvilleUnitTestProjectTests
{
    public class SemiTruckTests
    {
        //Verify that the SemiTruck constructor creates a new SemiTruck
        //object which is also a Vehicle and has 18 wheels. Verify that the
        //Cargo property for the newly created SemiTruck is a List of
        //CargoItems which is empty, but not null.
        [Fact]
        public void NewSemiTruckIsAVehicleAndHas18TiresAndEmptyCargoTest()
        {
            //arrange
            SemiTruck semiTruck = new SemiTruck();
            //act

            //assert
            using (new AssertionScope())
            {
                semiTruck.NumberOfTires.Should().Be(18);
                semiTruck.Cargo.Should().BeOfType<List<CargoItem>>();
                semiTruck.Cargo.Should().BeEmpty();
                semiTruck.Cargo.Should().NotBeNull();
            }
            
        }

        //Verify that adding a CargoItem using LoadCargo does successfully add
        //that CargoItem to the Cargo. Confirm both the existence of the new
        //CargoItem in the Cargo and also that the count of Cargo increased to 1.
        [Fact]
        public void LoadCargoTest()
        {
            //arrange
            SemiTruck semiTruck = new SemiTruck();
            CargoItem cargoItem = new CargoItem();
            //act
            semiTruck.LoadCargo(cargoItem);
            //assert
            using (new AssertionScope())
            {
                semiTruck.Cargo.Should().Contain(cargoItem);
                semiTruck.Cargo.Count.Should().Be(1);
            }
        }

        //Verify that unloading a  cargo item that is in the Cargo does
        //remove it from the Cargo and return the matching CargoItem
        [Fact]
        public void UnloadCargoWithValidCargoTest()
        {
            //arrange
            SemiTruck semiTruck = new SemiTruck();
            CargoItem cargoItem = new CargoItem();
            //act
            semiTruck.Cargo.Remove(cargoItem);
            //assert
            semiTruck.Cargo.Should().NotContain(cargoItem);
        }

        //Verify that attempting to unload a CargoItem that does not
        //appear in the Cargo throws a System.ArgumentException
        [Fact]
        public void UnloadCargoWithInvalidCargoTest()
        {
            //arrange
            SemiTruck semiTruck = new SemiTruck();
            //act
            semiTruck.UnloadCargo("CargoItem");
            //assert
            semiTruck.Invoking(semiTruck => semiTruck.UnloadCargo("CargoItem"))
                .Should().Throw<ArgumentException>();
        }

        //Verify that getting cargo items by name returns all items
        //in Cargo with that name.
        [Fact]
        public void GetCargoItemsByNameWithValidName()
        {
            //arrange
            SemiTruck semiTruck = new SemiTruck();
            CargoItem cargoItem = new CargoItem()
            { Name = "cargoItem", Description = "The Merchandise", Quantity =10};
            //act
            semiTruck.LoadCargo(cargoItem);
            //assert
            semiTruck.GetCargoItemsByName("cargoItem").Should().Contain(cargoItem);
        }

        //Verify that searching the Carto list for an item that does not
        //exist returns an empty list
        [Fact]
        public void GetCargoItemsByNameWithInvalidName()
        {
            //arrange
            SemiTruck semiTruck = new SemiTruck();
            CargoItem cargoItem = new CargoItem()
            { Name = "cargoItem", Description = "The Merchandise", Quantity = 10};
            //act
            semiTruck.LoadCargo(cargoItem);
            //assert
            semiTruck.GetCargoItemsByName("Item").Should().BeEmpty();
        }

        //Verify that searching the Cargo list by description for an item
        //that does exist returns all matched items that contain that description.
        [Fact]
        public void GetCargoItemsByPartialDescriptionWithValidDescription()
        {
            //arrange
            SemiTruck semiTruck = new SemiTruck();
            CargoItem cargoItem = new CargoItem()
            { Name = "cargoItem", Description = "The Merchandise", Quantity = 1 };
            //act
            semiTruck.LoadCargo(cargoItem);
            //assert
            semiTruck.GetCargoItemsByPartialDescription("The Merchandise").Should().Contain(cargoItem);
        }

        //Verify that searching the Carto list by description for an item
        //that does not exist returns an empty list
        [Fact]
        public void GetCargoItemsByPartialDescriptionWithInvalidDescription()
        {
            //arrange
            SemiTruck semiTruck = new SemiTruck();
            CargoItem cargoItem = new CargoItem()
            { Name = "cargoItem", Description = "The Merchandise", Quantity = 1 };
            //act
            semiTruck.LoadCargo(cargoItem);
            //assert
            semiTruck.GetCargoItemsByPartialDescription("The Food").Should().BeEmpty();
        }

        //Verify that the method returns the sum of all quantities of all
        //items in the Cargo
        [Fact]
        public void GetTotalNumberOfItemsReturnsSumOfAllQuantities()
        {
            //arrange
            SemiTruck semiTruck = new SemiTruck();
            CargoItem cargoItem = new CargoItem()
            { Name = "cargoItem", Description = "The Merchandise", Quantity = 1 };
            //act
            semiTruck.LoadCargo(cargoItem);
            //assert
            semiTruck.GetTotalNumberOfItems().Should().Be(1);
        }
    }
}
