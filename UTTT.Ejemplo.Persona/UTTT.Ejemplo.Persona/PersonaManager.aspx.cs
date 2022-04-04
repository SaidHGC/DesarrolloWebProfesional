#region Using

using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Text.RegularExpressions;
using System.Web.UI.WebControls;
using UTTT.Ejemplo.Linq.Data.Entity;
using UTTT.Ejemplo.Persona.Control;
using UTTT.Ejemplo.Persona.Control.Ctrl;

#endregion

namespace UTTT.Ejemplo.Persona
{
    public partial class PersonaManager : System.Web.UI.Page
    {
        #region Variables

        private SessionManager session = new SessionManager();
        private int idPersona = 0;
        private UTTT.Ejemplo.Linq.Data.Entity.Empleados baseEntity;
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
                this.idPersona = this.session.Parametros["idEmpleado"] != null ?
                    int.Parse(this.session.Parametros["idEmpleado"].ToString()) : 0;
                if (this.idPersona == 0)
                {
                    this.baseEntity = new Linq.Data.Entity.Empleados();
                    //this.baseEntity = new Linq.Data.Entity.Persona();
                    this.tipoAccion = 1;
                }
                else
                {
                    this.baseEntity = dcGlobal.GetTable<Linq.Data.Entity.Empleados>().First(c => c.idEmpleado == this.idPersona);
                    //this.baseEntity = dcGlobal.GetTable<Linq.Data.Entity.Persona>().First(c => c.id == this.idPersona);
                    this.tipoAccion = 2;
                }

                if (!this.IsPostBack)
                {
                    if (this.session.Parametros["baseEntity"] == null)
                    {
                        this.session.Parametros.Add("baseEntity", this.baseEntity);
                    }
                    List<EmpCede> listaCede = dcGlobal.GetTable<EmpCede>().ToList();
                    List<EmpSexo> listaSexo = dcGlobal.GetTable<EmpSexo>().ToList();
                    //List<EmpCatStatus> listaStatus = dcGlobal.GetTable<EmpCatStatus>().ToList();
                    //List<EmpCatPuesto> listaPuesto = dcGlobal.GetTable<EmpCatPuesto>().ToList();

                    this.ddlCede.DataTextField = "strValor";
                    this.ddlCede.DataValueField = "IdCede";

                    this.ddlSexo.DataTextField = "strValor";
                    this.ddlSexo.DataValueField = "idSexo";

                    //this.ddlStatus.DataTextField = "strValor";
                    //this.ddlStatus.DataValueField = "id";

                    //this.ddlPuesto.DataTextField = "strValor";
                    //this.ddlPuesto.DataValueField = "id";

                    if (this.idPersona == 0)
                    {
                        this.lblAccion.Text = "Agregar";

                        CalendarExtender1.SelectedDate = DateTime.Now;

                        EmpCede catTempCede = new EmpCede();
                        catTempCede.IdCede = -1;
                        catTempCede.strValor = "Seleccionar";
                        listaCede.Insert(0, catTempCede);
                        this.ddlCede.DataSource = listaCede;
                        this.ddlCede.DataBind();

                        EmpSexo catTemp = new EmpSexo();
                        catTemp.idSexo = -1;
                        catTemp.strValor = "Seleccionar";
                        listaSexo.Insert(0, catTemp);
                        this.ddlSexo.DataSource = listaSexo;
                        this.ddlSexo.DataBind();

                        //EmpCatStatus catTempStatus = new EmpCatStatus();
                        //catTempStatus.idStatus = -1;
                        //catTempStatus.strValor = "Seleccionar";
                        //listaStatus.Insert(0, catTempStatus);
                        //this.ddlStatus.DataSource = listaStatus;
                        //this.ddlStatus.DataBind();

                        //EmpCatPuesto catTempPuesto = new EmpCatPuesto();
                        //catTempPuesto.idPuesto = -1;
                        //catTempPuesto.strValor = "Seleccionar";
                        //listaPuesto.Insert(0, catTempPuesto);
                        //this.ddlPuesto.DataSource = listaPuesto;
                        //this.ddlPuesto.DataBind();
                    }
                    else
                    {
                        this.lblAccion.Text = "Editar";
                        this.txtNombre.Text = this.baseEntity.strNombre;
                        this.txtAPaterno.Text = this.baseEntity.strApPaterno;
                        this.txtAMaterno.Text = this.baseEntity.strApMaterno;
                        this.txtEmail.Text = this.baseEntity.strEmail.ToString();
                        //this.txtClaveUnica.Text = this.baseEntity.strClaveUnica;
                        this.txtFechaIngreso.Text = this.baseEntity.dteFechaIngreso.ToString();

                        if(baseEntity.dteFechaIngreso != null)
                            CalendarExtender1.SelectedDate = this.baseEntity.dteFechaIngreso.Value.Date;

                        this.ddlCede.DataSource = listaCede;
                        this.ddlCede.DataBind();

                        this.ddlSexo.DataSource = listaSexo;
                        this.ddlSexo.DataBind();

                        //this.ddlStatus.DataSource = listaStatus;
                        //this.ddlStatus.DataBind();

                        //this.ddlPuesto.DataSource = listaPuesto;
                        //this.ddlPuesto.DataBind();

                        this.setItem(ref this.ddlCede, baseEntity.EmpCede.strValor);
                        this.setItem(ref this.ddlSexo, baseEntity.EmpSexo.strValor);
                        //this.setItem(ref this.ddlStatus, baseEntity.EmpCatStatus.strValor);
                        //this.setItem(ref this.ddlPuesto, baseEntity.EmpCatPuesto.strValor);

                    }

                    this.ddlCede.SelectedIndexChanged += new EventHandler(ddlCede_SelectedIndexChanged);
                    this.ddlCede.AutoPostBack = true;

                    this.ddlSexo.SelectedIndexChanged += new EventHandler(ddlSexo_SelectedIndexChanged);
                    this.ddlSexo.AutoPostBack = true;

                    //this.ddlStatus.SelectedIndexChanged += new EventHandler(ddlStatus_SelectedIndexChanged);
                    //this.ddlStatus.AutoPostBack = true;

                    //this.ddlPuesto.SelectedIndexChanged += new EventHandler(ddlPuesto_SelectedIndexChanged);
                    //this.ddlPuesto.AutoPostBack = true;
                }

            }
            catch (Exception _e)
            {
                this.showMessage("Ha ocurrido un problema al cargar la página");
                this.Response.Redirect("~/PersonaPrincipal.aspx", false);
            }

        }

        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            if (this.txtNombre.Text.Trim().Equals("") && 
                this.txtAPaterno.Text.Trim().Equals("") && 
                this.txtAMaterno.Text.Trim().Equals("") && 
                int.Parse(this.ddlCede.Text).Equals(-1) && 
                int.Parse(this.ddlSexo.Text).Equals(-1) &&
                //int.Parse(this.ddlStatus.Text).Equals(-1) &&
                //int.Parse(this.ddlPuesto.Text).Equals(-1) &&
                this.txtFechaIngreso.Text.Trim().Equals("") &&
                this.txtEmail.Text.Trim().Equals(""))
            {
                this.Response.Redirect("~/PersonaPrincipal.aspx", false);
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

                //SE OBTINE LA FECHA DE INGRESO
                string date = Request.Form[this.txtFechaIngreso.UniqueID];
                DateTime fechaIngreso = DateTime.Parse(date, CultureInfo.CreateSpecificCulture("es-MX"));
                //DateTime fechaNacimiento = DateTime.Parse(date, CultureInfo.CreateSpecificCulture("es-MX"));

                DataContext dcGuardar = new ManoAmigaSysDataContext();
                UTTT.Ejemplo.Linq.Data.Entity.Empleados empleado = new Linq.Data.Entity.Empleados();
                //UTTT.Ejemplo.Linq.Data.Entity.Persona persona = new Linq.Data.Entity.Persona();
                if (this.idPersona == 0)
                {
                    empleado.strNombre = this.txtNombre.Text.Trim();
                    empleado.strApPaterno = this.txtAPaterno.Text.Trim();
                    empleado.strApMaterno = this.txtAMaterno.Text.Trim();
                    empleado.strEmail = this.txtEmail.Text.Trim();
                    empleado.idCede = int.Parse(this.ddlCede.Text);
                    empleado.idSexo = int.Parse(this.ddlSexo.Text);
                    //empleado.idStatus = int.Parse(this.ddlStatus.Text);
                    //empleado.idPuesto = int.Parse(this.ddlPuesto.Text);
                    
                    //persona.strClaveUnica = this.txtClaveUnica.Text.Trim();
                    //persona.strNombre = this.txtNombre.Text.Trim();
                    //persona.strAMaterno = this.txtAMaterno.Text.Trim();
                    //persona.strAPaterno = this.txtAPaterno.Text.Trim();
                    //persona.idCatSexo = int.Parse(this.ddlSexo.Text);
                    //persona.strCURP = this.txtCurp.Text.Trim();

                    //ASIGNA LA FECHA DE NACIMIENTO
                    empleado.dteFechaIngreso = fechaIngreso;
                    //persona.dteFechaNacimiento = fechaNacimiento;

                    String mensaje = String.Empty;
                    //Validacion de datos correctos desde código
                    if (!this.Validacion(empleado, ref mensaje))
                    {
                        this.lblMensaje.Text = mensaje;
                        this.lblMensaje.Visible = true;
                        return;
                    }

                    dcGuardar.GetTable<UTTT.Ejemplo.Linq.Data.Entity.Empleados>().InsertOnSubmit(empleado);
                    dcGuardar.SubmitChanges();
                    this.showMessage("El registro se agrego correctamente.");
                    this.Response.Redirect("~/PersonaPrincipal.aspx", false);
                    
                }
                if (this.idPersona > 0)
                {
                    empleado = dcGuardar.GetTable<UTTT.Ejemplo.Linq.Data.Entity.Empleados>().First
                                                                        (c => c.idEmpleado == idPersona);
                    empleado.strNombre = this.txtNombre.Text.Trim();
                    empleado.strApPaterno = this.txtAPaterno.Text.Trim();
                    empleado.strApMaterno = this.txtAMaterno.Text.Trim();
                    empleado.idCede = int.Parse(this.ddlCede.Text);
                    empleado.idSexo = int.Parse(this.ddlSexo.Text);
                    //empleado.idStatus = int.Parse(this.ddlStatus.Text);
                    //empleado.idPuesto = int.Parse(this.ddlPuesto.Text);
                    empleado.strEmail = this.txtEmail.Text.Trim();
                    //persona.strNombre = this.txtNombre.Text.Trim();
                    //persona.strAMaterno = this.txtAMaterno.Text.Trim();
                    //persona.strAPaterno = this.txtAPaterno.Text.Trim();
                    //persona.idCatSexo = int.Parse(this.ddlSexo.Text);
                    //persona.strCURP = this.txtCurp.Text.Trim();

                    //ASIGNA FECHA DE INGRESO
                    empleado.dteFechaIngreso = fechaIngreso;
                    //persona.dteFechaNacimiento = fechaNacimiento;

                    String mensaje = String.Empty;
                    //Validacion de datos correctos desde código
                    if (!this.Validacion(empleado, ref mensaje))
                    {
                        this.lblMensaje.Text = mensaje;
                        this.lblMensaje.Visible = true;
                        return;
                    }

                    dcGuardar.SubmitChanges();
                    this.showMessage("El registro se edito correctamente.");
                    this.Response.Redirect("~/PersonaPrincipal.aspx", false);
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
                this.Response.Redirect("~/PersonaPrincipal.aspx", false);


            }
            catch (Exception _e)
            {
                this.showMessage("Ha ocurrido un error inesperado");
            }
        }

        protected void ddlCede_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int idCede = int.Parse(this.ddlCede.Text);
                Expression<Func<EmpCede, bool>> predicateCede = c => c.IdCede == idCede;
                predicateCede.Compile();
                List<EmpCede> lista = dcGlobal.GetTable<EmpCede>().Where(predicateCede).ToList();
                EmpCede catTempCede = new EmpCede();
                this.ddlCede.DataTextField = "strValor";
                this.ddlCede.DataValueField = "IdCede";
                this.ddlCede.DataSource = lista;
                this.ddlCede.DataBind();
            }
            catch (Exception)
            {
                this.showMessage("Ha ocurrido un error inesperado");
            }
        }

        protected void ddlSexo_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int idSexo = int.Parse(this.ddlSexo.Text);
                Expression<Func<EmpSexo, bool>> predicateSexo = c => c.idSexo == idSexo;
                predicateSexo.Compile();
                List<EmpSexo> lista = dcGlobal.GetTable<EmpSexo>().Where(predicateSexo).ToList();
                EmpSexo catTempSexo = new EmpSexo();            
                this.ddlSexo.DataTextField = "strValor";
                this.ddlSexo.DataValueField = "idSexo";
                this.ddlSexo.DataSource = lista;
                this.ddlSexo.DataBind();
            }
            catch (Exception)
            {
                this.showMessage("Ha ocurrido un error inesperado");
            }
        }

        //protected void ddlStatus_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        int idStatus = int.Parse(this.ddlStatus.Text);
        //        Expression<Func<EmpStatus, bool>> predicateStatus = c => c.idStatus == idStatus;
        //        predicateStatus.Compile();
        //        List<EmpStatus> lista = dcGlobal.GetTable<EmpStatus>().Where(predicateStatus).ToList();
        //        EmpStatus catTempSexo = new EmpStatus();
        //        this.ddlStatus.DataTextField = "strValor";
        //        this.ddlStatus.DataValueField = "id";
        //        this.ddlStatus.DataSource = lista;
        //        this.ddlStatus.DataBind();
        //    }
        //    catch (Exception)
        //    {
        //        this.showMessage("Ha ocurrido un error inesperado");
        //    }
        //}

        //protected void ddlPuesto_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        int idPuesto = int.Parse(this.ddlPuesto.Text);
        //        Expression<Func<EmpCatPuesto, bool>> predicatePuesto = c => c.idPuesto == idPuesto;
        //        predicatePuesto.Compile();
        //        List<EmpCatPuesto> lista = dcGlobal.GetTable<EmpCatPuesto>().Where(predicatePuesto).ToList();
        //        EmpCatPuesto catTempSexo = new EmpCatPuesto();
        //        this.ddlPuesto.DataTextField = "strValor";
        //        this.ddlPuesto.DataValueField = "id";
        //        this.ddlPuesto.DataSource = lista;
        //        this.ddlPuesto.DataBind();
        //    }
        //    catch (Exception)
        //    {
        //        this.showMessage("Ha ocurrido un error inesperado");
        //    }
        //}

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

        public bool Validacion(UTTT.Ejemplo.Linq.Data.Entity.Empleados _empleado, ref String _mensaje)
        {
            if (_empleado.idSexo == -1)
            {
                _mensaje = "Seleccionar un sexo, Femenino o Masculino";
                return false;
            }

            int i = 0;
            //Verificar si un texto es un número
            //if (int.TryParse(_empleado.strClaveUnica, out i) == false)
            //{
            //    _mensaje = "La Clave Única no es un número";
            //    return false;
            //}

            //Validamos un número, validar que esten entre 1 y 999
            //if(int.Parse(_persona.strClaveUnica) < 1 || int.Parse(_persona.strClaveUnica) > 999)
            //{
            //    _mensaje = "La Clave única esta fuera de rango";
            //    return false;
            //}

            //Valida si el nommbre esta vacio
            if (_empleado.strNombre.Equals(String.Empty))
            {
                _mensaje = "El campo nombre esta vacio";
                return false;
            }

            //Valida solamente que se ingresen mas de 3 caracteres en el nombre
            if (_empleado.strNombre.Length < 3)
            {
                _mensaje = "El nombre necesita ser de al menos 3 caracteres, favor de ingresar un nombre valido";
                return false;
            }

            //Valida solamente que se ingresen menos de 50 caracteres en el nombre
            if (_empleado.strNombre.Length > 50)
            {
                _mensaje = "El nombre excede los 50 caracteres, favor de ingresar un nombre valido";
                return false;
            }

            //Validamos que solo se inserten letras en nombre...
            /*
             * RESCATADO DE
             * https://qastack.mx/programming/1181419/verifying-that-a-string-contains-only-letters-in-c-sharp
             */
            if (!Regex.IsMatch(_empleado.strNombre, @"^[a-zA-ZñÑáéíóúÁÉÍÓÚàèìòùÀÈÌÒÙüïÜÏ ]+$"))
            {
                _mensaje = "El campo Nombre solo acepta letras, favor de insertar caracteres validos";
                return false;
            }

            //Valida si el APaterno esta vacio
            if (_empleado.strApPaterno.Equals(String.Empty))
            {
                _mensaje = "El campo APaterno esta vacio";
                return false;
            }

            //Valida solamente que se ingresen mas de 3 caracteres en el APaterno
            if (_empleado.strApPaterno.Length < 3)
            {
                _mensaje = "El apellido paterno necesita ser de al menos 3 caracteres, favor de ingresar un apellido valido";
                return false;
            }

            //Valida solamente que se ingresen menos de 50 caracteres en el APaterno
            if (_empleado.strApPaterno.Length > 50)
            {
                _mensaje = "El apellido paterno excede los 50 caracteres, favor de ingresar un apellido valido";
                return false;
            }

            //Validamos que solo se inserten letras en APaterno...
            /*
             * RESCATADO DE
             * https://qastack.mx/programming/1181419/verifying-that-a-string-contains-only-letters-in-c-sharp
             */
            if (!Regex.IsMatch(_empleado.strApPaterno, @"^[a-zA-ZñÑáéíóúÁÉÍÓÚàèìòùÀÈÌÒÙüïÜÏ ]+$"))
            {
                _mensaje = "El campo de APaterno solo acepta letras, favor de insertar caracteres validos";
                return false;
            }

            //Valida si el AMaterno esta vacio
            if (_empleado.strApMaterno.Equals(String.Empty))
            {
                _mensaje = "El campo AMaterno esta vacio";
                return false;
            }

            //Valida solamente que se ingresen mas de 3 caracteres en el AMaterno
            if (_empleado.strApMaterno.Length < 3)
            {
                _mensaje = "El apellido Materno debe ser de al menos 3 caracteres, favor de ingresar un apellido valido";
                return false;
            }

            //Valida solamente que se ingresen menos de 50 caracteres en el AMaterno
            if (_empleado.strApMaterno.Length > 50)
            {
                _mensaje = "El apellido Materno excede los 50 caracteres, favor de ingresar un apellido valido";
                return false;
            }

            //Validamos que solo se inserten letras en AMaterno...
            /*
             * RESCATADO DE
             * https://qastack.mx/programming/1181419/verifying-that-a-string-contains-only-letters-in-c-sharp
             */
            if (!Regex.IsMatch(_empleado.strApMaterno, @"^[a-zA-ZñÑáéíóúÁÉÍÓÚàèìòùÀÈÌÒÙv ]+$"))
            {
                _mensaje = "El campo AMaterno solo acepta letras, favor de insertar caracteres validos";
                return false;
            }

            //Valida que sea tipo email el email
            if(!Regex.IsMatch(_empleado.strEmail, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase))
            {
                _mensaje = "El campo email no es del tipo indicado, favor de insertar caracteres validos";
                return false;
            }

            return true;
        }

        #endregion

        #endregion
    }
}