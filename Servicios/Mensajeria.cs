using System.Net.Mail;
using System.Net;

namespace Servicios
{
    public class Mensajeria
    {
        public void EnviarCorreo(string remitente, string destinatario, string asunto, string cuerpo, string adjunto = "")
        {
            MailMessage msj = new MailMessage();
            SmtpClient cli = new SmtpClient();

            msj.From = new MailAddress(remitente);
            msj.To.Add(new MailAddress(destinatario));

            msj.Subject = asunto;
            msj.Body = cuerpo;
            
            if (adjunto != "")
            { msj.Attachments.Add(new Attachment(adjunto)); }
                                      

            msj.IsBodyHtml = false;

            msj.Priority = MailPriority.Normal;


            cli.Host = "smtp.gmail.com";
            cli.Port = 587;
            cli.Credentials = new NetworkCredential("implantagraf@gmail.com", "Pm33834348");
            cli.EnableSsl = true;

            //cli.Host = "mailnotes.bancopatagonia.net.ar";
            //cli.Credentials = new NetworkCredential("", "");

            cli.Send(msj);
        }
    }

}
