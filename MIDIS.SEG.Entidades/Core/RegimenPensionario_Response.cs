using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIDIS.ORI.Entidades
{
    public partial class RegimenPensionario_Response
    {
        public Int32 iCodRegimenPen{ get; set; }
        public String vNombre { get; set; }
        public Boolean bEstado { get; set; }
        //
        public String sNombreRegimenPensionario { get; set; }
        public String sEstado { get; set; }

        public Grilla_Response Grilla { get; set; }

        //public string iCodRegistroRegimenPen { get; set; }
        //public string iCodRegimenPen { get; set; }
        //public string iCodTipoRegimenPen { get; set; }
        //public string iCodAFP { get; set; }
        //public string iMes { get; set; }
        //public string iAnio { get; set; }
        //public string sNombre { get; set; }
        //public string sTipo { get; set; }
        //public string dcComision { get; set; }
        //public string dcPrimaSeguro { get; set; }
        //public string dcAporte { get; set; }
        //public string dcTope { get; set; }
        //public string sEtado { get; set; }

        //public RegimenPensionario_Response(string _iCodRegistroRegimenPen, string _iCodRegimenPen, string _iCodTipoRegimenPen, string _iCodAPF, string _iMes, string _iAnio, string _sNombre,
        //    string _sTipo, string _dcComision, string _dcPrimaSeguro, string _dcAporte, string _dcTope, string _sEtado)
        //{
        //    iCodRegistroRegimenPen = _iCodRegistroRegimenPen;
        //    iCodRegimenPen = _iCodRegimenPen;
        //    iCodTipoRegimenPen = _iCodTipoRegimenPen;
        //    iCodAPF = _iCodAPF;
        //    iMes = _iMes;
        //    iAnio = _iAnio;
        //    sNombre = _sNombre;
        //    sTipo = _sTipo;
        //    dcComision = _dcComision;
        //    dcPrimaSeguro = _dcPrimaSeguro;
        //    dcAporte = _dcAporte;
        //    dcTope = _dcTope;
        //    sEtado = _sEtado;
        //}
    }
}
