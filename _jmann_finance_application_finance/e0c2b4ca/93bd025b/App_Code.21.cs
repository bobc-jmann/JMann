#pragma checksum "D:\JMann\Finance\Application\Finance\App_Code\DataLayerBase\EmployeeDataLayerBase.cs" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "C1D0634AFBB7B332322CFC2BAC1880EC7AC5B938"

#line 1 "D:\JMann\Finance\Application\Finance\App_Code\DataLayerBase\EmployeeDataLayerBase.cs"
using System;
using System.Data;
using System.Data.SqlClient;
using jmann.BusinessObject;
 
namespace jmann.DataLayer.Base
{
     /// <summary>
     /// Base class for EmployeeDataLayer.  Do not make changes to this class,
     /// instead, put additional code in the EmployeeDataLayer class 
     /// </summary>
     public class EmployeeDataLayerBase
     {
         // constructor 
         public EmployeeDataLayerBase()
         {
         }

         /// <summary>
         /// Selects a record by primary key(s)
         /// </summary>
         public static Employee SelectByPrimaryKey(int employeeID)
         {
              string storedProcName = "[dbo].[pr_Employee_SelectByPrimaryKey]";
              SqlConnection connection = Dbase.GetConnection();
              SqlCommand command = Dbase.GetCommand(storedProcName, connection);

              // parameters
              command.Parameters.AddWithValue("@employeeID", employeeID);

              DataSet ds = Dbase.GetDbaseDataSet(command);
              Employee objEmployee = null;

              if (ds.Tables[0].Rows.Count > 0)
              {
                  objEmployee = new Employee();
                  objEmployee.EmployeeID = (int)ds.Tables[0].Rows[0]["EmployeeID"];

                  if (ds.Tables[0].Rows[0]["Department"] != System.DBNull.Value)
                      objEmployee.Department = (int)ds.Tables[0].Rows[0]["Department"];
                  else
                      objEmployee.Department = null;

                  if (ds.Tables[0].Rows[0]["Name"] != System.DBNull.Value)
                      objEmployee.Name = (string)ds.Tables[0].Rows[0]["Name"];
                  else
                      objEmployee.Name = null;

                  if (ds.Tables[0].Rows[0]["LocationID"] != System.DBNull.Value)
                      objEmployee.LocationID = (int)ds.Tables[0].Rows[0]["LocationID"];
                  else
                      objEmployee.LocationID = null;

                  if (ds.Tables[0].Rows[0]["LastName"] != System.DBNull.Value)
                      objEmployee.LastName = (string)ds.Tables[0].Rows[0]["LastName"];
                  else
                      objEmployee.LastName = null;

                  if (ds.Tables[0].Rows[0]["FirstName"] != System.DBNull.Value)
                      objEmployee.FirstName = (string)ds.Tables[0].Rows[0]["FirstName"];
                  else
                      objEmployee.FirstName = null;

                  if (ds.Tables[0].Rows[0]["Expired"] != System.DBNull.Value)
                      objEmployee.Expired = (bool)ds.Tables[0].Rows[0]["Expired"];
                  else
                      objEmployee.Expired = null;

              }
              command.Dispose();
              connection.Close();
              connection.Dispose();
              ds.Dispose();

              return objEmployee;
         }
 
         /// <summary>
         /// Selects all Employee 
         /// </summary> 
         public static EmployeeCollection SelectAll() 
         { 
             return SelectShared("[dbo].[pr_Employee_SelectAll]", String.Empty, null); 
         } 
          /// <summary>
         /// Selects all Employee by Location, related to column LocationID 
         /// </summary> 
         public static EmployeeCollection SelectEmployeeCollectionByLocation(int locationID) 
         { 
             return SelectShared("[dbo].[pr_Employee_SelectByLocation]", "locationID", locationID); 
         } 
         /// <summary>
         /// Selects EmployeeID and Department columns for use with a DropDownList web control 
         /// </summary> 
         public static EmployeeCollection SelectEmployeeDropDownListData(int locationID) 
         { 
              string storedProcName = "[dbo].[pr_Employee_SelectByLocation]";
              SqlConnection connection = Dbase.GetConnection();
              SqlCommand command = Dbase.GetCommand(storedProcName, connection);
              // parameters
              command.Parameters.AddWithValue("@locationID", locationID); 
              DataSet ds = Dbase.GetDbaseDataSet(command); 
              EmployeeCollection objEmployeeCol = new EmployeeCollection(); 
              Employee objEmployee; 
 
              if (ds.Tables[0].Rows.Count > 0) 
              { 
                 foreach(DataRow dr in ds.Tables[0].Rows) 
                 { 
                     objEmployee = new Employee(); 
                     objEmployee.EmployeeID = (int)dr["EmployeeID"]; 
                   
              
                     if (dr["LocationID"] != System.DBNull.Value) 
                         objEmployee.LocationID = (int)dr["LocationID"]; 
                     else 
                         objEmployee.LocationID = null; 


                     objEmployeeCol.Add(objEmployee);
                 } 
              } 
 
              command.Dispose(); 
              connection.Close(); 
              connection.Dispose(); 
              ds.Dispose(); 
 
              return objEmployeeCol; 
         } 
 
         public static EmployeeCollection SelectShared(string storedProcName, string param, object paramValue) 
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
              EmployeeCollection objEmployeeCol = new EmployeeCollection(); 
              Employee objEmployee; 
 
              if (ds.Tables[0].Rows.Count > 0) 
              { 
                 foreach(DataRow dr in ds.Tables[0].Rows) 
                 { 
                     objEmployee = new Employee(); 
                     objEmployee.EmployeeID = (int)dr["EmployeeID"]; 
 
                     if (dr["Department"] != System.DBNull.Value) 
                         objEmployee.Department = (int)dr["Department"]; 
                     else 
                         objEmployee.Department = null; 
 
                     if (dr["Name"] != System.DBNull.Value) 
                         objEmployee.Name = dr["Name"].ToString(); 
                     else 
                         objEmployee.Name = null; 
 
                     if (dr["LocationID"] != System.DBNull.Value) 
                         objEmployee.LocationID = (int)dr["LocationID"]; 
                     else 
                         objEmployee.LocationID = null; 
 
                     if (dr["LastName"] != System.DBNull.Value) 
                         objEmployee.LastName = dr["LastName"].ToString(); 
                     else 
                         objEmployee.LastName = null; 
 
                     if (dr["FirstName"] != System.DBNull.Value) 
                         objEmployee.FirstName = dr["FirstName"].ToString(); 
                     else 
                         objEmployee.FirstName = null; 
 
                     if (dr["Expired"] != System.DBNull.Value) 
                         objEmployee.Expired = (bool)dr["Expired"]; 
                     else 
                         objEmployee.Expired = null; 
 
                     objEmployeeCol.Add(objEmployee);
                 } 
              } 
 
              command.Dispose(); 
              connection.Close(); 
              connection.Dispose(); 
              ds.Dispose(); 
 
              return objEmployeeCol; 
         } 
 
         /// <summary>
         /// Inserts a record 
         /// </summary> 
         public static int Insert(Employee objEmployee) 
         { 
             string storedProcName = "[dbo].[pr_Employee_Insert]"; 
             return InsertUpdate(objEmployee, false, storedProcName); 
         } 
 
         /// <summary>
         /// Updates a record 
         /// </summary>
         public static void Update(Employee objEmployee) 
         { 
             string storedProcName = "[dbo].[pr_Employee_Update]"; 
             InsertUpdate(objEmployee, true, storedProcName); 
         } 
 
         private static int InsertUpdate(Employee objEmployee, bool isUpdate, string storedProcName) 
         { 
             SqlConnection connection = Dbase.GetConnection(); 
             SqlCommand command = Dbase.GetCommand(storedProcName, connection); 
 
             object department = objEmployee.Department; 
             object name = objEmployee.Name; 
             object locationID = objEmployee.LocationID; 
             object lastName = objEmployee.LastName; 
             object firstName = objEmployee.FirstName; 
             object expired = objEmployee.Expired; 
 
             if (objEmployee.Department == null) 
                 department = System.DBNull.Value; 
 
             if (String.IsNullOrEmpty(objEmployee.Name)) 
                 name = System.DBNull.Value; 
 
             if (objEmployee.LocationID == null) 
                 locationID = System.DBNull.Value; 
 
             if (String.IsNullOrEmpty(objEmployee.LastName)) 
                 lastName = System.DBNull.Value; 
 
             if (String.IsNullOrEmpty(objEmployee.FirstName)) 
                 firstName = System.DBNull.Value; 
 
             if (objEmployee.Expired == null) 
                 expired = System.DBNull.Value; 
 
             // for update only 
             if (isUpdate) 
             { 
                 command.Parameters.AddWithValue("@employeeID", objEmployee.EmployeeID); 
             } 
 
             command.Parameters.AddWithValue("@department", department); 
             command.Parameters.AddWithValue("@name", name); 
             command.Parameters.AddWithValue("@locationID", locationID); 
             command.Parameters.AddWithValue("@lastName", lastName); 
             command.Parameters.AddWithValue("@firstName", firstName); 
             command.Parameters.AddWithValue("@expired", expired); 
 
             // execute and return value 
             int newlyCreatedEmployeeID = objEmployee.EmployeeID; 
 
             if (isUpdate) 
                 command.ExecuteNonQuery(); 
             else 
                 newlyCreatedEmployeeID = (int)command.ExecuteScalar(); 
 
             command.Dispose(); 
             connection.Close(); 
             connection.Dispose(); 
 
             return newlyCreatedEmployeeID; 
         } 
 
         /// <summary>
         /// Deletes a record based on primary key(s) 
         /// </summary>
         public static void Delete(int employeeID) 
         { 
             SqlConnection connection = Dbase.GetConnection(); 
             SqlCommand command = Dbase.GetCommand("[dbo].[pr_Employee_Delete]", connection); 
 
             command.Parameters.AddWithValue("@employeeID", employeeID); 
 
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
