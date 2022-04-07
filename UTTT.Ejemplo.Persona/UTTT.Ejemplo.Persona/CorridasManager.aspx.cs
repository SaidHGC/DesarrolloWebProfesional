using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Linq.Expressions;
using System.Text.RegularExpressions;
using System.Web.UI.WebControls;
using UTTT.Ejemplo.Linq.Data.Entity;
using UTTT.Ejemplo.Persona.Control;
using UTTT.Ejemplo.Persona.Control.Ctrl;

namespace UTTT.Ejemplo.Persona
{
    public partial class CorridasManager : System.Web.UI.Page
    {
        #region Variables

        private SessionManager session = new SessionManager();
        private int idPersona = 0;
        private UTTT.Ejemplo.Linq.Data.Entity.Corridas baseEntity;
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
                this.idPersona = this.session.Parametros["idCorrida"] != null ?
                    int.Parse(this.session.Parametros["idCorrida"].ToString()) : 0;
                if (this.idPersona == 0)
                {
                    this.baseEntity = new Linq.Data.Entity.Corridas();
                    this.tipoAccion = 1;
                }
                else
                {
                    this.baseEntity = dcGlobal.GetTable<Linq.Data.Entity.Corridas>().First(c => c.idCorrida == this.idPersona);
                    this.tipoAccion = 2;
                }

                if (!this.IsPostBack)
                {
                    if (this.session.Parametros["baseEntity"] == null)
                    {
                        this.session.Parametros.Add("baseEntity", this.baseEntity);
                    }
                    List<EmpCede> listaCede = dcGlobal.GetTable<EmpCede>().ToList();

                    this.ddlCede.DataTextField = "strValor";
                    this.ddlCede.DataValueField = "IdCede";

                    if (this.idPersona == 0)
                    {
                        this.lblAccion.Text = "Agregar";

                        EmpCede catTempCede = new EmpCede();
                        catTempCede.IdCede = -1;
                        catTempCede.strValor = "Seleccionar";
                        listaCede.Insert(0, catTempCede);
                        this.ddlCede.DataSource = listaCede;
                        this.ddlCede.DataBind();

                    }
                    else
                    {
                        this.lblAccion.Text = "Editar";
                        this.txtPuntoInicio.Text = this.baseEntity.strPuntoInicio;
                        this.txtPuntoFinal.Text = this.baseEntity.strPuntoFinal;
                        this.txtTipoCorrida.Text = this.baseEntity.strTipoCorrida;

                        this.ddlCede.DataSource = listaCede;
                        this.ddlCede.DataBind();

                        this.setItem(ref this.ddlCede, baseEntity.EmpCede.strValor);
                    }

                    this.ddlCede.SelectedIndexChanged += new EventHandler(ddlCede_SelectedIndexChanged);
                    this.ddlCede.AutoPostBack = true;
                }

            }
            catch (Exception _e)
            {
                this.showMessage("Ha ocurrido un problema al cargar la página");
                this.Response.Redirect("~/CorridasPrincipal.aspx", false);
            }
        }

        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            if (this.txtPuntoInicio.Text.Trim().Equals("") &&
                this.txtPuntoFinal.Text.Trim().Equals("") &&
                this.txtTipoCorrida.Text.Trim().Equals("") &&
                int.Parse(this.ddlCede.Text).Equals(-1))
            {
                this.Response.Redirect("~/CorridasPrincipal.aspx", false);
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
                UTTT.Ejemplo.Linq.Data.Entity.Corridas corrida = new Linq.Data.Entity.Corridas();

                string pInicialTemp = this.txtPuntoInicio.Text;
                string pFinalTemp = this.txtPuntoFinal.Text;

                using (dcGlobal = new ManoAmigaSysDataContext())
                {
                    List<Corridas> listaCorridasInicial =
                        dcGlobal.GetTable<Corridas>().Where(c => (c.strPuntoInicio.Equals(pInicialTemp))).ToList();

                    List<Corridas> listaCorridasFinal =
                        dcGlobal.GetTable<Corridas>().Where(c => (c.strPuntoFinal.Equals(pFinalTemp))).ToList();

                    if (listaCorridasInicial.Count > 0 && listaCorridasFinal.Count > 0)
                    {
                        this.showMessage("La corrida ya existe, favor de ingresar otro que sea valida");
                    }
                    else
                    {
                        if (this.idPersona == 0)
                        {
                            corrida.strPuntoInicio = this.txtPuntoInicio.Text.Trim();
                            corrida.strPuntoFinal = this.txtPuntoFinal.Text.Trim();
                            corrida.idCede = int.Parse(this.ddlCede.Text);
                            corrida.strTipoCorrida = this.txtTipoCorrida.Text.Trim();

                            String mensaje = String.Empty;
                            //Validacion de datos correctos desde código
                            if (!this.Validacion(corrida, ref mensaje))
                            {
                                this.lblMensaje.Text = mensaje;
                                this.lblMensaje.Visible = true;
                                return;
                            }

                            dcGuardar.GetTable<UTTT.Ejemplo.Linq.Data.Entity.Corridas>().InsertOnSubmit(corrida);
                            dcGuardar.SubmitChanges();
                            this.showMessage("El registro se agrego correctamente.");
                            this.Response.Redirect("~/CorridasPrincipal.aspx", false);

                        }
                        if (this.idPersona > 0)
                        {
                            corrida = dcGuardar.GetTable<UTTT.Ejemplo.Linq.Data.Entity.Corridas>().First
                                                                                (c => c.idCorrida == idPersona);
                            corrida.strPuntoInicio = this.txtPuntoInicio.Text.Trim();
                            corrida.strPuntoFinal = this.txtPuntoFinal.Text.Trim();
                            corrida.idCede = int.Parse(this.ddlCede.Text);
                            corrida.strTipoCorrida = this.txtTipoCorrida.Text.Trim();

                            String mensaje = String.Empty;
                            //Validacion de datos correctos desde código
                            if (!this.Validacion(corrida, ref mensaje))
                            {
                                this.lblMensaje.Text = mensaje;
                                this.lblMensaje.Visible = true;
                                return;
                            }

                            dcGuardar.SubmitChanges();
                            this.showMessage("El registro se edito correctamente.");
                            this.Response.Redirect("~/CorridasPrincipal.aspx", false);
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
                this.Response.Redirect("~/CorridasPrincipal.aspx", false);


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

        public bool Validacion(UTTT.Ejemplo.Linq.Data.Entity.Corridas _corrida, ref String _mensaje)
        {
            if (_corrida.idCede == -1)
            {
                _mensaje = "Seleccionar una cede";
                return false;
            }

            int i = 0;

            //Valida si el punto de inicio esta vacio
            if (_corrida.strPuntoInicio.Equals(String.Empty))
            {
                _mensaje = "El campo punto de inicio esta vacio";
                return false;
            }

            //Valida solamente que se ingresen mas de 6 caracteres en el punto de inicio
            if (_corrida.strPuntoInicio.Length < 6)
            {
                _mensaje = "El punto de inicio necesita ser de al menos 6 caracteres, favor de ingresar un punto de inicio valido";
                return false;
            }

            //Valida solamente que se ingresen menos de 50 caracteres en el punto de inicio
            if (_corrida.strPuntoInicio.Length > 50)
            {
                _mensaje = "El punto de inicio excede los 50 caracteres, favor de ingresar un punto de inicio valido";
                return false;
            }

            //Validamos que solo se inserten letras en punto de inicio...
            /*
             * RESCATADO DE
             * https://qastack.mx/programming/1181419/verifying-that-a-string-contains-only-letters-in-c-sharp
             */
            if (!Regex.IsMatch(_corrida.strPuntoInicio, @"^[a-zA-ZñÑáéíóúÁÉÍÓÚàèìòùÀÈÌÒÙüïÜÏ ]+$"))
            {
                _mensaje = "El campo de punto de inicio solo acepta letras, favor de insertar caracteres validos";
                return false;
            }

            //Valida si el punto final esta vacio
            if (_corrida.strPuntoFinal.Equals(String.Empty))
            {
                _mensaje = "El campo de punto final esta vacio";
                return false;
            }

            //Valida solamente que se ingresen mas de 6 caracteres en el punto final
            if (_corrida.strPuntoFinal.Length < 6)
            {
                _mensaje = "El punto final necesita ser de al menos 6 caracteres, favor de ingresar un punto final valido";
                return false;
            }

            //Valida solamente que se ingresen menos de 50 caracteres en el punto final
            if (_corrida.strPuntoFinal.Length > 50)
            {
                _mensaje = "El punto final excede los 50 caracteres, favor de ingresar un punto final valido";
                return false;
            }

            //Validamos que solo se inserten letras en el punto final...
            /*
             * RESCATADO DE
             * https://qastack.mx/programming/1181419/verifying-that-a-string-contains-only-letters-in-c-sharp
             */
            if (!Regex.IsMatch(_corrida.strPuntoFinal, @"^[a-zA-ZñÑáéíóúÁÉÍÓÚàèìòùÀÈÌÒÙüïÜÏ ]+$"))
            {
                _mensaje = "El campo de APaterno solo acepta letras, favor de insertar caracteres validos";
                return false;
            }

            //Valida si el tipo de corrida esta vacio
            if (_corrida.strTipoCorrida.Equals(String.Empty))
            {
                _mensaje = "El campo tipo de corrida esta vacio";
                return false;
            }

            //Valida solamente que se ingresen mas de 5 caracteres en el tipo de corrida
            if (_corrida.strTipoCorrida.Length < 5)
            {
                _mensaje = "El tipo de corrida debe ser de al menos 5 caracteres, favor de ingresar un tipo de corrida valido";
                return false;
            }

            //Valida solamente que se ingresen menos de 20 caracteres en el tipo de corrida
            if (_corrida.strTipoCorrida.Length > 20)
            {
                _mensaje = "El tipo de corrida excede los 20 caracteres, favor de ingresar un tipo de corrida valido";
                return false;
            }

            //Validamos que solo se inserten letras en tipo de corrida...
            /*
             * RESCATADO DE
             * https://qastack.mx/programming/1181419/verifying-that-a-string-contains-only-letters-in-c-sharp
             */
            if (!Regex.IsMatch(_corrida.strTipoCorrida, @"^[a-zA-ZñÑáéíóúÁÉÍÓÚàèìòùÀÈÌÒÙv ]+$"))
            {
                _mensaje = "El campo tipo de corrida solo acepta letras, favor de insertar caracteres validos";
                return false;
            }


            return true;
        }

        #endregion

        #endregion
    }
}