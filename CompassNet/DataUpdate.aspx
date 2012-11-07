<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DataUpdate.aspx.cs" Inherits="CompassNet.DataUpdate" %>
<%@ Register Assembly="obout_SuperForm" Namespace="Obout.SuperForm" TagPrefix="cc3" %>
<%@ Register Assembly="obout_Grid_NET" Namespace="Obout.Grid" TagPrefix="cc2" %>
<%@ Register assembly="obout_Interface" namespace="Obout.Interface" tagprefix="cc1" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript">
    function newRecord(sender) {
        if (OboutDropDownList1._value == "Clients") {
            SuperForm1_ID.value('');
            SuperForm1_Type.value('');
            SuperForm1_Name.value('');
        } else if (OboutDropDownList1._value == "Sales Reps") {
                SuperForm1_ID.value('');
                SuperForm1_Code.value('');
                SuperForm1_Name.value('');
                //SuperForm1_Active.checked(false);
                }
        return false;
    }
    function OnnewrecComplete(result, userContext, methodName) {

        alert(result);

    }

</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
   <div>
    <cc1:oboutdropdownlist id="OboutDropDownList1" name="OboutDropDownList1" runat="server" 
        AutoPostBack="True" 
        onselectedindexchanged="OboutDropDownList1_SelectedIndexChanged">
        <asp:ListItem Text="Clients" />
        <asp:ListItem Text="Sales Reps" />
    </cc1:oboutdropdownlist>
        
        
        
     </div>
   
    <div>
        <cc2:Grid ID="Grid1" runat="server" AutoPostBackOnSelect="True" onselect="Grid1_Select">
            <AddEditDeleteSettings AddLinksPosition="Top" />
            <ClientSideEvents ExposeSender="True" OnBeforeClientAdd="newRecord" />
		<TemplateSettings NewRecord_TemplateId="addTemplate" NewRecord_EditTemplateId="saveTemplate" />
        <Templates>
                <cc2:GridTemplate runat="server" ID="addTemplate">
                    <Template>
                        <img src="../Styles/images/add.png" height="16" onclick="Grid1.addRecord()" alt="" title="Add New" class="icon"/>
                    </Template>
                </cc2:GridTemplate>
                 <cc2:GridTemplate runat="server" ID="saveTemplate">
                    <Template>
                        <img src="../Styles/images/update.png" height="16" onclick="Grid1.insertRecord()" alt="" title="Save" class="icon"/>
                        
                        <img src="../Styles/images/undo.png" height="16" onclick="Grid1.cancelNewRecord()" alt="" title="Cancel" class="icon"/>
                    </Template>
                </cc2:GridTemplate>				
        </Templates>
        </cc2:Grid>
    </div>
    <div><cc3:SuperForm ID="SuperForm1" runat="server" DefaultMode="Edit" 
            AutoGenerateEditButton="True"
            onitemupdating="SuperForm1_ItemUpdating">
    </cc3:SuperForm>
    </div>
    
</asp:Content>
