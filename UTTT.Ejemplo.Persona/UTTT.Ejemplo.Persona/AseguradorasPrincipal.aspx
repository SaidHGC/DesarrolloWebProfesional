<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AseguradorasPrincipal.aspx.cs" Inherits="UTTT.Ejemplo.Persona.AseguradorasPrincipal" debug="true"%>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <title>Aseguradoras Principal Nuevo</title>
        <link href="Content\bootstrap.min.css" rel="stylesheet" />
</head>
<body>
    <div class="container">
        <form id="form1" runat="server">
        <div class="scriptManager">
            <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true"></asp:ScriptManager>
        </div>
            <!--INICIO DE TITULO PERSONA-->
            <div class="titlePersona">Aseguradoras</div>
            <!--FINAL DE TITULO PERSONA-->

            <!--INICIO CAMPOS DE NOMBRE Y DROPDOWN DE SEXO ADEMAS DE LOS BOTONES-->
            <div class="row" id="filtros">
                <div class="col-md-4" id="valor">
                    <p class="d-flex">
                        <label class="p-2">Nombre Aseguradora:</label>
                        <asp:TextBox class="form-control" ID="txtNombreAseguradora" runat="server" 
                            ViewStateMode="Disabled" OnTextChanged="buscarTextBox" AutoPostBack="true"></asp:TextBox>

                        <ajaxToolkit:AutoCompleteExtender ID="AutomCompleteExtender1" runat="server" CompletionInterval="100" EnableCaching="false"
                            MinimumPrefixLength="2" ServiceMethod="txtNombre_TextChanged" TargetControlID="txtNombreAseguradora">
                        </ajaxToolkit:AutoCompleteExtender>
                    </p>
                </div>

                <div class="col-md-4" id="botones">
                    <p class="d-flex">
                        
                        <asp:Button ID="btnBuscar" runat="server" Text="Buscar" 
                        onclick="btnBuscar_Click" ViewStateMode="Disabled" class="btn btn-outline-secondary" />

                        <asp:Button ID="btnAgregar" runat="server" Text="Agregar" 
                        onclick="btnAgregar_Click" ViewStateMode="Disabled" class="btn btn-outline-secondary"/>
                        
                    </p>
                </div>
            </div>
            <!--FINAL CAMPOS DE NOMBRE Y DROPDOWN DE SEXO ADEMAS DE LOS BOTONES-->

            <!--INICIO TITULO DETALLE-->
            <div class="titleDetalles">Detalles</div>
            <!--FINAL TITULO DETALLE-->

            <!--INICIO DE LA TABLA-->
            <div id="tablaGde" class="d-none d-md-block">
                <asp:GridView ID="dgvAseguradoras" runat="server" 
                AllowPaging="True" AutoGenerateColumns="False" DataSourceID="DataSourcePersona" 
                CellPadding="3" GridLines="Horizontal" 
                onrowcommand="dgvAseguradoras_RowCommand" BackColor="White" 
                ViewStateMode="Disabled" CssClass="table table-responsive">
                <AlternatingRowStyle BackColor="#F7F7F7" />
                <Columns>
                    <asp:BoundField DataField="strValor" HeaderText="Nombre Aseguradora" ReadOnly="True" 
                        SortExpression="strValor"/>
                    <asp:BoundField DataField="strDescripcion" HeaderText="Descripción" ReadOnly="True" 
                        SortExpression="strDescripcion" />
                    <asp:TemplateField HeaderText="Editar">
                        <ItemTemplate>
                                    <asp:ImageButton runat="server" ID="imgEditar" CommandName="Editar" CommandArgument='<%#Bind("idAseguradora") %>' ImageUrl="~/Images/editrecord_16x16.png" />
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" Width="50px" />
                    
                    </asp:TemplateField>
                     <asp:TemplateField HeaderText="Eliminar" Visible="True">
                            <ItemTemplate>
                                <asp:ImageButton runat="server" ID="imgEliminar" CommandName="Eliminar" CommandArgument='<%#Bind("idAseguradora") %>' ImageUrl="~/Images/delrecord_16x16.png" OnClientClick="javascript:return confirm('¿Está seguro de querer eliminar el registro seleccionado?', 'Mensaje de sistema')"/>
                            </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" Width="50px" />
                            </asp:TemplateField>

<%--                      <asp:TemplateField HeaderText="Direccion">
                        <ItemTemplate>
                                    <asp:ImageButton runat="server" ID="imgDireccion" CommandName="Direccion" CommandArgument='<%#Bind("id") %>' ImageUrl="~/Images/editrecord_16x16.png" />
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" Width="50px" />
                    
                    </asp:TemplateField>--%>
                </Columns>
                <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
                <HeaderStyle BackColor="#4A3C8C" Font-Bold="True" ForeColor="#F7F7F7" />
                <PagerStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Right" />
                <RowStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" />
                <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
                <SortedAscendingCellStyle BackColor="#F4F4FD" />
                <SortedAscendingHeaderStyle BackColor="#5A4C9D" />
                <SortedDescendingCellStyle BackColor="#D8D8F0" />
                <SortedDescendingHeaderStyle BackColor="#3E3277" />
            </asp:GridView>
            </div>

        
            <!--FINAL DE LA TABLA-->

        <asp:LinqDataSource ID="DataSourcePersona" runat="server" 
        ContextTypeName="UTTT.Ejemplo.Linq.Data.Entity.SistemaManoAmigaDataContext" 
        onselecting="DataSourceAseguradoras_Selecting" 
        Select="new (strValor, strDescripcion ,idAseguradora)" 
        TableName="Empleados" EntityTypeName="">
    </asp:LinqDataSource>
    <script src="Scripts/bootstrap.min.js"></script>

            <div class="row">
                <div class="col d-flex justify-content-start">
                    <asp:Button ID="btnRegresar" runat="server" Text="Menú" 
                        onclick="btnMenu_Click" ViewStateMode="Disabled"/>
                </div>
            </div>

        </form>
    </div>
    
</body>
</html>

<style>
    *{
        font-family: 'Century Gothic';
        background-color: #eeeeee;
    }

    body{
        margin:0;
        padding:0;
    }

    .container{
        padding:0px 0px;
        width:100%;
    }

    .titlePersona{
        padding-top: 1%;
    }

    .titlePersona,
    .titleDetalles{
        padding-bottom: 1%;
        text-align:center;
        color: black;
        font-size: 25px;
        font-weight: bold;
    }

    #filtros{
        text-align: center;
        margin: 0;
    }

    #txtNombre{
        width:100%;
    }

    #ddlSexo{
        Width: 100%;
        height:40px;
        border-radius: 6px;
    }

    #txtNombre, #ddlSexo{
        margin-left:5px;
    }

    #name, #sexo, #botones{
        padding-top:1%;
    }

    #btnBuscar{
        margin-left:4%;
    }

    #btnBuscar, #btnAgregar{
        width:100px;
        margin-right:4%;
    }

    #btnRegresar{
        padding: 1%;
        font-size:20px;
        width: 150px;
        background-color: #546E7A;
        color: lightgray;
        border-radius: 6px;
        border: none;
    }

    #btnRegresar:hover{
        background-color:#2E7D32;
        font-weight:600;
    }

/*
    #tablaGde, #tablaCh{
        margin-left:2%;
        margin-right:2%;
    }*/
</style>
