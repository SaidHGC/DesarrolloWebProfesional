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
    public partial class CorridasPrincipal : System.Web.UI.Page
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
                this.session.Pantalla = "~/CorridasManager.aspx";
                Hashtable parametrosRagion = new Hashtable();
                parametrosRagion.Add("idCorrida", "0");
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

        protected void DataSourceCorridas_Selecting(object sender, LinqDataSourceSelectEventArgs e)
        {
            try
            {
                DataContext dcConsulta = new ManoAmigaSysDataContext();
                bool valorPInicioBool = false;
                bool valorPFinalBool = false;

                if (!this.txtPuntoInicio.Text.Equals(String.Empty))
                {
                    valorPInicioBool = true;
                }

                if (!this.txtPuntoLlegada.Text.Equals(String.Empty))
                {
                    valorPFinalBool = true;
                }

                Expression<Func<UTTT.Ejemplo.Linq.Data.Entity.Corridas, bool>>
                    predicate =
                    (c =>
                    ((valorPInicioBool) ? (((valorPInicioBool) ? c.strPuntoInicio.Contains(this.txtPuntoInicio.Text.Trim()) : false)) : true) &&
                    ((valorPFinalBool) ? (((valorPFinalBool) ? c.strPuntoFinal.Contains(this.txtPuntoLlegada.Text.Trim()) : false)) : true)
                    );

                predicate.Compile();

                List<UTTT.Ejemplo.Linq.Data.Entity.Corridas> listaCorridas =
                    dcConsulta.GetTable<UTTT.Ejemplo.Linq.Data.Entity.Corridas>().Where(predicate).ToList();
                e.Result = listaCorridas;
            }
            catch (Exception _e)
            {
                throw _e;
                throw _e;
            }
        }

        protected void dgvCorridas_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                int idCorrida = int.Parse(e.CommandArgument.ToString());
                switch (e.CommandName)
                {
                    case "Editar":
                        this.editar(idCorrida);
                        break;
                    case "Eliminar":
                        this.eliminar(idCorrida);
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

        private void editar(int _idCorrida)
        {
            try
            {
                Hashtable parametrosRagion = new Hashtable();
                parametrosRagion.Add("idCorrida", _idCorrida.ToString());
                this.session.Parametros = parametrosRagion;
                this.Session["SessionManager"] = this.session;
                this.session.Pantalla = String.Empty;
                this.session.Pantalla = "~/CorridasManager.aspx";
                this.Response.Redirect(this.session.Pantalla, false);

            }
            catch (Exception _e)
            {
                throw _e;
            }
        }

        private void eliminar(int _idCorrida)
        {
            try
            {
                DataContext dcDelete = new ManoAmigaSysDataContext();
                UTTT.Ejemplo.Linq.Data.Entity.Corridas corrida = dcDelete.GetTable<UTTT.Ejemplo.Linq.Data.Entity.Corridas>().First(
                    c => c.idCorrida == _idCorrida);
                dcDelete.GetTable<UTTT.Ejemplo.Linq.Data.Entity.Corridas>().DeleteOnSubmit(corrida);
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