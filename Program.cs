using System;
using ConsoleApp2.Template;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace ConsoleApp2
{

    class Program
    {
        SqlConnection connection;
        string connectionString  = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Programming\\C#\\ConsoleApp2\\TimeZoneDatabase.mdf;Integrated Security=True";

        // string[] timeZones = {"utc","gmt"};
        public static void Main(string[] args)
        {

            Program manage = new Program();
            manage.print_locations();
            // Template.TimeZone interestTimeZone = fetch_request();


            // Print Result
            // Console.WriteLine(interestTimeZone.gmtOffset);
            // Console.WriteLine(interestTimeZone.name);

            //DateTime localDate = DateTime.Now;
            //int currentHour = localDate.TimeOfDay.Hours;
            //int currentMin = localDate.TimeOfDay.Minutes;
            // public static DateTime Now { get; }
            //Console.WriteLine("Cur Time = {0}:{1}", currentHour, currentMin);
            //Console.WriteLine($"Cur Time = {currentHour}:{currentMin}");
        }

        // Fetch from user
        static Template.TimeZone fetch_request()   
        {
            //try
            //{
            Console.WriteLine("\nWhat time?:");
            string targetTime = Console.ReadLine();
            int targetHour = Convert.ToInt32(targetTime.Substring(0, 2));
            int targetMin = Convert.ToInt32(targetTime.Substring(targetTime.Length - 2, 2));
            Console.WriteLine("\nTime Zone Select:");
            string timeZoneName = Console.ReadLine();
            Console.WriteLine("\nOffset:");
            int offset = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine($"targetTime = {targetTime}  timeZone = {timeZoneName}  offset = {offset}");
            Console.WriteLine($"Input Time = {targetHour}:{targetMin}");
            //}
            //catch (FormatException e)
            //{
            //    // Console.WriteLine($"Unable to parse {e}");
            //    Console.WriteLine(e.Message);
            //}
            Template.TimeZone interestTimeZone = new Template.TimeZone (timeZoneName, offset);


            return interestTimeZone;

           
        }

        private void print_locations()
        {
            Program data = new Program();
            Console.WriteLine("Getting Connection ...");
            data.connection = new SqlConnection(data.connectionString); //create instanace of database connection
            try
            {
                Console.WriteLine("Openning Connection ...");
                data.connection.Open(); //open connection
                Console.WriteLine("Connection successful!");
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e.Message);
            }


            string query = "SELECT City FROM Locations";
            using (data.connection = new SqlConnection(data.connectionString))
            using (SqlDataAdapter adapter = new SqlDataAdapter(query, data.connection))
            {
                DataTable citiesTable = new DataTable();
                adapter.Fill(citiesTable);

                foreach (DataRow dataRow in citiesTable.Rows)
                {
                    foreach (var item in dataRow.ItemArray)
                    {
                        Console.WriteLine(item);
                    }
                }

            }
        }

        


    }
}
