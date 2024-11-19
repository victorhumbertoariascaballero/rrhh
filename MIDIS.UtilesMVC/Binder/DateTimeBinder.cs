using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MIDIS.UtilesMVC.Binder
{
    /// <summary>
    /// Clase que permite validar los formatos de fecha de la clase Datetime
    /// </summary>
    public class DateTimeBinder : DefaultModelBinder
    {
        private string _customFormat;

        /// <summary>
        /// Constructor que permite asigna un formato de fecha en cadena
        /// </summary>
        /// <param name="customFormat">Formato de fecha en cadena</param>
        public DateTimeBinder(string customFormat)
        {
            _customFormat = customFormat;
        }

        /// <summary>
        /// Sobrecarga del Metodo BIND que hara el mapeo de forma personalizada
        /// </summary>
        /// <param name="controllerContext">Contexto de la Controladora</param>
        /// <param name="bindingContext">Contexto del Mapeo de Datos</param>
        /// <returns></returns>
        public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            var value = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);
            if (String.IsNullOrEmpty(value.AttemptedValue))
            {
                return string.Empty;
            }
            else if (value.AttemptedValue == "null")
            {
                return String.Empty;
            }
            else if (value.AttemptedValue.Substring(0, 1) == "/")
            {
                return new DateTime(1970, 1, 1).AddTicks(long.Parse(value.AttemptedValue.Substring(6, value.AttemptedValue.Length - 8)) * 10000);
            }
            else
            {
                try
                {
                    return DateTime.Parse(Convert.ToDateTime(value.AttemptedValue).ToString());
                }
                catch (Exception)
                {
                    return string.Empty;
                }
                
                //return DateTime.ParseExact(Convert.ToDateTime(value.AttemptedValue).ToString(), _customFormat, System.Threading.Thread.CurrentThread.CurrentCulture);
                //return DateTime.ParseExact(value.AttemptedValue, _customFormat, System.Threading.Thread.CurrentThread.CurrentCulture);
            }

            //if (value.AttemptedValue == "") return string.Empty; else return DateTime.ParseExact(value.AttemptedValue, _customFormat, CultureInfo.InvariantCulture);
            //try
            //{
            //    if (value.AttemptedValue == "") return string.Empty; else return DateTime.ParseExact(value.AttemptedValue, _customFormat, CultureInfo.InvariantCulture);
            //}
            //catch (Exception)
            //{
            //    return string.Empty;
            //}
        }

    }
}