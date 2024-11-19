using MIDIS.ORI.Entidades;
using MIDIS.ORI.LogicaNegocio;
using MIDIS.Utiles;
//using MIDIS.UtilesMVC;
//using MIDIS.UtilesWeb;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
//using System.Web.Mail;
using System.Web.Mvc;
using MVCSisRRHH.Models;


namespace MVCSisRRHH.Controllers
{
    [Authorize]
	public class EmpleadoController: Controller
	{
        private readonly T_genm_empleado_LN _empleado_Servicio = new T_genm_empleado_LN();
        
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Inicializar()
        {
            object respuesta = new
            {
                permisos = "acceder,buscar,insertar,actualizar,mostrar,eliminar,activar"
            };

            return Json(respuesta);
        }

        [HttpGet]
        public JsonResult ListarEmpleados(Empleado_Request peticion)
        {
            peticion.Nombre = "%" + (String.IsNullOrEmpty(peticion.Nombre) ? "" : peticion.Nombre.ToUpper()) + "%";
            object respuesta = _empleado_Servicio.ListarEmpleados(peticion);
            return Json(respuesta, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ObtenerParaEditar(Empleado_Request peticion)
        {
            object respuesta = _empleado_Servicio.ObtenerParaEditar(peticion);

            return Json(respuesta);
        }

        [HttpGet]
        public JsonResult ListarCuentasEmpleado(Empleado_Request peticion)
        {
            object respuesta = _empleado_Servicio.ListarCuentasEmpleado(peticion);
            return Json(respuesta, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult ListarOrdenesEmpleado(Empleado_Request peticion)
        {
            object respuesta = _empleado_Servicio.ListarOrdenesEmpleado(peticion);
            return Json(respuesta, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult RegistrarCuentaEmpleado(EmpleadoCuenta_Registro registro)
        {
            try
            {
                registro.FechaRegistro = DateTime.Now;
                registro.IdUsuarioRegistro = (int)VariablesWeb.ConsultaInformacion.iCodUsuario;

                object respuesta = _empleado_Servicio.RegistrarCuentaEmpleado(registro);

                return Json(new { success = "True", responseText = respuesta });
            }
            catch (Exception ex)
            {
                return Json(new { success = "False", responseText = ex.Message });
            }
        }
        [HttpPost]
        public JsonResult RegistrarOrdenEmpleado(EmpleadoOrden_Registro registro)
        {
            try
            {
                registro.FechaRegistro = DateTime.Now;
                registro.IdUsuarioRegistro = (int)VariablesWeb.ConsultaInformacion.iCodUsuario;

                object respuesta = _empleado_Servicio.RegistrarOrdenEmpleado(registro);

                return Json(new { success = "True", responseText = respuesta });
            }
            catch (Exception ex)
            {
                return Json(new { success = "False", responseText = ex.Message });
            }
        }
        [HttpPost]
        public JsonResult ActualizarCuentaEmpleado(EmpleadoCuenta_Registro registro)
        {
            try
            {
                registro.FechaModificacion = DateTime.Now;
                registro.IdUsuarioModificacion = (int)VariablesWeb.ConsultaInformacion.iCodUsuario;

                object respuesta = _empleado_Servicio.ActualizarCuentaEmpleado(registro);

                return Json(new { success = "True", responseText = respuesta });
            }
            catch (Exception ex)
            {
                return Json(new { success = "False", responseText = ex.Message });
            }
        }
        [HttpPost]
        public JsonResult ActualizarOrdenEmpleado(EmpleadoOrden_Registro registro)
        {
            try
            {
                registro.FechaModificacion = DateTime.Now;
                registro.IdUsuarioModificacion = (int)VariablesWeb.ConsultaInformacion.iCodUsuario;

                object respuesta = _empleado_Servicio.ActualizarOrdenEmpleado(registro);

                return Json(new { success = "True", responseText = respuesta });
            }
            catch (Exception ex)
            {
                return Json(new { success = "False", responseText = ex.Message });
            }
        }
        [HttpPost]
        public JsonResult EliminarCuentaEmpleado(EmpleadoCuenta_Registro registro)
        {
            try
            {
                registro.FechaModificacion = DateTime.Now;
                registro.IdUsuarioModificacion = (int)VariablesWeb.ConsultaInformacion.iCodUsuario;

                object respuesta = _empleado_Servicio.EliminarCuentaEmpleado(registro);

                return Json(new { success = "True", responseText = respuesta });
            }
            catch (Exception ex)
            {
                return Json(new { success = "False", responseText = ex.Message });
            }
        }
        [HttpPost]
        public JsonResult EliminarOrdenEmpleado(EmpleadoOrden_Registro registro)
        {
            try
            {
                registro.FechaModificacion = DateTime.Now;
                registro.IdUsuarioModificacion = (int)VariablesWeb.ConsultaInformacion.iCodUsuario;

                object respuesta = _empleado_Servicio.EliminarOrdenEmpleado(registro);

                return Json(new { success = "True", responseText = respuesta });
            }
            catch (Exception ex)
            {
                return Json(new { success = "False", responseText = ex.Message });
            }
        }
        [HttpPost]
        public JsonResult Registrar(Empleado_Registro registro)
        {
            try
            {
                registro.FechaRegistro = DateTime.Now;
                registro.IdUsuarioRegistro = (int)VariablesWeb.ConsultaInformacion.iCodUsuario;

                object respuesta = _empleado_Servicio.RegistrarEmpleado(registro);

                return Json(new { success = "True", responseText = respuesta });
            }
            catch (Exception ex)
            {
                return Json(new { success = "False", responseText = ex.Message });
            }
        }

        [HttpPost]
        public JsonResult Guardar(Empleado_Registro registro)
        {
            try
            {                
                registro.FechaRegistro = DateTime.Now;
                registro.IdUsuarioModificacion = (int)VariablesWeb.ConsultaInformacion.iCodUsuario;

                object respuesta = _empleado_Servicio.ActualizarEmpleado(registro);

                return Json(new { success = "True", responseText = respuesta });
            }
            catch (System.Data.SqlClient.SqlException es)
            {
                if (es.Number == 2627)
                    return Json(new { success = "False", responseText = "Empleado ya existente en la nómina" });
                else
                    return Json(new { success = "False", responseText = es.Message });
            }
            catch (Exception ex)
            {
                return Json(new { success = "False", responseText = ex.Message });
            }
        }
        [HttpPost]
        public JsonResult GuardarContacto(Empleado_Registro registro)
        {
            try
            {
                registro.FechaRegistro = DateTime.Now;
                registro.IdUsuarioModificacion = (int)VariablesWeb.ConsultaInformacion.iCodUsuario;

                object respuesta = _empleado_Servicio.ActualizarEmpleadoContacto(registro);

                return Json(new { success = "True", responseText = respuesta });
            }
            catch (System.Data.SqlClient.SqlException es)
            {
                if (es.Number == 2627)
                    return Json(new { success = "False", responseText = "Empleado ya existente en la nómina" });
                else
                    return Json(new { success = "False", responseText = es.Message });
            }
            catch (Exception ex)
            {
                return Json(new { success = "False", responseText = ex.Message });
            }
        }
	}
}