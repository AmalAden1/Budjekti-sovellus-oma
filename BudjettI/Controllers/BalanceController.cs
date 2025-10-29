using BudjettI.Models;
using System;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;

namespace BudjettI.Controllers
{
    public class BalanceController : Controller
    {
        private BudjektiBDEntities db = new BudjektiBDEntities();

        // GET: Balance
        public ActionResult Index()
        {
            decimal totalIncome = 0m;
            decimal totalExpense = 0m;

            // Tulot.Maara ja Menot.Maara ovat string-tyyppisiä; yritetään parsia turvallisesti
            foreach (var t in db.Tulot.Where(x => x.Maara != null))
            {
                var s = t.Maara.Trim();
                s = s.Replace(" ", "").Replace("€", "").Replace("EUR", "").Replace("\u00A0", "").Replace(",", ".");
                if (decimal.TryParse(s, NumberStyles.Any, CultureInfo.InvariantCulture, out var v))
                {
                    totalIncome += v;
                }
            }

            foreach (var m in db.Menot.Where(x => x.Maara != null))
            {
                var s = m.Maara.Trim();
                s = s.Replace(" ", "").Replace("€", "").Replace("EUR", "").Replace("\u00A0", "").Replace(",", ".");
                if (decimal.TryParse(s, NumberStyles.Any, CultureInfo.InvariantCulture, out var v))
                {
                    totalExpense += v;
                }
            }

            ViewBag.TotalIncome = totalIncome;
            ViewBag.TotalExpense = totalExpense;
            ViewBag.Balance = totalIncome - totalExpense;

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
