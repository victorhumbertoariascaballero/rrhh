using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Mvc.Html;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace MVCClienteSeguridad.Helpers
{
    public enum TipoLabel
    {
        LadoSuperior,
        Normal,
        Marcado
    }
    public enum TipoInput
    {
        Obligatorio,
        Error,
        Deshabilitado,
        Normal
    }

    public enum AnchoInput
    {
        Corto,
        Medio,
        Largo
    }

    public enum TipoLink
    {
        Editar,
        Nuevo,
        Detalle,
        Eliminar,
        Normal,
        Buscar,
        Versionar,
        Configuracion,
        Editar_Span
    }

    public enum TipoIcono
    {
        Buscar,
        Guardar
    }

    public static class UtilHelper
    {
        /// <summary>
        /// Paginador que retornas una seccion aplicada a una grilla
        /// </summary>
        /// <param name="helper">Helper a extender</param>
        /// <param name="cantRegxPag">Nro de Registros por Pagina</param>
        /// <param name="totalVirtual">Total de Paginas</param>
        /// <param name="pagActual">Nro de pagina Actual</param>
        /// <param name="id">id para identificar al paginador</param>
        /// <param name="totalTexto">Texto a mostrar en la seccion de total</param>
        /// <param name="paginaTexto">Texto a mostrar en la seccion de paginado</param>
        /// <returns></returns>
        public static MvcHtmlString MTCPaginador(this HtmlHelper helper, int cantRegxPag, int totalVirtual, int pagActual, string id = "paginacion_grilla", string totalTexto = "", string paginaTexto = "", string paginas = "5,10,20,100")
        {
            var wrapper = new TagBuilder("div");
            //wrapper.Attributes.Add("id", "paginacion_grilla");
            wrapper.AddCssClass("row form-horizontal");
                                                                                                                                    
            #region Campos Ocultos

            var inputHidePag = new TagBuilder("input");
            inputHidePag.Attributes.Add("value", pagActual.ToString());
            inputHidePag.Attributes.Add("type", "hidden");
            inputHidePag.Attributes.Add("name", "hdPag_" + id);
            inputHidePag.Attributes.Add("id", "hdPag_" + id);

            wrapper.InnerHtml += inputHidePag.ToString();

            var inputHideTotal = new TagBuilder("input");
            inputHideTotal.Attributes.Add("value", totalVirtual.ToString());
            inputHideTotal.Attributes.Add("type", "hidden");
            inputHideTotal.Attributes.Add("name", "hdTotal_" + id);
            inputHideTotal.Attributes.Add("id", "hdTotal_" + id);

            wrapper.InnerHtml += inputHideTotal.ToString(); 

            var inputHideRegXPag = new TagBuilder("input");
            inputHideRegXPag.Attributes.Add("value", cantRegxPag.ToString());
            inputHideRegXPag.Attributes.Add("type", "hidden");
            inputHideRegXPag.Attributes.Add("name", "hdRegxPag_" + id);
            inputHideRegXPag.Attributes.Add("id", "hdRegxPag_" + id);

            wrapper.InnerHtml += inputHideRegXPag.ToString();

            #endregion


            #region Grupo Total Registros

            var divNroRegistros = new TagBuilder("div");
            divNroRegistros.AddCssClass("col-md-2");
            var labelRegistros = new TagBuilder("label");
            labelRegistros.AddCssClass("control-label small");
            if (!string.IsNullOrEmpty(totalTexto))
            {
                labelRegistros.InnerHtml = totalTexto;
            }
            else
            {
                labelRegistros.InnerHtml = "Total de Registros: ";
            }


            var strongRegistros = new TagBuilder("strong");
            strongRegistros.InnerHtml = totalVirtual.ToString();
            labelRegistros.InnerHtml += strongRegistros.ToString();

            divNroRegistros.InnerHtml += labelRegistros.ToString();

            wrapper.InnerHtml += divNroRegistros.ToString();
            #endregion

            #region Numero de Paginas

            var divNroPaginas = new TagBuilder("div");
            divNroPaginas.AddCssClass("col-md-4");

            #region Texto Paginador
            var divCol5Paginas = new TagBuilder("div");
            divCol5Paginas.AddCssClass("col-md-5 text-right");
            var divLabel5Paginas = new TagBuilder("label");
            divLabel5Paginas.AddCssClass("control-label small");
            if (!string.IsNullOrEmpty(paginaTexto))
            {
                divLabel5Paginas.InnerHtml = paginaTexto;
            }
            else
            {
                divLabel5Paginas.InnerHtml = "Mostrar de ";
            }
            
            divCol5Paginas.InnerHtml += divLabel5Paginas.ToString();
            divNroPaginas.InnerHtml += divCol5Paginas.ToString();
            #endregion

            #region Combo paginador
            var divCol7Paginas = new TagBuilder("div");
            divCol7Paginas.AddCssClass("col-md-7");

            var selectPagina = new TagBuilder("select");
            selectPagina.AddCssClass("selectpicker");
            selectPagina.Attributes.Add("data-width", "100%");
            selectPagina.Attributes.Add("id", "select" + id);

            string[] listaPaginas = paginas.Split(',');

            foreach (string item in listaPaginas)
            {
                var opcion = new TagBuilder("option");
                opcion.Attributes.Add("value", item);
                opcion.InnerHtml = item + " en " + item;
                selectPagina.InnerHtml += opcion.ToString();
            }
            /*
            var opcion1 = new TagBuilder("option");
            opcion1.Attributes.Add("value", "5");
            opcion1.InnerHtml = "5 en 5";
            selectPagina.InnerHtml += opcion1.ToString();

            var opcion2 = new TagBuilder("option");
            opcion2.Attributes.Add("value", "10");
            opcion2.InnerHtml = "10 en 10";
            selectPagina.InnerHtml += opcion2.ToString();

            var opcion3 = new TagBuilder("option");
            opcion3.Attributes.Add("value", "20");
            opcion3.InnerHtml = "20 en 20";
            selectPagina.InnerHtml += opcion3.ToString();

            var opcion4 = new TagBuilder("option");
            opcion4.Attributes.Add("value", "50");
            opcion4.InnerHtml = "50 en 50";
            selectPagina.InnerHtml += opcion4.ToString();
            */
            divCol7Paginas.InnerHtml += selectPagina.ToString();

            divNroPaginas.InnerHtml += divCol7Paginas.ToString();

            #endregion


            wrapper.InnerHtml += divNroPaginas.ToString();


            #endregion

            #region Paginado

            var divPaginado = new TagBuilder("div");
            divPaginado.AddCssClass("col-md-6 pagination-conteiner");

            var divPagList = new TagBuilder("div");
            //divPagList.Attributes.Add("class", "paglist");
            divPagList.AddCssClass("paginationDHH");    
            divPagList.Attributes.Add("id", id);

            //var divSlider = new TagBuilder("div");
            ////divSlider.Attributes.Add("id", "pagination-slider");
            //divSlider.Attributes.Add("id", id);

            //divPagList.InnerHtml = divSlider.ToString();
            divPaginado.InnerHtml += divPagList.ToString();

            wrapper.InnerHtml += divPaginado.ToString();
            #endregion

            return new MvcHtmlString(wrapper.ToString());
        }

        public static MvcHtmlString MTCPaginadorex(this HtmlHelper helper, int cantRegxPag, int totalVirtual, int pagActual, string id = "paginacion_grilla")
        {
            var wrapper = new TagBuilder("div");

            var inputHidePag = new TagBuilder("input");                                     
            inputHidePag.Attributes.Add("value", pagActual.ToString());
            inputHidePag.Attributes.Add("type", "hidden");
            inputHidePag.Attributes.Add("name", "hdPag");
            inputHidePag.Attributes.Add("id", "hdPag" + id);

            wrapper.InnerHtml += inputHidePag.ToString();

            var inputHideTotal = new TagBuilder("input");
            inputHideTotal.Attributes.Add("value", totalVirtual.ToString());
            inputHideTotal.Attributes.Add("type", "hidden");
            inputHideTotal.Attributes.Add("name", "hdTotal");
            inputHideTotal.Attributes.Add("id", "hdTotal" + id);

            wrapper.InnerHtml += inputHideTotal.ToString();

            var inputHideRegXPag = new TagBuilder("input");
            inputHideRegXPag.Attributes.Add("value", cantRegxPag.ToString());
            inputHideRegXPag.Attributes.Add("type", "hidden");
            inputHideRegXPag.Attributes.Add("name", "hdRegxPag");
            inputHideRegXPag.Attributes.Add("id", "hdRegxPag" + id);

            wrapper.InnerHtml += inputHideRegXPag.ToString();

            var divContainer = new TagBuilder("div");
            divContainer.Attributes.Add("class", "pagination-conteiner");

            var divPagList = new TagBuilder("div");
            //divPagList.Attributes.Add("class", "paginacion_grilla");
            divPagList.Attributes.Add("class", "paglist");

            var divSlider = new TagBuilder("div");
            //divSlider.Attributes.Add("id", "pagination-slider");
            divSlider.Attributes.Add("id", id);

            divPagList.InnerHtml = divSlider.ToString();
            //divContainer.InnerHtml = divPagList.ToString();

            var divNomList = new TagBuilder("div");
            divNomList.Attributes.Add("class", "nomlist");

            var negrita = new TagBuilder("b");

            var spanTotal = new TagBuilder("span");
            spanTotal.Attributes.Add("class", "total-registros");
            spanTotal.InnerHtml = "Total de registros: " + totalVirtual.ToString();

            negrita.InnerHtml = spanTotal.ToString();
            divNomList.InnerHtml = negrita.ToString();

            divContainer.InnerHtml = divPagList.ToString() + divNomList.ToString();

            wrapper.InnerHtml += divContainer.ToString();

            return new MvcHtmlString(wrapper.ToString());
        }

        public static MvcHtmlString MTCUbicaPagina(this HtmlHelper helper, string[] ubicaciones)
        {
            var olPrincipal = new TagBuilder("ol");
            olPrincipal.AddCssClass("breadcrumb");

            if (ubicaciones != null && ubicaciones.Length >= 0)
            {
                #region Primera Seccion

                var liPrimero = new TagBuilder("li");
                var aPrimero = new TagBuilder("a");
                aPrimero.Attributes.Add("href", "#");
                var iPrimero = new TagBuilder("i");
                iPrimero.AddCssClass("fa fa-folder-o");
                aPrimero.InnerHtml += iPrimero.ToString();
                aPrimero.InnerHtml += ubicaciones[0];
                liPrimero.InnerHtml += aPrimero.ToString();
                olPrincipal.InnerHtml += liPrimero.ToString();
                #endregion

                if (ubicaciones.Length >= 1)
                {
                    #region Segunda Seccion

                    var liSegundo = new TagBuilder("li");
                    var aSegundo = new TagBuilder("a");
                    aSegundo.Attributes.Add("href", "#");
                    var iSegundo = new TagBuilder("i");
                    iSegundo.AddCssClass("fa fa-arrow-circle-right");
                    aSegundo.InnerHtml += iSegundo.ToString();
                    aSegundo.InnerHtml += ubicaciones[1];
                    liSegundo.InnerHtml += aSegundo.ToString();

                    olPrincipal.InnerHtml += liSegundo.ToString();

                    #endregion
                    if (ubicaciones.Length > 1)
                    {
                        #region Resto de Regiones

                        for (int i = 2; i < ubicaciones.Length; i++)
                        {
                            var liDetalle = new TagBuilder("li");
                            liDetalle.AddCssClass("active");
                            var iDetalle = new TagBuilder("i");
                            iDetalle.AddCssClass("fa fa-angle-double-right");
                            liDetalle.InnerHtml += iDetalle.ToString();
                            liDetalle.InnerHtml += ubicaciones[i];
                            olPrincipal.InnerHtml += liDetalle.ToString();
                        }

                        #endregion
                    }
                }
            }
            return new MvcHtmlString(olPrincipal.ToString());

        }

        public static void LlenarValores(TagBuilder tagBuilder, object htmlAttributes)
        {
            var x = htmlAttributes.ToString();
            x = x.Replace("{", "").Replace("}", "").Replace(" ", "");
            var y = x.Split(',');
            for (int i = 0; i < y.Length; i++)
            {
                if (y[i].Split('=')[0] == "class")
                {
                    tagBuilder.AddCssClass(y[i].Split('=')[1]);
                }
                else
                {
                    tagBuilder.Attributes.Add(y[i].Split('=')[0], y[i].Split('=')[1]);
                }
            }

        }

        public static MvcHtmlString MTCActionLink(this HtmlHelper helper, string accion, string controlador, TipoLink tipo = TipoLink.Editar_Span, object routeValues = null, object htmlAttributes = null, string toolTip = "")
        {
            var accionActual = helper.ViewContext.RouteData.GetRequiredString("action");
            var controladorActual = helper.ViewContext.RouteData.GetRequiredString("controller");

            var spanText = new TagBuilder("span");
            spanText.AddCssClass(ObtenerClaseLink(tipo));

            var urlHelper = new UrlHelper(helper.ViewContext.RequestContext);
            var link = new TagBuilder("a") { InnerHtml = spanText.ToString() };
            if (string.IsNullOrEmpty(accion)) accion = accionActual;
            if (string.IsNullOrEmpty(controlador)) controlador = controladorActual;

            link.Attributes["href"] = urlHelper.Action(accion, controlador, routeValues);
            link.Attributes["title"] = toolTip;

            if (htmlAttributes != null)
            {
                LlenarValores(spanText, htmlAttributes);
            }
            //link.MergeAttributes(new RouteValueDictionary(htmlAttributes));
            return MvcHtmlString.Create(link.ToString());
        }

        public static MvcHtmlString MTCBoton(this HtmlHelper helper, string texto, string id, TipoLink tipo = TipoLink.Editar, bool imagen = false, object htmlAttributes = null)
        {

            var inputText = new TagBuilder("input");
            inputText.Attributes.Add("id", id);
            inputText.Attributes.Add("type", "button");
            if (!imagen) inputText.SetInnerText(texto);
            inputText.AddCssClass(ObtenerClaseLink(tipo));

            if (htmlAttributes != null)
            {
                LlenarValores(inputText, htmlAttributes);
            }

            return MvcHtmlString.Create(inputText.ToString());
        }

        public static MvcHtmlString MTCBotonModal(this HtmlHelper helper, string texto, string accion, string controlador, string idContenedor, TipoLink tipo = TipoLink.Editar, bool imagen = false, int alto = 540, int ancho = 600, object routeValues = null, object htmlAttributes = null, bool Url = true)
        {
            var accionActual = helper.ViewContext.RouteData.GetRequiredString("action");
            var controladorActual = helper.ViewContext.RouteData.GetRequiredString("controller");

            var urlHelper = new UrlHelper(helper.ViewContext.RequestContext);
            var link = new TagBuilder("a") { InnerHtml = texto };
            if (string.IsNullOrEmpty(accion)) accion = accionActual;
            if (string.IsNullOrEmpty(controlador)) controlador = controladorActual;

            if (htmlAttributes != null)
            {
                LlenarValores(link, htmlAttributes);
            }

            link.Attributes["data-ancho"] = ancho.ToString();
            link.Attributes["data-alto"] = alto.ToString();

            if (Url)
            {
                link.Attributes["data-url"] = urlHelper.Action(accion, controlador, routeValues);
            }
            else
            {
                link.Attributes["role"] = "button";
                link.Attributes["data-toggle"] = "modal";
            }

            link.Attributes["href"] = "#" + idContenedor;

            link.AddCssClass(ObtenerClaseLink(tipo));
            //link.AddCssClass("btn");

            if (imagen)
                link.AddCssClass("modalMTC");
            else
                link.AddCssClass("btn modalMTC");

            return MvcHtmlString.Create(link.ToString());
        }

        public static MvcHtmlString MTCBotonGrilla(this HtmlHelper helper, string texto, string id, TipoLink tipo = TipoLink.Editar, bool imagen = false, object htmlAttributes = null, string toolTip = "")
        {
            var inputText = new TagBuilder("input");
            inputText.Attributes.Add("id", id);
            inputText.Attributes.Add("type", "button");
            if (!imagen) inputText.SetInnerText(texto);
            //inputText.AddCssClass("btnGrilla");
            inputText.AddCssClass("btn_icons");
            if (toolTip != "")
            {
                inputText.Attributes.Add("data-toggle", "tooltip");
                inputText.Attributes["title"] = toolTip;
            }

            inputText.AddCssClass(ObtenerClaseLinkGrilla(tipo));

            if (htmlAttributes != null)
            {
                LlenarValores(inputText, htmlAttributes);
            }
            return MvcHtmlString.Create(inputText.ToString());
        }

        public static MvcHtmlString MTCBotonGrillaModal(this HtmlHelper helper, string accion, string controlador, string id, TipoLink tipo = TipoLink.Editar, bool imagen = false, int alto = 540, int ancho = 600, object htmlAttributes = null, object routeValues = null, string toolTip = "")
        {
            var accionActual = helper.ViewContext.RouteData.GetRequiredString("action");
            var controladorActual = helper.ViewContext.RouteData.GetRequiredString("controller");
            var urlHelper = new UrlHelper(helper.ViewContext.RequestContext);
            if (string.IsNullOrEmpty(accion)) accion = accionActual;
            if (string.IsNullOrEmpty(controlador)) controlador = controladorActual;

            var inputText = new TagBuilder("input");
            inputText.Attributes.Add("id", id);
            inputText.Attributes.Add("type", "button");
            inputText.AddCssClass(ObtenerClaseLinkGrilla(tipo));
            //inputText.AddCssClass("modalMTC btnGrilla");
            inputText.AddCssClass("modalMTC btn_icons");
            if (toolTip != "")
            {
                inputText.Attributes.Add("data-toggle", "tooltip");
                inputText.Attributes["title"] = toolTip;
            }

            inputText.Attributes["data-url"] = urlHelper.Action(accion, controlador, routeValues);
            if (htmlAttributes != null)
            {
                LlenarValores(inputText, htmlAttributes);
            }
            inputText.Attributes["data-ancho"] = ancho.ToString();
            inputText.Attributes["data-alto"] = alto.ToString();
            return MvcHtmlString.Create(inputText.ToString());
        }

        public static MvcHtmlString MTCBotonIconoModal(this HtmlHelper helper, string accion, string controlador, string idContenedor, TipoIcono icono = TipoIcono.Buscar, object routeValues = null)
        {
            var accionActual = helper.ViewContext.RouteData.GetRequiredString("action");
            var controladorActual = helper.ViewContext.RouteData.GetRequiredString("controller");

            var imagen = new TagBuilder("i");

            imagen.AddCssClass(ObtenerIcono(icono));

            var urlHelper = new UrlHelper(helper.ViewContext.RequestContext);
            var link = new TagBuilder("a") { InnerHtml = imagen.ToString() };
            if (string.IsNullOrEmpty(accion)) accion = accionActual;
            if (string.IsNullOrEmpty(controlador)) controlador = controladorActual;

            link.Attributes["data-url"] = urlHelper.Action(accion, controlador, routeValues);
            link.Attributes["href"] = "#" + idContenedor;
            link.AddCssClass("btn btn-success modalMTC");

            return MvcHtmlString.Create(link.ToString());
        }

        public static MvcHtmlString MTCEditor(this HtmlHelper helper, string texto, TipoInput tipo = TipoInput.Normal, bool multiline = false, object htmlAttributes = null)
        {
            var wrapper = new TagBuilder("div");
            wrapper.AddCssClass("form_campo " + ObtenerClase(tipo));

            var inputText = new TagBuilder("input");
            //inputText.AddCssClass("text-box single-line");
            inputText.Attributes.Add("value", texto);
            inputText.Attributes.Add("type", "text");

            if (htmlAttributes != null)
            {
                LlenarValores(inputText, htmlAttributes);
            }

            if (tipo == TipoInput.Deshabilitado)
            {
                inputText.Attributes.Add("readonly", "readonly");
            }

            wrapper.InnerHtml = inputText.ToString();
            return new MvcHtmlString(wrapper.ToString());
        }

        public static MvcHtmlString MTCEditorFor<TModel, TTextProperty>(this HtmlHelper<TModel> html,
        Expression<Func<TModel, TTextProperty>> texto, TipoInput tipo = TipoInput.Normal, object htmlAttributesContenedor = null,
            object htmlAttributesInput = null
        )
        {

            var nombre = (string)ModelMetadata.FromLambdaExpression(texto, html.ViewData).PropertyName;

            string textoValor = "";
            if (ModelMetadata.FromLambdaExpression(texto, html.ViewData).Model != null)
            {
                textoValor = ModelMetadata.FromLambdaExpression(texto, html.ViewData).Model.ToString();
            }

            var wrapper = new TagBuilder("div");
            wrapper.AddCssClass("form_campo " + ObtenerClase(tipo));


            if (htmlAttributesContenedor != null)
            {
                LlenarValores(wrapper, htmlAttributesContenedor);
            }
            
            var inputText = new TagBuilder("input");
            inputText.AddCssClass("text-box single-line");
            inputText.Attributes.Add("name", nombre);
            inputText.Attributes.Add("id", nombre);
            inputText.Attributes.Add("value", textoValor);


            if (string.IsNullOrEmpty(ModelMetadata.FromLambdaExpression(texto, html.ViewData).DataTypeName))
            {
                inputText.Attributes.Add("type", "text");
            }
            else
            {
                inputText.Attributes.Add("type", ModelMetadata.FromLambdaExpression(texto, html.ViewData).DataTypeName);
            }

            if (tipo == TipoInput.Deshabilitado)
            {
                inputText.Attributes.Add("readonly", "readonly");
            }

            if (htmlAttributesInput != null)
            {
                LlenarValores(inputText, htmlAttributesInput);
            }

            string prefix = ExpressionHelper.GetExpressionText(texto);
            if (string.IsNullOrEmpty(prefix))
                prefix = "empty";

            var validationAttributes = html.GetUnobtrusiveValidationAttributes(prefix);
            foreach (KeyValuePair<string, object> pair in validationAttributes)
            {
                inputText.MergeAttribute(pair.Key, pair.Value.ToString());
            }

            MvcHtmlString validation = html.ValidationMessageFor(texto);
            if (validation != null)
            {
                inputText.InnerHtml = validation.ToString();
            }

            wrapper.InnerHtml = inputText.ToString();

            return new MvcHtmlString(wrapper.ToString());

        }
        
        public static MvcHtmlString MTCLabelFor<TModel, TTextProperty>(this HtmlHelper<TModel> html,
        Expression<Func<TModel, TTextProperty>> texto, TipoLabel tipo = TipoLabel.Normal, object htmlAttributesContenedor = null,
            object htmlAttributesInput = null
        )
        {

            var nombre = (string)ModelMetadata.FromLambdaExpression(texto, html.ViewData).DisplayName;

            string textoValor = "";
            if (ModelMetadata.FromLambdaExpression(texto, html.ViewData).Model != null)
            {
                textoValor = ModelMetadata.FromLambdaExpression(texto, html.ViewData).Model.ToString();
            }

            var inputText = new TagBuilder("label");

            inputText.Attributes.Add("for", nombre);
            inputText.Attributes.Add("value", nombre);

            inputText.InnerHtml = nombre;

            if (htmlAttributesInput != null)
            {
                LlenarValores(inputText, htmlAttributesInput);
            }

            //wrapper.InnerHtml = inputText.ToString();

            return new MvcHtmlString(inputText.ToString());

        }

        public static MvcHtmlString MTCLabel(this HtmlHelper html,
        string texto, string id, string @for, TipoLabel tipo = TipoLabel.Normal, object htmlAttributesInput = null)
        {

            var nombre = (string)id;

            string textoValor = "";
            textoValor = texto;

            var label = new TagBuilder("label");

            label.Attributes.Add("for", @for);
            //label.Attributes.Add("value", textoValor);

            label.InnerHtml = textoValor;

            if (htmlAttributesInput != null)
            {
                LlenarValores(label, htmlAttributesInput);
            }

            return new MvcHtmlString(label.ToString());

        }

        public static MvcHtmlString MTCEditorNormalFor<TModel, TTextProperty>(this HtmlHelper<TModel> html,
        Expression<Func<TModel, TTextProperty>> texto, AnchoInput ancho = AnchoInput.Largo,
            TipoInput tipo = TipoInput.Normal, object htmlAttributes = null
        )
        {

            var nombre = (string)ModelMetadata.FromLambdaExpression(texto, html.ViewData).PropertyName;
            var textoValor = (ModelMetadata.FromLambdaExpression(texto, html.ViewData).Model).ToString();

            var wrapper = new TagBuilder("div");
            wrapper.AddCssClass("form_campo " + ObtenerClase(tipo));

            var inputText = new TagBuilder("input");
            if (ancho == AnchoInput.Corto)
                inputText.AddCssClass("text-box single-line corto-input");
            else
                inputText.AddCssClass("text-box single-line");

            inputText.Attributes.Add("name", nombre);
            inputText.Attributes.Add("id", nombre);
            inputText.Attributes.Add("value", textoValor);
            inputText.Attributes.Add("type", "text");

            if (tipo == TipoInput.Deshabilitado)
            {
                inputText.Attributes.Add("disabled", "disabled");
            }

            if (htmlAttributes != null)
            {
                LlenarValores(inputText, htmlAttributes);
            }
            wrapper.InnerHtml = inputText.ToString();
            return new MvcHtmlString(wrapper.ToString());

        }

        #region Utilitarios

        public static string ObtenerIcono(TipoIcono icono)
        {
            string clase = "";
            switch (icono)
            {
                case TipoIcono.Buscar: clase = "icon-search icon-white"; break;
                case TipoIcono.Guardar: clase = "icon-search icon-white"; break;

            }
            return clase;
        }
        
        public static string ObtenerClase(TipoInput tipo)
        {
            string clase = "";
            switch (tipo)
            {
                case TipoInput.Deshabilitado: clase = "disabled"; break;
                case TipoInput.Error: clase = "error"; break;
                case TipoInput.Obligatorio: clase = "obligatorio"; break;
            }
            return clase;
        }
        
        public static string ObtenerClase(TipoLabel tipo)
        {
            string clase = "";
            switch (tipo)
            {
                //                case TipoLabel.Normal: clase = "disabled"; break;
                case TipoLabel.Marcado: clase = "marcado"; break;
                case TipoLabel.LadoSuperior: clase = "obligatorio"; break;
            }
            return clase;
        }

        public static string ObtenerClaseLink(TipoLink tipo)
        {
            string clase = "";
            switch (tipo)
            {
                case TipoLink.Editar: clase = "btn_editar"; break;
                case TipoLink.Detalle: clase = "btn_editar"; break;
                case TipoLink.Eliminar: clase = "btn_eliminar"; break;
                case TipoLink.Nuevo: clase = "btn_nuevo"; break;
                case TipoLink.Buscar: clase = "btn_buscar"; break;
                case TipoLink.Versionar: clase = "btn_Versionar_grilla"; break;
                case TipoLink.Configuracion: clase = "btn_config"; break;
                case TipoLink.Editar_Span: clase = "spanBtn_editar"; break;
            }
            return clase;
        }

        public static string ObtenerClaseLinkGrilla(TipoLink tipo)
        {
            string clase = "";
            switch (tipo)
            {
                case TipoLink.Editar: clase = "btn_icons_edit"; break;
                case TipoLink.Detalle: clase = "btn_cancelar_grilla"; break;
                case TipoLink.Eliminar: clase = "btn_icons_delete"; break;
                case TipoLink.Nuevo: clase = "btn_nuevo_grilla"; break;
                case TipoLink.Buscar: clase = "btn_buscar_grilla"; break;
                case TipoLink.Versionar: clase = "btn_Versionar"; break;
            }
            return clase;
        }

        #endregion

        #region Extensiones
        public static MvcHtmlString MiActionLink(this HtmlHelper helper
            , string accion, string controlador, TipoLink tipo = TipoLink.Editar_Span
            , object routeValues = null
            , object htmlAttributes = null
            , string toolTip = "", string texto = "")
        {
            var accionActual = helper.ViewContext.RouteData.GetRequiredString("action");
            var controladorActual = helper.ViewContext.RouteData.GetRequiredString("controller");

            var spanText = new TagBuilder("span");
            spanText.AddCssClass(ObtenerClaseLink(tipo));

            var urlHelper = new UrlHelper(helper.ViewContext.RequestContext);
            var link = new TagBuilder("a") { InnerHtml = spanText.ToString() };
            if (string.IsNullOrEmpty(accion)) accion = accionActual;
            if (string.IsNullOrEmpty(controlador)) controlador = controladorActual;

            link.Attributes["href"] = urlHelper.Action(accion, controlador, routeValues);
            link.Attributes["title"] = toolTip;
            link.AddCssClass(ObtenerClaseLinkGrilla(tipo));

            if (!string.IsNullOrEmpty(texto))
            {
                link.InnerHtml += texto;
            }

            return MvcHtmlString.Create(link.ToString());
        }

        #endregion

    }



}