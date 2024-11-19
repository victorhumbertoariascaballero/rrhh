using System;
using System.Collections.Generic;
using MIDIS.ORI.Entidades.Auth;
using MIDIS.SEG.AccesoDatosSQL;
using System.Transactions;
using System.Net.Mail;
using System.Configuration;

namespace MIDIS.ORI.LogicaNegocio
{
    public class BL_TAB_Usuario
    {

        public BE_TAB_Usuario Autenticar(BE_TAB_Usuario oParam)
        {
            try
            {
                DA_TAB_Usuario da = new DA_TAB_Usuario();
                return da.Autenticar(oParam);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        

        public List<BE_TAB_Persona> ListarAbogados(BE_TAB_Persona oParam)
        {
            try
            {
                DA_TAB_Usuario da = new DA_TAB_Usuario();
                return da.ListarAbogados(oParam);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public List<BE_TAB_Personal> ListarPersonal(BE_TAB_Persona oParam)
        {
            try
            {
                DA_TAB_Usuario da = new DA_TAB_Usuario();
                return da.ListarPersonal(oParam);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public BE_TAB_Usuario ObtenerDatosUsuario(BE_TAB_Usuario oParam)
        {
            try
            {
                DA_TAB_Usuario da = new DA_TAB_Usuario();
                return da.ObtenerDatosUsuario(oParam);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public BE_TAB_Usuario GenerarTokenCorreo(BE_TAB_Usuario oParam)
        {
            BE_TAB_Usuario resultado = null;
            BL_Encryption encriptadorTexto = new BL_Encryption();
            try
            {
                DA_TAB_Usuario da = new DA_TAB_Usuario();


                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {

                    string gpos = "";


                    Guid g = Guid.NewGuid();
                    


                    string token= g.ToString() + "." +BL_Encryption.EncryptBase64(oParam.vUsuario);
                        //BL_Encryption.encryptText(DateTime.Now.AddDays(1).ToString("dd/MM/yy HH:mm:ss.ffffff") + "|" + oParam.vUsuario);

                    
                    string gposParagraph = "<tr><td><a href='" + oParam.vRutaInicio + "?token="+ token+"'>"+ oParam.vRutaInicio +"?token="+ token +"</a></td></tr>" ;
                    oParam.vToken = token;
                    int resultActual = da.GenerarToken(oParam); //guardar token

                    if (resultActual > 0)
                    {
                        MailMessage msg = new MailMessage();
                        SmtpClient smtp = new SmtpClient();

                        smtp.Host = ConfigurationManager.AppSettings["email_servidor"].ToString();

                        smtp.Credentials = new System.Net.NetworkCredential(ConfigurationManager.AppSettings["email_soporte"].ToString(),
                            ConfigurationManager.AppSettings["email_contrasena"].ToString());

                        smtp.Port = Int32.Parse(ConfigurationManager.AppSettings["smpt_puerto"].ToString());// 25;
                        smtp.EnableSsl = true;


                        //msg.From = new MailAddress("juan.paucar@qw.gob.pe", "NO RESPONDER", System.Text.Encoding.UTF8);
                        msg.From = new MailAddress(ConfigurationManager.AppSettings["email_soporte"].ToString(), "NO RESPONDER", System.Text.Encoding.UTF8);
                        msg.To.Add(oParam.vEmail);


                        msg.Body = @"<html xmlns='http://www.w3.org/1999/xhtml'><head>
                      <meta http-equiv='Content-Type' content='text/html; charset=utf-8'>
                      <title>ACTUALIZACION DE CONTRASEÑA</title>
                    </head>

                    <body yahoo='' bgcolor='#ffffff'>
                    <table width='100%' bgcolor='#ffffff' border='0' cellpadding='10' cellspacing='0'>
                    <tbody><tr>
                      <td>
                        <table bgcolor='#ffffff' class='content' align='left' cellpadding='0' cellspacing='0' border='0'>
			                    <tbody><tr>
				                    <td valign='top' mc:edit='headerBrand' id='templateContainerHeader'>
                                        Estimado usuario, 
				                    </td>
			                    </tr>
                                <tr><td><br/></td></tr>
			                    <tr>
				                    <td align='left' valign='top'>
				                     Usted ha solicitado un cambio de clave, para lo cual puede ingresar a través del siguiente link, el cual solo podrá realizarlo una vez: 
				                    </td>
			                    </tr>" + gposParagraph + @"
                                <tr><td><br/></td></tr>
                                <tr>
                                    <td>Atentamente,</td>
                                </tr>
                                <tr><td><br/></td></tr>
                                <tr>
                                    <td>NOTA: Este es un buzón desatendido, favor de no responder.</td>
                                </tr>
                        </tbody></table>
                        </td>
                      </tr>
                    </tbody></table>

                    </body></html>";

                        msg.IsBodyHtml = true;
                        msg.BodyEncoding = System.Text.Encoding.UTF8;
                        msg.Subject = "Cambio de clave - Sispad:" + gpos;
                        msg.IsBodyHtml = true;
                        msg.SubjectEncoding = System.Text.Encoding.UTF8;



                        smtp.Send(msg);


                        tx.Complete();

                        resultado = new BE_TAB_Usuario();
                        resultado.MessageCode = "0000";
                        resultado.Message = "Se le envió un correo, favor de revisar su buzón para poder realizar el cambio de contraseña.";
                    }
                    else
                    {

                    }

                }
                 

                return resultado;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }



        public BE_TAB_Usuario ActualizarContrasena(BE_TAB_Usuario oParam)
        {
            DA_TAB_Usuario daUs = new DA_TAB_Usuario();
            int registros = 0;
            BE_TAB_Usuario result = null;
            try
            {
                string[] partes = oParam.vToken.Split('.');
                
                 oParam.vUsuario= BL_Encryption.DecryptBase64(partes[1]);

                 registros = daUs.ActualizarContrasena(oParam);
                 if (registros > 0)
                 {
                     result = new BE_TAB_Usuario();
                     result.MessageCode = "0000";
                     result.Message = "Se ha actualizado su contraseña";

                 }
                 else
                 {
                     result = new BE_TAB_Usuario();
                     result.MessageCode = "9999";
                     result.Message = "No se pudo actualizar la contraseña, compruebe que  los datos sean correctos";
                 }
            }
            catch (Exception ex)
            {
                throw ex; 
            }
            return result;
        }
    }
}
