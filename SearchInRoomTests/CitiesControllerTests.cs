using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SearchInRoomTests
{
    //[TestMethod] decorator: designed for only the test class
    [TestClass]
    public class CitiesControllerTests
    {
        #region Edit (GET)
        //Method underneath [TestMethod] decorator is a unit test
        [TestMethod]
        //Check for null CityId
        public void EditLoads404IfCityidNull()
        {
        }

        //Check for invalid CityId
        [TestMethod]
        public void EditLoads404IfInvalidCityid()
        { 
        }

        //Check for returning the correct data model (City obj)
        [TestMethod]
        public void EditLoadsCityIfValidCityid()
        { 
        }

        //Check for returning the correct view
        [TestMethod]
        public void EditLoadsViewIfValidCityid()
        { 
        }
        #endregion
    }
}
