using System;
using System.Data;
using System.Data.SqlClient;

namespace NorthwindReader
{
    class Program
    {
        static void Main(string[] args)
        {
            string connectionString = "Server=(localdb)\\MSSQLLocalDB;Database=Northwind;Trusted_Connection=True;";

            string query = @"
                SELECT p.ProductID, p.ProductName, c.CategoryName
                FROM Products p
                INNER JOIN Categories c ON p.CategoryID = c.CategoryID
                ORDER BY p.ProductName;
            ";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                    DataSet dataSet = new DataSet();

                    adapter.Fill(dataSet, "ProductCategories");

                    DataTable table = dataSet.Tables["ProductCategories"];

                    Console.WriteLine("Lista proizvoda i njihovih kategorija:");
                    Console.WriteLine("----------------------------------------");

                    foreach (DataRow row in table.Rows)
                    {
                        Console.WriteLine($"ID: {row["ProductID"]}, Naziv: {row["ProductName"]}, Kategorija: {row["CategoryName"]}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Greška: " + ex.Message);
            }

            Console.WriteLine("\nPritisnite taster za izlaz...");
            Console.ReadKey();
        }
    }
}
