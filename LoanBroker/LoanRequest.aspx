<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LoanRequest.aspx.cs" Inherits="LoanBroker.LoanRequest" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>

    
    <form id="form1" runat="server">
        <asp:Label ID="Label1" runat="server" Text="Amount"></asp:Label>
&nbsp;
        <asp:TextBox ID="tb_Amount" runat="server"></asp:TextBox>
        <p>
            <asp:Label ID="Label2" runat="server" Text="Lånetid"></asp:Label>
&nbsp;
            <asp:TextBox ID="tb_LoanDuration" runat="server" style="margin-bottom: 0px"></asp:TextBox>
        </p>
        <p>
            <asp:Label ID="Label3" runat="server" Text="CPR"></asp:Label>
&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:TextBox ID="tb_CPR" runat="server"></asp:TextBox>
        </p>
        <p>
            <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Button" />
        </p>
        <p>
            &nbsp;</p>
        <p>
            <asp:Label ID="lb_CreditPoint" runat="server" Text="label"></asp:Label>
        </p>
    </form>

    
</body>
</html>
