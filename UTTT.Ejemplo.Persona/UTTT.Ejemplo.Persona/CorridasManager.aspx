<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CorridasManager.aspx.cs" Inherits="UTTT.Ejemplo.Persona.CorridasManager" debug="true"%>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="Content\bootstrap.min.css" rel="stylesheet" />
    <title></title>
    <script type="text/javascript">
        function validaNumero(evt) {
            //Validar que solamente se ingresen números en nuestra caja de texto
            var code = (evt.which) ? evt.which : evt.keyCode;
            if (code == 8) {
                return true;
            }
            else if (code >= 48 && code <= 57) {
                return true;
            }
            else {
                return false;
            }
        }

        function validarLetras(e) {
            //Validar que solamente se ingresen letras y algunos caracteres especiales
            key = e.keyCode || e.which;
            tecla = String.fromCharCode(key).toLowerCase();
            letras = " áéíóúabcdefghijklmnñopqrstuvwxyzüï";
            especiales = "8-37-39-46";
            tecla_especial = false;
            for (var i in especiales) {
                if (key == especiales[i]) {
                    tecla_especial = true;
                    break;
                }
            }
            if (letras.indexOf(tecla) == -1 && !tecla_especial) {
                return false;
            }
        }
    </script>
</head>
<body>

    <nav class="navbar navbar-expand-sm navbar-dark bg-dark sticky-top">
    <div class="d-none d-md-block d-lg-block d-xl-block">
      <a class="navbar-brand" href="Menu.aspx">
        <img src="Images/Mano_Amiga_logo.png" alt="Logo" class="logoEmpresa" />
      </a>
    </div>
    <!--Se contrae la navBar-->
    <button class="navbar-toggler" type="button" data-toggle="collapse" data-target="#navbarSupportedContent"
      aria-controls="navbarSupportedContent" aria-expanded="false" aria-label="Toggle navigation">
      <span class="navbar-toggler-icon"></span>
    </button>

    <div class="collapse navbar-collapse" id="navbarSupportedContent">
      <ul class="navbar-nav">
        <li class="nav-item" id="navegacionCedes">
          <a class="nav-link" href="CedesPrincipal.aspx">Cedes</a>
        </li>
        <li class="nav-item" id="navegacionEmpleados">
          <a class="nav-link" href="PersonaPrincipal.aspx">Empleados</a>
        </li>
        <li class="nav-item" id="navegacionCorridas">
          <a class="nav-link" href="CorridasPrincipal.aspx">Corridas</a>
        </li>
          <li class="nav-item" id="navegacionLogOut">
          <a class="nav-link" href="Login.aspx">Cerrar Sesión</a>
        </li>
      </ul>
    </div>
  </nav>

    <form id="form1" runat="server">
        <div class="scriptManager">
            <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true"></asp:ScriptManager>
        </div>
        <!--INICIO DE TITULO PERSONA-->
        <div class="titleCorrida">Corridas</div>
        <!--FINAL DE TITULO PERSONA-->
        
        <!--INICIO TITULO DE LA ACCION PEDIDA-->
        <div class="titleAccion">
            <asp:Label ID="lblAccion" runat="server" Text="Accion" Font-Bold="True"></asp:Label>
        </div>
        <!--FINAL TITULO DE LA ACCION PEDIDA-->
        
        <div class="container">
            <!--INICIO CONTENIDO DE DATOS-->
            <div class="row" id="datos">

                <div class="row" id="puntoInicio">
                    <div class="col d-flex justify-content-end">
                        <label class="p-2">Punto de Inicio:</label>
                    </div>
                    <div class="col d-flex justify-content-start">
                        <asp:TextBox 
                            CssClass="form-control" 
                            ID="txtPuntoInicio" 
                            runat="server" 
                            ViewStateMode="Disabled"
                            onkeypress="return validarLetras(event);">
                        </asp:TextBox> 
                    </div>
                </div>

                <div class="row" id="valPuntoInicio">
                    <div class="col d-flex justify-content-center">
                        <asp:RequiredFieldValidator 
                            ID="rfvPuntoInicio" 
                            runat="server" 
                            ErrorMessage="*El campo de punto inicial es obligatorio" 
                            ForeColor="#660066" 
                            ControlToValidate="txtPuntoInicio" 
                            ValidationGroup="vgGuardar">
                        </asp:RequiredFieldValidator>
                    </div>
                </div>

                <div class="row" id="puntoLlegada">
                    <div class="col  d-flex justify-content-end">
                        <label class="p-2">Punto de Llegada:</label>
                    </div>
                    <div class="col d-flex justify-content-start">
                        <asp:TextBox 
                            CssClass="form-control"
                            ID="txtPuntoFinal" 
                            runat="server" 
                            ViewStateMode="Disabled"
                            onkeypress="return validarLetras(event);">
                        </asp:TextBox>
                    </div>
                </div>

                <div class="row" id="valPuntoFinal">
                    <div class="col d-flex justify-content-center">
                        <asp:RequiredFieldValidator 
                            ID="rfvPuntoFinal" 
                            runat="server" 
                            ControlToValidate="txtPuntoFinal" 
                            ErrorMessage="*El campo de punto final es obligatorio" 
                            ForeColor="#660066" 
                            ValidationGroup="vgGuardar">
                        </asp:RequiredFieldValidator>
                    </div>
                </div>

                <div class="row" id="cedes">
                    <div class="col  d-flex justify-content-end">
                        <label class="p-2">Cede:</label>
                    </div>
                    <div class="col d-flex justify-content-start">

                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                            <asp:DropDownList ID="ddlCede" runat="server"
                                OnSelectedIndexChanged="ddlCede_SelectedIndexChanged">
                            </asp:DropDownList>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="ddlCede" EventName="SelectedIndexChanged" />
                            </Triggers>
                        </asp:UpdatePanel>

                    </div>
                </div>

                <div class="row" id="valCede">
                    <div class="col d-flex justify-content-center">
                        <asp:RequiredFieldValidator ID="rfvCede" runat="server" ControlToValidate="ddlCede" ErrorMessage="*Seleccionar una Cede" ForeColor="#660066" InitialValue="-1" ValidationGroup="vgGuardar"></asp:RequiredFieldValidator>
                    </div>
                </div>

                <div class="row" id="tipoCorrida">
                    <div class="col  d-flex justify-content-end">
                        <label class="p-2">Tipo de Corrida:</label>
                    </div>
                    <div class="col d-flex justify-content-start">
                        <asp:TextBox 
                            CssClass="form-control"
                            ID="txtTipoCorrida" 
                            runat="server" 
                            ViewStateMode="Disabled"
                            onkeypress="return validarLetras(event);">
                        </asp:TextBox>
                    </div>
                </div>

                <div class="row" id="valTipoCorrida">
                    <div class="col d-flex justify-content-center">
                        <asp:RequiredFieldValidator 
                            ID="rfvTipoCorrida" 
                            runat="server" 
                            ControlToValidate="txtTipoCorrida" 
                            ErrorMessage="*El campo de tipo de corrida es obligatorio" 
                            ForeColor="#660066" 
                            ValidationGroup="vgGuardar">
                        </asp:RequiredFieldValidator>
                    </div>
                </div>

                <div class="row" id="messageBottom">
                    <div class="col d-flex justify-content-start">
                        <asp:Label ID="lblMensaje" runat="server" ForeColor="#660066" Text="Label" Visible="False"></asp:Label>
                    </div>
                </div>

                <div class="row" id="botones">
                    <div class="col d-flex justify-content-end">
                        <asp:Button ID="btnAceptar" runat="server" Text="Aceptar" 
                            onclick="btnAceptar_Click" ViewStateMode="Disabled"/>

                        <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" 
                            onclick="btnCancelar_Click" ViewStateMode="Disabled" />
                    </div>
                </div>
            </div>
            <!--FINAL CONTENIDO DE DATOS-->
        </div>
    </form>

    <script src="https://code.jquery.com/jquery-3.5.1.slim.min.js"
    integrity="sha384-DfXdz2htPH0lsSSs5nCTpuj/zy4C+OGpamoFVy38MVBnE+IbbVYUew+OrCXaRkfj"
    crossorigin="anonymous"></script>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@4.6.0/dist/js/bootstrap.bundle.min.js"
    integrity="sha384-Piv4xVNRyMGpqkS2by6br4gNJ7DXjqk09RmUpJ8jgGtD7zP9yug3goQfGII0yAns"
    crossorigin="anonymous"></script>

</body>
</html>

<style>
    *{
        font-family: 'Century Gothic';
    }

    body{
        margin:0;
        padding:0;
    }

    .navbar{
        height: 100px;
    }

    .logoEmpresa{
        width:100px;
    }

    #navegacionCedes,
    #navegacionEmpleados,
    #navegacionCorridas{
    border-right: 1px solid rgb(108, 113, 117);
    background-color: #212529;
    }

    #navegacionCedes:hover,
    #navegacionEmpleados:hover,
    #navegacionCorridas:hover,
    #navegacionLogOut:hover{
        background-color: rgb(108, 113, 117);
    }

    .titleCorrida{
        padding-top: 1%;
    }

    .titleCorrida,
    .titleAccion{
        padding-bottom: 1%;
        text-align:center;
        color: black;
        font-size: 25px;
        font-weight: bold;
    }

    #ddlCede,
    #txtPuntoInicio,
    #txtPuntoFinal,
    #txtTipoCorrida{
        width:248px;
    }

    #imgPopup{
        width: 25px;
        height: 25px;
    }

    #ddlSexo,
    #ddlStatus,
    #ddlCede,
    #ddlPuesto{
        height:40px;
        border-radius: 6px;
    }

    #btnAceptar,
    #btnCancelar{
        background-color: #014e35;
        color: #D1D1D1;
        border-radius: 6px;
        padding: 1%;
        font-size:15px;
        width: 125px;
    }

    #btnAceptar:hover,
    #btnCancelar:hover{
        background-color: #00724e;
    }

    #btnAceptar{
        margin-right:1.5%;
    }
</style>

