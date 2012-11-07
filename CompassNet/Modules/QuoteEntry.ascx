<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="QuoteEntry.ascx.cs" Inherits="CompassNet.Modules.QuoteEntry" %>
<%@ Register Assembly="obout_AJAXPage" Namespace="OboutInc" TagPrefix="oajax" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<%@ Register src="ContainerInfo.ascx" tagname="ContainerInfo" tagprefix="uc1" %>
<%@ Register assembly="obout_ComboBox" namespace="Obout.ComboBox" tagprefix="cc1" %>
<%@ Register Assembly="obout_Interface" Namespace="Obout.Interface" TagPrefix="cc3" %>
<%@ Register assembly="obout_SuperForm" namespace="Obout.SuperForm" tagprefix="cc2" %>
<%@ Register assembly="obout_Calendar2_Net" namespace="OboutInc.Calendar2" tagprefix="obout" %>
<%@ Register assembly="obout_Grid_NET" namespace="Obout.Grid" tagprefix="cc4" %>
<link href="../Styles/Site.css" rel="stylesheet" type="text/css" />
<%--<script src="../Scripts/QuoteEntry.js" type="text/javascript"></script>--%>
<asp:SqlDataSource ID="GetStates" runat="server" 
    ConnectionString="<%$ ConnectionStrings:CompassForwardingConnectionString %>" 
    SelectCommand="SELECT DISTINCT [StateCode], [State] FROM [States] ORDER BY [State]">
</asp:SqlDataSource>
<asp:SqlDataSource ID="GetServiceTypes" runat="server" 
    ConnectionString="<%$ ConnectionStrings:CompassForwardingConnectionString %>" 
    SelectCommand="SELECT ID, ServiceID, ServiceType FROM ServiceTypes">
</asp:SqlDataSource>
<asp:SqlDataSource ID="GetCountries" runat="server" 
    ConnectionString="<%$ ConnectionStrings:CompassForwardingConnectionString %>" 
    SelectCommand="SELECT DISTINCT [CountryCode],[CountryName] FROM [Countries] ORDER BY [CountryCode]">
</asp:SqlDataSource>
<asp:SqlDataSource ID="GetSalesReps" runat="server" 
    ConnectionString="<%$ ConnectionStrings:CompassForwardingConnectionString %>" 
    SelectCommand="SELECT ID, Code, NAME   FROM SalesReps where Active=1">
</asp:SqlDataSource>
<p>
<script type="text/javascript">
    function GoBack() {
        history.back(1);
    }
    var updateinfo = false;
    var lastitem = "";
    function beforeChange() {
        lastitem = cbxCustomer._dropDownList._value;
        // l;
    }
   function callUpdateInfo() {
      // p;
       if (updateinfo || lastitem != cbxCustomer._dropDownList._value) {
           if (confirm('Load client info?')) {
               ob_post.post(null, "PanelUpdate", function () {
                   ob_post.UpdatePanel('ajaxPanelContactInfo');
               });
               alert('info updated');
           }
       }
       lastitem = cbxCustomer._dropDownList._value;
   }
</script>
<div id="divContainer" class="popUpContainer">
            <div class="header">
                <h3 style="color: #f9f9f9;">
            Quote No:<asp:Label ID="lblQuoteNO" Text="New Quote" runat="server" />
                </h3>
            </div>
<!-- Text entry fields-->
    <asp:UpdatePanel ID="upnlQuoteEntry" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
        <div>
            <div class="guirow">
                <div class="guicell"><div>Customer:</div>  
                <div style="display:inline-block">
                    <cc1:ComboBox ID="cbxCustomer" runat="server" 
                        MenuWidth="400px" Width="150px" Height="200" DataTextField="NAME" DataValueField="ID"
                        FilterType="StartsWith" EnableLoadOnDemand="True" 
                        EnableVirtualScrolling="True" onloadingitems="cbxCustomer_LoadingItems">
	                    <HeaderTemplate>
	                        <div class="header c2">Code</div>
	                        <div class="header c2">Name</div>
	                        <div class="header c3">Type</div>
	                    </HeaderTemplate>
	                    <ItemTemplate>
	                        <div class="item c2"><%# Eval("ID")%></div>
	                        <div class="item c2"><%# Eval("Name")%></div>
	                        <div class="item c3"><%# Eval("Type")%></div>
	                    </ItemTemplate>
	                    <ClientSideEvents OnOpen="beforeChange" OnClose="callUpdateInfo" />
	                    <FooterTemplate>
                            Displaying items <%# Container.ItemsCount > 0 ? "1" : "0" %>-<%# Container.ItemsLoadedCount %>out of <%# Container.ItemsCount %>.
                        </FooterTemplate>
                    </cc1:ComboBox>
                    </div></div>
                <oajax:CallbackPanel ID="ajaxPanelContactInfo" runat="server">
                <Content>
                <div class="guicell"><div>Contact</div>
                <div style="display:inline-block">
                    <cc3:OboutTextBox runat="server" ID="txtContact" CssClass="small" /></div></div>
                <div class="guicell"><div>Telephone</div>
                <div style="display:inline-block"><cc3:OboutTextBox runat="server" ID="txtTelephone" 
                        CssClass="small" /></div></div>
                <div class="guicell"><div>Fax</div>
                <div style="display:inline-block"><cc3:OboutTextBox runat="server" ID="txtFax" 
                        CssClass="small" /></div></div>
                </Content></oajax:CallbackPanel>
           </div>
            <div  class="guirow">
                <div class="guicell"><div>Customer Ref:</div>  
                <div style="display:inline-block">
                    <cc3:OboutTextBox runat="server" ID="txtCustRef" 
                        CssClass="small" Width="100px" /></div></div>
                <div class="guicell"><div>Pickup City</div>
                <div style="display:inline-block">
                <cc1:ComboBox ID="ddPickupCity" runat="server" 
                        MenuWidth="400px" Width="150px" Height="200" DataTextField="POR PORT_NAME" DataValueField="POR CODE"
                        FilterType="StartsWith" EnableLoadOnDemand="True" 
                        EnableVirtualScrolling="True" onloadingitems="ddPickupCity_LoadingItems" >
	                    <HeaderTemplate>
	                        <div class="header c2">Code</div>
	                        <div class="header c3">Name ID</div>
	                    </HeaderTemplate>
	                    <ItemTemplate>
	                        <div class="item c2"><%# Eval("POR CODE")%></div>
	                        <div class="item c3"><%# Eval("POR PORT_NAME")%></div>
	                    </ItemTemplate>
	                    <FooterTemplate>
                            Displaying items <%# Container.ItemsCount > 0 ? "1" : "0" %>-<%# Container.ItemsLoadedCount %>out of <%# Container.ItemsCount %>.
                        </FooterTemplate>
                    </cc1:ComboBox>
                </div></div>
                <div class="guicell"><div>State</div>
                <div style="display:inline-block">
                    <cc1:ComboBox ID="ddcustState" runat="server" DataSourceID="GetStates" DataTextField="StateCode" DataValueField="StateCode" 
                                    MenuWidth="250px" Width="50px" FilterType="StartsWith">
	                    <HeaderTemplate>
	                        <div class="header c1">Code</div>
	                        <div class="header c2">State</div>
	                    </HeaderTemplate>
	                    <ItemTemplate>
	                        <div class="item c1"><%# Eval("StateCode")%></div>
	                        <div class="item c2"><%# Eval("State")%></div>
	                    </ItemTemplate>
	                    <FooterTemplate>
                            Displaying items <%# Container.ItemsCount > 0 ? "1" : "0" %>-<%# Container.ItemsLoadedCount %>out of <%# Container.ItemsCount %>.
                        </FooterTemplate>
                    </cc1:ComboBox>                                                                                                        
                    </div></div>
                <div class="guicell"><div>Zip</div>
                <div style="display:inline-block">
                    <cc3:OboutTextBox runat="server" ID="txtcustZip" 
                        CssClass="small" Width="100px" /></div></div>
                <div class="guicell"><div>Port of Load</div>
                <div style="display:inline-block">
                <cc1:ComboBox ID="ddPortofLoad" runat="server" 
                        MenuWidth="400px" Width="150px" Height="200" DataTextField="POR PORT_NAME" DataValueField="POR CODE"
                        FilterType="StartsWith" EnableLoadOnDemand="True" 
                        EnableVirtualScrolling="True" onloadingitems="ddPortofLoad_LoadingItems">
	                    <HeaderTemplate>
	                        <div class="header c2">Code</div>
	                        <div class="header c3">Name ID</div>
	                    </HeaderTemplate>
	                    <ItemTemplate>
	                        <div class="item c2"><%# Eval("POR CODE")%></div>
	                        <div class="item c3"><%# Eval("POR PORT_NAME")%></div>
	                    </ItemTemplate>
	                    <FooterTemplate>
                            Displaying items <%# Container.ItemsCount > 0 ? "1" : "0" %>-<%# Container.ItemsLoadedCount %>out of <%# Container.ItemsCount %>.
                        </FooterTemplate>
                    </cc1:ComboBox>
                </div></div>
                <div class="guicell"><div>Port of Dispatch</div>
                <div style="display:inline-block">
                <cc1:ComboBox ID="ddPortOfDispatch" runat="server" 
                        MenuWidth="400px" Width="150px" Height="200" DataTextField="POR PORT_NAME" DataValueField="POR CODE"
                        FilterType="StartsWith" EnableLoadOnDemand="True" 
                        EnableVirtualScrolling="True" 
                        onloadingitems="ddPortOfDispatch_LoadingItems">
	                    <HeaderTemplate>
	                        <div class="header c2">Code</div>
	                        <div class="header c3">Name ID</div>
	                    </HeaderTemplate>
	                    <ItemTemplate>
	                        <div class="item c2"><%# Eval("POR CODE")%></div>
	                        <div class="item c3"><%# Eval("POR PORT_NAME")%></div>
	                    </ItemTemplate>
	                    <FooterTemplate>
                            Displaying items <%# Container.ItemsCount > 0 ? "1" : "0" %>-<%# Container.ItemsLoadedCount %>out of <%# Container.ItemsCount %>.
                        </FooterTemplate>
                    </cc1:ComboBox>
                </div></div>
           </div>
           <div  class="guirow">
               <div class="guicell"><div>Final Dest:</div>
                <div style="display:inline-block">
                <cc1:ComboBox ID="ddFinalDestination" runat="server" 
                        MenuWidth="400px" Width="150px" Height="200" DataTextField="POR PORT_NAME" DataValueField="POR CODE"
                        FilterType="StartsWith" EnableLoadOnDemand="True" 
                        EnableVirtualScrolling="True" onloadingitems="ddFinalDestination_LoadingItems">
	                    <HeaderTemplate>
	                        <div class="header c2">Code</div>
	                        <div class="header c3">Name ID</div>
	                    </HeaderTemplate>
	                    <ItemTemplate>
	                        <div class="item c2"><%# Eval("POR CODE")%></div>
	                        <div class="item c3"><%# Eval("POR PORT_NAME")%></div>
	                    </ItemTemplate>
	                    <FooterTemplate>
                            Displaying items <%# Container.ItemsCount > 0 ? "1" : "0" %>-<%# Container.ItemsLoadedCount %>out of <%# Container.ItemsCount %>.
                        </FooterTemplate>
                    </cc1:ComboBox></div></div>
                <div class="guicell"><div>Zip</div>
                <div style="display:inline-block">
                    <cc3:OboutTextBox runat="server" ID="txtDestZip" 
                        CssClass="small" Width="100px" /></div></div>
                <div class="guicell"><div>Country</div>
                <div style="display:inline-block">
                    <cc1:ComboBox ID="ddDestCountry" runat="server" DataSourceID="GetCountries" DataTextField="CountryCode" DataValueField="CountryName" 
                                    MenuWidth="250px" Width="50px" FilterType="StartsWith">
	                    <HeaderTemplate>
	                        <div class="header c1">ID</div>
	                        <div class="header c2">Country Name</div>
	                    </HeaderTemplate>
	                    <ItemTemplate>
	                        <div class="item c1"><%# Eval("CountryCode")%></div>
	                        <div class="item c2"><%# Eval("CountryName")%></div>
	                    </ItemTemplate>
	                    <FooterTemplate>
                            Displaying items <%# Container.ItemsCount > 0 ? "1" : "0" %>-<%# Container.ItemsLoadedCount %>out of <%# Container.ItemsCount %>.
                        </FooterTemplate>
                    </cc1:ComboBox>                                                                                                        
                    </div></div>
                <div class="guicell"><div>Type</div>
                <div style="display:inline-block">
                    <cc1:ComboBox ID="ddTypeOfMove" runat="server" 
                        DataTextField="ServiceType"  
                        DataValueField="ServiceID" MenuWidth="400px" Width="150px" 
                        FilterType="StartsWith" EnableLoadOnDemand="True" EnableVirtualScrolling="True" 
                        onloadingitems="ddTypeOfMove_LoadingItems">
	                    <HeaderTemplate>
	                        <div class="header c1">ID</div>
	                        <div class="header c2">Service ID</div>
	                        <div class="header c3">Service</div>
	                    </HeaderTemplate>
	                    <ItemTemplate>
	                        <div class="item c1"><%# Eval("ID") %></div>
	                        <div class="item c2"><%# Eval("ServiceID")%></div>
	                        <div class="item c3"><%# Eval("ServiceType")%></div>
	                    </ItemTemplate>
	                    <FooterTemplate>
                            Displaying items <%# Container.ItemsCount > 0 ? "1" : "0" %>-<%# Container.ItemsLoadedCount %>out of <%# Container.ItemsCount %>.
                        </FooterTemplate>
                    </cc1:ComboBox>                                                                                                        
                                                    </div></div>
                <div class="guicell"><div>Quote No.</div>
                <div style="display:inline-block">
                    <cc3:OboutTextBox CssClass="small" runat="server" 
                        ID="txtQuoteNo" Width="100px" /></div></div>
                <div class="guicell"><div>Division</div>
                <div style="display:inline-block"><asp:DropDownList runat="server" ID="ddDivision"></asp:DropDownList></div></div>
                <div class="guicell"><div>Business Line</div>
                <div style="display:inline-block"><asp:DropDownList runat="server" ID="ddBusinessLine"></asp:DropDownList></div></div>
           </div>
           <div  class="guirow">
                <div class="guicell"><div>INCO Terms</div>
                <div style="display:inline-block">
                    <cc3:OboutTextBox runat="server" ID="txtINCOTerms" 
                        CssClass="small" Width="100px" /></div></div>
                <div class="guicell"><div>INCO Terms Location</div>
                <div style="display:inline-block">
                    <cc3:OboutTextBox runat="server" ID="txtINCOLocation" 
                        CssClass="small" style="top: 0px; left: 0px; width: 108px" Width="120px" /></div></div>
                <div class="guicell"><div>Effective Date</div>
                <div style="display:inline-block">
                    <cc3:OboutTextBox runat="server" ID="txtEffextiveDate" style="bottom:-100" Width="100px" />
                    <obout:Calendar ID="CalendarEffectivedate" runat="server" StyleFolder="../Styles/CalendarStyles/blue" 
                    DatePickerMode="true" TextBoxId = "txtEffextiveDate" DatePickerImagePath = "../Styles/CalendarStyles/icon2.gif"></obout:Calendar>
                    </div></div>
                <div class="guicell"><div>Expiration Date</div>
                <div style="display:inline-block;">
                    <cc3:OboutTextBox runat="server" ID="txtExpirationDate" CssClass="small" Width="100px" />
                    <obout:Calendar ID="CalendarExpirationDate" runat="server" StyleFolder="../Styles/CalendarStyles/blue"
                                     DatePickerMode="true" TextBoxId = "txtExpirationDate" DatePickerImagePath = "../Styles/CalendarStyles/icon2.gif"></obout:Calendar>
                    </div></div>
               <div class="guicell"><div>Quoted By</div>
                <div style="display:inline-block">
                    <cc3:OboutTextBox runat="server" ID="txtQuotedBy" 
                        CssClass="small" Width="100px" /></div></div>
                <div class="guicell"><div>Sales Rep</div>
                <div style="display:inline-block">
                    <cc1:ComboBox ID="ddSalesRep" runat="server" DataSourceID="GetSalesReps"
                        DataTextField="Name"  
                        DataValueField="Code" MenuWidth="400px" Width="150px" 
                        FilterType="StartsWith">
	                    <HeaderTemplate>
	                        <div class="header c1">ID</div>
	                        <div class="header c2">Code</div>
	                        <div class="header c3">Name</div>
	                    </HeaderTemplate>
	                    <ItemTemplate>
	                        <div class="item c1"><%# Eval("ID") %></div>
	                        <div class="item c2"><%# Eval("Code")%></div>
	                        <div class="item c3"><%# Eval("Name")%></div>
	                    </ItemTemplate>
                    </cc1:ComboBox>                                                                                                        
                </div></div>
           </div>
           <div  class="guirow">
                <div class="guicell"><div>Preferred Carrier</div>
                <div style="display:inline-block"><asp:DropDownList runat="server" ID="ddPreferredCarrier"></asp:DropDownList></div></div>
                <div class="guicell"><div>Voyage</div>
                <div style="display:inline-block"><cc3:OboutTextBox runat="server" ID="txtVoyage" /></div></div>
                <div class="guicell"><div>Vessel</div>
                <div style="display:inline-block"><asp:DropDownList runat="server" ID="ddVessel"></asp:DropDownList></div></div>
                <div class="guicell"><div>Container Handling</div>
                <div style="display:inline-block"><asp:DropDownList runat="server" ID="ddContainerHandling"></asp:DropDownList></div></div>
           </div>
        </div>
<%--                        <div> 
            <cc4:Grid ID="grContainers" runat="server" AutoGenerateColumns="False">
                <Columns>
                    <cc4:Column DataField="ContainerNumber" HeaderText="Container No" Index="0" Width="150">
				    <TemplateSettings RowEditTemplateControlId="ContainerInfo2" RowEditTemplateControlPropertyName="ContainerNumber"/>
                    </cc4:Column>
                    <cc4:Column DataField="ContainerType" HeaderText="Type" Index="1" Width="150">
				    <TemplateSettings RowEditTemplateControlId="ContainerInfo2" RowEditTemplateControlPropertyName="ContainerType"/>
                    </cc4:Column>
                    <cc4:Column DataField="Seal1" HeaderText="Seal 1" Index="2" Width="100">
				    <TemplateSettings RowEditTemplateControlId="ContainerInfo2" RowEditTemplateControlPropertyName="Seal1"/>
                    </cc4:Column>
                    <cc4:Column DataField="Seal2" HeaderText="Seal 2" Index="3" Width="100">
				    <TemplateSettings RowEditTemplateControlId="ContainerInfo2" RowEditTemplateControlPropertyName="Seal2"/>
                    </cc4:Column>
                    <cc4:Column DataField="Seal3" HeaderText="Seal 3" Index="4" Width="100">
				    <TemplateSettings RowEditTemplateControlId="ContainerInfo2" RowEditTemplateControlPropertyName="Seal3"/>
                    </cc4:Column>
                    <cc4:Column DataField="TareWeight" HeaderText="TareWeight" Index="5" Width="120">
				    <TemplateSettings RowEditTemplateControlId="ContainerInfo2" RowEditTemplateControlPropertyName="TareWeight"/>
                    </cc4:Column>
                    <cc4:Column AllowDelete="True" AllowEdit="True" HeaderText="EDIT" Index="6" 
                        Width="130">
                    </cc4:Column>
                </Columns>
                <TemplateSettings RowEditTemplateId="tplRowEdit" />
			    <ScrollingSettings ScrollHeight="250px" ScrollWidth="850px" />
			    <Templates> 			    
			    <cc4:GridTemplate runat="server" ID="tplRowEdit">
                    <Template>
                        <uc1:ContainerInfo ID="ContainerInfo2" runat="server" />           
                    </Template>
                    </cc4:GridTemplate>
                </Templates>
            </cc4:Grid>
            </div>
--%>
        </ContentTemplate>
     </asp:UpdatePanel>
            <hr />
            <h2>Containers</h2>
     <asp:UpdatePanel ID="upnlContainers" runat="server" UpdateMode="Conditional">
         <ContentTemplate>
        <div style="clear:both;max-height:350px;overflow:auto">
            <asp:ListView ID="lvContainers" runat="server"
                onitemcanceling="lvContainers_ItemCanceling" 
                onitemediting="lvContainers_ItemEditing" 
                onitemupdating="lvContainers_ItemUpdating" 
                oniteminserting="lvContainers_ItemInserting" 
                onitemdeleting="lvContainers_ItemDeleting">
            <LayoutTemplate>
            <table cellpadding="2" width="640px" border="1" runat="server" id="tbContainers">
            <tr id="Tr1" runat="server">
              <th id="Th1" runat="server">Action</th>
              <th id="Th2" runat="server">Container No</th>
              <th id="Th3" runat="server">Container Type</th>
              <th id="Th4" runat="server">Seal #1</th>
              <th id="Th5" runat="server">Seal #2</th>
              <th id="Th6" runat="server">Seal #3</th>
              <th id="Th7" runat="server">TareWeight</th>
            </tr>
            <tr runat="server" id="itemPlaceholder" />
            <tfoot><tr><td colspan="7">
                <asp:LinkButton ID="CreateButton" Text="Add" runat="server" OnClick="AddContainer_Click" ClientIDMode="AutoID" CommandName="Create">
                <img src="../Styles/images/add.png" alt="delete group" /></asp:LinkButton>
                </td></tr></tfoot>
            </table>
            </LayoutTemplate>
          <ItemTemplate>
          <tr id="Tr2" runat="server">
            <td>
              <asp:LinkButton ID="EditButton" CssClass="nooutline" runat="Server" Text="Edit" ClientIDMode="AutoID" CommandName="Edit" >
              <img src="../Styles/images/edit.png" alt="delete group" /></asp:LinkButton>
              <asp:LinkButton ID="DeleteButton" CssClass="nooutline" runat="Server" Text="Delete" OnClientClick="return confirmDelete()" ClientIDMode="AutoID" CommandName="Delete" >
              <img src="../Styles/images/delete.png" alt="delete group" /></asp:LinkButton>
            </td>
            <td><asp:Label ID="containeno" runat="Server" Text='<%#Eval("ContainerNumber") %>' /></td>
            <td valign="top"><asp:Label ID="containertpe" runat="Server" Text='<%#Eval("ContainerType") %>' /></td>
            <td><asp:Label ID="seal1" runat="Server" Text='<%#Eval("Seal1") %>' /></td>
            <td valign="top"><asp:Label ID="seal2" runat="Server" Text='<%#Eval("Seal2") %>' /></td>
            <td><asp:Label ID="seal3" runat="Server" Text='<%#Eval("Seal3") %>' /></td>
            <td valign="top"><asp:Label ID="TareWeight" runat="Server" Text='<%#Eval("TareWeight") %>' /></td>
          </tr>
        </ItemTemplate>
        <EditItemTemplate><tr><td><div>
            <asp:LinkButton ID="UpdateButton" ClientIDMode="AutoID" runat="server" CommandName="Update" Text="Update" >
            <img src="../Styles/images/update.png" alt="delete group" /></asp:LinkButton>
            <br />
            <asp:LinkButton ID="CancelButton" ClientIDMode="AutoID" runat="server" CommandName="Cancel" Text="Cancel">
            <img src="../Styles/images/undo.png" alt="delete group" /></asp:LinkButton>
            </div></td>
            <td colspan="6"><uc1:ContainerInfo ID="ContainerInfo1" ContainerObject="<%# ((CompassNet.BusinessObjects.Container)Container.DataItem)%>" runat="server" ClientIDMode="AutoID" /></td></tr>
         </EditItemTemplate>
         <InsertItemTemplate>"
            <tr><td><div>
            <asp:LinkButton ID="UpdateButton" ClientIDMode="AutoID" runat="server" CommandName="Insert" Text="Update" >
            <img src="../Styles/images/update.png" alt="delete group" /></asp:LinkButton>
            <br />
            <asp:LinkButton ID="CancelButton" ClientIDMode="AutoID" runat="server" CommandName="Cancel" Text="Cancel">
            <img src="../Styles/images/undo.png" alt="delete group" /></asp:LinkButton>
            </div></td>
            <td colspan="6"><uc1:ContainerInfo ID="ContainerInfo2" runat="server" ClientIDMode="AutoID" ViewStateMode="Enabled"/></td></tr>         
         </InsertItemTemplate>
         </asp:ListView>          
            
        </div>
        
<div>
        <div style="text-align: center;">
            <asp:Button ID="btnSave" Text="Save" runat="server" onclick="btnSave_Click" />
            <asp:Button ID="btnCancel" Text="Cancel" runat="server" 
                 OnClientClick="GoBack()" />
        </div></div>
        </ContentTemplate>
    </asp:UpdatePanel>
            
</div>
