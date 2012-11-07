using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CompassNet
{
    public partial class Rates : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            bindGrid();
        }

        private void bindGrid()
        {
            BusinessObjects.Rates.Rate r1 = new BusinessObjects.Rates.Rate();
            List<BusinessObjects.Rates.Rate> L = new List<BusinessObjects.Rates.Rate>();

            r1.Shipper = ddContract.SelectedText;
            r1.Origin = ddOrigin.SelectedText;
            r1.Destination = ddDestination.SelectedText;
            r1.Commodity = ddCommodity.SelectedText;
            r1.ContainerType = ddCommodity.SelectedText;
            r1.DollarValue = 456.00;
            L.Add(r1);

            GridView1.DataSource = L;
            GridView1.DataBind();
        
        }
        protected void ddOrigin_LoadingItems(object sender, Obout.ComboBox.ComboBoxLoadingItemsEventArgs e)
        {
            System.Data.DataTable data = Functions.DB.GetOrigins(e.Text, e.ItemsOffset, 20);
            ddOrigin.DataValueField = "Code";
            ddOrigin.DataTextField = "Origin";
            ddOrigin.DataSource = data;
            ddOrigin.DataBind();

            e.ItemsLoadedCount = e.ItemsOffset + data.Rows.Count;

            e.ItemsCount = Functions.DB.GetOriginsCount(e.Text);
        }

        protected void ddDestination_LoadingItems(object sender, Obout.ComboBox.ComboBoxLoadingItemsEventArgs e)
        {
            System.Data.DataTable data;
            
                data = Functions.DB.GetDestinations(e.Text, string.Empty, e.ItemsOffset, 20);

            ddDestination.DataValueField = "Code";
            ddDestination.DataTextField = "Destination";
            ddDestination.DataSource = data;
            ddDestination.DataBind();

            e.ItemsLoadedCount = e.ItemsOffset + data.Rows.Count;

            e.ItemsCount = Functions.DB.GetDestinationsCount(e.Text, string.Empty);
        }

        protected void btnGetRates_Click(object sender, EventArgs e)
        {
            string origin = ddOrigin.SelectedValue;
            string destination = ddDestination.SelectedValue;
            DateTime shipdt = DateTime.Parse( txtShippingDate.Text);
            int tester;
            int? contractid;int? commodityid; int? containerid;
            if(int.TryParse(ddContract.SelectedValue,out tester)){contractid=tester;}else{ contractid = null;}
            if(int.TryParse(ddCommodity.SelectedValue,out tester)){commodityid=tester;}else{ commodityid = null;}
            if(int.TryParse(ddContainer.SelectedValue,out tester)){containerid=tester;}else{ containerid = null;}

            List<BusinessObjects.Rates.Rate> L = Functions.DB.GetRates(origin, destination, shipdt, contractid, containerid, commodityid);
            GridView1.DataSource = L;
            GridView1.DataBind();
        }
    }
}