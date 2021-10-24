<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="AccountingCycle.Home" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="Content/bootstrap.css" rel="stylesheet" />
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.1.0/dist/css/bootstrap.min.css" rel="stylesheet"/>
    <link href="Content/home.css" rel="stylesheet" />

</head>
<body>
    <form id="form1" runat="server">
        <h1 style="text-align:center; color:darkblue; font-size:50px; font-family:'Times New Roman', Times, serif">Journal Entries</h1>
    <br />
    <div style="text-align:center" class="button">
        <input runat="server" type="submit" value="Create New Entry" class="btn btn-danger btn-success" style="margin-top:30px;"  onserverclick="btnConfirm_Click"/>
        <input runat="server" type="submit" value="Trial Balance" class="btn btn-primary" style="margin-top:30px;" onserverclick="Trial_ServerClick" />
        <input runat="server" type="submit" value="Financial Statement" class="btn btn-primary" style="margin-top:30px;" onserverclick="Fin_ServerClick"/>
        <input runat="server" type="submit" value="Closing" class="btn btn-primary" style="margin-top:30px;" onserverclick="Closing_ServerClick"/>
    </div>
    <br />
        <asp:GridView ID="GridView1" CssClass="table table-striped table-dark table-hover w-75" ShowFooter="true" runat="server" AutoGenerateColumns="false" HorizontalAlign="Center">    
        <Columns>    
        <asp:TemplateField HeaderText="Date" ItemStyle-Width="80">    
            <ItemTemplate>    
                <asp:label ID="txtName" runat="server" Text='<%# Eval("Date") %>' />    
            </ItemTemplate>    
        </asp:TemplateField>    
        <asp:TemplateField HeaderText="Account Title" ItemStyle-Width="100">    
            <ItemTemplate>    
                <asp:label ID="txtCountry" runat="server" Text='<%# Eval("AccountTitle") %>' />    
            </ItemTemplate>    
        </asp:TemplateField>    
        <asp:TemplateField HeaderText="Debit" ItemStyle-Width="80">    
            <ItemTemplate>    
                <asp:label ID="txtName" runat="server" Text='<%# Eval("EntryType").ToString() == "Debit" ? Eval("Amount").ToString()  : "" %>' />    
            </ItemTemplate>    
        </asp:TemplateField>    
        <asp:TemplateField HeaderText="Credit" ItemStyle-Width="100">    
            <ItemTemplate>    
                <asp:label ID="txtName" runat="server" Text='<%# Eval("EntryType").ToString() == "Credit" ? Eval("Amount").ToString()  : "" %>' />    
            </ItemTemplate>    
        </asp:TemplateField>
        </Columns>    
            <FooterStyle CssClass="table-dark" Font-Bold="True" ForeColor="White" />
        </asp:GridView>
    </form>
</body>
</html>
