using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MobileShop_.Models;
using System.Net.Mail;

namespace MobileShop_.Controllers
{
    public class SalesController : Controller
    {
        private readonly MobileContext _context;

        public SalesController(MobileContext context)
        {
            _context = context;
        }

        // GET: Sales
        public async Task<IActionResult> Index()
        {
            var mobileContext = _context.Sales.Include(s => s.Customer).Include(s => s.Items);
            return View(await mobileContext.ToListAsync());
        }

        // GET: Sales/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sales = await _context.Sales
                .Include(s => s.Customer)
                .Include(s => s.Items)
                .SingleOrDefaultAsync(m => m.SalesId == id);
            if (sales == null)
            {
                return NotFound();
            }

            return View(sales);
        }

        // GET: Sales/Create
        public IActionResult Create()
        {
            ViewData["CustomerId"] = new SelectList(_context.Customer, "CustomerId", "CustomerName");
            ViewData["ItemsId"] = new SelectList(_context.Items, "ItemsId", "ItemsName");
            return View();
        }

        // POST: Sales/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SalesId,CustomerId,ItemsId,Quantity,PricePerUnit,TotalPrice,TrDate")] Sales sales)
        {
			Items oitem = _context.Items.Where(abc => abc.ItemsId == Convert.ToInt32(sales.ItemsId)).FirstOrDefault<Items>();
			int a = Convert.ToInt32(oitem.Quantity) - Convert.ToInt32(sales.Quantity);
			oitem.Quantity = a;
			sendMail(sales);
			_context.Items.Update(oitem);
			if (ModelState.IsValid)
            {
                _context.Add(sales);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CustomerId"] = new SelectList(_context.Customer, "CustomerId", "CustomerId", sales.CustomerId);
            ViewData["ItemsId"] = new SelectList(_context.Items, "ItemsId", "ItemsId", sales.ItemsId);
            return View(sales);
        }

        // GET: Sales/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sales = await _context.Sales.SingleOrDefaultAsync(m => m.SalesId == id);
            if (sales == null)
            {
                return NotFound();
            }
            ViewData["CustomerId"] = new SelectList(_context.Customer, "CustomerId", "CustomerId", sales.CustomerId);
            ViewData["ItemsId"] = new SelectList(_context.Items, "ItemsId", "ItemsId", sales.ItemsId);
            return View(sales);
        }

        // POST: Sales/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SalesId,CustomerId,ItemsId,Quantity,PricePerUnit,TotalPrice,TrDate")] Sales sales)
        {
            if (id != sales.SalesId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sales);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SalesExists(sales.SalesId))
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
            ViewData["CustomerId"] = new SelectList(_context.Customer, "CustomerId", "CustomerId", sales.CustomerId);
            ViewData["ItemsId"] = new SelectList(_context.Items, "ItemsId", "ItemsId", sales.ItemsId);
            return View(sales);
        }

        // GET: Sales/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sales = await _context.Sales
                .Include(s => s.Customer)
                .Include(s => s.Items)
                .SingleOrDefaultAsync(m => m.SalesId == id);
            if (sales == null)
            {
                return NotFound();
            }

            return View(sales);
        }

        // POST: Sales/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var sales = await _context.Sales.SingleOrDefaultAsync(m => m.SalesId == id);
            _context.Sales.Remove(sales);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SalesExists(int id)
        {
            return _context.Sales.Any(e => e.SalesId == id);
        }
		public void sendMail(Sales Obj)
		{
			try
			{
				//Customer customer = _context.Customer.Where(m => m.== Obj.CustomerId).FirstOrDefault<Customer>();
				//setting up Mail Message
				MailMessage oMail = new MailMessage();
				oMail.Subject = "Sales Notification";
				oMail.Body = "Dear Customer,<br><br> you have bought " + Obj.ItemsId + " with worth of " + Obj.TotalPrice + "." +
					"I hope you will enjoy this " + Obj.ItemsId + ".For Any Complaint and Suggestion Please Contact with YourSelf ";
				oMail.IsBodyHtml = true;
				oMail.To.Add(new MailAddress("Fahidsafdar@gmail.com"));
				oMail.From = new MailAddress("Fahidbatthbatth@gmail.com", "MobileShop");
				//setting up SMTP Client
				SmtpClient oSmtp = new SmtpClient();
				oSmtp.Port = 587;
				oSmtp.EnableSsl = true;
				oSmtp.Host = "smtp.gmail.com";
				//Giving Creditientails to Mails
				oSmtp.Credentials = new System.Net.NetworkCredential("Fahidbatthbatth@gmail.com", "zahid62620");
				oSmtp.Send(oMail);

			}
			catch (Exception ex)
			{

			}
		}
	}
}
