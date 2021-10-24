<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Create.aspx.cs" Inherits="AccountingCycle.Create" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
        <link href="Content/bootstrap.css" rel="stylesheet" />
    <link href="Content/home.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <h1 style="text-align:center; color:darkblue; font-size:40px; font-family:'Times New Roman', Times, serif">New Journal Entry</h1>
        <div class="container w-100">
            <div class="row align-items-start">
                <div class="col-3">
                    <label class="lbl">Journal Entry Date</label>
                    <input runat="server" id="Date" class="form-control form-control-sm" type="date" placeholder="Select Date" aria-label=".form-control-sm example"/>
                </div>
                <div class="col-3">
                    <label class="lbl">Amount</label>
                    <input runat="server" id="Amount" class="form-control form-control-sm" type="number" placeholder="Enter Amount" aria-label=".form-control-sm example"/>
                </div>
            </div>
            <br />
            <div class="row align-items-start">
                <h2 style="color:darkblue">For Debit</h2>
                <hr style="color:darkblue" />
                <div class="col-3">
                    <label class="lbl">Account Title</label>
                    <input runat="server" id="Debit_Account_Title" class="form-control form-control-sm" type="text" placeholder="Enter Account Title" aria-label=".form-control-sm example"/>
                </div>
                <div class="col-3">
                    <label class="lbl">Account Type</label>
                    <select runat="server" id="Debit_Account_Type" class="form-select form-select-sm mb-3" aria-label=".form-select-sm example">
                        <option value="0" selected="selected">Select Account Type</option>
                        <option value="1">Asset</option>
                        <option value="2">Liability</option>
                        <option value="3">Owner Capital</option>
                        <option value="4">Owner Withdrawl</option>
                        <option value="5">Revenue</option>
                        <option value="6">Expense</option>
                    </select>
                </div>
                <div class="col-3">
                    <label class="lbl">Entry Type</label>
                    <input runat="server" id="Debit" class="form-control form-control-sm" type="text" value="Debit" readonly="readonly" aria-label=".form-control-sm example"/>
                </div>
            </div>
            <br />
            <div class="row align-items-start">
                <h2 style="color:darkblue">For Credit</h2>
                <hr style="color:darkblue" />
                <div class="col-3">
                    <label class="lbl">Account Title</label>
                    <input runat="server" id="Credit_Account_Title" class="form-control form-control-sm" type="text" placeholder="Enter Account Title" aria-label=".form-control-sm example"/>
                </div>
                <div class="col-3">
                    <label class="lbl">Account Type</label>
                    <select runat="server" id="Credit_Account_Type" class="form-select form-select-sm mb-3" aria-label=".form-select-sm example">
                        <option value="0" selected="selected">Select Account Type</option>
                        <option value="1">Asset</option>
                        <option value="2">Liability</option>
                        <option value="3">Owner Capital</option>
                        <option value="4">Owner Withdrawl</option>
                        <option value="5">Revenue</option>
                        <option value="6">Expense</option>
                    </select>
                </div>
                <div class="col-3">
                    <label class="lbl">Entry Type</label>
                    <input runat="server" id="Credit" class="form-control form-control-sm" type="text" value="Credit" readonly="readonly" aria-label=".form-control-sm example"/>
                </div>
            </div>
            <div style="text-align:center; margin-bottom:20px">
                <input runat="server" type="submit" value="Create" class="btn btn-primary w-25" style="margin-top:30px; margin-right:20px" onserverclick="submit_click" />
                <input runat="server" type="submit" value="Home" class="btn btn-primary w-25" style="margin-top:30px;" onserverclick="Home_Click" />
            </div>
        </div>

    </form>
</body>
</html>
