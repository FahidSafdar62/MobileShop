using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MobileShop_.Models;

namespace MobileShop_.Controllers
{
    public class VendersController : Controller
    {
        private readonly MobileContext _context;

        public VendersController(MobileContext context)
        {
            _context = context;
        }

        // GET: Venders
        public async Task<IActionResult> Index()
        {
            return View(await _context.Vender.ToListAsync());
        }

        // GET: Venders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vender = await _context.Vender
                .SingleOrDefaultAsync(m => m.Vid == id);
            if (vender == null)
            {
                return NotFound();
            }

            return View(vender);
        }

        // GET: Venders/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Venders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Vid,Vname,Vmobile")] Vender vender)
        {
            if (ModelState.IsValid)
            {
                _context.Add(vender);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(vender);
        }

        // GET: Venders/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vender = await _context.Vender.SingleOrDefaultAsync(m => m.Vid == id);
            if (vender == null)
            {
                return NotFound();
            }
            return View(vender);
        }

        // POST: Venders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Vid,Vname,Vmobile")] Vender vender)
        {
            if (id != vender.Vid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(vender);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VenderExists(vender.Vid))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(vender);
        }

        // GET: Venders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vender = await _context.Vender
                .SingleOrDefaultAsync(m => m.Vid == id);
            if (vender == null)
            {
                return NotFound();
            }

            return View(vender);
        }

        // POST: Venders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var vender = await _context.Vender.SingleOrDefaultAsync(m => m.Vid == id);
            _context.Vender.Remove(vender);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VenderExists(int id)
        {
            return _context.Vender.Any(e => e.Vid == id);
        }
    }
}
