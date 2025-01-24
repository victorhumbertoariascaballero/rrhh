using MIDIS.ORI.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCSisGesRRHH.Models
{
    public class ControlAsistenciaModel
    {
        public Empleado_Registro Empleado { get; set; }
        public IEnumerable<TurnoDiaSemana_Registro> TurnosDiasSemana { get; set; }
    }
}