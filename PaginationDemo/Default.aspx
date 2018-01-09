<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="PaginationDemo.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False"  EmptyDataText="No customers"
                AllowSorting="True" PageSize="4" OnSorting="GridView1_Sorting" CurrentSortField="EmployeeID"
                CurrentSortDirection="ASC" AllowPaging="True" OnRowCreated="GridView1_RowCreated" BackColor="#DEBA84" BorderColor="#DEBA84" BorderStyle="None" BorderWidth="1px" CellPadding="3" CellSpacing="2">
                <Columns>
                    <asp:BoundField DataField="EmployeeID" HeaderText="EmployeeID"
                        SortExpression="EmployeeID" />
                    <asp:BoundField DataField="FirstName" HeaderText="First Name"
                        SortExpression="FirstName" />
                    <asp:BoundField DataField="LastName" HeaderText="Last Name"
                        SortExpression="LastName" />
                    <asp:BoundField DataField="Title" HeaderText="Title"
                        SortExpression="Title" />
                    <asp:BoundField DataField="City" HeaderText="City"
                        SortExpression="City" />
                </Columns>
                <FooterStyle BackColor="#F7DFB5" ForeColor="#8C4510" />
                <HeaderStyle BackColor="#A55129" Font-Bold="True" ForeColor="White" />
                <PagerStyle ForeColor="#8C4510" HorizontalAlign="Center" />
                <RowStyle BackColor="#FFF7E7" ForeColor="#8C4510" />
                <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="White" />
                <SortedAscendingCellStyle BackColor="#FFF1D4" />
                <SortedAscendingHeaderStyle BackColor="#B95C30" />
                <SortedDescendingCellStyle BackColor="#F1E5CE" />
                <SortedDescendingHeaderStyle BackColor="#93451F" />
            </asp:GridView>

            <asp:Repeater ID="repeaterPaging" runat="server">
                <ItemTemplate>
                    <asp:LinkButton ID="pagingLinkButton" runat="server"
                        Text='<%#Eval("Text") %>'
                        CommandArgument='<%# Eval("Value") %>'
                        Enabled='<%# Eval("Enabled") %>'
                        OnClick="linkButton_Click">
                    </asp:LinkButton>
                </ItemTemplate>
            </asp:Repeater>
        </div>
    </form>
</body>
</html>
