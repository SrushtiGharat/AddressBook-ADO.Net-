using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestSharp;
using AddressBook_ADO.Net_;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace RestSharpTest
{
    [TestClass]
    public class UnitTest1
    {
        RestClient client = new RestClient("http://localhost:3000");

        /// <summary>
        /// Get list of contacts in Address Book using json server
        /// </summary>
        /// <returns></returns>
        private IRestResponse getContactList()
        {
            RestRequest request = new RestRequest("/Contacts", Method.GET);

            IRestResponse response = client.Execute(request);
            return response;
        }

        /// <summary>
        /// Test method to check the count of the contacts in the  list
        /// </summary>
        [TestMethod]
        public void OnCallingGETApi_ReturnEmployeeList()
        {
            IRestResponse response = getContactList();

            Assert.AreEqual(response.StatusCode, System.Net.HttpStatusCode.OK);
            List<Contact> dataResponse = JsonConvert.DeserializeObject<List<Contact>>(response.Content);

            Assert.AreEqual(9, dataResponse.Count);
        }

        /// <summary>
        /// Add multiple Contacts to json server and check count
        /// </summary>
        [TestMethod]
        public void GivenMultipleContacts_OnPost_ShouldReturn_TotalCount()
        {
            List<Contact> contactList = new List<Contact>();
            contactList.Add(new Contact { FirstName = "Sara", LastName = "Pandit", Address = "21-Jump Street",ZipCode = "300200", City = "Ahmedabad", State = "Gujarat",PhoneNo = "9120384565",Email = "sara@gmail.com",Type = "Friends"});
            contactList.Add(new Contact { FirstName = "Rohit", LastName = "Sankhe", Address = "27-Dalal Tower", ZipCode = "401501", City = "Mumbai", State = "Maharashtra", PhoneNo = "9999988888", Email = "rohit@gmail.com", Type = "Profession"});

            foreach (Contact contact in contactList)
            {
                RestRequest request = new RestRequest("/Contacts", Method.POST);
                JObject jObjectbody = new JObject();
                jObjectbody.Add("FirstName", contact.FirstName);
                jObjectbody.Add("LastName", contact.LastName);
                jObjectbody.Add("Address", contact.Address);
                jObjectbody.Add("ZipCode", contact.ZipCode);
                jObjectbody.Add("City", contact.City);
                jObjectbody.Add("State", contact.State);
                jObjectbody.Add("PhoneNo", contact.PhoneNo);
                jObjectbody.Add("Email", contact.Email);
                jObjectbody.Add("Type", contact.Type);
                
                request.AddParameter("application/json", jObjectbody, ParameterType.RequestBody);

                IRestResponse response = client.Execute(request);

                Assert.AreEqual(response.StatusCode, System.Net.HttpStatusCode.Created);
            }

            IRestResponse c = getContactList();
            List<Contact> dataResponse = JsonConvert.DeserializeObject<List<Contact>>(c.Content);

            Assert.AreEqual(11, dataResponse.Count);
        }

        /// <summary>
        /// Update contact information
        /// </summary>
        [TestMethod]
        public void Given_NewEmailAddress_ShouldReturn_UpdatedContact()
        {
            RestRequest request = new RestRequest("/Contacts/3", Method.PATCH);
            JObject jObjectBody = new JObject();
            jObjectBody.Add("Email", "ravi@gmail.com");
            request.AddParameter("application/json", jObjectBody, ParameterType.RequestBody);

            IRestResponse response = client.Execute(request);

            Assert.AreEqual(response.StatusCode, System.Net.HttpStatusCode.OK);
            Contact dataResponse = JsonConvert.DeserializeObject<Contact>(response.Content);
            Assert.AreEqual("Ravi", dataResponse.FirstName);
            Assert.AreEqual("ravi@gmail.com", dataResponse.Email);
        }
    }
}
