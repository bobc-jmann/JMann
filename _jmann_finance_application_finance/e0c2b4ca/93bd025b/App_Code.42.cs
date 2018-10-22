#pragma checksum "D:\JMann\Finance\Application\Finance\App_Code\DataLayerBase\RegisterRecordDataLayerBase.cs" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "632BA8C8EE0B91095083A2F3FC9C03386FC7DEDA"

#line 1 "D:\JMann\Finance\Application\Finance\App_Code\DataLayerBase\RegisterRecordDataLayerBase.cs"
using System;
using System.Data;
using System.Data.SqlClient;
using jmann.BusinessObject;
 
namespace jmann.DataLayer.Base
{
     /// <summary>
     /// Base class for RegisterRecordDataLayer.  Do not make changes to this class,
     /// instead, put additional code in the RegisterRecordDataLayer class 
     /// </summary>
     public class RegisterRecordDataLayerBase
     {
         // constructor 
         public RegisterRecordDataLayerBase()
         {
         }

         /// <summary>
         /// Selects a record by primary key(s)
         /// </summary>
         public static RegisterRecord SelectByPrimaryKey(int registerRecordID)
         {
              string storedProcName = "[dbo].[pr_RegisterRecord_SelectByPrimaryKey]";
              SqlConnection connection = Dbase.GetConnection();
              SqlCommand command = Dbase.GetCommand(storedProcName, connection);

              // parameters
              command.Parameters.AddWithValue("@registerRecordID", registerRecordID);

              DataSet ds = Dbase.GetDbaseDataSet(command);
              RegisterRecord objRegisterRecord = null;

              if (ds.Tables[0].Rows.Count > 0)
              {
                  objRegisterRecord = new RegisterRecord();
                  objRegisterRecord.RegisterRecordID = (int)ds.Tables[0].Rows[0]["RegisterRecordID"];

                  if (ds.Tables[0].Rows[0]["Register"] != System.DBNull.Value)
                      objRegisterRecord.Register = (int)ds.Tables[0].Rows[0]["Register"];
                  else
                      objRegisterRecord.Register = null;

                  if (ds.Tables[0].Rows[0]["Record"] != System.DBNull.Value)
                      objRegisterRecord.Record = (int)ds.Tables[0].Rows[0]["Record"];
                  else
                      objRegisterRecord.Record = null;

                  if (ds.Tables[0].Rows[0]["EmployeeID"] != System.DBNull.Value)
                      objRegisterRecord.EmployeeID = (int)ds.Tables[0].Rows[0]["EmployeeID"];
                  else
                      objRegisterRecord.EmployeeID = null;


                  if (ds.Tables[0].Rows[0]["LocationID"] != System.DBNull.Value)
                      objRegisterRecord.LocationID = (int)ds.Tables[0].Rows[0]["LocationID"];
                  else
                      objRegisterRecord.LocationID = null;


                  if (ds.Tables[0].Rows[0]["Date"] != System.DBNull.Value)
                      objRegisterRecord.Date = (DateTime)ds.Tables[0].Rows[0]["Date"];
                  else
                      objRegisterRecord.Date = null;

                  if (ds.Tables[0].Rows[0]["CustomerCount"] != System.DBNull.Value)
                      objRegisterRecord.CustomerCount = (int)ds.Tables[0].Rows[0]["CustomerCount"];
                  else
                      objRegisterRecord.CustomerCount = null;

                  if (ds.Tables[0].Rows[0]["Coins"] != System.DBNull.Value)
                      objRegisterRecord.Coins = (decimal)ds.Tables[0].Rows[0]["Coins"];
                  else
                      objRegisterRecord.Coins = null;

                  if (ds.Tables[0].Rows[0]["Currency"] != System.DBNull.Value)
                      objRegisterRecord.Currency = (decimal)ds.Tables[0].Rows[0]["Currency"];
                  else
                      objRegisterRecord.Currency = null;

                  if (ds.Tables[0].Rows[0]["MiscCash"] != System.DBNull.Value)
                      objRegisterRecord.MiscCash = (decimal)ds.Tables[0].Rows[0]["MiscCash"];
                  else
                      objRegisterRecord.MiscCash = null;

                  if (ds.Tables[0].Rows[0]["Visa"] != System.DBNull.Value)
                      objRegisterRecord.Visa = (decimal)ds.Tables[0].Rows[0]["Visa"];
                  else
                      objRegisterRecord.Visa = null;

                  if (ds.Tables[0].Rows[0]["Mastercard"] != System.DBNull.Value)
                      objRegisterRecord.Mastercard = (decimal)ds.Tables[0].Rows[0]["Mastercard"];
                  else
                      objRegisterRecord.Mastercard = null;

                  if (ds.Tables[0].Rows[0]["Discover"] != System.DBNull.Value)
                      objRegisterRecord.Discover = (decimal)ds.Tables[0].Rows[0]["Discover"];
                  else
                      objRegisterRecord.Discover = null;

                  if (ds.Tables[0].Rows[0]["Atm"] != System.DBNull.Value)
                      objRegisterRecord.Atm = (decimal)ds.Tables[0].Rows[0]["Atm"];
                  else
                      objRegisterRecord.Atm = null;

                  if (ds.Tables[0].Rows[0]["GiftCertificate"] != System.DBNull.Value)
                      objRegisterRecord.GiftCertificate = (decimal)ds.Tables[0].Rows[0]["GiftCertificate"];
                  else
                      objRegisterRecord.GiftCertificate = null;

                  if (ds.Tables[0].Rows[0]["ZTapeCash"] != System.DBNull.Value)
                      objRegisterRecord.ZTapeCash = (decimal)ds.Tables[0].Rows[0]["ZTapeCash"];
                  else
                      objRegisterRecord.ZTapeCash = null;

                  if (ds.Tables[0].Rows[0]["ZTapeCharge"] != System.DBNull.Value)
                      objRegisterRecord.ZTapeCharge = (decimal)ds.Tables[0].Rows[0]["ZTapeCharge"];
                  else
                      objRegisterRecord.ZTapeCharge = null;

                  if (ds.Tables[0].Rows[0]["ZTapeTotal"] != System.DBNull.Value)
                      objRegisterRecord.ZTapeTotal = (decimal)ds.Tables[0].Rows[0]["ZTapeTotal"];
                  else
                      objRegisterRecord.ZTapeTotal = null;

                  if (ds.Tables[0].Rows[0]["Overring"] != System.DBNull.Value)
                      objRegisterRecord.Overring = (decimal)ds.Tables[0].Rows[0]["Overring"];
                  else
                      objRegisterRecord.Overring = null;

                  if (ds.Tables[0].Rows[0]["OverringCount"] != System.DBNull.Value)
                      objRegisterRecord.OverringCount = (int)ds.Tables[0].Rows[0]["OverringCount"];
                  else
                      objRegisterRecord.OverringCount = null;

                  if (ds.Tables[0].Rows[0]["ActualCashOverShort"] != System.DBNull.Value)
                      objRegisterRecord.ActualCashOverShort = (decimal)ds.Tables[0].Rows[0]["ActualCashOverShort"];
                  else
                      objRegisterRecord.ActualCashOverShort = null;
              }
              command.Dispose();
              connection.Close();
              connection.Dispose();
              ds.Dispose();

              return objRegisterRecord;
         }
 
         /// <summary>
         /// Selects all RegisterRecord 
         /// </summary> 
         public static RegisterRecordCollection SelectAll() 
         { 
             return SelectShared("[dbo].[pr_RegisterRecord_SelectAll]", String.Empty, null); 
         } 
 
         /// <summary>
         /// Selects all RegisterRecord by Employee, related to column EmployeeID 
         /// </summary> 
         public static RegisterRecordCollection SelectRegisterRecordCollectionByEmployee(int employeeID) 
         { 
             return SelectShared("[dbo].[pr_RegisterRecord_SelectAllByEmployee]", "employeeID", employeeID); 
         } 
 
         
         /// <summary>
         /// Selects all RegisterRecord by Location, related to column LocationID 
         /// </summary> 
         public static RegisterRecordCollection SelectRegisterRecordCollectionByLocation(int locationID) 
         { 
             return SelectShared("[dbo].[pr_RegisterRecord_SelectAllByLocation]", "locationID", locationID); 
         } 
 
         /// <summary>
         /// Selects RegisterRecordID and Register columns for use with a DropDownList web control 
         /// </summary> 
         public static RegisterRecordCollection SelectRegisterRecordDropDownListData() 
         { 
              string storedProcName = "[dbo].[pr_RegisterRecord_SelectAll]";
              SqlConnection connection = Dbase.GetConnection();
              SqlCommand command = Dbase.GetCommand(storedProcName, connection);
 
              DataSet ds = Dbase.GetDbaseDataSet(command); 
              RegisterRecordCollection objRegisterRecordCol = new RegisterRecordCollection(); 
              RegisterRecord objRegisterRecord; 
 
              if (ds.Tables[0].Rows.Count > 0) 
              { 
                 foreach(DataRow dr in ds.Tables[0].Rows) 
                 { 
                     objRegisterRecord = new RegisterRecord(); 
                     objRegisterRecord.RegisterRecordID = (int)dr["RegisterRecordID"]; 
 
                     if (dr["Register"] != System.DBNull.Value) 
                         objRegisterRecord.Register = (int)(dr["Register"]); 
                     else 
                         objRegisterRecord.Register = null; 
 
                     objRegisterRecordCol.Add(objRegisterRecord);
                 } 
              } 
 
              command.Dispose(); 
              connection.Close(); 
              connection.Dispose(); 
              ds.Dispose(); 
 
              return objRegisterRecordCol; 
         } 
 
         public static RegisterRecordCollection SelectShared(string storedProcName, string param, object paramValue) 
         { 
              SqlConnection connection = Dbase.GetConnection(); 
              SqlCommand command = Dbase.GetCommand(storedProcName, connection); 
 
              // parameters 
              switch (param) 
              { 
                  case "employeeID": 
                      command.Parameters.AddWithValue("@employeeID", paramValue); 
                      break; 
                  case "locationID": 
                      command.Parameters.AddWithValue("@locationID", paramValue); 
                      break; 
                  default: 
                      break; 
              } 
 
              DataSet ds = Dbase.GetDbaseDataSet(command); 
              RegisterRecordCollection objRegisterRecordCol = new RegisterRecordCollection(); 
              RegisterRecord objRegisterRecord; 
 
              if (ds.Tables[0].Rows.Count > 0) 
              { 
                 foreach(DataRow dr in ds.Tables[0].Rows) 
                 { 
                     objRegisterRecord = new RegisterRecord(); 
                     objRegisterRecord.RegisterRecordID = (int)dr["RegisterRecordID"]; 
 
                     if (dr["Register"] != System.DBNull.Value) 
                         objRegisterRecord.Register = (int)dr["Register"]; 
                     else 
                         objRegisterRecord.Register = null; 
 
                     if (dr["Record"] != System.DBNull.Value) 
                         objRegisterRecord.Record = (int)dr["Record"]; 
                     else 
                         objRegisterRecord.Record = null; 
 
                     if (dr["EmployeeID"] != System.DBNull.Value) 
                         objRegisterRecord.EmployeeID = (int)dr["EmployeeID"];
                     else
                         objRegisterRecord.EmployeeID = null;
 
 
                     if (dr["LocationID"] != System.DBNull.Value) 
                         objRegisterRecord.LocationID = (int)dr["LocationID"];
                     else
                         objRegisterRecord.LocationID = null;
 
 
                     if (dr["Date"] != System.DBNull.Value) 
                         objRegisterRecord.Date = (DateTime)dr["Date"]; 
                     else 
                         objRegisterRecord.Date = null; 
 
                     if (dr["CustomerCount"] != System.DBNull.Value) 
                         objRegisterRecord.CustomerCount = (int)dr["CustomerCount"]; 
                     else 
                         objRegisterRecord.CustomerCount = null; 
 
                     if (dr["Coins"] != System.DBNull.Value) 
                         objRegisterRecord.Coins = (decimal)dr["Coins"]; 
                     else 
                         objRegisterRecord.Coins = null; 
 
                     if (dr["Currency"] != System.DBNull.Value) 
                         objRegisterRecord.Currency = (decimal)dr["Currency"]; 
                     else 
                         objRegisterRecord.Currency = null; 
 
                     if (dr["MiscCash"] != System.DBNull.Value) 
                         objRegisterRecord.MiscCash = (decimal)dr["MiscCash"]; 
                     else 
                         objRegisterRecord.MiscCash = null; 
 
                     if (dr["Visa"] != System.DBNull.Value) 
                         objRegisterRecord.Visa = (decimal)dr["Visa"]; 
                     else 
                         objRegisterRecord.Visa = null; 
 
                     if (dr["Mastercard"] != System.DBNull.Value) 
                         objRegisterRecord.Mastercard = (decimal)dr["Mastercard"]; 
                     else 
                         objRegisterRecord.Mastercard = null; 
 
                     if (dr["Discover"] != System.DBNull.Value) 
                         objRegisterRecord.Discover = (decimal)dr["Discover"]; 
                     else 
                         objRegisterRecord.Discover = null; 
 
                     if (dr["Atm"] != System.DBNull.Value) 
                         objRegisterRecord.Atm = (decimal)dr["Atm"]; 
                     else 
                         objRegisterRecord.Atm = null; 
 
                     if (dr["GiftCertificate"] != System.DBNull.Value) 
                         objRegisterRecord.GiftCertificate = (decimal)dr["GiftCertificate"]; 
                     else 
                         objRegisterRecord.GiftCertificate = null; 
 
                     if (dr["ZTapeCash"] != System.DBNull.Value) 
                         objRegisterRecord.ZTapeCash = (decimal)dr["ZTapeCash"]; 
                     else 
                         objRegisterRecord.ZTapeCash = null; 
 
                     if (dr["ZTapeCharge"] != System.DBNull.Value) 
                         objRegisterRecord.ZTapeCharge = (decimal)dr["ZTapeCharge"]; 
                     else 
                         objRegisterRecord.ZTapeCharge = null; 
                                                   
                     if (dr["ZTapeTotal"] != System.DBNull.Value) 
                         objRegisterRecord.ZTapeTotal = (decimal)dr["ZTapeTotal"]; 
                     else 
                         objRegisterRecord.ZTapeTotal = null; 
 
                     if (dr["Overring"] != System.DBNull.Value) 
                         objRegisterRecord.Overring = (decimal)dr["Overring"]; 
                     else 
                         objRegisterRecord.Overring = null; 
 
                     if (dr["OverringCount"] != System.DBNull.Value) 
                         objRegisterRecord.OverringCount = (int)dr["OverringCount"]; 
                     else 
                         objRegisterRecord.OverringCount = null; 
 
                     if (dr["ActualCashOverShort"] != System.DBNull.Value) 
                         objRegisterRecord.ActualCashOverShort = (decimal)dr["ActualCashOverShort"]; 
                     else 
                         objRegisterRecord.ActualCashOverShort = null;
                           
                     objRegisterRecordCol.Add(objRegisterRecord);
                 } 
              } 
 
              command.Dispose(); 
              connection.Close(); 
              connection.Dispose(); 
              ds.Dispose(); 
 
              return objRegisterRecordCol; 
         } 
 
         /// <summary>
         /// Inserts a record 
         /// </summary> 
         public static int Insert(RegisterRecord objRegisterRecord) 
         { 
             string storedProcName = "[dbo].[pr_RegisterRecord_Insert]"; 
             return InsertUpdate(objRegisterRecord, false, storedProcName); 
         } 
 
         /// <summary>
         /// Updates a record 
         /// </summary>
         public static void Update(RegisterRecord objRegisterRecord) 
         { 
             string storedProcName = "[dbo].[pr_RegisterRecord_Update]"; 
             InsertUpdate(objRegisterRecord, true, storedProcName); 
         } 
 
         private static int InsertUpdate(RegisterRecord objRegisterRecord, bool isUpdate, string storedProcName) 
         { 
             SqlConnection connection = Dbase.GetConnection(); 
             SqlCommand command = Dbase.GetCommand(storedProcName, connection); 
 
             object register = objRegisterRecord.Register; 
             object record = objRegisterRecord.Record; 
             object employeeID = objRegisterRecord.EmployeeID; 
             object locationID = objRegisterRecord.LocationID; 
             object date = objRegisterRecord.Date; 
             object customerCount = objRegisterRecord.CustomerCount; 
             object coins = objRegisterRecord.Coins; 
             object currency = objRegisterRecord.Currency; 
             object miscCash = objRegisterRecord.MiscCash; 
             object visa = objRegisterRecord.Visa; 
             object mastercard = objRegisterRecord.Mastercard; 
             object discover = objRegisterRecord.Discover; 
             object atm = objRegisterRecord.Atm; 
             object giftCertificate = objRegisterRecord.GiftCertificate; 
             object zTapeCash = objRegisterRecord.ZTapeCash; 
             object zTapeCharge = objRegisterRecord.ZTapeCharge;  
             object zTapeTotal = objRegisterRecord.ZTapeTotal; 
             object overring = objRegisterRecord.Overring; 
             object overringCount = objRegisterRecord.OverringCount; 
             object actualCashOverShort = objRegisterRecord.ActualCashOverShort; 
 
             if (objRegisterRecord.Register == null) 
                 register = System.DBNull.Value; 
 
             if (objRegisterRecord.Record == null) 
                 record = System.DBNull.Value; 
 
             if (objRegisterRecord.EmployeeID == null) 
                 employeeID = System.DBNull.Value; 
 
             if (objRegisterRecord.LocationID == null) 
                 locationID = System.DBNull.Value; 
             
             if (objRegisterRecord.Date == null) 
                 date = System.DBNull.Value; 
 
             if (objRegisterRecord.CustomerCount == null) 
                 customerCount = System.DBNull.Value; 
 
             if (objRegisterRecord.Coins == null) 
                 coins = System.DBNull.Value; 
 
             if (objRegisterRecord.Currency == null) 
                 currency = System.DBNull.Value; 
 
             if (objRegisterRecord.MiscCash == null) 
                 miscCash = System.DBNull.Value; 
 
             if (objRegisterRecord.Visa == null) 
                 visa = System.DBNull.Value; 
 
             if (objRegisterRecord.Mastercard == null) 
                 mastercard = System.DBNull.Value; 
 
             if (objRegisterRecord.Discover == null) 
                 discover = System.DBNull.Value; 
 
             if (objRegisterRecord.Atm == null) 
                 atm = System.DBNull.Value; 
 
             if (objRegisterRecord.GiftCertificate == null) 
                 giftCertificate = System.DBNull.Value; 
 
             if (objRegisterRecord.ZTapeCash == null) 
                 zTapeCash = System.DBNull.Value; 
  
             if (objRegisterRecord.ZTapeCharge == null) 
                 zTapeCharge = System.DBNull.Value; 
  
             if (objRegisterRecord.ZTapeTotal == null) 
                 zTapeTotal = System.DBNull.Value; 
 
             if (objRegisterRecord.Overring == null) 
                 overring = System.DBNull.Value; 
 
             if (objRegisterRecord.OverringCount == null) 
                 overringCount = System.DBNull.Value; 

             if (objRegisterRecord.ActualCashOverShort == null) 
                 actualCashOverShort = System.DBNull.Value; 
 
             // for update only 
             if (isUpdate) 
             { 
                 command.Parameters.AddWithValue("@registerRecordID", objRegisterRecord.RegisterRecordID); 
             } 
 
             command.Parameters.AddWithValue("@register", register); 
             command.Parameters.AddWithValue("@record", record); 
             command.Parameters.AddWithValue("@employeeID", employeeID); 
             command.Parameters.AddWithValue("@locationID", locationID); 
             command.Parameters.AddWithValue("@date", date); 
             command.Parameters.AddWithValue("@customerCount", customerCount); 
             command.Parameters.AddWithValue("@coins", coins); 
             command.Parameters.AddWithValue("@currency", currency); 
             command.Parameters.AddWithValue("@miscCash", miscCash); 
             command.Parameters.AddWithValue("@visa", visa); 
             command.Parameters.AddWithValue("@mastercard", mastercard); 
             command.Parameters.AddWithValue("@discover", discover); 
             command.Parameters.AddWithValue("@atm", atm); 
             command.Parameters.AddWithValue("@giftCertificate", giftCertificate); 
             command.Parameters.AddWithValue("@zTapeCash", zTapeCash); 
             command.Parameters.AddWithValue("@zTapeCharge", zTapeCharge); 
             command.Parameters.AddWithValue("@zTapeTotal", zTapeTotal); 
             command.Parameters.AddWithValue("@overring", overring); 
             command.Parameters.AddWithValue("@overringCount", overringCount); 
             command.Parameters.AddWithValue("@actualCashOverShort", actualCashOverShort);   

             // execute and return value 
             int newlyCreatedRegisterRecordID = objRegisterRecord.RegisterRecordID; 
 
             if (isUpdate) 
                 command.ExecuteNonQuery(); 
             else 
                 newlyCreatedRegisterRecordID = (int)command.ExecuteScalar(); 
 
             command.Dispose(); 
             connection.Close(); 
             connection.Dispose(); 
 
             return newlyCreatedRegisterRecordID; 
         } 
 
         /// <summary>
         /// Deletes a record based on primary key(s) 
         /// </summary>
         public static void Delete(int registerRecordID) 
         { 
             SqlConnection connection = Dbase.GetConnection(); 
             SqlCommand command = Dbase.GetCommand("[dbo].[pr_RegisterRecord_Delete]", connection); 
 
             command.Parameters.AddWithValue("@registerRecordID", registerRecordID); 
 
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
