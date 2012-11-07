<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="RateFinder.ascx.cs" Inherits="CompassNet.Modules.RateFinder" %>
<%@ Register Assembly="obout_Calendar2_Net" Namespace="OboutInc.Calendar2" TagPrefix="obout" %>
<%@ Register assembly="obout_ComboBox" namespace="Obout.ComboBox" tagprefix="cc1" %>
<asp:SqlDataSource ID="GetContainerTypes" runat="server" 
        ConnectionString="<%$ ConnectionStrings:CompassForwardingConnectionString %>" 
        SelectCommand="SELECT * FROM [ContainerTypes]"></asp:SqlDataSource>
<asp:SqlDataSource ID="GetCommodities" runat="server" 
        ConnectionString="<%$ ConnectionStrings:CompassForwardingConnectionString %>" 
        SelectCommand="SELECT * FROM [CommodityTypes]"></asp:SqlDataSource>
<asp:SqlDataSource ID="GetContracts" runat="server" 
        ConnectionString="<%$ ConnectionStrings:CompassForwardingConnectionString %>" 
        
        SelectCommand="SELECT ShippingContracts.ContractID, Shippers.Name, ShippingContracts.ContractNumber FROM ShippingContracts INNER JOIN Shippers ON ShippingContracts.ContractID = Shippers.ShipperID"></asp:SqlDataSource>
<div><div style="display:inline-block;width:100px">Contract:</div>
    <cc1:ComboBox ID="ddContract" runat="server" DataSourceID="GetContracts" DataValueField="ContractID" DataTextField="Name" MenuWidth="400px">
	                    <HeaderTemplate>
	                        <div class="header c2">Shipper</div>
	                        <div class="header c2">Contract</div>
	                    </HeaderTemplate>
	                    <ItemTemplate>
	                        <div class="item c2"><%# Eval("Name")%></div>
	                        <div class="item c2"><%# Eval("ContractNumber")%></div>
	                    </ItemTemplate>
    </cc1:ComboBox>
</div>
<div><div style="display:inline-block;width:100px">Origin:</div>
    <cc1:ComboBox ID="ddOrigin" runat="server" DataTextField="Origin" MenuWidth="400px" Height="200"
        DataValueField="Code" EnableLoadOnDemand="True" EnableVirtualScrolling="True" 
        onloadingitems="ddOrigin_LoadingItems">
	                    <HeaderTemplate>
	                        <div class="header c2">Code</div>
	                        <div class="header c2">Origin</div>
	                    </HeaderTemplate>
	                    <ItemTemplate>
	                        <div class="item c2"><%# Eval("Code")%></div>
	                        <div class="item c2"><%# Eval("Origin")%></div>
	                    </ItemTemplate>
        </cc1:ComboBox>
</div>
<div><div style="display:inline-block;width:100px">Destination</div>
    <cc1:ComboBox ID="ddDestination" runat="server" DataTextField="Destination" MenuWidth="400px" 
        DataValueField="Code" EnableLoadOnDemand="True" EnableVirtualScrolling="True" Height="200"
        onloadingitems="ddDestination_LoadingItems">
	                    <HeaderTemplate>
	                        <div class="header c2">Code</div>
	                        <div class="header c2">Destination</div>
	                    </HeaderTemplate>
	                    <ItemTemplate>
	                        <div class="item c2"><%# Eval("Code")%></div>
	                        <div class="item c2"><%# Eval("Destination")%></div>
	                    </ItemTemplate>
        </cc1:ComboBox>
</div>
<div><div style="display:inline-block;width:100px">Container Type</div>
    <cc1:ComboBox ID="ddContainer" runat="server" DataSourceID="GetContainerTypes" DataTextField="Name" DataValueField="ID"></cc1:ComboBox>
</div>
<div><div style="display:inline-block;width:100px">Shipping Date</div>
    <asp:TextBox ID="txtShippingDate" runat="server" />
    <obout:Calendar ID="calShippingDate" runat="server" DatePickerMode="true" TextBoxId="txtShippingDate" DatePickerImagePath="../Styles/CalendarStyles/icon2.gif" StyleFolder="../Styles/CalendarStyles/blue"></obout:Calendar>
</div>
<div><div style="display:inline-block;width:100px">Commodity</div>
    <cc1:ComboBox ID="ddCommodity" runat="server" DataSourceID="GetCommodities" DataTextField="Name" DataValueField="CommodityTypeID"
     MenuWidth="300px"></cc1:ComboBox>
    <asp:Button ID="btnGetRates" runat="server" onclick="btnGetRates_Click" 
        Text="Get Rates" />
</div>
<asp:UpdatePanel ID="upnlRatesGrid" runat="server" UpdateMode="Conditional">
<Triggers><asp:AsyncPostBackTrigger ControlID="btnGetRates" EventName="Click" />
<asp:AsyncPostBackTrigger ControlID="btnOk" EventName="Click" />
<asp:AsyncPostBackTrigger ControlID="btnCancel" EventName="Click" />
<asp:AsyncPostBackTrigger ControlID="GridView1" EventName="SelectedIndexChanged" />
</Triggers>
    <ContentTemplate>
        <asp:GridView ID="GridView1" runat="server" 
            onselectedindexchanged="GridView1_SelectedIndexChanged">
            <Columns>
                <asp:CommandField SelectImageUrl="~/Styles/images/update.png" 
                    ShowSelectButton="True" ButtonType="Image" />
            </Columns>
        </asp:GridView>
    </ContentTemplate>
</asp:UpdatePanel>
<div>
    <asp:Button ID="btnOk" runat="server" Text="Ok" onclick="btnOk_Click" />
    <asp:Button ID="btnCancel" runat="server" onclick="btnCancel_Click" 
        Text="Cancel" />
</div>