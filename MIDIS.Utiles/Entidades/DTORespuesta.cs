using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MIDIS.Utiles.Entidades
{
    [Serializable]
    [DataContract]
    public class DTORespuesta
    {
        [DataMember]
        public long Codigo { get; set; }
        
    }
}
