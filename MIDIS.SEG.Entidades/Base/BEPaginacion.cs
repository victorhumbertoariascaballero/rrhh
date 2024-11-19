using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MIDIS.ORI.Entidades.Base
{
    [Serializable]
    [DataContract]
    public class BEPaginacion
    {
        [DataMember]
        public int TotalVirtual { get; set; }
        [DataMember]
        public int CantRegxPag { get; set; }
        [DataMember]
        public DateTime Fecha { get; set; }
        [DataMember]
        public int Pagina { get; set; }
    }
}
