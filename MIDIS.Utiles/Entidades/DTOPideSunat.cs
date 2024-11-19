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
    public class DTOPideSunat
    {
        [DataMember]
        public int Code { get; set; }
        [DataMember]
        public string Info { get; set; }
        [DataMember]
        public bool Success { get; set; }
        [DataMember]
        public string cod_dep { get; set; }
        [DataMember]
        public string cod_prov { get; set; }
        [DataMember]
        public string ddp_ciiu { get; set; }
        [DataMember]
        public string ddp_fecact { get; set; }
        [DataMember]
        public string ddp_fecalt { get; set; }
        [DataMember]
        public string ddp_nombre { get; set; }
        [DataMember]
        public string ddp_nomvia { get; set; }
        [DataMember]
        public string ddp_nomzon { get; set; }
        [DataMember]
        public string ddp_numer1 { get; set; }
        [DataMember]
        public string ddp_numreg { get; set; }
        [DataMember]
        public string ddp_numruc { get; set; }
        [DataMember]
        public string ddp_tipzon { get; set; }
        [DataMember]
        public string ddp_tpoemp { get; set; }
        [DataMember]
        public string ddp_ubigeo { get; set; }
        [DataMember]
        public string desc_ciiu { get; set; }
        [DataMember]
        public string desc_dep { get; set; }
        [DataMember]
        public string desc_dist { get; set; }
        [DataMember]
        public string desc_estado { get; set; }
        [DataMember]
        public string desc_flag22 { get; set; }
        [DataMember]
        public string desc_identi { get; set; }
        [DataMember]
        public string desc_numreg { get; set; }
        [DataMember]
        public string desc_prov { get; set; }
        [DataMember]
        public string desc_tipzon { get; set; }
        [DataMember]
        public string desc_tpoemp { get; set; }
        [DataMember]
        public bool esActivo { get; set; }
        [DataMember]
        public bool esHabido { get; set; }
        [DataMember]
        public string Materno { get; set; }
        [DataMember]
        public string Nombre { get; set; }
        [DataMember]
        public string Paterno { get; set; }

    }
}
