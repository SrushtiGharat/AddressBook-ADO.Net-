﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Linq;

namespace AddressBook_ADO.Net_
{
    public class AddressBookRepo
    {
        string connectionString = @"Data Source=DESKTOP-QP0QMA4\SQLEXPRESS;Initial Catalog=address_book_service;Integrated Security=True";
        SqlConnection connection;

        /// <summary>
        /// Get all contacts from database
        /// </summary>
        /// <returns>True Or False</returns>
        public bool RetrieveFromDatabase()
        {
            connection = new SqlConnection(connectionString);
            try
            {
                using (connection)
                {
                    string query = "Select Type,FirstName,LastName,Address,Contact.ZipCode,PhoneNo,Email,City,State" +
                                    " from AddressBook Inner JOIN  AddressBookContact On AddressBook.BookId = AddressBookContact.BookId" +
                                    " Inner Join Contact ON AddressBookContact.CId = Contact.CId" +
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
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                connection.Close();
            }
            return false;
        }

        /// <summary>
        /// Update phone No Given FullName
        /// </summary>
        /// <param name="name"></param>
        /// <param name="phoneNo"></param>
        /// <returns></returns>
        public bool UpdateContact(string[] name, string phoneNo)
        {
            connection = new SqlConnection(connectionString);
            try
            {
                using (connection)
                {
                    SqlCommand command = new SqlCommand("SpUpdateDetails", this.connection);
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@FirstName", name[0]);
                    command.Parameters.AddWithValue("@LastName", name[1]);
                    command.Parameters.AddWithValue("@PhoneNo", phoneNo);

                    this.connection.Open();
                    var result = command.ExecuteNonQuery();
                    if (result == 0)
                    {
                        Console.WriteLine("No such contact");
                        return false;
                    }
                    Console.WriteLine("Contact updated successfully");
                    return true;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                connection.Close();
            }
            return false;
        }

        /// <summary>
        /// Get contacts added in a particular date range
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public void GetContactsByDateRange(string startDate, string endDate)
        {
            connection = new SqlConnection(connectionString);
            try
            {
                DateTime StartDate = Convert.ToDateTime(startDate);
                DateTime EndDate = Convert.ToDateTime(endDate);
                using (connection)
                {
                    SqlCommand command = new SqlCommand("SpGetContactsByDateRange", this.connection);
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@StartDate", StartDate);
                    command.Parameters.AddWithValue("@EndDate", EndDate);
                    this.connection.Open();

                    SqlDataReader reader = command.ExecuteReader();
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

                            Console.WriteLine(c.Type + "  " + c.FirstName + "  " + c.LastName + "  " + c.Address + "  " + c.ZipCode + "  " +
                                c.City + "  " + c.State + "  " + c.PhoneNo + "  " + c.Email);
                        }
                    }
                    else
                    {
                        Console.WriteLine("No such records found");
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                connection.Close();
            }
        }

        public void GetCountByCityOrState()
        {
            connection = new SqlConnection(connectionString);
            try
            {
                using (connection)
                {
                    SqlCommand command = new SqlCommand("SpGetCountByCityState", this.connection);
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    this.connection.Open();

                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            string state = reader.GetString(0);
                            string city = reader.GetString(1);
                            int count = reader.GetInt32(2);                            

                            Console.WriteLine(state+"  "+city+"  "+count);
                        }
                    }
                    else
                    {
                        Console.WriteLine("Table is empty");
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                connection.Close();
            }
        }
    }
}
