<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="UTTT.Ejemplo.Persona.Login" debug="true"%>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<!DOCTYPE html>
<html lang="es">
<head>
    <meta charset="UTF-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <link href="Content\bootstrap.min.css" rel="stylesheet" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Sistema Mano Amiga</title>
</head>
<body>
    <form class="formBox" runat="server">
        <h1 class="formTitle">SIGN IN</h1>
        <%--<input ID="" type="text" placeholder="username">--%>
        <asp:TextBox CssClass="form-control" ID="txtUsername" runat="server" ViewStateMode="Disabled"
             placeholder="username">
        </asp:TextBox> 
        <%--<input ID="" type="password" placeholder="password">--%>
        <asp:TextBox CssClass="form-control" ID="txtPassword" runat="server" ViewStateMode="Disabled"
             placeholder="password" TextMode="Password">
        </asp:TextBox> 
        <asp:Button ID="btnLogin" runat="server" Text="Ingresar" 
                        onclick="btnIngresar_Click" ViewStateMode="Disabled"/>
        <div id="forgotPass">
            <label><a href="CambioPassManager.aspx">He olvidado mi contraseña</a></label>
        </div>
        <div id="newUser">
            <label>Si no se cuenta con una cuenta puede crearla dando clic <a href="UsuariosManager.aspx">aquí</a></label>
        </div>
    </form>
</body>
</html>

<style>
    body, html{
    font-family: 'Century Gothic';
    height: 100%;
    }

    body{
        margin: 0;
        padding: 0;
        background-color: #eeeeee;

        display: flex;
        flex-direction: row;
        flex-wrap: wrap;
        justify-content: center;
        align-items: center;
    }

    .formBox{
        width: 400px;
        padding: 40px;
        background-color: #4d6687;
        text-align: center;
        border-radius: 5%;
        box-shadow: 3px 3px 3px 1px rgba(0, 0, 0, 0.2);
    }

    .formTitle{
        color: #D5D7D6;
        text-transform: uppercase;
        font-weight: 500;
    }

    #txtUsername,
    #txtPassword,
    #btnLogin{
        border: 0;
        background-color: rgba(223, 228, 235, 0.301);
        display: block;
        margin: 20px auto;
        padding: 14px 10px;
        text-align: center;
        border: 2px solid #4d6687;
        width: 200px;
        outline: none;
        color: #D5D7D6;
        border-radius: 24px;
        transition: 0.25s;
    }

    #txtUsername:focus,
    #txtPassword:focus{
        width: 280px;
        border-color: white;
    }

    #btnLogin{
        border: 0;
        background-color: #014e35;
        color: #D1D1D1;
        cursor: pointer;
    }

    #btnLogin:hover{
        background-color: #00724e;
        font-weight:600;
    }
    #forgotPass{
        padding-bottom: 5%;
    }

    #forgotPass,
    #newUser{
        color: #D1D1D1;
    }

    a{
        color: #D1D1D1;
    }

    a:hover{
        color: #D1D1D1;
        font-weight:600;
    }
</style>
