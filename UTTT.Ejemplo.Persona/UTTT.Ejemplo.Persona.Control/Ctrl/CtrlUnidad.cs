using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using UTTT.Ejemplo.Persona.Control.Interface;

namespace UTTT.Ejemplo.Persona.Control.Ctrl
{
    public class CtrlUnidad : Conexion, IOperacion
    {
        public bool insertar(object _o)
        {
            try
            {
                UTTT.Ejemplo.Persona.Data.Entity.Unidades unidad = (UTTT.Ejemplo.Persona.Data.Entity.Unidades)_o;
                SqlConnection conn = base.sqlConnection();
                conn.Open();
                SqlCommand comm = new SqlCommand("INSERT INTO Unidades (strPlacas,strModelo,strMarca,idTipoUnidad,idEdoAgencia,idAseguradora,idDisponibilidad) VALUES( '"
                    + unidad.StrPlacas + "','"
                    + unidad.IntModelo + "','"
                    + unidad.StrMarca + "','"
                    + unidad.IdTipoUnidad + "','"
                    + unidad.IdEdoAgencia + "','"
                    + unidad.IdAseguradora + "','"
                    + unidad.IdDisponibilidad + "')", conn);

                comm.ExecuteNonQuery();
                conn.Close();
                return true;
            }
            catch (Exception _e)
            {

            }
            return false;
        }

        public bool eliminar(object _o)
        {
            try
            {
                UTTT.Ejemplo.Persona.Data.Entity.Unidades unidad = (UTTT.Ejemplo.Persona.Data.Entity.Unidades)_o;
                SqlConnection conn = base.sqlConnection();
                conn.Open();
                SqlCommand comm = new SqlCommand("Delete from Unidades where idUnidad=" + unidad.IdUnidad, conn);
                comm.ExecuteNonQuery();
                conn.Close();
                return true;
            }
            catch (Exception _e)
            {

            }
            return false;
        }

        public bool actualizar(object _o)
        {
            try
            {
                UTTT.Ejemplo.Persona.Data.Entity.Unidades unidad = (UTTT.Ejemplo.Persona.Data.Entity.Unidades)_o;
                SqlConnection conn = base.sqlConnection();
                conn.Open();
                SqlCommand comm = new SqlCommand
                    ("Update Unidades  set  strPlacas='" + unidad.StrPlacas +
                    "', strModelo ='" + unidad.IntModelo +
                    "', strMarca ='" + unidad.StrMarca +
                    "', idTipoUnidad ='" + unidad.IdTipoUnidad +
                    "', idEdoAgencia ='" + unidad.IdEdoAgencia +
                    "', idAseguradora ='" + unidad.IdAseguradora +
                    "', idDisponibilidad ='" + unidad.IdDisponibilidad +
                     "' where idUnidad=" + unidad.IdUnidad, conn);

                comm.ExecuteNonQuery();
                conn.Close();
                return true;
            }
            catch (Exception _e)
            {

            }
            return false;
        }

        public List<object> consultarLista(object _o)
        {
            try
            {
                UTTT.Ejemplo.Persona.Data.Entity.Unidades unidad = (UTTT.Ejemplo.Persona.Data.Entity.Unidades)_o;
                SqlConnection conn = base.sqlConnection();
                conn.Open();

                SqlCommand comm = new SqlCommand("Select * from Unidades ", conn);
                SqlDataReader reader = comm.ExecuteReader();

                List<Object> lista = new List<object>();
                while (reader.Read())
                {
                    UTTT.Ejemplo.Persona.Data.Entity.Unidades unidadTemp = new Data.Entity.Unidades();
                    unidadTemp.IdUnidad = int.Parse(reader["idUnidad"].ToString());
                    unidadTemp.StrPlacas = reader["strPlacas"].ToString();
                    unidadTemp.IntModelo = int.Parse(reader["strModelo"].ToString());
                    unidadTemp.StrMarca = reader["strMarca"].ToString();
                    unidadTemp.IdTipoUnidad = int.Parse(reader["idTipoUnidad"].ToString());
                    unidadTemp.IdEdoAgencia = int.Parse(reader["idEdoAgencia"].ToString());
                    unidadTemp.IdAseguradora = int.Parse(reader["idAseguradora"].ToString());
                    unidadTemp.IdDisponibilidad = int.Parse(reader["idDisponibilidad"].ToString());

                    Object objeto = unidadTemp;
                    lista.Add(objeto);


                }
                conn.Close();
                return lista;
            }
            catch (Exception _e)
            {

            }
            return null;
        }

        public object consultarItem(object _o)
        {
            try
            {
                UTTT.Ejemplo.Persona.Data.Entity.Unidades unidad = (UTTT.Ejemplo.Persona.Data.Entity.Unidades)_o;
                SqlConnection conn = base.sqlConnection();
                conn.Open();
                SqlCommand comm = new SqlCommand("Select * from Unidades where idUnidad=" + unidad.IdUnidad + " ", conn);
                SqlDataReader reader = comm.ExecuteReader();
                UTTT.Ejemplo.Persona.Data.Entity.Unidades unidadTemp = new Data.Entity.Unidades();
                while (reader.Read())
                {
                    unidadTemp.IdUnidad = int.Parse(reader["idUnidad"].ToString());
                    unidadTemp.StrPlacas = reader["strPlacas"].ToString();
                    unidadTemp.IntModelo = int.Parse(reader["strModelo"].ToString());
                    unidadTemp.StrMarca = reader["strMarca"].ToString();
                }
                conn.Close();
                Object objeto = unidadTemp;
                return objeto;
            }
            catch (Exception _e)
            {

            }
            return null;
        }
    }
}
