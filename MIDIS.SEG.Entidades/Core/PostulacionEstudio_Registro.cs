using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MIDIS.ORI.Entidades
{
    public partial class PostulacionEstudio_Registro
    {
        public Int32 IdPostulacionEstudio { get; set; }
        public Int32 IdPostulacion { get; set; }
        public Int32 IdPostulante { get; set; }
        public Int32 IdEstudio { get; set; }
        public Int32 IdNivel { get; set; }
        public Int32 IdNoTieneTitulo { get; set; }
        public Int32 IdMes { get; set; }
        public Int32 IdAnio { get; set; }
        public Int32 IdFechaInicioMes { get; set; }
        public Int32 IdFechaInicioAnio { get; set; }
        public Int32 IdFechaFinMes { get; set; }
        public Int32 IdFechaFinAnio { get; set; }
        public String NombreGrado { get; set; }
        public String NombreNivel { get; set; }
        public String Especialidad { get; set; }
        public String Institucion { get; set; }
        public String Ciudad { get; set; }
        public Int32 Estado { get; set; }
        public String AnioMes
        {
            get
            {
                return formatAnioMes(IdMes, IdAnio);
            }
        }

        public String FechaInicioAnioMes
        {
            get
            {
                return formatAnioMes(IdFechaInicioMes, IdFechaInicioAnio);
            }
        }

        public String FechaFinAnioMes
        {
            get
            {
                return formatAnioMes(IdFechaFinMes, IdFechaFinAnio);
            }
        }

        public Int32 IdEstudioPerfil { get; set; }
        public Int32 IdEstudioAuditoria { get; set; }
        public Int32 IdTipoActualizacion { get; set; }
        public Int32 IdTipoConvocatoria { get; set; }
        public Int32? IdUsuarioRegistro { get; set; }
        public Int32? IdUsuarioModificacion { get; set; }
        public DateTime? FechaRegistro { get; set; }
        public DateTime? FechaModificacion { get; set; }
        public IEnumerable<object> formatos { get; set; }
        public Int32 IdTieneArchivo { get; set; }
        public byte[] FileArchivo { get; set; }
        public Estado_Response Requisito { get; set; }
        public Estado_Response Auditoria { get; set; }
        public Grilla_Response Grilla { get; set; }

        private string formatAnioMes(int mes, int anio)
        {
            String valor = String.Empty;

            switch (mes)
            {
                case 1:
                    valor = String.Format("{0}-{1}", anio.ToString(), "ENERO");
                    break;
                case 2:
                    valor = String.Format("{0}-{1}", anio.ToString(), "FEBRERO");
                    break;
                case 3:
                    valor = String.Format("{0}-{1}", anio.ToString(), "MARZO");
                    break;
                case 4:
                    valor = String.Format("{0}-{1}", anio.ToString(), "ABRIL");
                    break;
                case 5:
                    valor = String.Format("{0}-{1}", anio.ToString(), "MAYO");
                    break;
                case 6:
                    valor = String.Format("{0}-{1}", anio.ToString(), "JUNIO");
                    break;
                case 7:
                    valor = String.Format("{0}-{1}", anio.ToString(), "JULIO");
                    break;
                case 8:
                    valor = String.Format("{0}-{1}", anio.ToString(), "AGOSTO");
                    break;
                case 9:
                    valor = String.Format("{0}-{1}", anio.ToString(), "SEPTIEMBRE");
                    break;
                case 10:
                    valor = String.Format("{0}-{1}", anio.ToString(), "OCTUBRE");
                    break;
                case 11:
                    valor = String.Format("{0}-{1}", anio.ToString(), "NOVIEMBRE");
                    break;
                case 12:
                    valor = String.Format("{0}-{1}", anio.ToString(), "DICIEMBRE");
                    break;
            }

            return valor;
        }
    }
}