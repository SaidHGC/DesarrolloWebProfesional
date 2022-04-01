<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AseguradorasManager.aspx.cs" Inherits="UTTT.Ejemplo.Persona.AseguradorasManager" debug="true"%>
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
        <div class="titlePersona">Aseguradoras</div>
        <!--FINAL DE TITULO PERSONA-->
        
        <!--INICIO TITULO DE LA ACCION PEDIDA-->
        <div class="titleAccion">
            <asp:Label ID="lblAccion" runat="server" Text="Accion" Font-Bold="True"></asp:Label>
        </div>
        <!--FINAL TITULO DE LA ACCION PEDIDA-->
        
        <div class="container">
            <!--INICIO CONTENIDO DE DATOS-->
            <div class="row" id="datos">

                <div class="row" id="aseguradora">
                    <div class="col d-flex justify-content-end">
                        <label class="p-2">Nombre Aseguradora:</label>
                    </div>
                    <div class="col d-flex justify-content-start">
                        <asp:TextBox 
                            CssClass="form-control" 
                            ID="txtAseguradora" 
                            runat="server" 
                            ViewStateMode="Disabled"
                            onkeypress="return validarLetras(event);">
                        </asp:TextBox> 
                    </div>
                </div>

                <div class="row" id="valAseguradora">
                    <div class="col d-flex justify-content-center">
                        <asp:RequiredFieldValidator 
                            ID="rfvAseguradora" 
                            runat="server" 
                            ErrorMessage="*El campo de Aseguradora es obligatorio" 
                            ForeColor="#660066" 
                            ControlToValidate="txtAseguradora" 
                            ValidationGroup="vgGuardar">
                        </asp:RequiredFieldValidator>
                    </div>
                </div>

                <div class="row" id="descripcion">
                    <div class="col  d-flex justify-content-end">
                        <label class="p-2">Descripción:</label>
                    </div>
                    <div class="col d-flex justify-content-start">
                        <asp:TextBox 
                            CssClass="form-control"
                            ID="txtDescripcion" 
                            runat="server" 
                            ViewStateMode="Disabled">
                        </asp:TextBox>
                    </div>
                </div>

                <div class="row" id="valDescripcion">
                    <div class="col d-flex justify-content-center">
                        <asp:RequiredFieldValidator 
                            ID="rfvDescripcion" 
                            runat="server" 
                            ControlToValidate="txtDescripcion" 
                            ErrorMessage="*La descripcion de la aseguradora es Obligatoria" 
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

    .titlePersona{
        padding-top: 1%;
    }

    .titlePersona,
    .titleAccion{
        padding-bottom: 1%;
        text-align:center;
        color: black;
        font-size: 25px;
        font-weight: bold;
    }

    #ddlSexo,
    #ddlStatus,
    #ddlCede,
    #ddlPuesto,
    #txtNombre,
    #txtAPaterno,
    #txtAMaterno,
    #txtEmail,
    #txtFechaIngreso{
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

    #btnAceptar{
        margin-right:1.5%;
    }
</style>
