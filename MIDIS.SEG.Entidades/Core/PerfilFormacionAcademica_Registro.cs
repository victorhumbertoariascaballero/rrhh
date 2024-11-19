using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIDIS.ORI.Entidades
{
    public partial class PerfilFormacionAcademica_Registro
    {
        public int iCodPerfil { get; set; }
        public int iSecuencia { get; set; }
        public string strCodCarrera { get; set; }
        public int? iCodGrado { get; set; }
        public string strGrado { get; set; }
        public int? iCodNivel { get; set; }
        public string strNivel { get; set; }

        public bool? bColegiatura { get; set; }
        public bool? bHabilitado { get; set; }
        public bool? bCompleto { get; set; }

        public string vColegiatura { get; set; }
        public string vHabilitado { get; set; }
        public string vCompleto { get; set; }

        public string strCodCarreraN1 { get; set; }
        public string strNivel1 { get; set; }
        public string strCodCarreraN2 { get; set; }
        public string strNivel2 { get; set; }
        public string strCodCarreraN3 { get; set; }
        public string strNivel3 { get; set; }
        public string strCodCarreraN4 { get; set; }
        public string strNivel4 { get; set; }

        public int? iCodSubTipoCarrera { get; set; }
    }
}
