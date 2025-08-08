<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddBlog.aspx.cs" Inherits="Application.AddBlog" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Thêm Blog Mới</title>
    <style type="text/css">
        body {
            font-family: Arial, sans-serif;
            margin: 20px;
            background-color: #f8f9fa;
        }
        .container {
            max-width: 600px;
            margin: 0 auto;
            background-color: white;
            padding: 30px;
            border-radius: 10px;
            box-shadow: 0 0 10px rgba(0,0,0,0.1);
        }
        .header {
            background-color: #007bff;
            color: white;
            padding: 15px;
            margin: -30px -30px 30px -30px;
            border-radius: 10px 10px 0 0;
        }
        .form-group {
            margin-bottom: 20px;
        }
        label {
            display: block;
            font-weight: bold;
            margin-bottom: 5px;
            color: #333;
        }
        .form-control {
            width: 100%;
            padding: 10px;
            border: 1px solid #ccc;
            border-radius: 5px;
            font-size: 14px;
            box-sizing: border-box;
        }
        .form-control:focus {
            border-color: #007bff;
            outline: none;
            box-shadow: 0 0 5px rgba(0,123,255,0.3);
        }
        .btn {
            padding: 12px 25px;
            border: none;
            border-radius: 5px;
            cursor: pointer;
            font-size: 14px;
            text-decoration: none;
            display: inline-block;
            margin-right: 10px;
            text-align: center;
        }
        .btn-success {
            background-color: #28a745;
            color: white;
        }
        .btn-success:hover {
            background-color: #218838;
        }
        .btn-secondary {
            background-color: #6c757d;
            color: white;
        }
        .btn-secondary:hover {
            background-color: #5a6268;
        }
        .button-group {
            margin-top: 30px;
            text-align: center;
        }
        .message {
            padding: 10px;
            margin-bottom: 20px;
            border-radius: 5px;
        }
        .success-message {
            background-color: #d4edda;
            border: 1px solid #c3e6cb;
            color: #155724;
        }
        .error-message {
            background-color: #f8d7da;
            border: 1px solid #f5c6cb;
            color: #721c24;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <div class="header">
                <h1>Thêm Blog Mới</h1>
                <p>Tạo một blog mới trong hệ thống</p>
            </div>

            <asp:Panel ID="messagePanel" runat="server" Visible="false" CssClass="message">
                <asp:Label ID="messageLabel" runat="server"></asp:Label>
            </asp:Panel>

            <div class="form-group">
                <label for="txtBlogName">Tên Blog <span style="color: red;">*</span></label>
                <asp:TextBox ID="txtBlogName" runat="server" CssClass="form-control" placeholder="Nhập tên blog..."></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvBlogName" runat="server" 
                    ControlToValidate="txtBlogName" 
                    ErrorMessage="Tên blog không được để trống" 
                    ForeColor="Red" 
                    Display="Dynamic">
                </asp:RequiredFieldValidator>
            </div>

            <div class="form-group">
                <label for="txtBlogUrl">URL Blog (tùy chọn)</label>
                <asp:TextBox ID="txtBlogUrl" runat="server" CssClass="form-control" placeholder="http://example.com"></asp:TextBox>
            </div>

            <div class="button-group">
                <asp:Button ID="btnAddBlog" runat="server" Text="Thêm Blog" CssClass="btn btn-success" OnClick="btnAddBlog_Click" />
                <a href="Blog.aspx" class="btn btn-secondary">Quay lại</a>
            </div>
        </div>
    </form>
</body>
</html>
