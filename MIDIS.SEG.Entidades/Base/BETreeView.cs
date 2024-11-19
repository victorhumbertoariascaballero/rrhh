using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MIDIS.ORI.Entidades.Base
{
    [DataContract]
    public class BETreeView
    {
        [DataMember]
        public string id { get; set; }
        [DataMember]
        public bool expanded { get; set; }
        [DataMember]
        public string text { get; set; }
        [DataMember]
        public bool selected { get; set; }
        [DataMember]
        public bool hasChildren { get; set; }
        [DataMember]
        public List<BETreeView> items { get; set; }
        [DataMember]
        public string eschecked { get; set; }
        [DataMember]
        public bool @checked { get; set; }
    }
}
