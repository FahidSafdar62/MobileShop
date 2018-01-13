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
    public class PurchasesController : Controller
    {
        private readonly MobileContext _context;

        public PurchasesController(MobileContext context)
        {
            _context = context;
        }

        // GET: Purchases
        public async Task<IActionResult> Index()
        {
            var mobileContext = _context.Purchase.Include(p => p.Items).Include(p => p.Vender);
            return View(await mobileContext.ToListAsync());
        }

        // GET: Purchases/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var purchase = await _context.Purchase
                .Include(p => p.Items)
                .Include(p => p.Vender)
                .SingleOrDefaultAsync(m => m.PurchaseId == id);
            if (purchase == null)
            {
                return NotFound();
            }

            return View(purchase);
        }

        // GET: Purchases/Create
        public IActionResult Create()
        {
            
			ViewBag.ItemsId = new SelectList(_context.Items, "ItemsId", "ItemsName");
			ViewBag.VenderId = new SelectList(_context.Vender, "Vid", "Vname");


			return View();
        }

        // POST: Purchases/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PurchaseId,VenderId,ItemsId,Pquanity,PricePerUnit,TotalPrice,Date")] Purchase purchase)
        {
			Items oitem = _context.Items.Where(abc => abc.ItemsId == Convert.ToInt32(purchase.ItemsId)).FirstOrDefault<Items>();
			int a = Convert.ToInt32(oitem.Quantity) + Convert.ToInt32(purchase.Pquanity);
			oitem.Quantity = a;
			_context.Items.Update(oitem);

			if (ModelState.IsValid)
            {
                _context.Add(purchase);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ItemsId"] = new SelectList(_context.Items, "ItemsId", "ItemsId", purchase.ItemsId);
            ViewData["VenderId"] = new SelectList(_context.Vender, "Vid", "Vid", purchase.VenderId);
            return View(purchase);
        }

        // GET: Purchases/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var purchase = await _context.Purchase.SingleOrDefaultAsync(m => m.PurchaseId == id);
            if (purchase == null)
            {
                return NotFound();
            }
            ViewData["ItemsId"] = new SelectList(_context.Items, "ItemsId", "ItemsId", purchase.ItemsId);
            ViewData["VenderId"] = new SelectList(_context.Vender, "Vid", "Vid", purchase.VenderId);
            return View(purchase);
        }

        // POST: Purchases/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PurchaseId,VenderId,ItemsId,Pquanity,PricePerUnit,TotalPrice,Date")] Purchase purchase)
        {
            if (id != purchase.PurchaseId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(purchase);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PurchaseExists(purchase.PurchaseId))
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
            ViewData["ItemsId"] = new SelectList(_context.Items, "ItemsId", "ItemsId", purchase.ItemsId);
            ViewData["VenderId"] = new SelectList(_context.Vender, "Vid", "Vid", purchase.VenderId);
            return View(purchase);
        }

        // GET: Purchases/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var purchase = await _context.Purchase
                .Include(p => p.Items)
                .Include(p => p.Vender)
                .SingleOrDefaultAsync(m => m.PurchaseId == id);
            if (purchase == null)
            {
                return NotFound();
            }

            return View(purchase);
        }

        // POST: Purchases/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var purchase = await _context.Purchase.SingleOrDefaultAsync(m => m.PurchaseId == id);
            _context.Purchase.Remove(purchase);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PurchaseExists(int id)
        {
            return _context.Purchase.Any(e => e.PurchaseId == id);
        }
    }
}
