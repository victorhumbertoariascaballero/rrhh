using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MIDIS.ORI.Entidades
{
    public partial class EmpleadoSisper_Registro
	{
	    public String IdPlanilla { get; set; }
		public String TipoPlanilla { get; set; }
        public Int32 Anio { get; set; }
        public Int32 Mes { get; set; }
        public String Trabajador { get; set; }
        public String NivelRemunerativo { get; set; }
        public String CuentaBancaria { get; set; }
        public String NroAfiliacion { get; set; }
        public Int32 DiasLaborados { get; set; }
        public String NombreCompleto { get { return String.Format("{0}, {1}", Apellidos, Nombres); } }
        public String Nombres { get; set; }
		public String Apellidos { get; set; }
		public String NombreDependencia { get; set; }
		public String NroDocumento { get; set; }
		public String CargoEstructural { get; set; }
		public String CondicionLaboral { get; set; }
        public String NombreRegimenPensiones { get; set; }
        public String AbrevRegimenPensiones { get; set; }
        public String NombreBanco { get; set; }
        public String AbrevBanco { get; set; }
        public String NombreAFP { get; set; }
        public String NombrePlanilla { get; set; }
        public String NombreTipoPlanilla { get; set; }
        public Decimal Ingresos { get; set; }
        public Decimal Descuentos { get; set; }
        public Decimal Aportes { get; set; }
        public String FechaIngreso { get; set; }
        public String AnioMes { get { return String.Format("{0} {1}", Anio, NombreMes); } }
        public String NombreMes
        {
            get
            {
                String strMes = String.Empty;
                if (Mes == 1) strMes = "ENERO";
                if (Mes == 2) strMes = "FEBRERO";
                if (Mes == 3) strMes = "MARZO";
                if (Mes == 4) strMes = "ABRIL";
                if (Mes == 5) strMes = "MAYO";
                if (Mes == 6) strMes = "JUNIO";
                if (Mes == 7) strMes = "JULIO";
                if (Mes == 8) strMes = "AGOSTO";
                if (Mes == 9) strMes = "SETIEMBRE";
                if (Mes == 10) strMes = "OCTUBRE";
                if (Mes == 11) strMes = "NOVIEMBRE";
                if (Mes == 12) strMes = "DICIEMBRE";

                return strMes;
            }
        }
        //public byte[] Boleta { get; set; }
		
        //public Persona_Response Persona { get; set; }

        public Grilla_Response Grilla { get; set; }
	}
}