using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using UTTT.Ejemplo.Persona.Control.Interface;

namespace UTTT.Ejemplo.Persona.Control.Ctrl
{
    public class CtrlCatAseguradoras : Conexion, IOperacion
    {
        public bool insertar(object _o)
        {
            try
            {
                UTTT.Ejemplo.Persona.Data.Entity.Aseguradoras aseguradora = (UTTT.Ejemplo.Persona.Data.Entity.Aseguradoras)_o;
                SqlConnection conn = base.sqlConnection();
                conn.Open();
                SqlCommand comm = new SqlCommand("INSERT INTO UniAseguradoras (strValor,strDescripcion) VALUES( '"
                    + aseguradora.StrValorAseguradora + "','"
                    + aseguradora.StrDescripcionAseguradora + "')", conn);
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
                UTTT.Ejemplo.Persona.Data.Entity.Aseguradoras aseguradora = (UTTT.Ejemplo.Persona.Data.Entity.Aseguradoras)_o;
                SqlConnection conn = base.sqlConnection();
                conn.Open();
                SqlCommand comm = new SqlCommand("Delete from UniAseguradoras where idAseguradora=" + aseguradora.IdAseguradora, conn);
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
                UTTT.Ejemplo.Persona.Data.Entity.Aseguradoras aseguradora = (UTTT.Ejemplo.Persona.Data.Entity.Aseguradoras)_o;
                SqlConnection conn = base.sqlConnection();
                conn.Open();
                SqlCommand comm = new SqlCommand
                    ("Update UniAseguradoras  set  strValor='" + aseguradora.StrValorAseguradora +
                    "', strDescripcion ='" + aseguradora.StrDescripcionAseguradora +
                     "' where idAseguradora=" + aseguradora.IdAseguradora, conn);

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
                UTTT.Ejemplo.Persona.Data.Entity.Aseguradoras aseguradora = (UTTT.Ejemplo.Persona.Data.Entity.Aseguradoras)_o;
                SqlConnection conn = base.sqlConnection();
                conn.Open();

                SqlCommand comm = new SqlCommand("Select * from UniAseguradoras ", conn);
                SqlDataReader reader = comm.ExecuteReader();

                List<Object> lista = new List<object>();
                while (reader.Read())
                {
                    UTTT.Ejemplo.Persona.Data.Entity.Aseguradoras aseguradoraTemp = new Data.Entity.Aseguradoras();
                    aseguradoraTemp.IdAseguradora = int.Parse(reader["idAseguradora"].ToString());
                    aseguradora.StrValorAseguradora = reader["strValor"].ToString();
                    aseguradora.StrDescripcionAseguradora = reader["strDescripcion"].ToString();
                    Object objeto = aseguradoraTemp;
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
                UTTT.Ejemplo.Persona.Data.Entity.Aseguradoras aseguradora = (UTTT.Ejemplo.Persona.Data.Entity.Aseguradoras)_o;
                SqlConnection conn = base.sqlConnection();
                conn.Open();
                SqlCommand comm = new SqlCommand("Select * from UniAseguradoras where idAseguradora=" + aseguradora.IdAseguradora + " ", conn);
                SqlDataReader reader = comm.ExecuteReader();
                UTTT.Ejemplo.Persona.Data.Entity.Aseguradoras aseguradoraTemp = new Data.Entity.Aseguradoras();
                while (reader.Read())
                {
                    aseguradoraTemp.IdAseguradora = int.Parse(reader["idAseguradora"].ToString());
                    aseguradoraTemp.StrValorAseguradora = reader["strValor"].ToString();
                    aseguradoraTemp.StrDescripcionAseguradora = reader["strDescripcion"].ToString();
                }
                conn.Close();
                Object objeto = aseguradoraTemp;
                return objeto;
            }
            catch (Exception _e)
            {

            }
            return null;
        }
    }
}
