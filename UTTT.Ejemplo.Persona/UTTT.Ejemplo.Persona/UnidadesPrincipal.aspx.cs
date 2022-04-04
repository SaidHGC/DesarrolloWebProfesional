using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Linq.Expressions;
using System.Web.UI.WebControls;
using UTTT.Ejemplo.Linq.Data.Entity;
using UTTT.Ejemplo.Persona.Control;
using UTTT.Ejemplo.Persona.Control.Ctrl;

namespace UTTT.Ejemplo.Persona
{
    public partial class UnidadesPrincipal : System.Web.UI.Page
    {
        #region Variables

        private SessionManager session = new SessionManager();
        public static String ultimaExcepcion;

        #endregion

        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                Response.Buffer = true;
                DataContext dcTemp = new ManoAmigaSysDataContext();
                if (!this.IsPostBack)
                {
                    List<UniAseguradoras> lista = dcTemp.GetTable<UniAseguradoras>().ToList();
                    UniAseguradoras catTemp = new UniAseguradoras();
                    catTemp.idAseguradora = -1;
                    catTemp.strValor = "Todos";
                    lista.Insert(0, catTemp);
                    this.ddlAseguradora.DataTextField = "strValor";
                    this.ddlAseguradora.DataValueField = "idAseguradora";
                    this.ddlAseguradora.DataSource = lista;
                    this.ddlAseguradora.DataBind();
                }
            }
            catch (Exception _e)
            {
                this.showMessage("Ha ocurrido un problema al cargar la página");
            }
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                this.DataSourcePersona.RaiseViewChanged();
            }
            catch (Exception _e)
            {
                this.showMessage("Ha ocurrido un problema al buscar");
            }
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                this.session.Pantalla = "~/PersonaManager.aspx";
                Hashtable parametrosRagion = new Hashtable();
                parametrosRagion.Add("idUnidad", "0");
                this.session.Parametros = parametrosRagion;
                this.Session["SessionManager"] = this.session;
                this.Response.Redirect(this.session.Pantalla, false);
            }
            catch (Exception _e)
            {
                this.showMessage("Ha ocurrido un problema al agregar");
            }
        }

        protected void btnMenu_Click(object sender, EventArgs e)
        {
            try
            {
                this.Response.Redirect("~/Menu.aspx", false);
            }
            catch (Exception _e)
            {
                this.showMessage("Ha ocurrido un error inesperado");
            }
        }

        protected void DataSourceUnidades_Selecting(object sender, LinqDataSourceSelectEventArgs e)
        {
            try
            {
                DataContext dcConsulta = new ManoAmigaSysDataContext();
                bool placasBool = false;
                bool aseguradoraBool = false;
                if (!this.txtPlacas.Text.Equals(String.Empty))
                {
                    placasBool = true;
                }
                if (this.ddlAseguradora.Text != "-1")
                {
                    aseguradoraBool = true;
                }

                Expression<Func<UTTT.Ejemplo.Linq.Data.Entity.Unidades, bool>>
                    predicate =
                    (c =>
                    ((aseguradoraBool) ? c.idAseguradora == int.Parse(this.ddlAseguradora.Text) : true) &&
                    ((placasBool) ? (((placasBool) ? c.strPlacas.Contains(this.txtPlacas.Text.Trim()) : false)) : true)
                    );

                predicate.Compile();

                List<UTTT.Ejemplo.Linq.Data.Entity.Unidades> listaPersona =
                    dcConsulta.GetTable<UTTT.Ejemplo.Linq.Data.Entity.Unidades>().Where(predicate).ToList();
                e.Result = listaPersona;
            }
            catch (Exception _e)
            {
                throw _e;
            }
        }

        protected void dgvUnidades_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                int idUnidad = int.Parse(e.CommandArgument.ToString());
                switch (e.CommandName)
                {
                    case "Editar":
                        this.editar(idUnidad);
                        break;
                    case "Eliminar":
                        this.eliminar(idUnidad);
                        break;
                }
            }
            catch (Exception _e)
            {
                this.showMessage("Ha ocurrido un problema al seleccionar");
            }
        }

        #endregion

        #region Metodos

        private void editar(int _idPersona)
        {
            try
            {
                Hashtable parametrosRagion = new Hashtable();
                parametrosRagion.Add("idUnidad", _idPersona.ToString());
                this.session.Parametros = parametrosRagion;
                this.Session["SessionManager"] = this.session;
                this.session.Pantalla = String.Empty;
                this.session.Pantalla = "~/PersonaManager.aspx";
                this.Response.Redirect(this.session.Pantalla, false);

            }
            catch (Exception _e)
            {
                throw _e;
            }
        }

        private void eliminar(int _idPersona)
        {
            try
            {
                DataContext dcDelete = new ManoAmigaSysDataContext();
                UTTT.Ejemplo.Linq.Data.Entity.Unidades unidad = dcDelete.GetTable<UTTT.Ejemplo.Linq.Data.Entity.Unidades>().First(
                    c => c.idUnidad == _idPersona);
                dcDelete.GetTable<UTTT.Ejemplo.Linq.Data.Entity.Unidades>().DeleteOnSubmit(unidad);
                dcDelete.SubmitChanges();
                this.showMessage("El registro se eliminó correctamente.");
                this.DataSourcePersona.RaiseViewChanged();
            }
            catch (Exception _e)
            {
                throw _e;
            }
        }

        protected void onTxtNombreTextChanged(object sender, EventArgs e)
        {
            try
            {
                this.DataSourcePersona.RaiseViewChanged();
            }
            catch (Exception _e)
            {
                this.showMessage("Ha ocurrido un problema al buscar");
            }
        }

        protected void buscarTextBox(object sender, EventArgs e)
        {
            this.DataSourcePersona.RaiseViewChanged();
        }

        #endregion
    }
}