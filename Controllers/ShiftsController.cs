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
    public class ShiftsController : Controller
    {
        private readonly DataContext _context;


        public ShiftsController(DataContext context)
        {
            _context = context;
        }

        // GET: Shifts
        public async Task<IActionResult> Index()
        {
            return View(await _context.Shifts.ToListAsync());
        }

        // GET: Shifts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shift = await _context.Shifts
                .FirstOrDefaultAsync(m => m.ID == id);
            if (shift == null)
            {
                return NotFound();
            }

            return View(shift);
        }

        // GET: Shifts/Create
        public IActionResult Create()
        {
            //var model = new Shift();

            ViewBag.ShiftWorker = GetWorkers().Select(x => new SelectListItem()
            {
                Text = x.Name,
                Value = x.ID.ToString()
            }).ToList();

            ViewBag.Tasks = GetTasks().Select(x => new SelectListItem()
            {
                Text = x.Title,
                Value = x.ID.ToString()
            }).ToList();

            return View();
        }

        // POST: Shifts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,ShiftWorker,ShiftStart,ShiftEnd,Tasks, ShiftWorkerID, TasksID")] Shift shift)
        {

            ViewBag.ShiftWorker = GetWorkers().Select(x => new SelectListItem()
            {
                Text = x.Name,
                Value = x.ID.ToString()
            }).ToList();

            ViewBag.Tasks = GetTasks().Select(x => new SelectListItem()
            {
                Text = x.Title,
                Value = x.ID.ToString()
            }).ToList();

            shift.ShiftWorkerID = Int32.Parse(shift.ShiftWorker);
            shift.TasksID = "";
            bool validAge = false;
            Worker selectedWorker = new Worker();
            var taskList = GetTasks().Select(x => new SelectListItem()
                                    {
                                        Text = x.Title,
                                        Value = x.ID.ToString()
                                    }).ToList();

            List<TodoThings> selectedTasksObj = new List<TodoThings>();

            string[] selectedTasks = Request.Form["lstTasks"].ToString().Split(",");

            bool isEmpty = true;

            foreach (string s in selectedTasks) {
                if (s == "" && selectedTasks.Length == 1)
                {
                    isEmpty = true;
                }
                else {
                    isEmpty = false;
                }
            }

            foreach (Worker w in _context.Workers) {
                if (shift.ShiftWorkerID == w.ID) {
                    shift.ShiftWorker = w.Name;
                    selectedWorker = w;
                }
            }

            if (!isEmpty) {             
                foreach (string s in selectedTasks) {             
                    foreach (TodoThings t in _context.Tasks)
                    {
                        if (int.Parse(s) == t.ID) {
                            selectedTasksObj.Add(t);
                        }
                    }
                }

                TodoThings lastSelectedTasks = selectedTasksObj.Last();

                foreach (TodoThings t in selectedTasksObj)
                {
                    if (t == lastSelectedTasks)
                    {
                        shift.Tasks += t.Title;
                        shift.TasksID += t.ID;
                    }
                    else
                    {
                        shift.Tasks += t.Title + ", ";
                        shift.TasksID += t.ID + ",";
                    }
                }
            }

            foreach (TodoThings t in selectedTasksObj)
            {
                if (selectedWorker.Age >= t.AgeRestriction)
                {
                    validAge = true;
                }
                else
                {
                    validAge = false;
                }
            }


            if (ModelState.IsValid)
            {
                if (validAge && shift.Tasks !="")
                {
                    _context.Add(shift);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    if (!validAge) { 
                        ModelState.AddModelError("", "Aldersgrænsen er ikke overholdt");
                    }
                    if (shift.Tasks == "" || shift.Tasks == null) {
                        ModelState.AddModelError("", "Vælg venligt mindst en opgave");
                    }
                }
            }
            return View();
        }

        private List<Worker> GetWorkers() {
            List<Worker> workerList = new List<Worker>();

            foreach (Worker w in _context.Workers)
            {
                workerList.Add(w);
            }

            return workerList;
        }

        private List<TodoThings> GetTasks()
        {
            List<TodoThings> tasks = new List<TodoThings>();

            foreach (TodoThings t in _context.Tasks)
            {
                tasks.Add(t);
            }

            return tasks;
        }

        // GET: Shifts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shift = await _context.Shifts.FindAsync(id);
            if (shift == null)
            {
                return NotFound();
            }
            return View(shift);
        }

        // POST: Shifts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,ShiftWorker,ShiftStart,ShiftEnd,Tasks, ShiftWorkerID, TasksID")] Shift shift)
        {
            if (id != shift.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(shift);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ShiftExists(shift.ID))
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
            return View(shift);
        }

        // GET: Shifts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shift = await _context.Shifts
                .FirstOrDefaultAsync(m => m.ID == id);
            if (shift == null)
            {
                return NotFound();
            }

            return View(shift);
        }

        // POST: Shifts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var shift = await _context.Shifts.FindAsync(id);
            _context.Shifts.Remove(shift);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ShiftExists(int id)
        {
            return _context.Shifts.Any(e => e.ID == id);
        }
    }
}
