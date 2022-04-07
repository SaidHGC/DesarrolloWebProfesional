using System;
using System.Data.Linq;
using System.Linq;
using UTTT.Ejemplo.Linq.Data.Entity;
using UTTT.Ejemplo.Persona.Control;
using UTTT.Ejemplo.Persona.Control.Ctrl;

namespace UTTT.Ejemplo.Persona
{
    public partial class Menu : System.Web.UI.Page
    {
        #region Variables

        private SessionManager session = new SessionManager();
        private int idPersona = 0;
        private UTTT.Ejemplo.Linq.Data.Entity.Usuarios baseEntity;
        //private UTTT.Ejemplo.Linq.Data.Entity.Persona baseEntity;
        private DataContext dcGlobal = new ManoAmigaSysDataContext();
        //private DataContext dcGlobal = new DcGeneralDataContext();
        private int tipoAccion = 0;

        private int perfil = 2;

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                //perfil = int.Parse(Session["idPerfil"].ToString());
                //switch (perfil)
                //{
                //    case 1:
                //        this.btnEmpleados.Visible = true;
                //        this.btnCedes.Visible = true;
                //        this.btnCorridas.Visible = true;
                //        break;

                //    case 2:
                //        this.btnEmpleados.Visible = false;
                //        this.btnCedes.Visible = false;
                //        this.btnCorridas.Visible = false;
                //        break;

                //    case 3:
                //        this.btnEmpleados.Visible = false;
                //        this.btnCedes.Visible = true;
                //        this.btnCorridas.Visible = true;
                //        break;
                //}
            }
            catch (Exception _e)
            {
                this.showMessage("Ha ocurrido un problema al cargar la página");
                this.Response.Redirect("~/Login.aspx", false);
            }
        }

        protected void btnEmpleados_Click(object sender, EventArgs e)
        {
            try
            {
                this.Response.Redirect("~/PersonaPrincipal.aspx", false);
            }
            catch (Exception _e)
            {
                this.showMessage("Ha ocurrido un error inesperado");
            }
        }

        protected void btnCedes_Click(object sender, EventArgs e)
        {
            try
            {
                this.Response.Redirect("~/CedesPrincipal.aspx", false);
            }
            catch (Exception _e)
            {
                this.showMessage("Ha ocurrido un error inesperado");
            }
        }

        protected void btnCorridas_Click(object sender, EventArgs e)
        {
            try
            {
                this.Response.Redirect("~/CorridasPrincipal.aspx", false);
            }
            catch (Exception _e)
            {
                this.showMessage("Ha ocurrido un error inesperado");
            }
        }

        protected void btnCerrarSesion_Click(object sender, EventArgs e)
        {
            try
            {
                this.Response.Redirect("~/Login.aspx", false);
            }
            catch (Exception _e)
            {
                this.showMessage("Ha ocurrido un error inesperado");
            }
        }
    }
}