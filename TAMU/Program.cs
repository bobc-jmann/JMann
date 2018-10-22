using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Configuration;

namespace TAMU
{
	partial class Program
	{
		static void Main(string[] args)
		{
			// Get connection string
			string connStr = ConfigurationManager.AppSettings["ConnStr"];
			SqlConnection conn1 = new SqlConnection(ConfigurationManager.AppSettings["ConnStr"]);

			// Get Addresses to be Geocoded
			SqlCommand cmd1 = new SqlCommand();
			cmd1.Connection = conn1;
			cmd1.CommandText = "[JMANN-SQL].[Streamline_Live].dbo.spTamuGeocodingSelect";
			cmd1.CommandType = System.Data.CommandType.StoredProcedure;

			int retValue1 = 0; 
			cmd1.Parameters.Add(CreateParameter("@retValue", System.Data.ParameterDirection.ReturnValue, System.Data.DbType.Int32, retValue1));

			SqlDataReader rsql = null;
			if (!SqlSpOpen(ref conn1, ref rsql, cmd1))
			{
				Console.WriteLine(DateTime.Now.ToString());
				Console.Write("Press any key to continue...");
				Console.ReadKey();
			}
			retValue1 = Convert.ToInt32(cmd1.Parameters["@retValue"].Value);
			if (retValue1 > 0)
			{
				Console.WriteLine("ERROR in spTamuGeocodingSelect");
				Console.WriteLine(DateTime.Now.ToString());
				Console.Write("Press any key to continue...");
				Console.ReadKey();
				return;
			}
			while (rsql.Read())
			{
				decimal latitude = 0;
				decimal longitude = 0;
				string county = "";

				int addressID = rsql.GetInt32(0);
				string streetAddress = rsql.GetString(1);
				string city = rsql.GetString(2);
				string state = rsql.GetString(3);
				string zip5 = rsql.GetString(4);

				Console.WriteLine(streetAddress);

				try
				{
					using (var client = new ServiceReference1.GeocoderService_V04_01SoapClient("GeocoderService_V04_01Soap"))
					{
						var result = client.GeocodeAddressNonParsed(streetAddress, city, state, zip5,
							"e6baae0e17f0454283077cb20706fe42", 4.01, false, ServiceReference1.CensusYear.AllAvailable, false, true);

						latitude = Convert.ToDecimal(result.WebServiceGeocodeQueryResults[0].Latitude.ToString());
						longitude = Convert.ToDecimal(result.WebServiceGeocodeQueryResults[0].Longitude.ToString());
						if (result.WebServiceGeocodeQueryResults[0].FCounty != null) {
							county = result.WebServiceGeocodeQueryResults[0].FCounty.ToString();
						} else {
							county = "";
						}
					}
				}
				catch (Exception ex)
				{
					Console.WriteLine("ERROR in Geocoder Service: " + ex.Message);
					Console.WriteLine("\tLatitude:" + latitude.ToString());
					Console.WriteLine("\tLongitude:" + longitude.ToString());
					Console.WriteLine("\tCounty:" + county);
					Console.WriteLine(DateTime.Now.ToString());
					Console.Write("Press any key to continue...");
					Console.ReadKey();
					return;
				}

				// Update the record.
				SqlCommand cmd2 = new SqlCommand();
				cmd2.Connection = conn1;
				cmd2.CommandText = "[JMANN-SQL].[Streamline_Live].dbo.spTamuGeocodingUpdate";
				cmd2.CommandType = System.Data.CommandType.StoredProcedure;

				cmd2.Parameters.Add(CreateParameterSQL("@addressID", System.Data.ParameterDirection.Input, System.Data.SqlDbType.Int, addressID));
				cmd2.Parameters.Add(CreateParameterSQL("@lat", System.Data.ParameterDirection.Input, System.Data.SqlDbType.Decimal, latitude, 0, 15, 12));
				cmd2.Parameters.Add(CreateParameterSQL("@long", System.Data.ParameterDirection.Input, System.Data.SqlDbType.Decimal, longitude, 0, 15, 12));
				cmd2.Parameters.Add(CreateParameterSQL("@county", System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar, county, 50));
				int retValue2 = 0;
				cmd2.Parameters.Add(CreateParameterSQL("@retValue", System.Data.ParameterDirection.ReturnValue, System.Data.SqlDbType.Int, retValue2));

				try
				{
					cmd2.ExecuteNonQuery();
					retValue2 = Convert.ToInt32(cmd2.Parameters["@retValue"].Value);
					if (retValue2 > 0)
					{
						Console.WriteLine("ERROR in spTamuGeocodingUpdate");
						Console.WriteLine(DateTime.Now.ToString());
						Console.Write("Press any key to continue...");
						Console.ReadKey();
						return;
					}
				}
				catch (Exception ex)
				{
					LogProgramError(ex.Message, "spTamuGeocodingUpdate", ex.StackTrace, true);
					Console.WriteLine(DateTime.Now.ToString());
					Console.Write("Press any key to continue...");
					Console.ReadKey();
					return;
				}
			}
			SqlQueryClose(ref conn1, ref rsql);

			Console.WriteLine(DateTime.Now.ToString());
			Console.Write("Press any key to continue...");
			Console.ReadKey();
			return;
		}
	}
}
