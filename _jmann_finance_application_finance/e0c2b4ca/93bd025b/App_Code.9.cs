#pragma checksum "D:\JMann\Finance\Application\Finance\App_Code\Example\CartsExample.cs" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "DAF94E207290A10E88876675C1AE907222B4F159"

#line 1 "D:\JMann\Finance\Application\Finance\App_Code\Example\CartsExample.cs"
using System; 
using jmann.BusinessObject; 
using System.Web.UI.WebControls; 
// using System.Windows.Forms;    // Note: remove comment when using with windows forms 
 
/// <summary> 
/// These are data-centric code examples for the Carts table. 
/// You can cut and paste the respective codes into your application 
/// by changing the sample values assigned from these examples. 
/// NOTE: This class contains private methods because they're 
/// not meant to be called by an outside client.  Each method contains 
/// code for the respective example being shown 
/// </summary> 
public sealed class CartsExample 
{ 
    private CartsExample() 
    { 
    } 
 
    private void SelectAll() 
    { 
        // select all records 
        CartsCollection objCartsCol = Carts.SelectAll(); 
 
        // Example 1:  you can optionally sort the collection in ascending order by your chosen field  
        objCartsCol.Sort(Carts.ByDate); 
 
        // Example 2:  to sort in descending order, add this line to the Sort code in Example 1  
        objCartsCol.Reverse(); 
 
        // Example 3:  directly bind to a GridView 
        GridView grid = new GridView(); 
        grid.DataSource = objCartsCol; 
        grid.DataBind(); 
 
        // Example 4:  loop through all the Carts(s) 
        foreach (Carts objCarts in objCartsCol) 
        { 
            int cartsID = objCarts.CartsID; 
            DateTime? date = objCarts.Date; 
            int? locationID = objCarts.LocationID; 
            double? cartsWorkedHard = objCarts.CartsWorkedHard; 
            double? cartsWorkedSoft = objCarts.CartsWorkedSoft; 
            int? hangTotal = objCarts.HangTotal; 
            int? thrownCount = objCarts.ThrownCount; 
            int? thrownLbs = objCarts.ThrownLbs; 
            int? raggedLbs = objCarts.RaggedLbs; 
 
            // optionally get the Location related to LocationID. 
            // Note this is lazily loaded which means there's no value until you ask for it 
            if (objCarts.LocationID != null) 
            { 
                Location objLocationRelatedToLocationID; 
 
                if (objCarts.Location.IsValueCreated) 
                    objLocationRelatedToLocationID = objCarts.Location.Value; 
            } 
        } 
    } 
 
    private void SelectByPrimaryKey() 
    { 
        // select a record by primary key(s) 
        Carts objCarts = Carts.SelectByPrimaryKey(27); 
 
        if (objCarts != null) 
        { 
            // if record is found, a record is returned 
            int cartsID = objCarts.CartsID; 
            DateTime? date = objCarts.Date; 
            int? locationID = objCarts.LocationID; 
            double? cartsWorkedHard = objCarts.CartsWorkedHard; 
            double? cartsWorkedSoft = objCarts.CartsWorkedSoft; 
            int? hangTotal = objCarts.HangTotal; 
            int? thrownCount = objCarts.ThrownCount; 
            int? thrownLbs = objCarts.ThrownLbs; 
            int? raggedLbs = objCarts.RaggedLbs; 
 
            // optionally get the Location related to LocationID. 
            // Note this is lazily loaded which means there's no value until you ask for it 
            if (objCarts.LocationID != null) 
            { 
                Location objLocationRelatedToLocationID; 
 
                if (objCarts.Location.IsValueCreated) 
                    objLocationRelatedToLocationID = objCarts.Location.Value; 
            } 
        } 
    } 
 
    /// <summary> 
    /// Select all records by Location, related to column LocationID 
    /// </summary>  
    private void SelectCartsCollectionByLocation() 
    { 
        CartsCollection objCartsCol = Carts.SelectCartsCollectionByLocation(3); 
 
        // Example 1:  you can optionally sort the collection in ascending order by your chosen field  
        objCartsCol.Sort(Carts.ByDate); 
 
        // Example 2:  to sort in descending order, add this line to the Sort code in Example 1  
        objCartsCol.Reverse(); 
 
        // Example 3:  directly bind to a GridView 
        GridView grid = new GridView(); 
        grid.DataSource = objCartsCol; 
        grid.DataBind(); 
 
        // Example 4:  loop through all the Carts(s) 
        foreach (Carts objCarts in objCartsCol) 
        { 
            int cartsID = objCarts.CartsID; 
            DateTime? date = objCarts.Date; 
            int? locationID = objCarts.LocationID; 
            double? cartsWorkedHard = objCarts.CartsWorkedHard; 
            double? cartsWorkedSoft = objCarts.CartsWorkedSoft; 
            int? hangTotal = objCarts.HangTotal; 
            int? thrownCount = objCarts.ThrownCount; 
            int? thrownLbs = objCarts.ThrownLbs; 
            int? raggedLbs = objCarts.RaggedLbs; 
 
            // optionally get the Location related to LocationID. 
            // Note this is lazily loaded which means there's no value until you ask for it 
            if (objCarts.LocationID != null) 
            { 
                Location objLocationRelatedToLocationID; 
 
                if (objCarts.Location.IsValueCreated) 
                    objLocationRelatedToLocationID = objCarts.Location.Value; 
            } 
        } 
    } 
 
    /// <summary> 
    /// Selects CartsID and Date columns for use with a with a Drop Down List, Combo Box, Checked Box List, List View, List Box, etc 
    /// </summary> 
    private void SelectCartsDropDownListData() 
    { 
        CartsCollection objCartsCol = Carts.SelectCartsDropDownListData(); 
 
        // Example 1:  directly bind to a drop down list 
        DropDownList ddl1 = new DropDownList(); 
        ddl1.DataValueField = "CartsID"; 
        ddl1.DataTextField = "Date"; 
        ddl1.DataSource = objCartsCol; 
        ddl1.DataBind(); 
 
        // Example 2:  add each item through a loop 
        DropDownList ddl2 = new DropDownList(); 
 
        foreach (Carts objCarts in objCartsCol) 
        { 
            ddl2.Items.Add(new ListItem(objCarts.Date.ToString(), objCarts.CartsID.ToString())); 
        } 
 
        // Example 3:  bind to a combo box.  Note: remove comment when using with windows forms 
        // ComboBox cbx1 = new ComboBox(); 
 
        // foreach (Carts objCarts in objCartsCol) 
        // { 
        //     cbx1.Items.Add(new ListItem(objCarts.Date.ToString(), objCarts.CartsID.ToString())); 
        // } 
    } 
 
    private void Insert() 
    { 
        // first instantiate a new Carts 
        Carts objCarts = new Carts(); 
 
        // assign values you want inserted 
        objCarts.Date = DateTime.Now; 
        objCarts.LocationID = 3; 
        objCarts.CartsWorkedHard = 14; 
        objCarts.CartsWorkedSoft = 9.4; 
        objCarts.HangTotal = 2125; 
        objCarts.ThrownCount = 5171; 
        objCarts.ThrownLbs = 4137; 
        objCarts.RaggedLbs = 388; 
 
        // finally, insert a new record 
        // the insert method returns the newly created primary key 
        int newlyCreatedPrimaryKey = objCarts.Insert(); 
    } 
 
    private void Update() 
    { 
        // first instantiate a new Carts 
        Carts objCarts = new Carts(); 
 
        // assign the existing primary key(s) 
        // of the record you want updated 
        objCarts.CartsID = 27; 
 
        // assign values you want updated 
        objCarts.Date = DateTime.Now; 
        objCarts.LocationID = 3; 
        objCarts.CartsWorkedHard = 14; 
        objCarts.CartsWorkedSoft = 9.4; 
        objCarts.HangTotal = 2125; 
        objCarts.ThrownCount = 5171; 
        objCarts.ThrownLbs = 4137; 
        objCarts.RaggedLbs = 388; 
 
        // finally, update an existing record 
        objCarts.Update(); 
    } 
 
    private void Delete() 
    { 
        // delete a record by primary key 
        Carts.Delete(20931); 
    } 
} 


#line default
#line hidden
