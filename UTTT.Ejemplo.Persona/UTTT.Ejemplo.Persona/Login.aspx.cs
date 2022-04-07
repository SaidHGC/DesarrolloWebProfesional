using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
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
        }

        protected void btnIngresar_Click(object sender, EventArgs e)
        {
            try
            {
                string usuario = this.txtUsername.Text;

                //encriptamos la cadena inicial       
                //txtPassword.Text = Seguridad.Encriptar(txtPassword.Text);
                string pass = this.txtPassword.Text;


                if (usuario != "" && pass != "")
                {
                    using (dcGlobal = new ManoAmigaSysDataContext())
                    {
                        List<Usuarios> listaUsuarios = dcGlobal.GetTable<Usuarios>().Where(c => (c.strUsername.Equals(usuario))).ToList();

                        if (listaUsuarios.Count > 0)
                        {
                            foreach (var user in listaUsuarios)
                            {
                                if (Seguridad.Encriptar(pass).Equals(user.strPassword))
                                {
                                    baseEntity = user;
                                }
                            }
                        }
                    }
                    if (baseEntity != null)
                    {
                            //session = new SessionManager(baseEntity.idUsuario);
                            session.Pantalla = "~/Menu.aspx";
                            Session["SessionManager"] = this.session;
                            Session["idUsuario"] = baseEntity.idUsuario;
                            Session["idPerfil"] = baseEntity.idPerfil;
                            this.Response.Redirect(this.session.Pantalla, false);
                    }
                    else
                    {
                        this.showMessage("El usuario no existe o contraseña incorrecta");
                    }
                }
                else
                {
                    this.showMessage("Alguno o ambos campos estan vacios");
                }
            }
            catch (Exception _e)
            {
                this.showMessage("Ha ocurrido un error inesperado");
            }
        }
    }
}