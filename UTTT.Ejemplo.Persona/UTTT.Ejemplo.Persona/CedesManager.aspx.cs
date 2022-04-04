using System;
using System.Data.Linq;
using System.Linq;
using System.Text.RegularExpressions;
using UTTT.Ejemplo.Linq.Data.Entity;
using UTTT.Ejemplo.Persona.Control;
using UTTT.Ejemplo.Persona.Control.Ctrl;

namespace UTTT.Ejemplo.Persona
{
    public partial class CedesManager : System.Web.UI.Page
    {

        #region Variables

        private SessionManager session = new SessionManager();
        private int idPersona = 0;
        private UTTT.Ejemplo.Linq.Data.Entity.EmpCede baseEntity;
        //private UTTT.Ejemplo.Linq.Data.Entity.Persona baseEntity;
        private DataContext dcGlobal = new ManoAmigaSysDataContext();
        //private DataContext dcGlobal = new DcGeneralDataContext();
        private int tipoAccion = 0;

        #endregion

        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                this.Response.Buffer = true;
                this.session = (SessionManager)this.Session["SessionManager"];
                this.idPersona = this.session.Parametros["IdCede"] != null ?
                    int.Parse(this.session.Parametros["IdCede"].ToString()) : 0;
                if (this.idPersona == 0)
                {
                    this.baseEntity = new Linq.Data.Entity.EmpCede();
                    this.tipoAccion = 1;
                }
                else
                {
                    this.baseEntity = dcGlobal.GetTable<Linq.Data.Entity.EmpCede>().First(c => c.IdCede == this.idPersona);
                    this.tipoAccion = 2;
                }

                if (!this.IsPostBack)
                {
                    if (this.session.Parametros["baseEntity"] == null)
                    {
                        this.session.Parametros.Add("baseEntity", this.baseEntity);
                    }

                    if (this.idPersona == 0)
                    {
                        this.lblAccion.Text = "Agregar";
                    }
                    else
                    {
                        this.lblAccion.Text = "Editar";
                        this.txtCede.Text = this.baseEntity.strValor;
                        this.txtDescripcion.Text = this.baseEntity.strDescripcion;

                    }
                }

            }
            catch (Exception _e)
            {
                this.showMessage("Ha ocurrido un problema al cargar la página");
                this.Response.Redirect("~/CedesPrincipal.aspx", false);
            }
        }

        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            if (this.txtCede.Text.Trim().Equals("") &&
                this.txtDescripcion.Text.Trim().Equals(""))
            {
                this.Response.Redirect("~/CedesPrincipal.aspx", false);
            }
            else
            {
                btnAceptar.ValidationGroup = "vgGuardar";
                Page.Validate("vgGuardar");
            }
            try
            {
                if (!Page.IsValid)
                {
                    return;
                }

                DataContext dcGuardar = new ManoAmigaSysDataContext();
                UTTT.Ejemplo.Linq.Data.Entity.EmpCede cede = new Linq.Data.Entity.EmpCede();
                //UTTT.Ejemplo.Linq.Data.Entity.Persona persona = new Linq.Data.Entity.Persona();
                if (this.idPersona == 0)
                {
                    cede.strValor = this.txtCede.Text.Trim();
                    cede.strDescripcion = this.txtDescripcion.Text.Trim();

                    String mensaje = String.Empty;
                    //Validacion de datos correctos desde código
                    if (!this.Validacion(cede, ref mensaje))
                    {
                        this.lblMensaje.Text = mensaje;
                        this.lblMensaje.Visible = true;
                        return;
                    }

                    dcGuardar.GetTable<UTTT.Ejemplo.Linq.Data.Entity.EmpCede>().InsertOnSubmit(cede);
                    dcGuardar.SubmitChanges();
                    this.showMessage("El registro se agrego correctamente.");
                    this.Response.Redirect("~/CedesPrincipal.aspx", false);

                }
                if (this.idPersona > 0)
                {
                    cede = dcGuardar.GetTable<UTTT.Ejemplo.Linq.Data.Entity.EmpCede>().First
                                                                        (c => c.IdCede == idPersona);
                    cede.strValor = this.txtCede.Text.Trim();
                    cede.strDescripcion = this.txtDescripcion.Text.Trim();

                    String mensaje = String.Empty;
                    //Validacion de datos correctos desde código
                    if (!this.Validacion(cede, ref mensaje))
                    {
                        this.lblMensaje.Text = mensaje;
                        this.lblMensaje.Visible = true;
                        return;
                    }

                    dcGuardar.SubmitChanges();
                    this.showMessage("El registro se editó correctamente.");
                    this.Response.Redirect("~/CedesPrincipal.aspx", false);
                }
            }
            catch (Exception _e)
            {
                this.showMessageException(_e.Message);
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
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
        #endregion

        #region Metodos

        #region Validacion codigo

        //Validación de datos básicos

        public bool Validacion(UTTT.Ejemplo.Linq.Data.Entity.EmpCede _cede, ref String _mensaje)
        {

            int i = 0;

            //Valida si el nommbre de cede esta vacio
            if (_cede.strValor.Equals(String.Empty))
            {
                _mensaje = "El campo de nombre de cede esta vacio";
                return false;
            }

            //Valida solamente que se ingresen mas de 3 caracteres en el nombre
            if (_cede.strValor.Length < 3)
            {
                _mensaje = "El nombre de la cede necesita ser de al menos 3 caracteres, favor de ingresar un nombre valido";
                return false;
            }

            //Valida solamente que se ingresen menos de 50 caracteres en el nombre
            if (_cede.strValor.Length > 50)
            {
                _mensaje = "El nombre de la cede excede los 50 caracteres, favor de ingresar un nombre valido";
                return false;
            }

            //Validamos que solo se inserten letras en nombre...
            /*
             * RESCATADO DE
             * https://qastack.mx/programming/1181419/verifying-that-a-string-contains-only-letters-in-c-sharp
             */
            if (!Regex.IsMatch(_cede.strValor, @"^[a-zA-ZñÑáéíóúÁÉÍÓÚàèìòùÀÈÌÒÙüïÜÏ ]+$"))
            {
                _mensaje = "El campo del nombre de cede solo acepta letras, favor de insertar caracteres validos";
                return false;
            }

            //Valida si la Descripcion esta vacia
            if (_cede.strDescripcion.Equals(String.Empty))
            {
                _mensaje = "El campo de descripcion esta vacio";
                return false;
            }

            //Valida solamente que se ingresen mas de 3 caracteres en el APaterno
            if (_cede.strDescripcion.Length < 4)
            {
                _mensaje = "La descripción necesita ser de al menos 4 caracteres, favor de ingresar una descripción valida valido";
                return false;
            }

            //Valida solamente que se ingresen menos de 50 caracteres en el APaterno
            if (_cede.strDescripcion.Length > 250)
            {
                _mensaje = "La descripción excede los 250 caracteres, favor de ingresar una descripción valida";
                return false;
            }

            return true;
        }

        #endregion

        #endregion
    }
}