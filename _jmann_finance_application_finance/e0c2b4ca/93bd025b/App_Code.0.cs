#pragma checksum "D:\JMann\Finance\Application\Finance\App_Code\Helper\Functions.cs" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "C30BAF5936403DDF77F5F694DE2DA5C031E6F6CD"

#line 1 "D:\JMann\Finance\Application\Finance\App_Code\Helper\Functions.cs"
using System;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Text.RegularExpressions;
using System.Web.UI;
using System.IO;

namespace jmann
{
    public sealed class Functions
    {
        private Functions()
        {
        }

        public static void GridViewRowCreated(Object sender, GridViewRowEventArgs e, int nonSortableColumnCount)
        {
            if (e != null)
            {
                if (e.Row.RowType == DataControlRowType.Header)
                {
                    GridView gridView = (GridView)sender;
                    int columnCount = gridView.Columns.Count - nonSortableColumnCount;
                    int sortColumnIndex = GetSortColumnIndex(gridView);

                    if (sortColumnIndex != -1 && sortColumnIndex < columnCount)
                    {
                        AddSortImage(gridView, sortColumnIndex, e.Row);
                    }
                }
            }
        }

        private static int GetSortColumnIndex(GridView gridView)
        {
            foreach (DataControlField field in gridView.Columns)
            {
                if (!String.IsNullOrEmpty(gridView.SortExpression))
                {
                    if (field.SortExpression == gridView.SortExpression)
                    {
                        return gridView.Columns.IndexOf(field);
                    }
                }
            }

            return -1;
        }

        private static void AddSortImage(GridView gridView, int columnIndex, GridViewRow headerRow)
        {
            Image sortImage = new Image();

            if (gridView.SortDirection == SortDirection.Ascending)
                sortImage.ImageUrl = "~/" + ConfigurationManager.AppSettings["ArrowUp"];
            else
                sortImage.ImageUrl = "~/" + ConfigurationManager.AppSettings["ArrowDown"];

            // add the image to the appropriate header cell
            headerRow.Cells[columnIndex].Controls.Add(sortImage);
        }

        public static void GridViewRowDataBound(Object sender, GridViewRowEventArgs e, int primaryKeyCount)
        {
            if (e != null)
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    // set hover highlight for the gridview
                    e.Row.Attributes.Add("onmouseover", "SetGridViewHoverOn(this);");
                    e.Row.Attributes.Add("onmouseout", "SetGridViewHoverOff(this);");
                }
            }
        }

        public static void ObjectDataSourceDeleted(object sender, System.Web.UI.WebControls.ObjectDataSourceStatusEventArgs e, Page page)
        {
            if (e.Exception != null)
            {
                // show delete error
                string errorMessage = Functions.RemoveSpecialChars(e.Exception.InnerException.Message);
                ScriptManager.RegisterStartupScript(page, page.GetType(), "err_msg", "ShowError('" + errorMessage + "');", true);
                e.ExceptionHandled = true;
            }
        }

        private static string RemoveSpecialChars(string text)
        {
            Regex regex = new Regex("[^a-zA-Z0-9 -]");
            return regex.Replace(text, "");
        }
         
    }
}


#line default
#line hidden
