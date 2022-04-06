<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Menu.aspx.cs" Inherits="UTTT.Ejemplo.Persona.Menu" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <link href="Content\bootstrap.min.css" rel="stylesheet" />
    <style type="text/css">
        .auto-style1 {
            width: 1280px;
            height: 720px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="row">
                <div class="col" id="titleEmpresa">
                    <label id="title">Servicios Unidos Urbanos y Suburbanos Mano Amiga</label>
                </div>
            </div>
        <div class="container">
            <div class="row">
                <div class="col d-flex justify-content-center">
                    <img class="imgLogo" src="Images/Mano_Amiga_logo.png" />
                </div>
            </div>
            <div class="row">
                <div id="col1" class="col-md-6 col-sm-12">
                    <asp:Button ID="btnEmpleados" runat="server" Text="Empleados" 
                        onclick="btnEmpleados_Click" ViewStateMode="Disabled"
                        Enabled="false"/>
                </div>
                <div id="col2" class="col-md-6 col-sm-12">
                    <asp:Button ID="btnCedes" runat="server" Text="Catalogo Cedes" 
                        onclick="btnCedes_Click" ViewStateMode="Disabled"
                        Enabled="false"/>
                </div>
                <div id="col3" class="col-md-6 col-sm-12">
                    <asp:Button ID="btnCorridas" runat="server" Text="Corridas" 
                        onclick="btnCorridas_Click" ViewStateMode="Disabled"
                        Enabled="false"/>
                </div>
                <div id="col4" class="col-md-6 col-sm-12">
                    <asp:Button ID="btnCerrarSesion" runat="server" Text="Cerrar Sesión" 
                        onclick="btnCerrarSesion_Click" ViewStateMode="Disabled"
                        Enabled="false"/>
                </div>
                
            </div>
        </div>
    </form>
</body>
</html>

<style>
    *{
        font-family: 'Century Gothic';
        background-color: #eeeeee;
    }

    #titleEmpresa{
        padding-top: 3%;
        padding-bottom: 3%;
        text-align:center;
        color: #939597;
        font-size: 35px;
        font-weight: bold;
    }

    #titleEmpresa,
    #title{
        background-color: #212529;
    }

    .imgLogo{
        width:35%;
        margin-top: 3%;
        margin-bottom: 4%;
    }

    #col1, #col2, #col3, #col4{
        text-align: center;
        padding-top: 2%;
    }

    #btnEmpleados,
    #btnCorridas,
    #btnCedes,
    #btnCerrarSesion{
        height:50px;
        padding: 1%;
        font-size:20px;
        width: 200px;
        background-color: #014e35;
        color: #D1D1D1;
        border-radius: 6px;
        border: none;
    }

    #btnEmpleados:hover,
    #btnCorridas:hover,
    #btnCedes:hover,
    #btnCerrarSesion:hover{
        background-color: #00724e;
        font-weight:600;
    }

</style>
