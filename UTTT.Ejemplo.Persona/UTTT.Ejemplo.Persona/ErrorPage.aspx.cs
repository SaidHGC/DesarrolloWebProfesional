using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace UTTT.Ejemplo.Persona
{
    public partial class ErrorPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //Se coloca la dirección que RECIBIRA el correo electronico
            EmailSender emailSender = new EmailSender("19300669@uttt.edu.mx");
            //Se asigna el valor que recibamos el cual vamos a enviar
            emailSender.sendMessage(PersonaPrincipal.ultimaExcepcion.ToString());
        }
    }
}