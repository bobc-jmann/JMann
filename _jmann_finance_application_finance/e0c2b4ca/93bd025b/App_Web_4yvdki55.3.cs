#pragma checksum "D:\JMann\Finance\Application\Finance\GridViewAddEdit_Location.aspx.cs" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "5D743F5705FD97C540581E3342C0183743319414"

#line 1 "D:\JMann\Finance\Application\Finance\GridViewAddEdit_Location.aspx.cs"
using System;
using System.Web.UI;
using jmann.BusinessObject;
using System.Web.Services;

namespace jmann
{
    public partial class GridViewAddEdit_Location : System.Web.UI.Page
    {
        protected void GridView1_RowDataBound(object sender, System.Web.UI.WebControls.GridViewRowEventArgs e)
        {
            Functions.GridViewRowDataBound(sender, e, 1);
        }

        protected void GridView1_RowCreated(object sender, System.Web.UI.WebControls.GridViewRowEventArgs e)
        {
            Functions.GridViewRowCreated(sender, e, 1);
        }

        protected void ObjectDataSource1_Deleted(object sender, System.Web.UI.WebControls.ObjectDataSourceStatusEventArgs e)
        {
            Functions.ObjectDataSourceDeleted(sender, e, this.Page);
        }

        protected void BtnAddRecord_Click(object sender, EventArgs e)
        {
            AddOrUpdateRecord("add");
        }

        protected void BtnUpdateRecord_Click(object sender, EventArgs e)
        {
            AddOrUpdateRecord("update");
        }

        private void AddOrUpdateRecord(string operation)
        {
            if (IsValid)
            {
                Location objLocation;

                if (operation == "update")
                    objLocation = jmann.BusinessObject.Location.SelectByPrimaryKey(Convert.ToInt32(HfldLocationID.Value));
                else
                {
                    objLocation = new Location();
                }

                if (String.IsNullOrEmpty(TxtName.Text))
                    objLocation.Name = null;
                else
                    objLocation.Name = TxtName.Text;

                if (String.IsNullOrEmpty(TxtDescription.Text))
                    objLocation.Description = null;
                else
                    objLocation.Description = TxtDescription.Text;

                if (String.IsNullOrEmpty(TxtFeetOfRack.Text))
                    objLocation.FeetOfRack = null;
                else
                    objLocation.FeetOfRack = Convert.ToInt32(TxtFeetOfRack.Text);

                // the insert method returns the newly created primary key
                int newlyCreatedPrimaryKey;

                if (operation == "update")
                    objLocation.Update();
                else
                    newlyCreatedPrimaryKey = objLocation.Insert();

                GridView1.DataBind();
            }
        }

        [WebMethod]
        public static Location GetLocation(string locationID)
        {
            return jmann.BusinessObject.Location.SelectByPrimaryKey(Convert.ToInt32(locationID));
        }
    }
}


#line default
#line hidden
