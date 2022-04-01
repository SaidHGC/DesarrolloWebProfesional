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
        <div class="container">
            <div class="row">
                <div class="col" id="titleEmpresa">
                    <label>Servicios Unidos Urbanos y Suburbanos Mano Amiga</label>
                </div>
            </div>
            <div class="row">
                <div class="col d-flex justify-content-center">
                    <img class="imgLogo" src="Images/Mano_Amiga_logo.png" />
                </div>
            </div>
            <div class="row">
                <div class="col d-flex justify-content-around">
                    <asp:Button ID="btnEmpleados" runat="server" Text="Empleados" 
                        onclick="btnEmpleados_Click" ViewStateMode="Disabled"/>
                    <asp:Button ID="btnAseguradoras" runat="server" Text="Catalogo Aseguradoras" 
                        onclick="btnAseguradoras_Click" ViewStateMode="Disabled"/>
                    <asp:Button ID="btnCorridas" runat="server" Text="Corridas" 
                        onclick="btnCorridas_Click" ViewStateMode="Disabled"/>
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
        color: black;
        font-size: 35px;
        font-weight: bold;
    }

    .imgLogo{
        width:35%;
        margin-top: 1%;
        margin-bottom: 5%;
    }

    #btnEmpleados,
    #btnCorridas{
        padding: 1%;
        font-size:20px;
        width: 200px;
        background-color: #546E7A;
        color: lightgray;
        border-radius: 6px;
        border: none;
    }

    #btnEmpleados:hover,
    #btnCorridas:hover,
    #btnAseguradoras:hover{
        background-color:#2E7D32;
        font-weight:600;
    }

    #btnAseguradoras{
        padding: 1%;
        font-size:20px;
        width: 350px;
        background-color: #546E7A;
        color: lightgray;
        border-radius: 6px;
        border: none;
    }
</style>
