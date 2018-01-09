<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm2.aspx.cs" Inherits="PaginationDemo.WebForm2" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div style="font-family: Arial">
                <table>
                    <tr>
                        <td style="color: #A55129">
                            <strong>Page Size:</strong>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlPageSize" runat="server"
                                AutoPostBack="True" OnSelectedIndexChanged="ddlPageSize_SelectedIndexChanged">
                                <asp:ListItem>5</asp:ListItem>
                                <asp:ListItem>10</asp:ListItem>
                                <asp:ListItem>15</asp:ListItem>
                                <asp:ListItem>20</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td style="color: #A55129">
                            <strong>Page Number:
                            </strong>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlPageNumbers" runat="server"
                                AutoPostBack="True" OnSelectedIndexChanged="ddlPageNumbers_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false"
                                AllowPaging="True" PageSize="5"  EmptyDataText="No customers"
                                CurrentSortField="EmployeeID"
                                CurrentSortDirection="ASC" AllowSorting="True"
                                BackColor="#DEBA84" BorderColor="#DEBA84"
                                BorderStyle="None" BorderWidth="1px"
                                CellPadding="3" CellSpacing="2"
                                OnSorting="GridView1_Sorting" OnRowCreated="GridView1_RowCreated">
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
                                <FooterStyle BackColor="#F7DFB5"
                                    ForeColor="#8C4510" />
                                <HeaderStyle BackColor="#A55129" Font-Bold="True"
                                    ForeColor="White" />
                                <PagerStyle ForeColor="#8C4510"
                                    HorizontalAlign="Center" />
                                <RowStyle BackColor="#FFF7E7"
                                    ForeColor="#8C4510" />
                                <SelectedRowStyle BackColor="#738A9C"
                                    Font-Bold="True" ForeColor="White" />
                                <SortedAscendingCellStyle BackColor="#FFF1D4" />
                                <SortedAscendingHeaderStyle BackColor="#B95C30" />
                                <SortedDescendingCellStyle BackColor="#F1E5CE" />
                                <SortedDescendingHeaderStyle BackColor="#93451F" />
                            </asp:GridView>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </form>
</body>
</html>
