using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIDIS.ORI.Entidades.Auth
{

    public class BE_EncryptionAttribs
    {

        public int iCodEncriptacionDatos { get; set; }
        public String vTipoEncriptacion { get; set; }

        public String vClaveEncriptacion { get; set; }

        public byte[] vbSalt { get; set; }

        public int vbSaltSize { get; set; }

        public int iKeygenIterations { get; set; }
        public int iKeySizeInBits { get; set; }
    }


}
