using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MIDIS.ORI.Entidades
{
    public partial class EmpleadoBoletaResumen_Registro
	{
	    public Int32 Anio { get; set; }
        public Int32 Mes { get; set; }
        public Int32 TotalSisper { get; set; }
        public Int32 TotalValidado { get; set; }
        public Int32 TotalEnviado { get; set; }
        public String FechaIngreso { get; set; }
        public String IdPlanilla { get; set; }
        public String IdTipoPlanilla { get; set; }
        public String NombrePlanilla { get; set; }
        public String NombreTipoPlanilla { get; set; }
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
        public String Glosa
        {
            get
            {
                String glosa = String.Empty;
                if (Anio == 2018 && Mes == 11) glosa = "Duplicado obtenido del SISPER";
                if (Anio == 2018 && Mes == 12) glosa = "Duplicado obtenido del SISPER";
                if (Anio == 2019 && Mes == 1) glosa = "Duplicado obtenido del SISPER";
                if (Anio == 2019 && Mes == 2) glosa = "Duplicado obtenido del SISPER";
                if (Anio == 2019 && Mes == 3) glosa = "Duplicado obtenido del SISPER";
                if (Anio == 2019 && Mes == 4) glosa = "Duplicado obtenido del SISPER";
                if (Anio == 2019 && Mes == 5) glosa = "Duplicado obtenido del SISPER";
                if (Anio == 2019 && Mes == 8) glosa = "Empleador";
                if (Anio == 2019 && Mes == 9) glosa = "Empleador";
                if (Anio == 2019 && Mes == 10) glosa = "Empleador";
                if (Anio == 2019 && Mes == 11) glosa = "Empleador";
                if (Anio == 2019 && Mes == 12) glosa = "Empleador";
                if (Anio == 2020 && Mes == 1) glosa = "Empleador";
                
                return glosa;
            }
        }
        //public byte[] Boleta { get; set; }
		
        public Grilla_Response Grilla { get; set; }
	}
}