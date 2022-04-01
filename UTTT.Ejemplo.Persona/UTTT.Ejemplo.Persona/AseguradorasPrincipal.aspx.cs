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
    public partial class AseguradorasPrincipal : System.Web.UI.Page
    {
        #region Variables

        private SessionManager session = new SessionManager();
        public static String ultimaExcepcion;

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                Response.Buffer = true;
                DataContext dcTemp = new ManoAmigaSysDataContext();
                
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
                this.session.Pantalla = "~/AseguradorasManager.aspx";
                Hashtable parametrosRagion = new Hashtable();
                parametrosRagion.Add("idAseguradora", "0");
                this.session.Parametros = parametrosRagion;
                this.Session["SessionManager"] = this.session;
                this.Response.Redirect(this.session.Pantalla, false);
            }
            catch (Exception _e)
            {
                this.showMessage("Ha ocurrido un problema al agregar");
            }
        }

        protected void DataSourceAseguradoras_Selecting(object sender, LinqDataSourceSelectEventArgs e)
        {
            try
            {
                DataContext dcConsulta = new ManoAmigaSysDataContext();
                bool valorAseguradoraBool = false;

                if (!this.txtNombreAseguradora.Text.Equals(String.Empty))
                {
                    valorAseguradoraBool = true;
                }

                Expression<Func<UTTT.Ejemplo.Linq.Data.Entity.UniAseguradoras, bool>>
                    predicate =
                    (c =>
                    ((valorAseguradoraBool) ? (((valorAseguradoraBool) ? c.strValor.Contains(this.txtNombreAseguradora.Text.Trim()) : false)) : true) 
                    );

                predicate.Compile();

                List<UTTT.Ejemplo.Linq.Data.Entity.UniAseguradoras> listaAseguradoras =
                    dcConsulta.GetTable<UTTT.Ejemplo.Linq.Data.Entity.UniAseguradoras>().Where(predicate).ToList();
                e.Result = listaAseguradoras;
            }
            catch (Exception _e)
            {
                throw _e;
            }
        }

        protected void dgvAseguradoras_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                int idAseguradora = int.Parse(e.CommandArgument.ToString());
                switch (e.CommandName)
                {
                    case "Editar":
                        this.editar(idAseguradora);
                        break;
                    case "Eliminar":
                        this.eliminar(idAseguradora);
                        break;
                }
            }
            catch (Exception _e)
            {
                this.showMessage("Ha ocurrido un problema al seleccionar");
            }
        }

        #region Metodos

        private void editar(int _idAseguradora)
        {
            try
            {
                Hashtable parametrosRagion = new Hashtable();
                parametrosRagion.Add("idAseguradora", _idAseguradora.ToString());
                this.session.Parametros = parametrosRagion;
                this.Session["SessionManager"] = this.session;
                this.session.Pantalla = String.Empty;
                this.session.Pantalla = "~/AseguradorasManager.aspx";
                this.Response.Redirect(this.session.Pantalla, false);

            }
            catch (Exception _e)
            {
                throw _e;
            }
        }

        private void eliminar(int _idAseguradora)
        {
            try
            {
                DataContext dcDelete = new ManoAmigaSysDataContext();
                UTTT.Ejemplo.Linq.Data.Entity.UniAseguradoras aseguradora = dcDelete.GetTable<UTTT.Ejemplo.Linq.Data.Entity.UniAseguradoras>().First(
                    c => c.idAseguradora == _idAseguradora);
                dcDelete.GetTable<UTTT.Ejemplo.Linq.Data.Entity.UniAseguradoras>().DeleteOnSubmit(aseguradora);
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