using System;
using System.Xml;
using System.Collections.Generic;
using System.IdentityModel.Protocols.WSTrust;
using System.Linq;
using System.Net.Security;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading.Tasks;

namespace MIDIS.Autenticacion
{
    [ServiceContract]
    public interface IWSTrustFeb2005Contract
    {
        [OperationContract(ProtectionLevel = ProtectionLevel.EncryptAndSign, Action = "http://schemas.xmlsoap.org/ws/2005/02/trust/RST/Issue", ReplyAction = "http://schemas.xmlsoap.org/ws/2005/02/trust/RSTR/Issue", AsyncPattern = true)]
        IAsyncResult BeginIssue(System.ServiceModel.Channels.Message request, AsyncCallback callback, object state);
        System.ServiceModel.Channels.Message EndIssue(IAsyncResult asyncResult);
    }

    public partial class WSTrustFeb2005ContractClient : ClientBase<IWSTrustFeb2005Contract>, IWSTrustFeb2005Contract
    {
        public WSTrustFeb2005ContractClient(Binding binding, EndpointAddress remoteAddress)
            : base(binding, remoteAddress)
        {
        }

        public IAsyncResult BeginIssue(Message request, AsyncCallback callback, object state)
        {
            return base.Channel.BeginIssue(request, callback, state);
        }

        public Message EndIssue(IAsyncResult asyncResult)
        {
            return base.Channel.EndIssue(asyncResult);
        }
    }

    class RequestBodyWriter : BodyWriter
    {
        WSTrustRequestSerializer _serializer;
        RequestSecurityToken _rst;

        public RequestBodyWriter(WSTrustRequestSerializer serializer, RequestSecurityToken rst)
            : base(false)
        {
            if (serializer == null)
                throw new ArgumentNullException("serializer");

            this._serializer = serializer;
            this._rst = rst;
        }


        protected override void OnWriteBodyContents(XmlDictionaryWriter writer)
        {
            _serializer.WriteXml(_rst, writer, new WSTrustSerializationContext());
        }
    }
}
