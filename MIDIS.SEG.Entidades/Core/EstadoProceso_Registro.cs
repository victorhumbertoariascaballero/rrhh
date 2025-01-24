using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIDIS.ORI.Entidades.Core
{
    public class EstadoProceso_Registro
    {
        // Propiedad que corresponde a la columna iCodEstadoProceso
        public int iCodEstadoProceso { get; set; }

        // Propiedad que corresponde a la columna vDescripcion
        public string vDescripcion { get; set; }

        // Propiedad que corresponde a la columna iNivel
        public int? iNivel { get; set; } // Puede ser null, por eso usamos int?

        // Propiedad que corresponde a la columna bEstado
        public bool? bEstado { get; set; } // Puede ser null, por eso usamos bool?

        // Propiedad que corresponde a la columna dtAuditCreacion
        public DateTime? dtAuditCreacion { get; set; } // Puede ser null, por eso usamos DateTime?

        // Propiedad que corresponde a la columna vAuditCreacion
        public string vAuditCreacion { get; set; }

        // Propiedad que corresponde a la columna dtAuditModificacion
        public DateTime? dtAuditModificacion { get; set; } // Puede ser null, por eso usamos DateTime?

        // Propiedad que corresponde a la columna vAuditModificacion
        public string vAuditModificacion { get; set; }
    }
}
