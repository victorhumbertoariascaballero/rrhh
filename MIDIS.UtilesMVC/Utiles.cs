using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace MIDIS.UtilesMVC
{
    public static class Utiles
    {
        public static Dictionary<string, object> GetErrorsFromModelState(ModelStateDictionary modelStateDictionary)
        {
            var errors = new Dictionary<string, object>();
            foreach (var key in modelStateDictionary.Keys)
            {
                // Only send the errors to the client.
                if (modelStateDictionary[key].Errors.Count > 0)
                {
                    errors[key] = modelStateDictionary[key].Errors;
                }
            }

            return errors;
        }
    }
}
