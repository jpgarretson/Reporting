using Reporting.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Reporting.Controllers
{
	public class HomeController : Controller
	{
		//
		// GET: /Hone/
		public ActionResult Index()
		{
			Models.IndexModel model = new Models.IndexModel();
			var sType = "";

            for (int i = 0; i < 3; i++)
            {
                sType = WidgetType.PieChart.ToString();
                model.Widgets.Add(
                    new Reporting.Models.WidgetModel()
                    {
                        Id = i + 1,
                        Title = "Blah Pie",
                        DataUrl = "/api/pie",
                        Query = "This Week",
                        QueryOptions = new List<String> { "This Week", "Last Week", "This Month", "Last Month" },
                        Type = sType
                    });

            }

            for (int i = 0; i < 3; i++)
            {
                sType = WidgetType.LineChart.ToString();
                model.Widgets.Add(
                    new WidgetModel()
                    {
                        Id = i + 1,
                        Title = "Blah Line",
                        DataUrl = "/api/line",
                        Query = "This Week",
                        QueryOptions = new List<String> { "This Week", "Last Week", "This Month", "Last Month" },
                        Type = sType
                    });
            }

			return View(model);
		}
	}
}