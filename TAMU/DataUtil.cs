using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.IO;
using System.Globalization;
using System.Configuration;
using System.Net.Mail;

namespace TAMU
{
	//class DataUtil
	partial class Program
	{
		public string GetDateString(DateTime date)
		{
			return string.Format("{0:MMMM d, yyyy}", date);
		}

		public static SqlParameter CreateParameter(string parameterName, ParameterDirection direction, DbType dbType,
				Object parameterValue, int size = 0) 
		{
			SqlParameter objPar = new SqlParameter();
			string a = "";

			objPar.ParameterName = parameterName;
			objPar.Direction = direction;
			objPar.DbType = dbType;
			objPar.Value = parameterValue;
			objPar.Size = size;
			a = objPar.SourceColumn;
			
			return objPar;
		}

		public static SqlParameter CreateParameterSQL(string parameterName, ParameterDirection direction, SqlDbType dbType,
				Object parameterValue, int size = 0, byte precision = 0, byte scale = 0) 
		{
			SqlParameter objPar = new SqlParameter();
			string a = "";

			objPar.ParameterName = parameterName;
			objPar.Direction = direction;
			objPar.SqlDbType = dbType;
			objPar.Value = parameterValue;
			objPar.Size = size;
			objPar.Precision = precision;
			objPar.Scale = scale;
			a = objPar.SourceColumn;
			
			return objPar;
		}

		public static int SqlNonQueryConn(string sql, string connStr)
		{
			SqlConnection connSql = new SqlConnection(connStr);
			int numRows = -1;

			try
			{
				connSql.Open();
				while (connSql.State == ConnectionState.Connecting) {}
			}
			catch (Exception ex)
			{
				LogProgramError(ex.Message, sql, ex.StackTrace, true);
				return -1;
			}
			SqlCommand cmd = new SqlCommand(sql, connSql);
			while (numRows < 0)
			{
				try
				{
					numRows = cmd.ExecuteNonQuery();
				}
				catch (Exception ex)
				{
					LogProgramError(ex.Message, sql, ex.StackTrace, true);
					return -1;
				}
			}
			cmd.Dispose();
			connSql.Close();
			return numRows;
		}

		public static int SqlNonQueryIdentityConn(string sql, string connStr)
		{
			SqlConnection connSql = new SqlConnection(connStr);

			try
			{
				connSql.Open();
				while (connSql.State == ConnectionState.Connecting) {}
			}
			catch (Exception ex)
			{
				LogProgramError(ex.Message, sql, ex.StackTrace, true);
				return -1;
			}

			SqlCommand cmd = new SqlCommand(sql, connSql);
			int identity = 0;
			try
			{
				int numRows = cmd.ExecuteNonQuery();
				sql = "SELECT @@IDENTITY ";
				cmd.Parameters.Clear();
				cmd.CommandText = sql;
				identity = Convert.ToInt32(cmd.ExecuteScalar().ToString());
			}
			catch (Exception ex)
			{
				LogProgramError(ex.Message, sql, ex.StackTrace, true);
				return -1;
			}
			cmd.Dispose();
			connSql.Close();
			return identity;	
		}

		public static object SqlExecuteScalarConn(string sql, string connStr)
		{
			SqlConnection connSql = new SqlConnection(connStr);
			try
			{
				connSql.Open();
				while (connSql.State == ConnectionState.Connecting) {}
			}
			catch (Exception ex)
			{
				LogProgramError(ex.Message, sql, ex.StackTrace, true);
				return -1;
			}

			SqlCommand cmd = new SqlCommand(sql, connSql);
			object retVal = -1;

			try
			{
				 retVal = cmd.ExecuteScalar();
			}
			catch (Exception ex)
			{
				LogProgramError(ex.Message, sql, ex.StackTrace, true);
				return -1;
			}
			cmd.Dispose();
			connSql.Close();
			return retVal;

		}

		public class QueryParms
		{
			public string parmName { get; set; }
			public System.Data.SqlDbType dbType { get; set; }
			public int size { get; set; }
			public object value { get; set; }
		}
 
		public static bool SqlQueryOpenWithParms(ref SqlConnection connSql, ref SqlDataReader rsql, string sql, List<QueryParms> parms = null)
		{
			try
			{
				connSql.Open();
				while (connSql.State == ConnectionState.Connecting) {}
			}
			catch (Exception ex)
			{
				LogProgramError(ex.Message, sql, ex.StackTrace, true);
				return false;
			}
			rsql = null;
			try
			{
				SqlCommand rCmd = new SqlCommand(sql, connSql);
				int i = 0;
				if (parms != null)
				{
					foreach (QueryParms p in parms)
					{
						rCmd.Parameters.Add(p.parmName, p.dbType, p.size);
						rCmd.Parameters[i].Value = p.value;
						i += 1;
					}
				}
				rsql = rCmd.ExecuteReader();
			}
			catch (Exception ex)
			{
				LogProgramError(ex.Message, sql, ex.StackTrace, true);
				rsql.Close();
				connSql.Close();
				return false;
			}
			return true;
		}

		public static bool SqlQueryOpen(ref SqlConnection connSql, ref SqlDataReader rsql, string sql)
		{
			try
			{
				connSql.Open();
				while (connSql.State == ConnectionState.Connecting) { }
			}
			catch (Exception ex)
			{
				LogProgramError(ex.Message, sql, ex.StackTrace, true);
				return false;
			}
			rsql = null;
			try
			{
				SqlCommand rCmd = new SqlCommand(sql, connSql);
				rsql = rCmd.ExecuteReader();
			}
			catch (Exception ex)
			{
				LogProgramError(ex.Message, sql, ex.StackTrace, true);
				if (rsql != null)
					rsql.Close();
				connSql.Close();
				return false;
			}
			return true;
		}

		public static bool SqlSpOpen(ref SqlConnection connSql, ref SqlDataReader rsql, SqlCommand rCmd)
		{
			try
			{
				connSql.Open();
				while (connSql.State == ConnectionState.Connecting) { }
			}
			catch (Exception ex)
			{
				LogProgramError(ex.Message, "", ex.StackTrace, true);
				return false;
			}
			rsql = null;
			try
			{
				rsql = rCmd.ExecuteReader();
			}
			catch (Exception ex)
			{
				LogProgramError(ex.Message, "", ex.StackTrace, true);
				if (rsql != null)
					rsql.Close();
				connSql.Close();
				return false;
			}
			return true;
		}

		public static void SqlQueryClose(ref SqlConnection connSql, ref SqlDataReader rsql)
		{
			if (rsql != null)
				rsql.Close();
			connSql.Close();
		}

		public static void LogProgramError(string message, string description, string stackTrace, bool sendEmail)
		{
			Console.WriteLine(message + "\n" + description + "\n" + stackTrace + "\n");
		}

		public static void SendErrorEmail(string from, string to, string subject, string body, string priority)
		{
			MailMessage objMail = new MailMessage();

			objMail.From = new MailAddress(from);
			objMail.To.Add(to);
			objMail.Subject = subject.Replace("\n", " ").Replace("\r", " ").Replace("\r\n", " ");
			objMail.IsBodyHtml = true;
			if (priority.ToUpper() == "HIGH")
				objMail.Priority = MailPriority.High;
			else
				objMail.Priority = MailPriority.Normal;
			objMail.Body = body;
			SmtpClient client = new SmtpClient();
			try
			{
				client.Send(objMail);
			}
			catch (Exception ex)
			{
				LogProgramError(ex.Message, "Error Sending Error Mail", ex.StackTrace, false);
			}
		}
	}
}
