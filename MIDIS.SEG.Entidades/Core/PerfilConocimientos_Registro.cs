using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIDIS.ORI.Entidades
{
    public partial class PerfilConocimientos_Registro
    {
        public int iCodPerfil { get; set; }
        public int iSecuencia { get; set; }
        public string Conocimientos { get; set; }
        public bool? bConDocumento { get; set; }
        public int? iCodTipoMateria { get; set; }
        public string strTipoMateria { get; set; }
        public int? iCodTipoMateriaOtros { get; set; }
        public string strTipoMateriaOtros { get; set; }
        public int? iCodTipoSubMateriaOtros { get; set; }
        public string strTipoSubMateriaOtros { get; set; }
        public int? iTipoNivelMateria { get; set; }
        public string strTipoNivelMateria { get; set; }

        public PerfilTipoMateria_Response PerfilTipoMateria { get; set; }

        public PerfilTipoMateriaOtros_Response PerfilTipoMateriaOtros { get; set; }

        public PerfilTipoSubMateriaOtros_Response PerfilTipoSubMateriaOtros { get; set; }

        public PerfilNivelMateria_Response PerfilNivelMateria { get; set; }

    }
}
