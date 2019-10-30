using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Jewelry_Store_e.a.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Accord.Controls;
using Accord.MachineLearning.VectorMachines.Learning;
using Accord.Math.Optimization.Losses;
using Accord.Statistics;
using Accord.Statistics.Kernels;
using Accord.MachineLearning;
using System.Security.Claims;
using System.Text;


namespace Jewelry_Store_e.a.Controllers
{
    public class HomeController : BaseController
    {
        public HomeController(JewelryContext context) : base(context)
        {
        }

        public IActionResult Index()
        {
            string mystring = knn();
            ViewBag.MyString = mystring;
            List<string> articles =Twitter();
            ViewBag.articles = articles;
            return View();
        }
        public List<string> Twitter()
        {
            List<string> articles = new List<string>();
            string credentials = "TUhiVGI0eVlJdzRSMVdGYTUzSnNrYXlqajpyZEszREJhdlhMZWFOOGdKbWdKcGxBOWlJTkgxQXFDQkhMZEJnVlp5ZEdCWGdLa3o2MA==";
            string access_token = "";
            var post = WebRequest.Create("https://api.twitter.com/oauth2/token") as HttpWebRequest;
            post.Method = "POST";
            post.ContentType = "application/x-www-form-urlencoded";
            post.Headers[HttpRequestHeader.Authorization] = "Basic " + credentials;
            var reqbody = Encoding.UTF8.GetBytes("grant_type=client_credentials");
            post.ContentLength = reqbody.Length;
            using (var req = post.GetRequestStream())
            {
                req.Write(reqbody, 0, reqbody.Length);
            }
            try
            {
                string respbody = null;
                using (var resp = post.GetResponse().GetResponseStream())//there request sends
                {
                    var respR = new StreamReader(resp);
                    respbody = respR.ReadToEnd();
                }
                access_token = respbody.Substring(respbody.IndexOf("access_token\":\"") + "access_token\":\"".Length, respbody.IndexOf("\"}") - (respbody.IndexOf("access_token\":\"") + "access_token\":\"".Length));
            }
            catch //if credentials are not valid (403 error)
            {
                //TODO
            }

            var gettimeline = WebRequest.Create("https://api.twitter.com/1.1/statuses/user_timeline.json?count=3&screen_name=PANDORA_NA") as HttpWebRequest;
            gettimeline.Method = "GET";
            gettimeline.Headers[HttpRequestHeader.Authorization] = "Bearer " + access_token;
            try
            {
                string respbody = null;
                using (var resp = gettimeline.GetResponse().GetResponseStream())
                {
                    var respR = new StreamReader(resp);
                    respbody = respR.ReadToEnd();
                    dynamic json = JsonConvert.DeserializeObject(respbody);
                    articles.Add(json[0].text.ToString());
                }

                //TODO use a library to parse json

            }
            catch //401 (access token invalid or expired)
            {
                //TODO
            }
       
            var gettimeline1 = WebRequest.Create("https://api.twitter.com/1.1/statuses/user_timeline.json?count=3&screen_name=hsternofficial") as HttpWebRequest;
            gettimeline1.Method = "GET";
            gettimeline1.Headers[HttpRequestHeader.Authorization] = "Bearer " + access_token;
            try
            {
                string respbody = null;
                using (var resp = gettimeline1.GetResponse().GetResponseStream())
                {
                    var respR = new StreamReader(resp);
                    respbody = respR.ReadToEnd();
                    dynamic json = JsonConvert.DeserializeObject(respbody);
                    articles.Add(json[0].text.ToString());

                }

                //TODO use a library to parse json

            }
            catch //401 (access token invalid or expired)
            {
                //TODO
            }

            var gettimeline2 = WebRequest.Create("https://api.twitter.com/1.1/statuses/user_timeline.json?count=3&screen_name=RockHerJewelry") as HttpWebRequest;
            gettimeline2.Method = "GET";
            gettimeline2.Headers[HttpRequestHeader.Authorization] = "Bearer " + access_token;
            try
            {
                string respbody = null;
                using (var resp = gettimeline2.GetResponse().GetResponseStream())
                {
                    var respR = new StreamReader(resp);
                    respbody = respR.ReadToEnd();
                    dynamic json = JsonConvert.DeserializeObject(respbody);
                    articles.Add(json[0].text.ToString());

                }

                //TODO use a library to parse json

            }
            catch //401 (access token invalid or expired)
            {
                //TODO
            }
            return articles;
        }
        public string knn()
        {
            double[][] inputs =
            {
                // The first two are from class 0
                new double[] { 10 },

                // The next four are from class 1
                new double[] { 30 },

                // The last three are from class 2
                new double[] { 50 },

            };

            int[] outputs =
            {
                0,        // First two from class 0
                1,      // Next four from class 1
                2,     // Last three from class 2
            };


            // Now we will create the K-Nearest Neighbors algorithm. For this
            // example, we will be choosing k = 4. This means that, for a given
            // instance, its nearest 4 neighbors will be used to cast a decision.
            var knn = new KNearestNeighbors(k: 1);

            // We learn the algorithm:
            knn.Learn(inputs, outputs);
            //put a diffoult video
            if(!User.Identity.IsAuthenticated)
            {
                return "instegram.mp4";
            }
                int customerId = int.Parse(User.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.Sid).Value);
            int age = _context.Customers.Where(a => a.ID == customerId).Select(a => a.BirthDate.Year).Single();
            // After the algorithm has been created, we can classify a new instance:
            int answer = knn.Decide(new double[] { (DateTime.Now.Year -age) }); // answer will be 2.
            if (answer == 0)
            {
                return "bracletRing.mp4";
            }
            if (answer == 1)
            {
                return "instegram.mp4";
            }
            return "bracletRing2.mp4";
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}


