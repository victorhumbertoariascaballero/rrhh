﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIDIS.ORI.Entidades
{
   
    public partial class RegimenPensionario_Registro
    {
        //public Int32 iCodRegistroRegimenPen { get; set; }
        public Int32 iCodRegimenPen { get; set; }

        //public Int32 iCodTipoRegimenPen { get; set; }
        //public Int32 iCodAFP { get; set; }
        //public Int32 iMes { get; set; }
        //public Int32 iAnio { get; set; }
        public String sNombre { get; set; }
        //public String sTipo { get; set; }
        //public Decimal dcComision { get; set; }
        //public Decimal dcPrimaSeguro { get; set; }
        //public Decimal dcAporte { get; set; }
        //public Decimal dcTope { get; set; }
        public bool bEstado { get; set; }
        /* */
        //public String sNombreRegimenPensionario { get; set; }
        //public String sNombreTipoRegimenPensionario { get; set; }
        //public string sEstado { get; set; }

        public Grilla_Response Grilla { get; set; }

    }
}
