<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ContainerInfo.ascx.cs" Inherits="CompassNet.Modules.ContainerInfo" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<%@ Register Assembly="obout_Grid_NET" Namespace="Obout.Grid" TagPrefix="cc2" %>
<%@ Register Assembly="obout_Calendar2_Net" Namespace="OboutInc.Calendar2" TagPrefix="obout" %>
<%@ Register Assembly="obout_Interface" Namespace="Obout.Interface" TagPrefix="cc1" %>
<%@ Register src="RateFinder.ascx" tagname="RateFinder" tagprefix="uc2" %>
<link href="../Styles/Site.css" rel="stylesheet" type="text/css" />
<link href="../Styles/ui-lightness/jquery.ui.dialog.css" rel="stylesheet" type="text/css" />
<link href="../Styles/ui-lightness/jquery.ui.all.css" rel="stylesheet" type="text/css" />
<link href="../Styles/ui-lightness/jquery-ui.css" rel="stylesheet" type="text/css" />
<script src="../Scripts/jquery-1.8.2.min.js" type="text/javascript"></script>
<script src="../Scripts/ui/external/jquery.bgiframe-2.1.2.js" type="text/javascript"></script>
<script src="../Scripts/ui/minified/jquery.ui.core.min.js" type="text/javascript"></script>
<script src="../Scripts/ui/minified/jquery.ui.widget.min.js" type="text/javascript"></script>
<script src="../Scripts/ui/minified/jquery.ui.mouse.min.js" type="text/javascript"></script>
<script src="../Scripts/ui/minified/jquery.ui.draggable.min.js" type="text/javascript"></script>
<script src="../Scripts/ui/minified/jquery.ui.position.min.js" type="text/javascript"></script>
<script src="../Scripts/ui/minified/jquery.ui.resizable.min.js" type="text/javascript"></script>
<script src="../Scripts/ui/minified/jquery.ui.dialog.min.js" type="text/javascript"></script>
<script src="../Scripts/QuoteEntry.js" type="text/javascript"></script>

<script type="text/javascript">
</script>
<div id="container" style="width:780px;overflow:hidden;border-width:medium;border-color:Lime">
<div class="guirow" style="overflow:hidden">
    <asp:HiddenField ID="hdContainerID" runat="server" Value='<%#Eval("ID") %>' />
    <div class="guicell"><div>Container No.</div><cc1:OboutTextBox ID="txtContainerNo" runat="server" Width="100" Text='<%#Eval("ContainerNumber") %>'></cc1:OboutTextBox></div>
    <div class="guicell"><div>Container Type:</div><cc1:OboutTextBox ID="txtContainerType" runat="server" Text='<%#Eval("ContainerType") %>' Width="100"></cc1:OboutTextBox></div>
    <div class="guicell"><div>Seal #1</div><cc1:OboutTextBox ID="txtSeal1" runat="server" Text='<%#Eval("Seal1") %>' Width="100"></cc1:OboutTextBox></div>
    
    <div class="guicell"><div>Seal #2</div><cc1:OboutTextBox ID="txtSeal2" runat="server" Text='<%#Eval("Seal2") %>' Width="100"></cc1:OboutTextBox>
    </div>
    <div class="guicell"><div>Seal #3</div><cc1:OboutTextBox ID="txtSeal3" runat="server" Text='<%#Eval("Seal3") %>' Width="100"></cc1:OboutTextBox></div>
    <div class="guicell"><div style="display:inline-block;"><div>Tare Weight</div>        
        <cc1:OboutTextBox ID="txtTareWeight" runat="server" Text='<%#Eval("TareWeight") %>' Width="100"></cc1:OboutTextBox></div>
        <div style="display:inline-block;font-size:xx-small;position:relative;top:10px">
            <asp:RadioButtonList ID="rbTareWeightUnit" runat="server" CellPadding="0" 
                CellSpacing="0" RepeatLayout="Flow">
            <asp:ListItem>Kgs</asp:ListItem>
            <asp:ListItem>Lbs</asp:ListItem>
        </asp:RadioButtonList></div>
     </div>
</div>
<div class="guirow" style="overflow:hidden">
    <div class="guicell"><div>Discharge Date</div><asp:TextBox ID="txtDischargeDate" runat="server" Width="100" Text='<%#CompassNet.Functions.Tools.FormatShortDate(Eval("DischargeDate"))%>'></asp:TextBox>
        <obout:Calendar ID="calDischarge" runat="server" StyleFolder="../Styles/CalendarStyles/blue"
                                     DatePickerMode="true" TextBoxId = "txtDischargeDate" DatePickerImagePath = "../Styles/CalendarStyles/icon2.gif"></obout:Calendar>
    </div>
    <div class="guicell"><div>Pickupdate</div><asp:TextBox ID="txtPickupDate" runat="server" Width="100" Text='<%#CompassNet.Functions.Tools.FormatShortDate(Eval("PickupDate")) %>'></asp:TextBox>
        <obout:Calendar ID="Calendar2" runat="server" StyleFolder="../Styles/CalendarStyles/blue"
                                     DatePickerMode="true" TextBoxId = "txtPickupDate" DatePickerImagePath = "../Styles/CalendarStyles/icon2.gif"></obout:Calendar>
    </div>
    <div class="guicell"><div>Customer Pickup</div>
        <asp:TextBox ID="txtCustPickupLastFree" runat="server" Width="100" 
            Text='<%# CompassNet.Functions.Tools.FormatShortDate(Eval("CustomerPickupLastDate"))%>'></asp:TextBox>
        <obout:Calendar ID="Calendar3" runat="server" StyleFolder="../Styles/CalendarStyles/blue"
                                     DatePickerMode="true" TextBoxId = "txtCustPickupLastFree" DatePickerImagePath = "../Styles/CalendarStyles/icon2.gif"></obout:Calendar>
    </div>
    <div class="guicell"><div>Customer Return</div>
        <asp:TextBox ID="txtCustReturnLastFree" runat="server" Width="100" 
            Text='<%#CompassNet.Functions.Tools.FormatShortDate(Eval("CustomerReturnLastDate")) %>'></asp:TextBox>
        <obout:Calendar ID="Calendar4" runat="server" StyleFolder="../Styles/CalendarStyles/blue"
                                     DatePickerMode="true" TextBoxId = "txtCustReturnLastFree" DatePickerImagePath = "../Styles/CalendarStyles/icon2.gif"></obout:Calendar>
    </div>
    <div class="guicell"><div>Return Date</div><asp:TextBox ID="txtReturnDate" 
            runat="server" Width="100" Text='<%#CompassNet.Functions.Tools.FormatShortDate(Eval("ReturnDate")) %>'></asp:TextBox>
        <obout:Calendar ID="Calendar5" runat="server" StyleFolder="../Styles/CalendarStyles/blue"
                                     DatePickerMode="true" TextBoxId = "txtReturnDate" DatePickerImagePath = "../Styles/CalendarStyles/icon2.gif"></obout:Calendar>
    </div>
</div>
<div class="guirow" style="overflow:hidden">
    <div class="guicell"><div>Carrier Pickup</div>
        <asp:TextBox ID="txtCarrierPickup" runat="server" Width="100" 
            Text='<%#CompassNet.Functions.Tools.FormatShortDate(Eval("CarrierPickUpLastDate")) %>'></asp:TextBox>
        <obout:Calendar ID="Calendar6" runat="server" StyleFolder="../Styles/CalendarStyles/blue"
                                     DatePickerMode="true" TextBoxId = "txtCarrierPickup" DatePickerImagePath = "../Styles/CalendarStyles/icon2.gif"></obout:Calendar>
    </div>
    <div class="guicell"><div>Carrier Return</div>
        <asp:TextBox ID="txtCarrierReturn" runat="server" Width="100" 
            Text='<%#CompassNet.Functions.Tools.FormatShortDate(Eval("CarrierReturnLastDate")) %>'></asp:TextBox>
        <obout:Calendar ID="Calendar7" runat="server" StyleFolder="../Styles/CalendarStyles/blue"
                                     DatePickerMode="true" TextBoxId = "txtCarrierReturn" DatePickerImagePath = "../Styles/CalendarStyles/icon2.gif"></obout:Calendar>
    </div>
    <div class="guicell"><div>Rates</div><asp:TextBox ID="txtRates" runat="server" Width="200" Text='<%#Eval("Rate") %>'></asp:TextBox>
        <cc1:OboutButton ID="btnRates" runat="server" OnClientClick="GoToRates();return false;" Text="Get Rates"></cc1:OboutButton>
    <div id="dialog" title="Basic dialog" class="dlg" style="max-height:500px;overflow:auto;display:none">     
             <asp:Panel ID="pnlRates" runat="server" style="background-color:White" EnableViewState="False">
                 <uc2:RateFinder ID="RateFinder1" runat="server" ClientIDMode="AutoID" EnableViewState="False" />
             </asp:Panel>
    </div>
    </div>   
</div>
<div class="guirow" style="overflow:hidden">
    <cc2:Grid ID="grCargo" runat="server" AutoGenerateColumns="False" DataSource='<%#((List<CompassNet.BusinessObjects.ContainerCargo>)Eval("Cargo")) %>' 
        ShowFooter="False" ViewStateMode="Enabled"
        ondeletecommand="grCargo_DeleteCommand" oninsertcommand="grCargo_InsertCommand" 
        onupdatecommand="grCargo_UpdateCommand" EnableTypeValidation="False">
        <AddEditDeleteSettings NewRecordPosition="Bottom" AddLinksPosition="Top" />
        <Columns>
            <cc2:Column AllowDelete="True" AllowEdit="True" Index="0" Width="65">
				    <TemplateSettings TemplateId="editBtnTemplate" EditTemplateId="updateBtnTemplate" />
            </cc2:Column>
            <cc2:Column DataField="Description" HeaderText="Description" Index="1" 
                Width="180">
            </cc2:Column>
            <cc2:Column DataField="Pieces" HeaderText="Pieces" Index="2" Width="75">
            </cc2:Column>
            <cc2:Column DataField="UOM" HeaderText="UOM" Index="3" Width="100">
            </cc2:Column>
            <cc2:Column DataField="GrossWeight" HeaderText="GrossWeight" Index="4" 
                Width="100">
            </cc2:Column>
            <cc2:Column DataField="NetWeight" HeaderText="NetWeight" Index="5" Width="100">
            </cc2:Column>
            <cc2:Column DataField="Hazardous" HeaderText="Hazardous" Index="6" Width="80">
            </cc2:Column>
            <cc2:Column DataField="ID" HeaderText="ID" Index="7" Visible="False">
            </cc2:Column>
        </Columns>
		<TemplateSettings NewRecord_TemplateId="addTemplate" NewRecord_EditTemplateId="saveTemplate" />
        <ScrollingSettings ScrollWidth="750px" />
		<Templates>								
				<cc2:GridTemplate runat="server" ID="editBtnTemplate">
                    <Template>
                        <img src="../Styles/images/edit.png" height="16" onclick="grCargo.edit_record(this)" alt="" title="Edit" class="icon"/>
                        
                        <img src="../Styles/images/delete.png" height="16" onclick="grCargo.delete_record(this)" alt="" title="Delete" class="icon"/>
                    </Template>
                </cc2:GridTemplate>
                <cc2:GridTemplate runat="server" ID="updateBtnTemplate">
                    <Template>
                        <img src="../Styles/images/update.png" height="16"  onclick="grCargo.update_record(this)" alt="" title="Save" class="icon"/> 
                        
                        <img src="../Styles/images/undo.png" height="16" onclick="grCargo.cancel_edit(this)" alt="" title="Cancel" class="icon"/> 
                    </Template>
                </cc2:GridTemplate>
                <cc2:GridTemplate runat="server" ID="addTemplate">
                    <Template>
                        <img src="../Styles/images/add.png" height="16" onclick="grCargo.addRecord()" alt="" title="Add New" class="icon"/>
                    </Template>
                </cc2:GridTemplate>
                 <cc2:GridTemplate runat="server" ID="saveTemplate">
                    <Template>
                        <img src="../Styles/images/update.png" height="16" onclick="grCargo.insertRecord()" alt="" title="Save" class="icon"/>
                        
                        <img src="../Styles/images/undo.png" height="16" onclick="grCargo.cancelNewRecord()" alt="" title="Cancel" class="icon"/>
                    </Template>
                </cc2:GridTemplate>				
			</Templates>
    </cc2:Grid>
</div>

</div>
