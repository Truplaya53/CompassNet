<%@ Page Language="C#" AutoEventWireup="true"  MasterPageFile="~/Site.master" CodeBehind="Quotations.aspx.cs" Inherits="CompassNet.Quotations" %>

<%@ Register src="Modules/QuoteEntry.ascx" tagname="QuoteEntry" tagprefix="uc1" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<%@ Register assembly="obout_ComboBox" namespace="Obout.ComboBox" tagprefix="cc1" %>
<%@ Register assembly="obout_Grid_NET" namespace="Obout.Grid" tagprefix="cc1" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <script type="text/javascript" src="Scripts/jquery-1.8.2.min.js"></script>
    <script type="text/javascript" src="Scripts/jquery-ui-1.8.24.custom.min.js"></script>
    <script type="text/javascript">
        function NewQuoteEntry(sender, records) {
            location.href = 'QuoteDetails.aspx?q=0';
            return false;
        }
        function GoToQuoteEntry(sender, records) {
            var orderId = sender.SelectedRecords[0].QuoteNumber;
            location.href = 'QuoteDetails.aspx?q=' + orderId;
        }

    </script>
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <h2>
        Quotes
    </h2>
    <div>
        <div>Search</div>
        <div style="display:inline">
                            <cc1:ComboBox ID="cbQuoteSearch" runat="server" 
                            MenuWidth="200px" Width="150px" Height="200"
                            FilterType="StartsWith" EnableLoadOnDemand="True" 
                            EnableVirtualScrolling="True" onloadingitems="cbQuoteSearch_LoadingItems">
                        </cc1:ComboBox>
        </div>
        <div style="display:inline">
            <asp:DropDownList ID="ddSearchType" runat="server">
                <asp:ListItem Text="Quote Number" />
                <asp:ListItem Text="Customer" />
                <asp:ListItem Text="Commodity" />
                <asp:ListItem Text="Port of Discharge" />
                <asp:ListItem Text="Customer Ref. No" />
                <asp:ListItem Text="Pickup City" />
                <asp:ListItem Text="Final Destination" />
         </asp:DropDownList>
         </div>
         <asp:Button ID="btnSearch" runat="server" onclick="btnSearch_Click" Text="Search" />
    </div>
    <div>    
        <div>
        <asp:Button ID="btnClearSearch" Visible="false" runat="server" Text="Clear Search" onclick="btnClearSearch_Click" />
        </div>
        <div style="overflow:auto;POSITION:relative;width:900px;height:500px">
            <cc1:Grid ID="GridView1" runat="server"  AutoGenerateColumns="false"
                ondeletecommand="GridView1_DeleteCommand" FolderStyle="Styles\GridStyles\style_11">
                <Columns>
                    <cc1:Column AllowDelete="True" Index="0" Width="50px">
				    <TemplateSettings TemplateId="editBtnTemplate" EditTemplateId="updateBtnTemplate" />
                    </cc1:Column>
                    <cc1:Column DataField="Customer" HeaderText="Customer"></cc1:Column>
                    <cc1:Column DataField="QuoteNumber" HeaderText="QuoteNumber"></cc1:Column>
                    <cc1:Column DataField="PickupCity" HeaderText="PickupCity"></cc1:Column>
                    <cc1:Column DataField="FinalDestination" HeaderText="FinalDestination"></cc1:Column>
                    <cc1:Column DataField="QuotedBy" HeaderText="QuotedBy"></cc1:Column>
                    <cc1:Column DataField="Effectivdate" HeaderText="Effectivdate"></cc1:Column>
                    <cc1:Column DataField="SalesRep" HeaderText="SalesRep"></cc1:Column>
               </Columns>
                <ScrollingSettings ScrollHeight="600px" ScrollWidth="890px" />
                <CssSettings CSSColumnHeader="" />
                <ClientSideEvents OnClientDblClick="GoToQuoteEntry"  OnBeforeClientDelete="confirmDelete" OnBeforeClientAdd="NewQuoteEntry" ExposeSender="true" />
		        <TemplateSettings NewRecord_TemplateId="addTemplate" NewRecord_EditTemplateId="saveTemplate" />
                <Templates>
				        <cc1:GridTemplate runat="server" ID="editBtnTemplate">
                            <Template>
                                <img src="../Styles/images/delete.png" height="16" onclick="GridView1.delete_record(this)" alt="" title="Delete" class="icon"/>
                            </Template>
                        </cc1:GridTemplate>
                        <cc1:GridTemplate runat="server" ID="addTemplate">
                            <Template>
                                <img src="../Styles/images/add.png" height="16" onclick="GridView1.addRecord()" alt="" title="Add New" class="icon"/>
                            </Template>
                        </cc1:GridTemplate>
                         <cc1:GridTemplate runat="server" ID="saveTemplate">
                            <Template>
                                <img src="../Styles/images/update.png" height="16" onclick="GridView1.insertRecord()" alt="" title="Save" class="icon"/>
                        
                                <img src="../Styles/images/undo.png" height="16" onclick="GridView1.cancelNewRecord()" alt="" title="Cancel" class="icon"/>
                            </Template>
                        </cc1:GridTemplate>				
            </Templates>
            </cc1:Grid>
        </div>
        <asp:GridView ID="GridView2" runat="server"></asp:GridView>
    </div>
    
</asp:Content>
