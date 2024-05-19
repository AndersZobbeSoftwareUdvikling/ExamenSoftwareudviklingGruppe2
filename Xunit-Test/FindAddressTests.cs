using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Xunit_Test
{
    public class FindAddressTests
    {
        private readonly Address _address;

        public Address()
        {
            //Arrange: Setting up an address component
            _address = new Address();
        }
        [Fact]
        public void Test_InputtingCorrectAddress()
        {
            // Arrange: Insert the address into the searchbar
            string correctAddress = "H.C. Andersen Haven 1, 5000 Odense C";

            // Act: The provided address is now being searched for data
            var result = _address.FindAddressData(correctAddress);

            // Assert: The result screen displays the data provided by the correctly provided address
            Assert.NotNull(result);
            Assert.Equal(expectedData, result);
        }
        [Fact]
        public void Test_InputtingWrongAddress() 
        {
            // Arrange: Insert the address into the searchbar
            string wrongAddress = "H.C. Andersen Haven 42, 4100 Odense C";

            // Act: The provided address is now being searched for data
            var result = _address.FindAddressData(wrongAddress);

            // Assert: The results screen shows an error as the provided address is incorrect
            Assert.NotNull(result);
            Assert.Equal(unexpectedData, result="The address you have provided does not exist");
        }
        [Fact]
        public void Test_Inputting2CorrectAddresses() 
        {
            // Arrange: Insert the addresses into the searchbar
            string correctAddress1 = "H.C. Andersen Haven 1, 5000 Odense C";
            string correctAddress2 = "Seebladgade 1, 5000 Odense C";
            // Act: The provided addresses is now being searched for data
            var result = _address.FindAddressData(correctAddress1,correctAddress2);

            // Assert: The result screen displays the data provided by the correctly provided addresses
            Assert.NotNull(result);
            Assert.Equal(expectedData, result);
        }
        [Fact]
        public void Test_Inputting2WrongAddresses()
        {
            // Arrange: Insert the addresses into the searchbar
            string wrongAddress1 = "H.C. Andersen Haven 42, 4100 Odense C";
            string wrongAddress2 = "Seebladgade 59, 4220 Odense C";
            // Act: The provided addresses is now being searched for data
            var result = _address.FindAddressData(wrongAddress1, wrongAddress2);

            // Assert: The result screen displays the data provided by the correctly provided addresses
            Assert.NotNull(result);
            Assert.Equal(expectedData, result="Both the addresses you have provided do not exist");
        }
        [Fact]
        public void Test_Inputting1CorrectAnd1WrongAddress()
        {
            // Arrange: Insert the addresses into the searchbar
            string CorrectAddress = "H.C. Andersen Haven 1, 5000 Odense C";
            string wrongAddress = "Seebladgade 59, 4220 Odense C";
            // Act: The provided addresses is now being searched for data
            var result = _address.FindAddressData(CorrectAddress, wrongAddress);

            // Assert: The result screen displays the data provided by the correctly provided address, while also displaying an error on the wrong address
            Assert.NotNull(result);
            Assert.Equal(expectedData, result = CorrectAddress + "1 of the addresses you have provided do not exist");
        }
    }
}
