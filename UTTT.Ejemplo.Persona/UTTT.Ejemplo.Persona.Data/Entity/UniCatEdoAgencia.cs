using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UTTT.Ejemplo.Persona.Data.Entity
{
    public class UniCatEdoAgencia
    {
        private int idEdoAgencia;
        private String strValor;
        private String strDescripcion;

        public int Id
        {
            get { return idEdoAgencia; }
            set { idEdoAgencia = value; }
        }

        public String StrValor
        {
            get { return strValor; }
            set { strValor = value; }
        }

        public String StrDescripcion
        {
            get { return strDescripcion; }
            set { strDescripcion = value; }
        }

        public override string ToString()
        {
            return this.strValor;
        }
    }
}
