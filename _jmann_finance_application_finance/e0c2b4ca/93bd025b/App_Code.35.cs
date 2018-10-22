#pragma checksum "D:\JMann\Finance\Application\Finance\App_Code\Helper\Dbase.cs" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "BD549877A545B69D95C401919AD4F2004693AE6F"

#line 1 "D:\JMann\Finance\Application\Finance\App_Code\Helper\Dbase.cs"
using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace jmann.DataLayer
{
     public sealed class Dbase
     {
         private Dbase()
         {
         }

         public static SqlConnection GetConnection()
         {
             //string connectionString = "Data Source=172.16.0.22;Initial Catalog=Finance;User ID=Finance_User;Password=!Finance5678$";
             Configuration rootWebConfig = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration("~/");
             string connectionString = rootWebConfig.ConnectionStrings.ConnectionStrings["App-MainConnectionString"].ConnectionString;
             SqlConnection connection = new SqlConnection(connectionString);
             connection.Open();
             return connection;
         }

         public static SqlCommand GetCommand(string storedProcedureName, SqlConnection connection)
         {
             SqlCommand command = new SqlCommand(storedProcedureName, connection);
             command.CommandType = CommandType.StoredProcedure;
             return command;
         }

         public static SqlCommand GetCommand(SqlConnection connection, string sql)
         {
             SqlCommand command = new SqlCommand(sql, connection);
             command.CommandType = CommandType.Text;
             return command;
         }

         public static DataSet GetDbaseDataSet(SqlCommand command)
         {
             SqlDataAdapter adapter = new SqlDataAdapter(command);
             DataSet dataset = ((DataSet)Activator.CreateInstance(typeof(DataSet)));
             adapter.Fill(dataset);
             adapter.Dispose();
             return dataset;
         }
     }
}


#line default
#line hidden
