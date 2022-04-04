using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UTTT.Ejemplo.Persona.Data.Entity
{
    public class Unidades
    {
        private int idUnidad;
        private String strPlacas;
        private int intModelo;
        private String strMarca;
        private int idTipoUnidad;
        private UniCatTipoUnidad catTipoUnidadTemp;
        private string strValorTipoUnidad;
        private int idEdoAgencia;
        private UniCatEdoAgencia catEdoAgenciaTemp;
        private string strValorEdoAgencia;
        private int idAseguradora;
        private Aseguradoras catAseguradoraTemp;
        private string strValorAseguradora;
        private int idDisponibilidad;
        private UniCatDisponibilidad catDisponibilidadTemp;
        private string strValorDisponibilidad;

        public int IdUnidad
        {
            get { return idUnidad; }
            set { idUnidad = value; }
        }

        public String StrPlacas
        {
            get { return strPlacas; }
            set { strPlacas = value; }
        }

        public int IntModelo
        {
            get { return intModelo; }
            set { intModelo = value; }
        }

        public String StrMarca
        {
            get { return strMarca; }
            set { strMarca = value; }
        }

        public int IdTipoUnidad
        {
            get { return idTipoUnidad; }
            set { idTipoUnidad = value; }
        }

        public UniCatTipoUnidad CatTipoUnidadTemp
        {
            get { return catTipoUnidadTemp; }
            set { catTipoUnidadTemp = value; }
        }

        public String StrValorTipoUnidad
        {
            get { return strValorTipoUnidad; }
            set { strValorTipoUnidad = value; }
        }

        public int IdEdoAgencia
        {
            get { return idEdoAgencia; }
            set { idEdoAgencia = value; }
        }

        public UniCatEdoAgencia CatEdoAgenciaTemp
        {
            get { return catEdoAgenciaTemp; }
            set { catEdoAgenciaTemp = value; }
        }

        public String StrValorEdoAgencia
        {
            get { return strValorEdoAgencia; }
            set { strValorEdoAgencia = value; }
        }

        public int IdAseguradora
        {
            get { return idAseguradora; }
            set { idAseguradora = value; }
        }

        public Aseguradoras CatAseguradoraTemp
        {
            get { return catAseguradoraTemp; }
            set { catAseguradoraTemp = value; }
        }

        public String StrValorAseguradora
        {
            get { return strValorAseguradora; }
            set { strValorAseguradora = value; }
        }

        public int IdDisponibilidad
        {
            get { return idDisponibilidad; }
            set { idDisponibilidad = value; }
        }

        public UniCatDisponibilidad CatDisponibilidadTemp
        {
            get { return catDisponibilidadTemp; }
            set { catDisponibilidadTemp = value; }
        }

        public String StrValorDisponibilidad
        {
            get { return strValorDisponibilidad; }
            set { strValorDisponibilidad = value; }
        }
    }
}
