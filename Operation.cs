using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Azure.Core;
using Microsoft.Data.SqlClient;
using Npgsql;
namespace PostgresqlAssignment
{
    public class Operation
    {
        static StringBuilder sb = new StringBuilder();
        static string connectionString =@"Data Source=SYNCLAPN-37905;Initial Catalog=Customer;TrustServerCertificate=True;
            Integrated Security=True;User id=sa;Password=Surya@1232002";
        public static void CreateDatabase(){
            string connection=@"Data Source=SYNCLAPN-37905;Initial Catalog=master;TrustServerCertificate=True;
            Integrated Security=True;User id=sa;Password=Surya@1232002";
            string query=@"create database Customer";
            SqlConnection conn = new SqlConnection(connection);
            SqlCommand cmd = new SqlCommand(query,conn);
            try{
                conn.Open();
                cmd.ExecuteNonQuery();
                System.Console.WriteLine("database created");
            }
            catch(SqlException e){
                System.Console.WriteLine("Error : "+e.ToString());
            }
            finally{
                conn.Close();
            }

        }
        public static void Createtable(){
            string query=@"create table Userdetails(
                id int not null primary key identity(1,1),
                userName varchar(20),
                Age int,
                checkin date
            );";
            SqlConnection conn= new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand(query,conn);
            try{
                conn.Open();
                cmd.ExecuteNonQuery();
                System.Console.WriteLine("table created");
            }
            catch(SqlException e){
                System.Console.WriteLine("Error : "+e.ToString());
            }
            finally{
                conn.Close();
            }
        }
        public static void getDate(DateTime intime,DateTime outtime){
            string query=@"select * from Userdetails where checkin >= @checkintime and checkin<=@checkoutime ";
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand(query,connection);
            SqlParameter param = new SqlParameter("@checkintime",intime);
            cmd.Parameters.Add(param);
            SqlParameter param2 = new SqlParameter("@checkoutime",outtime);
            cmd.Parameters.Add(param2);
            try{
                connection.Open();
                DataTable dt = new DataTable();
                SqlDataAdapter adapter= new SqlDataAdapter(cmd);
                adapter.Fill(dt);
                dt.PrimaryKey = new DataColumn[] {dt.Columns["id"]};  //try to set the primary key to the datatable
                string name="";
                int age=0;
                string checkin="";
                DateTime entry;
                foreach (DataColumn col in dt.Columns)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        name = (string)row[1];
                        age = (int)row[2];
                        checkin = row[3].ToString();
                        entry = DateTime.Parse(checkin);
                        InsertValues(name, age, entry, intime, outtime);
                        string str = SuccessOrFailure(dt);
                        sb.AppendLine(str);
                        sb.Append("-----------------------------------------------------------");
                        sb.AppendLine();
                        File.WriteAllText("Output.txt", sb.ToString());
                    }
                    break;
                }
            }
            catch(SqlException e){
                System.Console.WriteLine("Error : "+e.ToString());
            }
            finally{
                connection.Close();
            }
        }

        public static void CreateTableInPSQL(){
            string connString =@"Server=localhost;Username=postgres;Database=postgres;Port=5432;Password=Surya@1232002;SSLMode=Prefer";
            NpgsqlConnection npgsqlConnection = new NpgsqlConnection(connString);
            npgsqlConnection.Open();
            string query=@"create table Userdetails(
                id BIGSERIAL not null primary key,
                userName VARCHAR(20) ,
                Age int ,
                Checkin date ,
                unique(userName,Age,Checkin)
            )";
            NpgsqlCommand cmd = new NpgsqlCommand(query,npgsqlConnection);
            cmd.ExecuteNonQuery();
            System.Console.WriteLine("table created");
            npgsqlConnection.Close();
        }   


        public static void InsertValues(string name,int age,DateTime entry,DateTime intime,DateTime outtime){
            string connString=@"Server=localhost;Username=postgres;Database=postgres;Port=5432;Password=Surya@1232002;SSLMode=Prefer";
            NpgsqlConnection npgsqlConnection = new NpgsqlConnection(connString);
           // string query=@"insert into Userdetails(userName,Age,Checkin) values(@name,@age,@entry)";
            string query3=@"insert into Userdetails(userName,Age,Checkin)
                            values(@name,@age,@entry)
                            on conflict(userName,Age,Checkin) do update
                            set userName=excluded.userName,
                                Age=excluded.Age,
                                Checkin=excluded.Checkin";
            NpgsqlCommand cmd = new NpgsqlCommand(query3,npgsqlConnection);
            NpgsqlParameter param = new NpgsqlParameter("@name",name);
            cmd.Parameters.Add(param);
            NpgsqlParameter param2 = new NpgsqlParameter("@age",age);
            cmd.Parameters.Add(param2);
            NpgsqlParameter param3 = new NpgsqlParameter("@entry",entry);
            cmd.Parameters.Add(param3);
            npgsqlConnection.Open();
            cmd.ExecuteNonQuery();
            System.Console.WriteLine("value inserted");
            sb.Append(query3);
            sb.AppendLine();
            sb.Append(intime);
            sb.Append("-");
            sb.Append(outtime);
            sb.AppendLine();
            File.WriteAllText("Output.txt",sb.ToString());
            npgsqlConnection.Close();
        }
/*
        public static void InsertValues(string name ,int age,DateTime date,DateTime intime,DateTime outtime,DataTable dataTable){
            string query=@"insert into Userdetails(userName,Age,Checkin)
                            values(@name,@age,@entry) ";
            string connecString =@"Data Source=SYNCLAPN-37905;Initial Catalog=Sample1;TrustServerCertificate=True;
            Integrated Security=True;User id=sa;Password=Surya@1232002";
            SqlConnection newconn = new SqlConnection(connecString);newconn.Open();
            SqlCommand cmd = new SqlCommand(query,newconn);
            // string newquery = @"CREATE PROCEDURE [dbo].[InsertTable]
            //                      @myTableType MyTableType readonly
            //                     AS
            //                     BEGIN
            //                     insert into [dbo].Records select * from @myTableType 
            //                     END";
            // SqlCommand cmd = new SqlCommand("InsertTable", newconn);
            // cmd.Parameters.Add(new SqlParameter("@tabledata", dataTable));
            // cmd.CommandType = CommandType.StoredProcedure;
           cmd.Parameters.Add(new SqlParameter("@myTableType", dataTable));
            SqlParameter param = new SqlParameter("@name",name);
            cmd.Parameters.Add(param);
            SqlParameter param2 = new SqlParameter("@age",age);
            cmd.Parameters.Add(param2);
            SqlParameter param3 = new SqlParameter("@entry",date);
            cmd.Parameters.Add(param3);
           newconn.Open();
            cmd.ExecuteNonQuery();
            System.Console.WriteLine("values inserted");
            newconn.Close();
        }*/

        public static string SuccessOrFailure(DataTable dt){
            string connString=@"Server=localhost;Username=postgres;Database=postgres;Port=5432;Password=Surya@1232002;SSLMode=Prefer";
            NpgsqlConnection npgsqlConnection = new NpgsqlConnection(connString);
            string query2=@"select * from Userdetails";
            NpgsqlCommand cmd = new NpgsqlCommand(query2,npgsqlConnection);
            npgsqlConnection.Open();
            DataTable dt2 = new DataTable();
            NpgsqlDataAdapter adpt = new NpgsqlDataAdapter(cmd);
            adpt.Fill(dt2);
            string str="Success";
            if(dt.Rows.Count!=dt2.Rows.Count || dt.Columns.Count!=dt2.Columns.Count){
                str="Failed";
            }
            return str; 
        }
    }
}