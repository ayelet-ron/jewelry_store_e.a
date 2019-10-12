using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.IO;
using Jewelry_Store_e.a.Models;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;

namespace Jewelry_Store_e.a.Controllers
{
    public class KNNController : BaseController
    {
        public KNNController(SDMDbContext context) : base(context)
        {
        }
        
    }
}
        