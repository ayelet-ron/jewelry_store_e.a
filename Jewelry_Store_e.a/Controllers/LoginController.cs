using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Jewelry_Store_e.a.Models;

namespace Jewelry_Store_e.a.Controllers
{
    public class LoginController : BaseController
    {
        public LoginController(SDMDbContext context) : base(context)
        {
        }

        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> Register(string Email, string Password, string FirstName, string LastName,
            DateTime BirthDate)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Customers.Add(new Customer
                    {
                        Email = Email,
                        Password = Password,
                        FirstName = FirstName,
                        LastName = LastName,
                        BirthDate = BirthDate,
                        IsAdmin = false,
                    });
                    await _context.SaveChangesAsync();
                    ViewData["Message"] = "You have been successfully registered!";
                    return Redirect("/Login/");
                }
                catch (DbUpdateException ex)
                {
                    ViewData["Message"] = $"{Email} already exists";
                }
            }
            return View();
        }
        [Authorize]
        public async Task<IActionResult> Details(int? id)
        {
            var userEmail = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email);
            Customer currentCustomer = _context.Customers.FirstOrDefault(x => x.Email == userEmail.Value);
            if (id == null)
            {
                return View(currentCustomer);
            }
            if (!User.HasClaim(ClaimTypes.Role, "Admin"))
            {
                if (currentCustomer.ID == id)
                {
                    return View(currentCustomer);
                }
                else
                {
                    return Redirect("/error/denied");
                }
            }
            else
            {
                Customer customer = await _context.Customers.FirstOrDefaultAsync(m => m.ID == id);
                if (customer == null)
                {
                    //ViewData["Object"] = "Customer";
                    return NotFound();
                }
                return View(customer);
            }
        }

        [AllowAnonymous]
        public ActionResult Index()
        {
            // Put the ReturnUrl in the user's cookies
            if (Request.Query.ContainsKey("ReturnUrl"))
            {
                ViewBag.Message = "This is an authorized path, please login";
                Response.Cookies.Append("ReturnUrl", Request.Query["ReturnUrl"]);
            }
            return View();
        }

        [Authorize]
        public ActionResult Logout()
        {
            // Sign the user out
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            ClearCookies(Request, Response);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Index(string email, string password)
        {
            var user = _context.Customers.SingleOrDefault(customer => customer.Email == email && customer.Password == password);
            if (user == null)
            {
                ViewBag.Message = "Invalid username or password";
                return View();
            }

            // Create the user claims
            List<Claim> claims = new List<Claim>();
            if (user.IsAdmin)
            {
                // Set admin claims
                claims.Add(new Claim(ClaimTypes.Role, "Admin"));
                claims.Add(new Claim(ClaimTypes.Name, "Admin"));
            }
            else
            {
                // Set user claims
                claims.Add(new Claim(ClaimTypes.Role, "Customer"));
                claims.Add(new Claim(ClaimTypes.Name, user.FirstName + " " + user.LastName));
            }
            // Set username claim
            claims.Add(new Claim(ClaimTypes.Sid, user.ID.ToString()));
            claims.Add(new Claim(ClaimTypes.Email, user.Email));

            // Create the identity of the user
            ClaimsIdentity identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            ClaimsPrincipal principal = new ClaimsPrincipal(identity);

            // Sign the user in
            HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

            // Handle ReturnUrl
            string redirect = "/Home/";
            if (Request.Cookies.ContainsKey("ReturnUrl"))
            {
                redirect = Request.Cookies["ReturnUrl"];
                Response.Cookies.Delete("ReturnUrl");
            }
            ClearCookies(Request, Response);

            return Redirect(redirect);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult List()
        {
            return View(_context.Customers.ToList());
        }


        // GET: Products/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _context.Customers
                .FirstOrDefaultAsync(m => m.ID == id);
            if (customer == null)
            {
                return NotFound();
            }

            if (customer.IsAdmin) return RedirectToAction(nameof(List));

            return View(customer);
        }


        // POST: Products/Delete/5
        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var customer = await _context.Customers.FindAsync(id);
            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(List));
        }


        [Authorize]
        // GET: Customers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            int userId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Sid).Value);
            int ID = -1;
            if (id == null)
            {
                ID = userId;
            }
            else
            {
                if (id == userId || User.IsInRole("Admin"))
                    ID = (int)id;
                else
                    return Redirect("/error/denied");
            }

            var customer = await _context.Customers.FindAsync(ID);
            if (customer == null)
            {
                return NotFound();
            }
            return View(customer);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Email,FirstName,LastName,BirthDate,Password")] Customer customer)
        {
            int userId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Sid).Value);
            if (id != customer.ID)
            {
                return NotFound();
            }
            if (userId != id && !User.IsInRole("Admin"))
            {
                return Redirect("/error/denied");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    Customer currentCustomer = _context.Customers.AsNoTracking().FirstOrDefault(c => c.ID == id);
                    customer.IsAdmin = currentCustomer.IsAdmin;
                    _context.Update(customer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CustomerExists(customer.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                if (User.IsInRole("Admin"))
                    return RedirectToAction(nameof(List));
                return RedirectToAction(nameof(Details));
            }
            return View(customer);
        }


        private bool CustomerExists(int id)
        {
            return _context.Customers.Any(e => e.ID == id);
        }
    }
}

