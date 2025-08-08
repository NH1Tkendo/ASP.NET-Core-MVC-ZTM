<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Post.aspx.cs" Inherits="Application.Post" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Quản lý Blog</title>
<style type="text/css">
    body {
        font-family: Arial, sans-serif;
        margin: 20px;
    }
    .header {
        background-color: #f4f4f4;
        padding: 15px;
        margin-bottom: 20px;
        border-radius: 5px;
    }
    .nav-buttons {
        margin-bottom: 20px;
    }
    .nav-button {
        background-color: #007bff;
        color: white;
        padding: 10px 20px;
        border: none;
        border-radius: 5px;
        margin-right: 10px;
        cursor: pointer;
        text-decoration: none;
        display: inline-block;
    }
    .nav-button:hover {
        background-color: #0056b3;
    }
    .add-button {
        background-color: #28a745;
    }
    .add-button:hover {
        background-color: #1e7e34;
    }
    table {
        width: 100%;
        border-collapse: collapse;
        margin-top: 10px;
    }
    th, td {
        padding: 10px;
        text-align: left;
        border: 1px solid #ddd;
    }
    th {
        background-color: #f8f9fa;
    }
    tr:nth-child(even) {
        background-color: #f2f2f2;
    }
    .detail-link {
        color: #007bff;
        text-decoration: none;
    }
    .detail-link:hover {
        text-decoration: underline;
    }
</style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div class="header">
    <h1>Quản lý Blog</h1>
    <p>Danh sách tất cả các Post trong hệ thống</p>
</div>

<div class="nav-buttons">
    <a href="AddBlog.aspx" class="nav-button add-button">Thêm Blog Mới</a>
    <a href="Blog.aspx" class="nav-button">Xem Danh Sách Blog</a>
</div>
            <asp:Repeater ID="dataRepeater" runat="server">
    <HeaderTemplate>
        <table>
            <tr>
                <th>ID</th>
                <th>Tên bài báo</th>
                <th>Nội dung</th>
            </tr>
    </HeaderTemplate>

    <ItemTemplate>
        <tr>
            <td><%# Eval("BlogID") %></td>
            <td><%# Eval("Title") %></td>
            <td><%# Eval("Content") %></td>
        </tr>
    </ItemTemplate>

    <FooterTemplate>
        </table>
    </FooterTemplate>
</asp:Repeater>
        </div>
    </form>
</body>
</html>
