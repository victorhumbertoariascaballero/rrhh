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
using MVCSisGesRRHH.Models;
using MIDIS.ORI.Entidades.Core;

namespace MVCSisGesRRHH.Controllers
{
    [Authorize]
    public class EmpleadoController: Controller
	{
        private readonly T_genm_empleado_LN _empleado_Servicio = new T_genm_empleado_LN();
        
        [HttpGet]
        [Authorize]
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
        public JsonResult ListarParentesco()
        {
            List<Estado_Response> lista = new List<Estado_Response>();
            lista.Add(new Estado_Response("1", "ESPOSO (A)"));
            lista.Add(new Estado_Response("2", "CONVIVIENTE"));
            lista.Add(new Estado_Response("3", "HIJO (A)"));
            lista.Add(new Estado_Response("4", "PADRE"));
            lista.Add(new Estado_Response("5", "MADRE"));
            lista.Add(new Estado_Response("6", "HERMANO (A)"));

            object respuesta = lista;

            return Json(respuesta, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [Authorize]
        public JsonResult ListarEmpleadosMaestro(Empleado_Request peticion)
        {
            peticion.Nombre = "%" + (String.IsNullOrEmpty(peticion.Nombre) ? "" : peticion.Nombre.ToUpper()) + "%";
            object respuesta = _empleado_Servicio.ListarEmpleadosMaestro(peticion);
            var jsonResult = Json(respuesta, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
            //return Json(respuesta, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        [Authorize]
        public JsonResult ListarEmpleados(Empleado_Request peticion)
        {
            peticion.Nombre = "%" + (String.IsNullOrEmpty(peticion.Nombre) ? "" : peticion.Nombre.ToUpper()) + "%";
            object respuesta = _empleado_Servicio.ListarEmpleados(peticion);
            var jsonResult = Json(respuesta, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
            //return Json(respuesta, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [Authorize]
        public JsonResult ObtenerParaEditar(Empleado_Request peticion)
        {
            //Empleado_Registro rpta = _empleado_Servicio.ObtenerParaEditar(peticion);

            //if (rpta.lstDescuentoJudicialBeneficiario_Registro!=null && rpta.lstDescuentoJudicialBeneficiario_Registro.Count > 0)
            //{
            //    List<DescuentoJudicialBeneficiario_Registro> lstBeneficiario_Registro = new List<DescuentoJudicialBeneficiario_Registro>();                    
            //    Session["ListaBeneficiariosPlanillaJudicialTrabajadores"] = rpta.lstDescuentoJudicialBeneficiario_Registro;                
            //}
            object respuesta = _empleado_Servicio.ObtenerParaEditar(peticion); ;
            return Json(respuesta);
        }

        [HttpGet]
        [Authorize]
        public JsonResult ListarCuentasEmpleado(Empleado_Request peticion)
        {
            object respuesta = _empleado_Servicio.ListarCuentasEmpleado(peticion);
            return Json(respuesta, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        [Authorize]
        public JsonResult ListarOrdenesEmpleado(Empleado_Request peticion)
        {
            object respuesta = _empleado_Servicio.ListarOrdenesEmpleado(peticion);
            return Json(respuesta, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        [Authorize]
        public JsonResult RegistrarCuentaEmpleado(EmpleadoCuenta_Registro registro)
        {
            try
            {
                registro.FechaRegistro = DateTime.Now;
                registro.IdUsuarioRegistro = Convert.ToInt32(VariablesWeb.ConsultaInformacion.iCodUsuario);

                object respuesta = _empleado_Servicio.RegistrarCuentaEmpleado(registro);

                return Json(new { success = "True", responseText = respuesta });
            }
            catch (Exception ex)
            {
                return Json(new { success = "False", responseText = ex.Message });
            }
        }
        [HttpPost]
        [Authorize]
        public JsonResult RegistrarOrdenEmpleado(EmpleadoOrden_Registro registro)
        {
            try
            {
                registro.FechaRegistro = DateTime.Now;
                registro.IdUsuarioRegistro = Convert.ToInt32(VariablesWeb.ConsultaInformacion.iCodUsuario);

                object respuesta = _empleado_Servicio.RegistrarOrdenEmpleado(registro);

                return Json(new { success = "True", responseText = respuesta });
            }
            catch (Exception ex)
            {
                return Json(new { success = "False", responseText = ex.Message });
            }
        }
        [HttpPost]
        [Authorize]
        public JsonResult ActualizarCuentaEmpleado(EmpleadoCuenta_Registro registro)
        {
            try
            {
                registro.FechaModificacion = DateTime.Now;
                registro.IdUsuarioModificacion = Convert.ToInt32(VariablesWeb.ConsultaInformacion.iCodUsuario);

                object respuesta = _empleado_Servicio.ActualizarCuentaEmpleado(registro);

                return Json(new { success = "True", responseText = respuesta });
            }
            catch (Exception ex)
            {
                return Json(new { success = "False", responseText = ex.Message });
            }
        }
        [HttpPost]
        [Authorize]
        public JsonResult ActualizarOrdenEmpleado(EmpleadoOrden_Registro registro)
        {
            try
            {
                registro.FechaModificacion = DateTime.Now;
                registro.IdUsuarioModificacion = Convert.ToInt32(VariablesWeb.ConsultaInformacion.iCodUsuario);

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
                registro.IdUsuarioModificacion = Convert.ToInt32(VariablesWeb.ConsultaInformacion.iCodUsuario);

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
                registro.IdUsuarioModificacion = Convert.ToInt32(VariablesWeb.ConsultaInformacion.iCodUsuario);

                object respuesta = _empleado_Servicio.EliminarOrdenEmpleado(registro);

                return Json(new { success = "True", responseText = respuesta });
            }
            catch (Exception ex)
            {
                return Json(new { success = "False", responseText = ex.Message });
            }
        }
        [HttpPost]
        [Authorize]
        public JsonResult Registrar(Empleado_Registro registro)
        {
            try
            {
                registro.FechaRegistro = DateTime.Now;
                registro.IdUsuarioRegistro = Convert.ToInt32(VariablesWeb.ConsultaInformacion.iCodUsuario);

                object respuesta = _empleado_Servicio.RegistrarEmpleado(registro);

                return Json(new { success = "True", responseText = respuesta });
            }
            catch (Exception ex)
            {
                return Json(new { success = "False", responseText = ex.Message });
            }
        }

        [HttpPost]
        [Authorize]
        public JsonResult Guardar(Empleado_Registro registro)
        {
            try
            {                
                registro.FechaRegistro = DateTime.Now;
                registro.IdUsuarioModificacion = Convert.ToInt32(VariablesWeb.ConsultaInformacion.iCodUsuario);

                object respuesta = _empleado_Servicio.ActualizarEmpleado(registro);
                //KMM POR AHORA NO SE ACTUALIZA EL CONTRATO
                //try
                //{
                //    new T_genm_contrato_LN().ActualizarContratoNominaTrabajador(new EmpleadoContrato_Registro() {   IdContrato = registro.IdContrato, 
                //                                                                                                    NroAIRHSP = registro.NroAIRHSP,
                //                                                                                                    FechaModificacion = DateTime.Now,
                //                                                                                                    IdUsuarioModificacion = Convert.ToInt32(VariablesWeb.ConsultaInformacion.iCodUsuario)
                //    });
                //}
                //catch (Exception)
                //{

                //    //throw;
                //}

                return Json(new { success = "True", responseText = respuesta });
            }
            catch (System.Data.SqlClient.SqlException es)
            {
                if (es.Number == 2627)
                    return Json(new { success = "False", responseText = "Empleado ya existente en la n�mina" });
                else
                    return Json(new { success = "False", responseText = es.Message });
            }
            catch (Exception ex)
            {
                return Json(new { success = "False", responseText = ex.Message });
            }
        }
        [HttpPost]
        [Authorize]
        public JsonResult RegistrarCese(Empleado_Registro registro)
        {
            try
            {
                registro.FechaRegistro = DateTime.Now;
                registro.IdUsuarioModificacion = Convert.ToInt32(VariablesWeb.ConsultaInformacion.iCodUsuario);

                object respuesta = _empleado_Servicio.RegistrarCese(registro);

                return Json(new { success = "True", responseText = respuesta });
            }
            catch (Exception ex)
            {
                return Json(new { success = "False", responseText = ex.Message });
            }
        }
        [HttpPost]
        public JsonResult ObtenerIndicadores()
        {
            object respuesta = _empleado_Servicio.ObtenerIndicadores();

            return Json(respuesta);
        }
        [HttpPost]
        public JsonResult GuardarContacto(Empleado_Registro registro)
        {
            try
            {
                registro.FechaRegistro = DateTime.Now;
                registro.IdUsuarioModificacion = Convert.ToInt32(VariablesWeb.ConsultaInformacion.iCodUsuario);

                object respuesta = _empleado_Servicio.ActualizarEmpleadoContacto(registro);

                return Json(new { success = "True", responseText = respuesta });
            }
            catch (System.Data.SqlClient.SqlException es)
            {
                if (es.Number == 2627)
                    return Json(new { success = "False", responseText = "Empleado ya existente en la n�mina" });
                else
                    return Json(new { success = "False", responseText = es.Message });
            }
            catch (Exception ex)
            {
                return Json(new { success = "False", responseText = ex.Message });
            }
        }
        [HttpGet]
        [Authorize]
        public JsonResult ListarBooleanos()
        {
            List<Mes_Response> lista = new List<Mes_Response>();
            //lista.Add(new Mes_Response("", "SELECCIONE"));
            lista.Add(new Mes_Response("0", "NO"));
            lista.Add(new Mes_Response("1", "SI"));            
            object respuesta = lista;
            return Json(respuesta, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        [Authorize]
        public JsonResult ObtenerBeneficiarios(Empleado_Request peticion)
        {
            object respuesta = _empleado_Servicio.ObtenerBeneficiarios(peticion);
            return Json(respuesta, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [Authorize]
        public JsonResult ListarEncargaturasEmpleado(Empleado_Request peticion)
        {
            object respuesta = _empleado_Servicio.ListarEncargaturasEmpleado(peticion);
            return Json(respuesta, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        [Authorize]
        public JsonResult ListarFamiliaresEmpleado(Empleado_Request peticion)
        {
            object respuesta = _empleado_Servicio.ListarFamiliaresEmpleado(peticion);
            return Json(respuesta, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        [Authorize]
        public JsonResult ListarDesplazamientoEmpleado(Empleado_Request peticion)
        {
            object respuesta = _empleado_Servicio.ListarDesplazamientoEmpleado(peticion);
            return Json(respuesta, JsonRequestBehavior.AllowGet);
        }
        
        [HttpPost]
        [Authorize]
        public JsonResult RegistrarEncargaturasEmpleado(EmpleadoEncargatura_Registro registro)
        {
            try
            {
                //registro.FechaRegistro = DateTime.Now;
                if (registro.FechaFin.HasValue) {
                    if (registro.FechaFin.Value < registro.FechaIni.Value) {
                        return Json(new { success = "False", responseText = "La fecha de inicio no puede ser mayor que la fecha de fin" });
                    }
                }

                registro.IdUsuarioRegistro = Convert.ToInt32(VariablesWeb.ConsultaInformacion.iCodUsuario);

                object respuesta = _empleado_Servicio.RegistrarEncargaturasEmpleado(registro);

                if (respuesta.ToString() == "1")
                {
                    return Json(new { success = "True", responseText = respuesta });
                }
                else
                {
                    return Json(new { success = "False", responseText = respuesta });
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = "False", responseText = ex.Message });
            }
        }
        [HttpPost]
        [Authorize]
        public JsonResult ActualizarEncargaturasEmpleado(EmpleadoEncargatura_Registro registro)
        {
            try
            {
                DateTime aux;
                if (registro.FechaIni.HasValue)
                {
                    if (!DateTime.TryParse(registro.FechaIni.Value.ToString("dd/MM/yyyy"), out aux))
                    {
                        return Json(new { success = "False", responseText = "La fecha de inicio no es v�lida" });
                    }
                }
                else {
                    return Json(new { success = "False", responseText = "La fecha de inicio no es v�lida" });
                }
                if (registro.FechaFin.HasValue)
                {
                    if (!DateTime.TryParse(registro.FechaFin.Value.ToString("dd/MM/yyyy"), out aux))
                    {
                        return Json(new { success = "False", responseText = "La fecha de fin no es v�lida" });
                    }
                    if (registro.FechaFin.Value < registro.FechaIni.Value)
                    {
                        return Json(new { success = "False", responseText = "La fecha de inicio no puede ser mayor que la fecha de fin" });
                    }
                }
                //else {
                //    return Json(new { success = "False", responseText = "La fecha de fin no es v�lida" });
                //}

                registro.IdUsuarioModificacion = Convert.ToInt32(VariablesWeb.ConsultaInformacion.iCodUsuario);

                object respuesta = _empleado_Servicio.ActualizarEncargaturasEmpleado(registro);

                if (respuesta.ToString() == "1")
                {
                    return Json(new { success = "True", responseText = respuesta });
                }
                else
                {
                    return Json(new { success = "False", responseText = respuesta });
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = "False", responseText = ex.Message });
            }
        }
        [HttpPost]
        public JsonResult EliminarEncargaturasEmpleado(EmpleadoCuenta_Registro registro)
        {
            try
            {
                registro.FechaModificacion = DateTime.Now;
                registro.IdUsuarioModificacion = Convert.ToInt32(VariablesWeb.ConsultaInformacion.iCodUsuario);

                object respuesta = _empleado_Servicio.EliminarCuentaEmpleado(registro);

                return Json(new { success = "True", responseText = respuesta });
            }
            catch (Exception ex)
            {
                return Json(new { success = "False", responseText = ex.Message });
            }
        }

        [HttpPost]
        public JsonResult EliminarHistorialEmpleado(EmpleadoCuenta_Registro registro)
        {
            try
            {
                registro.FechaModificacion = DateTime.Now;
                registro.IdUsuarioModificacion = Convert.ToInt32(VariablesWeb.ConsultaInformacion.iCodUsuario);

                object respuesta = _empleado_Servicio.EliminarHistorialEmpleado(registro);

                return Json(new { success = "True", responseText = respuesta });
            }
            catch (Exception ex)
            {
                return Json(new { success = "False", responseText = ex.Message });
            }
        }

        [HttpPost]
        public JsonResult RegistrarFamiliarEmpleado(EmpleadoFamiliar_Registro registro)
        {
            try
            {
                registro.FechaRegistro = DateTime.Now;
                registro.IdUsuarioRegistro = (int)VariablesWeb.ConsultaInformacion.iCodUsuario;

                object respuesta = _empleado_Servicio.RegistrarFamiliarEmpleado(registro);

                return Json(new { success = "True", responseText = respuesta });
            }
            catch (Exception ex)
            {
                return Json(new { success = "False", responseText = ex.Message });
            }
        }
        [HttpPost]
        public JsonResult ActualizarFamiliarEmpleado(EmpleadoFamiliar_Registro registro)
        {
            try
            {
                registro.FechaModificacion = DateTime.Now;
                registro.IdUsuarioModificacion = (int)VariablesWeb.ConsultaInformacion.iCodUsuario;

                object respuesta = _empleado_Servicio.ActualizarFamiliarEmpleado(registro);

                return Json(new { success = "True", responseText = respuesta });
            }
            catch (Exception ex)
            {
                return Json(new { success = "False", responseText = ex.Message });
            }
        }
        [HttpPost]
        public JsonResult EliminarFamiliarEmpleado(EmpleadoFamiliar_Registro registro)
        {
            try
            {
                registro.FechaModificacion = DateTime.Now;
                registro.IdUsuarioModificacion = (int)VariablesWeb.ConsultaInformacion.iCodUsuario;

                object respuesta = _empleado_Servicio.EliminarFamiliarEmpleado(registro);

                return Json(new { success = "True", responseText = respuesta });
            }
            catch (Exception ex)
            {
                return Json(new { success = "False", responseText = ex.Message });
            }
        }
        [HttpGet]
        [Authorize]
        public JsonResult ListarEstudiosEmpleado(Empleado_Request peticion)
        {
            object respuesta = _empleado_Servicio.ListarEmpleadoEstudios(peticion);
            return Json(new { success = "True", data = respuesta }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        [Authorize]
        public JsonResult RegistrarEstudioEmpleado(EmpleadoEstudio_Registro registro)
        {
            try
            {
                registro.IdUsuarioRegistro = (int)VariablesWeb.ConsultaInformacion.iCodUsuario;
                object respuesta = _empleado_Servicio.RegistrarEstudioEmpleado(registro);
                return Json(new { success = "True", data = respuesta }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = "False", data = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        [Authorize]
        public JsonResult ActualizarEstudioEmpleado(EmpleadoEstudio_Registro registro)
        {
            try
            {
                registro.IdUsuarioModificacion = (int)VariablesWeb.ConsultaInformacion.iCodUsuario;
                object respuesta = _empleado_Servicio.ActualizarEstudioEmpleado(registro);
                return Json(new { success = "True", data = respuesta }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = "False", data = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        [Authorize]
        public JsonResult EliminarEstudioEmpleado(EmpleadoEstudio_Registro registro)
        {
            try
            {
                registro.IdUsuarioModificacion = (int)VariablesWeb.ConsultaInformacion.iCodUsuario;
                object respuesta = _empleado_Servicio.EliminarEstudioEmpleado(registro);
                return Json(new { success = "True", data = respuesta }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = "False", data = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}