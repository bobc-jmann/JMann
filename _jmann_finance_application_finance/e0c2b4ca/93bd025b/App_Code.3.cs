#pragma checksum "D:\JMann\Finance\Application\Finance\App_Code\Example\RegisterRecordExample.cs" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "6D307A506D07CDC99266CF0BD0E37262512D1E38"

#line 1 "D:\JMann\Finance\Application\Finance\App_Code\Example\RegisterRecordExample.cs"
using System; 
using jmann.BusinessObject; 
using System.Web.UI.WebControls; 
// using System.Windows.Forms;    // Note: remove comment when using with windows forms 
 
/// <summary> 
/// These are data-centric code examples for the RegisterRecord table. 
/// You can cut and paste the respective codes into your application 
/// by changing the sample values assigned from these examples. 
/// NOTE: This class contains private methods because they're 
/// not meant to be called by an outside client.  Each method contains 
/// code for the respective example being shown 
/// </summary> 
public sealed class RegisterRecordExample 
{ 
    private RegisterRecordExample() 
    { 
    } 
 
    private void SelectAll() 
    { 
        // select all records 
        RegisterRecordCollection objRegisterRecordCol = RegisterRecord.SelectAll(); 
 
        // Example 1:  you can optionally sort the collection in ascending order by your chosen field  
        objRegisterRecordCol.Sort(RegisterRecord.ByRegister); 
 
        // Example 2:  to sort in descending order, add this line to the Sort code in Example 1  
        objRegisterRecordCol.Reverse(); 
 
        // Example 3:  directly bind to a GridView 
        GridView grid = new GridView(); 
        grid.DataSource = objRegisterRecordCol; 
        grid.DataBind(); 
 
        // Example 4:  loop through all the RegisterRecord(s) 
        foreach (RegisterRecord objRegisterRecord in objRegisterRecordCol) 
        { 
            int registerRecordID = objRegisterRecord.RegisterRecordID; 
            int? register = objRegisterRecord.Register; 
            int? record = objRegisterRecord.Record; 
            int? employeeID = objRegisterRecord.EmployeeID; 
            int? locationID = objRegisterRecord.LocationID; 
            DateTime? date = objRegisterRecord.Date; 
            int? customerCount = objRegisterRecord.CustomerCount; 
            decimal? coins = objRegisterRecord.Coins; 
            decimal? currency = objRegisterRecord.Currency; 
            decimal? miscCash = objRegisterRecord.MiscCash; 
            decimal? visa = objRegisterRecord.Visa; 
            decimal? mastercard = objRegisterRecord.Mastercard; 
            decimal? discover = objRegisterRecord.Discover; 
            decimal? atm = objRegisterRecord.Atm; 
            decimal? giftCertificate = objRegisterRecord.GiftCertificate; 
            decimal? zTape = objRegisterRecord.ZTapeTotal; 
            decimal? overring = objRegisterRecord.Overring; 
            int? overringCount = objRegisterRecord.OverringCount; 
 
            // optionally get the Employee related to EmployeeID. 
            // Note this is lazily loaded which means there's no value until you ask for it 
            if (objRegisterRecord.EmployeeID != null) 
            { 
                Employee objEmployeeRelatedToEmployeeID; 
 
                if (objRegisterRecord.Employee.IsValueCreated) 
                    objEmployeeRelatedToEmployeeID = objRegisterRecord.Employee.Value; 
            } 
 
            // optionally get the Location related to LocationID. 
            // Note this is lazily loaded which means there's no value until you ask for it 
            if (objRegisterRecord.LocationID != null) 
            { 
                Location objLocationRelatedToLocationID; 
 
                if (objRegisterRecord.Location.IsValueCreated) 
                    objLocationRelatedToLocationID = objRegisterRecord.Location.Value; 
            } 
        } 
    } 
 
    private void SelectByPrimaryKey() 
    { 
        // select a record by primary key(s) 
        RegisterRecord objRegisterRecord = RegisterRecord.SelectByPrimaryKey(1); 
 
        if (objRegisterRecord != null) 
        { 
            // if record is found, a record is returned 
            int registerRecordID = objRegisterRecord.RegisterRecordID; 
            int? register = objRegisterRecord.Register; 
            int? record = objRegisterRecord.Record; 
            int? employeeID = objRegisterRecord.EmployeeID; 
            int? locationID = objRegisterRecord.LocationID; 
            DateTime? date = objRegisterRecord.Date; 
            int? customerCount = objRegisterRecord.CustomerCount; 
            decimal? coins = objRegisterRecord.Coins; 
            decimal? currency = objRegisterRecord.Currency; 
            decimal? miscCash = objRegisterRecord.MiscCash; 
            decimal? visa = objRegisterRecord.Visa; 
            decimal? mastercard = objRegisterRecord.Mastercard; 
            decimal? discover = objRegisterRecord.Discover; 
            decimal? atm = objRegisterRecord.Atm; 
            decimal? giftCertificate = objRegisterRecord.GiftCertificate; 
            decimal? zTape = objRegisterRecord.ZTapeTotal; 
            decimal? overring = objRegisterRecord.Overring; 
            int? overringCount = objRegisterRecord.OverringCount; 
 
            // optionally get the Employee related to EmployeeID. 
            // Note this is lazily loaded which means there's no value until you ask for it 
            if (objRegisterRecord.EmployeeID != null) 
            { 
                Employee objEmployeeRelatedToEmployeeID; 
 
                if (objRegisterRecord.Employee.IsValueCreated) 
                    objEmployeeRelatedToEmployeeID = objRegisterRecord.Employee.Value; 
            } 
 
            // optionally get the Location related to LocationID. 
            // Note this is lazily loaded which means there's no value until you ask for it 
            if (objRegisterRecord.LocationID != null) 
            { 
                Location objLocationRelatedToLocationID; 
 
                if (objRegisterRecord.Location.IsValueCreated) 
                    objLocationRelatedToLocationID = objRegisterRecord.Location.Value; 
            } 
        } 
    } 
 
    /// <summary> 
    /// Select all records by Employee, related to column EmployeeID 
    /// </summary>  
    private void SelectRegisterRecordCollectionByEmployee() 
    { 
        RegisterRecordCollection objRegisterRecordCol = RegisterRecord.SelectRegisterRecordCollectionByEmployee(1); 
 
        // Example 1:  you can optionally sort the collection in ascending order by your chosen field  
        objRegisterRecordCol.Sort(RegisterRecord.ByRegister); 
 
        // Example 2:  to sort in descending order, add this line to the Sort code in Example 1  
        objRegisterRecordCol.Reverse(); 
 
        // Example 3:  directly bind to a GridView 
        GridView grid = new GridView(); 
        grid.DataSource = objRegisterRecordCol; 
        grid.DataBind(); 
 
        // Example 4:  loop through all the RegisterRecord(s) 
        foreach (RegisterRecord objRegisterRecord in objRegisterRecordCol) 
        { 
            int registerRecordID = objRegisterRecord.RegisterRecordID; 
            int? register = objRegisterRecord.Register; 
            int? record = objRegisterRecord.Record; 
            int? employeeID = objRegisterRecord.EmployeeID; 
            int? locationID = objRegisterRecord.LocationID; 
            DateTime? date = objRegisterRecord.Date; 
            int? customerCount = objRegisterRecord.CustomerCount; 
            decimal? coins = objRegisterRecord.Coins; 
            decimal? currency = objRegisterRecord.Currency; 
            decimal? miscCash = objRegisterRecord.MiscCash; 
            decimal? visa = objRegisterRecord.Visa; 
            decimal? mastercard = objRegisterRecord.Mastercard; 
            decimal? discover = objRegisterRecord.Discover; 
            decimal? atm = objRegisterRecord.Atm; 
            decimal? giftCertificate = objRegisterRecord.GiftCertificate; 
            decimal? zTape = objRegisterRecord.ZTapeTotal; 
            decimal? overring = objRegisterRecord.Overring; 
            int? overringCount = objRegisterRecord.OverringCount; 
 
            // optionally get the Employee related to EmployeeID. 
            // Note this is lazily loaded which means there's no value until you ask for it 
            if (objRegisterRecord.EmployeeID != null) 
            { 
                Employee objEmployeeRelatedToEmployeeID; 
 
                if (objRegisterRecord.Employee.IsValueCreated) 
                    objEmployeeRelatedToEmployeeID = objRegisterRecord.Employee.Value; 
            } 
 
            // optionally get the Location related to LocationID. 
            // Note this is lazily loaded which means there's no value until you ask for it 
            if (objRegisterRecord.LocationID != null) 
            { 
                Location objLocationRelatedToLocationID; 
 
                if (objRegisterRecord.Location.IsValueCreated) 
                    objLocationRelatedToLocationID = objRegisterRecord.Location.Value; 
            } 
        } 
    } 
 
    /// <summary> 
    /// Select all records by Location, related to column LocationID 
    /// </summary>  
    private void SelectRegisterRecordCollectionByLocation() 
    { 
        RegisterRecordCollection objRegisterRecordCol = RegisterRecord.SelectRegisterRecordCollectionByLocation(4); 
 
        // Example 1:  you can optionally sort the collection in ascending order by your chosen field  
        objRegisterRecordCol.Sort(RegisterRecord.ByRegister); 
 
        // Example 2:  to sort in descending order, add this line to the Sort code in Example 1  
        objRegisterRecordCol.Reverse(); 
 
        // Example 3:  directly bind to a GridView 
        GridView grid = new GridView(); 
        grid.DataSource = objRegisterRecordCol; 
        grid.DataBind(); 
 
        // Example 4:  loop through all the RegisterRecord(s) 
        foreach (RegisterRecord objRegisterRecord in objRegisterRecordCol) 
        { 
            int registerRecordID = objRegisterRecord.RegisterRecordID; 
            int? register = objRegisterRecord.Register; 
            int? record = objRegisterRecord.Record; 
            int? employeeID = objRegisterRecord.EmployeeID; 
            int? locationID = objRegisterRecord.LocationID; 
            DateTime? date = objRegisterRecord.Date; 
            int? customerCount = objRegisterRecord.CustomerCount; 
            decimal? coins = objRegisterRecord.Coins; 
            decimal? currency = objRegisterRecord.Currency; 
            decimal? miscCash = objRegisterRecord.MiscCash; 
            decimal? visa = objRegisterRecord.Visa; 
            decimal? mastercard = objRegisterRecord.Mastercard; 
            decimal? discover = objRegisterRecord.Discover; 
            decimal? atm = objRegisterRecord.Atm; 
            decimal? giftCertificate = objRegisterRecord.GiftCertificate; 
            decimal? zTape = objRegisterRecord.ZTapeTotal; 
            decimal? overring = objRegisterRecord.Overring; 
            int? overringCount = objRegisterRecord.OverringCount; 
 
            // optionally get the Employee related to EmployeeID. 
            // Note this is lazily loaded which means there's no value until you ask for it 
            if (objRegisterRecord.EmployeeID != null) 
            { 
                Employee objEmployeeRelatedToEmployeeID; 
 
                if (objRegisterRecord.Employee.IsValueCreated) 
                    objEmployeeRelatedToEmployeeID = objRegisterRecord.Employee.Value; 
            } 
 
            // optionally get the Location related to LocationID. 
            // Note this is lazily loaded which means there's no value until you ask for it 
            if (objRegisterRecord.LocationID != null) 
            { 
                Location objLocationRelatedToLocationID; 
 
                if (objRegisterRecord.Location.IsValueCreated) 
                    objLocationRelatedToLocationID = objRegisterRecord.Location.Value; 
            } 
        } 
    } 
 
    /// <summary> 
    /// Selects RegisterRecordID and Register columns for use with a with a Drop Down List, Combo Box, Checked Box List, List View, List Box, etc 
    /// </summary> 
    private void SelectRegisterRecordDropDownListData() 
    { 
        RegisterRecordCollection objRegisterRecordCol = RegisterRecord.SelectRegisterRecordDropDownListData(); 
 
        // Example 1:  directly bind to a drop down list 
        DropDownList ddl1 = new DropDownList(); 
        ddl1.DataValueField = "RegisterRecordID"; 
        ddl1.DataTextField = "Register"; 
        ddl1.DataSource = objRegisterRecordCol; 
        ddl1.DataBind(); 
 
        // Example 2:  add each item through a loop 
        DropDownList ddl2 = new DropDownList(); 
 
        foreach (RegisterRecord objRegisterRecord in objRegisterRecordCol) 
        { 
            ddl2.Items.Add(new ListItem(objRegisterRecord.Register.ToString(), objRegisterRecord.RegisterRecordID.ToString())); 
        } 
 
        // Example 3:  bind to a combo box.  Note: remove comment when using with windows forms 
        // ComboBox cbx1 = new ComboBox(); 
 
        // foreach (RegisterRecord objRegisterRecord in objRegisterRecordCol) 
        // { 
        //     cbx1.Items.Add(new ListItem(objRegisterRecord.Register.ToString(), objRegisterRecord.RegisterRecordID.ToString())); 
        // } 
    } 
 
    private void Insert() 
    { 
        // first instantiate a new RegisterRecord 
        RegisterRecord objRegisterRecord = new RegisterRecord(); 
 
        // assign values you want inserted 
        objRegisterRecord.Register = 1; 
        objRegisterRecord.Record = 1; 
        objRegisterRecord.EmployeeID = 1; 
        objRegisterRecord.LocationID = 4; 
        objRegisterRecord.Date = Convert.ToDateTime("2003-09-09 00:00:00"); 
        objRegisterRecord.CustomerCount = 109; 
        objRegisterRecord.Coins = Convert.ToDecimal(0.3600); 
        objRegisterRecord.Currency = Convert.ToDecimal(889.0000); 
        objRegisterRecord.MiscCash = 52.4m; 
        objRegisterRecord.Visa = Convert.ToDecimal(331.6000); 
        objRegisterRecord.Mastercard = Convert.ToDecimal(252.0600); 
        objRegisterRecord.Discover = Convert.ToDecimal(46.4000); 
        objRegisterRecord.Atm = Convert.ToDecimal(258.6100); 
        objRegisterRecord.GiftCertificate = Convert.ToDecimal(10.0000); 
        objRegisterRecord.ZTapeTotal = 52.4m; 
        objRegisterRecord.Overring = 52.4m; 
        objRegisterRecord.OverringCount = 12; 
 
        // finally, insert a new record 
        // the insert method returns the newly created primary key 
        int newlyCreatedPrimaryKey = objRegisterRecord.Insert(); 
    } 
 
    private void Update() 
    { 
        // first instantiate a new RegisterRecord 
        RegisterRecord objRegisterRecord = new RegisterRecord(); 
 
        // assign the existing primary key(s) 
        // of the record you want updated 
        objRegisterRecord.RegisterRecordID = 1; 
 
        // assign values you want updated 
        objRegisterRecord.Register = 1; 
        objRegisterRecord.Record = 1; 
        objRegisterRecord.EmployeeID = 1; 
        objRegisterRecord.LocationID = 4; 
        objRegisterRecord.Date = Convert.ToDateTime("2003-09-09 00:00:00"); 
        objRegisterRecord.CustomerCount = 109; 
        objRegisterRecord.Coins = Convert.ToDecimal(0.3600); 
        objRegisterRecord.Currency = Convert.ToDecimal(889.0000); 
        objRegisterRecord.MiscCash = 52.4m; 
        objRegisterRecord.Visa = Convert.ToDecimal(331.6000); 
        objRegisterRecord.Mastercard = Convert.ToDecimal(252.0600); 
        objRegisterRecord.Discover = Convert.ToDecimal(46.4000); 
        objRegisterRecord.Atm = Convert.ToDecimal(258.6100); 
        objRegisterRecord.GiftCertificate = Convert.ToDecimal(10.0000); 
        objRegisterRecord.ZTapeTotal = 52.4m; 
        objRegisterRecord.Overring = 52.4m; 
        objRegisterRecord.OverringCount = 12; 
 
        // finally, update an existing record 
        objRegisterRecord.Update(); 
    } 
 
    private void Delete() 
    { 
        // delete a record by primary key 
        RegisterRecord.Delete(187380); 
    } 
} 


#line default
#line hidden
