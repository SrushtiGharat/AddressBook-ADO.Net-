using Microsoft.VisualStudio.TestTools.UnitTesting;
using AddressBook_ADO.Net_;
using System.Collections.Generic;

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
        [TestMethod]
        public void Given_FirstAndLastName_Should_ReturnTrue()
        {
            string[] name = "Ram Singh".Split(" ");
            string phonNo = "9999999999";
            AddressBookRepo addressBookRepo = new AddressBookRepo();

            bool result = addressBookRepo.UpdateContact(name, phonNo);

            Assert.AreEqual(result, true);
        }
        [TestMethod]
        public void Given_WrongFirstAndLastName_Should_ReturnFalse()
        {
            string[] name = "Ram S".Split(" ");
            string phonNo = "9999999999";
            AddressBookRepo addressBookRepo = new AddressBookRepo();

            bool result = addressBookRepo.UpdateContact(name, phonNo);

            Assert.AreEqual(result, false);
        }
        [TestMethod]
        public void Given_ContactInfo_WhenAddedToDatabase_Should_Return_True()
        {
            AddressBookRepo addressBookRepo = new AddressBookRepo();

            Contact c = new Contact();
            c.Type = "Family";
            c.FirstName = "Manasi";
            c.LastName = "Rathi";
            c.Address = "04-Sai Nagar";
            c.ZipCode = "400500";
            c.City = "Mumbai";
            c.State = "Maharashtra";
            c.PhoneNo = "5555555555";
            c.Email = "manasi@gmail.com";

            bool result = addressBookRepo.AddContact(c);

            Assert.AreEqual(result, true);
        }
        [TestMethod]
        public void Given_MultipleContactInfo_WhenAddedToDatabaseUsingThreads_Should_Return_NoOfContactsAdded()
        {
            AddressBookRepo addressBookRepo = new AddressBookRepo();
            List<Contact> contactList = new List<Contact>();

            contactList.Add( new Contact(FirstName: "Shreya", LastName : "Pandey",Address: "14-Grace Pinnacle", ZipCode: "300203",
            City: "Hyderabad", State: "Telangana", PhoneNo: "9823439977",  Email: "shreya@gmail.com", Type: "Professional"));
            
            contactList.Add(new Contact(FirstName: "Raju", LastName: "Naik", Address: "12-Arcade Colony", ZipCode: "401602",
            City: "Ahmedabad", State: "Gujarat", PhoneNo: "7028827730", Email: "raju@gmail.com", Type: "Professional"));

            int result = addressBookRepo.AddMultipleContactsWithThread(contactList);

            Assert.AreEqual(2, result);
        }
    }
}
