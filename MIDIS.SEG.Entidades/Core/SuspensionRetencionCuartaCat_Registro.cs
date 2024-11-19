using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIDIS.ORI.Entidades
{
    public partial class SuspensionRetencionCuartaCat_Registro
    {
        public int iCodTrabajador { get; set; }
        public int iMes { get; set; }
        public int iAnio { get; set; }
        public bool bEstadoRetencion { get; set; }
        public string sNroAutorizacionExoneracion { get; set; }        
        public String NombreCompleto { get { return String.Format("{0} {1}, {2}", Paterno, Materno, Nombre); } }        
        public String Nombre { get; set; }
        public String Paterno { get; set; }
        public String Materno { get; set; }
        public String TipoDocumento { get; set; }
        public String NroDocumento { get; set; }
        public IEnumerable<String> formatos { get; set; }
    }
}
