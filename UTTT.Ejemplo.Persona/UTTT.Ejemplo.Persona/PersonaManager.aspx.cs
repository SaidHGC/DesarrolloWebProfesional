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

        String mensajeValidacion = String.Empty;
        DateTime fechaIngreso = new DateTime(DateTime.MaxValue.Ticks);

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
                //Comento la fecha porque no me deja comparar que sea la fecha minima
                //this.txtFechaIngreso.Text.Trim().Equals("") &&
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
                
                DataContext dcGuardar = new ManoAmigaSysDataContext();
                UTTT.Ejemplo.Linq.Data.Entity.Empleados empleado = new Linq.Data.Entity.Empleados();

                string empleadoTemp = this.txtNombre.Text;
                string empleadoAPTemp = this.txtAPaterno.Text;
                string empleadoAMTemp = this.txtAMaterno.Text;

                using (dcGlobal = new ManoAmigaSysDataContext())
                {
                    List<Empleados> listaEmpleadosNombre =
                        dcGlobal.GetTable<Empleados>().Where(c => (c.strNombre.Equals(empleadoTemp))).ToList();

                    List<Empleados> listaEmpleadosAP =
                        dcGlobal.GetTable<Empleados>().Where(c => (c.strApPaterno.Equals(empleadoAPTemp))).ToList();

                    List<Empleados> listaEmpleadosAM =
                        dcGlobal.GetTable<Empleados>().Where(c => (c.strApMaterno.Equals(empleadoAMTemp))).ToList();

                    if (listaEmpleadosNombre.Count > 0 && listaEmpleadosAP.Count > 0 && listaEmpleadosAM.Count > 0)
                    {
                        this.showMessage("El empleado ya existe, ingrese uno nuevo");
                    }
                    else
                    {
                        if (Validacion(ref mensajeValidacion))
                        {
                            //SE OBTINE LA FECHA DE INGRESO
                            string date = Request.Form[this.txtFechaIngreso.UniqueID];

                            if (!DateTime.TryParse(date, CultureInfo.CreateSpecificCulture("es-MX"), DateTimeStyles.None, out fechaIngreso))
                            {
                                this.lblMensaje.Text = "La fecha no es valida";
                                this.lblMensaje.Visible = true;
                            }
                            else
                            {

                                if (this.idPersona == 0)
                                {
                                    empleado.strNombre = this.txtNombre.Text.Trim();
                                    empleado.strApPaterno = this.txtAPaterno.Text.Trim();
                                    empleado.strApMaterno = this.txtAMaterno.Text.Trim();
                                    empleado.strEmail = this.txtEmail.Text.Trim();
                                    empleado.idCede = int.Parse(this.ddlCede.Text);
                                    empleado.idSexo = int.Parse(this.ddlSexo.Text);

                                    //ASIGNA LA FECHA DE NACIMIENTO
                                    empleado.dteFechaIngreso = fechaIngreso;

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
                                    empleado.strEmail = this.txtEmail.Text.Trim();

                                    empleado.dteFechaIngreso = fechaIngreso;

                                    //String mensaje = String.Empty;
                                    //Validacion de datos correctos desde código
                                    //if (!this.Validacion(ref mensaje))
                                    //{
                                    //    this.lblMensaje.Text = mensaje;
                                    //    this.lblMensaje.Visible = true;
                                    //    return;
                                    //}

                                    dcGuardar.SubmitChanges();
                                    this.showMessage("El registro se edito correctamente.");
                                    this.Response.Redirect("~/PersonaPrincipal.aspx", false);
                                }
                            }

                        }
                        else
                        {
                            this.lblMensaje.Text = mensajeValidacion;
                            this.lblMensaje.Visible = true;
                        }
                    }
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

        //public bool ValidacionFecha(DateTime fechaEvaluar, ref String _mensaje)
        //{
        //    //Validar fecha
        //    //Regex re = new Regex("^(0?[1-9]|1[0-9]|2|2[0-9]|3[0-1])/(0?[1-9]|1[0-2])/(d{2}|d{4})$");
        //    //if (!re.IsMatch(_empleado.dteFechaIngreso.ToString()))
        //    //{
        //    //    _mensaje = "La fecha no es valida";
        //    //    return false;
        //    //}
        //    //Valida que el año no sea 0 en la fecha
        //    if (fechaEvaluar.Year == 0000)
        //    {
        //        _mensaje = "El año no es valido";
        //        return false;
        //    }

        //    return true;
        //}

        //Validación de datos básicos

        public bool Validacion(ref String _mensaje)
        {
            if (ddlSexo.SelectedValue.Equals(-1))
            {
                _mensaje = "Seleccionar un sexo, Femenino o Masculino";
                return false;
            }

            //Valida si el nommbre esta vacio
            if (txtNombre.Text.Trim().Equals(String.Empty))
            {
                _mensaje = "El campo nombre esta vacio";
                return false;
            }

            //Valida solamente que se ingresen mas de 3 caracteres en el nombre
            if (txtNombre.Text.Trim().Length < 3)
            {
                _mensaje = "El nombre necesita ser de al menos 3 caracteres, favor de ingresar un nombre valido";
                return false;
            }

            //Valida solamente que se ingresen menos de 50 caracteres en el nombre
            if (txtNombre.Text.Trim().Length > 50)
            {
                _mensaje = "El nombre excede los 50 caracteres, favor de ingresar un nombre valido";
                return false;
            }

            //Validamos que solo se inserten letras en nombre...
            /*
             * RESCATADO DE
             * https://qastack.mx/programming/1181419/verifying-that-a-string-contains-only-letters-in-c-sharp
             */
            if (!Regex.IsMatch(txtNombre.Text.Trim(), @"^[a-zA-ZñÑáéíóúÁÉÍÓÚàèìòùÀÈÌÒÙüïÜÏ ]+$"))
            {
                _mensaje = "El campo Nombre solo acepta letras, favor de insertar caracteres validos";
                return false;
            }

            //Valida si el APaterno esta vacio
            if (txtAPaterno.Text.Trim().Equals(String.Empty))
            {
                _mensaje = "El campo APaterno esta vacio";
                return false;
            }

            //Valida solamente que se ingresen mas de 3 caracteres en el APaterno
            if (txtAPaterno.Text.Trim().Length < 3)
            {
                _mensaje = "El apellido paterno necesita ser de al menos 3 caracteres, favor de ingresar un apellido valido";
                return false;
            }

            //Valida solamente que se ingresen menos de 50 caracteres en el APaterno
            if (txtAPaterno.Text.Trim().Length > 50)
            {
                _mensaje = "El apellido paterno excede los 50 caracteres, favor de ingresar un apellido valido";
                return false;
            }

            //Validamos que solo se inserten letras en APaterno...
            /*
             * RESCATADO DE
             * https://qastack.mx/programming/1181419/verifying-that-a-string-contains-only-letters-in-c-sharp
             */
            if (!Regex.IsMatch(txtAPaterno.Text.Trim(), @"^[a-zA-ZñÑáéíóúÁÉÍÓÚàèìòùÀÈÌÒÙüïÜÏ ]+$"))
            {
                _mensaje = "El campo de APaterno solo acepta letras, favor de insertar caracteres validos";
                return false;
            }

            //Valida si el AMaterno esta vacio
            if (txtAMaterno.Text.Trim().Equals(String.Empty))
            {
                _mensaje = "El campo AMaterno esta vacio";
                return false;
            }

            //Valida solamente que se ingresen mas de 3 caracteres en el AMaterno
            if (txtAMaterno.Text.Trim().Length < 3)
            {
                _mensaje = "El apellido Materno debe ser de al menos 3 caracteres, favor de ingresar un apellido valido";
                return false;
            }

            //Valida solamente que se ingresen menos de 50 caracteres en el AMaterno
            if (txtAMaterno.Text.Trim().Length > 50)
            {
                _mensaje = "El apellido Materno excede los 50 caracteres, favor de ingresar un apellido valido";
                return false;
            }

            //Validamos que solo se inserten letras en AMaterno...
            /*
             * RESCATADO DE
             * https://qastack.mx/programming/1181419/verifying-that-a-string-contains-only-letters-in-c-sharp
             */
            if (!Regex.IsMatch(txtAMaterno.Text.Trim(), @"^[a-zA-ZñÑáéíóúÁÉÍÓÚàèìòùÀÈÌÒÙv ]+$"))
            {
                _mensaje = "El campo AMaterno solo acepta letras, favor de insertar caracteres validos";
                return false;
            }

            //Valida que sea tipo email el email
            if(!Regex.IsMatch(txtEmail.Text.Trim(), @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase))
            {
                _mensaje = "El campo email no es del tipo indicado, favor de insertar caracteres validos";
                return false;
            }

            //Validar la fecha
            if (!Regex.IsMatch(txtFechaIngreso.Text, @"^([0-2][0-9]|3[0-1])(\/|-)(0[1-9]|1[0-2])\2(\d{4})$"))
            {
                _mensaje = "La fecha de ingreso no corresponde con el formato solicitado";
                return false;
            }

            return true;
        }

        #endregion

        #endregion
    }
}