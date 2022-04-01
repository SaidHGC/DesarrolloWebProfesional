using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using UTTT.Ejemplo.Persona.Control.Interface;

namespace UTTT.Ejemplo.Persona.Control.Ctrl
{
    public class CtrlEdoAgencia : Conexion, IOperacion
    {
        public bool actualizar(object _o)
        {
            throw new System.NotImplementedException();
        }

        public object consultarItem(object _o)
        {
            throw new System.NotImplementedException();
        }

        public List<object> consultarLista(object _o)
        {
            try
            {
                UTTT.Ejemplo.Persona.Data.Entity.Unidades unidad = (UTTT.Ejemplo.Persona.Data.Entity.Unidades)_o;
                SqlConnection conn = base.sqlConnection();
                conn.Open();

                SqlCommand comm = new SqlCommand("Select * from UniEdoAgencia ", conn);
                SqlDataReader reader = comm.ExecuteReader();

                List<Object> lista = new List<object>();
                while (reader.Read())
                {
                    UTTT.Ejemplo.Persona.Data.Entity.UniCatEdoAgencia edoAgencia = new Data.Entity.UniCatEdoAgencia();
                    edoAgencia.Id = int.Parse(reader["idEdoAgencia"].ToString());
                    edoAgencia.StrValor = reader["strValor"].ToString();
                    edoAgencia.StrDescripcion = reader["strDescripcion"].ToString();

                    Object objeto = edoAgencia;
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

        public bool eliminar(object _o)
        {
            throw new System.NotImplementedException();
        }

        public bool insertar(object _o)
        {
            throw new System.NotImplementedException();
        }
    }
}
