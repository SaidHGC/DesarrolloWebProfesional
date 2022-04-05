<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CambioPassManager.aspx.cs" Inherits="UTTT.Ejemplo.Persona.CambioPassManager" debug="true"%>
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

    <form id="form1" runat="server">
        <div class="scriptManager">
            <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true"></asp:ScriptManager>
        </div>
        <!--INICIO DE TITULO PERSONA-->
        <div class="titleRecuperar">Recuperar Contraseña</div>
        <!--FINAL DE TITULO PERSONA-->
        
        <!--INICIO TITULO DE LA ACCION PEDIDA-->
        <div class="titleAccion">
            <asp:Label ID="lblAccion" runat="server" Text="Accion" Font-Bold="True"></asp:Label>
        </div>
        <!--FINAL TITULO DE LA ACCION PEDIDA-->
        
        <div class="container">
            <!--INICIO CONTENIDO DE DATOS-->
            <div class="row" id="datos">

                <div class="row" id="username">
                    <div class="col d-flex justify-content-end">
                        <label class="p-2">Nombre de usuario:</label>
                    </div>
                    <div class="col d-flex justify-content-start">
                        <asp:TextBox CssClass="form-control" ID="txtUsername" 
                            runat="server" ViewStateMode="Disabled"
                            OnTextChanged="txtUsername_TextChanged">
                        </asp:TextBox> 
                    </div>
                </div>

                <div class="row" id="valNombre">
                    <div class="col d-flex justify-content-center">
                        <asp:RequiredFieldValidator ID="rfvNombre" runat="server" ErrorMessage="*El campo de nombre de usuario obligatorio" ForeColor="#660066" ControlToValidate="txtUsername" ValidationGroup="vgGuardar"></asp:RequiredFieldValidator>
                    </div>
                </div>

                <div class="row" id="passNew">
                    <div class="col  d-flex justify-content-end">
                        <label class="p-2">Nueva Contraseña:</label>
                    </div>
                    <div class="col d-flex justify-content-start">
                        <asp:TextBox 
                                CssClass="form-control"
                                ID="txtNewPassOne" runat="server" ViewStateMode="Disabled"
                                TextMode="Password"
                                Enabled="false">
                        </asp:TextBox>
                    </div>
                </div>

                <div class="row" id="valNewPassOne">
                    <div class="col d-flex justify-content-center">
                        <asp:RequiredFieldValidator ID="rfvNewPassOne" runat="server" ControlToValidate="txtNewPassOne" ErrorMessage="*La contraseña es Obligatoria" ForeColor="#660066" ValidationGroup="vgGuardar"></asp:RequiredFieldValidator>
                    </div>
                </div>

                <div class="row" id="newPassTwo">
                    <div class="col  d-flex justify-content-end">
                        <label class="p-2">Confirmar contraseña:</label>
                    </div>
                    <div class="col d-flex justify-content-start">
                        <asp:TextBox CssClass="form-control" ID="txtNewPassTwo" runat="server"
                                ViewStateMode="Disabled" TextMode="Password" Enabled="false">
                        </asp:TextBox>
                    </div>
                </div>

                <div class="row" id="valNewPassTwo">
                    <div class="col d-flex justify-content-center">
                        <asp:RequiredFieldValidator ID="rfvNewPassTwo" runat="server" ControlToValidate="txtNewPassTwo" ErrorMessage="*Esta confirmación es obligatoria" ForeColor="#660066" ValidationGroup="vgGuardar"></asp:RequiredFieldValidator>
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

    .titleRecuperar{
        padding-top: 1%;
    }

    .titleRecuperar,
    .titleAccion{
        padding-bottom: 1%;
        text-align:center;
        color: black;
        font-size: 25px;
        font-weight: bold;
    }

    #txtUsername,
    #txtNewPassOne,
    #txtNewPassTwo{
        width:248px;
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
