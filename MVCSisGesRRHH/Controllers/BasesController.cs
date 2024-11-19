using MIDIS.ORI.Entidades;
using MIDIS.SEG.LogicaNegocio;
using MIDIS.Utiles;
using System;
using System.Net;
using System.Net.Mail;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCSisGesRRHH.Models;
using System.IO;
using SelectPdf;
using Xceed.Document.NET;
using Xceed.Words.NET;

namespace MVCSisGesRRHH.Controllers
{
    public class BasesController : Controller
    {
        // GET: Bases
        private readonly T_genm_perfil_puesto_LN _perfil_Puesto_Servicio = new T_genm_perfil_puesto_LN();
        private readonly T_genm_bases_perfil_puesto_LN _bases_Perfil_Puesto_Servicio = new T_genm_bases_perfil_puesto_LN();
        
        [HttpGet]
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        [Authorize]
        public ActionResult IndexJefeRRHH()
        {
            return View();
        }
        [HttpGet]
        [Authorize]
        public ActionResult NuevoCAS()
        {
            if (TempData["id"] != null)
            {
                ViewBag.idBasePerfil = TempData["id"];
            }
            return View();
        }
        [HttpGet]
        [Authorize]
        public ActionResult NuevoSERVIR()
        {
            if (TempData["id"] != null)
            {
                ViewBag.idBasePerfil = TempData["id"];
            }
            return View();
        }
        [HttpGet]
        [Authorize]
        public ActionResult Ver()
        {
            if (TempData["id"] != null)
            {
                ViewBag.idBasePerfil = TempData["id"];
            }
            return View();
        }
        [HttpGet]
        [Authorize]
        public ActionResult VerJefeRRHH()
        {
            if (TempData["id"] != null)
            {
                ViewBag.idBasePerfil = TempData["id"];
            }
            return View();
        }
        [HttpGet]
        public ActionResult ActualizarBasesPerfilPuesto(string id)
        {
            //Verbo_Registro verbos = new Verbo_Registro();
            //object respuesta = _perfil_Puesto_Servicio.ObtenerPerfilesPuesto(id);
            //return Json(respuesta, JsonRequestBehavior.AllowGet);
            //ViewBag.ProgramaSocial
            TempData["id"] = id;
            return RedirectToAction("nuevo", "Bases");

        }
        [HttpGet]
        public ActionResult VerBasesPerfilPuesto(string id)
        {
            //Verbo_Registro verbos = new Verbo_Registro();
            //object respuesta = _perfil_Puesto_Servicio.ObtenerPerfilesPuesto(id);
            //return Json(respuesta, JsonRequestBehavior.AllowGet);
            //ViewBag.ProgramaSocial
            TempData["id"] = id;
            return RedirectToAction("ver", "Bases");
        }
        [HttpGet]
        public ActionResult VerBasesPerfilPuestoJefeRRHH(string id)
        {
            //Verbo_Registro verbos = new Verbo_Registro();
            //object respuesta = _perfil_Puesto_Servicio.ObtenerPerfilesPuesto(id);
            //return Json(respuesta, JsonRequestBehavior.AllowGet);
            //ViewBag.ProgramaSocial
            TempData["id"] = id;
            return RedirectToAction("verJefeRRHH", "Bases");
        }
        [HttpPost]
        public JsonResult InsertarBasesPerfilPuesto(BasesPerfilPuestoRegistro registro)
        {
            try
            {
                registro.iCodTrabajadorReg = (int)VariablesWeb.oT_Genm_Usuario.iCodTrabajador;
                object respuesta = _bases_Perfil_Puesto_Servicio.InsertarBasesPerfilPuesto(registro);

                return Json(new { success = "True", responseText = respuesta });
            }
            catch (Exception ex)
            {
                return Json(new { success = "False", responseText = ex.Message });
            }
        }
        [HttpPost]
        public JsonResult ActualizarBasesPerfilPuesto(BasesPerfilPuestoRegistro registro)
        {
            try
            {
                registro.iCodTrabajadorMod = (int)VariablesWeb.oT_Genm_Usuario.iCodTrabajador;
                object respuesta = _bases_Perfil_Puesto_Servicio.ActualizarBasesPerfilPuesto(registro);

                return Json(new { success = "True", responseText = respuesta });
            }
            catch (Exception ex)
            {
                return Json(new { success = "False", responseText = ex.Message });
            }
        }
        [HttpPost]
        public JsonResult AprobarBasesPerfilPuesto(BasesPerfilPuestoRegistro registro)
        {
            try
            {
                object respuesta = _bases_Perfil_Puesto_Servicio.AprobarBasesPerfilPuesto(registro);
                
                return Json(new { success = "True", responseText = respuesta });
            }
            catch (Exception ex)
            {
                return Json(new { success = "False", responseText = ex.Message });
            }
        }
        [HttpPost]
        [Authorize]
        public JsonResult RegistrarBasesArchivo(BasesPerfilPuestoRegistro registro)
        {
            try
            {
                registro.FechaModificacion = DateTime.Now;
                registro.IdUsuarioModificacion = (int)VariablesWeb.ConsultaInformacion.iCodUsuario;

                String nameFile = String.Empty;
                for (Int32 j = 0; j < registro.formatos.ToList().Count; j++) {
                    HttpPostedFileBase postfile = ((HttpPostedFileBase[])(registro.formatos.ToList())[j])[0];
                    if (postfile.ContentLength > 0)  {
                        nameFile = postfile.FileName;

                        Stream str = postfile.InputStream;
                        BinaryReader Br = new BinaryReader(str);
                        Byte[] FileDet = Br.ReadBytes((Int32)str.Length);

                        registro.archivo = FileDet;
                    }
                }

                object respuesta = _bases_Perfil_Puesto_Servicio.RegistrarBasesArchivo(registro);
                //EmpleadoContrato_Registro objContrato = _contrato_Servicio.ObtenerParaEditar(new Contrato_Request() { IdContrato = registro.IdContrato, Nombre = "", Estado = -1 });
                //PostulanteInformacion_Registro obj = _postulante_Servicio.ObtenerPostulanteFicha(new PostulanteInformacion_Registro() { IdPostulante = objContrato.IdPostulante, IdPostulacion = objContrato.IdPostulacion, IdConvocatoria = objContrato.IdConvocatoria, Nombre = objContrato.Nombre });
                //this.SendEmail(obj, "3");

                return Json(new { success = "True", responseText = respuesta });
            }
            catch (Exception ex)
            {
                return Json(new { success = "False", responseText = ex.Message });
            }
        }
        public FileResult Ficha()
        {
            Int32 IdBases = (Request.QueryString.Get("IdBases") == null ? 0 : Int32.Parse(Request.QueryString["IdBases"]));
            BasesPerfilPuestoRegistro respuesta = _bases_Perfil_Puesto_Servicio.ObtenerBasesPerfilesPuestoPorID(IdBases.ToString()).FirstOrDefault();

            BasesPerfilPuesto_Request pBasesPerfilPuesto_Request = new BasesPerfilPuesto_Request() { iCodBasePerfil = IdBases };
            IEnumerable<BasesPerfilPuestoRegistro> BasesCab = _bases_Perfil_Puesto_Servicio.ObtenerBasesPerfilesPuestoPorID(IdBases.ToString());

            PerfilFunciones_Request pPerfilFunciones_Request = new PerfilFunciones_Request() { iCodPerfil = Convert.ToInt32(respuesta.iCodPerfil) };
            RequisitosAdicionales_Registro pRequisitosAdicionales_Registro = new RequisitosAdicionales_Registro() { iCodPerfil = Convert.ToInt32(respuesta.iCodPerfil) };
            Habilidad_Competencias_Registro pHabilidad_Competencias_Registro = new Habilidad_Competencias_Registro() { iCodPerfil = Convert.ToInt32(respuesta.iCodPerfil) };
            PerfilCoordinaciones_Registro pPerfilCoordinaciones_Registro = new PerfilCoordinaciones_Registro() { iCodPerfil = Convert.ToInt32(respuesta.iCodPerfil) };
            PerfilConocimientos_Registro pPerfilConocimientos_Registro = new PerfilConocimientos_Registro() { iCodPerfil = Convert.ToInt32(respuesta.iCodPerfil) };
            PerfilFormacionAcademica_Registro pPerfilFormacionAcademica_Registro = new PerfilFormacionAcademica_Registro() { iCodPerfil = Convert.ToInt32(respuesta.iCodPerfil) };

            IEnumerable<PerfilPuestoRegistro> PerfilCab = _perfil_Puesto_Servicio.ObtenerPerfilesPuestoPorID(respuesta.iCodPerfil.ToString());
            IEnumerable<PerfilFunciones_Request> Funciones = _perfil_Puesto_Servicio.ListarPerfilDetFunciones(pPerfilFunciones_Request);
            IEnumerable<RequisitosAdicionales_Registro> RequisitosAdicionales = _perfil_Puesto_Servicio.ListarPerfilDetRequisitosAdicionales(pRequisitosAdicionales_Registro);
            IEnumerable<Habilidad_Competencias_Registro> Habilidades = _perfil_Puesto_Servicio.ListarPerfilDetHabilidades(pHabilidad_Competencias_Registro);
            IEnumerable<Habilidad_Competencias_Registro> Competencias = _perfil_Puesto_Servicio.ListarPerfilDetCompetencias(pHabilidad_Competencias_Registro);
            IEnumerable<PerfilCoordinaciones_Registro> CoordinacionInterna = _perfil_Puesto_Servicio.ListarPerfilDetCoordinacionInterna(pPerfilCoordinaciones_Registro);
            IEnumerable<PerfilCoordinaciones_Registro> CoordinacionExterna = _perfil_Puesto_Servicio.ListarPerfilDetCoordinacionExterna(pPerfilCoordinaciones_Registro);
            IEnumerable<PerfilConocimientos_Registro> ConocimientosTecnicos = _perfil_Puesto_Servicio.ListarPerfilDetConocimientosTecnicos(pPerfilConocimientos_Registro);
            IEnumerable<PerfilConocimientos_Registro> ConocimientosCursosProgramas = _perfil_Puesto_Servicio.ListarPerfilDetConocimientosCursosProgramas(pPerfilConocimientos_Registro);
            IEnumerable<PerfilConocimientos_Registro> ConocimientosOfficeIdiomas = _perfil_Puesto_Servicio.ListarPerfilDetConocimientosOfficeIdiomas(pPerfilConocimientos_Registro);
            IEnumerable<PerfilFormacionAcademica_Registro> FormAcaNivelBasico = _perfil_Puesto_Servicio.ListarPerfilDetFormAcaNivelBasico(pPerfilFormacionAcademica_Registro);
            IEnumerable<PerfilFormacionAcademica_Registro> FormAcaNivelEducativo = _perfil_Puesto_Servicio.ListarPerfilDetFormAcaNivelEducativo(pPerfilFormacionAcademica_Registro);
            IEnumerable<PerfilFormacionAcademica_Registro> FormAcaMaestria = _perfil_Puesto_Servicio.ListarPerfilDetFormAcaMaestria(pPerfilFormacionAcademica_Registro);
            IEnumerable<PerfilFormacionAcademica_Registro> FormAcaDoctorado = _perfil_Puesto_Servicio.ListarPerfilDetFormAcaDoctorado(pPerfilFormacionAcademica_Registro);


            var fileName = "Bases_" + DateTime.Now.ToString("yyyyMMddHHmm") + ".docx";
            string fullPathOri = Path.Combine(Server.MapPath("~/Templates/Bases/formato"), "ANEXO_05_FORMATO_BASES_CAS.docx");
            if (respuesta.IdExamenConocimiento == 1)
                fullPathOri = Path.Combine(Server.MapPath("~/Templates/Bases/formato"), "ANEXO_05_FORMATO_BASES_CAS_EVAL.docx");

            string fullPathNew = Path.Combine(Server.MapPath("~/Templates/Bases"), fileName);
            using (var doc = DocX.Load(fullPathOri))
            {
                doc.ReplaceText("<PROCESO_CAS>", BasesCab.FirstOrDefault().strNroCAS);
                doc.ReplaceText("<CARGO>", BasesCab.FirstOrDefault().strNombrePuesto.Trim());
                doc.ReplaceText("<CANTIDAD>", (BasesCab.FirstOrDefault().iCantPersonalRequerido == 1 ? "un/a (01) profesional para que se desempeñe" : (BasesCab.FirstOrDefault().iCantPersonalRequerido == 2 ? "dos (02) profesionales para que se desempeñen" :  (BasesCab.FirstOrDefault().iCantPersonalRequerido == 3 ? "tres (03) profesionales para que se desempeñen" : (BasesCab.FirstOrDefault().iCantPersonalRequerido == 4 ? "cuatro (04) profesionales para que se desempeñen" : (BasesCab.FirstOrDefault().iCantPersonalRequerido == 5 ? "cinco (05) profesionales para que se desempeñen" :  BasesCab.FirstOrDefault().iCantPersonalRequerido.ToString() + " profesionales para que se desempeñen"))))));
                doc.ReplaceText("<DEPENDENCIA>", BasesCab.FirstOrDefault().strUnidadOrganica);

                //doc.Replace("//dFechaAprobConv", BasesCab.FirstOrDefault().dFechaAprobConv.ToString("dd/MM/yyyy"));
                doc.ReplaceText("<FECHA_PUB_CAS>", BasesCab.FirstOrDefault().dFechaDesdePubMIDIS.ToString("dd/MM/yyyy") + " al " + BasesCab.FirstOrDefault().dFechaHastaPubMIDIS.ToString("dd/MM/yyyy"));
                //doc.ReplaceText("//dFechaDesdePubMIDIS", BasesCab.FirstOrDefault().dFechaDesdePubMIDIS.ToString("dd/MM/yyyy"));
                //doc.ReplaceText("//dFechaHastaPubMIDIS", BasesCab.FirstOrDefault().dFechaHastaPubMIDIS.ToString("dd/MM/yyyy"));
                doc.ReplaceText("<FECHA_POS_CAS>", BasesCab.FirstOrDefault().dFechaRegCVPostulante.ToString("dd/MM/yyyy"));
                doc.ReplaceText("<FECHA_EVAL_CUR>", BasesCab.FirstOrDefault().dFechaDesdeEvaCV.ToString("dd/MM/yyyy") + " al " + BasesCab.FirstOrDefault().dFechaHastaEvaCV.ToString("dd/MM/yyyy"));
                //doc.ReplaceText("//dFechaHastaEvaCV", BasesCab.FirstOrDefault().dFechaHastaEvaCV.ToString("dd/MM/yyyy"));
                doc.ReplaceText("<FECHA_PUB_CUR>", BasesCab.FirstOrDefault().dFechaPubResultadoMIDIS.ToString("dd/MM/yyyy"));
                doc.ReplaceText("<FECHA_ENT_CAS>", BasesCab.FirstOrDefault().dFechaDesdeEntrevista.ToString("dd/MM/yyyy") + " al " + BasesCab.FirstOrDefault().dFechaHastaEntrevista.ToString("dd/MM/yyyy"));
                //doc.ReplaceText("//dFechaHastaEntrevista", BasesCab.FirstOrDefault().dFechaHastaEntrevista.ToString("dd/MM/yyyy"));
                doc.ReplaceText("<FECHA_PUB_FIN>", BasesCab.FirstOrDefault().dFechaPubResultadoFinalMIDIS.ToString("dd/MM/yyyy"));
                doc.ReplaceText("<FECHA_CONT>", BasesCab.FirstOrDefault().dFechaDesdeSuscripcionContrato.ToString("dd/MM/yyyy") + " al " + BasesCab.FirstOrDefault().dFechaHastaSuscripcionContrato.ToString("dd/MM/yyyy"));
                //doc.ReplaceText("//dFechaHastaSuscripcionContrato", BasesCab.FirstOrDefault().dFechaHastaSuscripcionContrato.ToString("dd/MM/yyyy"));

                if (respuesta.IdExamenConocimiento == 1) {
                    doc.ReplaceText("<FECHA_EVAL_CON>", BasesCab.FirstOrDefault().dFechaConocimiento.ToString("dd/MM/yyyy"));
                    doc.ReplaceText("<FECHA_PUB_CON>", BasesCab.FirstOrDefault().dFechaPubConocimiento.ToString("dd/MM/yyyy"));
                }

                if (BasesCab.FirstOrDefault().bDuracionContrato31Diciembre)
                    doc.ReplaceText("<DURACION>", "Hasta el 31 de Diciembre");
                else
                    doc.ReplaceText("<DURACION>", fn_anio(Convert.ToInt32(BasesCab.FirstOrDefault().strDuracionContrato)) + " meses"); //"Tres (03) meses"

                doc.ReplaceText("<REMUNERACION>", "S/ " + BasesCab.FirstOrDefault().decRemuneracion + " (" + NumeroALetras(BasesCab.FirstOrDefault().decRemuneracion) + " soles)");

                Int32 cont = 0;
                String strFunciones = string.Empty;
                foreach (PerfilFunciones_Request item in Funciones) { 
                    if (!String.IsNullOrEmpty(strFunciones)) strFunciones = strFunciones + Environment.NewLine;

                    cont += 1;
                    strFunciones = strFunciones + cont.ToString() + ". " +  item.Verbo.strDescripcion + " " + item.Objetivo + "" + item.Funcion;
                }

                doc.ReplaceText("<FUNCIONES>", strFunciones);
                
                string strHabilidades_Competencias = string.Empty;
                if (Habilidades.Count() > 0)
                {
                    foreach (var item in Habilidades)
                    {
                        item.Cualidad.strNombre = item.Cualidad.strNombre.Replace("\r\n", String.Empty);
                        if (!String.IsNullOrEmpty(strHabilidades_Competencias)) strHabilidades_Competencias = strHabilidades_Competencias + ", ";
                        strHabilidades_Competencias = strHabilidades_Competencias + item.Cualidad.strNombre;
                    }
                }
                if (Competencias.Count() > 0)
                {
                    foreach (var item in Competencias)
                    {
                        item.Cualidad.strNombre = item.Cualidad.strNombre.Replace("\r\n", String.Empty);
                        if (!String.IsNullOrEmpty(strHabilidades_Competencias)) strHabilidades_Competencias = strHabilidades_Competencias + ", ";
                        strHabilidades_Competencias = strHabilidades_Competencias + item.Cualidad.strNombre;
                    }
                }
                doc.ReplaceText("<HABILIDADES>", strHabilidades_Competencias);

                string strFormacion1 = string.Empty;
                if (FormAcaNivelEducativo.Count() > 0) {
                    foreach (var item in FormAcaNivelEducativo) {
                        if (!String.IsNullOrEmpty(strFormacion1)) strFormacion1 = strFormacion1 + " y/o ";
                        
                        strFormacion1 = strFormacion1 + item.strNivel4;
                    }
                    if (FormAcaNivelEducativo.Where(x => x.bColegiatura == true).Count() > 0)
                        strFormacion1 = strFormacion1 + " colegiado";
                    if (FormAcaNivelEducativo.Where(x => x.bHabilitado == true).Count() > 0)
                        strFormacion1 = strFormacion1 + " y habilitado";
                }

                string strFormacion2 = string.Empty;
                if (FormAcaMaestria.Count() > 0) {
                    foreach (var item in FormAcaMaestria) {
                        if (!String.IsNullOrEmpty(strFormacion2)) strFormacion2 = strFormacion2 + " o ";
                        
                        strFormacion2 = strFormacion2 + item.strNivel4;
                    }
                    strFormacion2 = "Maestría en " + strFormacion2;
                }

                string strFormacion3 = string.Empty;
                if (FormAcaDoctorado.Count() > 0) {
                    foreach (var item in FormAcaDoctorado) {
                        if (!String.IsNullOrEmpty(strFormacion3)) strFormacion3 = strFormacion3 + " o ";

                        strFormacion3 = strFormacion3 + item.strNivel4;
                    }
                    strFormacion3 = "Doctorado en " + strFormacion3;
                }


                doc.ReplaceText("<FORMACION>",  (strFormacion1 == "" ? "" : strFormacion1 + Environment.NewLine) + 
                                                (strFormacion2 == "" ? "" : strFormacion2 + Environment.NewLine) + 
                                                (strFormacion3 == "" ? "" : strFormacion3 + Environment.NewLine));


                String strExperiencia_General = string.Empty;
                String strExperiencia_Especifico = string.Empty;
                String strExperiencia_publico = string.Empty;
                String strNivelMinimo = string.Empty;
                strExperiencia_General = fn_anio(PerfilCab.FirstOrDefault().iAnioExpGeneral);
                doc.ReplaceText("<EXP_GENERAL>", strExperiencia_General);

                if (PerfilCab.FirstOrDefault().iAnioExpEspecifica > 0) {
                    strExperiencia_Especifico = fn_anio(PerfilCab.FirstOrDefault().iAnioExpEspecifica) + " " + (String.IsNullOrEmpty(PerfilCab.FirstOrDefault().strDesExpEspecifica) ? String.Empty : PerfilCab.FirstOrDefault().strDesExpEspecifica);
                    //doc.ReplaceText("<EXP_ESPECIFICA>", strExperiencia_Especifico);
                }
                if (PerfilCab.FirstOrDefault().iAnioExpSectorPublico > 0) {
                    strExperiencia_publico = fn_anio(PerfilCab.FirstOrDefault().iAnioExpSectorPublico);
                    //doc.ReplaceText("//exp_sec_pub", ", de los cuales " + strExperiencia_publico + "en el sector público.");
                }
                if (PerfilCab.FirstOrDefault().iCodNivelMinimo > 0)
                {
                    String nivel = String.Empty;
                    switch (PerfilCab.FirstOrDefault().iCodNivelMinimo) {
                        case 1: nivel = "Practicante Profesional";
                            break;
                        case 2:
                            nivel = "Auxiliar o Asistente";
                            break;
                        case 3:
                            nivel = "Analista";
                            break;
                        case 4:
                            nivel = "Especialista";
                            break;
                        case 5:
                            nivel = "Supervisor / Coordinador";
                            break;
                        case 6:
                            nivel = "Jefe de Area o Departamento";
                            break;
                        case 7:
                            nivel = "Gerente o Director";
                            break;
                    }
                    strNivelMinimo = "El nivel mínimo del puesto requerido en la experiencia específica es " + nivel;
                    //doc.ReplaceText("//exp_sec_pub", ", de los cuales " + strExperiencia_publico + "en el sector público.");
                }
                doc.ReplaceText("<EXP_ESPECIFICA>", strExperiencia_Especifico + (strExperiencia_publico == "" ? "" : ", de los cuales " + strExperiencia_publico + " en el sector público" + (strNivelMinimo == "" ? "" : Environment.NewLine + Environment.NewLine + strNivelMinimo)));
                
                string strCursos_Programas = string.Empty;
                if (ConocimientosCursosProgramas.Count() > 0) {
                    foreach (var item in ConocimientosCursosProgramas)
                    {
                        if(!String.IsNullOrEmpty(strCursos_Programas)) strCursos_Programas = strCursos_Programas + Environment.NewLine; 
                        strCursos_Programas = strCursos_Programas + "• " + item.PerfilTipoMateria.strDescripcion + (item.PerfilTipoMateria.iCodTipoMateria == 1 ? " en " : (item.PerfilTipoMateria.iCodTipoMateria == 2 ? " en " : (item.PerfilTipoMateria.iCodTipoMateria == 3 ? " de " : (item.PerfilTipoMateria.iCodTipoMateria == 4 ? " en " : (item.PerfilTipoMateria.iCodTipoMateria == 5 ? " en " : (item.PerfilTipoMateria.iCodTipoMateria == 6 ? " de " : String.Empty)))))) + item.Conocimientos;
                    }
                }

                string strCursos_ofimatica = string.Empty;
                if (ConocimientosOfficeIdiomas.Count() > 0) {
                    foreach (var item in ConocimientosOfficeIdiomas)
                    {
                        if(!String.IsNullOrEmpty(strCursos_ofimatica)) strCursos_ofimatica = strCursos_ofimatica + Environment.NewLine;
                        strCursos_ofimatica = strCursos_ofimatica + "• " + item.PerfilTipoMateriaOtros.strDescripcion + ": " + item.PerfilTipoSubMateriaOtros.strDescripcion + " nivel " + item.PerfilNivelMateria.strDescripcion;
                    }
                }
                doc.ReplaceText("<CURSOS>", strCursos_Programas);
                doc.ReplaceText("<CONOC_OFIMATICA>", strCursos_ofimatica);
                
                string strConocimientosTec = string.Empty;
                if (ConocimientosTecnicos.Count() > 0) {
                    foreach (var item in ConocimientosTecnicos)
                    {
                        if(!String.IsNullOrEmpty(strConocimientosTec)) strConocimientosTec = strConocimientosTec + Environment.NewLine;
                        strConocimientosTec = strConocimientosTec + "• " + item.Conocimientos;
                    }
                }
                doc.ReplaceText("<CONOC_TECNICOS>", strConocimientosTec);
                
                //doc.ReplaceText("<CONOC_TECNICOS>", objContrato.FechaInicio.Value.ToLongDateString().Substring(objContrato.FechaInicio.Value.ToLongDateString().IndexOf(',') + 2));
                //doc.ReplaceText("<CONOC_OFIMATICA>", (objContrato.IdTipoLimite == 1 ? objContrato.FechaCese.Value.ToLongDateString().Substring(objContrato.FechaCese.Value.ToLongDateString().IndexOf(',') + 2) : "AL FINALIZAR LA DESIGNACIÓN"));
                //doc.ReplaceText("<REMUNERACION>", objContrato.Remuneracion.ToString("C"));
                //doc.ReplaceText("<FECHA>", DateTime.Now.ToLongDateString().Substring(DateTime.Now.ToLongDateString().IndexOf(',') + 2));


                doc.SaveAs(fullPathNew);
            }

            FileContentResult result = new FileContentResult(System.IO.File.ReadAllBytes(fullPathNew), "application/msword") { FileDownloadName = fileName };

            return result;
        }
        [HttpPost]
        public JsonResult ObtenerBasesPerfilesPuestoConvocatoria(Int32 idDependencia, Int32 iTipo = 1)
        {
            //Verbo_Registro verbos = new Verbo_Registro();

            object respuesta = _bases_Perfil_Puesto_Servicio.ObtenerBasesPerfilesPuestoConvocatoria(idDependencia, iTipo);
            return Json(respuesta, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult ObtenerBasesPerfilesPuesto(String strOrgano, String strUO, String strEstado, string fechaIni, string fechaFin)
        {
            //Verbo_Registro verbos = new Verbo_Registro();

            object respuesta = _bases_Perfil_Puesto_Servicio.ObtenerBasesPerfilesPuesto(strOrgano, strUO, strEstado, fechaIni, fechaFin);
            return Json(respuesta, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult ObtenerBasesPerfilesPuestoPorID(string id)
        {
            //Verbo_Registro verbos = new Verbo_Registro();

            object respuesta = _bases_Perfil_Puesto_Servicio.ObtenerBasesPerfilesPuestoPorID(id);
            var jsonResult = Json(respuesta, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = Int32.MaxValue;

            return jsonResult;
        }
        [HttpPost]
        public JsonResult ObtenerBasesPerfilesConvocatoriaPorID(string id)
        {
            BasesPerfilPuestoRegistro peticion = new BasesPerfilPuestoRegistro();
            peticion.iCodBasePerfil = Int32.Parse(id);
            
            //ESTA ERA LA ANTIGUA LLAMADA CUANDO NO SE GUARDABA LAS BASES EN LA BD
            BasesPerfilPuestoRegistro respuesta = _bases_Perfil_Puesto_Servicio.ObtenerBasesPerfilesPuestoPorID(id).FirstOrDefault();
            //var lista = _bases_Perfil_Puesto_Servicio.ObtenerBasesPerfilesPuestoPorID(id).Select(p => new { p.iCodBasePerfil, p.strNombreArchivo, p.archivo });
            //var item = lista.Where(x => x.iCodBasePerfil == peticion.iCodBasePerfil).SingleOrDefault();


            //BasesPerfilPuesto_Request pBasesPerfilPuesto_Request = new BasesPerfilPuesto_Request() { iCodBasePerfil = Convert.ToInt32(id) };
            //IEnumerable<BasesPerfilPuestoRegistro> BasesCab = _bases_Perfil_Puesto_Servicio.ObtenerBasesPerfilesPuestoPorID(id);

            //PerfilFunciones_Request pPerfilFunciones_Request = new PerfilFunciones_Request() { iCodPerfil = Convert.ToInt32(respuesta.iCodPerfil) };
            //RequisitosAdicionales_Registro pRequisitosAdicionales_Registro = new RequisitosAdicionales_Registro() { iCodPerfil = Convert.ToInt32(respuesta.iCodPerfil) };
            //Habilidad_Competencias_Registro pHabilidad_Competencias_Registro = new Habilidad_Competencias_Registro() { iCodPerfil = Convert.ToInt32(respuesta.iCodPerfil) };
            //PerfilCoordinaciones_Registro pPerfilCoordinaciones_Registro = new PerfilCoordinaciones_Registro() { iCodPerfil = Convert.ToInt32(respuesta.iCodPerfil) };
            //PerfilConocimientos_Registro pPerfilConocimientos_Registro = new PerfilConocimientos_Registro() { iCodPerfil = Convert.ToInt32(respuesta.iCodPerfil) };
            //PerfilFormacionAcademica_Registro pPerfilFormacionAcademica_Registro = new PerfilFormacionAcademica_Registro() { iCodPerfil = Convert.ToInt32(respuesta.iCodPerfil) };

            //IEnumerable<PerfilPuestoRegistro> PerfilCab = _perfil_Puesto_Servicio.ObtenerPerfilesPuestoPorID(respuesta.iCodPerfil.ToString());
            //IEnumerable<PerfilFunciones_Request> Funciones = _perfil_Puesto_Servicio.ListarPerfilDetFunciones(pPerfilFunciones_Request);
            //IEnumerable<RequisitosAdicionales_Registro> RequisitosAdicionales = _perfil_Puesto_Servicio.ListarPerfilDetRequisitosAdicionales(pRequisitosAdicionales_Registro);
            //IEnumerable<Habilidad_Competencias_Registro> Habilidades = _perfil_Puesto_Servicio.ListarPerfilDetHabilidades(pHabilidad_Competencias_Registro);
            //IEnumerable<Habilidad_Competencias_Registro> Competencias = _perfil_Puesto_Servicio.ListarPerfilDetCompetencias(pHabilidad_Competencias_Registro);
            //IEnumerable<PerfilCoordinaciones_Registro> CoordinacionInterna = _perfil_Puesto_Servicio.ListarPerfilDetCoordinacionInterna(pPerfilCoordinaciones_Registro);
            //IEnumerable<PerfilCoordinaciones_Registro> CoordinacionExterna = _perfil_Puesto_Servicio.ListarPerfilDetCoordinacionExterna(pPerfilCoordinaciones_Registro);
            //IEnumerable<PerfilConocimientos_Registro> ConocimientosTecnicos = _perfil_Puesto_Servicio.ListarPerfilDetConocimientosTecnicos(pPerfilConocimientos_Registro);
            //IEnumerable<PerfilConocimientos_Registro> ConocimientosCursosProgramas = _perfil_Puesto_Servicio.ListarPerfilDetConocimientosCursosProgramas(pPerfilConocimientos_Registro);
            //IEnumerable<PerfilConocimientos_Registro> ConocimientosOfficeIdiomas = _perfil_Puesto_Servicio.ListarPerfilDetConocimientosOfficeIdiomas(pPerfilConocimientos_Registro);
            //IEnumerable<PerfilFormacionAcademica_Registro> FormAcaNivelBasico = _perfil_Puesto_Servicio.ListarPerfilDetFormAcaNivelBasico(pPerfilFormacionAcademica_Registro);
            //IEnumerable<PerfilFormacionAcademica_Registro> FormAcaNivelEducativo = _perfil_Puesto_Servicio.ListarPerfilDetFormAcaNivelEducativo(pPerfilFormacionAcademica_Registro);
            //IEnumerable<PerfilFormacionAcademica_Registro> FormAcaMaestria = _perfil_Puesto_Servicio.ListarPerfilDetFormAcaMaestria(pPerfilFormacionAcademica_Registro);
            //IEnumerable<PerfilFormacionAcademica_Registro> FormAcaDoctorado = _perfil_Puesto_Servicio.ListarPerfilDetFormAcaDoctorado(pPerfilFormacionAcademica_Registro);

            ////PostulanteInformacion_Registro peticion = new PostulanteInformacion_Registro() { IdPostulante = IdPostulante, IdPostulacion = IdPostulacion, IdConvocatoria = IdConvocatoria };
            //peticion = _postulante_Servicio.ObtenerPostulanteFicha(peticion);
            string pathUpload = Server.MapPath("~/temp/" + respuesta.strNroCAS);
            if (!Directory.Exists(pathUpload))
                Directory.CreateDirectory(pathUpload);

            String strArchivo = String.Empty;
            String strNombreArchivo = String.Empty;
            strArchivo = String.Format("BA{0}_{1}.pdf", respuesta.strNroCAS, DateTime.Now.ToString("yyyyMMddHHmm"));
            strNombreArchivo = System.IO.Path.Combine(pathUpload, strArchivo);
                    
            //string path = Server.MapPath("~/Documents/Bases");
            //string fileName = "CAS " + BasesCab.FirstOrDefault().strNroCAS + " - " + BasesCab.FirstOrDefault().strNombrePuesto.ToUpper() + ".pdf";
            //string fullPath = Path.Combine(path, fileName);

            using (FileStream fs = System.IO.File.Create(strNombreArchivo)){
                //Stream pdfStream = GenerarFichaBasesPerfilPuestoPdf(BasesCab, PerfilCab, Funciones, RequisitosAdicionales, Habilidades, Competencias, CoordinacionInterna, CoordinacionExterna, ConocimientosTecnicos, ConocimientosCursosProgramas, ConocimientosOfficeIdiomas, FormAcaNivelBasico, FormAcaNivelEducativo, FormAcaMaestria, FormAcaDoctorado);
                Stream pdfStream = new MemoryStream(respuesta.archivo);
                pdfStream.CopyTo(fs);
            }

            respuesta.strNombreArchivo = Request.Url.AbsoluteUri.Substring(0, Request.Url.AbsoluteUri.IndexOf("Bases")) + "temp/" + respuesta.strNroCAS + "/" + strArchivo;

            //return Json(respuesta, JsonRequestBehavior.AllowGet);
            return new JsonResult(){ 
                Data = respuesta, 
                JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                MaxJsonLength = Int32.MaxValue
            };
        }
        [HttpGet]
        public JsonResult ListarEstadoBases()
        {
            List<Estado_Response> lista = new List<Estado_Response>();
            lista.Add(new Estado_Response("P", "PENDIENTE"));
            lista.Add(new Estado_Response("A", "APROBADO"));
            lista.Add(new Estado_Response("F", "PUBLICADO"));
            
            object respuesta = lista;

            return Json(respuesta, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult ListarPerfilPuesto(string strPerfil, int tipo)
        {
            object respuesta = _bases_Perfil_Puesto_Servicio.ListarPerfilPuesto(strPerfil, tipo);
            return Json(respuesta, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult LiberarBases(BasesPerfilPuestoObservacionRegistro peticion)
        {
            try
            {
                //registro.FechaRegistro = DateTime.Now;
                //registro.iCodTrabajador = (int)VariablesWeb.oT_Genm_Usuario.iCodTrabajador;

                object respuesta = _bases_Perfil_Puesto_Servicio.LiberarBases(peticion);                
                return Json(new { success = "True", responseText = respuesta });
            }
            catch (Exception ex)
            {
                return Json(new { success = "False", responseText = ex.Message });
            }
        }

        [HttpPost]
        public JsonResult ListarBasesPerfilPuestoObservacion(string id)
        {
            object respuesta = _bases_Perfil_Puesto_Servicio.ListarBasesPerfilPuestoObservacion(id);
            return Json(respuesta, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult ListarTipoEvaluacion()
        {
            List<Estado_Response> lista = new List<Estado_Response>();
            lista.Add(new Estado_Response("0", "NO APLICA"));
            lista.Add(new Estado_Response("1", "SI"));

            object respuesta = lista;

            return Json(respuesta, JsonRequestBehavior.AllowGet);
        }

        public FileResult PlantillaBasesPerfilPuesto(string id, string id2)
        {
            //ObtenerPerfilesPuestoPorID


            BasesPerfilPuestoRegistro pBasesPerfilPuesto_Request = new BasesPerfilPuestoRegistro() { iCodBasePerfil = Convert.ToInt32(id) };
            IEnumerable<BasesPerfilPuestoRegistro> BasesCab = _bases_Perfil_Puesto_Servicio.ObtenerBasesPerfilesPuestoPorID(id);

            PerfilFunciones_Request pPerfilFunciones_Request = new PerfilFunciones_Request() { iCodPerfil = Convert.ToInt32(id2) };
            RequisitosAdicionales_Registro pRequisitosAdicionales_Registro = new RequisitosAdicionales_Registro() { iCodPerfil = Convert.ToInt32(id2) };
            Habilidad_Competencias_Registro pHabilidad_Competencias_Registro = new Habilidad_Competencias_Registro() { iCodPerfil = Convert.ToInt32(id2) };
            PerfilCoordinaciones_Registro pPerfilCoordinaciones_Registro = new PerfilCoordinaciones_Registro() { iCodPerfil = Convert.ToInt32(id2) };
            PerfilConocimientos_Registro pPerfilConocimientos_Registro = new PerfilConocimientos_Registro() { iCodPerfil = Convert.ToInt32(id2) };
            PerfilFormacionAcademica_Registro pPerfilFormacionAcademica_Registro = new PerfilFormacionAcademica_Registro() { iCodPerfil = Convert.ToInt32(id2) };


            IEnumerable<PerfilPuestoRegistro> PerfilCab = _perfil_Puesto_Servicio.ObtenerPerfilesPuestoPorID(id2);
            IEnumerable<PerfilFunciones_Request> Funciones = _perfil_Puesto_Servicio.ListarPerfilDetFunciones(pPerfilFunciones_Request);
            IEnumerable<RequisitosAdicionales_Registro> RequisitosAdicionales = _perfil_Puesto_Servicio.ListarPerfilDetRequisitosAdicionales(pRequisitosAdicionales_Registro);
            IEnumerable<Habilidad_Competencias_Registro> Habilidades = _perfil_Puesto_Servicio.ListarPerfilDetHabilidades(pHabilidad_Competencias_Registro);
            IEnumerable<Habilidad_Competencias_Registro> Competencias = _perfil_Puesto_Servicio.ListarPerfilDetCompetencias(pHabilidad_Competencias_Registro);
            IEnumerable<PerfilCoordinaciones_Registro> CoordinacionInterna = _perfil_Puesto_Servicio.ListarPerfilDetCoordinacionInterna(pPerfilCoordinaciones_Registro);
            IEnumerable<PerfilCoordinaciones_Registro> CoordinacionExterna = _perfil_Puesto_Servicio.ListarPerfilDetCoordinacionExterna(pPerfilCoordinaciones_Registro);
            IEnumerable<PerfilConocimientos_Registro> ConocimientosTecnicos = _perfil_Puesto_Servicio.ListarPerfilDetConocimientosTecnicos(pPerfilConocimientos_Registro);
            IEnumerable<PerfilConocimientos_Registro> ConocimientosCursosProgramas = _perfil_Puesto_Servicio.ListarPerfilDetConocimientosCursosProgramas(pPerfilConocimientos_Registro);
            IEnumerable<PerfilConocimientos_Registro> ConocimientosOfficeIdiomas = _perfil_Puesto_Servicio.ListarPerfilDetConocimientosOfficeIdiomas(pPerfilConocimientos_Registro);
            IEnumerable<PerfilFormacionAcademica_Registro> FormAcaNivelBasico = _perfil_Puesto_Servicio.ListarPerfilDetFormAcaNivelBasico(pPerfilFormacionAcademica_Registro);
            IEnumerable<PerfilFormacionAcademica_Registro> FormAcaNivelEducativo = _perfil_Puesto_Servicio.ListarPerfilDetFormAcaNivelEducativo(pPerfilFormacionAcademica_Registro);
            IEnumerable<PerfilFormacionAcademica_Registro> FormAcaMaestria = _perfil_Puesto_Servicio.ListarPerfilDetFormAcaMaestria(pPerfilFormacionAcademica_Registro);
            IEnumerable<PerfilFormacionAcademica_Registro> FormAcaDoctorado = _perfil_Puesto_Servicio.ListarPerfilDetFormAcaDoctorado(pPerfilFormacionAcademica_Registro);




            //PostulanteInformacion_Registro peticion = new PostulanteInformacion_Registro() { IdPostulante = IdPostulante, IdPostulacion = IdPostulacion, IdConvocatoria = IdConvocatoria };
            //peticion = _postulante_Servicio.ObtenerPostulanteFicha(peticion);
            string path = Server.MapPath("~/Documents/Bases");
            string fileName = "CAS " + BasesCab.FirstOrDefault().strNroCAS + " - " + BasesCab.FirstOrDefault().strNombrePuesto.ToUpper() + ".pdf";
            string fullPath = Path.Combine(path, fileName);

            System.IO.FileInfo file = new System.IO.FileInfo(fullPath);
            if (file.Exists)
            {
                return File(fullPath, "application/pdf");
            }
            else
            {
                Stream pdfStream = GenerarFichaBasesPerfilPuestoPdf(BasesCab, PerfilCab, Funciones, RequisitosAdicionales, Habilidades, Competencias, CoordinacionInterna, CoordinacionExterna, ConocimientosTecnicos, ConocimientosCursosProgramas, ConocimientosOfficeIdiomas, FormAcaNivelBasico, FormAcaNivelEducativo, FormAcaMaestria, FormAcaDoctorado);
                return File(pdfStream, "application/pdf"); // ("Ficha", "_Layout_Blank", oAsistenciaTecnicaDetalle_Registro);
            }            
        }

        [HttpPost]
        public JsonResult PlantillaBasesPerfilPuestoConvocatoria(string iCodBasePerfil, string iCodPerfil, string iCodTrabajador)
        {
            //ObtenerPerfilesPuestoPorID

            BasesPerfilPuestoRegistro pBasesPerfilPuestoRegistro = new BasesPerfilPuestoRegistro() { iCodBasePerfil = Convert.ToInt32(iCodBasePerfil), iCodTrabajadorReg = Convert.ToInt32(iCodTrabajador) };
            object respuesta = _bases_Perfil_Puesto_Servicio.PublicarBasesPerfilPuesto(pBasesPerfilPuestoRegistro);
            BasesPerfilPuesto_Request pBasesPerfilPuesto_Request = new BasesPerfilPuesto_Request() { iCodBasePerfil = Convert.ToInt32(iCodBasePerfil) };
            IEnumerable<BasesPerfilPuestoRegistro> BasesCab = _bases_Perfil_Puesto_Servicio.ObtenerBasesPerfilesPuestoPorID(iCodBasePerfil);

            PerfilFunciones_Request pPerfilFunciones_Request = new PerfilFunciones_Request() { iCodPerfil = Convert.ToInt32(iCodPerfil) };
            RequisitosAdicionales_Registro pRequisitosAdicionales_Registro = new RequisitosAdicionales_Registro() { iCodPerfil = Convert.ToInt32(iCodPerfil) };
            Habilidad_Competencias_Registro pHabilidad_Competencias_Registro = new Habilidad_Competencias_Registro() { iCodPerfil = Convert.ToInt32(iCodPerfil) };
            PerfilCoordinaciones_Registro pPerfilCoordinaciones_Registro = new PerfilCoordinaciones_Registro() { iCodPerfil = Convert.ToInt32(iCodPerfil) };
            PerfilConocimientos_Registro pPerfilConocimientos_Registro = new PerfilConocimientos_Registro() { iCodPerfil = Convert.ToInt32(iCodPerfil) };
            PerfilFormacionAcademica_Registro pPerfilFormacionAcademica_Registro = new PerfilFormacionAcademica_Registro() { iCodPerfil = Convert.ToInt32(iCodPerfil) };

            IEnumerable<PerfilPuestoRegistro> PerfilCab = _perfil_Puesto_Servicio.ObtenerPerfilesPuestoPorID(iCodPerfil);
            IEnumerable<PerfilFunciones_Request> Funciones = _perfil_Puesto_Servicio.ListarPerfilDetFunciones(pPerfilFunciones_Request);
            IEnumerable<RequisitosAdicionales_Registro> RequisitosAdicionales = _perfil_Puesto_Servicio.ListarPerfilDetRequisitosAdicionales(pRequisitosAdicionales_Registro);
            IEnumerable<Habilidad_Competencias_Registro> Habilidades = _perfil_Puesto_Servicio.ListarPerfilDetHabilidades(pHabilidad_Competencias_Registro);
            IEnumerable<Habilidad_Competencias_Registro> Competencias = _perfil_Puesto_Servicio.ListarPerfilDetCompetencias(pHabilidad_Competencias_Registro);
            IEnumerable<PerfilCoordinaciones_Registro> CoordinacionInterna = _perfil_Puesto_Servicio.ListarPerfilDetCoordinacionInterna(pPerfilCoordinaciones_Registro);
            IEnumerable<PerfilCoordinaciones_Registro> CoordinacionExterna = _perfil_Puesto_Servicio.ListarPerfilDetCoordinacionExterna(pPerfilCoordinaciones_Registro);
            IEnumerable<PerfilConocimientos_Registro> ConocimientosTecnicos = _perfil_Puesto_Servicio.ListarPerfilDetConocimientosTecnicos(pPerfilConocimientos_Registro);
            IEnumerable<PerfilConocimientos_Registro> ConocimientosCursosProgramas = _perfil_Puesto_Servicio.ListarPerfilDetConocimientosCursosProgramas(pPerfilConocimientos_Registro);
            IEnumerable<PerfilConocimientos_Registro> ConocimientosOfficeIdiomas = _perfil_Puesto_Servicio.ListarPerfilDetConocimientosOfficeIdiomas(pPerfilConocimientos_Registro);
            IEnumerable<PerfilFormacionAcademica_Registro> FormAcaNivelBasico = _perfil_Puesto_Servicio.ListarPerfilDetFormAcaNivelBasico(pPerfilFormacionAcademica_Registro);
            IEnumerable<PerfilFormacionAcademica_Registro> FormAcaNivelEducativo = _perfil_Puesto_Servicio.ListarPerfilDetFormAcaNivelEducativo(pPerfilFormacionAcademica_Registro);
            IEnumerable<PerfilFormacionAcademica_Registro> FormAcaMaestria = _perfil_Puesto_Servicio.ListarPerfilDetFormAcaMaestria(pPerfilFormacionAcademica_Registro);
            IEnumerable<PerfilFormacionAcademica_Registro> FormAcaDoctorado = _perfil_Puesto_Servicio.ListarPerfilDetFormAcaDoctorado(pPerfilFormacionAcademica_Registro);

            Stream pdfStream = GenerarFichaBasesPerfilPuestoPdf(BasesCab, PerfilCab, Funciones, RequisitosAdicionales, Habilidades, Competencias, CoordinacionInterna, CoordinacionExterna, ConocimientosTecnicos, ConocimientosCursosProgramas, ConocimientosOfficeIdiomas, FormAcaNivelBasico, FormAcaNivelEducativo, FormAcaMaestria, FormAcaDoctorado);
            //object respuesta = new object();
            string path = Server.MapPath("~/Documents/Bases");
            string strParametro = ConfigurationManager.AppSettings["rutaBasesConvocatoria"].ToString();
            string pathConvocatorias = _bases_Perfil_Puesto_Servicio.ObtenerRutaBasesConvocatorias(strParametro);
            string fileName = "CAS " + BasesCab.FirstOrDefault().strNroCAS + " - " + BasesCab.FirstOrDefault().strNombrePuesto.ToUpper() + ".pdf";//Path.GetFileName("");
            string fullPath = Path.Combine(path, fileName);
            string fullPathConvocatoria = Path.Combine(pathConvocatorias, fileName);
                        
            using (FileStream fs = new FileStream(fullPath, FileMode.Create, System.IO.FileAccess.Write))
            {
                pdfStream.CopyTo(fs);                
                try
                {
                    System.IO.File.Copy(fullPath, fullPathConvocatoria, true);
                }
                catch (IOException iox)
                {
                    Console.WriteLine(iox.Message);
                }  
            }
            
            return Json(respuesta, JsonRequestBehavior.AllowGet);
        }

        [AllowAnonymous]
        public ActionResult DescargarArchivo(string id)
        {
            BasesPerfilPuestoRegistro peticion = new BasesPerfilPuestoRegistro();
            peticion.iCodBasePerfil = Int32.Parse(id);
            //peticion.Nombre = String.Empty;
            //peticion.Estado = -1;

            var lista = _bases_Perfil_Puesto_Servicio.ObtenerBasesPerfilesPuestoPorID(id).Select(p => new { p.iCodBasePerfil, p.strNombreArchivo, p.archivo });
            var item = lista.Where(x => x.iCodBasePerfil == peticion.iCodBasePerfil).SingleOrDefault();

            return File(item.archivo, "application/pdf", "BasesConvocatoria" + ".pdf");
        }

        private Stream GenerarFichaBasesPerfilPuestoPdf(IEnumerable<BasesPerfilPuestoRegistro> BasesCab, IEnumerable<PerfilPuestoRegistro> PerfilCab, IEnumerable<PerfilFunciones_Request> Funciones, IEnumerable<RequisitosAdicionales_Registro> RequisitosAdicionales, IEnumerable<Habilidad_Competencias_Registro> Habilidades, IEnumerable<Habilidad_Competencias_Registro> Competencias, IEnumerable<PerfilCoordinaciones_Registro> CoordinacionInterna, IEnumerable<PerfilCoordinaciones_Registro> CoordinacionExterna, IEnumerable<PerfilConocimientos_Registro> ConocimientosTecnicos, IEnumerable<PerfilConocimientos_Registro> ConocimientosCursosProgramas, IEnumerable<PerfilConocimientos_Registro> ConocimientosOfficeIdiomas, IEnumerable<PerfilFormacionAcademica_Registro> FormAcaNivelBasico, IEnumerable<PerfilFormacionAcademica_Registro> FormAcaNivelEducativo, IEnumerable<PerfilFormacionAcademica_Registro> FormAcaMaestria, IEnumerable<PerfilFormacionAcademica_Registro> FormAcaDoctorado)
        {

            //string headerUrl = (new Uri(Request.Url, Page.ResolveUrl("~/files/header.html")))
            //                        .AbsoluteUri; 

            HtmlToPdf converter = new HtmlToPdf();
            converter.Options.PdfPageSize = PdfPageSize.A4;

            converter.Options.PdfPageOrientation = PdfPageOrientation.Portrait;
            converter.Options.WebPageWidth = 1024;
            converter.Options.WebPageHeight = 0;
            converter.Options.WebPageFixedSize = false;
            converter.Options.AutoFitWidth = HtmlToPdfPageFitMode.ShrinkOnly;
            converter.Options.AutoFitHeight = HtmlToPdfPageFitMode.NoAdjustment;

            converter.Options.MarginLeft = 30;
            converter.Options.MarginRight = 30;
            converter.Options.MarginTop = 30;
            converter.Options.MarginBottom = 30;

            // header settings
            //converter.Options.DisplayHeader = showHeaderOnFirstPage || showHeaderOnOddPages || showHeaderOnEvenPages;
            //converter.Header.DisplayOnFirstPage = showHeaderOnFirstPage;
            //converter.Header.DisplayOnOddPages = showHeaderOnOddPages;
            //converter.Header.DisplayOnEvenPages = showHeaderOnEvenPages;
            //converter.Header.Height = headerHeight;

            //PdfHtmlSection headerHtml = new PdfHtmlSection(headerUrl);
            //headerHtml.AutoFitHeight = HtmlToPdfPageFitMode.AutoFit;
            //converter.Header.Add(headerHtml);

                       

            string html = System.IO.File.ReadAllText(Path.Combine(Server.MapPath("~/Templates/"), "Bases/bases.html"));
            //string htmlParticipantes = System.IO.File.ReadAllText(ConfigurationManager.AppSettings["Negociacion.Ficha.Plantilla.Participantes.Ruta"]);
            //string htmlIndicadores = System.IO.File.ReadAllText(ConfigurationManager.AppSettings["Negociacion.Ficha.Plantilla.Indicadores.Ruta"]);

            //CultureInfo culture = new CultureInfo("es-PE");
            #region Bases
            //html = html.Replace("//logo", Path.Combine(Server.MapPath("~/Templates/"), "PerfilPuesto/logo_servir.png"));
            html = html.Replace("//strNroCAS", BasesCab.FirstOrDefault().strNroCAS);
            html = html.Replace("//iCantPersonalRequerido", BasesCab.FirstOrDefault().iCantPersonalRequerido.ToString());
            html = html.Replace("//unidad_organica_solicitante", BasesCab.FirstOrDefault().strUnidadOrganica);
            html = html.Replace("//puesto", BasesCab.FirstOrDefault().strNombrePuesto.Trim());
            //html = html.Replace("//dFechaAprobConv", BasesCab.FirstOrDefault().dFechaAprobConv.ToString("dd/MM/yyyy"));
            //html = html.Replace("//dFechaDesdePubSNE_MTPE", BasesCab.FirstOrDefault().dFechaDesdePubSNE_MTPE.ToString("dd/MM/yyyy"));
            //html = html.Replace("//dFechaHastaPubSNE_MTPE", BasesCab.FirstOrDefault().dFechaHastaPubSNE_MTPE.ToString("dd/MM/yyyy"));
            html = html.Replace("//dFechaDesdePubMIDIS", BasesCab.FirstOrDefault().dFechaDesdePubMIDIS.ToString("dd/MM/yyyy"));
            html = html.Replace("//dFechaHastaPubMIDIS", BasesCab.FirstOrDefault().dFechaHastaPubMIDIS.ToString("dd/MM/yyyy"));
            html = html.Replace("//dFechaRegCVPostulante", BasesCab.FirstOrDefault().dFechaRegCVPostulante.ToString("dd/MM/yyyy"));
            html = html.Replace("//dFechaDesdeEvaCV", BasesCab.FirstOrDefault().dFechaDesdeEvaCV.ToString("dd/MM/yyyy"));
            html = html.Replace("//dFechaHastaEvaCV", BasesCab.FirstOrDefault().dFechaHastaEvaCV.ToString("dd/MM/yyyy"));
            html = html.Replace("//dFechaPubResultadoMIDIS", BasesCab.FirstOrDefault().dFechaPubResultadoMIDIS.ToString("dd/MM/yyyy"));
            html = html.Replace("//dFechaDesdeEntrevista", BasesCab.FirstOrDefault().dFechaDesdeEntrevista.ToString("dd/MM/yyyy"));
            html = html.Replace("//dFechaHastaEntrevista", BasesCab.FirstOrDefault().dFechaHastaEntrevista.ToString("dd/MM/yyyy"));
            html = html.Replace("//dFechaPubResultadoFinalMIDIS", BasesCab.FirstOrDefault().dFechaPubResultadoFinalMIDIS.ToString("dd/MM/yyyy"));
            html = html.Replace("//dFechaDesdeSuscripcionContrato", BasesCab.FirstOrDefault().dFechaDesdeSuscripcionContrato.ToString("dd/MM/yyyy"));
            html = html.Replace("//dFechaHastaSuscripcionContrato", BasesCab.FirstOrDefault().dFechaHastaSuscripcionContrato.ToString("dd/MM/yyyy"));
            if (BasesCab.FirstOrDefault().bDuracionContrato31Diciembre)
            {
                html = html.Replace("//Duracion", "Hasta el 31 de Diciembre");
            }
            else
            {
                html = html.Replace("//Duracion", fn_anio(Convert.ToInt32(BasesCab.FirstOrDefault().strDuracionContrato)) + " desde la suscripción del contrato  (prorrogable).");
            }

            html = html.Replace("//remuneracion", "S/"+BasesCab.FirstOrDefault().decRemuneracion+" ("+ NumeroALetras(BasesCab.FirstOrDefault().decRemuneracion)+" soles)");
            
            #endregion

            #region Funciones
            


            int contFunciones = 0;
            if (Funciones.Count() > 0)
            {
                String strFunciones = string.Empty;
                foreach (PerfilFunciones_Request item in Funciones)
                {
                    contFunciones += 1;
                    string strLetra = string.Empty;
                    switch (contFunciones)
                    {
                        case 1:
                            {
                                strLetra = "a)";
                                break;
                            }
                        case 2:
                            {
                                strLetra = "b)";
                                break;
                            }
                        case 3:
                            {
                                strLetra = "c)";
                                break;
                            }
                        case 4:
                            {
                                strLetra = "d)";
                                break;
                            }
                        case 5:
                            {
                                strLetra = "e)";
                                break;
                            }
                        case 6:
                            {
                                strLetra = "f)";
                                break;
                            }
                        case 7:
                            {
                                strLetra = "g)";
                                break;
                            }
                        case 8:
                            {
                                strLetra = "h)";
                                break;
                            }
                        case 9:
                            {
                                strLetra = "i)";
                                break;
                            }
                        case 10:
                            {
                                strLetra = "j)";
                                break;
                            }
                        case 11:
                            {
                                strLetra = "k)";
                                break;
                            }
                        case 12:
                            {
                                strLetra = "l)";
                                break;
                            }
                        case 13:
                            {
                                strLetra = "m)";
                                break;
                            }
                        case 14:
                            {
                                strLetra = "n)";
                                break;
                            }
                        case 15:
                            {
                                strLetra = "o)";
                                break;
                            }
                        case 16:
                            {
                                strLetra = "p)";
                                break;
                            }
                        case 17:
                            {
                                strLetra = "q)";
                                break;
                            }
                        case 18:
                            {
                                strLetra = "r)";
                                break;
                            }
                        case 19:
                            {
                                strLetra = "s)";
                                break;
                            }
                        case 20:
                            {
                                strLetra = "t)";
                                break;
                            }
                        case 21:
                            {
                                strLetra = "u)";
                                break;
                            }
                        case 22:
                            {
                                strLetra = "v)";
                                break;
                            }
                        case 23:
                            {
                                strLetra = "w)";
                                break;
                            }
                        case 24:
                            {
                                strLetra = "x)";
                                break;
                            }
                        case 25:
                            {
                                strLetra = "y)";
                                break;
                            }
                        case 26:
                            {
                                strLetra = "z)";
                                break;
                            }
                    }
                    strFunciones = strFunciones + "<tr>" +
                                                    "<td style='width: 20px;'>" + strLetra + "</td>" +
                                                    "<td style='width: 525px;'>" + item.Verbo.strDescripcion + " " + item.Objetivo + "" + item.Funcion + "</td>" +
                                                "</tr>" +
                                                "<tr>" +
                                                    "<td style='width: 100%;' colspan='2'>&nbsp;</td>" +
                                                "</tr>";                    
                    
                }
                html = html.Replace("//funciones", strFunciones);
            }
            
            #endregion
            
            #region Habilidades_Competencias

            string strHabilidades_Competencias = string.Empty;

            if (Habilidades.Count() > 0)
            {
                foreach (var item in Habilidades)
                {
                    strHabilidades_Competencias = strHabilidades_Competencias + "•&nbsp;" + item.Cualidad.strNombre + "<br />";
                }
            }
            if (Competencias.Count() > 0)
            {
                foreach (var item in Competencias)
                {
                    strHabilidades_Competencias = strHabilidades_Competencias + "•&nbsp;" + item.Cualidad.strNombre + "<br />";
                }
            }

            html = html.Replace("//habilidad_Competencias", strHabilidades_Competencias);
                        
            #endregion

            #region Formacion_Academica
            int iCantFormacion_Academica = 0;
            string strFormacion_Academica = string.Empty;

            //if (FormAcaNivelBasico.Count() > 0)
            //{
            //    foreach (var item in FormAcaNivelBasico)
            //    {
            //        iCantFormacion_Academica += 1;
            //        if (iCantFormacion_Academica == 1)
            //        {
            //            strFormacion_Academica = strFormacion_Academica + "•&nbsp;" + item.strGrado + " o,";
            //        }
            //        else
            //        {
            //            strFormacion_Academica = strFormacion_Academica + item.strGrado + " o,";
            //        }
                    
            //    }
            //}
            if (FormAcaNivelEducativo.Count() > 0)
            {
                foreach (var item in FormAcaNivelEducativo)
                {
                    iCantFormacion_Academica += 1;
                    if (iCantFormacion_Academica == 1)
                    {
                        strFormacion_Academica = strFormacion_Academica + "•&nbsp;" + item.strNivel4 + " o,";
                    }
                    else
                    {
                        if (FormAcaNivelEducativo.Last() == item)
                        {
                            strFormacion_Academica = strFormacion_Academica + item.strNivel4;    
                        }
                        else
                        {
                            strFormacion_Academica = strFormacion_Academica + item.strNivel4 + " o,";
                        }                        
                    }

                }
            }
            if (FormAcaMaestria.Count() > 0)
            {
                foreach (var item in FormAcaMaestria)
                {
                    iCantFormacion_Academica += 1;
                    if (iCantFormacion_Academica == 1)
                    {
                        strFormacion_Academica = strFormacion_Academica + "•&nbsp;" + item.strNivel4 + " o,";
                    }
                    else
                    {
                        if (FormAcaMaestria.Last() == item)
                        {
                            strFormacion_Academica = strFormacion_Academica + item.strNivel4;
                        }
                        else
                        {
                            strFormacion_Academica = strFormacion_Academica + item.strNivel4 + " o,";
                        }                           
                    }

                }
            }
            if (FormAcaDoctorado.Count() > 0)
            {
                foreach (var item in FormAcaDoctorado)
                {
                    iCantFormacion_Academica += 1;
                    if (iCantFormacion_Academica == 1)
                    {
                        strFormacion_Academica = strFormacion_Academica + "•&nbsp;" + item.strNivel4 + " o,";
                    }
                    else
                    {
                        if (FormAcaDoctorado.Last() == item)
                        {
                            strFormacion_Academica = strFormacion_Academica + item.strNivel4;
                        }
                        else
                        {
                            strFormacion_Academica = strFormacion_Academica + item.strNivel4 + " o,";
                        }                         
                    }
                }
            }


            html = html.Replace("//carreras", strFormacion_Academica);

            #endregion

            #region Experiencia_General

            string strExperiencia_General = string.Empty;
            strExperiencia_General = fn_anio(PerfilCab.FirstOrDefault().iAnioExpGeneral);
            html = html.Replace("//exp_gen", strExperiencia_General);           

            #endregion

            #region Experiencia_Especifico_publico
            if (PerfilCab.FirstOrDefault().iAnioExpEspecifica > 0)
            {
                string strExperiencia_Especifico = string.Empty;
                strExperiencia_Especifico = fn_anio(PerfilCab.FirstOrDefault().iAnioExpEspecifica);
                html = html.Replace("//exp_esp", strExperiencia_Especifico);
            }
            if (PerfilCab.FirstOrDefault().iAnioExpSectorPublico>0)
            {
                string strExperiencia_publico = string.Empty;
                strExperiencia_publico = fn_anio(PerfilCab.FirstOrDefault().iAnioExpSectorPublico);
                html = html.Replace("//exp_sec_pub", ", de los cuales " + strExperiencia_publico + "en el sector público.");
            }
            

            #endregion

            #region Cursos_Programas
            
            string strCursos_Programas = string.Empty;

            if (ConocimientosCursosProgramas.Count() > 0)
            {
                foreach (var item in ConocimientosCursosProgramas)
                {                    
                    strCursos_Programas = strCursos_Programas + "•&nbsp;" + item.Conocimientos + ".";                    
                }
            }

            if (ConocimientosOfficeIdiomas.Count() > 0)
            {
                foreach (var item in ConocimientosOfficeIdiomas)
                {
                    strCursos_Programas = strCursos_Programas + "•&nbsp;" + item.Conocimientos + ".";
                }
            }            

            html = html.Replace("//cursos_programas", strCursos_Programas);

            #endregion

            #region Conocimientos

            string strConocimientos = string.Empty;

            if (RequisitosAdicionales.Count() > 0)
            {
                foreach (var item in RequisitosAdicionales)
                {
                    strConocimientos = strConocimientos + "•&nbsp;" + item.Requisito + ".";
                }
            }

            html = html.Replace("//conocimientos", strConocimientos);

            #endregion


            converter.Options.DisplayHeader = true;
            converter.Header.DisplayOnFirstPage = true;
            converter.Header.DisplayOnOddPages = true;
            converter.Header.DisplayOnEvenPages = true;
            converter.Header.Height = 50;
            PdfHtmlSection headerHtml = new PdfHtmlSection(Path.Combine(Server.MapPath("~/Templates/"), "Bases/OGRH.png"));
            headerHtml.AutoFitHeight = HtmlToPdfPageFitMode.AutoFit;
            //PdfTextSection headerText = new PdfTextSection(100, 2, "Decenio de la Igualdad de oportunidades para mujeres y hombres", );
            converter.Header.Add(headerHtml);

            SelectPdf.PdfDocument doc = converter.ConvertHtmlString(html, Server.MapPath("~/temp"));

            MemoryStream pdfStream = new MemoryStream();
            doc.Save(pdfStream);
            pdfStream.Position = 0;
            doc.Close();

            return pdfStream;
        }
            
        private string fn_anio(int iAnio)
        {
            string strAnio = string.Empty;
            strAnio = NumeroALetrasContrato(iAnio) + " (" + iAnio.ToString("D2") + ")" + " años";
            //switch (iAnio)
            //{
            //    case 1:
            //        {
            //            strAnio = "Un (01) años";
            //            break;
            //        }
            //    case 2:
            //        {
            //            strAnio = "Dos (02) años";
            //            break;
            //        }
            //    case 3:
            //        {
            //            strAnio = "Tres (03) años";
            //            break;
            //        }
            //    case 4:
            //        {
            //            strAnio = "Cuarto (04) años";
            //            break;
            //        }
            //    case 5:
            //        {
            //            strAnio = "Cinco (05) años";
            //            break;
            //        }
            //    case 6:
            //        {
            //            strAnio = "Seis (06) años";
            //            break;
            //        }
            //    case 7:
            //        {
            //            strAnio = "Siete (07) años";
            //            break;
            //        }
            //    case 8:
            //        {
            //            strAnio = "Ocho (08) años";
            //            break;
            //        }
            //    case 9:
            //        {
            //            strAnio = "Nueve (09) años";
            //            break;
            //        }
            //    case 10:
            //        {
            //            strAnio = "Diez (10) años";
            //            break;
            //        }
            //    case 11:
            //        {
            //            strAnio = "Once (11) años";
            //            break;
            //        }
            //    case 12:
            //        {
            //            strAnio = "Doce (12) años";
            //            break;
            //        }
            //    case 13:
            //        {
            //            strAnio = "Trece (13) años";
            //            break;
            //        }
            //    case 14:
            //        {
            //            strAnio = "Catorce (14) años";
            //            break;
            //        }
            //    case 15:
            //        {
            //            strAnio = "Quince (15) años";
            //            break;
            //        }
            //    case 16:
            //        {
            //            strAnio = "Dieciseis (16) años";
            //            break;
            //        }
            //    case 17:
            //        {
            //            strAnio = "Dieciseite (17) años";
            //            break;
            //        }
            //    case 18:
            //        {
            //            strAnio = "Dieciocho (18) años";
            //            break;
            //        }
            //    case 19:
            //        {
            //            strAnio = "Diecinueve (19) años";
            //            break;
            //        }
            //    case 20:
            //        {
            //            strAnio = "Veinte (20) años";
            //            break;
            //        }
            //}

            return strAnio;

        }

        public string NumeroALetrasContrato(decimal numberAsString)
        {
            string dec;

            var entero = Convert.ToInt64(Math.Truncate(numberAsString));
            //var decimales = Convert.ToInt32(Math.Round((numberAsString - entero) * 100, 2));
            //if (decimales > 0)
            //{
            //    dec = " SOLES y " + decimales.ToString() + "/100";
            //    //dec = " SOLES {decimales:0,0} /100";
            //}
            ////Código agregado por mí
            //else
            //{
            //    dec = " SOLES y " + decimales.ToString() + "/100";
            //    //dec = " SOLES {decimales:0,0} /100";
            //}
            var res = NumeroALetras2(Convert.ToDouble(entero));
            return res;
        }

        public string NumeroALetras(decimal numberAsString)
        {
            string dec;            
           
            var entero = Convert.ToInt64(Math.Truncate(numberAsString));
            var decimales = Convert.ToInt32(Math.Round((numberAsString - entero) * 100, 2));
            if (decimales > 0)
            {
                dec = " SOLES y " + decimales.ToString() + "/100";
                //dec = " SOLES {decimales:0,0} /100";
            }
            //Código agregado por mí
            else
            {
                dec = " SOLES y " + decimales.ToString() + "/100";
                //dec = " SOLES {decimales:0,0} /100";
            }
            var res = NumeroALetras1(Convert.ToDouble(entero)) + dec;
            return res;
        }

        //[SuppressMessage("ReSharper", "CompareOfFloatsByEqualityOperator")]
        private string NumeroALetras1(double value)
        {
            string num2Text; value = Math.Truncate(value);
            if (value == 0) num2Text = "CERO";
            else if (value == 1) num2Text = "UNO";
            else if (value == 2) num2Text = "DOS";
            else if (value == 3) num2Text = "TRES";
            else if (value == 4) num2Text = "CUATRO";
            else if (value == 5) num2Text = "CINCO";
            else if (value == 6) num2Text = "SEIS";
            else if (value == 7) num2Text = "SIETE";
            else if (value == 8) num2Text = "OCHO";
            else if (value == 9) num2Text = "NUEVE";
            else if (value == 10) num2Text = "DIEZ";
            else if (value == 11) num2Text = "ONCE";
            else if (value == 12) num2Text = "DOCE";
            else if (value == 13) num2Text = "TRECE";
            else if (value == 14) num2Text = "CATORCE";
            else if (value == 15) num2Text = "QUINCE";
            else if (value < 20) num2Text = "DIECI" + NumeroALetras1(value - 10);
            else if (value == 20) num2Text = "VEINTE";
            else if (value < 30) num2Text = "VEINTI" + NumeroALetras1(value - 20);
            else if (value == 30) num2Text = "TREINTA";
            else if (value == 40) num2Text = "CUARENTA";
            else if (value == 50) num2Text = "CINCUENTA";
            else if (value == 60) num2Text = "SESENTA";
            else if (value == 70) num2Text = "SETENTA";
            else if (value == 80) num2Text = "OCHENTA";
            else if (value == 90) num2Text = "NOVENTA";
            else if (value < 100) num2Text = NumeroALetras1(Math.Truncate(value / 10) * 10) + " Y " + NumeroALetras1(value % 10);
            else if (value == 100) num2Text = "CIEN";
            else if (value < 200) num2Text = "CIENTO " + NumeroALetras1(value - 100);
            else if ((value == 200) || (value == 300) || (value == 400) || (value == 600) || (value == 800)) num2Text = NumeroALetras1(Math.Truncate(value / 100)) + "CIENTOS";
            else if (value == 500) num2Text = "QUINIENTOS";
            else if (value == 700) num2Text = "SETECIENTOS";
            else if (value == 900) num2Text = "NOVECIENTOS";
            else if (value < 1000) num2Text = NumeroALetras1(Math.Truncate(value / 100) * 100) + " " + NumeroALetras1(value % 100);
            else if (value == 1000) num2Text = "MIL";
            else if (value < 2000) num2Text = "MIL " + NumeroALetras1(value % 1000);
            else if (value < 1000000)
            {
                num2Text = NumeroALetras1(Math.Truncate(value / 1000)) + " MIL";
                if ((value % 1000) > 0)
                {
                    num2Text = num2Text + " " + NumeroALetras1(value % 1000);
                }
            }
            else if (value == 1000000)
            {
                num2Text = "UN MILLON";
            }
            else if (value < 2000000)
            {
                num2Text = "UN MILLON " + NumeroALetras1(value % 1000000);
            }
            else if (value < 1000000000000)
            {
                num2Text = NumeroALetras1(Math.Truncate(value / 1000000)) + " MILLONES ";
                if ((value - Math.Truncate(value / 1000000) * 1000000) > 0)
                {
                    num2Text = num2Text + " " + NumeroALetras1(value - Math.Truncate(value / 1000000) * 1000000);
                }
            }
            else if (value == 1000000000000) num2Text = "UN BILLON";
            else if (value < 2000000000000) num2Text = "UN BILLON " + NumeroALetras1(value - Math.Truncate(value / 1000000000000) * 1000000000000);
            else
            {
                num2Text = NumeroALetras1(Math.Truncate(value / 1000000000000)) + " BILLONES";
                if ((value - Math.Truncate(value / 1000000000000) * 1000000000000) > 0)
                {
                    num2Text = num2Text + " " + NumeroALetras1(value - Math.Truncate(value / 1000000000000) * 1000000000000);
                }
            }
            return num2Text;
        }

        private string NumeroALetras2(double value)
        {
            string num2Text; value = Math.Truncate(value);
            if (value == 0) num2Text = "CERO";
            else if (value == 1) num2Text = "Un";
            else if (value == 2) num2Text = "Dos";
            else if (value == 3) num2Text = "Tres";
            else if (value == 4) num2Text = "Cuatro";
            else if (value == 5) num2Text = "Cinco";
            else if (value == 6) num2Text = "Seis";
            else if (value == 7) num2Text = "Siete";
            else if (value == 8) num2Text = "Ocho";
            else if (value == 9) num2Text = "Nueve";
            else if (value == 10) num2Text = "Diez";
            else if (value == 11) num2Text = "Once";
            else if (value == 12) num2Text = "Doce";
            else if (value == 13) num2Text = "Trece";
            else if (value == 14) num2Text = "Catorce";
            else if (value == 15) num2Text = "Quince";
            else if (value < 20) num2Text = "Dieci" + NumeroALetras2(value - 10);
            else if (value == 20) num2Text = "Veinte";
            else if (value < 30) num2Text = "Veinti" + NumeroALetras2(value - 20);
            else if (value == 30) num2Text = "Treinta";
            else if (value == 40) num2Text = "Cuarenta";
            else if (value == 50) num2Text = "Cincuenta";
            else if (value == 60) num2Text = "Sesenta";
            else if (value == 70) num2Text = "Setenta";
            else if (value == 80) num2Text = "Ochenta";
            else if (value == 90) num2Text = "Noventa";
            else if (value < 100) num2Text = NumeroALetras2(Math.Truncate(value / 10) * 10) + " Y " + NumeroALetras2(value % 10);
            else if (value == 100) num2Text = "Cien";
            else if (value < 200) num2Text = "Ciento " + NumeroALetras2(value - 100);
            else if ((value == 200) || (value == 300) || (value == 400) || (value == 600) || (value == 800)) num2Text = NumeroALetras1(Math.Truncate(value / 100)) + "CIENTOS";
            else if (value == 500) num2Text = "Quinientos";
            else if (value == 700) num2Text = "Setecientos";
            else if (value == 900) num2Text = "Novecientos";
            else if (value < 1000) num2Text = NumeroALetras2(Math.Truncate(value / 100) * 100) + " " + NumeroALetras2(value % 100);
            else if (value == 1000) num2Text = "Mil";
            else if (value < 2000) num2Text = "Mil " + NumeroALetras2(value % 1000);
            else if (value < 1000000)
            {
                num2Text = NumeroALetras2(Math.Truncate(value / 1000)) + " Mil";
                if ((value % 1000) > 0)
                {
                    num2Text = num2Text + " " + NumeroALetras2(value % 1000);
                }
            }
            else if (value == 1000000)
            {
                num2Text = "Un Millon";
            }
            else if (value < 2000000)
            {
                num2Text = "Un Millon " + NumeroALetras2(value % 1000000);
            }
            else if (value < 1000000000000)
            {
                num2Text = NumeroALetras2(Math.Truncate(value / 1000000)) + " Millones ";
                if ((value - Math.Truncate(value / 1000000) * 1000000) > 0)
                {
                    num2Text = num2Text + " " + NumeroALetras2(value - Math.Truncate(value / 1000000) * 1000000);
                }
            }
            else if (value == 1000000000000) num2Text = "Un Billon";
            else if (value < 2000000000000) num2Text = "Un Billon " + NumeroALetras2(value - Math.Truncate(value / 1000000000000) * 1000000000000);
            else
            {
                num2Text = NumeroALetras2(Math.Truncate(value / 1000000000000)) + " Billones";
                if ((value - Math.Truncate(value / 1000000000000) * 1000000000000) > 0)
                {
                    num2Text = num2Text + " " + NumeroALetras1(value - Math.Truncate(value / 1000000000000) * 1000000000000);
                }
            }
            return num2Text;
        }
    }
        
}