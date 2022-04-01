using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using UTTT.Ejemplo.Persona.Control.Interface;

namespace UTTT.Ejemplo.Persona.Control.Ctrl
{
    public class CtrlDisponibilidad : Conexion, IOperacion
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

                SqlCommand comm = new SqlCommand("Select * from UniDisponibilidad ", conn);
                SqlDataReader reader = comm.ExecuteReader();

                List<Object> lista = new List<object>();
                while (reader.Read())
                {
                    UTTT.Ejemplo.Persona.Data.Entity.UniCatDisponibilidad catDisponibilidad = new Data.Entity.UniCatDisponibilidad();
                    catDisponibilidad.Id = int.Parse(reader["idDisponibilidad"].ToString());
                    catDisponibilidad.StrValor = reader["strValor"].ToString();
                    catDisponibilidad.StrDescripcion = reader["strDescripcion"].ToString();

                    Object objeto = catDisponibilidad;
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
