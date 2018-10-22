#pragma checksum "D:\JMann\Finance\Application\Finance\App_Code\DataLayerBase\CartsDataLayerBase.cs" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "6C7D1B48B0FDB74A67DF3F0AEC6A54BFEEE9BD9B"

#line 1 "D:\JMann\Finance\Application\Finance\App_Code\DataLayerBase\CartsDataLayerBase.cs"
using System;
using System.Data;
using System.Data.SqlClient;
using jmann.BusinessObject;
 
namespace jmann.DataLayer.Base
{
     /// <summary>
     /// Base class for CartsDataLayer.  Do not make changes to this class,
     /// instead, put additional code in the CartsDataLayer class 
     /// </summary>
     public class CartsDataLayerBase
     {
         // constructor 
         public CartsDataLayerBase()
         {
         }

         /// <summary>
         /// Selects a record by primary key(s)
         /// </summary>
         public static Carts SelectByPrimaryKey(int cartsID)
         {
              string storedProcName = "[dbo].[pr_Carts_SelectByPrimaryKey]";
              SqlConnection connection = Dbase.GetConnection();
              SqlCommand command = Dbase.GetCommand(storedProcName, connection);

              // parameters
              command.Parameters.AddWithValue("@cartsID", cartsID);

              DataSet ds = Dbase.GetDbaseDataSet(command);
              Carts objCarts = null;

              if (ds.Tables[0].Rows.Count > 0)
              {
                  objCarts = new Carts();
                  objCarts.CartsID = (int)ds.Tables[0].Rows[0]["CartsID"];

                  if (ds.Tables[0].Rows[0]["Date"] != System.DBNull.Value)
                      objCarts.Date = (DateTime)ds.Tables[0].Rows[0]["Date"];
                  else
                      objCarts.Date = null;

                  if (ds.Tables[0].Rows[0]["LocationID"] != System.DBNull.Value)
                      objCarts.LocationID = (int)ds.Tables[0].Rows[0]["LocationID"];
                  else
                      objCarts.LocationID = null;


				  if (ds.Tables[0].Rows[0]["CartsWorkedHard"] != System.DBNull.Value)
					  objCarts.CartsWorkedHard = (double)ds.Tables[0].Rows[0]["CartsWorkedHard"];
				  else
					  objCarts.CartsWorkedHard = null;

				  if (ds.Tables[0].Rows[0]["CartsWorkedSoft"] != System.DBNull.Value)
					  objCarts.CartsWorkedSoft = (double)ds.Tables[0].Rows[0]["CartsWorkedSoft"];
				  else
					  objCarts.CartsWorkedSoft = null;

				  if (ds.Tables[0].Rows[0]["CartsWorkedTotal"] != System.DBNull.Value)
					  objCarts.CartsWorkedTotal = (double)ds.Tables[0].Rows[0]["CartsWorkedTotal"];
				  else
					  objCarts.CartsWorkedTotal = null;

				  if (ds.Tables[0].Rows[0]["OnHandHard"] != System.DBNull.Value)
					  objCarts.OnHandHard = (double)ds.Tables[0].Rows[0]["OnHandHard"];
				  else
					  objCarts.OnHandHard = null;

				  if (ds.Tables[0].Rows[0]["OnHandSoft"] != System.DBNull.Value)
					  objCarts.OnHandSoft = (double)ds.Tables[0].Rows[0]["OnHandSoft"];
				  else
					  objCarts.OnHandSoft = null;

				  if (ds.Tables[0].Rows[0]["OnHandTotal"] != System.DBNull.Value)
					  objCarts.OnHandTotal = (double)ds.Tables[0].Rows[0]["OnHandTotal"];
				  else
					  objCarts.OnHandTotal = null;

				  if (ds.Tables[0].Rows[0]["HangTotal"] != System.DBNull.Value)
                      objCarts.HangTotal = (int)ds.Tables[0].Rows[0]["HangTotal"];
                  else
                      objCarts.HangTotal = null;

                  if (ds.Tables[0].Rows[0]["ThrownCount"] != System.DBNull.Value)
                      objCarts.ThrownCount = (int)ds.Tables[0].Rows[0]["ThrownCount"];
                  else
                      objCarts.ThrownCount = null;

                  if (ds.Tables[0].Rows[0]["ThrownLbs"] != System.DBNull.Value)
                      objCarts.ThrownLbs = (int)ds.Tables[0].Rows[0]["ThrownLbs"];
                  else
                      objCarts.ThrownLbs = null;

                  if (ds.Tables[0].Rows[0]["RaggedLbs"] != System.DBNull.Value)
                      objCarts.RaggedLbs = (int)ds.Tables[0].Rows[0]["RaggedLbs"];
                  else
                      objCarts.RaggedLbs = null;

              }
              command.Dispose();
              connection.Close();
              connection.Dispose();
              ds.Dispose();

              return objCarts;
         }
 
         /// <summary>
         /// Selects all Carts 
         /// </summary> 
         public static CartsCollection SelectAll() 
         { 
             return SelectShared("[dbo].[pr_Carts_SelectAll]", String.Empty, null); 
         } 
 
         /// <summary>
         /// Selects all Carts by Location, related to column LocationID 
         /// </summary> 
         public static CartsCollection SelectCartsCollectionByLocation(int locationID) 
         { 
             return SelectShared("[dbo].[pr_Carts_SelectAllByLocation]", "locationID", locationID); 
         } 
 
         /// <summary>
         /// Selects CartsID and Date columns for use with a DropDownList web control 
         /// </summary> 
         public static CartsCollection SelectCartsDropDownListData() 
         { 
              string storedProcName = "[dbo].[pr_Carts_SelectAll]";
              SqlConnection connection = Dbase.GetConnection();
              SqlCommand command = Dbase.GetCommand(storedProcName, connection);
 
              DataSet ds = Dbase.GetDbaseDataSet(command); 
              CartsCollection objCartsCol = new CartsCollection(); 
              Carts objCarts; 
 
              if (ds.Tables[0].Rows.Count > 0) 
              { 
                 foreach(DataRow dr in ds.Tables[0].Rows) 
                 { 
                     objCarts = new Carts(); 
                     objCarts.CartsID = (int)dr["CartsID"]; 
 
                     if (dr["Date"] != System.DBNull.Value) 
                         objCarts.Date = (DateTime)(dr["Date"]); 
                     else 
                         objCarts.Date = null; 
 
                     objCartsCol.Add(objCarts);
                 } 
              } 
 
              command.Dispose(); 
              connection.Close(); 
              connection.Dispose(); 
              ds.Dispose(); 
 
              return objCartsCol; 
         } 
 
         public static CartsCollection SelectShared(string storedProcName, string param, object paramValue) 
         { 
              SqlConnection connection = Dbase.GetConnection(); 
              SqlCommand command = Dbase.GetCommand(storedProcName, connection); 
 
              // parameters 
              switch (param) 
              { 
                  case "locationID": 
                      command.Parameters.AddWithValue("@locationID", paramValue); 
                      break; 
                  default: 
                      break; 
              } 
 
              DataSet ds = Dbase.GetDbaseDataSet(command); 
              CartsCollection objCartsCol = new CartsCollection(); 
              Carts objCarts; 
 
              if (ds.Tables[0].Rows.Count > 0) 
              { 
                 foreach(DataRow dr in ds.Tables[0].Rows) 
                 { 
                     objCarts = new Carts(); 
                     objCarts.CartsID = (int)dr["CartsID"]; 
 
                     if (dr["Date"] != System.DBNull.Value) 
                         objCarts.Date = (DateTime)dr["Date"]; 
                     else 
                         objCarts.Date = null; 
 
                     if (dr["LocationID"] != System.DBNull.Value) 
                         objCarts.LocationID = (int)dr["LocationID"];
                     else
                         objCarts.LocationID = null;
 
 
                     if (dr["CartsWorkedHard"] != System.DBNull.Value) 
                         objCarts.CartsWorkedHard = (double)dr["CartsWorkedHard"]; 
                     else 
                         objCarts.CartsWorkedHard = null; 
 
                     if (dr["CartsWorkedSoft"] != System.DBNull.Value) 
                         objCarts.CartsWorkedSoft = (double)dr["CartsWorkedSoft"]; 
                     else 
                         objCarts.CartsWorkedSoft = null; 
  
                     if (dr["CartsWorkedTotal"] != System.DBNull.Value) 
                         objCarts.CartsWorkedTotal = (double)dr["CartsWorkedTotal"]; 
                     else 
                         objCarts.CartsWorkedTotal = null;

					 if (dr["OnHandHard"] != System.DBNull.Value)
						 objCarts.OnHandHard = (double)dr["OnHandHard"];
					 else
						 objCarts.OnHandHard = null;

					 if (dr["OnHandSoft"] != System.DBNull.Value)
						 objCarts.OnHandSoft = (double)dr["OnHandSoft"];
					 else
						 objCarts.OnHandSoft = null;

					 if (dr["OnHandTotal"] != System.DBNull.Value)
						 objCarts.OnHandTotal = (double)dr["OnHandTotal"];
					 else
						 objCarts.OnHandTotal = null;
					 
					 if (dr["HangTotal"] != System.DBNull.Value) 
                         objCarts.HangTotal = (int)dr["HangTotal"]; 
                     else 
                         objCarts.HangTotal = null; 
 
                     if (dr["ThrownCount"] != System.DBNull.Value) 
                         objCarts.ThrownCount = (int)dr["ThrownCount"]; 
                     else 
                         objCarts.ThrownCount = null; 
 
                     if (dr["ThrownLbs"] != System.DBNull.Value) 
                         objCarts.ThrownLbs = (int)dr["ThrownLbs"]; 
                     else 
                         objCarts.ThrownLbs = null; 
 
                     if (dr["RaggedLbs"] != System.DBNull.Value) 
                         objCarts.RaggedLbs = (int)dr["RaggedLbs"]; 
                     else 
                         objCarts.RaggedLbs = null; 
 
                     objCartsCol.Add(objCarts);
                 } 
              } 
 
              command.Dispose(); 
              connection.Close(); 
              connection.Dispose(); 
              ds.Dispose(); 
 
              return objCartsCol; 
         } 
 
         /// <summary>
         /// Inserts a record 
         /// </summary> 
         public static int Insert(Carts objCarts) 
         { 
             string storedProcName = "[dbo].[pr_Carts_Insert]"; 
             return InsertUpdate(objCarts, false, storedProcName); 
         } 
 
         /// <summary>
         /// Updates a record 
         /// </summary>
         public static void Update(Carts objCarts) 
         { 
             string storedProcName = "[dbo].[pr_Carts_Update]"; 
             InsertUpdate(objCarts, true, storedProcName); 
         } 
 
         private static int InsertUpdate(Carts objCarts, bool isUpdate, string storedProcName) 
         { 
             SqlConnection connection = Dbase.GetConnection(); 
             SqlCommand command = Dbase.GetCommand(storedProcName, connection); 
 
             object date = objCarts.Date; 
             object locationID = objCarts.LocationID;
			 object cartsWorkedHard = objCarts.CartsWorkedHard;
			 object cartsWorkedSoft = objCarts.CartsWorkedSoft;
			 object onHandHard = objCarts.OnHandHard;
			 object onHandSoft = objCarts.OnHandSoft;
			 object hangTotal = objCarts.HangTotal; 
             object thrownCount = objCarts.ThrownCount; 
             object thrownLbs = objCarts.ThrownLbs; 
             object raggedLbs = objCarts.RaggedLbs; 
 
             if (objCarts.Date == null) 
                 date = System.DBNull.Value; 
 
             if (objCarts.LocationID == null) 
                 locationID = System.DBNull.Value;

			 if (objCarts.CartsWorkedHard == null)
				 cartsWorkedHard = System.DBNull.Value;

			 if (objCarts.CartsWorkedSoft == null)
				 cartsWorkedSoft = System.DBNull.Value;

			 if (objCarts.OnHandHard == null)
				 onHandHard = System.DBNull.Value;

			 if (objCarts.OnHandSoft == null)
				 onHandSoft = System.DBNull.Value;

			 if (objCarts.HangTotal == null) 
                 hangTotal = System.DBNull.Value; 
 
             if (objCarts.ThrownCount == null) 
                 thrownCount = System.DBNull.Value; 
 
             if (objCarts.ThrownLbs == null) 
                 thrownLbs = System.DBNull.Value; 
 
             if (objCarts.RaggedLbs == null) 
                 raggedLbs = System.DBNull.Value; 
 
             // for update only 
             if (isUpdate) 
             { 
                 command.Parameters.AddWithValue("@cartsID", objCarts.CartsID); 
             } 
 
             command.Parameters.AddWithValue("@date", date); 
             command.Parameters.AddWithValue("@locationID", locationID);
			 command.Parameters.AddWithValue("@cartsWorkedHard", cartsWorkedHard);
			 command.Parameters.AddWithValue("@cartsWorkedSoft", cartsWorkedSoft);
			 command.Parameters.AddWithValue("@onHandHard", onHandHard);
			 command.Parameters.AddWithValue("@onHandSoft", onHandSoft);
			 command.Parameters.AddWithValue("@hangTotal", hangTotal); 
             command.Parameters.AddWithValue("@thrownCount", thrownCount); 
             command.Parameters.AddWithValue("@thrownLbs", thrownLbs); 
             command.Parameters.AddWithValue("@raggedLbs", raggedLbs); 
 
             // execute and return value 
             int newlyCreatedCartsID = objCarts.CartsID; 
 
             if (isUpdate) 
                 command.ExecuteNonQuery(); 
             else 
                 newlyCreatedCartsID = (int)command.ExecuteScalar(); 
 
             command.Dispose(); 
             connection.Close(); 
             connection.Dispose(); 
 
             return newlyCreatedCartsID; 
         } 
 
         /// <summary>
         /// Deletes a record based on primary key(s) 
         /// </summary>
         public static void Delete(int cartsID) 
         { 
             SqlConnection connection = Dbase.GetConnection(); 
             SqlCommand command = Dbase.GetCommand("[dbo].[pr_Carts_Delete]", connection); 
 
             command.Parameters.AddWithValue("@cartsID", cartsID); 
 
             // execute stored proc 
             command.ExecuteNonQuery(); 
             command.Dispose(); 
             connection.Close(); 
             connection.Dispose(); 
         } 
     }
}


#line default
#line hidden
