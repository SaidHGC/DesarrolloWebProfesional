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
    public partial class CedesPrincipal : System.Web.UI.Page
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
                this.session.Pantalla = "~/CedesManager.aspx";
                Hashtable parametrosRagion = new Hashtable();
                parametrosRagion.Add("IdCede", "0");
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

        protected void DataSourceCede_Selecting(object sender, LinqDataSourceSelectEventArgs e)
        {
            try
            {
                DataContext dcConsulta = new ManoAmigaSysDataContext();
                bool cedeBool = false;

                if (!this.txtCede.Text.Equals(String.Empty))
                {
                    cedeBool = true;
                }

                Expression<Func<UTTT.Ejemplo.Linq.Data.Entity.EmpCede, bool>>
                    predicate =
                    (c =>
                    ((cedeBool) ? (((cedeBool) ? c.strValor.Contains(this.txtCede.Text.Trim()) : false)) : true)
                    );

                predicate.Compile();

                List<UTTT.Ejemplo.Linq.Data.Entity.EmpCede> listaCedes =
                    dcConsulta.GetTable<UTTT.Ejemplo.Linq.Data.Entity.EmpCede>().Where(predicate).ToList();
                e.Result = listaCedes;
            }
            catch (Exception _e)
            {
                throw _e;
            }
        }

        protected void dgvCede_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                int idCede = int.Parse(e.CommandArgument.ToString());
                switch (e.CommandName)
                {
                    case "Editar":
                        this.editar(idCede);
                        break;
                    case "Eliminar":
                        this.eliminar(idCede);
                        break;
                }
            }
            catch (Exception _e)
            {
                this.showMessage("Ha ocurrido un problema al seleccionar");
            }
        }

        #region Metodos

        private void editar(int _idCede)
        {
            try
            {
                Hashtable parametrosRagion = new Hashtable();
                parametrosRagion.Add("IdCede", _idCede.ToString());
                this.session.Parametros = parametrosRagion;
                this.Session["SessionManager"] = this.session;
                this.session.Pantalla = String.Empty;
                this.session.Pantalla = "~/CedesManager.aspx";
                this.Response.Redirect(this.session.Pantalla, false);

            }
            catch (Exception _e)
            {
                throw _e;
            }
        }

        private void eliminar(int _idCede)
        {
            try
            {
                DataContext dcDelete = new ManoAmigaSysDataContext();
                UTTT.Ejemplo.Linq.Data.Entity.EmpCede cede = dcDelete.GetTable<UTTT.Ejemplo.Linq.Data.Entity.EmpCede>().First(
                    c => c.IdCede == _idCede);
                dcDelete.GetTable<UTTT.Ejemplo.Linq.Data.Entity.EmpCede>().DeleteOnSubmit(cede);
                dcDelete.SubmitChanges();
                this.showMessage("El registro se eliminó correctamente.");
                this.DataSourcePersona.RaiseViewChanged();
            }
            catch (Exception _e)
            {
                throw _e;
            }
        }

        protected void onTxtCedeTextChanged(object sender, EventArgs e)
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