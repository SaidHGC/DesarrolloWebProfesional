using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using UTTT.Ejemplo.Persona.Control.Interface;

namespace UTTT.Ejemplo.Persona.Control.Ctrl
{
    public class CtrlCorridas : Conexion, IOperacion
    {
        public bool insertar(object _o)
        {
            try
            {
                UTTT.Ejemplo.Persona.Data.Entity.Corridas corrida = (UTTT.Ejemplo.Persona.Data.Entity.Corridas)_o;
                SqlConnection conn = base.sqlConnection();
                conn.Open();
                SqlCommand comm = new SqlCommand("INSERT INTO Corridas (strPuntoInicio,strPuntoFinal,idCede,strTipoCorrida) VALUES( '"
                    + corrida.StrPuntoInicio + "','"
                    + corrida.StrPuntoFinal + "','"
                    + corrida.IdCatCede + "','"
                    + corrida.StrTipoCorrida + "')", conn);
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
                UTTT.Ejemplo.Persona.Data.Entity.Corridas corrida = (UTTT.Ejemplo.Persona.Data.Entity.Corridas)_o;
                SqlConnection conn = base.sqlConnection();
                conn.Open();
                SqlCommand comm = new SqlCommand("Delete from Corridas where idCorrida=" + corrida.IdCorrida, conn);
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
                UTTT.Ejemplo.Persona.Data.Entity.Corridas corrida = (UTTT.Ejemplo.Persona.Data.Entity.Corridas)_o;
                SqlConnection conn = base.sqlConnection();
                conn.Open();
                SqlCommand comm = new SqlCommand
                    ("Update Corridas  set  strPuntoInicio='" + corrida.StrPuntoInicio +
                    "', strPuntoFinal ='" + corrida.StrPuntoFinal +
                    "', idCede ='" + corrida.IdCatCede +
                    "', strTipoCorrida ='" + corrida.StrTipoCorrida +
                     "' where idCorrida=" + corrida.IdCorrida, conn);

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
                UTTT.Ejemplo.Persona.Data.Entity.Corridas corrida = (UTTT.Ejemplo.Persona.Data.Entity.Corridas)_o;
                SqlConnection conn = base.sqlConnection();
                conn.Open();

                SqlCommand comm = new SqlCommand("Select * from Corridas ", conn);
                SqlDataReader reader = comm.ExecuteReader();

                List<Object> lista = new List<object>();
                while (reader.Read())
                {
                    UTTT.Ejemplo.Persona.Data.Entity.Corridas corridaTemp = new Data.Entity.Corridas();
                    corridaTemp.IdCorrida = int.Parse(reader["idCorrida"].ToString());
                    corridaTemp.StrPuntoInicio = reader["strPuntoInicio"].ToString();
                    corridaTemp.StrPuntoFinal = reader["strPuntoFinal"].ToString();
                    corridaTemp.IdCatCede = int.Parse(reader["idCede"].ToString());
                    corridaTemp.StrTipoCorrida = reader["strTipoCorrida"].ToString();
                    Object objeto = corridaTemp;
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
                UTTT.Ejemplo.Persona.Data.Entity.Corridas corrida = (UTTT.Ejemplo.Persona.Data.Entity.Corridas)_o;
                SqlConnection conn = base.sqlConnection();
                conn.Open();
                SqlCommand comm = new SqlCommand("Select * from Corridas where idCorrida=" + corrida.IdCorrida + " ", conn);
                SqlDataReader reader = comm.ExecuteReader();
                UTTT.Ejemplo.Persona.Data.Entity.Corridas corridaTemp = new Data.Entity.Corridas();
                while (reader.Read())
                {
                    corridaTemp.IdCorrida = int.Parse(reader["idCorrida"].ToString());
                    corridaTemp.StrPuntoInicio = reader["strPuntoInicio"].ToString();
                    corridaTemp.StrPuntoFinal = reader["strPuntoFinal"].ToString();
                    corridaTemp.StrTipoCorrida = reader["strTipoCorrida"].ToString();
                }
                conn.Close();
                Object objeto = corridaTemp;
                return objeto;
            }
            catch (Exception _e)
            {

            }
            return null;
        }

    }
}
