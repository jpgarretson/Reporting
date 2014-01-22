using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Reporting.Models
{
    public enum WidgetType
    {
        LineChart,
        PieChart,
        BarChart,
        Gauge
    }

    public class WidgetModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string DataUrl { get; set; }

        public String Query { get; set; }

        public List<String> QueryOptions { get; set; }

        public string Type { get; set; }

    }
}