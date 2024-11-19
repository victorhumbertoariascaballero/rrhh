using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIDIS.ORI.Entidades.Core
{
    public class Convocatoria_Historica
    {
        public Int32 IdConvocatoria { get; set; }
        public Int32 IdDependencia { get; set; }
        public Int32 IdTipo { get; set; } //1: CAS  2: PRACTICAS
        public Int32 CantidadVacantes { get; set; }
        public string NroConvocatoria { get; set; }
        public string CodigoConv { get; set; }
        public string Bases { get; set; }
        public string FechaPublicacion { get; set; }
        public string FechaIniPost { get; set; }
        public string FechaFinPost { get; set; }
        public string Dependencia { get; set; }
        public string NombreCargo { get; set; }
        public string DocumentoComunicados { get; set; }
        public string DocumentoCurricular { get; set; }
        public string DocumentoConocimientos { get; set; }
        public string DocumentoResultadoFinal { get; set; }
        public string DocumentoReactivar { get; set; }
        public Grilla_Response Grilla { get; set; }

    }
}
