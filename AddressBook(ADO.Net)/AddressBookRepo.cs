using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;

namespace AddressBook_ADO.Net_
{
    public class AddressBookRepo
    {
        string connectionString = @"Data Source=DESKTOP-QP0QMA4\SQLEXPRESS;Initial Catalog=address_book_service;Integrated Security=True";
        SqlConnection connection;
        List<Contact> contacList = new List<Contact>();
        int count;
        public bool RetrieveFromDatabase()
        {
            connection = new SqlConnection(connectionString);
            try 
            {
                using (connection)
                {
                    string query = "Select Type,FirstName,LastName,Address,Contact.ZipCode,PhoneNo,Email,City,State"+ 
                                    " from AddressBook Inner JOIN  AddressBookContact On AddressBook.BookId = AddressBookContact.BookId"+
                                    " Inner Join Contact ON AddressBookContact.CId = Contact.CId"+
                                    " Inner Join Zip ON Contact.ZipCode = Zip.ZipCode;";

                    SqlCommand cmd = new SqlCommand(query, connection);
                    this.connection.Open();

                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            Contact c = new Contact();
                            c.Type = reader.GetString(0);
                            c.FirstName = reader.GetString(1);
                            c.LastName = reader.GetString(2);
                            c.Address = reader.GetString(3);
                            c.ZipCode = reader.GetString(4);
                            c.PhoneNo = reader.GetString(5);
                            c.Email = reader.GetString(6);
                            c.City = reader.GetString(7);
                            c.State = reader.GetString(8);

                            contacList.Add(c);

                            Console.WriteLine(c.Type + "  " + c.FirstName + "  " + c.LastName + "  " + c.Address + "  " + c.ZipCode + "  " +
                                c.City + "  " + c.State + "  " + c.PhoneNo + "  " + c.Email);
                        }
                    } 
                    else
                    {
                        Console.WriteLine("Table is empty");
                    }
                    return true;
                }
            
            }
            catch(Exception e) 
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                connection.Close();
            }
            return false;
        }
    }
}
