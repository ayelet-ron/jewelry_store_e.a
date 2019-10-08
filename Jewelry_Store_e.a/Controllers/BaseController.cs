using Jewelry_Store_e.a.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Jewelry_Store_e.a.Controllers
{
    public abstract class BaseController : Controller
    {
        protected SDMDbContext _context;

        protected BaseController(SDMDbContext context)
        {
            this._context = context;
        }

        public static void ClearCookies(HttpRequest request, HttpResponse response)
        {
            foreach (string key in request.Cookies.Keys)
            {
                if (key.StartsWith("SD"))
                    response.Cookies.Delete(key);
            }
        }
    }
}
