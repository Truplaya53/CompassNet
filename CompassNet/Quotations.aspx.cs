using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CompassNet
{
    public partial class Quotations : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                    GridView1.DataSource = Functions.DB.getQuotes();
                    GridView1.DataBind();
            }

        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Modules.QuoteEntry qe = (Modules.QuoteEntry)LoadControl("Modules/QuoteEntry.ascx");
            //qe.ID = "popup";
            //qe.QuoteNo = GridView1.SelectedDataKey.Value.ToString();
            //phPopup.Controls.Clear();
            //phPopup.Controls.Add(qe);
            //ViewState["popup"] = qe.QuoteNo;
            //pnlQuoteDetails_ModalPopupExtender.Show();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            string criteria = cbQuoteSearch.SelectedText;
            string searchType = ddSearchType.SelectedValue;
            GridView1.DataSource = Functions.DB.getQuotes(criteria,searchType);
            //GridView1.DataKeyNames = new string[] { "QuoteNumber" };
            GridView1.DataBind();
            btnClearSearch.Visible = true;
        }

        protected void btnClearSearch_Click(object sender, EventArgs e)
        {
            GridView1.DataSource = Functions.DB.getQuotes();
            //GridView1.DataKeyNames = new string[] { "QuoteNumber" };
            GridView1.DataBind();
            btnClearSearch.Visible = false;
        }

        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod]
        public static List<string> SearchQuotes(string prefixText, int count) 
        { 
              List<string> quotes = new List<string>();
              quotes = Functions.DB.GetQuoteNumers(prefixText,count);
              return quotes;
}

        protected void cbQuoteSearch_LoadingItems(object sender, Obout.ComboBox.ComboBoxLoadingItemsEventArgs e)
        {
            System.Data.DataTable data = Functions.DB.GetQuoteNumers(e.Text, e.ItemsOffset, 10);
            cbQuoteSearch.DataValueField = "REFERENCE_NO";
            cbQuoteSearch.DataTextField = "REFERENCE_NO";
            cbQuoteSearch.DataSource = data;
            cbQuoteSearch.DataBind();

            e.ItemsLoadedCount = e.ItemsOffset + data.Rows.Count;

            e.ItemsCount = Functions.DB.GetCustomersCount(e.Text);

        }

        protected void GridView1_DeleteCommand(object sender, Obout.Grid.GridRecordEventArgs e)
        {

        }
    }
}