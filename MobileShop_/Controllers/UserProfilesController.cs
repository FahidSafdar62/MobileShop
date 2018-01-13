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
    public class UserProfilesController : Controller
    {
        private readonly MobileContext _context;

        public UserProfilesController(MobileContext context)
        {
            _context = context;
        }

        // GET: UserProfiles
        public async Task<IActionResult> Index()
        {
            return View(await _context.UserProfile.ToListAsync());
        }

        // GET: UserProfiles/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userProfile = await _context.UserProfile
                .SingleOrDefaultAsync(m => m.UserId == id);
            if (userProfile == null)
            {
                return NotFound();
            }

            return View(userProfile);
        }

        // GET: UserProfiles/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: UserProfiles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(UserProfile obj)
        {
			_context.UserProfile.Add(obj);
			_context.SaveChanges();
			return RedirectToAction("Index");
		}
		[HttpGet]
		public async Task<IActionResult> Edit(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var UserProfiles = await _context.UserProfile.SingleOrDefaultAsync(m => m.UserId == id);
			if (UserProfiles == null)
			{
				return NotFound();
			}
			
			return View(UserProfiles);
		}
		// POST: UserProfiles/Edit/5
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("UserId,UserName,Password,UserRole")] UserProfile userProfile)
        {
            if (id != userProfile.UserId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(userProfile);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserProfileExists(userProfile.UserId))
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
            return View(userProfile);
        }

        // GET: UserProfiles/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userProfile = await _context.UserProfile
                .SingleOrDefaultAsync(m => m.UserId == id);
            if (userProfile == null)
            {
                return NotFound();
            }

            return View(userProfile);
        }

        // POST: UserProfiles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var userProfile = await _context.UserProfile.SingleOrDefaultAsync(m => m.UserId == id);
            _context.UserProfile.Remove(userProfile);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserProfileExists(int id)
        {
            return _context.UserProfile.Any(e => e.UserId == id);
        }
		public IActionResult Dasboard()
		{
			return View();
		}
		public int CatagoryCount()
		{
			return _context.Catagory.ToList<Catagory>().Count;

		}
		public int ItemsCount()
		{
			return _context.Items.ToList<Items>().Count;

		}
		public int PurchaseCount()
		{
			return _context.Purchase.ToList<Purchase>().Count;

		}
		public int SalesCount()
		{
			return _context.Sales.ToList<Sales>().Count;

		}
		public int VenderCount()
		{
			return _context.Vender.ToList<Vender>().Count;

		}
		public int CustomerCount()
		{
			return _context.Customer.ToList<Customer>().Count;

		}
		[HttpGet]
		public IActionResult checklogin()
		{
			return View();
		}
		[HttpPost]
		public IActionResult checklogin(UserProfile obj)
		{
			UserProfile obje = _context.UserProfile.Where(abc => abc.UserName == obj.UserName).FirstOrDefault<UserProfile>();
			if (obje.Password == obj.Password)
			{
				return RedirectToAction("Dasboard");
			}
			else
			{
				return View();
			}

		}
		
		public IActionResult NewLogin()
		{
			return View();
		}
		
		public IActionResult NewLoginByFahid(string username, string password )
		{
			UserProfile obj = _context.UserProfile.Where(abc => abc.UserName == username).FirstOrDefault<UserProfile>();
			if(obj.Password == password)
			{
				return RedirectToAction("Dasboard");
			}
			else
			{
				return View();
			}
			
		}
	}
}
