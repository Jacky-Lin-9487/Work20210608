using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Work20210608.Data;
using Work20210608.Models;

namespace Work20210608.Controllers
{
    public class CRUDsController : Controller
    {
        private readonly Work20210608Context _context;

        public CRUDsController(Work20210608Context context)
        {
            _context = context;
        }

        // GET: CRUDs
        public async Task<IActionResult> Index()
        {
            return View(await _context.CRUD.ToListAsync());
        }

        // GET: CRUDs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cRUD = await _context.CRUD
                .FirstOrDefaultAsync(m => m.ID == id);
            if (cRUD == null)
            {
                return NotFound();
            }

            return View(cRUD);
        }

        // GET: CRUDs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CRUDs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Name")] CRUD cRUD)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cRUD);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(cRUD);
        }

        // GET: CRUDs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cRUD = await _context.CRUD.FindAsync(id);
            if (cRUD == null)
            {
                return NotFound();
            }
            return View(cRUD);
        }

        // POST: CRUDs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Name")] CRUD cRUD)
        {
            if (id != cRUD.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cRUD);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CRUDExists(cRUD.ID))
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
            return View(cRUD);
        }

        // GET: CRUDs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cRUD = await _context.CRUD
                .FirstOrDefaultAsync(m => m.ID == id);
            if (cRUD == null)
            {
                return NotFound();
            }

            return View(cRUD);
        }

        // POST: CRUDs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cRUD = await _context.CRUD.FindAsync(id);
            _context.CRUD.Remove(cRUD);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CRUDExists(int id)
        {
            return _context.CRUD.Any(e => e.ID == id);
        }
    }
}
