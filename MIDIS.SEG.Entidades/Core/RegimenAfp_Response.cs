﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIDIS.ORI.Entidades
{
    public partial class RegimenAfp_Response
    {
        public Int32 iCodAFP { get; set; }
        public String vNombre { get; set; }
        public Boolean bEstado { get; set; }
        //
        public String sEstado { get; set; }

        public Grilla_Response Grilla { get; set; }
    }
}
