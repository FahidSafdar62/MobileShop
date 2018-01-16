using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MobileShop_.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace MobileShop_.Controllers
{
	public class ItemsController : Controller
	{
		private readonly MobileContext _context;
		private IHostingEnvironment env;

		public ItemsController(MobileContext context, IHostingEnvironment _env)
		{
			_context = context;
			env = _env;
		}

		// GET: Items
		public async Task<IActionResult> Index()
		{
			var mobileContext = _context.Items.Include(i => i.Catagory);
			return View(await mobileContext.ToListAsync());
		}

		// GET: Items/Details/5
		public async Task<IActionResult> Details(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var items = await _context.Items
				.Include(i => i.Catagory)
				.SingleOrDefaultAsync(m => m.ItemsId == id);
			if (items == null)
			{
				return NotFound();
			}

			return View(items);
		}

		// GET: Items/Create
		public IActionResult Create()
		{
			ViewBag.CatagoryName = new SelectList(_context.Catagory, "CatagoryId", "CatagoryName");
			return View();
		}

		// POST: Items/Create
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create([Bind("ItemsId,CatagoryId,CatagoryName,ItemsName,Description,Quantity,Price,Image,Model,Color")] Items items, ICollection<IFormFile> Image)
		{
			string wwwrootPath = env.WebRootPath;
			string PPFolderPath = wwwrootPath + "/ItemsImage/";

			foreach (var file in Image)
			{
				string Name = file.Name;
				string FileName = file.FileName;
				long FileLength = file.Length;

				string FileNameWithoutExtension = Path.GetFileNameWithoutExtension(FileName);
				Random r = new Random();

				FileNameWithoutExtension = DateTime.Now.ToString("ddMMyyyyhhmm") + r.Next(1, 1000).ToString();
				string Extension = Path.GetExtension(FileName);

				FileStream fs = new FileStream(PPFolderPath + FileNameWithoutExtension + Extension, FileMode.CreateNew);
				file.CopyTo(fs);
				fs.Close();
				fs.Dispose();


				items.Image = "~/ItemsImage/" + FileNameWithoutExtension + Extension;
			}

			if (ModelState.IsValid)
			{
				_context.Add(items);
				await _context.SaveChangesAsync();
				return RedirectToAction(nameof(Index));
			}

			return View(items);
		}

		// GET: Items/Edit/5
		public async Task<IActionResult> Edit(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var items = await _context.Items.SingleOrDefaultAsync(m => m.ItemsId == id);
			if (items == null)
			{
				return NotFound();
			}
			ViewData["CatagoryName"] = new SelectList(_context.Catagory, "CatagoryId", "CatagoryName", items.Catagory);
			return View(items);
		}

		// POST: Items/Edit/5
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(int id, [Bind("ItemsId,CatagoryId,ItemsName,Description,Quantity,Price,Image,Model,Color")] Items items)
		{
			if (id != items.ItemsId)
			{
				return NotFound();
			}

			if (ModelState.IsValid)
			{
				try
				{
					_context.Update(items);
					await _context.SaveChangesAsync();
				}
				catch (DbUpdateConcurrencyException)
				{
					if (!ItemsExists(items.ItemsId))
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
			ViewData["CatagoryId"] = new SelectList(_context.Catagory, "CatagoryId", "CatagoryId", items.CatagoryId);
			return View(items);
		}

		// GET: Items/Delete/5
		public async Task<IActionResult> Delete(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var items = await _context.Items
				.Include(i => i.Catagory)
				.SingleOrDefaultAsync(m => m.ItemsId == id);
			if (items == null)
			{
				return NotFound();
			}

			return View(items);
		}

		// POST: Items/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(int id)
		{
			var items = await _context.Items.SingleOrDefaultAsync(m => m.ItemsId == id);
			_context.Items.Remove(items);
			await _context.SaveChangesAsync();
			return RedirectToAction(nameof(Index));
		}

		private bool ItemsExists(int id)
		{
			return _context.Items.Any(e => e.ItemsId == id);
		}

		public int ProductSaleCountAjax(int ItemsCode)
		{
			return _context.Sales.Where(p => p.ItemsId == ItemsCode).Count();
		}

		public string ProductOutOfStockCheck(int ItemsCode)
		{
			Items pd = _context.Items.Where(p => p.ItemsId == ItemsCode).SingleOrDefault();

			if (pd.Quantity == 0 || pd.Quantity == null)
			{
				return "OutStock";
			}
			else
				return "";
		}
	}
}

