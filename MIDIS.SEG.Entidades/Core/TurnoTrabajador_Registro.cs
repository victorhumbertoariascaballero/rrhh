﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIDIS.ORI.Entidades
{
    public partial class TurnoTrabajador_Registro
    {
        public int iCodTurnoTrabajador { get; set; }
        public int iCodTurno { get; set; }
        public int iCodTrabajador { get; set; }
        public int iCodigoDependencia { get; set; }
        public bool bVigente { get; set; }
        public DateTime dtVigenciaInicio { get; set; }
        public DateTime dtVigenciaFin { get; set; }
        public bool bEstado { get; set; }
        public DateTime dtAuditCreacion { get; set; }
        public string vAuditCreacion { get; set; }
        public DateTime dtAuditModificacion { get; set; }
        public string vAuditModificacion { get; set; }
        public Grilla_Response Grilla { get; set; }

        public string vNombreTrabajador { get; set; }
        public string vNumeroDocumento { get; set; }
        public string vTurno { get; set; }
        public string vDependencia { get; set; }
    }
}