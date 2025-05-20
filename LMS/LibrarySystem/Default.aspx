<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Library Login</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/css/bootstrap.min.css" rel="stylesheet" />
    <style>
        body {
            background-color: #e9ecef;
            font-family: 'Segoe UI', sans-serif;
            margin: 0;
        }

        .banner {
            text-align: center;
            background-color: #ffffff;
            padding: 20px 0;
            box-shadow: 0 2px 8px rgba(0, 0, 0, 0.1);
        }

        .banner img {
            width: 85%;
            max-width: 800px;
        }

        .content-wrapper {
            display: flex;
            justify-content: center;
            align-items: center;
            height: calc(100vh - 160px); /* full height minus banner height */
            padding: 20px;
        }

        .login-wrapper {
            width: 100%;
            max-width: 650px;
            background-color: #ffffff;
            padding: 50px 60px;
            border-radius: 16px;
            box-shadow: 0 0 20px rgba(0, 0, 0, 0.1);
            margin: -20px auto 60px auto; /* top 30px, bottom 60px, horizontal auto to center */
}


        .login-wrapper h3 {
            color: #004080;
            margin-bottom: 30px;
            text-align: center;
            font-size: 26px;
        }

        .form-label {
            font-weight: 600;
        }

        .form-control {
            height: 50px;
            border-radius: 10px;
        }

        .btn-login {
            background-color: #004080;
            color: #fff;
            width: 100%;
            height: 50px;
            border-radius: 10px;
            font-weight: 600;
            font-size: 16px;
        }

        .radio-group {
            display: flex;
            justify-content: center;
            gap: 50px;
            margin-top: 20px;
            margin-bottom: 25px;
        }

        .radio-group .radio-label {
            font-weight: 500;
            color: #333;
        }

        .radio-group input[type="radio"] {
            margin-right: 8px;
            transform: scale(1.2);
        }

        .btn-login:hover {
            background-color: #003366;
            box-shadow: 0 4px 12px rgba(0, 0, 0, 0.2);
            color: #fff;
            transition: background-color 0.3s ease, box-shadow 0.3s ease;
        }


        .error-label {
            color: red;
            font-size: 0.95rem;
            text-align: center;
            margin-bottom: 10px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">

        <!-- Banner -->
        <div class="banner">
            <asp:Image ID="Image1" runat="server" ImageUrl="~/img/banner.png" CssClass="img-fluid" />
        </div>

        <!-- Login Form Section -->
        <div class="content-wrapper">
            <div class="login-wrapper">
                <h3>Login to Library System</h3>

                <asp:Label ID="lbl" runat="server" CssClass="error-label" EnableViewState="false" />

                <!-- Username -->
                <div class="mb-3">
                    <label for="txtuname" class="form-label">Username</label>
                    <asp:TextBox ID="txtuname" runat="server" CssClass="form-control"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                        ControlToValidate="txtuname" ErrorMessage="Username required!"
                        CssClass="text-danger" Display="Dynamic" />
                </div>

                <!-- Password -->
                <div class="mb-3">
                    <label for="txtupass" class="form-label">Password</label>
                    <asp:TextBox ID="txtupass" runat="server" CssClass="form-control" TextMode="Password"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"
                        ControlToValidate="txtupass" ErrorMessage="Password required!"
                        CssClass="text-danger" Display="Dynamic" />
                </div>

                <!-- Role Selection -->
                <div class="radio-group">
                    <label class="radio-label">
                        <asp:RadioButton ID="rdolibrary" runat="server" GroupName="role" Checked="True" />
                        Librarian
                    </label>
                    <label class="radio-label">
                        <asp:RadioButton ID="rdosudent" runat="server" GroupName="role" />
                        Student
                    </label>
                </div>

                <!-- Login Button -->
                <asp:Button ID="Button1" runat="server" Text="Login" CssClass="btn btn-login" OnClick="Button1_Click" />
            </div>
        </div>

    </form>
</body>
</html>
