using System;
namespace PostgresqlAssignment;
class Program{
    public static void Main(string[] args)
    {
        // System.Console.WriteLine("Enter the checkin date");
        // DateTime checkin = DateTime.ParseExact(Console.ReadLine(),"yyyy-MM-dd",null);
        // DateTime checkout = DateTime.ParseExact(Console.ReadLine(),"yyyy-MM-dd",null);
        DateTime date = new DateTime(2023,05,01);
        DateTime date1 = new DateTime(2023,12,11);
        Operation.getDate(date,date1);
        //Operation.WriteToCSV();
        //Operation.CreateTableInPSQL();
    }
}