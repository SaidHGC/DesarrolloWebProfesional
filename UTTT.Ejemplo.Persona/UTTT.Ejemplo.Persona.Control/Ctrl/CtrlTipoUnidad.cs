using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using UTTT.Ejemplo.Persona.Control.Interface;

namespace UTTT.Ejemplo.Persona.Control.Ctrl
{
    public class CtrlTipoUnidad : Conexion, IOperacion
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

                SqlCommand comm = new SqlCommand("Select * from UniTipoUnidad ", conn);
                SqlDataReader reader = comm.ExecuteReader();

                List<Object> lista = new List<object>();
                while (reader.Read())
                {
                    UTTT.Ejemplo.Persona.Data.Entity.UniCatTipoUnidad tipoUnidad = new Data.Entity.UniCatTipoUnidad();
                    tipoUnidad.Id = int.Parse(reader["idTipoUnidad"].ToString());
                    tipoUnidad.StrValor = reader["strValor"].ToString();
                    tipoUnidad.StrDescripcion = reader["strDescripcion"].ToString();

                    Object objeto = tipoUnidad;
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
