#pragma checksum "D:\JMann\Finance\Application\Finance\AddEdit_Carts.aspx.cs" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "206B34EFF072A38475FA19DCBF6EBD4987C37F15"

#line 1 "D:\JMann\Finance\Application\Finance\AddEdit_Carts.aspx.cs"
using System;
using System.Web.UI;
using jmann.BusinessObject;
using System.Web.Services;

namespace jmann
{
    public partial class AddEdit_Carts : System.Web.UI.Page
    {   private string CURRENT_LOCATION = "0";	// create variable to hold current location
        protected void Page_Load(object sender, EventArgs e)
        {   CURRENT_LOCATION = Globals.isNull(Session["locationID"], "0");		// get current location from session, default "0" if null
            if (!IsPostBack)
            {   fill_location();					// populate location dropdown
                string operation = (string)Request["operation"];

                if (!String.IsNullOrEmpty(operation))
                {
                    if (operation == "update")
                    {
                        LblTitle.Text = "Edit Carts";
                        BtnUpdateRecord.Visible = true;
                        BtnAddRecord.Visible = false;

                        // retrieve id(s) to be updated
                        string cartsID = (string)Request["cartsid"];

                        if (!String.IsNullOrEmpty(cartsID))
                        {
                            // retrieve record to be updated
                            Carts objCarts = jmann.BusinessObject.Carts.SelectByPrimaryKey(Convert.ToInt32(cartsID));

                            if (objCarts != null)
                            {
                                TxtCartsID.Enabled = false;
                                TxtCartsID.Text = objCarts.CartsID.ToString();
                                ddlLocation.SelectedValue = objCarts.LocationID.ToString();

                                if(objCarts.Date != null)
                                    TxtDate.Text = String.Format("{0:M/d/yyyy}",Convert.ToDateTime(objCarts.Date.ToString()));

								if (objCarts.CartsWorkedHard != null)
									TxtCartsWorkedHard.Text = objCarts.CartsWorkedHard.ToString();

								if (objCarts.CartsWorkedSoft != null)
									TxtCartsWorkedSoft.Text = objCarts.CartsWorkedSoft.ToString();

								if (objCarts.OnHandHard != null)
									TxtOnHandHard.Text = objCarts.OnHandHard.ToString();

								if (objCarts.OnHandSoft != null)
									TxtOnHandSoft.Text = objCarts.OnHandSoft.ToString();

								if (objCarts.HangTotal != null)
									TxtHangTotal.Text = objCarts.HangTotal.ToString();

								//                                if(objCarts.ThrownCount != null)
//                                      ThrownCount.Text = objCarts.ThrownCount.ToString();

                                if(objCarts.ThrownLbs != null)
                                    TxtThrownLbs.Text = objCarts.ThrownLbs.ToString();

                                if(objCarts.RaggedLbs != null)
                                    TxtRaggedLbs.Text = objCarts.RaggedLbs.ToString();
                            }
                        }
                    }
                    else
                    {
                        // add a record
                        PnlPrimaryKey.Visible = false;
                        BtnUpdateRecord.Visible = false;
                    }
                }
            }
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
                Carts objCarts;

                if (operation == "update")
                    objCarts = jmann.BusinessObject.Carts.SelectByPrimaryKey(Convert.ToInt32(TxtCartsID.Text));
                else
                {
                    objCarts = new Carts();
                }

                if (String.IsNullOrEmpty(TxtDate.Text))
                    objCarts.Date = null;
                else
                    objCarts.Date = Convert.ToDateTime(TxtDate.Text);

                if (String.IsNullOrEmpty(ddlLocation.SelectedValue))
                    objCarts.LocationID = null;
                else
                    objCarts.LocationID = Convert.ToInt32(ddlLocation.SelectedValue);

				if (String.IsNullOrEmpty(TxtCartsWorkedHard.Text))
					objCarts.CartsWorkedHard = null;
				else
					objCarts.CartsWorkedHard = Convert.ToDouble(TxtCartsWorkedHard.Text);

				if (String.IsNullOrEmpty(TxtCartsWorkedSoft.Text))
					objCarts.CartsWorkedSoft = null;
				else
					objCarts.CartsWorkedSoft = Convert.ToDouble(TxtCartsWorkedSoft.Text);

				if (String.IsNullOrEmpty(TxtOnHandHard.Text))
					objCarts.OnHandHard = null;
				else
					objCarts.OnHandHard = Convert.ToDouble(TxtOnHandHard.Text);

				if (String.IsNullOrEmpty(TxtOnHandSoft.Text))
					objCarts.OnHandSoft = null;
				else
					objCarts.OnHandSoft = Convert.ToDouble(TxtOnHandSoft.Text);

				if (String.IsNullOrEmpty(TxtHangTotal.Text))
                    objCarts.HangTotal = null;
                else
                    objCarts.HangTotal = Convert.ToInt32(TxtHangTotal.Text);

                if (String.IsNullOrEmpty(TxtThrownCount.Text))
                    objCarts.ThrownCount = null;
                else
                    objCarts.ThrownCount = Convert.ToInt32(TxtThrownCount.Text);

                if (String.IsNullOrEmpty(TxtThrownLbs.Text))
                    objCarts.ThrownLbs = null;
                else
                    objCarts.ThrownLbs = Convert.ToInt32(TxtThrownLbs.Text);

                if (String.IsNullOrEmpty(TxtRaggedLbs.Text))
                    objCarts.RaggedLbs = null;
                else
                    objCarts.RaggedLbs = Convert.ToInt32(TxtRaggedLbs.Text);

                // the insert method returns the newly created primary key
                int newlyCreatedPrimaryKey;

                if (operation == "update")
                    objCarts.Update();
                else
                    newlyCreatedPrimaryKey = objCarts.Insert();

                Response.Redirect("~/GridView_Carts.aspx");
            }
        }
        private void fill_location()
	    {
		    DAL dal = new DAL();	// instanciate the DAL

		    // get list of locations and bind to the location dropdown list
		    ddlLocation.DataSource = dal.LocationsAll;
		    ddlLocation.DataTextField = "locationname";
		    ddlLocation.DataValueField = "locationID";
		    ddlLocation.DataBind();
            if (ddlLocation.Items.Count > 0)
			{
				ddlLocation.SelectedValue = CURRENT_LOCATION;
			}
	    }
        protected void ddlLocation_SelectedIndexChanged(object sender, EventArgs e)
	    {
		    Session["locationID"] = ddlLocation.SelectedValue;		// store the current location value into session
	    }
    }
}


#line default
#line hidden
