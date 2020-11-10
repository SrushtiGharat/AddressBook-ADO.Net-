using System;

namespace AddressBook_ADO.Net_
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome To Address Book Ado.Net\n");

            Console.WriteLine("Getting values from table......");
            Console.WriteLine("-------------------------------------");
            AddressBookRepo addressBookRepo = new AddressBookRepo();
            addressBookRepo.RetrieveFromDatabase();
            Console.WriteLine("-------------------------------------");

            Console.WriteLine("1.Update Contact\n2.Get Contacts By Date Range\n3.Get Count By City And State\n4.Add Contact To Database");
            int choice = Convert.ToInt32(Console.ReadLine());

            switch (choice)
            {
                case 1:
                    Console.WriteLine("Enter name");
                    string[] name = Console.ReadLine().Split(" ");
                    Console.WriteLine("Enter phone no");
                    string phoneNo = Console.ReadLine();
                    addressBookRepo.UpdateContact(name, phoneNo);
                    break;
                case 2:
                    Console.WriteLine("Enter start date");
                    string startDate = Console.ReadLine();
                    Console.WriteLine("Enter end date");
                    string endDate = Console.ReadLine();
                    addressBookRepo.GetContactsByDateRange(startDate, endDate);
                    break;
                case 3:
                    addressBookRepo.GetCountByCityOrState();
                    break;
                case 4:
                    Contact c = new Contact();
                    Console.WriteLine("Enter Contact Type");
                    c.Type = Console.ReadLine();
                    Console.WriteLine("Enter First Name");
                    c.FirstName = Console.ReadLine();
                    Console.WriteLine("Enter Last Name");
                    c.LastName = Console.ReadLine();
                    Console.WriteLine("Enter Address");
                    c.Address = Console.ReadLine();
                    Console.WriteLine("Enter ZipCode");
                    c.ZipCode = Console.ReadLine();
                    Console.WriteLine("Enter City");
                    c.City= Console.ReadLine();
                    Console.WriteLine("Enter State");
                    c.State = Console.ReadLine();
                    Console.WriteLine("Enter PhoneNo");
                    c.PhoneNo = Console.ReadLine();
                    Console.WriteLine("Enter Email");
                    c.Email = Console.ReadLine();
                    addressBookRepo.AddContact(c);
                    break;
                default:
                    Console.WriteLine("Invalid Option");
                    break;
            }

        }  
    }
}
        

    

