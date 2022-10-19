using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DigitalPartnersTask.Data;
using DigitalPartnersTask.Models;

namespace DigitalPartnersTask.Controllers
{
    public class TodoThingsController : Controller
    {
        private readonly DataContext _context;

        public TodoThingsController(DataContext context)
        {
            _context = context;
        }

        // GET: TodoThings
        public async Task<IActionResult> Index()
        {
            return View(await _context.Tasks.ToListAsync());
        }

        // GET: TodoThings/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var todoThings = await _context.Tasks
                .FirstOrDefaultAsync(m => m.ID == id);
            if (todoThings == null)
            {
                return NotFound();
            }

            return View(todoThings);
        }

        // GET: TodoThings/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TodoThings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Title,AgeRestriction")] TodoThings todoThings)
        {
            if (ModelState.IsValid)
            {
                _context.Add(todoThings);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(todoThings);
        }

        // GET: TodoThings/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var todoThings = await _context.Tasks.FindAsync(id);
            if (todoThings == null)
            {
                return NotFound();
            }
            return View(todoThings);
        }

        // POST: TodoThings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Title,AgeRestriction")] TodoThings todoThings)
        {
            if (id != todoThings.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(todoThings);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TodoThingsExists(todoThings.ID))
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
            return View(todoThings);
        }

        // GET: TodoThings/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var todoThings = await _context.Tasks
                .FirstOrDefaultAsync(m => m.ID == id);
            if (todoThings == null)
            {
                return NotFound();
            }

            return View(todoThings);
        }

        // POST: TodoThings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var todoThings = await _context.Tasks.FindAsync(id);
            _context.Tasks.Remove(todoThings);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TodoThingsExists(int id)
        {
            return _context.Tasks.Any(e => e.ID == id);
        }
    }
}
