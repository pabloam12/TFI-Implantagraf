﻿using System.Net.Mail;
using System.Net;

namespace Servicios
{
    public class Mensajeria
    {

        //public void EnviarCorreo(string correoRemitente, string correoDestinatario, string asunto, string cuerpo)
        public void EnviarCorreo(string remitente, string destinatario, string asunto, string cuerpo)
        {
            MailMessage msj = new MailMessage();
            SmtpClient cli = new SmtpClient();
            
            msj.From = new MailAddress(remitente);
            msj.To.Add(new MailAddress(destinatario));

            msj.Subject = asunto;
            msj.Body = cuerpo;

            msj.IsBodyHtml = false;

            msj.Priority = MailPriority.Normal;

            
            //cli.Host = "smtp.gmail.com";
            //cli.Port = 587;
            //cli.Credentials = new NetworkCredential("pablo.a.mahiques@gmail.com", "psw");

            cli.Host = "mailnotes.bancopatagonia.net.ar";
            cli.Credentials = new NetworkCredential("", "");
            
            cli.Send(msj);
        }
    }

}
