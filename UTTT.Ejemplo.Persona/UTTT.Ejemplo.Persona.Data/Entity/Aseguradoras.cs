using System;

namespace UTTT.Ejemplo.Persona.Data.Entity
{
    public class Aseguradoras
    {
        private int idAseguradora;
        private String strValor;
        private String strDescripcion;

        public int IdAseguradora
        {
            get { return idAseguradora; }
            set { idAseguradora = value; }
        }

        public String StrValorAseguradora
        {
            get { return strValor; }
            set { strValor = value; }
        }

        public String StrDescripcionAseguradora
        {
            get { return strDescripcion; }
            set { strDescripcion = value; }
        }
    }
}
