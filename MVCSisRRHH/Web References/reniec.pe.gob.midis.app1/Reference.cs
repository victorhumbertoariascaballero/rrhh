﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Este código fue generado por una herramienta.
//     Versión de runtime:4.0.30319.42000
//
//     Los cambios en este archivo podrían causar un comportamiento incorrecto y se perderán si
//     se vuelve a generar el código.
// </auto-generated>
//------------------------------------------------------------------------------

// 
// Microsoft.VSDesigner generó automáticamente este código fuente, versión=4.0.30319.42000.
// 
#pragma warning disable 1591

namespace MVCSisRRHH.reniec.pe.gob.midis.app1 {
    using System.Diagnostics;
    using System;
    using System.Xml.Serialization;
    using System.ComponentModel;
    using System.Web.Services.Protocols;
    using System.Web.Services;
    
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.9032.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Web.Services.WebServiceBindingAttribute(Name="BasicHttpBinding_IReniecPersonaFoto_Servicio", Namespace="http://tempuri.org/")]
    public partial class ReniecPersonaFoto_Servicio : System.Web.Services.Protocols.SoapHttpClientProtocol {
        
        private System.Threading.SendOrPostCallback ConsultarPorNumeroDeDocumentoOperationCompleted;
        
        private bool useDefaultCredentialsSetExplicitly;
        
        /// <remarks/>
        public ReniecPersonaFoto_Servicio() {
            this.Url = global::MVCSisRRHH.Properties.Settings.Default.MVCSisRRHH_reniec_pe_gob_midis_app1_ReniecPersonaFoto_Servicio;
            if ((this.IsLocalFileSystemWebService(this.Url) == true)) {
                this.UseDefaultCredentials = true;
                this.useDefaultCredentialsSetExplicitly = false;
            }
            else {
                this.useDefaultCredentialsSetExplicitly = true;
            }
        }
        
        public new string Url {
            get {
                return base.Url;
            }
            set {
                if ((((this.IsLocalFileSystemWebService(base.Url) == true) 
                            && (this.useDefaultCredentialsSetExplicitly == false)) 
                            && (this.IsLocalFileSystemWebService(value) == false))) {
                    base.UseDefaultCredentials = false;
                }
                base.Url = value;
            }
        }
        
        public new bool UseDefaultCredentials {
            get {
                return base.UseDefaultCredentials;
            }
            set {
                base.UseDefaultCredentials = value;
                this.useDefaultCredentialsSetExplicitly = true;
            }
        }
        
        /// <remarks/>
        public event ConsultarPorNumeroDeDocumentoCompletedEventHandler ConsultarPorNumeroDeDocumentoCompleted;
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/IReniecPersonaFoto_Servicio/ConsultarPorNumeroDeDocumento", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        [return: System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public ReniecPersonaFoto_Response ConsultarPorNumeroDeDocumento([System.Xml.Serialization.XmlElementAttribute(IsNullable=true)] ReniecPersonaFoto_Request peticion) {
            object[] results = this.Invoke("ConsultarPorNumeroDeDocumento", new object[] {
                        peticion});
            return ((ReniecPersonaFoto_Response)(results[0]));
        }
        
        /// <remarks/>
        public void ConsultarPorNumeroDeDocumentoAsync(ReniecPersonaFoto_Request peticion) {
            this.ConsultarPorNumeroDeDocumentoAsync(peticion, null);
        }
        
        /// <remarks/>
        public void ConsultarPorNumeroDeDocumentoAsync(ReniecPersonaFoto_Request peticion, object userState) {
            if ((this.ConsultarPorNumeroDeDocumentoOperationCompleted == null)) {
                this.ConsultarPorNumeroDeDocumentoOperationCompleted = new System.Threading.SendOrPostCallback(this.OnConsultarPorNumeroDeDocumentoOperationCompleted);
            }
            this.InvokeAsync("ConsultarPorNumeroDeDocumento", new object[] {
                        peticion}, this.ConsultarPorNumeroDeDocumentoOperationCompleted, userState);
        }
        
        private void OnConsultarPorNumeroDeDocumentoOperationCompleted(object arg) {
            if ((this.ConsultarPorNumeroDeDocumentoCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.ConsultarPorNumeroDeDocumentoCompleted(this, new ConsultarPorNumeroDeDocumentoCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        public new void CancelAsync(object userState) {
            base.CancelAsync(userState);
        }
        
        private bool IsLocalFileSystemWebService(string url) {
            if (((url == null) 
                        || (url == string.Empty))) {
                return false;
            }
            System.Uri wsUri = new System.Uri(url);
            if (((wsUri.Port >= 1024) 
                        && (string.Compare(wsUri.Host, "localHost", System.StringComparison.OrdinalIgnoreCase) == 0))) {
                return true;
            }
            return false;
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.9032.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://schemas.datacontract.org/2004/07/APP.GSWEE.Entidades")]
    public partial class ReniecPersonaFoto_Request {
        
        private string claveField;
        
        private string numeroDeDocumentoField;
        
        private string usuarioField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public string Clave {
            get {
                return this.claveField;
            }
            set {
                this.claveField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public string NumeroDeDocumento {
            get {
                return this.numeroDeDocumentoField;
            }
            set {
                this.numeroDeDocumentoField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public string Usuario {
            get {
                return this.usuarioField;
            }
            set {
                this.usuarioField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.9032.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://schemas.datacontract.org/2004/07/APP.GSWEE.Entidades")]
    public partial class ReniecPersonaFoto_Registro {
        
        private string direccionField;
        
        private string estadoCivilField;
        
        private byte[] fotoField;
        
        private string preNombresField;
        
        private string primerApellidoField;
        
        private string restriccionField;
        
        private string segundoApellidoField;
        
        private string ubigeoField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public string Direccion {
            get {
                return this.direccionField;
            }
            set {
                this.direccionField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public string EstadoCivil {
            get {
                return this.estadoCivilField;
            }
            set {
                this.estadoCivilField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType="base64Binary", IsNullable=true)]
        public byte[] Foto {
            get {
                return this.fotoField;
            }
            set {
                this.fotoField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public string PreNombres {
            get {
                return this.preNombresField;
            }
            set {
                this.preNombresField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public string PrimerApellido {
            get {
                return this.primerApellidoField;
            }
            set {
                this.primerApellidoField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public string Restriccion {
            get {
                return this.restriccionField;
            }
            set {
                this.restriccionField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public string SegundoApellido {
            get {
                return this.segundoApellidoField;
            }
            set {
                this.segundoApellidoField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public string Ubigeo {
            get {
                return this.ubigeoField;
            }
            set {
                this.ubigeoField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.9032.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://schemas.datacontract.org/2004/07/APP.GSWEE.Entidades")]
    public partial class ReniecPersonaFoto_Response {
        
        private string codigoField;
        
        private string mensajeField;
        
        private ReniecPersonaFoto_Registro personaFotoDatoField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public string Codigo {
            get {
                return this.codigoField;
            }
            set {
                this.codigoField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public string Mensaje {
            get {
                return this.mensajeField;
            }
            set {
                this.mensajeField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public ReniecPersonaFoto_Registro PersonaFotoDato {
            get {
                return this.personaFotoDatoField;
            }
            set {
                this.personaFotoDatoField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.9032.0")]
    public delegate void ConsultarPorNumeroDeDocumentoCompletedEventHandler(object sender, ConsultarPorNumeroDeDocumentoCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.9032.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class ConsultarPorNumeroDeDocumentoCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal ConsultarPorNumeroDeDocumentoCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public ReniecPersonaFoto_Response Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((ReniecPersonaFoto_Response)(this.results[0]));
            }
        }
    }
}

#pragma warning restore 1591