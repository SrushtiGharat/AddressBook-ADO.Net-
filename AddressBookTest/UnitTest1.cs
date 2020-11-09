using Microsoft.VisualStudio.TestTools.UnitTesting;
using AddressBook_ADO.Net_;

namespace AddressBookTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void Given_RetrieveContactsFromDatabase_ShouldReturnCount()
        {
            AddressBookRepo addressBookRepo = new AddressBookRepo();

            bool result = addressBookRepo.RetrieveFromDatabase();

            Assert.AreEqual(result, true);
        }
    }
}
