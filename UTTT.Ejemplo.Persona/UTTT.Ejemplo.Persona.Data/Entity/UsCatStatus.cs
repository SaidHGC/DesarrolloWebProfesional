using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UTTT.Ejemplo.Persona.Data.Entity
{
    public class UsCatStatus
    {
        private int idStatus;
        private String strValor;
        private String strDescripcion;

        public int Id
        {
            get { return idStatus; }
            set { idStatus = value; }
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
