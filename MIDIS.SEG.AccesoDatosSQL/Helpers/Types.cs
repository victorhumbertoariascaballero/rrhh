using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace MIDIS.SEG.AccesoDatosSQL.Helpers
{

    /// <summary>Clase base para verificar los tipos de datos de objetos.</summary>
    /// <remarks>
    /// <list type="table">
    ///     <listheader>
    ///         <term>Accion</term>
    ///         <description>[Fecha - Responsable - Motivo]</description>
    ///     </listheader>
    ///     <item>
    ///         <term>Creación</term>
    ///         <description>02/05/2010 - Yual Alfredo Pino Acevedo - Implementación de la clase</description>
    ///     </item>
    /// </list>
    /// </remarks>
    public sealed class Types
    {
        #region Métodos Públicos

        /// <summary>Devuelve una cadena de texto de un objeto.</summary>
        /// <param name="vobjValor">Objeto a ser evaluado.</param>
        /// <returns>Cadena de texto</returns>
        /// <remarks>
        /// <list type="table">
        ///     <listheader>
        ///         <term>Accion</term>
        ///         <description>[Fecha - Responsable - Motivo]</description>
        ///     </listheader>
        ///     <item>
        ///         <term>Creación</term>
        ///         <description>29/05/2010 - Yual Alfredo Pino Acevedo - Implementación del método</description>
        ///     </item>
        /// </list>
        /// </remarks>
        public static string CheckString(System.Object vobjValor)
        {
            if ((vobjValor != null) & (!System.Object.ReferenceEquals(vobjValor, System.DBNull.Value)))
            {
                return Convert.ToString(vobjValor).Trim();
            }
            else
            {
                return "";
            }
        }

        /// <summary>Devuelve un entero de 32 bits con signo de un objeto.</summary>
        /// <param name="vobjValor">Objeto a ser evaluado.</param>
        /// <returns>Entero de 32 bits con signo.</returns>
        /// <remarks>
        /// <list type="table">
        ///     <listheader>
        ///         <term>Accion</term>
        ///         <description>[Fecha - Responsable - Motivo]</description>
        ///     </listheader>
        ///     <item>
        ///         <term>Creación</term>
        ///         <description>29/05/2010 - Yual Alfredo Pino Acevedo - Implementación del método</description>
        ///     </item>
        /// </list>
        /// </remarks>
        public static Int64 CheckInt64(System.Object vobjValor)
        {
            if ((vobjValor != null) & (!System.Object.ReferenceEquals(vobjValor, System.DBNull.Value)))
            {
                if (IsNumeric(vobjValor))
                {
                    return Convert.ToInt64(vobjValor);
                }
                else
                {
                    return 0;
                }
            }
            else
            {
                return 0;
            }
        }

        /// <summary>Devuelve un entero de 32 bits con signo de un objeto.</summary>
        /// <param name="vobjValor">Objeto a ser evaluado.</param>
        /// <returns>Entero de 32 bits con signo.</returns>
        /// <remarks>
        /// <list type="table">
        ///     <listheader>
        ///         <term>Accion</term>
        ///         <description>[Fecha - Responsable - Motivo]</description>
        ///     </listheader>
        ///     <item>
        ///         <term>Creación</term>
        ///         <description>29/05/2010 - Yual Alfredo Pino Acevedo - Implementación del método</description>
        ///     </item>
        /// </list>
        /// </remarks>
        public static int CheckInt32(System.Object vobjValor)
        {
            if ((vobjValor != null) & (!System.Object.ReferenceEquals(vobjValor, System.DBNull.Value)))
            {
                if (IsNumeric(vobjValor))
                {
                    return Convert.ToInt32(vobjValor);
                }
                else
                {
                    return 0;
                }
            }
            else 
            {
                return 0;
            }
        }

        /// <summary>Devuelve un entero de 16 bits con signo de un objeto.</summary>
        /// <param name="vobjValor">Objeto a ser evaluado.</param>
        /// <returns>Entero de 16 bits con signo.</returns>
        /// <remarks>
        /// <list type="table">
        ///     <listheader>
        ///         <term>Accion</term>
        ///         <description>[Fecha - Responsable - Motivo]</description>
        ///     </listheader>
        ///     <item>
        ///         <term>Creación</term>
        ///         <description>29/05/2010 - Yual Alfredo Pino Acevedo - Implementación del método</description>
        ///     </item>
        /// </list>
        /// </remarks>
        public static Int16 CheckInt16(System.Object vobjValor)
        {
            if ((vobjValor != null) & (!System.Object.ReferenceEquals(vobjValor, System.DBNull.Value)))
            {
                if (IsNumeric(vobjValor))
                {
                    return Convert.ToInt16(vobjValor);
                }
                else
                {
                    return 0;
                }
            }
            else
            {
                return 0;
            }
        }

        /// <summary>Devuelve un valor booleano de un objeto.</summary>
        /// <param name="vobjValor">Objeto a ser evaluado.</param>
        /// <returns>Valor booleano.</returns>
        /// <remarks>
        /// <list type="table">
        ///     <listheader>
        ///         <term>Accion</term>
        ///         <description>[Fecha - Responsable - Motivo]</description>
        ///     </listheader>
        ///     <item>
        ///         <term>Creación</term>
        ///         <description>29/05/2010 - Yual Alfredo Pino Acevedo - Implementación del método</description>
        ///     </item>
        /// </list>
        /// </remarks>
        public static bool CheckBoolean(System.Object vobjValor)
        { 
            if ((vobjValor != null) & (!System.Object.ReferenceEquals(vobjValor,System.DBNull.Value)))
            {               
                return BoolParser.GetValue(vobjValor.ToString());
            }
            else
            {
                return false;
            }
        }

        /// <summary>Devuelve un número de punto flotante de precisión doble de un objeto.</summary>
        /// <param name="vobjValor">Objeto a ser evaluado.</param>
        /// <returns>Número de punto flotante de precisión doble.</returns>
        /// <remarks>
        /// <list type="table">
        ///     <listheader>
        ///         <term>Accion</term>
        ///         <description>[Fecha - Responsable - Motivo]</description>
        ///     </listheader>
        ///     <item>
        ///         <term>Creación</term>
        ///         <description>29/05/2010 - Yual Alfredo Pino Acevedo - Implementación del método</description>
        ///     </item>
        /// </list>
        /// </remarks>
        public static double CheckDouble(System.Object vobjValor)
        {
            if ((vobjValor != null) & (!System.Object.ReferenceEquals(vobjValor,System.DBNull.Value)))
            {
                if (IsDecimal(vobjValor))
                {
                    return Convert.ToDouble(vobjValor);
                }
                else
                {
                    return 0;
                }
            }
            else
            {
                return 0;
            }
        }

        /// <summary>Devuelve un número de punto flotante de precisión doble de un objeto.</summary>
        /// <param name="vobjValor">Objeto a ser evaluado.</param>
        /// <returns>Número de punto flotante de precisión doble.</returns>
        /// <remarks>
        /// <list type="table">
        ///     <listheader>
        ///         <term>Accion</term>
        ///         <description>[Fecha - Responsable - Motivo]</description>
        ///     </listheader>
        ///     <item>
        ///         <term>Creación</term>
        ///         <description>29/05/2010 - Yual Alfredo Pino Acevedo - Implementación del método</description>
        ///     </item>
        /// </list>
        /// </remarks>
        public static decimal CheckDecimal(System.Object vobjValor)
        {
            if ((vobjValor != null) & (!System.Object.ReferenceEquals(vobjValor, System.DBNull.Value)))
            {
                if (IsDecimal(vobjValor))
                {
                    return Convert.ToDecimal(vobjValor);
                }
                else
                {
                    return 0;
                }
            }
            else
            {
                return 0;
            }
        }

        /// <summary>Devuelve el valor de un objeto verificado si es nulo o no.</summary>
        /// <remarks>
        /// <list type="table">
        ///     <listheader>
        ///         <term>Accion</term>
        ///         <description>[Fecha - Responsable - Motivo]</description>
        ///     </listheader>
        ///     <item>
        ///         <term>Creación</term>
        ///         <description>14/04/2014 - Yual Alfredo Pino Acevedo - Implementación del método</description>
        ///     </item>
        /// </list>
        /// </remarks>
        public static Nullable<T> CheckNullValue<T>(object dbValue) where T : struct
        {
            Nullable<T> returnValue = null;

            if ((dbValue != null) && (dbValue != DBNull.Value))
            {
                returnValue = (T)dbValue;
            }

            return returnValue;
        }

        /// <summary>Devuelve el valor de un objeto verificado si es nulo o no.</summary>
        /// <remarks>
        /// <list type="table">
        ///     <listheader>
        ///         <term>Accion</term>
        ///         <description>[Fecha - Responsable - Motivo]</description>
        ///     </listheader>
        ///     <item>
        ///         <term>Creación</term>
        ///         <description>14/04/2014 - Yual Alfredo Pino Acevedo - Implementación del método</description>
        ///     </item>
        /// </list>
        /// </remarks>
        public static T CheckDefaultValue<T>(object obj)
        {
            if (obj == null || obj == DBNull.Value) return default(T);
            else { return (T)obj; }
        }

        /// <summary>Verifica si el objeto es númerico.</summary>
        /// <param name="vobjValor">Objeto a ser evaluado.</param>
        /// <returns>True si es númerico; en caso contrario False.</returns>
        /// <remarks>
        /// <list type="table">
        ///     <listheader>
        ///         <term>Accion</term>
        ///         <description>[Fecha - Responsable - Motivo]</description>
        ///     </listheader>
        ///     <item>
        ///         <term>Creación</term>
        ///         <description>06/03/2010 - Yual Alfredo Pino Acevedo - Implementación del método</description>
        ///     </item>
        /// </list>
        /// </remarks>
        public static bool IsNumeric(object vobjValor)
        {
            return Regex.IsMatch(vobjValor.ToString(), "^\\d+$");
        }

        /// <summary>Verifica si el objeto es decimal.</summary>
        /// <param name="vobjValor">Objeto a ser evaluado.</param>
        /// <returns>True si es decimal; en caso contrario False.</returns>
        /// <remarks>
        /// <list type="table">
        ///     <listheader>
        ///         <term>Accion</term>
        ///         <description>[Fecha - Responsable - Motivo]</description>
        ///     </listheader>
        ///     <item>
        ///         <term>Creación</term>
        ///         <description>06/03/2010 - Yual Alfredo Pino Acevedo - Implementación del método</description>
        ///     </item>
        /// </list>
        /// </remarks>
        public static bool IsDecimal(object vobjValor)
        {
            return Regex.IsMatch(vobjValor.ToString(), @"^\d+(\.\d+)?$");
        }
        #endregion

        #region Eventos
        /// <summary>Permite la instancia de la clase DataTypes.</summary>
        /// <remarks>
        /// <list type="table">
        ///     <listheader>
        ///         <term>Accion</term>
        ///         <description>[Fecha - Responsable - Motivo]</description>
        ///     </listheader>
        ///     <item>
        ///         <term>Creación</term>
        ///         <description>02/05/2010 - Yual Alfredo Pino Acevedo - Implementación del método.</description>
        ///     </item>
        /// </list>
        /// </remarks>
        public Types() 
            : base()
        {
        }
        #endregion
    }

    /// <summary>
    /// Parse strings into true or false bools using relaxed parsing rules
    /// </summary>
    public sealed class BoolParser
    {
        /// <summary>
        /// Get the boolean value for this string
        /// </summary>
        public static bool GetValue(string value)
        {
            return IsTrue(value);
        }

        /// <summary>
        /// Determine whether the string is not True
        /// </summary>
        public static bool IsFalse(string value)
        {
            return !IsTrue(value);
        }

        /// <summary>
        /// Determine whether the string is equal to True
        /// </summary>
        public static bool IsTrue(string value)
        {
            try
            {
                // 1
                // Avoid exceptions
                if (value == null)
                {
                    return false;
                }

                // 2
                // Remove whitespace from string
                value = value.Trim();

                // 3
                // Lowercase the string
                value = value.ToLower();

                // 4
                // Check for word true
                if (value == "true")
                {
                    return true;
                }

                // 5
                // Check for letter true
                if (value == "t")
                {
                    return true;
                }

                // 6
                // Check for one
                if (value == "1")
                {
                    return true;
                }

                // 7
                // Check for word yes
                if (value == "yes")
                {
                    return true;
                }

                // 8
                // Check for letter yes
                if (value == "y")
                {
                    return true;
                }

                // 9
                // It is false
                return false;
            }
            catch
            {
                return false;
            }
        }
    }
}
