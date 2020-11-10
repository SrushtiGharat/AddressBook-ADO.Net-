using System;
using System.Collections.Generic;
using System.Text;

namespace AddressBook_ADO.Net_
{
    public class Contact
    {
        public Contact() { }
        public Contact(string FirstName, string LastName, string Address, string ZipCode, string City, string State, string PhoneNo, string Email, string Type)
        {
            this.FirstName = FirstName;
            this.LastName = LastName;
            this.Address = Address;
            this.ZipCode = ZipCode;
            this.City = City;
            this.State = State;
            this.PhoneNo = PhoneNo;
            this.Email = Email;
            this.Type = Type;
        }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ZipCode { get; set; }
        public string PhoneNo { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Type { get; set; }
    }
}
