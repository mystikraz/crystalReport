using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CrystalDecisions.CrystalReports.Engine;
using CRApp.Models;
using CRApp.Reports;



namespace CRApp.Controllers
{
    public class StatementController : Controller
    {
        private CreditCardEntities entities=new CreditCardEntities();
        //
        // GET: /Statement/
        public ActionResult Index()
        {
            ViewBag.ListStatement = entities.statements.ToList();
            return View();
        }

        public ActionResult Export()
        {
            var query = entities.statements.Select(s => s.Acnumber).FirstOrDefault();


            string pwd = GeneratePassword(query);
            ReportDocument rd = new ReportDocument();
            rd.Load(Path.Combine(Server.MapPath("~/Reports/CrystalReport2.rpt")));
            rd.SetDataSource(entities.statements.Select(s => new
            {
                                Id = s.Id,
                                TransactionDate = s.TransactionDate,
                                CustomerName = s.CustomerName,
                                CardNumber = s.CardNumber,
                                imagePath = s.imagePath,


                
                PostedDate = s.PostedDate,
                DescriptionOfTransactions = s.DescriptionOfTransactions,
                TransactionsAmount = s.TransactionsAmount,
                BilledAmount = s.BilledAmount,
                DRCR = s.DRCR,

            }).ToList());
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
           // stream.Seek(0, SeekOrigin.Begin);

            Stream t_Output;
            using (Stream t_Input = stream)
            //using (Stream t_Output = new FileStream(Server.MapPath(".") + "\\test_encrypted.pdf", FileMode.Create, FileAccess.Write, FileShare.None))
            using (t_Output = new FileStream("C:\\img\\" + "test_encrypted.pdf", FileMode.Create, FileAccess.Write, FileShare.None))
            {
                iTextSharp.text.pdf.PdfReader t_Reader = new iTextSharp.text.pdf.PdfReader(t_Input);
                iTextSharp.text.pdf.PdfEncryptor.Encrypt(t_Reader, t_Output, true, pwd, pwd, iTextSharp.text.pdf.PdfWriter.ALLOW_PRINTING);
            }

            //return File("C:\\img\\" + "test_encrypted.pdf", "application/pdf", "ListStatement.pdf");
           
            return View();



            //PdfDocument document = PdfReader.Open(stream);


            //PdfSecuritySettings securitySettings = document.SecuritySettings;

            //// Setting one of the passwords automatically sets the security level to 
            //// PdfDocumentSecurityLevel.Encrypted128Bit.
            //securitySettings.UserPassword = "user";
            //securitySettings.OwnerPassword = "owner";

            //// Don´t use 40 bit encryption unless needed for compatibility reasons
            ////securitySettings.DocumentSecurityLevel = PdfDocumentSecurityLevel.Encrypted40Bit;

            //// Restrict some rights.            
            //securitySettings.PermitAccessibilityExtractContent = false;
            //securitySettings.PermitAnnotations = false;
            //securitySettings.PermitAssembleDocument = false;
            //securitySettings.PermitExtractContent = false;
            //securitySettings.PermitFormsFill = true;
            //securitySettings.PermitFullQualityPrint = false;
            //securitySettings.PermitModifyDocument = true;
            //securitySettings.PermitPrint = false;

            //// Save the document...
            //document.Save(securitySettings);



            //return File(stream, "application/pdf", "ListStatement.pdf");

            //FileStream output;
            //using (var input = new FileStream("c:/ListStatement.pdf", FileMode.Open, FileAccess.Read, FileShare.Read))
            //using (output = new FileStream("ListStatement_encrypted.pdf", FileMode.Create, FileAccess.Write, FileShare.None))
            //{
            //    var reader = new PdfReader(input);
            //    PdfEncryptor.Encrypt(reader, output, true, "userPassword", "userPassword", PdfWriter.ALLOW_PRINTING);
            //   // return File(reader);
            //}



        }

        public string GeneratePassword(string AccountNumber)
        {
            string password = AccountNumber.Substring(4, 8);
            return password;
        }

        
	}
}