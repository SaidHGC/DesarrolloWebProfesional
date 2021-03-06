#region Using
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

#endregion

namespace UTTT.Ejemplo.Persona
{
    public partial class PersonaPrincipal : System.Web.UI.Page
    {
        #region Variables

        private SessionManager session = new SessionManager();
        public static String ultimaExcepcion;

        #endregion

        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {
            //try
            //{
            //    //Recibe la pila de excepciones, como es un "acumulador" donde se agregan las mismas y se...
            //    //... guardan en un string
            //    AppDomain.CurrentDomain.FirstChanceException += (senderr, ee) =>
            //    {
            //        System.Text.StringBuilder msg = new System.Text.StringBuilder();
            //        //Obtiene el nombre general de la excepcion
            //        msg.AppendLine(ee.Exception.GetType().FullName);
            //        //Obtiene el mensaje de la excepcion completa
            //        msg.AppendLine(ee.Exception.Message);
            //        //Obtine las razones de la excepcion
            //        System.Diagnostics.StackTrace st = new System.Diagnostics.StackTrace();
            //        //Se vuelve String el mensaje completo
            //        msg.AppendLine(st.ToString());
            //        //Se agrega una linea extra, importante porque no es con \n como pensaria
            //        msg.AppendLine();
            //        //Se le asigna a la variable global el valor del mensaje
            //        PersonaPrincipal.ultimaExcepcion = msg.ToString();
            //    };
            //}
            //catch (Exception error)
            //{
            //    throw error;
            //}
            try
            {
                Response.Buffer = true;
                DataContext dcTemp = new ManoAmigaSysDataContext();
                if (!this.IsPostBack)
                {
                    List<EmpSexo> lista = dcTemp.GetTable<EmpSexo>().ToList();
                    EmpSexo catTemp = new EmpSexo();
                    catTemp.idSexo = -1;
                    catTemp.strValor = "Todos";
                    lista.Insert(0, catTemp);
                    this.ddlSexo.DataTextField = "strValor";
                    this.ddlSexo.DataValueField = "idSexo";
                    this.ddlSexo.DataSource = lista;
                    this.ddlSexo.DataBind();
                }
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
                this.session.Pantalla = "~/PersonaManager.aspx";
                Hashtable parametrosRagion = new Hashtable();
                parametrosRagion.Add("idEmpleado", "0");
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

        protected void DataSourcePersona_Selecting(object sender, LinqDataSourceSelectEventArgs e)
        {
            try
            {
                DataContext dcConsulta = new ManoAmigaSysDataContext();
                bool nombreBool = false;
                bool sexoBool = false;
                if (!this.txtNombre.Text.Equals(String.Empty))
                {
                    nombreBool = true;
                }
                if (this.ddlSexo.Text != "-1")
                {
                    sexoBool = true;
                }

                Expression<Func<UTTT.Ejemplo.Linq.Data.Entity.Empleados, bool>>
                    predicate =
                    (c =>
                    ((sexoBool) ? c.idSexo == int.Parse(this.ddlSexo.Text) : true) &&
                    ((nombreBool) ? (((nombreBool) ? c.strNombre.Contains(this.txtNombre.Text.Trim()) : false)) : true)
                    );

                predicate.Compile();

                List<UTTT.Ejemplo.Linq.Data.Entity.Empleados> listaPersona =
                    dcConsulta.GetTable<UTTT.Ejemplo.Linq.Data.Entity.Empleados>().Where(predicate).ToList();
                e.Result = listaPersona;
            }
            catch (Exception _e)
            {
                throw _e;
            }
        }

        protected void dgvPersonas_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                int idPersona = int.Parse(e.CommandArgument.ToString());
                switch (e.CommandName)
                {
                    case "Editar":
                        this.editar(idPersona);
                        break;
                    case "Eliminar":
                        this.eliminar(idPersona);
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

        private void editar(int _idPersona)
        {
            try
            {
                Hashtable parametrosRagion = new Hashtable();
                parametrosRagion.Add("idEmpleado", _idPersona.ToString());
                this.session.Parametros = parametrosRagion;
                this.Session["SessionManager"] = this.session;
                this.session.Pantalla = String.Empty;
                this.session.Pantalla = "~/PersonaManager.aspx";
                this.Response.Redirect(this.session.Pantalla, false);

            }
            catch (Exception _e)
            {
                throw _e;
            }
        }

        private void eliminar(int _idPersona)
        {
            try
            {
                DataContext dcDelete = new ManoAmigaSysDataContext();
                UTTT.Ejemplo.Linq.Data.Entity.Empleados persona = dcDelete.GetTable<UTTT.Ejemplo.Linq.Data.Entity.Empleados>().First(
                    c => c.idEmpleado == _idPersona);
                dcDelete.GetTable<UTTT.Ejemplo.Linq.Data.Entity.Empleados>().DeleteOnSubmit(persona);
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

        protected void buscarTextBox(object sender , EventArgs e)
        {
            this.DataSourcePersona.RaiseViewChanged();
        }

        #endregion
    }
}