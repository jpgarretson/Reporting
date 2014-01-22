using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace Reporting.Controllers.Api
{
    public class Data
    {
        public string label {get;set;}
        public dynamic data {get;set;}
        public string color {get;set;}
    }
   
    public class PieController : ApiController
    {
        public async Task<IEnumerable<Data>> Post(Models.WidgetModel model)
        {
            List<Data> result = new List<Data>();

            if (ModelState.IsValid)
            {
                var NewData = await FakeDatabaeCall_Pie(model.Id);
                result.AddRange(NewData);
            }
            
            return result;
        }

        private async Task<IEnumerable<Data>> FakeDatabaeCall_Pie(int id)
        {
            List<Data> result = new List<Data>();

            switch (id % 3)
            {
                case 0: //Survey Sources
                    result.Add(new Data { label = "North Atlanta Urgent Care", data = 28.7, color = "#68BC31" });
                    result.Add(new Data { label = "Doctors Express Urgent Care", data = 14.5, color = "#2091CF" });
                    result.Add(new Data { label = "Buckhead South Urgent Care", data = 8.2, color = "#AF4E96" });
                    result.Add(new Data { label = "Pain Consultants of Atlanta", data = 18.6, color = "#DA5430" });
                    result.Add(new Data { label = "Other", data = 30, color = "#FEE074" });
                    break;
                case 1: //Prescription Sales
                    result.Add(new Data { label = "North Atlanta Urgent Care", data = 15, color = "#68BC31" });
                    result.Add(new Data { label = "Doctors Express Urgent Care", data = 32, color = "#2091CF" });
                    result.Add(new Data { label = "Buckhead South Urgent Care", data = 10, color = "#AF4E96" });
                    result.Add(new Data { label = "Pain Consultants of Atlanta", data = 38, color = "#DA5430" });
                    result.Add(new Data { label = "Other", data = 5, color = "#FEE074" });
                    break;
                case 2: //Site Vists
                    result.Add(new Data { label = "social networks", data = 38.7, color = "#68BC31" });
                    result.Add(new Data { label = "search engines", data = 24.5, color = "#2091CF" });
                    result.Add(new Data { label = "ad campaigns", data = 8.2, color = "#AF4E96" });
                    result.Add(new Data { label = "direct traffic", data = 18.6, color = "#DA5430" });
                    result.Add(new Data { label = "other", data = 10, color = "#FEE074" });
                    break;
            };

            return result;
        }
    }

    public class LineController : ApiController
    {
        public async Task<IEnumerable<Data>> Post(Models.WidgetModel model)
        {
            List<Data> result = new List<Data>();

            if (ModelState.IsValid)
            {
                var NewData = await FakeDatabaeCall_Line(model.Id);
                result.AddRange(NewData);
            }

            return result;
        }

       
        public async Task<IEnumerable<Data>> FakeDatabaeCall_Line(int id)
        {
            List<Data> result = new List<Data>();

            var d1 = new List<double[]>();
            for (double i = id; i < Math.PI * 2; i += 0.5)
            {
                d1.Add(new double[] { i, Math.Sin(i) });
            }

            var d2 = new List<double[]>();
            for (double i = id; i < Math.PI * 2; i += 0.5)
            {
                d2.Add(new double[] { i, Math.Cos(i) });
            }

            var d3 = new List<double[]>();
            for (double i = id; i < Math.PI * 2; i += 0.2)
            {
                d3.Add(new double[] { i, Math.Tan(i) });
            }

            result.Add(new Data() { label = "Domains", data = d1 });
            result.Add(new Data() { label = "Hosting", data = d2 });
            result.Add(new Data() { label = "Services", data = d3 });

            return result;

        }
    }


    public class GaugeController : ApiController
    {
        public async Task<IEnumerable<Data>> Post(Models.WidgetModel model)
        {
            List<Data> result = new List<Data>();

            if (ModelState.IsValid)
            {
                var NewData = await FakeDatabaeCall_Gauge(model.Id);
                result.AddRange(NewData);
            }

            return result;
        }

        public async Task<IEnumerable<Data>> FakeDatabaeCall_Gauge(int position)
        {
            List<Data> result = new List<Data>();

            var minCord = new {x= -60, y= -57};
            var maxCord = new {x= 60, y= -60};
            var radius = 90;

            // some calculations
            var startAngle = (6.2831 + Math.Atan2(minCord.y, minCord.x));
            var endAngle = Math.Atan2(maxCord.y, maxCord.x);
            var degreesSweep = (-endAngle) + startAngle;

            Random r = new Random(DateTime.Now.Millisecond);
            var magnitude = r.NextDouble();

            var numDegrees = degreesSweep * (magnitude / 100.0);
            var angle = (startAngle - numDegrees);
            var posX = radius * Math.Cos(angle);
            var posY = radius * Math.Sin(angle);

            List<List<double>> data = new List<List<double>>();
            data.Add(new List<double>() { 0, 0 });
            data.Add(new List<double>() { posX, posY });


            result.Add(new Data() { data = data });
            return result;



  


        }

    }
    
}
