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

namespace MVCSisRRHH.reniec.pe.gob.pide.ws5 {
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
    [System.Web.Services.WebServiceBindingAttribute(Name="ReniecConsultaDniSoap11Binding", Namespace="http://ws.reniec.gob.pe/")]
    public partial class ReniecConsultaDni : System.Web.Services.Protocols.SoapHttpClientProtocol {
        
        private System.Threading.SendOrPostCallback consultarOperationCompleted;
        
        private System.Threading.SendOrPostCallback actualizarcredencialOperationCompleted;
        
        private bool useDefaultCredentialsSetExplicitly;
        
        /// <remarks/>
        public ReniecConsultaDni() {
            this.Url = global::MVCSisRRHH.Properties.Settings.Default.MVCSisRRHH_reniec_pe_gob_pide_ws5_ReniecConsultaDni;
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
        public event consultarCompletedEventHandler consultarCompleted;
        
        /// <remarks/>
        public event actualizarcredencialCompletedEventHandler actualizarcredencialCompleted;
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("consultar", RequestNamespace="http://ws.reniec.gob.pe/", ResponseNamespace="http://ws.reniec.gob.pe/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        [return: System.Xml.Serialization.XmlElementAttribute("return", Form=System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable=true)]
        public resultadoConsulta consultar([System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable=true)] peticionConsulta arg0) {
            object[] results = this.Invoke("consultar", new object[] {
                        arg0});
            return ((resultadoConsulta)(results[0]));
        }
        
        /// <remarks/>
        public void consultarAsync(peticionConsulta arg0) {
            this.consultarAsync(arg0, null);
        }
        
        /// <remarks/>
        public void consultarAsync(peticionConsulta arg0, object userState) {
            if ((this.consultarOperationCompleted == null)) {
                this.consultarOperationCompleted = new System.Threading.SendOrPostCallback(this.OnconsultarOperationCompleted);
            }
            this.InvokeAsync("consultar", new object[] {
                        arg0}, this.consultarOperationCompleted, userState);
        }
        
        private void OnconsultarOperationCompleted(object arg) {
            if ((this.consultarCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.consultarCompleted(this, new consultarCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("actualizarcredencial", RequestNamespace="http://ws.reniec.gob.pe/", ResponseNamespace="http://ws.reniec.gob.pe/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        [return: System.Xml.Serialization.XmlElementAttribute("return", Form=System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable=true)]
        public resultadoActualizacionCredencial actualizarcredencial([System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable=true)] peticionActualizarCredencial arg0) {
            object[] results = this.Invoke("actualizarcredencial", new object[] {
                        arg0});
            return ((resultadoActualizacionCredencial)(results[0]));
        }
        
        /// <remarks/>
        public void actualizarcredencialAsync(peticionActualizarCredencial arg0) {
            this.actualizarcredencialAsync(arg0, null);
        }
        
        /// <remarks/>
        public void actualizarcredencialAsync(peticionActualizarCredencial arg0, object userState) {
            if ((this.actualizarcredencialOperationCompleted == null)) {
                this.actualizarcredencialOperationCompleted = new System.Threading.SendOrPostCallback(this.OnactualizarcredencialOperationCompleted);
            }
            this.InvokeAsync("actualizarcredencial", new object[] {
                        arg0}, this.actualizarcredencialOperationCompleted, userState);
        }
        
        private void OnactualizarcredencialOperationCompleted(object arg) {
            if ((this.actualizarcredencialCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.actualizarcredencialCompleted(this, new actualizarcredencialCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
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
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://ws.reniec.gob.pe/")]
    public partial class peticionConsulta {
        
        private string nuDniConsultaField;
        
        private string nuDniUsuarioField;
        
        private string nuRucUsuarioField;
        
        private string passwordField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string nuDniConsulta {
            get {
                return this.nuDniConsultaField;
            }
            set {
                this.nuDniConsultaField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string nuDniUsuario {
            get {
                return this.nuDniUsuarioField;
            }
            set {
                this.nuDniUsuarioField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string nuRucUsuario {
            get {
                return this.nuRucUsuarioField;
            }
            set {
                this.nuRucUsuarioField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string password {
            get {
                return this.passwordField;
            }
            set {
                this.passwordField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.9032.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://ws.reniec.gob.pe/")]
    public partial class resultadoActualizacionCredencial {
        
        private string coResultadoField;
        
        private string deResultadoField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string coResultado {
            get {
                return this.coResultadoField;
            }
            set {
                this.coResultadoField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string deResultado {
            get {
                return this.deResultadoField;
            }
            set {
                this.deResultadoField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.9032.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://ws.reniec.gob.pe/")]
    public partial class peticionActualizarCredencial {
        
        private string credencialAnteriorField;
        
        private string credencialNuevaField;
        
        private string nuDniField;
        
        private string nuRucField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string credencialAnterior {
            get {
                return this.credencialAnteriorField;
            }
            set {
                this.credencialAnteriorField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string credencialNueva {
            get {
                return this.credencialNuevaField;
            }
            set {
                this.credencialNuevaField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string nuDni {
            get {
                return this.nuDniField;
            }
            set {
                this.nuDniField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string nuRuc {
            get {
                return this.nuRucField;
            }
            set {
                this.nuRucField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.9032.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://ws.reniec.gob.pe/")]
    public partial class datosPersona {
        
        private string apPrimerField;
        
        private string apSegundoField;
        
        private string direccionField;
        
        private string estadoCivilField;
        
        private byte[] fotoField;
        
        private string prenombresField;
        
        private string restriccionField;
        
        private string ubigeoField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string apPrimer {
            get {
                return this.apPrimerField;
            }
            set {
                this.apPrimerField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string apSegundo {
            get {
                return this.apSegundoField;
            }
            set {
                this.apSegundoField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string direccion {
            get {
                return this.direccionField;
            }
            set {
                this.direccionField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string estadoCivil {
            get {
                return this.estadoCivilField;
            }
            set {
                this.estadoCivilField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, DataType="base64Binary")]
        public byte[] foto {
            get {
                return this.fotoField;
            }
            set {
                this.fotoField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string prenombres {
            get {
                return this.prenombresField;
            }
            set {
                this.prenombresField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string restriccion {
            get {
                return this.restriccionField;
            }
            set {
                this.restriccionField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string ubigeo {
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
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://ws.reniec.gob.pe/")]
    public partial class resultadoConsulta {
        
        private string coResultadoField;
        
        private datosPersona datosPersonaField;
        
        private string deResultadoField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string coResultado {
            get {
                return this.coResultadoField;
            }
            set {
                this.coResultadoField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public datosPersona datosPersona {
            get {
                return this.datosPersonaField;
            }
            set {
                this.datosPersonaField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string deResultado {
            get {
                return this.deResultadoField;
            }
            set {
                this.deResultadoField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.9032.0")]
    public delegate void consultarCompletedEventHandler(object sender, consultarCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.9032.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class consultarCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal consultarCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public resultadoConsulta Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((resultadoConsulta)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.9032.0")]
    public delegate void actualizarcredencialCompletedEventHandler(object sender, actualizarcredencialCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.9032.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class actualizarcredencialCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal actualizarcredencialCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public resultadoActualizacionCredencial Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((resultadoActualizacionCredencial)(this.results[0]));
            }
        }
    }
}

#pragma warning restore 1591