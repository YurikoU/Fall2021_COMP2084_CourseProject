using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace SearchInRoomTests
{
    //[TestMethod] decorator: designed for only the test class
    [TestClass]
    public class CitiesControllerTests
    {
        #region Class level variables and methods

        #endregion


        #region Edit (GET)
        //[TestMethod] decorator: designed for a unit test method
        [TestMethod]
        //Check for null CityId
        public void EditLoads404IfCityidNull()
        {
            //Arrange(=given values)


            //Act(=when execution)

            //Assert(=result) 

        }

        //Check for invalid CityId
        [TestMethod]
        public void EditLoads404IfCityidInvalid()
        {
            //Arrange(=given values)


            //Act(=when execution)

            //Assert(=result) 

        }

        //Check for returning the correct data model (City obj)
        [TestMethod]
        public void EditLoadsCityIfCityidValid()
        {
            //Arrange(=given values)


            //Act(=when execution)

            //Assert(=result) 

        }

        //Check for returning the correct view
        [TestMethod]
        public void EditLoadsViewIfCityidValid()
        {
            //Arrange(=given values)


            //Act(=when execution)

            //Assert(=result) 

        }
        #endregion
    }
}
