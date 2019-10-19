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

namespace Jewelry_Store_e.a.Controllers
{
    public class HomeController : BaseController
    {
        public HomeController(SDMDbContext context) : base(context)
        {
        }

        public IActionResult Index()
        {
            string mystring = knn();
            ViewBag.MyString = mystring;
            return View();
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


