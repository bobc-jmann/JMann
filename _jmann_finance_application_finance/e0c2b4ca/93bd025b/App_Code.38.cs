#pragma checksum "D:\JMann\Finance\Application\Finance\App_Code\BusinessObjectBase\EmployeeBase.cs" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "2B77B69DEF0FBAE8F68C9408042F0FF88E6DCF13"

#line 1 "D:\JMann\Finance\Application\Finance\App_Code\BusinessObjectBase\EmployeeBase.cs"
using System; 
using System.Data; 
using jmann.DataLayer; 
using jmann.BusinessObject; 
using System.Web.Script.Serialization; 
 
namespace jmann.BusinessObject.Base 
{ 
     /// <summary>
     /// Base class for Employee.  Do not make changes to this class,
     /// instead, put additional code in the Employee class 
     /// </summary>
     public class EmployeeBase
     { 
         /// <summary> 
         /// Gets or Sets EmployeeID 
         /// </summary> 
         public int EmployeeID { get; set; } 
 
         /// <summary> 
         /// Gets or Sets Department 
         /// </summary> 
         public int? Department { get; set; } 
 
         /// <summary> 
         /// Gets or Sets Name 
         /// </summary> 
         public string Name { get; set; } 
 
         /// <summary> 
         /// Gets or Sets LocationID 
         /// </summary> 
         public int? LocationID { get; set; } 
 
         /// <summary> 
         /// Gets or Sets LastName 
         /// </summary> 
         public string LastName { get; set; } 
 
         /// <summary> 
         /// Gets or Sets FirstName 
         /// </summary> 
         public string FirstName { get; set; } 
 
         /// <summary> 
         /// Gets or Sets Expired 
         /// </summary> 
         public bool? Expired { get; set; } 
 
         /// <summary> 
         /// Gets or sets the related RegisterRecord(s) by EmployeeID 
         /// </summary> 
         [ScriptIgnore] 
         public Lazy<RegisterRecordCollection> RegisterRecordCollection 
         { 
             get 
             { 
                 int value; 
                 bool hasValue = Int32.TryParse(EmployeeID.ToString(), out value); 
 
                 if (hasValue) 
                     return new Lazy<RegisterRecordCollection>(() => jmann.BusinessObject.RegisterRecord.SelectRegisterRecordCollectionByEmployee(value)); 
                 else 
                     return null; 
             } 
             set { } 
         } 
 
 
         /// <summary> 
         /// Constructor 
         /// </summary> 
         public EmployeeBase() 
         { 
         } 
 
         /// <summary>
         /// Selects a record by primary key(s) 
         /// </summary>
         public static Employee SelectByPrimaryKey(int employeeID) 
         { 
             return EmployeeDataLayer.SelectByPrimaryKey(employeeID); 
         } 
 
         /// <summary> 
         /// Selects all records as a collection (List) of Employee 
         /// </summary> 
         public static EmployeeCollection SelectAll() 
         { 
             return EmployeeDataLayer.SelectAll(); 
         } 
 
         /// <summary> 
         /// Selects all records as a collection (List) of Employee sorted by the sort expression 
         /// </summary> 
         public static EmployeeCollection SelectAll(string sortExpression) 
         { 
             EmployeeCollection objEmployeeCol = EmployeeDataLayer.SelectAll();
             return SortByExpression(objEmployeeCol, sortExpression);
         } 

         /// <summary>
         /// Selects all records as a collection (List) of Employee by Location
         /// </summary> 
         public static EmployeeCollection SelectEmployeeCollectionByLocation(int locationID) 
         { 
             return EmployeeDataLayer.SelectEmployeeCollectionByLocation(locationID); 
         } 
         /// <summary>
         /// Selects EmployeeID and Department columns for use with a DropDownList web control, ComboBox, CheckedBoxList, ListView, ListBox, etc 
         /// </summary> 
         public static EmployeeCollection SelectEmployeeDropDownListData(int locationID) 
         { 
             return EmployeeDataLayer.SelectEmployeeDropDownListData(locationID); 
         } 
 
         /// <summary>
         /// Sorts the EmployeeCollection by sort expression 
         /// </summary> 
         public static EmployeeCollection SortByExpression(EmployeeCollection objEmployeeCol, string sortExpression) 
         { 
             bool isSortDescending = sortExpression.Contains(" DESC");

             if (isSortDescending)
                 sortExpression = sortExpression.Replace(" DESC", "");

             switch (sortExpression)
             {
                 case "EmployeeID":
                     objEmployeeCol.Sort(jmann.BusinessObject.Employee.ByEmployeeID);
                     break;
                 case "Department":
                     objEmployeeCol.Sort(jmann.BusinessObject.Employee.ByDepartment);
                     break;
                 case "Name":
                     objEmployeeCol.Sort(jmann.BusinessObject.Employee.ByName);
                     break;
                 case "LocationID":
                     objEmployeeCol.Sort(jmann.BusinessObject.Employee.ByLocationID);
                     break;
                 case "LastName":
                     objEmployeeCol.Sort(jmann.BusinessObject.Employee.ByLastName);
                     break;
                 case "FirstName":
                     objEmployeeCol.Sort(jmann.BusinessObject.Employee.ByFirstName);
                     break;
                 case "Expired":
                     objEmployeeCol.Sort(jmann.BusinessObject.Employee.ByExpired);
                     break;
                 default:
                     break;
             }

             if (isSortDescending) 
                 objEmployeeCol.Reverse();

             return objEmployeeCol;
         } 
 
         /// <summary>
         /// Inserts a record 
         /// </summary> 
         public int Insert() 
         { 
             Employee objEmployee = (Employee)this; 
             return EmployeeDataLayer.Insert(objEmployee); 
         } 
 
         /// <summary>
         /// Updates a record 
         /// </summary> 
         public void Update() 
         { 
             Employee objEmployee = (Employee)this; 
             EmployeeDataLayer.Update(objEmployee); 
         } 
 
         /// <summary>
         /// Deletes a record based on primary key(s) 
         /// </summary>
         public static void Delete(int employeeID) 
         { 
             EmployeeDataLayer.Delete(employeeID); 
         } 
 
         /// <summary> 
         /// Compares EmployeeID used for sorting 
         /// </summary> 
         public static Comparison<Employee> ByEmployeeID = delegate(Employee x, Employee y) 
         { 
             return x.EmployeeID.CompareTo(y.EmployeeID); 
         }; 
 
         /// <summary> 
         /// Compares Department used for sorting 
         /// </summary> 
         public static Comparison<Employee> ByDepartment = delegate(Employee x, Employee y) 
         { 
             return Nullable.Compare(x.Department, y.Department); 
         }; 
 
         /// <summary> 
         /// Compares Name used for sorting 
         /// </summary> 
         public static Comparison<Employee> ByName = delegate(Employee x, Employee y) 
         { 
             string value1 = x.Name ?? String.Empty; 
             string value2 = y.Name ?? String.Empty; 
             return value1.CompareTo(value2); 
         }; 
 
         /// <summary> 
         /// Compares LocationID used for sorting 
         /// </summary> 
         public static Comparison<Employee> ByLocationID = delegate(Employee x, Employee y) 
         { 
             return Nullable.Compare(x.LocationID, y.LocationID); 
         }; 
 
         /// <summary> 
         /// Compares LastName used for sorting 
         /// </summary> 
         public static Comparison<Employee> ByLastName = delegate(Employee x, Employee y) 
         { 
             string value1 = x.LastName ?? String.Empty; 
             string value2 = y.LastName ?? String.Empty; 
             return value1.CompareTo(value2); 
         }; 
 
         /// <summary> 
         /// Compares FirstName used for sorting 
         /// </summary> 
         public static Comparison<Employee> ByFirstName = delegate(Employee x, Employee y) 
         { 
             string value1 = x.FirstName ?? String.Empty; 
             string value2 = y.FirstName ?? String.Empty; 
             return value1.CompareTo(value2); 
         }; 
 
         /// <summary> 
         /// Compares Expired used for sorting 
         /// </summary> 
         public static Comparison<Employee> ByExpired = delegate(Employee x, Employee y) 
         { 
             return Nullable.Compare(x.Expired, y.Expired); 
         }; 
 
     } 
} 


#line default
#line hidden
