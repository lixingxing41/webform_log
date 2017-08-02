<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Log.aspx.cs" Inherits="HTTPBASE.Log" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            Log目錄
            <asp:Button ID="Button1" runat="server" OnClick="goback"  Text="back"/>
        </div>
        <div>
            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" OnRowCommand="GridView1_RowCommand">
                <Columns>
                    <asp:BoundField HeaderText="Log檔" DataField="Name" />
                    <asp:TemplateField>
                        <ItemTemplate>
                        <asp:Button ID="BtnDetail" runat="server" Text="詳細資料" CssClass="btn btn-default"/>
                            </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
        <p>
            <asp:Label ID="result" runat="server" Text=""></asp:Label>
        </p>
    </form>
</body>
</html>
