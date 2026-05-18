<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="CRUDASPDocente6133261.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Registro de Estudiantes</title>
    <style type="text/css">
        body {
            font-family: Arial, sans-serif;
            margin: 20px;
        }
        .container {
            max-width: 800px;
            margin: 0 auto;
        }
        .form-group {
            margin-bottom: 10px;
        }
        .form-group label {
            display: inline-block;
            width: 80px;
            font-weight: bold;
        }
        .form-group input[type="text"], .form-group select {
            width: 250px;
            padding: 5px;
        }
        .radio-group {
            display: inline-block;
        }
        .radio-group label {
            width: auto;
            margin-right: 15px;
        }
        .buttons {
            margin: 20px 0;
        }
        .buttons input {
            margin-right: 10px;
            padding: 5px 15px;
            cursor: pointer;
        }
        .search-area {
            margin-bottom: 20px;
            border: 1px solid #ccc;
            padding: 10px;
        }
        .grid-container {
            margin-top: 20px;
        }
        .student-grid {
            width: 100%;
            border-collapse: collapse;
            background-color: #e6f0ff;
        }
        .student-grid th, .student-grid td {
            border: 1px solid #4a7ab5;
            padding: 8px;
            text-align: left;
        }
        .student-grid th {
            background-color: #4a7ab5;
            color: white;
        }
        .title {
            font-size: 24px;
            font-weight: bold;
            margin-bottom: 20px;
            text-align: center;
        }
        .error-message {
            color: red;
            font-size: 12px;
            margin-left: 85px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <div class="title">REGISTRO DE ESTUDIANTES</div>
            
            <div class="form-group">
                <label>Id:</label>
                <asp:TextBox ID="txtId" runat="server" Width="250px"></asp:TextBox>
                <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click" />
            </div>
            
            <div class="form-group">
                <label>Nombre:</label>
                <asp:TextBox ID="txtNombre" runat="server"></asp:TextBox>
            </div>
            
            <div class="form-group">
                <label>Genero:</label>
                <div class="radio-group">
                    <asp:RadioButton ID="rbMasculino" runat="server" Text="Masculino" GroupName="Genero" />
                    <asp:RadioButton ID="rbFemenino" runat="server" Text="Femenino" GroupName="Genero" />
                </div>
            </div>
            
            <div class="form-group">
                <label>Email:</label>
                <asp:TextBox ID="txtEmail" runat="server"></asp:TextBox>
            </div>
            
            <div class="form-group">
                <label>Ciudad:</label>
                <asp:TextBox ID="txtCiudad" runat="server"></asp:TextBox>
            </div>
            
            <div class="buttons">
                <asp:Button ID="btnNew" runat="server" Text="New" OnClick="btnNew_Click" />
                <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" />
                <asp:Button ID="btnUpdate" runat="server" Text="Update" OnClick="btnUpdate_Click" />
                <asp:Button ID="btnDelete" runat="server" Text="Delete" OnClick="btnDelete_Click" />
                <asp:Button ID="btnShow" runat="server" Text="Show" OnClick="btnShow_Click" />
            </div>
            
            <div class="grid-container">
                <asp:GridView ID="gvEstudiantes" runat="server" CssClass="student-grid" 
                    AutoGenerateColumns="True" OnSelectedIndexChanged="gvEstudiantes_SelectedIndexChanged">
                    <HeaderStyle BackColor="#4a7ab5" ForeColor="White" />
                    <AlternatingRowStyle BackColor="#cce0ff" />
                </asp:GridView>
            </div>
            
            <asp:Label ID="lblMensaje" runat="server" ForeColor="Red" Font-Bold="true"></asp:Label>
        </div>
        
        <script type="text/javascript">
            // Validación del genero con JavaScript
            function validateGenero() {
                var rb1 = document.getElementById('<%= rbMasculino.ClientID %>');
                var rb2 = document.getElementById('<%= rbFemenino.ClientID %>');
                if (rb1.checked || rb2.checked) {
                    return true;
                }
                return false;
            }
        </script>
    </form>
</body>
</html>