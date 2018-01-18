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
            ReportDocument rd = new ReportDocument();
            rd.Load(Path.Combine(Server.MapPath("~/Reports/CRStatement.rpt")));
            rd.SetDataSource(entities.statements.Select(s => new
            {
                Id = s.Id,
                TransactionDate = s.TransactionDate,
                CustomerName = s.CustomerName,
                CardNumber = s.CardNumber,
                imagePath = s.imagePath
               
            }).ToList());
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            stream.Seek(0, SeekOrigin.Begin);
            return File(stream, "application/pdf", "ListStatement.pdf");
        }
	}
}