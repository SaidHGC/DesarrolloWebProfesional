using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using UTTT.Ejemplo.Persona.Control.Interface;

namespace UTTT.Ejemplo.Persona.Control.Ctrl
{
    public class CtrlEmpleado : Conexion, IOperacion
    {
        public bool insertar(object _o)
        {
            try
            {
                UTTT.Ejemplo.Persona.Data.Entity.Empleados empleado = (UTTT.Ejemplo.Persona.Data.Entity.Empleados)_o;
                SqlConnection conn = base.sqlConnection();
                conn.Open();
                SqlCommand comm = new SqlCommand("INSERT INTO Empleados (strNombre,strApPaterno,strApMaterno,strEmail,dteFechaIngreso,idCede,idSexo) VALUES( '"
                    + empleado.StrNombre + "','"
                    + empleado.StrApPaterno + "','"
                    + empleado.StrApMaterno + "','"
                    + empleado.StrEmail + "','"
                    + empleado.FechaIngreso + "','"
                    + empleado.IdCatCede + "','"
                    + empleado.IdCatSexo + "')", conn);
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
                UTTT.Ejemplo.Persona.Data.Entity.Empleados empleado = (UTTT.Ejemplo.Persona.Data.Entity.Empleados)_o;
                SqlConnection conn = base.sqlConnection();
                conn.Open();
                SqlCommand comm = new SqlCommand("Delete from Empleados where idEmpleado=" + empleado.IdEmpleado, conn);
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
                UTTT.Ejemplo.Persona.Data.Entity.Empleados empleado = (UTTT.Ejemplo.Persona.Data.Entity.Empleados)_o;
                SqlConnection conn = base.sqlConnection();
                conn.Open();
                SqlCommand comm = new SqlCommand
                    ("Update Empleados  set  strNombre='" + empleado.StrNombre +
                    "', strApPaterno ='" + empleado.StrApPaterno +
                    "', strApMaterno ='" + empleado.StrApMaterno +
                    "', strEmail ='" + empleado.StrEmail +
                    "', dteFechaIngreso ='" + empleado.FechaIngreso +
                    "', idCede ='" + empleado.IdCatCede +
                    "', idSexo ='" + empleado.IdCatSexo +
                     "' where idEmpleado=" + empleado.IdEmpleado, conn);

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
                UTTT.Ejemplo.Persona.Data.Entity.Empleados empleado = (UTTT.Ejemplo.Persona.Data.Entity.Empleados)_o;
                SqlConnection conn = base.sqlConnection();
                conn.Open();

                SqlCommand comm = new SqlCommand("Select * from Empleados ", conn);
                SqlDataReader reader = comm.ExecuteReader();

                List<Object> lista = new List<object>();
                while (reader.Read())
                {
                    UTTT.Ejemplo.Persona.Data.Entity.Empleados empleadoTemp = new Data.Entity.Empleados();
                    empleadoTemp.IdEmpleado = int.Parse(reader["idEmpleado"].ToString());
                    empleadoTemp.StrNombre = reader["strNombre"].ToString();
                    empleadoTemp.StrApPaterno = reader["strApPaterno"].ToString();
                    empleadoTemp.StrApMaterno = reader["strApMaterno"].ToString();
                    empleadoTemp.StrEmail = reader["strEmail"].ToString();
                    empleadoTemp.FechaIngreso = DateTime.Parse(reader["dteFechaIngreso"].ToString());
                    empleadoTemp.IdCatCede = int.Parse(reader["idCede"].ToString());
                    empleadoTemp.IdCatSexo = int.Parse(reader["idSexo"].ToString());
                    Object objeto = empleadoTemp;
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
                UTTT.Ejemplo.Persona.Data.Entity.Empleados empleado = (UTTT.Ejemplo.Persona.Data.Entity.Empleados)_o;
                SqlConnection conn = base.sqlConnection();
                conn.Open();
                SqlCommand comm = new SqlCommand("Select * from Empleados where idEmpleado=" + empleado.IdEmpleado + " ", conn);
                SqlDataReader reader = comm.ExecuteReader();
                UTTT.Ejemplo.Persona.Data.Entity.Empleados empleadoTemp = new Data.Entity.Empleados();
                while (reader.Read())
                {
                    empleadoTemp.IdEmpleado = int.Parse(reader["idEmpleado"].ToString());
                    empleadoTemp.StrNombre = reader["strNombre"].ToString();
                    empleadoTemp.StrApPaterno = reader["strApPaterno"].ToString();
                    empleadoTemp.StrApMaterno = reader["strApMaterno"].ToString();
                    empleadoTemp.FechaIngreso = DateTime.Parse(reader["dteFechaIngreso"].ToString());
                    empleadoTemp.StrEmail = reader["strEmail"].ToString();                    
                }
                conn.Close();
                Object objeto = empleadoTemp;
                return objeto;
            }
            catch (Exception _e)
            {

            }
            return null;
        }

        
    }
}
