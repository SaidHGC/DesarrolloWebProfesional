<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UsuariosManager.aspx.cs" Inherits="UTTT.Ejemplo.Persona.UusariosManager" %>
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
        <div class="titleUser">Usuarios</div>
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
                        <asp:TextBox CssClass="form-control" ID="txtUsername" runat="server" ViewStateMode="Disabled">
                        </asp:TextBox> 
                    </div>
                </div>

                <div class="row" id="valNombre">
                    <div class="col d-flex justify-content-center">
                        <asp:RequiredFieldValidator ID="rfvNombre" runat="server" ErrorMessage="*El campo de nombre obligatorio" ForeColor="#660066" ControlToValidate="txtUsername" ValidationGroup="vgGuardar"></asp:RequiredFieldValidator>
                    </div>
                </div>

                <div class="row" id="passOne">
                    <div class="col  d-flex justify-content-end">
                        <label class="p-2">Contraseña:</label>
                    </div>
                    <div class="col d-flex justify-content-start">
                        <asp:TextBox 
                                CssClass="form-control"
                                ID="txtPassOne" runat="server" ViewStateMode="Disabled"
                                TextMode="Password">
                        </asp:TextBox>
                    </div>
                </div>

                <div class="row" id="valPassOne">
                    <div class="col d-flex justify-content-center">
                        <asp:RequiredFieldValidator ID="rfvPassOne" runat="server" ControlToValidate="txtPassOne" ErrorMessage="*La contraseña es Obligatorio" ForeColor="#660066" ValidationGroup="vgGuardar"></asp:RequiredFieldValidator>
                    </div>
                </div>

                <div class="row" id="passTwo">
                    <div class="col  d-flex justify-content-end">
                        <label class="p-2">Confirmar contraseña:</label>
                    </div>
                    <div class="col d-flex justify-content-start">
                        <asp:TextBox CssClass="form-control" ID="txtPassTwo" runat="server"
                                ViewStateMode="Disabled" TextMode="Password">
                        </asp:TextBox>
                    </div>
                </div>

                <div class="row" id="valPassTwo">
                    <div class="col d-flex justify-content-center">
                        <asp:RequiredFieldValidator ID="rfvPassTwo" runat="server" ControlToValidate="txtPassTwo" ErrorMessage="*Esta confirmación es obligatoria" ForeColor="#660066" ValidationGroup="vgGuardar"></asp:RequiredFieldValidator>
                    </div>
                </div>

                <div class="row" id="status">
                    <div class="col  d-flex justify-content-end">
                        <label class="p-2">Status:</label>
                    </div>
                    <div class="col d-flex justify-content-start">

                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                            <ContentTemplate>
                            <asp:DropDownList ID="ddlStatus" runat="server"
                                OnSelectedIndexChanged="ddlStatus_SelectedIndexChanged">
                            </asp:DropDownList>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="ddlStatus" EventName="SelectedIndexChanged" />
                            </Triggers>
                        </asp:UpdatePanel>

                    </div>
                </div>

                <div class="row" id="valStatus">
                    <div class="col d-flex justify-content-center">
                        <asp:RequiredFieldValidator ID="rfvStatus" runat="server" ControlToValidate="ddlStatus" ErrorMessage="*Seleccionar un Status para el usuario" ForeColor="#660066" InitialValue="-1" ValidationGroup="vgGuardar"></asp:RequiredFieldValidator>
                    </div>
                </div>

                <div class="row" id="perfil">
                    <div class="col  d-flex justify-content-end">
                        <label class="p-2">Perfil / Puesto:</label>
                    </div>
                    <div class="col d-flex justify-content-start">

                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                            <asp:DropDownList ID="ddlPerfil" runat="server"
                                OnSelectedIndexChanged="ddlPerfil_SelectedIndexChanged">
                            </asp:DropDownList>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="ddlPerfil" EventName="SelectedIndexChanged" />
                            </Triggers>
                        </asp:UpdatePanel>

                    </div>
                </div>

                <div class="row" id="valPerfil">
                    <div class="col d-flex justify-content-center">
                        <asp:RequiredFieldValidator ID="rfvPerfil" runat="server" ControlToValidate="ddlPerfil" ErrorMessage="*Seleccionar un Perfil para el usuario" ForeColor="#660066" InitialValue="-1" ValidationGroup="vgGuardar"></asp:RequiredFieldValidator>
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
    #navegacionCorridas:hover{
        background-color: rgb(108, 113, 117);
    }

    .titleUser{
        padding-top: 1%;
    }

    .titleUser,
    .titleAccion{
        padding-bottom: 1%;
        text-align:center;
        color: black;
        font-size: 25px;
        font-weight: bold;
    }

    #ddlStatus
    #ddlPerfil,
    #txtUsername,
    #txtPassOne,
    #txtPassTwo{
        width:248px;
    }

    #ddlStatus,
    #ddlPerfil{
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
