using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CompassNet
{
    public partial class DataUpdate : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                bindgrid();
            }

        }

        protected void OboutDropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            bindgrid();
        }

        protected void Grid1_Select(object sender, Obout.Grid.GridRecordEventArgs e)
        {
            SqlDataSource src = new SqlDataSource();
            System.Collections.Hashtable al = (System.Collections.Hashtable)e.RecordsCollection[0];

            if (OboutDropDownList1.SelectedValue == "Clients")
            {
                src = Functions.DB.GetSQLDataSource2(string.Format("SELECT top 1 *  FROM CLIENT_DAT Where ID='{0}'", al["ID"].ToString()));
            }
            else if (OboutDropDownList1.SelectedValue == "Sales Reps")
            {
                src = Functions.DB.GetSQLDataSource(string.Format("SELECT top 1 *  FROM SalesReps Where ID={0}", al["ID"].ToString()));
            }

            SuperForm1.DefaultMode = DetailsViewMode.Edit;
            SuperForm1.DataSource = src;
            SuperForm1.DataBind();
        }
        public void bindgrid()
        {
            System.Data.DataTable dt = new System.Data.DataTable();

            if (OboutDropDownList1.SelectedValue == "Clients")
            {
                dt = Functions.DB.GetCustomers();
            }
            else if (OboutDropDownList1.SelectedValue == "Sales Reps")
            {
                dt = Functions.DB.GetSalesReps();
            }

            Grid1.DataSource = dt;
            Grid1.DataBind();
            string ID = string.Empty;
            SqlDataSource src = new SqlDataSource();
            if (dt.Rows.Count > 0)
            {
                System.Collections.Hashtable al = Grid1.Rows[0].ToHashtable();
                ID = al["ID"].ToString();
            }
            if (OboutDropDownList1.SelectedValue == "Clients")
            {
                src = Functions.DB.GetSQLDataSource2("SELECT top 1 *  FROM CLIENT_DAT Where ID='" + ID + "'");
            }
            else if (OboutDropDownList1.SelectedValue == "Sales Reps")
            {
                src = Functions.DB.GetSQLDataSource("SELECT top 1 *  FROM SalesReps Where ID=" + ID);
            }

            // SuperForm1.DefaultMode = DetailsViewMode.ReadOnly;
            SuperForm1.DataSource = src;
            SuperForm1.DataBind();

        }

        protected void SuperForm1_ItemUpdating(object sender, DetailsViewUpdateEventArgs e)
        {

            SqlDataSource src = new SqlDataSource();
            if (OboutDropDownList1.SelectedValue == "Clients")
            {
                BusinessObjects.Client c = new BusinessObjects.Client();
                c.ClientID = e.NewValues["ID"].ToString();
                c.Type = e.NewValues["Type"].ToString();
                c.Name = e.NewValues["Name"].ToString();
                c.Contact = e.NewValues["Contact"].ToString();
                c.Phone = e.NewValues["Phone"].ToString();
                c.Fax = e.NewValues["Fax"].ToString();
                c.ReferenceNo = e.NewValues["ReferenceNo"].ToString();
                Functions.DB.SaveClient(c);
                ID = e.NewValues["ID"].ToString();
            }
            else if (OboutDropDownList1.SelectedValue == "Sales Reps")
            {
                BusinessObjects.SalesRep s = new BusinessObjects.SalesRep();
                int id; int.TryParse(e.NewValues["ID"].ToString(),out id);
                s.ID = id;
                s.Code = e.NewValues["Code"].ToString();
                s.Name = e.NewValues["Name"].ToString();
                bool act; bool.TryParse(e.NewValues["Active"].ToString(),out act);
                s.Active = act;
                Functions.DB.SaveSalesRep(s);
                ID = e.NewValues["ID"].ToString();
            }
            bindgrid();
        }

    }
}