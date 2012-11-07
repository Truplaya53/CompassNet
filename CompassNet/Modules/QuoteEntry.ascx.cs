using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Obout.ComboBox;

namespace CompassNet.Modules
{
    public partial class QuoteEntry : System.Web.UI.UserControl
    {
        AjaxControlToolkit.ModalPopupExtender mp;

        private BusinessObjects.Quote q;
        public BusinessObjects.Quote QuoteObject
        {
            get { return q; }
        }
        private string _quoteno=string.Empty;
        public string QuoteNo { 
            get { return _quoteno;} 
            set { _quoteno  = value;
                    q = Functions.DB.getQuote(_quoteno);
                    ViewState["Quote"] = q;
                } 
        }



        public event System.EventHandler myDialogHidden;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {//QuoteNo = "000111";
            
                if (q!=null && q.QuoteNumber != null && q.QuoteNumber != string.Empty) { lblQuoteNO.Text = q.QuoteNumber; }
                bindDropdowns("ddDivision");
                bindDropdowns("ddPreferredCarrier");

                if (ViewState["Quote"] != null) { q = (BusinessObjects.Quote)ViewState["Quote"]; }
                bindControls();
                Session["Quote"] = null;
            }
            else
            {
                //I'm using the session state here because the Viewstate has not been holding the changes consistently
                if (Session["Quote"] != null)
                {
                    q = (BusinessObjects.Quote)Session["Quote"];
                    Session["Quote"] = null; ViewState["Quote"] = q;
                }
                else if (ViewState["Quote"] != null) { 
                    q = (BusinessObjects.Quote)ViewState["Quote"];                 
                }
            }               
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            q.Customer = cbxCustomer.SelectedText;
            q.CustomerCode = cbxCustomer.SelectedValue;
            q.CustomerContact = txtContact.Text;
            q.CustomerPhone = txtTelephone.Text;
            q.CustomerFax = txtFax.Text;
            q.CustomerReferenceNo = txtCustRef.Text;
            q.PickupCity = ddPickupCity.SelectedValue;
            q.CustomerState = ddcustState.SelectedValue;
            q.CustomerZip = txtcustZip.Text;
            q.PortOfLoad = ddPortofLoad.SelectedValue;
            q.PortOfDispatch = ddPortOfDispatch.SelectedValue;
            q.FinalDestination = ddFinalDestination.SelectedValue;
            q.DestinationZip = txtDestZip.Text;
            q.DestinationCountry = ddDestCountry.SelectedValue;
            q.QuoteNumber = txtQuoteNo.Text;
            q.Division = ddDivision.SelectedValue;
            q.Businessline = ddBusinessLine.SelectedValue;
            q.INCOTerms = txtINCOTerms.Text;
            q.INCOTermsLocation = txtINCOLocation.Text;
            DateTime effdate;
            DateTime.TryParse( txtEffextiveDate.Text, out effdate);
            q.Effectivdate = effdate;
            DateTime expdate;
            DateTime.TryParse(txtExpirationDate.Text,out expdate);
            q.ExpirationDate = expdate;
            q.QuotedBy = txtQuotedBy.Text;
            q.SalesRep = ddSalesRep.SelectedValue;
            q.PreferredCarrier = ddPreferredCarrier.SelectedValue;
            q.Voyage = txtVoyage.Text;
            q.Vessel = ddVessel.SelectedValue;
            q.ContainerHandling = ddContainerHandling.SelectedValue;

            Functions.DB.SaveQuote(q);
            Session["Quote"] = null;
            ViewState["Quote"] = null;
            ViewState["BeforeEdit"] = null;

            if (this.myDialogHidden != null)this.myDialogHidden(this, new EventArgs());
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            if (mp != null) { mp.Hide(); }
            Session["Quote"] = null;
            ViewState["Quote"] = null;
            ViewState["BeforeEdit"] = null;

            if(this.myDialogHidden!=null)this.myDialogHidden(this, new EventArgs());
        }

        private void bindControls()
        {
            if (q == null) { q = new BusinessObjects.Quote(); }

            txtQuoteNo.Text = q.QuoteNumber;
            if (cbxCustomer.Items.Count < 2) { cbxCustomer.DataSource = Functions.DB.GetCustomers(q.Customer, 0, 1); cbxCustomer.DataBind(); }
            if (cbxCustomer.Items.Count(i => i.Text == q.Customer) > 0)
            {
                cbxCustomer.SelectedIndex = cbxCustomer.Items.IndexOf(cbxCustomer.Items.First(i => i.Text == q.Customer));
            }
            txtContact.Text = q.CustomerContact;
            txtTelephone.Text=q.CustomerPhone;
            txtFax.Text=q.CustomerFax;
            txtCustRef.Text=q.CustomerReferenceNo;
            if (ddPickupCity.Items.Count < 2) { ddPickupCity.DataSource = Functions.DB.GetPlaces(q.PickupCity, 0, 1); ddPickupCity.DataBind(); }
            if (ddPickupCity.Items.Count(i => i.Text == q.PickupCity) > 0)
            {
                ddPickupCity.SelectedIndex = ddPickupCity.Items.IndexOf(ddPickupCity.Items.First(i => i.Text == q.PickupCity));
            }
            if (ddcustState.Items.Count < 2) { ddcustState.DataBind(); }
            if (ddcustState.Items.Count(i => i.Text == q.CustomerState) > 0)
            {
                ddcustState.SelectedIndex = ddcustState.Items.IndexOf(ddcustState.Items.First(i => i.Text == q.CustomerState));
            }
            txtcustZip.Text=q.CustomerZip;
            if (ddPortofLoad.Items.Count < 2) { ddPortofLoad.DataSource = Functions.DB.GetPorts(q.PortOfLoad, 0, 1); ddPortofLoad.DataBind(); }
            if (ddPortofLoad.Items.Count(i => i.Text == q.PortOfLoad) > 0)
            {
                ddPortofLoad.SelectedIndex = ddPortofLoad.Items.IndexOf(ddPortofLoad.Items.First(i => i.Text == q.PortOfLoad));
            }
            if (ddPortOfDispatch.Items.Count < 2) { ddPortOfDispatch.DataSource = Functions.DB.GetPorts(q.PortOfDispatch, 0, 1); ddPortOfDispatch.DataBind(); }
            if (ddPortOfDispatch.Items.Count(i => i.Text == q.PortOfDispatch) > 0)
            {
                ddPortOfDispatch.SelectedIndex = ddPortOfDispatch.Items.IndexOf(ddPortOfDispatch.Items.First(i => i.Text == q.PortOfDispatch));
            }
            if (ddFinalDestination.Items.Count < 2) { ddFinalDestination.DataSource = Functions.DB.GetPlaces(q.FinalDestination, 0, 1); ddFinalDestination.DataBind(); }
            if (ddFinalDestination.Items.Count(i => i.Value == q.FinalDestination) > 0)
            {
                ddFinalDestination.SelectedIndex = ddFinalDestination.Items.IndexOf(ddFinalDestination.Items.First(i => i.Value == q.FinalDestination));
            }
            txtDestZip.Text=q.DestinationZip;
            if (ddDestCountry.Items.Count < 2) { ddDestCountry.DataBind(); }
            if (ddDestCountry.Items.Count(i => i.Text == q.DestinationCountry) >0)
            {
                ddDestCountry.SelectedIndex =ddDestCountry.Items.IndexOf( ddDestCountry.Items.First(i=>i.Text==q.DestinationCountry));
            }
            if (ddTypeOfMove.Items.Count < 2) { ddTypeOfMove.DataSource = Functions.DB.GetServiceTypes(q.TypeOfMove, 0, 1); ddTypeOfMove.DataBind(); }
            if (ddTypeOfMove.Items.Count(i => i.Text == q.TypeOfMove) >0) {
                ddTypeOfMove.SelectedIndex= ddTypeOfMove.Items.IndexOf(ddTypeOfMove.Items.First(i => i.Text == q.TypeOfMove));
             }
            txtQuoteNo.Text = q.QuoteNumber;
            ddDivision.SelectedValue = ddDivision.Items.FindByValue(q.Division) != null ? q.Division : string.Empty;
            ddBusinessLine.SelectedValue = ddBusinessLine.Items.FindByValue(q.Businessline) != null ? q.Businessline : string.Empty;
            txtINCOTerms.Text=q.INCOTerms;
            txtINCOLocation.Text=q.INCOTermsLocation;
            txtEffextiveDate.Text= q.Effectivdate==DateTime.MinValue?"": q.Effectivdate.ToShortDateString();
            txtExpirationDate.Text = q.ExpirationDate == DateTime.MinValue ? "" : q.ExpirationDate.ToShortDateString();
            txtQuotedBy.Text=q.QuotedBy;
            if (ddSalesRep.Items.Count < 2) { ddSalesRep.DataBind(); }
            if (ddSalesRep.Items.Count(i => i.Value == q.SalesRep) > 0)
            {
                ddSalesRep.SelectedIndex = ddSalesRep.Items.IndexOf(ddSalesRep.Items.First(i => i.Value == q.SalesRep));
            }
            ddPreferredCarrier.SelectedValue = ddPreferredCarrier.Items.FindByText(q.PreferredCarrier) != null ? q.PreferredCarrier : string.Empty;
            txtVoyage.Text=q.Voyage;
            ddVessel.SelectedValue = ddVessel.Items.FindByText(q.Vessel) != null ? q.Vessel : string.Empty;
            ddContainerHandling.SelectedValue=ddContainerHandling.Items.FindByText(q.ContainerHandling) != null ? q.ContainerHandling : string.Empty;

            bindgrid();
        }
        private void bindgrid()
        { 
            lvContainers.DataSource = q.Containers;
            lvContainers.DataBind();
            ViewState["DoNotRebind"] = null;        
      
        }
        private void bindDropdowns(string controlname)
        {            
            switch (controlname)
            {
                case "ddCustomer":
                    cbxCustomer.DataSource = Functions.DB.GetCustomers();
                    cbxCustomer.DataValueField = "ID";
                    cbxCustomer.DataTextField = "NAME";
                    cbxCustomer.DataBind();
                    cbxCustomer.Items.Insert(0, new ComboBoxItem(string.Empty));
                    break;
                case "ddPickupCity":
                    ddPickupCity.DataSource = Functions.DB.GetPlaces();
                    ddPickupCity.DataValueField = "POR CODE";
                    ddPickupCity.DataTextField = "POR PORT_NAME";
                    ddPickupCity.DataBind();
                    ddPickupCity.Items.Insert(0, new ComboBoxItem(string.Empty));
                    ddFinalDestination.DataSource = Functions.DB.GetPlaces();
                    ddFinalDestination.DataValueField = "POR CODE";
                    ddFinalDestination.DataTextField = "POR PORT_NAME";
                    ddFinalDestination.DataBind();
                    ddFinalDestination.Items.Insert(0, new ComboBoxItem(string.Empty));
                    break;
                case "ddPortofLoad":
                    ddPortofLoad.DataSource = Functions.DB.GetPorts();
                    ddPortofLoad.DataValueField = "POR CODE";
                    ddPortofLoad.DataTextField = "POR PORT_NAME";
                    ddPortofLoad.DataBind();
                    ddPortofLoad.Items.Insert(0, new ComboBoxItem(string.Empty));
                    ddPortOfDispatch.DataSource = Functions.DB.GetPorts();
                    ddPortOfDispatch.DataValueField = "POR CODE";
                    ddPortOfDispatch.DataTextField = "POR PORT_NAME";
                    ddPortOfDispatch.DataBind();
                    ddPortOfDispatch.Items.Insert(0, new ComboBoxItem(string.Empty));
                    break;
                case "ddState":
                    ddcustState.DataSource = Functions.DB.GetStates();
                    ddcustState.DataValueField = "StateCode";
                    ddcustState.DataTextField = "StateCode";
                    ddcustState.DataBind();
                    ddcustState.Items.Insert(0, new ComboBoxItem(string.Empty));
                    ddDestCountry.DataSource = Functions.DB.GetCountries();
                    ddDestCountry.DataValueField = "CountryCode";
                    ddDestCountry.DataTextField = "CountryCode";
                    ddDestCountry.DataBind();
                    ddDestCountry.Items.Insert(0, new ComboBoxItem(string.Empty));
                    break;
                case "ddTypeOfMove":
                    ddTypeOfMove.DataSource = Functions.DB.GetServiceTypes();
                    ddTypeOfMove.DataValueField = "ServiceID";
                    ddTypeOfMove.DataTextField = "ServiceType";
                    ddTypeOfMove.DataBind();
                    ddTypeOfMove.Items.Insert(0, new Obout.ComboBox.ComboBoxItem(string.Empty));
                    break;
                case "ddDivision":
                    ddDivision.DataSource = Functions.DB.GetDivisions();
                    ddDivision.DataValueField = "Code";
                    ddDivision.DataTextField = "Code";
                    ddDivision.DataBind();
                    ddDivision.Items.Insert(0, new ListItem(string.Empty));
                    ddBusinessLine.DataSource = Functions.DB.GetBusinessLines();
                    ddBusinessLine.DataValueField = "Code";
                    ddBusinessLine.DataTextField = "Code";
                    ddBusinessLine.DataBind();
                    ddBusinessLine.Items.Insert(0, new ListItem(string.Empty));
                    break;
                case "ddSalesRep":
                    ddSalesRep.DataSource = Functions.DB.GetSalesReps();
                    ddSalesRep.DataValueField = "Code";
                    ddSalesRep.DataTextField = "NAME";
                    ddSalesRep.DataBind();
                    ddSalesRep.Items.Insert(0, new ComboBoxItem(string.Empty));
                    break;
                case "ddPreferredCarrier":
                    ddPreferredCarrier.DataSource = Functions.DB.GetCarriers();
                    ddPreferredCarrier.DataValueField = "ID";
                    ddPreferredCarrier.DataTextField = "NAME";
                    ddPreferredCarrier.DataBind();
                    ddPreferredCarrier.Items.Insert(0, new ListItem(string.Empty));
                    ddVessel.DataSource = Functions.DB.GetVessels();
                    ddVessel.DataValueField = "ID";
                    ddVessel.DataTextField = "NAME";
                    ddVessel.DataBind();
                    ddVessel.Items.Insert(0, new ListItem(string.Empty));
                    ddContainerHandling.DataSource = Functions.DB.GetCustomers();
                    ddContainerHandling.DataValueField = "ID";
                    ddContainerHandling.DataTextField = "NAME";
                    ddContainerHandling.DataBind();
                    ddContainerHandling.Items.Insert(0, new ListItem(string.Empty));
                    break;
                default:
                    break;
            }
        
        
        }


        public void PanelUpdate()
        {
            txtContact.Text = "ppppp";
            txtTelephone.Text = "q.CustomerPhone";
            txtFax.Text = "q.CustomerFax";
            
        }


#region "Controls Dynamic Paging Events"
        protected void ddTypeOfMove_LoadingItems(object sender, ComboBoxLoadingItemsEventArgs e)
        {
            // Getting the items
            System.Data.DataTable data=Functions.DB.GetServiceTypes(e.Text, e.ItemsOffset, 10);
            ddTypeOfMove.DataSource = data;
            ddTypeOfMove.DataBind();

            // Calculating the numbr of items loaded so far in the ComboBox
            e.ItemsLoadedCount = e.ItemsOffset + data.Rows.Count;

            // Getting the total number of items that start with the typed text
            e.ItemsCount = Functions.DB.GetServiceTypesCount(e.Text);
        }
        protected void ddFinalDestination_LoadingItems(object sender, ComboBoxLoadingItemsEventArgs e)
        {
            System.Data.DataTable data = Functions.DB.GetPlaces(e.Text, e.ItemsOffset, 10);
            ddFinalDestination.DataValueField = "POR CODE";
            ddFinalDestination.DataTextField = "POR PORT_NAME";
            ddFinalDestination.DataSource = data;
            ddFinalDestination.DataBind();

            e.ItemsLoadedCount = e.ItemsOffset + data.Rows.Count;

            e.ItemsCount = Functions.DB.GetPlacesCount(e.Text);
        }
        protected void ddPickupCity_LoadingItems(object sender, ComboBoxLoadingItemsEventArgs e)
        {
            System.Data.DataTable data = Functions.DB.GetPlaces(e.Text, e.ItemsOffset, 10);
            ddPickupCity.DataValueField = "POR CODE";
            ddPickupCity.DataTextField = "POR PORT_NAME";
            ddPickupCity.DataSource = data;
            ddPickupCity.DataBind();

            e.ItemsLoadedCount = e.ItemsOffset + data.Rows.Count;

            e.ItemsCount = Functions.DB.GetPlacesCount(e.Text);
        }
        protected void ddPortofLoad_LoadingItems(object sender, ComboBoxLoadingItemsEventArgs e)
        {
            System.Data.DataTable data = Functions.DB.GetPorts(e.Text, e.ItemsOffset, 10);
            ddPortofLoad.DataValueField = "POR CODE";
            ddPortofLoad.DataTextField = "POR PORT_NAME";
            ddPortofLoad.DataSource = data;
            ddPortofLoad.DataBind();

            e.ItemsLoadedCount = e.ItemsOffset + data.Rows.Count;

            e.ItemsCount = Functions.DB.GetPortsCount(e.Text);
        }
        protected void ddPortOfDispatch_LoadingItems(object sender, ComboBoxLoadingItemsEventArgs e)
        {
            System.Data.DataTable data = Functions.DB.GetPorts(e.Text, e.ItemsOffset, 10);
            ddPortOfDispatch.DataValueField = "POR CODE";
            ddPortOfDispatch.DataTextField = "POR PORT_NAME";
            ddPortOfDispatch.DataSource = data;
            ddPortOfDispatch.DataBind();

            e.ItemsLoadedCount = e.ItemsOffset + data.Rows.Count;

            e.ItemsCount = Functions.DB.GetPortsCount(e.Text);
        }
        protected void cbxCustomer_LoadingItems(object sender, ComboBoxLoadingItemsEventArgs e)
        {
            System.Data.DataTable data = Functions.DB.GetCustomers(e.Text, e.ItemsOffset, 10);
            cbxCustomer.DataValueField = "ID";
            cbxCustomer.DataTextField = "NAME";
            cbxCustomer.DataSource = data;
            cbxCustomer.DataBind();

            e.ItemsLoadedCount = e.ItemsOffset + data.Rows.Count;

            e.ItemsCount = Functions.DB.GetCustomersCount(e.Text);
        }
#endregion


#region "Grid Editing Events"

        protected void AddContainer_Click(object sender, EventArgs e)
        {
            ViewState["BeforeEdit"] = q;
            lvContainers.InsertItemPosition = InsertItemPosition.LastItem;
            bindgrid();
            ViewState["Quote"] = q;
        }
        protected void lvContainers_ItemEditing(object sender, ListViewEditEventArgs e)
        {
            lvContainers.EditIndex = e.NewEditIndex;
            bindControls();
            ViewState["Quote"] = q;
            ViewState["BeforeEdit"] = q;
        }
        protected void lvContainers_ItemCanceling(object sender, ListViewCancelEventArgs e)
        {
            lvContainers.EditIndex = -1;
            lvContainers.InsertItemPosition = InsertItemPosition.None;
            q = (BusinessObjects.Quote) ViewState["BeforeEdit"];
            bindControls();
            ViewState["Quote"] = q;
        }
        protected void lvContainers_ItemUpdating(object sender, ListViewUpdateEventArgs e)
        {
            ContainerInfo c = (ContainerInfo)lvContainers.EditItem.FindControl("ContainerInfo1");
            q.Containers[lvContainers.EditIndex] = c.ContainerObject;

            lvContainers.EditIndex = -1;
            bindControls();
            ViewState["Quote"] = q;
            ViewState["BeforeEdit"] = null;
        }
        protected void lvContainers_ItemInserting(object sender, ListViewInsertEventArgs e)
        {
            ContainerInfo c = (ContainerInfo)lvContainers.InsertItem.FindControl("ContainerInfo2");
            BusinessObjects.Container container = c.ContainerObject;

            q.Containers.Add(container);
            lvContainers.DataSource = q.Containers;
            lvContainers.InsertItemPosition = InsertItemPosition.None;
            lvContainers.DataBind();
            ViewState["Quote"] = q;
        }
        protected void lvContainers_ItemDeleting(object sender, ListViewDeleteEventArgs e)
        {
            q.Containers.RemoveAt(e.ItemIndex);
            bindgrid();
            ViewState["Quote"] = q;
        }
        public void ContainerCargo_Saving(System.Collections.Hashtable rcd)
        {
            bool insrt = false; int id=-1;
            BusinessObjects.Container c;
            c= q.Containers[lvContainers.EditIndex];
            BusinessObjects.ContainerCargo edit = new BusinessObjects.ContainerCargo();
                
            if(int.TryParse(rcd["ID"].ToString(),out id))
            {
                edit=(from crg in c.Cargo where crg.ID == int.Parse(rcd["ID"].ToString()) select crg).FirstOrDefault();
                if (edit == null) {insrt = true; }
            }else{insrt = true;}

            edit.ID = id;
            int i; double d; bool b;
            edit.NetWeight = double.TryParse(rcd["NetWeight"].ToString(), out d) ? d : 0;
            i = 1; //int.TryParse(row["NetWeightUnit"].ToString(), out i);
            edit.NetWeightUnit = (BusinessObjects.Units.WeightUnits)i;
            i = 0; int.TryParse(rcd["Pieces"].ToString(), out i);
            edit.Pieces = i;
            edit.UOM = rcd["UOM"].ToString();
            edit.Description = rcd["Description"].ToString();
            d = 0; double.TryParse(rcd["GrossWeight"].ToString(), out d);
            edit.GrossWeight = d;
            i = 1; //int.TryParse(row["GrossWeightUnit"].ToString(), out i);
            edit.GrossWeightUnit = (BusinessObjects.Units.WeightUnits)i;
            edit.Hazardous = bool.TryParse(rcd["Hazardous"].ToString(), out b) ? b : false;
            if (insrt) { c.Cargo.Add(edit); }

            Session["Quote"] = q;
            ViewState["Quote"] = q;
            lvContainers.DataSource = q.Containers;
            lvContainers.DataBind();
        }
        public void ContainerInfo_Deleting(System.Collections.Hashtable rcd)
        {
            var edit = (from crg in q.Containers[lvContainers.EditIndex].Cargo where crg.ID == int.Parse(rcd["ID"].ToString()) select crg).FirstOrDefault();
            q.Containers[lvContainers.EditIndex].Cargo.Remove(edit);

            Session["Quote"] = q;
            ViewState["Quote"] = q;
            lvContainers.DataSource = q.Containers;
            lvContainers.DataBind();
        }

#endregion


    }
}