using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MIDIS.ORI.LogicaNegocio.Base
{
    public class BaseLN
    {
        /// <summary>
        /// Gets the log.
        /// </summary>
        /// <value>The log.</value>
        protected log4net.ILog Log
        {
            get
            {
                return log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().GetType());
            }
        }
    }
}
