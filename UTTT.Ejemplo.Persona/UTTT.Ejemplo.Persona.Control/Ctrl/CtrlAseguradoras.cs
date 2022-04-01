using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using UTTT.Ejemplo.Persona.Control.Interface;

namespace UTTT.Ejemplo.Persona.Control.Ctrl
{
    public class CtrlAseguradoras : Conexion, IOperacion
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

                SqlCommand comm = new SqlCommand("Select * from UniAseguradoras ", conn);
                SqlDataReader reader = comm.ExecuteReader();

                List<Object> lista = new List<object>();
                while (reader.Read())
                {
                    UTTT.Ejemplo.Persona.Data.Entity.UniCatAseguradoras catAseguradora = new Data.Entity.UniCatAseguradoras();
                    catAseguradora.Id = int.Parse(reader["idAseguradora"].ToString());
                    catAseguradora.StrValor = reader["strValor"].ToString();
                    catAseguradora.StrDescripcion = reader["strDescripcion"].ToString();

                    Object objeto = catAseguradora;
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
