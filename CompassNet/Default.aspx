<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="Default.aspx.cs" Inherits="CompassNet._Default" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<%@ Register src="Modules/QuoteEntry.ascx" tagname="QuoteEntry" tagprefix="uc1" %>
<%@ Register src="Modules/ContainerInfo.ascx" tagname="ContainerInfo" tagprefix="uc2" %>
<%@ Register Assembly="obout_Calendar2_Net" Namespace="OboutInc.Calendar2" TagPrefix="obout" %>
<%@ Register Assembly="obout_Interface" Namespace="Obout.Interface" TagPrefix="cc1" %>

<%@ Register src="Modules/RateFinder.ascx" tagname="RateFinder" tagprefix="uc3" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <h2>
        Welcome to Compass Forwarding!
    </h2>
    <p>
        Company info goes here
        </p>
    
   
<%--    <uc3:RateFinder ID="RateFinder1" runat="server" />
--%>    
   
    <uc2:ContainerInfo ID="ContainerInfo1" runat="server" />
    
    
    
    
<%--    <uc1:QuoteEntry ID="QuoteEntry1" runat="server" />
--%>    
    
    
    
    <p>
Stufff.
    </p>
</asp:Content>
