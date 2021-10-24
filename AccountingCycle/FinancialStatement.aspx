<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FinancialStatement.aspx.cs" Inherits="AccountingCycle.FinancialStatement" %>

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
        <h1 style="text-align:center; color:darkblue; font-size:50px; font-family:'Times New Roman', Times, serif">Financial Statement</h1>
        <div style="text-align:center">
            <input runat="server" type="submit" value="Back Home" class="btn btn-primary" style="margin-top:20px;"  onserverclick="Home_Click"/>
        </div>
        <br />
        <div class="container" style="border:2px solid black">
            <h1 style="color:black; font-size:50px; font-weight:bolder; font-family:'Times New Roman', Times, serif">Income Summary</h1>
            <div class="row align-items-start">
                <h2 style="color:darkblue">All Revenue</h2>
                <hr style="color:darkblue" />
                <asp:GridView ID="Income1" CssClass="table table-dark table-striped table-hover w-75" ShowFooter="true" runat="server" AutoGenerateColumns="false">    
                    <Columns>      
                    <asp:TemplateField HeaderText="Account Title" ItemStyle-Width="100">    
                        <ItemTemplate>    
                            <asp:label ID="txtCountry" runat="server" Text='<%# Eval("AccountTitle") %>' />    
                        </ItemTemplate>    
                    </asp:TemplateField>      
                    <asp:TemplateField HeaderText="Amount" ItemStyle-Width="100">    
                        <ItemTemplate>    
                            <asp:label ID="txtName" runat="server" Text='<%# Eval("Amount") %>' />    
                        </ItemTemplate>    
                    </asp:TemplateField>
                    </Columns>    
                    <FooterStyle CssClass="table-dark" Font-Bold="True" ForeColor="White" />
                </asp:GridView>
            </div>
            <div class="row align-items-start">
                <h2 style="color:darkblue">All Expense</h2>
                <hr style="color:darkblue" />
                <asp:GridView ID="Income2" CssClass="table table-dark table-striped table-hover w-75" ShowFooter="true" runat="server" AutoGenerateColumns="false">    
                    <Columns>      
                    <asp:TemplateField HeaderText="Account Title" ItemStyle-Width="100">    
                        <ItemTemplate>    
                            <asp:label ID="txtCountry" runat="server" Text='<%# Eval("AccountTitle") %>' />    
                        </ItemTemplate>    
                    </asp:TemplateField>      
                    <asp:TemplateField HeaderText="Amount" ItemStyle-Width="100">    
                        <ItemTemplate>    
                            <asp:label ID="txtName" runat="server" Text='<%# Eval("Amount") %>' />    
                        </ItemTemplate>    
                    </asp:TemplateField>
                    </Columns>    
                    <FooterStyle CssClass="table-dark" Font-Bold="True" ForeColor="White" />
                </asp:GridView>
            </div>
            <div class="row align-items-start rowhead">
                <table class="table table-dark table-hover w-75">
                    <tr>
                        <td class="w-50" style="font-weight:bold">Net Income</td>
                        <td class="w-50 val" style="font-weight:bold">
                            <asp:Label runat="server" ID="netincome"></asp:Label>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
        <br />
        <div class="container" style="border:2px solid black">
            <h1 style="color:black; font-size:50px; font-weight:bolder; font-family:'Times New Roman', Times, serif">Owner Equity</h1>
            <table class="table table-dark table-striped table-hover w-75 bal" style="border:1px solid black;">
                <tr style="font-weight:bold">
                    <td>Account Title</td>
                    <td>Amount</td>
                </tr>
                <tr>
                    <td>Begining Balance </td>
                    <td><asp:Label runat="server" ID="lblBal"></asp:Label></td>
                </tr>
                <tr>
                    <td>Net Income</td>
                    <td><asp:Label runat="server" ID="lblIncome"></asp:Label></td>
                </tr>
                <tr>
                    <td>Owner Withdrawl </td>
                    <td><asp:Label runat="server" ID="lblWith"></asp:Label></td>
                </tr>
                <tr class="table-dark" style="font-weight:bold">
                    <td>Ending Balance </td>
                    <td><asp:Label runat="server" ID="endbal"></asp:Label></td>
                </tr>
            </table>
        </div>
        <br />
        <div class="container" style="border:2px solid black">
            <h1 style="color:black; font-size:50px; font-weight:bolder; font-family:'Times New Roman', Times, serif">Balance Sheet</h1>
            <div class="row align-items-start">
            <div class="col-6">
                <h2 style="color:darkblue">Asset</h2>
                <hr style="color:darkblue" />
                <asp:GridView ID="asset" CssClass="table table-dark table-striped table-hover w-75" ShowFooter="true" runat="server" AutoGenerateColumns="false">    
                    <Columns>      
                    <asp:TemplateField HeaderText="Account Title" ItemStyle-Width="100">    
                        <ItemTemplate>    
                            <asp:label ID="txtCountry" runat="server" Text='<%# Eval("AccountTitle") %>' />    
                        </ItemTemplate>    
                    </asp:TemplateField>      
                    <asp:TemplateField HeaderText="Amount" ItemStyle-Width="100">    
                        <ItemTemplate>    
                            <asp:label ID="txtName" runat="server" Text='<%# Eval("EntryType").ToString() == "Debit" ? Eval("Amount").ToString()  : "("+Eval("Amount").ToString()+")" %>' />    
                        </ItemTemplate>    
                    </asp:TemplateField>
                    </Columns>    
                    <FooterStyle CssClass="table-dark" Font-Bold="True" ForeColor="White" />
                </asp:GridView>
            </div>
            <div class="col-6">
                <h2 style="color:darkblue">Liability</h2>
                <hr style="color:darkblue" />
                <asp:GridView ID="liability" CssClass="table table-dark table-striped table-hover w-75" ShowFooter="true" runat="server" AutoGenerateColumns="false">    
                    <Columns>      
                    <asp:TemplateField HeaderText="Account Title" ItemStyle-Width="100">    
                        <ItemTemplate>    
                            <asp:label ID="txtCountry" runat="server" Text='<%# Eval("AccountTitle") %>' />    
                        </ItemTemplate>    
                    </asp:TemplateField>      
                    <asp:TemplateField HeaderText="Amount" ItemStyle-Width="100">    
                        <ItemTemplate>    
                            <asp:label ID="txtName" runat="server" Text='<%# Eval("Amount") %>' />    
                        </ItemTemplate>    
                    </asp:TemplateField>
                    </Columns>    
                    <FooterStyle CssClass="table-dark" Font-Bold="True" ForeColor="White" />
                </asp:GridView>
            </div>
        </div>
        </div>
        <br />
    </form>
</body>
</html>
