using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIDIS.ORI.Entidades
{
    public partial class BasesPerfilPuestoObservacionRequest
    {
        public int iMaeBasesPerfilObservaciones { get; set; }
        public int iCodBasePerfil { get; set; }
        public string strObservacion { get; set; }
        public DateTime datFechaReg { get; set; }
        public string strFechaReg { get; set; }
        public DateTime datFechaAprobacionAnterior { get; set; }
        public string strFechaAprobacionAnterior { get; set; }
    }
}
