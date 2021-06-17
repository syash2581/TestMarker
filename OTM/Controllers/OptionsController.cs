using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OTM.Data;
using OTM.Models;

namespace OTM.Controllers
{
    public class OptionsController : Controller
    {
        private readonly OTMContext _context;
        static int qid;
        public OptionsController(OTMContext context)
        {
            _context = context;
        }
        public bool facultyExist()
        {
            int id = _context.Faculties.Where(f => f.rollno == User.Identity.Name).Select(f => f.Id).FirstOrDefault();
            if (id <= 0)
                return false;
            return true;
        }
        public bool IsFaculty()
        {
            int id = _context.Faculties.Where(f => f.type == "Faculty").Select(f => f.Id).FirstOrDefault();
            if (id <= 0)
                return false;
            return true;
        }

        // GET: Options
        public async Task<IActionResult> Index(int? id)
        {
            if (!facultyExist() || !IsFaculty())
                return RedirectToAction("Logout", "Account");
            if (id == null)
            {

            }
            else
            {
                qid =(int)id;
            }
            
            ViewData["qid"] = qid;
            ViewData["tid"] = _context.Questions.Where(Q => Q.Id == qid).Select(Q => Q.TestId).FirstOrDefault();
            var oTMContext = _context.Options.Where(o=>o.QuestionId == qid).Include(o => o.Question);
            return View(await oTMContext.ToListAsync());
        }

        // GET: Options/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (!facultyExist() || !IsFaculty())
                return RedirectToAction("Logout", "Account");
            if (id == null)
            {
                return NotFound();
            }

            var options = await _context.Options
                .Include(o => o.Question)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (options == null)
            {
                return NotFound();
            }

            return View(options);
        }

        // GET: Options/Create
        public IActionResult Create()
        {
            if (!facultyExist() || !IsFaculty())
                return RedirectToAction("Logout", "Account");
            ViewData["QuestionId"] = new SelectList(_context.Questions, "Id", "Id");
            return View();
        }

        // POST: Options/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Option,Description,Correct,QuestionId")] Options options)
        {
            if (!facultyExist() || !IsFaculty())
                return RedirectToAction("Logout", "Account");
            if (ModelState.IsValid)
            {
                options.QuestionId = qid;
                _context.Add(options);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index",qid);
            }
            ViewData["QuestionId"] = new SelectList(_context.Questions, "Id", "Id", options.QuestionId);
            return View(options);
        }

        // GET: Options/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (!facultyExist() || !IsFaculty())
                return RedirectToAction("Logout", "Account");
            if (id == null)
            {
                return NotFound();
            }

            var options = await _context.Options.FindAsync(id);
            if (options == null)
            {
                return NotFound();
            }
            ViewData["QuestionId"] = new SelectList(_context.Questions, "Id", "Id", options.QuestionId);
            return View(options);
        }

        // POST: Options/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Option,Description,Correct,QuestionId")] Options options)
        {
            if (!facultyExist() || !IsFaculty())
                return RedirectToAction("Logout", "Account");
            if (id != options.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    options.QuestionId = qid;
                    _context.Update(options);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OptionsExists(options.Id))
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
            ViewData["QuestionId"] = new SelectList(_context.Questions, "Id", "Id", options.QuestionId);
            return View(options);
        }

        // GET: Options/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (!facultyExist() || !IsFaculty())
                return RedirectToAction("Logout", "Account");
            if (id == null)
            {
                return NotFound();
            }

            var options = await _context.Options
                .Include(o => o.Question)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (options == null)
            {
                return NotFound();
            }

            return View(options);
        }

        // POST: Options/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (!facultyExist() || !IsFaculty())
                return RedirectToAction("Logout", "Account");
            var options = await _context.Options.FindAsync(id);
            _context.Options.Remove(options);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OptionsExists(int id)
        {
            return _context.Options.Any(e => e.Id == id);
        }
    }
}
