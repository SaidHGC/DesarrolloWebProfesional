using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Web.Services;
using UTTT.Ejemplo.Linq.Data.Entity;

namespace UTTT.Ejemplo.Persona.WebServices
{
    /// <summary>
    /// Summary description for Service1
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class Ejemplo : System.Web.Services.WebService
    {
        #region Web Metodos Persona

        [WebMethod]
        public bool insertarPersona(UTTT.Ejemplo.Persona.Data.Entity.Empleados _empleado)
        {
            try
            {
                DataContext dcTemp = new ManoAmigaSysDataContext();
                UTTT.Ejemplo.Linq.Data.Entity.Empleados empleado = new Linq.Data.Entity.Empleados();
                empleado.strNombre = _empleado.StrNombre;
                empleado.strApPaterno = _empleado.StrApPaterno;
                empleado.strApMaterno = _empleado.StrApMaterno;
                empleado.strEmail = _empleado.StrEmail;
                empleado.dteFechaIngreso = _empleado.FechaIngreso;
                empleado.idCede = _empleado.IdCatCede;
                empleado.idSexo = _empleado.IdCatSexo;
                //empleado.idStatus = _empleado.IdCatStatus;
                //empleado.idPuesto = _empleado.IdCatPuesto;

                dcTemp.GetTable<UTTT.Ejemplo.Linq.Data.Entity.Empleados>().InsertOnSubmit(empleado);
                dcTemp.SubmitChanges();
                dcTemp.Dispose();
                return true;
            }
            catch(Exception _e)
            {
                return false;
            }
        }

        [WebMethod]
        public bool editarPersona(UTTT.Ejemplo.Persona.Data.Entity.Empleados _empleado)
        {
            try
            {
                DataContext dcTemp = new ManoAmigaSysDataContext();
                UTTT.Ejemplo.Linq.Data.Entity.Empleados empleado = 
                    dcTemp.GetTable<UTTT.Ejemplo.Linq.Data.Entity.Empleados>().First(c => c.idEmpleado == _empleado.IdEmpleado);
                empleado.strNombre = _empleado.StrNombre;
                empleado.strApPaterno = _empleado.StrApPaterno;
                empleado.strApMaterno = _empleado.StrApMaterno;
                empleado.strEmail = _empleado.StrEmail;
                empleado.dteFechaIngreso = _empleado.FechaIngreso;
                empleado.idCede = _empleado.IdCatCede;
                empleado.idSexo = _empleado.IdCatSexo;
                //empleado.idStatus = _empleado.IdCatStatus;
                //empleado.idPuesto = _empleado.IdCatPuesto;

                dcTemp.SubmitChanges();
                dcTemp.Dispose();
                return true;
            }
            catch (Exception _e)
            {
                return false;
            }
        }

        [WebMethod]
        public bool eliminarPersona(UTTT.Ejemplo.Persona.Data.Entity.Empleados _empleado)
        {
            try
            {
                DataContext dcTemp = new ManoAmigaSysDataContext();
                UTTT.Ejemplo.Linq.Data.Entity.Empleados empleado =
                    dcTemp.GetTable<UTTT.Ejemplo.Linq.Data.Entity.Empleados>().First(c => c.idEmpleado == _empleado.IdEmpleado);
                dcTemp.GetTable<UTTT.Ejemplo.Linq.Data.Entity.Empleados>().DeleteOnSubmit(empleado);
                dcTemp.SubmitChanges();
                dcTemp.Dispose();
                return true;
            }
            catch (Exception _e)
            {
                return false;
            }
        }

        [WebMethod]
        public UTTT.Ejemplo.Persona.Data.Entity.Empleados[] consultarGlobalPersona()
        {
            try
            {
                DataContext dcTemp = new ManoAmigaSysDataContext();
                List<UTTT.Ejemplo.Linq.Data.Entity.Empleados> listaEmpleados =
                    dcTemp.GetTable<UTTT.Ejemplo.Linq.Data.Entity.Empleados>().ToList();
                UTTT.Ejemplo.Persona.Data.Entity.Empleados[] tempEmpleado = new Data.Entity.Empleados[listaEmpleados.Count()];
                
                for (int i = 0; i < listaEmpleados.Count(); i++)
                {
                    //asignamos el objeto persona uno por uno
                    UTTT.Ejemplo.Persona.Data.Entity.Empleados temp = new Data.Entity.Empleados();
                    temp.IdEmpleado = listaEmpleados[i].idEmpleado;
                    temp.StrNombre = listaEmpleados[i].strNombre;
                    temp.StrApPaterno = listaEmpleados[i].strApPaterno;
                    temp.StrApMaterno = listaEmpleados[i].strApMaterno;
                    temp.StrEmail = listaEmpleados[i].strEmail;
                    temp.FechaIngreso = (DateTime)listaEmpleados[i].dteFechaIngreso;
                    temp.StrValorCede = listaEmpleados[i].EmpCede.strValor;
                    temp.StrValorSexo = listaEmpleados[i].EmpSexo.strValor;
                    //temp.StrValorStatus = listaEmpleados[i].EmpCatStatus.strValor;
                    //temp.StrValorPuesto = listaEmpleados[i].EmpCatPuesto.strValor;

                    //asignamos el objeto catCede adjunto al de persona
                    UTTT.Ejemplo.Persona.Data.Entity.EmpCatCede catCedeTemp = new Data.Entity.EmpCatCede();
                    catCedeTemp.Id = listaEmpleados[i].EmpCede.IdCede;
                    catCedeTemp.StrValor = listaEmpleados[i].EmpCede.strValor;
                    temp.CatCedeTemp = catCedeTemp;

                    //asignamos el objeto catsexo adjunto al de persona
                    UTTT.Ejemplo.Persona.Data.Entity.EmpCatSexo catSexoTemp = new Data.Entity.EmpCatSexo();
                    catSexoTemp.Id = listaEmpleados[i].EmpSexo.idSexo;
                    catSexoTemp.StrValor = listaEmpleados[i].EmpSexo.strValor;
                    temp.CatSexoTemp = catSexoTemp;

                    tempEmpleado[i] = temp;
                }
                dcTemp.Dispose();
                return tempEmpleado;
               
            }
            catch (Exception _e)
            {
                return null;
            }
            
        }

        [WebMethod]
        public UTTT.Ejemplo.Persona.Data.Entity.Empleados consultarUnicaPersona(UTTT.Ejemplo.Persona.Data.Entity.Empleados _empleado)
        {
            try
            {
                DataContext dcTemp = new ManoAmigaSysDataContext();
             
                //objeto persona
                UTTT.Ejemplo.Linq.Data.Entity.Empleados empleado =
                    dcTemp.GetTable<UTTT.Ejemplo.Linq.Data.Entity.Empleados>().First(c => c.idEmpleado == _empleado.IdEmpleado);
                UTTT.Ejemplo.Persona.Data.Entity.Empleados temp = new Data.Entity.Empleados();
                temp.IdEmpleado = empleado.idEmpleado;
                temp.StrNombre = empleado.strNombre;
                temp.StrApPaterno = empleado.strApPaterno;
                temp.StrApMaterno = empleado.strApMaterno;
                temp.StrEmail = empleado.strEmail;
                temp.FechaIngreso = (DateTime)empleado.dteFechaIngreso;
                temp.StrValorCede = empleado.EmpCede.strValor;
                temp.StrValorSexo = empleado.EmpSexo.strValor;
                //temp.StrValorStatus = empleado.EmpCatStatus.strValor;
                //temp.StrValorPuesto = empleado.EmpCatPuesto.strValor;

                //asignamos el objeto catCede adjunto al de persona
                UTTT.Ejemplo.Persona.Data.Entity.EmpCatCede catCedeTemp = new Data.Entity.EmpCatCede();
                catCedeTemp.Id = empleado.EmpCede.IdCede;
                catCedeTemp.StrValor = empleado.EmpCede.strValor;
                temp.CatCedeTemp = catCedeTemp;

                //asignamos el objeto catSexo adjunto al de persona
                UTTT.Ejemplo.Persona.Data.Entity.EmpCatSexo catSexoTemp = new Data.Entity.EmpCatSexo();
                catSexoTemp.Id = empleado.EmpSexo.idSexo;
                catSexoTemp.StrValor = empleado.EmpSexo.strValor;
                temp.CatSexoTemp = catSexoTemp;

                dcTemp.Dispose();
                return temp;                
            }
            catch (Exception _e)
            {
                return null;
            }            
        }

        #endregion

        #region Catalogo Sexo

        [WebMethod]
        public UTTT.Ejemplo.Persona.Data.Entity.EmpCatSexo[] consultaGlobalSexo()
        {
            try
            {
                DataContext dcTemp = new ManoAmigaSysDataContext();
                List<UTTT.Ejemplo.Linq.Data.Entity.EmpSexo> listaSexo = dcTemp.GetTable<UTTT.Ejemplo.Linq.Data.Entity.EmpSexo>().ToList();
                UTTT.Ejemplo.Persona.Data.Entity.EmpCatSexo[] tempSexo = new Data.Entity.EmpCatSexo[listaSexo.Count()];

                for (int i = 0; i < listaSexo.Count(); i++)
                {
                    //asignamos el objeto persona uno por uno
                    UTTT.Ejemplo.Persona.Data.Entity.EmpCatSexo temp = new Data.Entity.EmpCatSexo();
                    temp.Id = listaSexo[i].idSexo;
                    temp.StrValor = listaSexo[i].strValor;
                    tempSexo[i] = temp;
                }
                dcTemp.Dispose();
                return tempSexo;

            }
            catch (Exception _e)
            {
                return null;
            }
        }

        #endregion

        #region Catalogo Cede

        [WebMethod]
        public UTTT.Ejemplo.Persona.Data.Entity.EmpCatCede[] consultaGlobalCede()
        {
            try
            {
                DataContext dcTemp = new ManoAmigaSysDataContext();
                List<UTTT.Ejemplo.Linq.Data.Entity.EmpCede> listaCede = dcTemp.GetTable<UTTT.Ejemplo.Linq.Data.Entity.EmpCede>().ToList();
                UTTT.Ejemplo.Persona.Data.Entity.EmpCatCede[] tempCede = new Data.Entity.EmpCatCede[listaCede.Count()];

                for (int i = 0; i < listaCede.Count(); i++)
                {
                    //asignamos el objeto persona uno por uno
                    UTTT.Ejemplo.Persona.Data.Entity.EmpCatCede temp = new Data.Entity.EmpCatCede();
                    temp.Id = listaCede[i].IdCede;
                    temp.StrValor = listaCede[i].strValor;
                    tempCede[i] = temp;
                }
                dcTemp.Dispose();
                return tempCede;

            }
            catch (Exception _e)
            {
                return null;
            }
        }

        #endregion

        #region Catalogo Status

        //[WebMethod]
        //public UTTT.Ejemplo.Persona.Data.Entity.UsCatStatus[] consultaGlobalStatus()
        //{
        //    try
        //    {
        //        DataContext dcTemp = new SistemaManoAmigaDataContext();
        //        List<UTTT.Ejemplo.Linq.Data.Entity.EmpCatStatus> listaStatus = dcTemp.GetTable<UTTT.Ejemplo.Linq.Data.Entity.EmpCatStatus>().ToList();
        //        UTTT.Ejemplo.Persona.Data.Entity.UsCatStatus[] tempStatus = new Data.Entity.UsCatStatus[listaStatus.Count()];

        //        for (int i = 0; i < listaStatus.Count(); i++)
        //        {
        //            //asignamos el objeto persona uno por uno
        //            UTTT.Ejemplo.Persona.Data.Entity.UsCatStatus temp = new Data.Entity.UsCatStatus();
        //            temp.Id = listaStatus[i].idStatus;
        //            temp.StrValor = listaStatus[i].strValor;
        //            tempStatus[i] = temp;
        //        }
        //        dcTemp.Dispose();
        //        return tempStatus;

        //    }
        //    catch (Exception _e)
        //    {
        //        return null;
        //    }
        //}

        #endregion

        #region Catalogo Puesto

        //[WebMethod]
        //public UTTT.Ejemplo.Persona.Data.Entity.UsCatPuesto[] consultaGlobalPuesto()
        //{
        //    try
        //    {
        //        DataContext dcTemp = new SistemaManoAmigaDataContext();
        //        List<UTTT.Ejemplo.Linq.Data.Entity.EmpCatPuesto> listaPuesto = dcTemp.GetTable<UTTT.Ejemplo.Linq.Data.Entity.EmpCatPuesto>().ToList();
        //        UTTT.Ejemplo.Persona.Data.Entity.UsCatPuesto[] tempPuesto = new Data.Entity.UsCatPuesto[listaPuesto.Count()];

        //        for (int i = 0; i < listaPuesto.Count(); i++)
        //        {
        //            //asignamos el objeto persona uno por uno
        //            UTTT.Ejemplo.Persona.Data.Entity.UsCatPuesto temp = new Data.Entity.UsCatPuesto();
        //            temp.Id = listaPuesto[i].idPuesto;
        //            temp.StrValor = listaPuesto[i].strValor;
        //            tempPuesto[i] = temp;
        //        }
        //        dcTemp.Dispose();
        //        return tempPuesto;

        //    }
        //    catch (Exception _e)
        //    {
        //        return null;
        //    }
        //}

        #endregion

    }
}