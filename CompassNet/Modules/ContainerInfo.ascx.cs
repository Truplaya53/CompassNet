using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel;
namespace CompassNet.Modules
{
    public partial class ContainerInfo : System.Web.UI.UserControl
    {
        private BusinessObjects.Container c;
        public string ContainerNumber { get { return txtContainerNo.Text; } set { txtContainerNo.Text = value; } }
        public string ContainerType { get { return txtContainerType.Text; } set { txtContainerType.Text = value; } }
        public string Seal1 { get { return txtSeal1.Text; } set { txtSeal1.Text = value; } }
        public string Seal2 { get { return txtSeal2.Text; } set { txtSeal2.Text = value; } }
        public string Seal3 { get { return txtSeal3.Text; } set { txtSeal3.Text = value; } }
        public string TareWeight { get { return txtTareWeight.Text; } set { txtTareWeight.Text = value; } }
        public List<BusinessObjects.ContainerCargo> Cargo {
            get { if (c == null) { getContainerFromControls(); } return c.Cargo; }
        }
        
        [Bindable(true, BindingDirection.TwoWay)]
        [PersistenceMode(PersistenceMode.Attribute)] 
        public BusinessObjects.Container ContainerObject {
            get { getContainerFromControls(); return c; }
            set { c = value; ViewState["Container"] = c; 
            }
        }

        
        protected void Page_Load(object sender, EventArgs e)
        {
            RateFinder1.RateChosen += new RateFinder.RateChosenEventHandler(RateFinder1_RateChosen);
        }
        //public void bindcontrols()
        //{
        //    if (c != null)
        //    {
        //        //txtContainerNo.Text = c.ContainerNumber;
        //        //txtContainerType.Text = c.ContainerType;
        //        //txtSeal1.Text = c.Seal1;
        //        //txtSeal2.Text = c.Seal2;
        //        //txtSeal3.Text = c.Seal3;
        //        //txtTareWeight.Text = c.TareWeight;
        //        //txtDischargeDate.Text=c.DischargeDate.ToShortDateString();
        //        //txtPickupDate.Text = c.PickupDate.ToShortDateString();
        //        //txtReturnDate.Text = c.ReturnDate.ToShortDateString();
        //        //txtCarrierPickup.Text = c.CarrierPickUpLastDate.ToShortDateString();
        //        //txtCustReturnLastFree.Text = c.CustomerReturnLastDate.ToShortDateString();
        //        //txtCustPickupLastFree.Text = c.CustomerPickupLastDate.ToShortDateString();
        //        //txtCarrierReturn.Text = c.CarrierReturnLastDate.ToShortDateString();
        //        //grCargo.DataSource = c.Cargo;
        //        //grCargo.DataBind();
        //    }
        //}

        private void getContainerFromControls()
        {
            //Because of an error with viewstate. This will always be the original container before any edits deletes or inserts.
            //Viewstate is not holding updates. Only the first entry.
            if (c==null&&ViewState["Container"] != null) { c = (BusinessObjects.Container)ViewState["Container"]; }
            if (c==null){c = new BusinessObjects.Container();}

            c.ContainerNumber = txtContainerNo.Text;
            c.ContainerType = txtContainerType.Text;
            c.Seal1 = txtSeal1.Text;
            c.Seal2 = txtSeal2.Text;
            c.Seal3 = txtSeal3.Text;
            c.TareWeight = txtTareWeight.Text;
            DateTime dt = DateTime.MinValue;
            c.DischargeDate = DateTime.TryParse(txtDischargeDate.Text, out dt) ? dt : dt;
            c.PickupDate = DateTime.TryParse(txtPickupDate.Text, out dt) ? dt : dt;
            c.ReturnDate = DateTime.TryParse(txtReturnDate.Text, out dt) ? dt : dt;
            c.CarrierPickUpLastDate = DateTime.TryParse(txtCarrierPickup.Text, out dt) ? dt : dt;
            c.CustomerReturnLastDate = DateTime.TryParse(txtCustReturnLastFree.Text, out dt) ? dt : dt;
            c.CustomerPickupLastDate = DateTime.TryParse(txtCustPickupLastFree.Text, out dt) ? dt : dt;
            c.CarrierReturnLastDate = DateTime.TryParse(txtCarrierReturn.Text, out dt) ? dt : dt;
            c.Rate = txtRates.Text;

            if (grCargo.DataSource != null)
            {
                foreach (Obout.Grid.GridRow gr in grCargo.Rows)
                {
                    bool inst = false;
                    System.Collections.Hashtable row = gr.ToHashtable();
                    if (row.Count > 0)
                    {
                        BusinessObjects.ContainerCargo crg = (from cr in c.Cargo where cr.ID == int.Parse(row["ID"].ToString()) select cr).FirstOrDefault();
                        if (crg == null) { crg = new BusinessObjects.ContainerCargo(); inst = true; }
                        int i; double d; bool b;
                        i = 0; int.TryParse(row["ID"].ToString(), out i);
                        crg.ID = i;
                        crg.NetWeight = double.TryParse(row["NetWeight"].ToString(), out d) ? d : 0;
                        //i = 1; int.TryParse(row["NetWeightUnit"].ToString(), out i);
                        crg.NetWeightUnit = (BusinessObjects.Units.WeightUnits)i;
                        i = 0; int.TryParse(row["Pieces"].ToString(), out i);
                        crg.Pieces = i;
                        crg.UOM = row["UOM"].ToString();
                        crg.Description = row["Description"].ToString();
                        d = 0; double.TryParse(row["GrossWeight"].ToString(), out d);
                        crg.GrossWeight = d;
                        //i = 1; int.TryParse(row["GrossWeightUnit"].ToString(), out i);
                        crg.GrossWeightUnit = (BusinessObjects.Units.WeightUnits)i;
                        crg.Hazardous = bool.TryParse(row["Hazardous"].ToString(), out b) ? b : false;
                        if (inst) { c.Cargo.Add(crg); }
                    }
                }
            }
       }

        protected void RateFinder1_RateChosen(object sender, RateFinder.RateChosenEventArgs e)
        {
            txtRates.Text = e.RateValue;
            c = ContainerObject;

            object[] o = { c };
            //Control qe = Functions.Tools.FindConrolRecursive(this.Page.Controls[0], "QuoteEntry1");
            Functions.Tools.ExecutePageMethod("ContainerInfo_Saving", o, this.Page);
        }
        protected void grCargo_UpdateCommand(object sender, Obout.Grid.GridRecordEventArgs e)
        {

            System.Collections.Hashtable rcd = e.Record;

            object[] o = {rcd};
            string t = "QuoteEntry1";
            Control qe = Functions.Tools.FindConrolRecursive(this.Page.Controls[0], t) ;
            Functions.Tools.ExecuteControlMethod("ContainerCargo_Saving", o, qe);
        }
        protected void grCargo_DeleteCommand(object sender, Obout.Grid.GridRecordEventArgs e)
        {
            System.Collections.Hashtable rcd = e.Record;

            object[] o = { rcd };
            string t = "QuoteEntry1";
            Control qe = Functions.Tools.FindConrolRecursive(this.Page.Controls[0], t);
            Functions.Tools.ExecuteControlMethod("ContainerInfo_Deleting", o, qe);
        }
        protected void grCargo_InsertCommand(object sender, Obout.Grid.GridRecordEventArgs e)
        {
            System.Collections.Hashtable rcd = e.Record;

            object[] o = { rcd };
            string t = "QuoteEntry1";
            Control qe = Functions.Tools.FindConrolRecursive(this.Page.Controls[0], t);
            Functions.Tools.ExecuteControlMethod("ContainerCargo_Saving", o, qe);
        }
        protected override void Render(HtmlTextWriter writer)
        {
            //I have register all the scripts all over again because the update panel in the caller (parent page)
            //does not evaluate them

            base.Render(writer);
            ScriptManager sm = ScriptManager.GetCurrent(Page); 
            if (sm !=null && sm.IsInAsyncPostBack) {System.Text.StringBuilder sb = new System.Text.StringBuilder(); 
                string script = @"<script src='../Scripts/jquery-1.8.2.js' type='text/javascript'></script>
                                <script src='../Scripts/QuoteEntry.js' type='text/javascript'></script>
                                <script src='../Scripts/ui/external/jquery.bgiframe-2.1.2.js' type='text/javascript'></script>
                                <script src='../Scripts/ui/jquery.ui.core.js' type='text/javascript'></script>
                                <script src='../Scripts/ui/jquery.ui.widget.js' type='text/javascript'></script>
                                <script src='../Scripts/ui/jquery.ui.mouse.js' type='text/javascript'></script>
                                <script src='../Scripts/ui/jquery.ui.draggable.js' type='text/javascript'></script>
                                <script src='../Scripts/ui/jquery.ui.position.js' type='text/javascript'></script>
                                <script src='../Scripts/ui/jquery.ui.resizable.js' type='text/javascript'></script>
                                <script src='../Scripts/ui/jquery.ui.dialog.js' type='text/javascript'></script>";
                ScriptManager.RegisterStartupScript(this, typeof(ContainerInfo), UniqueID, script, false); 
            }
        }
    }
}