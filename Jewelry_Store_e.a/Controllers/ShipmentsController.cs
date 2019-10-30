using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Jewelry_Store_e.a.Data;
using Jewelry_Store_e.a.Models;
using Microsoft.AspNetCore.Authorization;

namespace Jewelry_Store_e.a.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ShipmentsController : BaseController
    {
        public ShipmentsController(JewelryContext context) : base(context)
        {
        }
        [AllowAnonymous]
        public IActionResult AjaxAsync()
        {
            return View();
        }

        // GET: Shipments
        public async Task<IActionResult> Index()
        {
            return View(await _context.Shipments.ToListAsync());
        }

        // GET: Shipments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return Redirect("/error/PageNotFound");
            }

            var shipment = await _context.Shipments
                .FirstOrDefaultAsync(m => m.ShipmentID == id);
            if (shipment == null)
            {
                return Redirect("/error/PageNotFound");
            }

            return View(shipment);
        }

        // GET: Shipments/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Shipments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ShipmentID,Address")] Shipment shipment)
        {
            if (ModelState.IsValid)
            {
                _context.Add(shipment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(shipment);
        }

        // GET: Shipments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return Redirect("/error/PageNotFound");
            }

            var shipment = await _context.Shipments.FindAsync(id);
            if (shipment == null)
            {
                return Redirect("/error/PageNotFound");
            }
            return View(shipment);
        }

        // POST: Shipments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ShipmentID,Address")] Shipment shipment)
        {
            if (id != shipment.ShipmentID)
            {
                return Redirect("/error/PageNotFound");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(shipment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ShipmentExists(shipment.ShipmentID))
                    {
                        return Redirect("/error/PageNotFound");
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(shipment);
        }

        // GET: Shipments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return Redirect("/error/PageNotFound");
            }

            var shipment = await _context.Shipments
                .FirstOrDefaultAsync(m => m.ShipmentID == id);
            if (shipment == null)
            {
                return Redirect("/error/PageNotFound");
            }

            return View(shipment);
        }

        // POST: Shipments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var shipment = await _context.Shipments.FindAsync(id);
            _context.Shipments.Remove(shipment);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ShipmentExists(int id)
        {
            return _context.Shipments.Any(e => e.ShipmentID == id);
        }
    }
}
