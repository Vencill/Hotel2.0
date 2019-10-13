using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;


namespace Hotel2._0.Utils
{
    public class EmailSender
    {
        // Please use your API KEY here.
        private const String API_KEY = "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";

        public void Send(String toEmailList, String subject, String contents, string path, HttpPostedFileBase postedFile)
        {
            var client = new SendGridClient(API_KEY);
            var from = new EmailAddress("noreply@localhost.com", "FIT5032 Example Email User");

            var emailList = toEmailList.Split(';');

            foreach (string email in emailList)
            {
                var to = new EmailAddress(email, "");

                //contents generated
                var plainTextContent = contents;
                var htmlContent = "<p>" + contents + "</p>";
                var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);

                if (postedFile != null && postedFile.ContentLength > 0)
                {
                    var bytes = File.ReadAllBytes(path);
                    var file = Convert.ToBase64String(bytes);
                    msg.AddAttachment(postedFile.FileName, file);
                }

                var response = client.SendEmailAsync(msg);
            }
            
        }

    }
}