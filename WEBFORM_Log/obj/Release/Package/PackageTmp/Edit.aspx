<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Edit.aspx.cs" Inherits="HTTPBASE.Edit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table>
                <tr>
                    <th>EmployeeName</th>
                    <td>
                        <asp:TextBox ID="name" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <th>Title</th>
                    <td>
                        <asp:TextBox ID="title" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <th>TitleOfCourtesy</th>
                    <td>
                        <asp:TextBox ID="titleofc" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <th>BirthDate</th>
                    <td>
                        <asp:TextBox ID="bdate" runat="server"></asp:TextBox>
                        <asp:Label runat="server" id="error_type_1" style="color:red"> 請輸入正確時間日期格式</asp:Label>
                    </td>
                </tr>
                <tr>
                    <th>HireDate</th>
                    <td>
                        <asp:TextBox ID="hdate" runat="server"></asp:TextBox>
                        <asp:Label runat="server" id="error_type_2" style="color:red"> 請輸入正確時間日期格式</asp:Label>
                    </td>
                </tr>
                <tr>
                    <th>Address</th>
                    <td>
                        <asp:TextBox ID="addr" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <th>HomePhone</th>
                    <td>
                        <asp:TextBox ID="hphone" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <th>Extension</th>
                    <td>
                        <asp:TextBox ID="exten" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <th>PhotoPath</th>
                    <td>
                        <asp:TextBox ID="ppath" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <th>Notes</th>
                    <td>
                        <asp:TextBox ID="notes" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <th>ManagerID</th>
                    <td>
                        <asp:TextBox ID="mid" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <th>Salary</th>
                    <td>
                        <asp:TextBox ID="salary" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <th colspan="2" style="text-align: center">
                            <asp:Label runat="server" ID="remind" style="color:red">請確認所有欄位都填寫了</asp:Label>    
                    </th>
                </tr>
                <tr>
                    <th colspan="2" style="text-align: center">
                        <asp:Button ID="Button1" runat="server" Text="存檔" onclick="Save" UseSubmitBehavior="false"/>
                        <asp:Button ID="Button2" runat="server" Text="取消" onclick="Cancel" UseSubmitBehavior="false"/>
                    </th>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
