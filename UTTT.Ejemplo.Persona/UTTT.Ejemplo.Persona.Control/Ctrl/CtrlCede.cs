using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using UTTT.Ejemplo.Persona.Control.Interface;

namespace UTTT.Ejemplo.Persona.Control.Ctrl
{
    public class CtrlCede : Conexion, IOperacion
    {
        public bool actualizar(object _o)
        {
            throw new NotImplementedException();
        }

        public object consultarItem(object _o)
        {
            throw new NotImplementedException();
        }

        public List<object> consultarLista(object _o)
        {
            try
            {
                UTTT.Ejemplo.Persona.Data.Entity.Empleados empleado = (UTTT.Ejemplo.Persona.Data.Entity.Empleados)_o;
                SqlConnection conn = base.sqlConnection();
                conn.Open();

                SqlCommand comm = new SqlCommand("Select * from EmpCede ", conn);
                SqlDataReader reader = comm.ExecuteReader();

                List<Object> lista = new List<object>();
                while (reader.Read())
                {
                    UTTT.Ejemplo.Persona.Data.Entity.EmpCatCede catCede = new Data.Entity.EmpCatCede();
                    catCede.Id = int.Parse(reader["idCede"].ToString());
                    catCede.StrValor = reader["strValor"].ToString();
                    catCede.StrDescripcion = reader["strDescripcion"].ToString();

                    Object objeto = catCede;
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
            throw new NotImplementedException();
        }

        public bool insertar(object _o)
        {
            throw new NotImplementedException();
        }
    }
}
