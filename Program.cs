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
        int offset;
        int resultHour;

        // string[] timeZones = {"utc","gmt"};
        public static void Main(string[] args)
        {

            Program manage = new Program();
            
            // manage.print_locations();
            Template.TimeZone interestTimeZone = fetch_request();
            int convertedHour = manage.convert_time(interestTimeZone);
            Console.WriteLine($"Result time: {convertedHour}:{interestTimeZone.interestMin}");
        }

        // Fetch from user
        static Template.TimeZone fetch_request()   
        {

            Console.WriteLine("\nWhat time?:");
            string targetTime = Console.ReadLine();
            string targetHour = targetTime.Substring(0, 2);
            string targetMin = targetTime.Substring(targetTime.Length - 2, 2);
            Console.WriteLine("\nTime Zone Select:");
            string timeZoneName = Console.ReadLine();
            Console.WriteLine("\nOffset:");
            int offset = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine($"targetTime = {targetTime}  timeZone = {timeZoneName}  offset = {offset}");
            Console.WriteLine($"Input Time = {targetHour}:{targetMin}");

            Template.TimeZone interestTimeZone = new Template.TimeZone(timeZoneName, offset, targetHour, targetMin);


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

        private int convert_time(Template.TimeZone interestTimeZone)
        {
            Program data = new Program();
            string query = $"SELECT GmtOffset FROM Zone where ZoneName = '{interestTimeZone.name.ToUpper()}'";
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
                        offset = Convert.ToInt32(item);
                    }
                }

            }

            resultHour = Convert.ToInt32(interestTimeZone.interestHour) + interestTimeZone.interestOffset + offset;
            return resultHour;
        }


    }
}


//try
//{
//}
//catch (FormatException e)
//{
//    // Console.WriteLine($"Unable to parse {e}");
//    Console.WriteLine(e.Message);
//}




// Print Result
// Console.WriteLine(interestTimeZone.gmtOffset);
// Console.WriteLine(interestTimeZone.name);

//DateTime localDate = DateTime.Now;
//int currentHour = localDate.TimeOfDay.Hours;
//int currentMin = localDate.TimeOfDay.Minutes;
// public static DateTime Now { get; }
//Console.WriteLine("Cur Time = {0}:{1}", currentHour, currentMin);
//Console.WriteLine($"Cur Time = {currentHour}:{currentMin}");