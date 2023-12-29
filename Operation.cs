using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Formats.Asn1;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Npgsql;
using NpgsqlTypes;

namespace Assignment
{
    public class Operation
    {
        Decrypt decrypt = new Decrypt();        
        public void AllOperation(string path){
            List<AllProperties> allPropertieslist = new List<AllProperties>();
            try{
                foreach(string file in Directory.GetFiles(path)){
                    string[] files=File.ReadAllLines(file);
                    foreach(string toEncrypt in files){
                        //string toEncrypt1="HfDj69O0CjXLjxT84uqedXXsdDarAJKVVkogKLoymJWjllkbjVrvz5+ZYfxnmaHbnotbQLCVEC41MS2efM/4RCsoyQd+wBrv+0jTzK4d6VUL4N4OjWW0J9dssCSwYY1rdFxtfEyWoTNxb7KhKaOoR5LL1FizmDpxcid2ZjWaZuGlf1Fj28mD2VKZaqD0S4m0t+QDfWS5miNrO08wONleF9ecDwYFsI1V3Jbdg1pppv/ctg4FyFmdXv9SiWcrbhfI8BL/bqsEOyRGNwVf0whcEhwh8Xf5FaqS83QHS5Q1mtujb0L+TrnCQ/nRMBYc0MLModCceV7YUaWr88PEgtV+1Hg8wOd3D6hQcvfPy6uCHjwXNXUXDUXXiPRiGqwZIAmdJZEWU9jN47CuueEYN+u34mFdkCqrn/ONomL0b5Ue60F40nK7dOWU4nFjGfm18bShT6PKYaeVfQfOb82Yz907wrXZ5h2sAPhkVdxNwGStQ1+Vp5PKx9P2Y1nkaCKV9WcP/lMeePdzLzMoUK3T5FrkXFQ8RKwaZEkjOkK2t8lHKs176P6eP/Mx8lddv3k7F2hTPWpnOOyhti2y70ZLK/5X6UlTmt7hE9E591RpuNyAOCk5sdbqmn0pzK88Ar7WlZjalO04mLC0i3pkzcS8FZbLhA==";
                       var ans=decrypt.DecryptFile(toEncrypt);
                    //    System.Console.WriteLine(ans);
                    //     System.Console.WriteLine("--------------------------------");
                        int count = 1;
                        //var ans="NotificationType:Delivery,ServerId:6586995,MessageStream:outbound,MessageId:39ae3efa-2283-44e5-8733-80226f6fea73,Tag:dinesh,Recipient:satheesh@skytrust.com.au,DeliveredAt:12/10/2023 7:53:36 PM,Details:smtp;250 2.6.0 <39ae3efa-2283-44e5-8733-80226f6fea73@mtasv.net> [InternalId=7619272024915, Hostname=SY4P282MB1257.AUSP282.PROD.OUTLOOK.COM] 48435 bytes in 0.160, 294.858 KB/sec Queued mail for delivery,ToList:Email:satheesh@skytrust.com.au & Name:;,CC:,BCC:,Recipients:satheesh@skytrust.com.au;,Subject:Syncfusion Forum [185804] created - Custom DataManager not working after package update,From:forum-support@syncfusion.com,Status:Sent,MessageEvents:,Attchment";
                        if (ans.StartsWith("NotificationType:Delivery") || ans.StartsWith("NotificationType:Bounce") )
                        {
                            string word = "MessageEvents";
                            var arr = ans.Split(new char[] { ':', ',' });
                            count = Array.FindAll(arr, s => s.Contains(word.Trim())).Length;
                        }
                        for(int i=1;i<=count;i++){
                            var data =new AllProperties{
                                NotificationType=Regex.Match(ans,RegexPattern.NotificationTypeRegex).Groups[1].Value,
                                ServerId=Regex.Match(ans,RegexPattern.ServerIdRegex).Groups[1].Value,
                                MessageStream=Regex.Match(ans,RegexPattern.MessageStreamRegex).Groups[1].Value,
                                MessageId=Regex.Match(ans,RegexPattern.MessageIdRegex).Groups[1].Value,
                                Tag=Regex.Match(ans,RegexPattern.TagRegex).Groups[1].Value,               
                                Recipient=Regex.Match(ans,RegexPattern.RecipientRegex).Groups[1].Value,
                                DeliveryAt=Regex.Match(ans,RegexPattern.DelieveryAtRegex).Groups[1].Value,
                                Details=Regex.Match(ans,RegexPattern.DetailsRegex).Groups[1].Value,
                                ToList=Regex.Match(ans,RegexPattern.ToListRegex).Groups[1].Value,
                                CC=Regex.Match(ans,RegexPattern.CCRegex).Groups[1].Value,
                                BCC=Regex.Match(ans,RegexPattern.BCCRegex).Groups[1].Value,
                                Recipients=Regex.Match(ans,RegexPattern.RecipientsRegex).Groups[1].Value,
                                Subject=Regex.Match(ans,RegexPattern.SubjectRegex).Groups[1].Value,
                                From=Regex.Match(ans,RegexPattern.FromRegex).Groups[1].Value,
                                Status=Regex.Match(ans,RegexPattern.StatusRegex).Groups[1].Value,
                                MessageEvents=Regex.Match(ans,RegexPattern.MessageEventsRegex).Groups[1].Value

                            };
                            
                            allPropertieslist.Add(data);
                            // System.Console.WriteLine(data.NotificationType);
                            // System.Console.WriteLine(data.ServerId);
                            // System.Console.WriteLine(data.MessageStream);
                            // System.Console.WriteLine(data.MessageId);
                            // System.Console.WriteLine(data.Tag);
                            // System.Console.WriteLine(data.Recipient);
                            // System.Console.WriteLine(data.DeliveryAt);
                            // System.Console.WriteLine(data.Details);
                            // System.Console.WriteLine(data.ToList);
                            // System.Console.WriteLine(data.CC);
                            // System.Console.WriteLine(data.BCC);
                            // System.Console.WriteLine(data.Recipients);
                            // System.Console.WriteLine(data.Subject);
                            // System.Console.WriteLine(data.From);
                            // System.Console.WriteLine(data.Status);
                            // System.Console.WriteLine(data.MessageEvents);
                            // System.Console.WriteLine("------------------------------------------------------");
                        }
                        
                        //break;
                    }
                    //break;
                }

                DataTable dataTable = new DataTable();
                dataTable.Columns.Add("NotificationType",typeof(string));
                dataTable.Columns.Add("ServerId",typeof(string));
                dataTable.Columns.Add("MessageStream",typeof(string));
                dataTable.Columns.Add("MessageId",typeof(string));
                dataTable.Columns.Add("Tag",typeof(string));
                dataTable.Columns.Add("Recipient",typeof(string));
                dataTable.Columns.Add("DeliveryAt",typeof(string));
                dataTable.Columns.Add("Details",typeof(string));
                dataTable.Columns.Add("ToList",typeof(string));
                dataTable.Columns.Add("CC",typeof(string));
                dataTable.Columns.Add("BCC",typeof(string));
                dataTable.Columns.Add("Recipients",typeof(string));
                dataTable.Columns.Add("Subject",typeof(string));
                dataTable.Columns.Add("From",typeof(string));
                dataTable.Columns.Add("Status",typeof(string));
                dataTable.Columns.Add("MessageEvents",typeof(string));


                for(int data=0;data<allPropertieslist.Count;data++){
                    DataRow dataRow = dataTable.NewRow();
                    dataRow["NotificationType"]=allPropertieslist[data].NotificationType;
                    dataRow["ServerId"]=allPropertieslist[data].ServerId;
                    dataRow["MessageStream"]=allPropertieslist[data].MessageStream;
                    dataRow["MessageId"]=allPropertieslist[data].MessageId;
                    dataRow["Tag"]=allPropertieslist[data].Tag;
                    dataRow["Recipient"]=allPropertieslist[data].Recipient;
                    dataRow["DeliveryAt"]=allPropertieslist[data].DeliveryAt;
                    dataRow["Details"]=allPropertieslist[data].Details;
                    dataRow["ToList"]=allPropertieslist[data].ToList;
                    dataRow["CC"]=allPropertieslist[data].CC;
                    dataRow["BCC"]=allPropertieslist[data].BCC;
                    dataRow["Recipients"]=allPropertieslist[data].Recipients;
                    dataRow["Subject"]=allPropertieslist[data].Subject;
                    dataRow["From"]=allPropertieslist[data].From;
                    dataRow["Status"]=allPropertieslist[data].Status;
                    dataRow["MessageEvents"]=allPropertieslist[data].MessageEvents;

                    dataTable.Rows.Add(dataRow);
                }

                WriteToPostgre(dataTable,"amazonlog");

            }
            catch(Exception e){
                System.Console.WriteLine(e.ToString());
            }
        }

         public static int SuccessOrFailure(){
            string connString=@"Server=localhost;Username=postgres;Database=surya;Port=5432;Password=Surya@1232002;SSLMode=Prefer";
            NpgsqlConnection npgsqlConnection = new NpgsqlConnection(connString);
            string query2=@"select * from amazonlog";
            NpgsqlCommand cmd = new NpgsqlCommand(query2,npgsqlConnection);
            npgsqlConnection.Open();
            DataTable dt2 = new DataTable();
            NpgsqlDataAdapter adpt = new NpgsqlDataAdapter(cmd);
            adpt.Fill(dt2);
            int TotalCount=dt2.Rows.Count;
            return TotalCount;
        }
        //static string connString=@"Server=localhost;Username=postgres;Database=surya;Port=5432;Password=Surya@1232002;SSLMode=Prefer";
        public string WriteToPostgre(DataTable dataTable,string table){
            try{
                bool flag = false;
                using(NpgsqlConnection conn =this.RetryGetConnectionAsync()){
                    using (NpgsqlTransaction transaction = conn.BeginTransaction()){
                        try{
                            using (var importText = conn.BeginTextImport("COPY \""+ "public" +"\".\"" + table + "\" (\"notificationtype\",\"serverid\",\"messagestream\",\"messageid\",\"tag\",\"recipient\",\"deliveryat\",\"details\",\"tolist\",\"cc\",\"bcc\",\"recipients\",\"subject\",\"fromcolumn\",\"status\",\"messageevents\")FROM STDIN WITH DELIMITER AS '\x7F' NULL AS ''")){
                                foreach(DataRow row in dataTable.Rows){
                                    string line= string.Empty;
                                    for(int i=0;i<dataTable.Columns.Count;i++){
                                        if(dataTable.Columns[i].DataType==typeof(string)){
                                            row[i]=row[i].ToString().Replace("\r\n","").Replace("\n","").Replace("\r","").Replace("\\","\\\\");
                                        }
                                        if((i+1)<dataTable.Columns.Count){
                                            line+=row[i]+ "\x7F";
                                        }
                                        else{
                                            line+=row[i];
                                        }
                                    }
                                   importText.WriteLine(line);
                                }
                            }
                            flag=true;
                            int totalcount=SuccessOrFailure();
                            System.Console.WriteLine("Total data inserted : "+totalcount);
                            System.Console.WriteLine("Build Success");
                        }catch(Exception e){    
                            var exceptionData=e.Data.Cast<DictionaryEntry>().Where(temp=>temp.Key is string && temp.Value is object).ToDictionary(temp=>(string)temp.Key , temp =>(object)temp.Value);
                            string exceptionPrint=string.Empty;
                            if(exceptionData!=null){
                                foreach(var item in exceptionData){
                                    exceptionPrint+=item.Value+ ";";
                                }
                                System.Console.WriteLine(exceptionPrint);
                            }
                            transaction.Rollback();
                            flag=false;
                        }
                        finally{
                            if(flag){
                                transaction.Commit();
                            }
                        }
                    }
                }
                flag = true;



            }
            catch (Exception e)
            {
                var exceptionData = e.Data.Cast<DictionaryEntry>().Where(temp => temp.Key is string && temp.Value is object).ToDictionary(temp => (string)temp.Key, temp => (object)temp.Value);
                string exceptionPrint = string.Empty;
                if (exceptionData != null)
                {
                    foreach (var item in exceptionData)
                    {
                        exceptionPrint += item.Value + ";";
                    }
                }
            }
            return "";
        }
         public NpgsqlConnection RetryGetConnectionAsync()
        {
            NpgsqlConnection connection = null;
            for (int tries = 1; tries <= 3; tries++)
            {
                try
                {
                    connection = this.GetConnectionAsync();
                    break;
                }
                catch (Exception ex)
                {
                    System.Console.WriteLine(ex.Data.ToString());
                }
            }
            return connection;
        }
        protected NpgsqlConnection GetConnectionAsync(bool open = true)
        {
            var c = new NpgsqlConnection
            {
                ConnectionString = this.GetConnectionString() 
            };
            if (open)
            {
                c.Open();
            }
            return c;
        }
        protected string GetConnectionString()
        {
            NpgsqlConnectionStringBuilder builder = new NpgsqlConnectionStringBuilder();
            builder.ConnectionString = "Server=localhost;Username=postgres;Database=surya;Port=5432;Password=Surya@1232002;SSLMode=Prefer";
            return builder.ConnectionString;
        }
    }
}