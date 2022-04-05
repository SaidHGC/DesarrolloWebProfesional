using System;
using System.Data.Linq;
using System.Linq;
using UTTT.Ejemplo.Linq.Data.Entity;
using UTTT.Ejemplo.Persona.Control;
using UTTT.Ejemplo.Persona.Control.Ctrl;

namespace UTTT.Ejemplo.Persona
{
    public partial class Login : System.Web.UI.Page
    {
        #region Variables

        private SessionManager session = new SessionManager();
        public static String ultimaExcepcion;
        private UTTT.Ejemplo.Linq.Data.Entity.Usuarios baseEntity;
        private DataContext dcGlobal = new ManoAmigaSysDataContext();

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                //Recibe la pila de excepciones, como es un "acumulador" donde se agregan las mismas y se...
                //... guardan en un string
                AppDomain.CurrentDomain.FirstChanceException += (senderr, ee) =>
                {
                    System.Text.StringBuilder msg = new System.Text.StringBuilder();
                    //Obtiene el nombre general de la excepcion
                    msg.AppendLine(ee.Exception.GetType().FullName);
                    //Obtiene el mensaje de la excepcion completa
                    msg.AppendLine(ee.Exception.Message);
                    //Obtine las razones de la excepcion
                    System.Diagnostics.StackTrace st = new System.Diagnostics.StackTrace();
                    //Se vuelve String el mensaje completo
                    msg.AppendLine(st.ToString());
                    //Se agrega una linea extra, importante porque no es con \n como pensaria
                    msg.AppendLine();
                    //Se le asigna a la variable global el valor del mensaje
                    PersonaPrincipal.ultimaExcepcion = msg.ToString();
                };
            }
            catch (Exception error)
            {
                throw error;
            }
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string usuario = this.txtUsername.Text;
            string pass = this.txtPassword.Text;

            using(dcGlobal = new ManoAmigaSysDataContext())
            {
                baseEntity = dcGlobal.GetTable<Usuarios>().FirstOrDefault(c => (c.strUsername.Equals(usuario) && (c.strPassword.Equals(pass))));
            }
            if (baseEntity != null)
            {
                //session = new SessionManager(baseEntity.idUsuario);
                session.Pantalla = "~/Menu.aspx";
                Session["SessionManager"] = this.session;
                Session["idUsuario"] = baseEntity.idUsuario;
                this.Response.Redirect(this.session.Pantalla, false);
            }
        }
    }
}