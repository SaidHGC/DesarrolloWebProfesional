using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography;
using System.Text;
using System.Web.UI.WebControls;
using UTTT.Ejemplo.Linq.Data.Entity;
using UTTT.Ejemplo.Persona.Control;
using UTTT.Ejemplo.Persona.Control.Ctrl;

namespace UTTT.Ejemplo.Persona
{
    public partial class UusariosManager : System.Web.UI.Page
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
                    List<UsStatus> listaStatus = dcGlobal.GetTable<UsStatus>().ToList();
                    List<UsPerfil> listaPerfil = dcGlobal.GetTable<UsPerfil>().ToList();

                    this.ddlStatus.DataTextField = "strValor";
                    this.ddlStatus.DataValueField = "idStatus";

                    this.ddlPerfil.DataTextField = "strValor";
                    this.ddlPerfil.DataValueField = "idPerfil";

                    if (this.idPersona == 0)
                    {
                        this.lblAccion.Text = "Agregar";

                        UsStatus caetTempStatus = new UsStatus();
                        caetTempStatus.idStatus = -1;
                        caetTempStatus.strValor = "Seleccionar";
                        listaStatus.Insert(0, caetTempStatus);
                        this.ddlStatus.DataSource = listaStatus;
                        this.ddlStatus.DataBind();

                        UsPerfil catTempPerfil = new UsPerfil();
                        catTempPerfil.idPerfil = -1;
                        catTempPerfil.strValor = "Seleccionar";
                        listaPerfil.Insert(0, catTempPerfil);
                        this.ddlPerfil.DataSource = listaPerfil;
                        this.ddlPerfil.DataBind();
                    }
                    else
                    {
                        this.lblAccion.Text = "Editar";
                        this.txtUsername.Text = this.baseEntity.strUsername;
                        this.txtPassOne.Text = this.baseEntity.strPassword;

                        this.ddlStatus.DataSource = listaStatus;
                        this.ddlStatus.DataBind();

                        this.ddlPerfil.DataSource = listaPerfil;
                        this.ddlPerfil.DataBind();

                        this.setItem(ref this.ddlStatus, baseEntity.UsStatus.strValor);
                        this.setItem(ref this.ddlPerfil, baseEntity.UsPerfil.strValor);

                    }

                    this.ddlStatus.SelectedIndexChanged += new EventHandler(ddlStatus_SelectedIndexChanged);
                    this.ddlStatus.AutoPostBack = true;

                    this.ddlPerfil.SelectedIndexChanged += new EventHandler(ddlPerfil_SelectedIndexChanged);
                    this.ddlPerfil.AutoPostBack = true;
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
            if (this.txtUsername.Text.Trim().Equals("") &&
                this.txtPassOne.Text.Trim().Equals("") &&
                this.txtPassTwo.Text.Trim().Equals("") &&
                int.Parse(this.ddlStatus.Text).Equals(-1) &&
                int.Parse(this.ddlPerfil.Text).Equals(-1))
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
                if (this.txtPassOne.Text == this.txtPassTwo.Text)
                {
                    if (!Page.IsValid)
                    {
                        return;
                    }

                    DataContext dcGuardar = new ManoAmigaSysDataContext();
                    UTTT.Ejemplo.Linq.Data.Entity.Usuarios usuario = new Linq.Data.Entity.Usuarios();

                    string usuarioTemp = this.txtUsername.Text;

                    using (dcGlobal = new ManoAmigaSysDataContext())
                    {
                        List<Usuarios> listaUsuariosUsername =
                            dcGlobal.GetTable<Usuarios>().Where(c => (c.strUsername.Equals(usuarioTemp))).ToList();

                        if (listaUsuariosUsername.Count > 0)
                        {
                            this.showMessage("El usuario ya existe, favor de ingresar otro que sea valido");
                        }
                        else
                        {
                            //DataContext dcGuardar = new ManoAmigaSysDataContext();
                            //UTTT.Ejemplo.Linq.Data.Entity.Usuarios usuario = new Linq.Data.Entity.Usuarios();
                            if (this.idPersona == 0)
                            {
                                usuario.strUsername = this.txtUsername.Text.Trim();
                                //encriptamos la cadena inicial       
                                txtPassOne.Text = Seguridad.Encriptar(txtPassOne.Text);
                                usuario.strPassword = this.txtPassOne.Text.Trim();
                                usuario.idStatus = int.Parse(this.ddlStatus.Text);
                                usuario.idPerfil = int.Parse(this.ddlPerfil.Text);

                                String mensaje = String.Empty;
                                //Validacion de datos correctos desde código
                                if (!this.Validacion(usuario, ref mensaje))
                                {
                                    this.lblMensaje.Text = mensaje;
                                    this.lblMensaje.Visible = true;
                                    return;
                                }

                                dcGuardar.GetTable<UTTT.Ejemplo.Linq.Data.Entity.Usuarios>().InsertOnSubmit(usuario);
                                dcGuardar.SubmitChanges();
                                this.showMessage("El registro se agrego correctamente.");
                                this.Response.Redirect("~/Login.aspx", false);

                            }
                            if (this.idPersona > 0)
                            {
                                usuario = dcGuardar.GetTable<UTTT.Ejemplo.Linq.Data.Entity.Usuarios>().First
                                                                                    (c => c.idUsuario == idPersona);
                                usuario.strUsername = this.txtUsername.Text.Trim();
                                usuario.strPassword = this.txtPassOne.Text.Trim();
                                usuario.idStatus = int.Parse(this.ddlStatus.Text);
                                usuario.idPerfil = int.Parse(this.ddlPerfil.Text);


                                String mensaje = String.Empty;
                                //Validacion de datos correctos desde código
                                if (!this.Validacion(usuario, ref mensaje))
                                {
                                    this.lblMensaje.Text = mensaje;
                                    this.lblMensaje.Visible = true;
                                    return;
                                }

                                dcGuardar.SubmitChanges();
                                this.showMessage("El registro se edito correctamente.");
                                this.Response.Redirect("~/Login.aspx", false);
                            }
                        }
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

        protected void ddlStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int idStatus = int.Parse(this.ddlStatus.Text);
                Expression<Func<UsStatus, bool>> predicateStatus = c => c.idStatus == idStatus;
                predicateStatus.Compile();
                List<UsStatus> lista = dcGlobal.GetTable<UsStatus>().Where(predicateStatus).ToList();
                UsStatus catTempStatus = new UsStatus();
                this.ddlStatus.DataTextField = "strValor";
                this.ddlStatus.DataValueField = "idStatus";
                this.ddlStatus.DataSource = lista;
                this.ddlStatus.DataBind();
            }
            catch (Exception)
            {
                this.showMessage("Ha ocurrido un error inesperado");
            }
        }

        protected void ddlPerfil_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int idPerfil = int.Parse(this.ddlPerfil.Text);
                Expression<Func<UsPerfil, bool>> predicatePerfil = c => c.idPerfil == idPerfil;
                predicatePerfil.Compile();
                List<UsPerfil> lista = dcGlobal.GetTable<UsPerfil>().Where(predicatePerfil).ToList();
                UsPerfil catTempPerfil = new UsPerfil();
                this.ddlPerfil.DataTextField = "strValor";
                this.ddlPerfil.DataValueField = "idPerfil";
                this.ddlPerfil.DataSource = lista;
                this.ddlPerfil.DataBind();
            }
            catch (Exception)
            {
                this.showMessage("Ha ocurrido un error inesperado");
            }
        }

        #endregion

        #region Metodos

        public void setItem(ref DropDownList _control, String _value)
        {
            foreach (ListItem item in _control.Items)
            {
                if (item.Value == _value)
                {
                    item.Selected = true;
                    break;
                }
            }
            _control.Items.FindByText(_value).Selected = true;
        }

        #region Validacion codigo

        //Validación de datos básicos

        public bool Validacion(UTTT.Ejemplo.Linq.Data.Entity.Usuarios _usuario, ref String _mensaje)
        {
            if (_usuario.idStatus == -1)
            {
                _mensaje = "Seleccionar un Status para el usuario";
                return false;
            }

            if (_usuario.idPerfil == -1)
            {
                _mensaje = "Seleccionar un Perfil para el usuario";
                return false;
            }

            //Valida si el username esta vacio
            if (_usuario.strUsername.Equals(String.Empty))
            {
                _mensaje = "El campo de username esta vacio";
                return false;
            }

            //Valida solamente que se ingresen mas de 3 caracteres en el nombre
            if (_usuario.strUsername.Length < 3)
            {
                _mensaje = "El username necesita ser de al menos 3 caracteres, favor de ingresar uno valido";
                return false;
            }

            //Valida solamente que se ingresen menos de 50 caracteres en el nombre
            if (_usuario.strUsername.Length > 50)
            {
                _mensaje = "El username excede los 50 caracteres, favor de ingresar uno valido";
                return false;
            }

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