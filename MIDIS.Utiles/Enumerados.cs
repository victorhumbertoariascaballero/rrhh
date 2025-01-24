using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MIDIS.Utiles
{
    class Enumerados
    {
    }

    [DataContract]
    public enum EnumOrigenUsuario
    {
        [EnumMember]
        Interno = 0,
        [EnumMember]
        Externo = 1
    }

    [DataContract]
    public enum EnumMaestras
    {
        [EnumMember]
        TIPO_DOCUMENTO = 1,
        [EnumMember]
        FORMA_DE_ENTREGA = 2,
        [EnumMember]
        SITUACION_ACCESO_INFORMACION = 3,
        [EnumMember]
        TIPO_LIBRO_RECLAMACION = 4,
        [EnumMember]
        SITUACION_RECLAMACION = 5,
        [EnumMember]
        TIPO_VIA = 6,
        [EnumMember]
        TIPO_ZONA = 7,
        [EnumMember]
        TAMAÑO_HOJA = 8,
        [EnumMember]
        ESTADO_ATENCION_SOLICITUD = 9,
        [EnumMember]
        INDICADOR_ATENCION = 10,
        [EnumMember]
        ESTADO_ATENCION_RECLAMO = 11,
        [EnumMember]
        TIPO_SOLICITUD_NOMINA = 12,
        [EnumMember]
        ESTADO_CIVIL = 13,
        [EnumMember]
        TIPO_DISCAPACIDAD = 14,
        [EnumMember]
        NIVEL_PROPUESTO = 15,
        [EnumMember]
        TITULO_GRADO_ACADEMICO = 16,
        [EnumMember]
        TIPO_ESPECIALIZACION = 17,
        [EnumMember]
        TIPO_VISITA = 18,
        [EnumMember]
        TIPO_INSTITUCION = 19,
        [EnumMember]
        SITUACION_VISITA = 20,
        [EnumMember]
        TIPO_CATETORIA_TRABAJADOR = 21,
        [EnumMember]
        MOTIVO_FERIADO = 22,
        [EnumMember]
        SEDE = 23,
        [EnumMember]
        TIPO_MEDIO_CONTACTO = 24,
        [EnumMember]
        CARGO_EMPLEADO = 25,
        [EnumMember]
        TIPO_PERSONA = 26,
        [EnumMember]
        GENERO_PERSONA = 27,
        [EnumMember]
        LUGAR_ATENCION = 28,
        [EnumMember]
        PROFESIONES = 29,
        [EnumMember]
        SITUACION_GENERALES = 30,
        [EnumMember]
        COLEGIOS_PROFESIONALES = 31,
        [EnumMember]
        SUB_SECTOR = 32,
        [EnumMember]
        ASUNTO_REUNION = 33
    }

    #region Detalles de Maestros

    [DataContract]
    public enum EnumGenero
    {
        [EnumMember]
        FEMENINO = 171,
        [EnumMember]
        MASCULINO = 170
    }


    [DataContract]
    public enum EnumTipoDocumento
    {
        [EnumMember]
        DNI = 2,
        [EnumMember]
        RUC = 5,
        [EnumMember]
        CE = 12
    }


    [DataContract]
    public enum EnumTipoReclamo
    {
        [EnumMember]
        LibroFisico = 24,
        [EnumMember]
        LibroElectronico = 25
    }

    [DataContract]
    public enum EnumEstadoReclamo
    {
        [EnumMember]
        PendienteAtencion = 28,
        [EnumMember]
        EnTramite = 29,
        [EnumMember]
        Atendido = 30,
        [EnumMember]
        Derivado = 31
    }

    [DataContract]
    public enum EnumEstadoSolicitudInformacion
    {
        [EnumMember]
        PendienteAtencion = 18,
        [EnumMember]
        EnTramite = 19,
        [EnumMember]
        Atendido = 20,
        [EnumMember]
        NoProcede = 21,
        [EnumMember]
        Derivado = 71

    }


    [DataContract]
    public enum EnumTipoVia
    {
        SinAsignar = 56
    }

    [DataContract]
    public enum EnumTipoZona
    {
        SinAsignar = 77
    }


    [DataContract]
    public enum EnumDependencia
    {
        SinAsignar = 9999
    }
    [DataContract]
    public enum EnumTipoSolicitudNomina
    {
        [EnumMember]
        INSCRIPCION = 79,
        [EnumMember]
        MODIFICACION = 80,
        [EnumMember]
        CANCELACION = 81

    }

    [DataContract]
    public enum EnumNivelPropuesto
    {
        [EnumMember]
        NIVELI = 96,
        [EnumMember]
        NIVELII = 97,
        [EnumMember]
        NIVELIII = 98,
        [EnumMember]
        SinAsignar = 107
    }

    [DataContract]
    public enum EnumTipoDiscapacidad
    {
        [EnumMember]
        FISICA = 91,
        [EnumMember]
        AUDITIVA = 92,
        [EnumMember]
        VISUAL = 93,
        [EnumMember]
        MENTAL = 94,
        [EnumMember]
        SIN_ASIGNAR = 108
    }

    [DataContract]
    public enum EnumTipoMedio
    {
        [EnumMember]
        CORREO = 143,
        [EnumMember]
        TELEFONO = 144,
        [EnumMember]
        CELULAR = 145,
        [EnumMember]
        FAX = 146,
        [EnumMember]
        PAGINA_WEB = 147
    }

    [DataContract]
    public enum EnumModulo
    {
        [EnumMember]
        Reclamo = 1,
        [EnumMember]
        Informacion = 2
    }

    [DataContract]
    public enum EnumSituacionVisita
    {
        [EnumMember]
        PENDIENTE = 116,
        [EnumMember]
        PROCESO = 117,
        [EnumMember]
        TERMINADO = 118,
        [EnumMember]
        OTRO = 119
    }

    [DataContract]
    public enum EnumTipoPersona
    {
        [EnumMember]
        NATURAL = 157,
        [EnumMember]
        JURIDICA = 158
    }


    [DataContract]
    public enum EnumTipoSituacionSolicitud
    {
        [EnumMember]
        PENDIENTE = 248,
        [EnumMember]
        APROBADO = 249,
        [EnumMember]
        RECHAZADO = 250,
        [EnumMember]
        ANULADO = 251
    }
    #endregion


    [DataContract]
    public enum EnumMaeEstadoProceso
    {
        [EnumMember]
        PENDIENTE = 1,
        [EnumMember]
        APROBADO_POR_JEFE = 2,
        [EnumMember]
        RECHAZADO_POR_JEFE = 3,
        [EnumMember]
        APROBADO_POR_ADMINISTRADOR = 4,
        [EnumMember]
        RECHAZADO_POR_ADMINISTRADOR = 5,
        [EnumMember]
        SUBSANADO = 6,
        [EnumMember]
        SUBSANADO_PARA_ADMINISTRADOR = 7
    }
}