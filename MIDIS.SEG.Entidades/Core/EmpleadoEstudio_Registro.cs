using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIDIS.ORI.Entidades.Core
{
    public class EmpleadoEstudio_Registro
    {
        public int IdEstudio { get; set; }
        public int IdEmpleado { get; set; }
        public int IdSinTitulo { get; set; }
        public Int32 IdNivel { get; set; }
        public String Especialidad { get; set; }
        public String Institucion { get; set; }
        public int IdMes { get; set; }
        public int IdAnio { get; set; }
        public String Ciudad { get; set; }
        public Int32 IdFechaInicioMes { get; set; }
        public Int32 IdFechaInicioAnio { get; set; }
        public Int32 IdFechaFinMes { get; set; }
        public Int32 IdFechaFinAnio { get; set; }
        public Int32? IdUsuarioRegistro { get; set; }
        public Int32? IdUsuarioModificacion { get; set; }
        public Grilla_Response Grilla { get; set; }
        public PerfillNivelEducativo_Response NivelAlcanzado { get; set; }
        public Mes_Response Obtencion_Estudio_Mes { get; set; }
        public Anio_Response Obtencion_Estudio_Anio { get; set; }
        public Mes_Response Inicio_Estudio_Mes { get; set; }
        public Anio_Response Inicio_Estudio_Anio { get; set; }
        public Mes_Response Fin_Estudio_Mes { get; set; }
        public Anio_Response Fin_Estudio_Anio { get; set; }
    }
}
