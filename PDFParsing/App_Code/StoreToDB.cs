using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;

namespace PDFParsing
{
    public class StoreToDB
    {
        public void SendErrorEmailToAdmin(string Name, string email, string password)
        {
            using (MailMessage mm = new MailMessage("noreply@mudracube.in", email))
            {
                mm.Bcc.Add("piush190388@gmail.com");
                mm.Subject = "Welcome to Mudracube";
                mm.Body = "Hi "+Name+"<br/>Welcome to Mudracube. <br/><br/>Your Password is: " + password;
                mm.IsBodyHtml = true;
                SmtpClient smtp = new SmtpClient();
                smtp.Host = "webmail.mudracube.in";
                smtp.EnableSsl = false;
                NetworkCredential NetworkCred = new NetworkCredential("noreply@mudracube.in", "Faridabad@123");
                smtp.UseDefaultCredentials = true;
                smtp.Credentials = NetworkCred;
                smtp.Port = 587;
                smtp.Send(mm);
            }

        }

        public void ForgotPasswordEmail(string Name, string email, string password)
        {
            using (MailMessage mm = new MailMessage("noreply@mudracube.in", email))
            {
                mm.Bcc.Add("piush190388@gmail.com");
                mm.Subject = "Mudracube: Forgot Password";
                mm.Body = "Hi " + Name + "<br/>Your MudraCube Password is: " + password;
                mm.IsBodyHtml = true;
                SmtpClient smtp = new SmtpClient();
                smtp.Host = "webmail.mudracube.in";
                smtp.EnableSsl = false;
                NetworkCredential NetworkCred = new NetworkCredential("noreply@mudracube.in", "Faridabad@123");
                smtp.UseDefaultCredentials = true;
                smtp.Credentials = NetworkCred;
                smtp.Port = 587;
                smtp.Send(mm);
            }

        }

        public void SendContactUsEmail(string Name, string email, string mobile,string message)
        {
            using (MailMessage mm = new MailMessage("noreply@mudracube.in", "mudracube@gmail.com"))
            {
                mm.Bcc.Add("piush190388@gmail.com");
                mm.Subject = "Mudracube: A new query submitted";
                mm.Body = "Hi.<br/>A new query is submitted on mudracude. Details is as follows:<br/>Name: " + Name + "<br/>Email: " + email + "<br/>Mobile: " + mobile + "<br/>Message: " + message + "<br/><br/>Thanks";
                mm.IsBodyHtml = true;
                SmtpClient smtp = new SmtpClient();
                smtp.Host = "webmail.mudracube.in";
                smtp.EnableSsl = false;
                NetworkCredential NetworkCred = new NetworkCredential("noreply@mudracube.in", "Faridabad@123");
                smtp.UseDefaultCredentials = true;
                smtp.Credentials = NetworkCred;
                smtp.Port = 25;
                smtp.Send(mm);
            }

        }
    }
}