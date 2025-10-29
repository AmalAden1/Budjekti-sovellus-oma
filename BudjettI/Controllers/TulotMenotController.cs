using BudjettI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace BudjettI.Controllers
{
    public class TulotMenotController : Controller
    {
        private BudjektiBDEntities db = new BudjektiBDEntities();

        // GET: TulotMenot
        public ActionResult Index()
        {
            // Haetaan tulot ja menot
            var tulot = db.Tulot.Select(t => new { Date = t.PVM, Amount = t.Maara }).ToList();
            var menot = db.Menot.Select(m => new { Date = m.PVM, Amount = m.Maara }).ToList();

            // Ryhmitellään kuukausittain (vuosi-kuukausi)
            var allMonths = tulot.Select(t => new { Year = t.Date.Value.Year, Month = t.Date.Value.Month })
                                 .Union(menot.Select(m => new { Year = m.Date.Value.Year, Month = m.Date.Value.Month }))
                                 .Distinct()
                                 .OrderBy(x => x.Year).ThenBy(x => x.Month)
                                 .ToList();

            var labels = new List<string>();
            var incomeData = new List<decimal>();
            var expenseData = new List<decimal>();

            foreach (var ym in allMonths)
            {
                var monthLabel = string.Format("{0}/{1}", ym.Month.ToString("D2"), ym.Year);
                labels.Add(monthLabel);

                var incomeSum = tulot.Where(t => t.Date.Value.Year == ym.Year && t.Date.Value.Month == ym.Month)
                                     .Sum(t => (decimal?)t.Amount) ?? 0m;
                var expenseSum = menot.Where(m => m.Date.Value.Year == ym.Year && m.Date.Value.Month == ym.Month)
                                       .Sum(m => (decimal?)m.Amount) ?? 0m;

                incomeData.Add(incomeSum);
                expenseData.Add(expenseSum);
            }

            var js = new JavaScriptSerializer();
            ViewBag.LabelsJson = js.Serialize(labels);
            ViewBag.IncomeJson = js.Serialize(incomeData);
            ViewBag.ExpenseJson = js.Serialize(expenseData);

            return View();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
