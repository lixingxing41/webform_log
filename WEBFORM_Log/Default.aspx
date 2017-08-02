<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="HTTPBASE.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        查詢條件:<br />
        名字:
        <asp:TextBox ID="TxtQueryName" runat="server"></asp:TextBox>
        職稱:<asp:TextBox ID="TxtQueryTitle" runat="server"></asp:TextBox>
        <asp:Button ID="BtnQuery" runat="server" Text="查詢" UseSubmitBehavior="false" />
        <br />
        <asp:Button ID="BtnAdd" runat="server" Text="新增" UseSubmitBehavior="false" />
        <div>
            <asp:GridView ID="GridView1" runat="server" >
                <Columns>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:Button ID="edit" name="action" value="edit" runat="server" Text="編輯" UseSubmitBehavior="false" />
                            <asp:Button ID="delete" name="action" value="delete" runat="server" Text="刪除" UseSubmitBehavior="false" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns> 
            </asp:GridView>
        </div>
        <asp:Button ID="BtnFirstPage" runat="server" Text="第一頁" onclick="toFirstPage"/>
        <asp:Button ID="BtnPPage" runat="server" Text="上一頁" onclick="PreviousPage"/>
        <asp:Button ID="BtnNPage" runat="server" Text="下一頁" onclick="NextPage"/>
        <asp:Button ID="BtnLastPage" runat="server" Text="最後頁" onclick="toLastPage"/>
        <asp:Button ID="BtnLog" runat="server" Text="Log" OnClick="toLog" />
    </form>

</body>
</html>
