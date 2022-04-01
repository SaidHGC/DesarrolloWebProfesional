using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UTTT.Ejemplo.Persona.Data.Entity
{
    public class Corridas
    {
        private int idCorrida;
        private String strPuntoInicio;
        private String strPuntoFinal;
        private int idCatCede;
        private EmpCatCede catCedeTemp;
        private string strValorCede;
        private String strTipoCorrida;

        public int IdCorrida
        {
            get { return idCorrida; }
            set { idCorrida = value; }
        }

        public String StrPuntoInicio
        {
            get { return strPuntoInicio; }
            set { strPuntoInicio = value; }
        }

        public String StrPuntoFinal
        {
            get { return strPuntoFinal; }
            set { strPuntoFinal = value; }
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

        public String StrTipoCorrida
        {
            get { return strTipoCorrida; }
            set { strTipoCorrida = value; }
        }

    }
}
