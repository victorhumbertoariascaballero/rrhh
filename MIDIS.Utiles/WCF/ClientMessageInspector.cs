using System;
using System.ServiceModel.Dispatcher;
using System.Globalization;
using System.ServiceModel;
using System.ServiceModel.Channels;

namespace MIDIS.Utiles.WCF
{
    internal class ClientMessageInspector : IClientMessageInspector
    {

        #region IClientMessageInspector Members

        public void AfterReceiveReply(ref Message reply, object correlationState)
        {

        }

        public object BeforeSendRequest(ref Message request, IClientChannel channel)
        {
            MessageHeader<string> culture = new MessageHeader<string>(CultureInfo.CurrentCulture.Name);
            MessageHeader untyped = culture.GetUntypedHeader("CultureInfo", "ci");
            request.Headers.Add(untyped);

            return Guid.NewGuid();
        }

        #endregion
    }
}
