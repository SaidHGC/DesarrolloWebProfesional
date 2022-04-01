using System;

namespace UTTT.Ejemplo.Persona.Data.Entity
{
    public class EmpCatCede
    {
        private int idCede;
        private String strValor;
        private String strDescripcion;

        public int Id
        {
            get { return idCede; }
            set { idCede = value; }
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
