using System;
using System.Configuration;
//using Dapper.Models;
using Dapper.Repository;

namespace Dapper.ConsoleRun
{
    class Program
    {
        //static async System.Threading.Tasks.Task Main(string[] args)
        static void Main(string[] args)
        {
            //// Create a reference to the database.  The using statement will ensure the Database connections are cleaned up after use.
            //using var connection = ConnectionFactory.GetConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);

            //// Get a new reference to customer respository
            //var customerRespository = new CustomerRespository(connection);

            //// Create a new customer
            //var newCustomer = new Customer
            //{
            //    FirstName = "Test",
            //    LastName = "Last",
            //    CreatedDateTime = DateTime.Now
            //};

            //// Insert a new customer
            //var insertedCustomer = await customerRespository.Insert(newCustomer);
            //if (insertedCustomer.CustomerId != 0)
            //{
            //    Console.WriteLine($"New Customer: {insertedCustomer.CustomerId}  {insertedCustomer.FirstName}  {insertedCustomer.LastName}  {insertedCustomer.CreatedDateTime}");
            //    Console.WriteLine();

            //    // Retrieve a list of all customers
            //    var customers = await customerRespository.GetAll();

            //    Console.WriteLine("List of all Customers");
            //    foreach (var customer in customers)
            //    {
            //        Console.WriteLine($"  {customer.CustomerId}  {customer.FirstName}  {customer.LastName}  {customer.CreatedDateTime}  {customer.CreatedDateTime.ToShortDateString()}  {customer.CreatedDateTime.ToShortTimeString()}");
            //    }

            //    // Delete the customer we inserted above
            //    var isDeleted = await customerRespository.Delete(insertedCustomer.CustomerId);
            //    if (isDeleted)
            //    {
            //        Console.WriteLine();
            //        Console.WriteLine($"Deleted New Customer: {insertedCustomer.CustomerId}  {insertedCustomer.FirstName}  {insertedCustomer.LastName}  {insertedCustomer.CreatedDateTime}");
            //    }

            //    // Retrieve the first customer and display its results
            //    var firstCustomer = await customerRespository.GetById(1);
            //    if (firstCustomer != null)
            //    {
            //        Console.WriteLine();
            //        Console.WriteLine($"Retrieved First Customer: {firstCustomer.CustomerId}  {firstCustomer.FirstName}  {firstCustomer.LastName}  {firstCustomer.CreatedDateTime}");
            //    }

            //    // Update the first customer
            //    //firstCustomer.FirstName = "New FirstName";
            //    //var isUpdated = await customerRespository.Update(firstCustomer);
            //    //if (isUpdated)
            //    //    {
            //    //        Console.WriteLine();
            //    //        Console.WriteLine($"Updated First Customer: {firstCustomer.CustomerId}  {firstCustomer.FirstName}  {firstCustomer.LastName}  {firstCustomer.CreatedDateTime}");
            //    //    }

            //    Console.ReadLine();
        
        }
    }
}

