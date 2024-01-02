using System;
using System.Net;
using System.Net.Http;
namespace Assignment;
class Program{
    public static void Main(string[] args)
    {
        Operation operation = new Operation();
        operation.AllOperation(args[0]);
        int TotalCount=Operation.SuccessOrFailure();
        System.Console.WriteLine("Total data inserted : "+TotalCount);
        System.Console.WriteLine("Build Success");
    }
}       