<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Inventario_Alimentacion.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server" class="">
        <div>
            <div><asp:Label ID="lblUsuario" runat="server" Text="Usuario" /></div>
        </div>
            <div><asp:TextBox ID ="txtUsuario" runat="server" /></div>
       
            <div><asp:Label ID="lblContraseña" runat="server" Text="Contraseña" /></div>
        
            <div><asp:TextBox ID ="txtContraseña" runat="server" TextMode="Password" /></div>
       
            <div><asp:Button id ="btnLogin" runat="server" Text="Login" OnClick="btnLogin_Click" /></div>

            <div><asp:Label ID ="lblMensaje" runat="server" ForeColor="Red"/></div>
    </form>
</body>
</html>
