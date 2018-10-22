#pragma checksum "D:\JMann\Finance\Application\Finance\App_Code\BusinessObjectBase\RegisterRecordBase.cs" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "A5A278D53FFF7BA1C081E976B8E42F0E89A35CF2"

#line 1 "D:\JMann\Finance\Application\Finance\App_Code\BusinessObjectBase\RegisterRecordBase.cs"
using System; 
using System.Data; 
using jmann.DataLayer; 
using jmann.BusinessObject; 
using System.Web.Script.Serialization; 
 
namespace jmann.BusinessObject.Base 
{ 
     /// <summary>
     /// Base class for RegisterRecord.  Do not make changes to this class,
     /// instead, put additional code in the RegisterRecord class 
     /// </summary>
     public class RegisterRecordBase
     { 
         /// <summary> 
         /// Gets or Sets RegisterRecordID 
         /// </summary> 
         public int RegisterRecordID { get; set; } 
 
         /// <summary> 
         /// Gets or Sets Register 
         /// </summary> 
         public int? Register { get; set; } 
 
         /// <summary> 
         /// Gets or Sets Record 
         /// </summary> 
         public int? Record { get; set; } 
 
         /// <summary> 
         /// Gets or Sets EmployeeID 
         /// </summary> 
         public int? EmployeeID { get; set; } 
 
         /// <summary> 
         /// Gets or Sets LocationID 
         /// </summary> 
         public int? LocationID { get; set; } 
 
         /// <summary> 
         /// Gets or Sets Date 
         /// </summary> 
         public DateTime? Date { get; set; } 
 
         /// <summary> 
         /// Gets or Sets CustomerCount 
         /// </summary> 
         public int? CustomerCount { get; set; } 
 
         /// <summary> 
         /// Gets or Sets Coins 
         /// </summary> 
         public decimal? Coins { get; set; } 
 
         /// <summary> 
         /// Gets or Sets Currency 
         /// </summary> 
         public decimal? Currency { get; set; } 
 
         /// <summary> 
         /// Gets or Sets MiscCash 
         /// </summary> 
         public decimal? MiscCash { get; set; } 
 
         /// <summary> 
         /// Gets or Sets Visa 
         /// </summary> 
         public decimal? Visa { get; set; } 
 
         /// <summary> 
         /// Gets or Sets Mastercard 
         /// </summary> 
         public decimal? Mastercard { get; set; } 
 
         /// <summary> 
         /// Gets or Sets Discover 
         /// </summary> 
         public decimal? Discover { get; set; } 
 
         /// <summary> 
         /// Gets or Sets Atm 
         /// </summary> 
         public decimal? Atm { get; set; } 
 
         /// <summary> 
         /// Gets or Sets GiftCertificate 
         /// </summary> 
         public decimal? GiftCertificate { get; set; } 
 
         /// <summary> 
         /// Gets or Sets ZTapeCash
         /// </summary> 
         public decimal? ZTapeCash { get; set; } 

         /// <summary> 
         /// Gets or Sets ZTapeCharge 
         /// </summary> 
         public decimal? ZTapeCharge { get; set; } 

         /// <summary> 
         /// Gets or Sets ZTapeTotal 
         /// </summary> 
         public decimal? ZTapeTotal { get; set; } 
 
         /// <summary> 
         /// Gets or Sets Overring 
         /// </summary> 
         public decimal? Overring { get; set; } 
 
         /// <summary> 
         /// Gets or Sets OverringCount 
         /// </summary> 
         public int? OverringCount { get; set; } 

         /// <summary> 
         /// Gets or Sets ActualCashOverShort 
         /// </summary> 
         public decimal? ActualCashOverShort { get; set; }  

         /// <summary> 
         /// Gets or sets the Related Employee.  Related to column EmployeeID 
         /// </summary> 
         [ScriptIgnore] 
         public Lazy<Employee> Employee 
         { 
             get 
             { 
                 int value; 
                 bool hasValue = Int32.TryParse(EmployeeID.ToString(), out value); 
 
                 if (hasValue) 
                     return new Lazy<Employee>(() => jmann.BusinessObject.Employee.SelectByPrimaryKey(value)); 
                 else 
                     return null; 
             } 
             set{ } 
         }  
 
         /// <summary> 
         /// Gets or sets the Related Location.  Related to column LocationID 
         /// </summary> 
         [ScriptIgnore] 
         public Lazy<Location> Location 
         { 
             get 
             { 
                 int value; 
                 bool hasValue = Int32.TryParse(LocationID.ToString(), out value); 
 
                 if (hasValue) 
                     return new Lazy<Location>(() => jmann.BusinessObject.Location.SelectByPrimaryKey(value)); 
                 else 
                     return null; 
             } 
             set{ } 
         }  
 
 
         /// <summary> 
         /// Constructor 
         /// </summary> 
         public RegisterRecordBase() 
         { 
         } 
 
         /// <summary>
         /// Selects a record by primary key(s) 
         /// </summary>
         public static RegisterRecord SelectByPrimaryKey(int registerRecordID) 
         { 
             return RegisterRecordDataLayer.SelectByPrimaryKey(registerRecordID); 
         } 
 
         /// <summary> 
         /// Selects all records as a collection (List) of RegisterRecord 
         /// </summary> 
         public static RegisterRecordCollection SelectAll() 
         { 
             return RegisterRecordDataLayer.SelectAll(); 
         } 
 
         /// <summary> 
         /// Selects all records as a collection (List) of RegisterRecord sorted by the sort expression 
         /// </summary> 
         public static RegisterRecordCollection SelectAll(string sortExpression) 
         { 
             RegisterRecordCollection objRegisterRecordCol = RegisterRecordDataLayer.SelectAll();
             return SortByExpression(objRegisterRecordCol, sortExpression);
         } 
 
         /// <summary>
         /// Selects all RegisterRecord by Employee, related to column EmployeeID 
         /// </summary> 
         public static RegisterRecordCollection SelectRegisterRecordCollectionByEmployee(int employeeID) 
         { 
             return RegisterRecordDataLayer.SelectRegisterRecordCollectionByEmployee(employeeID); 
         } 
 
         /// <summary>
         /// Selects all RegisterRecord by Employee, related to column EmployeeID, sorted by the sort expression 
         /// </summary> 
         public static RegisterRecordCollection SelectRegisterRecordCollectionByEmployee(int employeeID, string sortExpression) 
         { 
             RegisterRecordCollection objRegisterRecordCol = RegisterRecordDataLayer.SelectRegisterRecordCollectionByEmployee(employeeID); 
             return SortByExpression(objRegisterRecordCol, sortExpression);
         } 
 
         /// <summary>
         /// Selects all RegisterRecord by Location, related to column LocationID 
         /// </summary> 
         public static RegisterRecordCollection SelectRegisterRecordCollectionByLocation(int locationID) 
         { 
             return RegisterRecordDataLayer.SelectRegisterRecordCollectionByLocation(locationID); 
         } 
 
         /// <summary>
         /// Selects all RegisterRecord by Location, related to column LocationID, sorted by the sort expression 
         /// </summary> 
         public static RegisterRecordCollection SelectRegisterRecordCollectionByLocation(int locationID, string sortExpression) 
         { 
             RegisterRecordCollection objRegisterRecordCol = RegisterRecordDataLayer.SelectRegisterRecordCollectionByLocation(locationID); 
             return SortByExpression(objRegisterRecordCol, sortExpression);
         } 
 
         /// <summary>
         /// Selects RegisterRecordID and Register columns for use with a DropDownList web control, ComboBox, CheckedBoxList, ListView, ListBox, etc 
         /// </summary> 
         public static RegisterRecordCollection SelectRegisterRecordDropDownListData() 
         { 
             return RegisterRecordDataLayer.SelectRegisterRecordDropDownListData(); 
         } 
 
         /// <summary>
         /// Sorts the RegisterRecordCollection by sort expression 
         /// </summary> 
         public static RegisterRecordCollection SortByExpression(RegisterRecordCollection objRegisterRecordCol, string sortExpression) 
         { 
             bool isSortDescending = sortExpression.Contains(" DESC");

             if (isSortDescending)
                 sortExpression = sortExpression.Replace(" DESC", "");

             switch (sortExpression)
             {
                 case "RegisterRecordID":
                     objRegisterRecordCol.Sort(jmann.BusinessObject.RegisterRecord.ByRegisterRecordID);
                     break;
                 case "Register":
                     objRegisterRecordCol.Sort(jmann.BusinessObject.RegisterRecord.ByRegister);
                     break;
                 case "Record":
                     objRegisterRecordCol.Sort(jmann.BusinessObject.RegisterRecord.ByRecord);
                     break;
                 case "EmployeeID":
                     objRegisterRecordCol.Sort(jmann.BusinessObject.RegisterRecord.ByEmployeeID);
                     break;
                 case "LocationID":
                     objRegisterRecordCol.Sort(jmann.BusinessObject.RegisterRecord.ByLocationID);
                     break;
                 case "Date":
                     objRegisterRecordCol.Sort(jmann.BusinessObject.RegisterRecord.ByDate);
                     break;
                 case "CustomerCount":
                     objRegisterRecordCol.Sort(jmann.BusinessObject.RegisterRecord.ByCustomerCount);
                     break;
                 case "Coins":
                     objRegisterRecordCol.Sort(jmann.BusinessObject.RegisterRecord.ByCoins);
                     break;
                 case "Currency":
                     objRegisterRecordCol.Sort(jmann.BusinessObject.RegisterRecord.ByCurrency);
                     break;
                 case "MiscCash":
                     objRegisterRecordCol.Sort(jmann.BusinessObject.RegisterRecord.ByMiscCash);
                     break;
                 case "Visa":
                     objRegisterRecordCol.Sort(jmann.BusinessObject.RegisterRecord.ByVisa);
                     break;
                 case "Mastercard":
                     objRegisterRecordCol.Sort(jmann.BusinessObject.RegisterRecord.ByMastercard);
                     break;
                 case "Discover":
                     objRegisterRecordCol.Sort(jmann.BusinessObject.RegisterRecord.ByDiscover);
                     break;
                 case "Atm":
                     objRegisterRecordCol.Sort(jmann.BusinessObject.RegisterRecord.ByAtm);
                     break;
                 case "GiftCertificate":
                     objRegisterRecordCol.Sort(jmann.BusinessObject.RegisterRecord.ByGiftCertificate);
                     break;
                 case "ZTapeCash":
                     objRegisterRecordCol.Sort(jmann.BusinessObject.RegisterRecord.ByZTapeCash);
                     break;
                 case "ZTapeCharge":
                     objRegisterRecordCol.Sort(jmann.BusinessObject.RegisterRecord.ByZTapeCharge);
                     break;
                 case "ZTapeTotal":
                     objRegisterRecordCol.Sort(jmann.BusinessObject.RegisterRecord.ByZTapeTotal);
                     break;
                 case "Overring":
                     objRegisterRecordCol.Sort(jmann.BusinessObject.RegisterRecord.ByOverring);
                     break;
                 case "OverringCount":
                     objRegisterRecordCol.Sort(jmann.BusinessObject.RegisterRecord.ByOverringCount);
                     break;
                 case "ActualCashOverShort":
                     objRegisterRecordCol.Sort(jmann.BusinessObject.RegisterRecord.ByActualCashOverShort);
                     break;
                 default:
                     break;
             }

             if (isSortDescending) 
                 objRegisterRecordCol.Reverse();

             return objRegisterRecordCol;
         } 
 
         /// <summary>
         /// Inserts a record 
         /// </summary> 
         public int Insert() 
         { 
             RegisterRecord objRegisterRecord = (RegisterRecord)this; 
             return RegisterRecordDataLayer.Insert(objRegisterRecord); 
         } 
 
         /// <summary>
         /// Updates a record 
         /// </summary> 
         public void Update() 
         { 
             RegisterRecord objRegisterRecord = (RegisterRecord)this; 
             RegisterRecordDataLayer.Update(objRegisterRecord); 
         } 
 
         /// <summary>
         /// Deletes a record based on primary key(s) 
         /// </summary>
         public static void Delete(int registerRecordID) 
         { 
             RegisterRecordDataLayer.Delete(registerRecordID); 
         } 
 
         /// <summary> 
         /// Compares RegisterRecordID used for sorting 
         /// </summary> 
         public static Comparison<RegisterRecord> ByRegisterRecordID = delegate(RegisterRecord x, RegisterRecord y) 
         { 
             return x.RegisterRecordID.CompareTo(y.RegisterRecordID); 
         }; 
 
         /// <summary> 
         /// Compares Register used for sorting 
         /// </summary> 
         public static Comparison<RegisterRecord> ByRegister = delegate(RegisterRecord x, RegisterRecord y) 
         { 
             return Nullable.Compare(x.Register, y.Register); 
         }; 
 
         /// <summary> 
         /// Compares Record used for sorting 
         /// </summary> 
         public static Comparison<RegisterRecord> ByRecord = delegate(RegisterRecord x, RegisterRecord y) 
         { 
             return Nullable.Compare(x.Record, y.Record); 
         }; 
 
         /// <summary> 
         /// Compares EmployeeID used for sorting 
         /// </summary> 
         public static Comparison<RegisterRecord> ByEmployeeID = delegate(RegisterRecord x, RegisterRecord y) 
         { 
             return Nullable.Compare(x.EmployeeID, y.EmployeeID); 
         }; 
 
         /// <summary> 
         /// Compares LocationID used for sorting 
         /// </summary> 
         public static Comparison<RegisterRecord> ByLocationID = delegate(RegisterRecord x, RegisterRecord y) 
         { 
             return Nullable.Compare(x.LocationID, y.LocationID); 
         }; 
 
         /// <summary> 
         /// Compares Date used for sorting 
         /// </summary> 
         public static Comparison<RegisterRecord> ByDate = delegate(RegisterRecord x, RegisterRecord y) 
         { 
             return Nullable.Compare(x.Date, y.Date); 
         }; 

         /// <summary> 
         /// Compares CustomerCount used for sorting 
         /// </summary> 
         public static Comparison<RegisterRecord> ByCustomerCount = delegate(RegisterRecord x, RegisterRecord y) 
         { 
             return Nullable.Compare(x.CustomerCount, y.CustomerCount); 
         }; 
 
         /// <summary> 
         /// Compares Coins used for sorting 
         /// </summary> 
         public static Comparison<RegisterRecord> ByCoins = delegate(RegisterRecord x, RegisterRecord y) 
         { 
             return Nullable.Compare(x.Coins, y.Coins); 
         }; 
 
         /// <summary> 
         /// Compares Currency used for sorting 
         /// </summary> 
         public static Comparison<RegisterRecord> ByCurrency = delegate(RegisterRecord x, RegisterRecord y) 
         { 
             return Nullable.Compare(x.Currency, y.Currency); 
         }; 
 
         /// <summary> 
         /// Compares MiscCash used for sorting 
         /// </summary> 
         public static Comparison<RegisterRecord> ByMiscCash = delegate(RegisterRecord x, RegisterRecord y) 
         { 
             return Nullable.Compare(x.MiscCash, y.MiscCash); 
         }; 
 
         /// <summary> 
         /// Compares Visa used for sorting 
         /// </summary> 
         public static Comparison<RegisterRecord> ByVisa = delegate(RegisterRecord x, RegisterRecord y) 
         { 
             return Nullable.Compare(x.Visa, y.Visa); 
         }; 
 
         /// <summary> 
         /// Compares Mastercard used for sorting 
         /// </summary> 
         public static Comparison<RegisterRecord> ByMastercard = delegate(RegisterRecord x, RegisterRecord y) 
         { 
             return Nullable.Compare(x.Mastercard, y.Mastercard); 
         }; 
 
         /// <summary> 
         /// Compares Discover used for sorting 
         /// </summary> 
         public static Comparison<RegisterRecord> ByDiscover = delegate(RegisterRecord x, RegisterRecord y) 
         { 
             return Nullable.Compare(x.Discover, y.Discover); 
         }; 
 
         /// <summary> 
         /// Compares Atm used for sorting 
         /// </summary> 
         public static Comparison<RegisterRecord> ByAtm = delegate(RegisterRecord x, RegisterRecord y) 
         { 
             return Nullable.Compare(x.Atm, y.Atm); 
         }; 
 
         /// <summary> 
         /// Compares GiftCertificate used for sorting 
         /// </summary> 
         public static Comparison<RegisterRecord> ByGiftCertificate = delegate(RegisterRecord x, RegisterRecord y) 
         { 
             return Nullable.Compare(x.GiftCertificate, y.GiftCertificate); 
         }; 

         /// <summary> 
         /// Compares ZTapeCash used for sorting 
         /// </summary> 
         public static Comparison<RegisterRecord> ByZTapeCash = delegate(RegisterRecord x, RegisterRecord y) 
         { 
             return Nullable.Compare(x.ZTapeCash, y.ZTapeCash); 
         }; 
         
         /// <summary> 
         /// Compares ZTapeCharge used for sorting 
         /// </summary> 
         public static Comparison<RegisterRecord> ByZTapeCharge = delegate(RegisterRecord x, RegisterRecord y) 
         { 
             return Nullable.Compare(x.ZTapeCharge, y.ZTapeCharge); 
         }; 
                   
         /// <summary> 
         /// Compares ZTapeTotal used for sorting 
         /// </summary> 
         public static Comparison<RegisterRecord> ByZTapeTotal = delegate(RegisterRecord x, RegisterRecord y) 
         { 
             return Nullable.Compare(x.ZTapeTotal, y.ZTapeTotal); 
         }; 
 
         /// <summary> 
         /// Compares Overring used for sorting 
         /// </summary> 
         public static Comparison<RegisterRecord> ByOverring = delegate(RegisterRecord x, RegisterRecord y) 
         { 
             return Nullable.Compare(x.Overring, y.Overring); 
         }; 
 
         /// <summary> 
         /// Compares OverringCount used for sorting 
         /// </summary> 
         public static Comparison<RegisterRecord> ByOverringCount = delegate(RegisterRecord x, RegisterRecord y) 
         { 
             return Nullable.Compare(x.OverringCount, y.OverringCount); 
         }; 
 
         /// <summary> 
         /// Compares ActualCashOverShort used for sorting 
         /// </summary> 
         public static Comparison<RegisterRecord> ByActualCashOverShort = delegate(RegisterRecord x, RegisterRecord y) 
         { 
             return Nullable.Compare(x.ActualCashOverShort, y.ActualCashOverShort); 
         };  
     } 
} 


#line default
#line hidden
