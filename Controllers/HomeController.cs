using IronPdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Hotel2._0.Controllers
{
    [RequireHttps]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Send_Email()

        {
            
           //var path = GetPdf();
           return View(new Models.SendEmailViewModel());
        }

        [HttpPost]
        public ActionResult Send_Email(Models.SendEmailViewModel model, HttpPostedFileBase postedFile)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    String toEmail = model.ToEmail;
                    String subject = model.Subject;
                    String contents = model.Contents;

                    string serverPath = Server.MapPath("~/Uploads/");
                    if (!Directory.Exists(serverPath))
                    {
                        Directory.CreateDirectory(serverPath);
                    }
                    string path = "";

                    if (postedFile != null && postedFile.ContentLength > 0)
                    {
                        string fileName = Path.GetFileName(postedFile.FileName);
                        path = serverPath + fileName;
                        postedFile.SaveAs(serverPath + fileName);

                    }

                    Utils.EmailSender es = new Utils.EmailSender();
                    es.Send(toEmail, subject, contents, path, postedFile);

                    ViewBag.Result = "Email has been send.";

                    ModelState.Clear();

                    return View(new Models.SendEmailViewModel());
                }
                catch
                {
                    return View();
                }
            }

            return View();
        }

        
        /*public string GetPdf()
        {
            var htmlToPdf = new HtmlToPdf();
            var html = @"<h1>Happy Halloween!</h1>";
            var pdf = HtmlToPdf.StaticRenderHtmlAsPdf(html);
            //pdf.SaveAs(Path.Combine(Server.MapPath("~/PdfFiles"), "Halloween.pdf"));
            var path = @"C:\Users\Vencill\source\repos\Hotel2.0\PdfFiles";
            pdf.SaveAs(path);
            return path;
        }*/

    }
}