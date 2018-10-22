#pragma checksum "D:\JMann\Finance\Application\Finance\App_Code\Example\CreditCardRecordExample.cs" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "5F4F8C030A922B2916D4E96F74F5703543ECF848"

#line 1 "D:\JMann\Finance\Application\Finance\App_Code\Example\CreditCardRecordExample.cs"
using System; 
using jmann.BusinessObject; 
using System.Web.UI.WebControls; 
// using System.Windows.Forms;    // Note: remove comment when using with windows forms 
 
/// <summary> 
/// These are data-centric code examples for the CreditCardRecord table. 
/// You can cut and paste the respective codes into your application 
/// by changing the sample values assigned from these examples. 
/// NOTE: This class contains private methods because they're 
/// not meant to be called by an outside client.  Each method contains 
/// code for the respective example being shown 
/// </summary> 
public sealed class CreditCardRecordExample 
{ 
    private CreditCardRecordExample() 
    { 
    } 
 
    private void SelectAll() 
    { 
        // select all records 
        CreditCardRecordCollection objCreditCardRecordCol = CreditCardRecord.SelectAll(); 
 
        // Example 1:  you can optionally sort the collection in ascending order by your chosen field  
        objCreditCardRecordCol.Sort(CreditCardRecord.ByDate); 
 
        // Example 2:  to sort in descending order, add this line to the Sort code in Example 1  
        objCreditCardRecordCol.Reverse(); 
 
        // Example 3:  directly bind to a GridView 
        GridView grid = new GridView(); 
        grid.DataSource = objCreditCardRecordCol; 
        grid.DataBind(); 
 
        // Example 4:  loop through all the CreditCardRecord(s) 
        foreach (CreditCardRecord objCreditCardRecord in objCreditCardRecordCol) 
        { 
            int creditCardRecordID = objCreditCardRecord.CreditCardRecordID; 
            DateTime? date = objCreditCardRecord.Date; 
            int? locationID = objCreditCardRecord.LocationID; 
            int? atmCount = objCreditCardRecord.AtmCount; 
            decimal? atmTotal = objCreditCardRecord.AtmTotal; 
            int? visaCount = objCreditCardRecord.VisaCount; 
            decimal? visaTotal = objCreditCardRecord.VisaTotal; 
            int? mastercardCount = objCreditCardRecord.MastercardCount; 
            decimal? mastercardTotal = objCreditCardRecord.MastercardTotal; 
            int? discoverCount = objCreditCardRecord.DiscoverCount; 
            decimal? discoverTotal = objCreditCardRecord.DiscoverTotal; 
 
            // optionally get the Location related to LocationID. 
            // Note this is lazily loaded which means there's no value until you ask for it 
            if (objCreditCardRecord.LocationID != null) 
            { 
                Location objLocationRelatedToLocationID; 
 
                if (objCreditCardRecord.Location.IsValueCreated) 
                    objLocationRelatedToLocationID = objCreditCardRecord.Location.Value; 
            } 
        } 
    } 
 
    private void SelectByPrimaryKey() 
    { 
        // select a record by primary key(s) 
        CreditCardRecord objCreditCardRecord = CreditCardRecord.SelectByPrimaryKey(5); 
 
        if (objCreditCardRecord != null) 
        { 
            // if record is found, a record is returned 
            int creditCardRecordID = objCreditCardRecord.CreditCardRecordID; 
            DateTime? date = objCreditCardRecord.Date; 
            int? locationID = objCreditCardRecord.LocationID; 
            int? atmCount = objCreditCardRecord.AtmCount; 
            decimal? atmTotal = objCreditCardRecord.AtmTotal; 
            int? visaCount = objCreditCardRecord.VisaCount; 
            decimal? visaTotal = objCreditCardRecord.VisaTotal; 
            int? mastercardCount = objCreditCardRecord.MastercardCount; 
            decimal? mastercardTotal = objCreditCardRecord.MastercardTotal; 
            int? discoverCount = objCreditCardRecord.DiscoverCount; 
            decimal? discoverTotal = objCreditCardRecord.DiscoverTotal; 
 
            // optionally get the Location related to LocationID. 
            // Note this is lazily loaded which means there's no value until you ask for it 
            if (objCreditCardRecord.LocationID != null) 
            { 
                Location objLocationRelatedToLocationID; 
 
                if (objCreditCardRecord.Location.IsValueCreated) 
                    objLocationRelatedToLocationID = objCreditCardRecord.Location.Value; 
            } 
        } 
    } 
 
    /// <summary> 
    /// Select all records by Location, related to column LocationID 
    /// </summary>  
    private void SelectCreditCardRecordCollectionByLocation() 
    { 
        CreditCardRecordCollection objCreditCardRecordCol = CreditCardRecord.SelectCreditCardRecordCollectionByLocation(8); 
 
        // Example 1:  you can optionally sort the collection in ascending order by your chosen field  
        objCreditCardRecordCol.Sort(CreditCardRecord.ByDate); 
 
        // Example 2:  to sort in descending order, add this line to the Sort code in Example 1  
        objCreditCardRecordCol.Reverse(); 
 
        // Example 3:  directly bind to a GridView 
        GridView grid = new GridView(); 
        grid.DataSource = objCreditCardRecordCol; 
        grid.DataBind(); 
 
        // Example 4:  loop through all the CreditCardRecord(s) 
        foreach (CreditCardRecord objCreditCardRecord in objCreditCardRecordCol) 
        { 
            int creditCardRecordID = objCreditCardRecord.CreditCardRecordID; 
            DateTime? date = objCreditCardRecord.Date; 
            int? locationID = objCreditCardRecord.LocationID; 
            int? atmCount = objCreditCardRecord.AtmCount; 
            decimal? atmTotal = objCreditCardRecord.AtmTotal; 
            int? visaCount = objCreditCardRecord.VisaCount; 
            decimal? visaTotal = objCreditCardRecord.VisaTotal; 
            int? mastercardCount = objCreditCardRecord.MastercardCount; 
            decimal? mastercardTotal = objCreditCardRecord.MastercardTotal; 
            int? discoverCount = objCreditCardRecord.DiscoverCount; 
            decimal? discoverTotal = objCreditCardRecord.DiscoverTotal; 
 
            // optionally get the Location related to LocationID. 
            // Note this is lazily loaded which means there's no value until you ask for it 
            if (objCreditCardRecord.LocationID != null) 
            { 
                Location objLocationRelatedToLocationID; 
 
                if (objCreditCardRecord.Location.IsValueCreated) 
                    objLocationRelatedToLocationID = objCreditCardRecord.Location.Value; 
            } 
        } 
    } 
 
    /// <summary> 
    /// Selects CreditCardRecordID and Date columns for use with a with a Drop Down List, Combo Box, Checked Box List, List View, List Box, etc 
    /// </summary> 
    private void SelectCreditCardRecordDropDownListData() 
    { 
        CreditCardRecordCollection objCreditCardRecordCol = CreditCardRecord.SelectCreditCardRecordDropDownListData(); 
 
        // Example 1:  directly bind to a drop down list 
        DropDownList ddl1 = new DropDownList(); 
        ddl1.DataValueField = "CreditCardRecordID"; 
        ddl1.DataTextField = "Date"; 
        ddl1.DataSource = objCreditCardRecordCol; 
        ddl1.DataBind(); 
 
        // Example 2:  add each item through a loop 
        DropDownList ddl2 = new DropDownList(); 
 
        foreach (CreditCardRecord objCreditCardRecord in objCreditCardRecordCol) 
        { 
            ddl2.Items.Add(new ListItem(objCreditCardRecord.Date.ToString(), objCreditCardRecord.CreditCardRecordID.ToString())); 
        } 
 
        // Example 3:  bind to a combo box.  Note: remove comment when using with windows forms 
        // ComboBox cbx1 = new ComboBox(); 
 
        // foreach (CreditCardRecord objCreditCardRecord in objCreditCardRecordCol) 
        // { 
        //     cbx1.Items.Add(new ListItem(objCreditCardRecord.Date.ToString(), objCreditCardRecord.CreditCardRecordID.ToString())); 
        // } 
    } 
 
    private void Insert() 
    { 
        // first instantiate a new CreditCardRecord 
        CreditCardRecord objCreditCardRecord = new CreditCardRecord(); 
 
        // assign values you want inserted 
        objCreditCardRecord.Date = DateTime.Now; 
        objCreditCardRecord.LocationID = 8; 
        objCreditCardRecord.AtmCount = 4; 
        objCreditCardRecord.AtmTotal = Convert.ToDecimal(174.7200); 
        objCreditCardRecord.VisaCount = 8; 
        objCreditCardRecord.VisaTotal = Convert.ToDecimal(232.5100); 
        objCreditCardRecord.MastercardCount = 4; 
        objCreditCardRecord.MastercardTotal = Convert.ToDecimal(147.8100); 
        objCreditCardRecord.DiscoverCount = 2; 
        objCreditCardRecord.DiscoverTotal = Convert.ToDecimal(177.2800); 
 
        // finally, insert a new record 
        // the insert method returns the newly created primary key 
        int newlyCreatedPrimaryKey = objCreditCardRecord.Insert(); 
    } 
 
    private void Update() 
    { 
        // first instantiate a new CreditCardRecord 
        CreditCardRecord objCreditCardRecord = new CreditCardRecord(); 
 
        // assign the existing primary key(s) 
        // of the record you want updated 
        objCreditCardRecord.CreditCardRecordID = 5; 
 
        // assign values you want updated 
        objCreditCardRecord.Date = DateTime.Now; 
        objCreditCardRecord.LocationID = 8; 
        objCreditCardRecord.AtmCount = 4; 
        objCreditCardRecord.AtmTotal = Convert.ToDecimal(174.7200); 
        objCreditCardRecord.VisaCount = 8; 
        objCreditCardRecord.VisaTotal = Convert.ToDecimal(232.5100); 
        objCreditCardRecord.MastercardCount = 4; 
        objCreditCardRecord.MastercardTotal = Convert.ToDecimal(147.8100); 
        objCreditCardRecord.DiscoverCount = 2; 
        objCreditCardRecord.DiscoverTotal = Convert.ToDecimal(177.2800); 
 
        // finally, update an existing record 
        objCreditCardRecord.Update(); 
    } 
 
    private void Delete() 
    { 
        // delete a record by primary key 
        CreditCardRecord.Delete(123782); 
    } 
} 


#line default
#line hidden
