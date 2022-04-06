<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CedesPrincipal.aspx.cs" Inherits="UTTT.Ejemplo.Persona.CedesPrincipal" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <title>Cedes Principal Nuevo</title>
        <link href="Content\bootstrap.min.css" rel="stylesheet" />
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

    <div class="container">
        <form id="form1" runat="server">
        <div class="scriptManager">
            <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true"></asp:ScriptManager>
        </div>
            <!--INICIO DE TITULO PERSONA-->
            <div class="titlePersona">Cedes</div>
            <!--FINAL DE TITULO PERSONA-->

            <!--INICIO CAMPOS DE NOMBRE Y DROPDOWN DE SEXO ADEMAS DE LOS BOTONES-->
            <div class="row d-flex justify-content-end" id="filtros">
                <div class="col-md-4" id="valor">
                    <p class="d-flex">
                        <label id="lblCede" class="p-2">Cede:</label>
                        <asp:TextBox class="form-control" ID="txtCede" runat="server" 
                            ViewStateMode="Disabled" OnTextChanged="buscarTextBox" AutoPostBack="true"></asp:TextBox>

                        <ajaxToolkit:AutoCompleteExtender ID="AutomCompleteExtender1" runat="server" CompletionInterval="100" EnableCaching="false"
                            MinimumPrefixLength="2" ServiceMethod="txtCede_TextChanged" TargetControlID="txtCede">
                        </ajaxToolkit:AutoCompleteExtender>
                    </p>
                </div>

                <div class="col-md-4 d-flex justify-content-around" id="botones">
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
            <div id="tablaGde" class="table table-responsive">
                <asp:GridView ID="dgvCede" runat="server" 
                AllowPaging="True" AutoGenerateColumns="False" DataSourceID="DataSourcePersona" 
                CellPadding="3" GridLines="Horizontal" 
                onrowcommand="dgvCede_RowCommand" BackColor="White" 
                ViewStateMode="Disabled" CssClass="table table-responsive">
                <AlternatingRowStyle BackColor="#F7F7F7" />
                <Columns>
                    <asp:BoundField DataField="strValor" HeaderText="Cede" ReadOnly="True" 
                        SortExpression="strValor"/>
                    <asp:BoundField DataField="strDescripcion" HeaderText="Descripción" ReadOnly="True" 
                        SortExpression="strDescripcion" />
                    <asp:TemplateField HeaderText="Editar">
                        <ItemTemplate>
                                    <asp:ImageButton runat="server" ID="imgEditar" CommandName="Editar" CommandArgument='<%#Bind("idCede") %>' ImageUrl="~/Images/editrecord_16x16.png" />
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" Width="50px" />
                    
                    </asp:TemplateField>
                     <asp:TemplateField HeaderText="Eliminar" Visible="True">
                            <ItemTemplate>
                                <asp:ImageButton runat="server" ID="imgEliminar" CommandName="Eliminar" CommandArgument='<%#Bind("idCede") %>' ImageUrl="~/Images/delrecord_16x16.png" OnClientClick="javascript:return confirm('¿Está seguro de querer eliminar el registro seleccionado?', 'Mensaje de sistema')"/>
                            </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" Width="50px" />
                            </asp:TemplateField>

                </Columns>
                <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
                <HeaderStyle BackColor="#666666" Font-Bold="True" ForeColor="#F7F7F7" />
                <PagerStyle BackColor="#b0b0b0" ForeColor="#424242" HorizontalAlign="Right" />
                <RowStyle BackColor="#b0b0b0" ForeColor="#424242" Font-Bold="True"/>
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
        onselecting="DataSourceCede_Selecting" 
        Select="new (strValor, strDescripcion ,IdCede)" 
        TableName="Cedes" EntityTypeName="">
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

    #lblCede{
        text-align:right;
    }

    #txtNombre{
        width:100%;
    }

    #txtCede{
        Width: 100%;
        border-radius: 6px;
    }

    #txtCede{
        margin-left:5px;
    }

    #valor,#botones{
        padding-top:1%;
    }

    #btnBuscar{
        margin-left:4%;
    }

    #btnBuscar, #btnAgregar{
        margin-right:5%;
        background-color: #014e35;
        color: #D1D1D1;

        padding: 1%;
        font-size:20px;
        width: 150px;
    }

    #btnBuscar:hover, 
    #btnAgregar:hover{
        background-color: #00724e;
    }

    #btnRegresar{
        padding: 1%;
        font-size:20px;
        width: 150px;
        background-color: #014e35;
        color: #D1D1D1;
        border-radius: 6px;
        border: none;
    }

    #btnRegresar:hover{
        background-color: #00724e;
        font-weight:600;
    }

/*
    #tablaGde, #tablaCh{
        margin-left:2%;
        margin-right:2%;
    }*/
</style>

