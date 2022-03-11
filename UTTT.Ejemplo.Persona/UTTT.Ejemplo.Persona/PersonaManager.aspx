<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PersonaManager.aspx.cs" Inherits="UTTT.Ejemplo.Persona.PersonaManager" debug=false%>

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
        <!--INICIO DE TITULO PERSONA-->
        <div class="titlePersona">Persona</div>
        <!--FINAL DE TITULO PERSONA-->
        
        <!--INICIO TITULO DE LA ACCION PEDIDA-->
        <div class="titleAccion">
            <asp:Label ID="lblAccion" runat="server" Text="Accion" Font-Bold="True"></asp:Label>
        </div>
        <!--FINAL TITULO DE LA ACCION PEDIDA-->
        
        <div class="container">
            <!--INICIO CONTENIDO DE DATOS-->
            <div class="row" id="datos">
                <div class="row" id="sex">
                    <div class="col  d-flex justify-content-end">
                        <label class="p-2">Sexo:</label>
                    </div>
                    <div class="col d-flex justify-content-start">
                        <asp:DropDownList ID="ddlSexo" runat="server"></asp:DropDownList>
                    </div>
                </div>

                <div class="row" id="valSex">
                    <div class="col d-flex justify-content-center">
                        <asp:RequiredFieldValidator ID="rfvSexo" runat="server" ControlToValidate="ddlSexo" ErrorMessage="*Seleccionar un Sexo" ForeColor="#660066" InitialValue="-1" ValidationGroup="vgGuardar"></asp:RequiredFieldValidator>
                    </div>
                </div>

                <div class="row" id="clave">
                    <div class="col  d-flex justify-content-end">
                        <label class="p-2">Clave Unica:</label>
                    </div>
                    <div class="col d-flex justify-content-start">
                        <asp:TextBox CssClass="form-control" ID="txtClaveUnica" runat="server" 
                                ViewStateMode="Disabled"
                                onkeypress="return validaNumero(event);" pattern=".{1,3}"
                                title="1 a 3 es la longitud permitida a ingresar">
                        </asp:TextBox>
                    </div>
                </div>

                <div class="row" id="rangoClave">
                    <div class="col d-flex justify-content-center">
                        <asp:RangeValidator ID="rvClaveUnica" runat="server" ControlToValidate="txtClaveUnica" ErrorMessage="*La Clave debe de ser entre 1 y 999" ForeColor="#660066" MaximumValue="999" MinimumValue="1" Type="Integer" ValidationGroup="vgGuardar"></asp:RangeValidator> 
                    </div>
                </div>

                <div class="row" id="valClave">
                    <div class="col d-flex justify-content-center">
                        <asp:RequiredFieldValidator ID="rfvClaveUnica" runat="server" ControlToValidate="txtClaveUnica" ErrorMessage="*El campo de clave unica es Obligatorio" ForeColor="#660066" ValidationGroup="vgGuardar"></asp:RequiredFieldValidator>
                    </div>
                </div>

                <div class="row" id="nombre">
                    <div class="col d-flex justify-content-end">
                        <label class="p-2">Nombre:</label>
                    </div>
                    <div class="col d-flex justify-content-start">
                        <asp:TextBox CssClass="form-control" ID="txtNombre" runat="server" ViewStateMode="Disabled"
                                onkeypress="return validarLetras(event);">
                        </asp:TextBox> 
                    </div>
                </div>

                <div class="row" id="valNombre">
                    <div class="col d-flex justify-content-center">
                        <asp:RequiredFieldValidator ID="rfvNombre" runat="server" ErrorMessage="*El campo de nombre obligatorio" ForeColor="#660066" ControlToValidate="txtNombre" ValidationGroup="vgGuardar"></asp:RequiredFieldValidator>
                    </div>
                </div>

                <div class="row" id="apPaterno">
                    <div class="col  d-flex justify-content-end">
                        <label class="p-2">A Paterno:</label>
                    </div>
                    <div class="col d-flex justify-content-start">
                        <asp:TextBox 
                                CssClass="form-control"
                                ID="txtAPaterno" runat="server" ViewStateMode="Disabled"
                                onkeypress="return validarLetras(event);">
                        </asp:TextBox>
                    </div>
                </div>

                <div class="row" id="valApPaterno">
                    <div class="col d-flex justify-content-center">
                        <asp:RequiredFieldValidator ID="rfvAPaterno" runat="server" ControlToValidate="txtAPaterno" ErrorMessage="*El apellido paterno es Obligatorio" ForeColor="#660066" ValidationGroup="vgGuardar"></asp:RequiredFieldValidator>
                    </div>
                </div>

                <div class="row" id="apMaterno">
                    <div class="col  d-flex justify-content-end">
                        <label class="p-2">A Materno:</label>
                    </div>
                    <div class="col d-flex justify-content-start">
                        <asp:TextBox CssClass="form-control" ID="txtAMaterno" runat="server"
                                ViewStateMode="Disabled"
                                onkeypress="return validarLetras(event);">
                        </asp:TextBox>
                    </div>
                </div>

                <div class="row" id="valApMaterno">
                    <div class="col d-flex justify-content-center">
                        <asp:RequiredFieldValidator ID="rfvAMaterno" runat="server" ControlToValidate="txtAMaterno" ErrorMessage="*El apellido materno es Obligatorio" ForeColor="#660066" ValidationGroup="vgGuardar"></asp:RequiredFieldValidator>
                    </div>
                </div>

                <div class="row" id="curp">
                    <div class="col  d-flex justify-content-end">
                        <label class="p-2">CURP:</label>
                    </div>
                    <div class="col d-flex justify-content-start">
                        <asp:TextBox CssClass="form-control" ID="txtCurp" runat="server"></asp:TextBox>
                    </div>
                </div>

                <div class="row" id="tipoCurp">
                    <div class="col d-flex justify-content-center">
                        <asp:RegularExpressionValidator ID="revCURP" runat="server" ControlToValidate="txtCurp" ErrorMessage="*CURP es incorrecto" ForeColor="#660066" ValidationExpression="^[A-Z]{1}[AEIOU]{1}[A-Z]{2}[0-9]{2}(0[1-9]|1[0-2])(0[1-9]|1[0-9]|2[0-9]|3[0-1])[HM]{1}(AS|BC|BS|CC|CS|CH|CL|CM|DF|DG|GT|GR|HG|JC|MC|MN|MS|NT|NL|OC|PL|QT|QR|SP|SL|SR|TC|TS|TL|VZ|YN|ZS|NE)[B-DF-HJ-NP-TV-Z]{3}[0-9A-Z]{1}[0-9]{1}$" ValidationGroup="vgGuardar"></asp:RegularExpressionValidator>
                    </div>
                </div>

                <div class="row" id="valCurp">
                    <div class="col d-flex justify-content-center">
                        <asp:RequiredFieldValidator ID="rfvCURP" runat="server" ControlToValidate="txtCurp" ErrorMessage="*El CURP es Obligatorio" ForeColor="#660066" ValidationGroup="vgGuardar"></asp:RequiredFieldValidator>
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

    #txtClaveUnica, 
    #ddlSexo,
    #txtNombre,
    #txtAPaterno,
    #txtAMaterno,
    #txtCurp{
        width:248px;
    }

    #ddlSexo{
        height:40px;
        border-radius: 6px;
    }

    #btnAceptar{
        margin-right:1.5%;
    }
</style>