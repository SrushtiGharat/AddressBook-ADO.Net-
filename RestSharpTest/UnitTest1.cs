using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestSharp;
using AddressBook_ADO.Net_;
using System.Collections.Generic;
using Newtonsoft.Json;

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
    }
}
