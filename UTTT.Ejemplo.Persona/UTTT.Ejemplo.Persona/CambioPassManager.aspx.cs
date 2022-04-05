using System;
using System.Data.Linq;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using UTTT.Ejemplo.Linq.Data.Entity;
using UTTT.Ejemplo.Persona.Control;
using UTTT.Ejemplo.Persona.Control.Ctrl;

namespace UTTT.Ejemplo.Persona
{
    public partial class CambioPassManager : System.Web.UI.Page
    {
        #region Variables

        private SessionManager session = new SessionManager();
        private int idPersona = 0;
        private UTTT.Ejemplo.Linq.Data.Entity.Usuarios baseEntity;
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
                //this.idPersona = this.session.Parametros["idUsuario"] != null ?
                //    int.Parse(this.session.Parametros["idUsuario"].ToString()) : 0;
                if (this.idPersona == 0)
                {
                    this.baseEntity = new Linq.Data.Entity.Usuarios();
                    this.tipoAccion = 1;
                }
                else
                {
                    this.baseEntity = dcGlobal.GetTable<Linq.Data.Entity.Usuarios>().First(c => c.idUsuario == this.idPersona);
                    this.tipoAccion = 2;
                }

                if (!this.IsPostBack)
                {
                    //if (this.session.Parametros["baseEntity"] == null)
                    //{
                    //    this.session.Parametros.Add("baseEntity", this.baseEntity);
                    //}

                    if (this.idPersona == 0)
                    {
                        this.lblAccion.Text = "Cambio Uno";
                    }
                    this.txtUsername.TextChanged += new EventHandler(txtUsername_TextChanged);
                    this.txtUsername.AutoPostBack = true;
                }

            }
            catch (Exception _e)
            {
                this.showMessage("Ha ocurrido un problema al cargar la página");
                this.Response.Redirect("~/Login.aspx", false);
            }
        }

        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            if (
                this.txtUsername.Text.Trim().Equals("") &&
                this.txtNewPassOne.Text.Trim().Equals("") &&
                this.txtNewPassTwo.Text.Trim().Equals(""))
            {
                this.Response.Redirect("~/Login.aspx", false);
            }
            else
            {
                btnAceptar.ValidationGroup = "vgGuardar";
                Page.Validate("vgGuardar");
            }
            try
            {
                if (this.txtNewPassOne.Text == this.txtNewPassTwo.Text)
                {
                    if (!Page.IsValid)
                    {
                        return;
                    }

                    DataContext dcGuardar = new ManoAmigaSysDataContext();
                    UTTT.Ejemplo.Linq.Data.Entity.Usuarios usuario = new Linq.Data.Entity.Usuarios();
                    if (this.idPersona == 0)
                    {
                        //usuario.strUsername = this.txtUsername.Text.Trim();
                        txtNewPassOne.Text = GetSha256(Guid.NewGuid().ToString());
                        usuario.strPassword = this.txtNewPassOne.Text.Trim();

                        String mensaje = String.Empty;
                        //Validacion de datos correctos desde código
                        if (!this.Validacion(usuario, ref mensaje))
                        {
                            this.lblMensaje.Text = mensaje;
                            this.lblMensaje.Visible = true;
                            return;
                        }

                        //dcGuardar.GetTable<UTTT.Ejemplo.Linq.Data.Entity.Usuarios>().InsertOnSubmit(usuario);
                        dcGuardar.SubmitChanges();
                        this.showMessage("El registro se corrigió correctamente.");
                        this.Response.Redirect("~/Login.aspx", false);

                    }
                    if (this.idPersona > 0)
                    {
                        usuario = dcGuardar.GetTable<UTTT.Ejemplo.Linq.Data.Entity.Usuarios>().First
                                                                            (c => c.idUsuario == idPersona);
                        //usuario.strUsername = this.txtUsername.Text.Trim();
                        usuario.strPassword = this.txtNewPassOne.Text.Trim();


                        String mensaje = String.Empty;
                        //Validacion de datos correctos desde código
                        if (!this.Validacion(usuario, ref mensaje))
                        {
                            this.lblMensaje.Text = mensaje;
                            this.lblMensaje.Visible = true;
                            return;
                        }

                        dcGuardar.SubmitChanges();
                        this.showMessage("El registro se editó correctamente.");
                        this.Response.Redirect("~/Login.aspx", false);
                    }
                }
                else
                {
                    this.showMessage("Las contraseñas no coinciden, favor de escribirlas igual");
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
                this.Response.Redirect("~/Login.aspx", false);


            }
            catch (Exception _e)
            {
                this.showMessage("Ha ocurrido un error inesperado");
            }
        }

        protected void txtUsername_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (this.txtUsername.Text.Trim().Equals(""))
                {
                    txtNewPassOne.Enabled = false;
                    txtNewPassTwo.Enabled = false;
                }
                else
                {
                    txtNewPassOne.Enabled = true;
                    txtNewPassTwo.Enabled = true;
                }
            }
            catch (Exception)
            {
                this.showMessage("Ha ocurrido un error inesperado");
            }
        }

        #endregion

        #region Metodos

        //public void setItem(ref DropDownList _control, String _value)
        //{
        //    foreach (ListItem item in _control.Items)
        //    {
        //        if (item.Value == _value)
        //        {
        //            item.Selected = true;
        //            break;
        //        }
        //    }
        //    _control.Items.FindByText(_value).Selected = true;
        //}

        #region Cifrado

        private string GetSha256(string str)
        {
            SHA256 sha256 = SHA256Managed.Create();
            ASCIIEncoding enconding = new ASCIIEncoding();
            byte[] stream = null;
            StringBuilder sb = new StringBuilder();
            stream = sha256.ComputeHash(enconding.GetBytes(str));
            for (int i = 0; i < stream.Length; i++) sb.AppendFormat("{0:x2}", stream[i]);
            return sb.ToString();
        }

        #endregion

        #region Validacion codigo

        //Validación de datos básicos

        public bool Validacion(UTTT.Ejemplo.Linq.Data.Entity.Usuarios _usuario, ref String _mensaje)
        {
            
            //Valida si el password esta vacio
            if (_usuario.strPassword.Equals(String.Empty))
            {
                _mensaje = "El campo de contraseña esta vacio";
                return false;
            }

            //Valida solamente que se ingresen mas de 8 caracteres en el password
            if (_usuario.strPassword.Length < 8)
            {
                _mensaje = "La contraseña necesita ser de al menos 8 caracteres, favor de ingresar una valida";
                return false;
            }

            //Valida solamente que se ingresen menos de 50 caracteres en el APaterno
            if (_usuario.strPassword.Length > 65)
            {
                _mensaje = "Por cuestiones que mejoren su experiencia, la contraseña no debede de exceder los 65 caracteres, favor de ingresar una más corta";
                return false;
            }

            return true;
        }

        #endregion
        #endregion
    }
}