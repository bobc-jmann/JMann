#pragma checksum "D:\JMann\Finance\Application\Finance\App_Code\Example\LocationExample.cs" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "9481E35B519937B9DD824F15010274A79CDD728D"

#line 1 "D:\JMann\Finance\Application\Finance\App_Code\Example\LocationExample.cs"
using System; 
using jmann.BusinessObject; 
using System.Web.UI.WebControls; 
// using System.Windows.Forms;    // Note: remove comment when using with windows forms 
 
/// <summary> 
/// These are data-centric code examples for the Location table. 
/// You can cut and paste the respective codes into your application 
/// by changing the sample values assigned from these examples. 
/// NOTE: This class contains private methods because they're 
/// not meant to be called by an outside client.  Each method contains 
/// code for the respective example being shown 
/// </summary> 
public sealed class LocationExample 
{ 
    private LocationExample() 
    { 
    } 
 
    private void SelectAll() 
    { 
        // select all records 
        LocationCollection objLocationCol = Location.SelectAll(); 
 
        // Example 1:  you can optionally sort the collection in ascending order by your chosen field  
        objLocationCol.Sort(Location.ByName); 
 
        // Example 2:  to sort in descending order, add this line to the Sort code in Example 1  
        objLocationCol.Reverse(); 
 
        // Example 3:  directly bind to a GridView 
        GridView grid = new GridView(); 
        grid.DataSource = objLocationCol; 
        grid.DataBind(); 
 
        // Example 4:  loop through all the Location(s) 
        foreach (Location objLocation in objLocationCol) 
        { 
            int locationID = objLocation.LocationID; 
            string name = objLocation.Name; 
            string description = objLocation.Description; 
            int? feetOfRack = objLocation.FeetOfRack; 
        } 
    } 
 
    private void SelectByPrimaryKey() 
    { 
        // select a record by primary key(s) 
        Location objLocation = Location.SelectByPrimaryKey(0); 
 
        if (objLocation != null) 
        { 
            // if record is found, a record is returned 
            int locationID = objLocation.LocationID; 
            string name = objLocation.Name; 
            string description = objLocation.Description; 
            int? feetOfRack = objLocation.FeetOfRack; 
        } 
    } 
 
    /// <summary> 
    /// Selects LocationID and Name columns for use with a with a Drop Down List, Combo Box, Checked Box List, List View, List Box, etc 
    /// </summary> 
    private void SelectLocationDropDownListData() 
    { 
        LocationCollection objLocationCol = Location.SelectLocationDropDownListData(); 
 
        // Example 1:  directly bind to a drop down list 
        DropDownList ddl1 = new DropDownList(); 
        ddl1.DataValueField = "LocationID"; 
        ddl1.DataTextField = "Name"; 
        ddl1.DataSource = objLocationCol; 
        ddl1.DataBind(); 
 
        // Example 2:  add each item through a loop 
        DropDownList ddl2 = new DropDownList(); 
 
        foreach (Location objLocation in objLocationCol) 
        { 
            ddl2.Items.Add(new ListItem(objLocation.Name, objLocation.LocationID.ToString())); 
        } 
 
        // Example 3:  bind to a combo box.  Note: remove comment when using with windows forms 
        // ComboBox cbx1 = new ComboBox(); 
 
        // foreach (Location objLocation in objLocationCol) 
        // { 
        //     cbx1.Items.Add(new ListItem(objLocation.Name, objLocation.LocationID.ToString())); 
        // } 
    } 
 
    private void Insert() 
    { 
        // first instantiate a new Location 
        Location objLocation = new Location(); 
 
        // assign values you want inserted 
        objLocation.Name = "Unknown"; 
        objLocation.Description = "Unknown location"; 
        objLocation.FeetOfRack = 0; 
 
        // finally, insert a new record 
        // the insert method returns the newly created primary key 
        int newlyCreatedPrimaryKey = objLocation.Insert(); 
    } 
 
    private void Update() 
    { 
        // first instantiate a new Location 
        Location objLocation = new Location(); 
 
        // assign the existing primary key(s) 
        // of the record you want updated 
        objLocation.LocationID = 0; 
 
        // assign values you want updated 
        objLocation.Name = "Unknown"; 
        objLocation.Description = "Unknown location"; 
        objLocation.FeetOfRack = 0; 
 
        // finally, update an existing record 
        objLocation.Update(); 
    } 
 
    private void Delete() 
    { 
        // delete a record by primary key 
        Location.Delete(9); 
    } 
} 


#line default
#line hidden
