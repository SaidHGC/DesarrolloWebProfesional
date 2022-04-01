using System;

namespace UTTT.Ejemplo.Persona.Data.Entity
{
    public class Empleados
    {
        private int idEmpleado;
        private String strNombre;
        private String strApPaterno;
        private String strApMaterno;
        private DateTime dteFechaIngreso;
        private int idCatCede;
        private EmpCatCede catCedeTemp;
        private string strValorCede;
        private int idCatSexo;
        private EmpCatSexo catSexoTemp;
        private String strValorSexo;
        private String strEmail;

        public int IdEmpleado
        {
            get { return idEmpleado; }
            set { idEmpleado = value; }
        }

        public String StrNombre
        {
            get { return strNombre; }
            set { strNombre = value; }
        }

        public String StrApPaterno
        {
            get { return strApPaterno; }
            set { strApPaterno = value; }
        }

        public String StrApMaterno
        {
            get { return strApMaterno; }
            set { strApMaterno = value; }
        }
        
        public String StrEmail
        {
            get { return strEmail; }
            set { strEmail = value; }
        }

        public DateTime FechaIngreso
        {
            get { return dteFechaIngreso; }
            set { dteFechaIngreso = value; }
        }

        public int IdCatCede
        {
            get { return idCatCede; }
            set { idCatCede = value; }
        }

        public EmpCatCede CatCedeTemp
        {
            get { return catCedeTemp; }
            set { catCedeTemp = value; }
        }

        public String StrValorCede
        {
            get { return strValorCede; }
            set { strValorCede = value; }
        }

        public int IdCatSexo
        {
            get { return idCatSexo; }
            set { idCatSexo = value; }
        }

        public EmpCatSexo CatSexoTemp
        {
            get { return catSexoTemp; }
            set { catSexoTemp = value; }
        }

        public String StrValorSexo
        {
            get { return strValorSexo; }
            set { strValorSexo = value; }
        }
    }
}
