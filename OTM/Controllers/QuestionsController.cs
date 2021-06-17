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
    public class QuestionsController : Controller
    {
        private readonly OTMContext _context;
        static int tid;
        public QuestionsController(OTMContext context)
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
        // GET: Questions
        public async Task<IActionResult> Index(int? id)
        {
            if (!facultyExist() || !IsFaculty())
                return RedirectToAction("Logout", "Account");
            if (id == null)
            {

            }
            else
            {
                tid = (int)id;
            }
            
            var oTMContext = _context.Questions.Where(Q=>Q.TestId == tid).Include(q => q.Test);
            return View(await oTMContext.ToListAsync());
        }

        // GET: Questions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (!facultyExist() || !IsFaculty())
                return RedirectToAction("Logout", "Account");
            if (id == null)
            {
                return NotFound();
            }

            var question = await _context.Questions
                .Include(q => q.Test)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (question == null)
            {
                return NotFound();
            }

            return View(question);
        }

        // GET: Questions/Create
        public IActionResult Create()
        {
            if (!facultyExist() || !IsFaculty())
                return RedirectToAction("Logout", "Account");
            ViewData["TestId"] = new SelectList(_context.Tests, "Id", "Id");
            return View();
        }

        // POST: Questions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,question,QuestionBrief,Marks,TestId")] Question q)
        {
            if (!facultyExist() || !IsFaculty())
                return RedirectToAction("Logout", "Account");
            if (ModelState.IsValid)
            {
                q.TestId = tid;
                _context.Add(q);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index",tid);
            }
            ViewData["TestId"] = new SelectList(_context.Tests, "Id", "Id", q.TestId);
            return View(q);
        }

        // GET: Questions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (!facultyExist() || !IsFaculty())
                return RedirectToAction("Logout", "Account");
            if (id == null)
            {
                return NotFound();
            }

            var question = await _context.Questions.FindAsync(id);
            if (question == null)
            {
                return NotFound();
            }
            ViewData["TestId"] = new SelectList(_context.Tests, "Id", "Id", question.TestId);
            return View(question);
        }

        // POST: Questions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,question,QuestionBrief,Marks,TestId")] Question q)
        {
            if (!facultyExist() || !IsFaculty())
                return RedirectToAction("Logout", "Account");
            if (id != q.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    q.TestId = tid;
                    _context.Update(q);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!QuestionExists(q.Id))
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
            ViewData["TestId"] = new SelectList(_context.Tests, "Id", "Id", q.TestId);
            return View(q);
        }

        // GET: Questions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (!facultyExist() || !IsFaculty())
                return RedirectToAction("Logout", "Account");
            if (id == null)
            {
                return NotFound();
            }

            var question = await _context.Questions
                .Include(q => q.Test)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (question == null)
            {
                return NotFound();
            }

            return View(question);
        }

        // POST: Questions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (!facultyExist() || !IsFaculty())
                return RedirectToAction("Logout", "Account");
            var question = await _context.Questions.FindAsync(id);
            _context.Questions.Remove(question);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool QuestionExists(int id)
        {
            return _context.Questions.Any(e => e.Id == id);
        }
    }
}
