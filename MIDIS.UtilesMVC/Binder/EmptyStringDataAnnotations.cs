using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace MIDIS.UtilesMVC.Binder
{
    /// <summary>
    /// Metadata Aplicada a las cadenas vacias
    /// Evita que lo convierta a NULL
    /// </summary>
    public class EmptyStringDataAnnotations : DataAnnotationsModelMetadataProvider
    {
        /// <summary>
        /// Crea la metadata general y le agrega el atributo ConvertEmptyStringToNull 
        /// para no convertir la cadena vacia en null
        /// </summary>
        /// <param name="attributes"></param>
        /// <param name="containerType"></param>
        /// <param name="modelAccessor"></param>
        /// <param name="modelType"></param>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        protected override ModelMetadata CreateMetadata(IEnumerable<Attribute> attributes,
            Type containerType, Func<object> modelAccessor, Type modelType, string propertyName)
        {
            var modelMetadata = base.CreateMetadata(attributes, containerType, modelAccessor, modelType, propertyName);
            if (modelType == typeof(string))
            {
                modelMetadata.ConvertEmptyStringToNull = false;
            }

            return modelMetadata;
        }
    }
}
