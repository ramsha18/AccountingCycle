<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TrialBalance.aspx.cs" Inherits="AccountingCycle.TrialBalance" %>

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
        <h1 style="text-align:center; color:darkblue; font-size:50px; font-family:'Times New Roman', Times, serif">Trial Balance</h1>
    <div style="text-align:center">
        <input runat="server" type="submit" value="Back Home" class="btn btn-primary" style="margin-top:20px;"  onserverclick="Home_Click"/>
    </div>
    <br />
        <asp:GridView ID="GridView1" CssClass="table table-dark table-striped table-hover w-75" ShowFooter="true" runat="server" AutoGenerateColumns="false" HorizontalAlign="Center">    
        <Columns>      
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
