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
    public class TestsController : Controller
    {
        private readonly OTMContext _context;

        public TestsController(OTMContext context)
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
        // GET: Tests
        public async Task<IActionResult> Index()
        {
            if (!facultyExist() || !IsFaculty())
                return RedirectToAction("Logout", "Account");
            int id = _context.Faculties.Where(f => f.rollno == User.Identity.Name).Select(f => f.Id).FirstOrDefault();

            var oTMContext = _context.Tests.Include(t => t.Faculty).Where(t=>t.FacultyId == id ).Include(t => t.semester);
            return View(await oTMContext.ToListAsync());
        }

        // GET: Tests/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (!facultyExist() || !IsFaculty())
                return RedirectToAction("Logout", "Account");
            if (id == null)
            {
                return NotFound();
            }

            var test = await _context.Tests
                .Include(t => t.Faculty)
                .Include(t => t.semester)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (test == null)
            {
                return NotFound();
            }

            return View(test);
        }

        // GET: Tests/Create
        public IActionResult Create()
        {
            if (!facultyExist() || !IsFaculty())
                return RedirectToAction("Logout", "Account");
            ViewData["FacultyId"] = new SelectList(_context.Faculties, "Id", "Id");
            ViewData["SemesterId"] = new SelectList(_context.semesters, "Id", "Id");
            return View();
        }

        // POST: Tests/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,TotalQuestions,Totalmarks,Testdate,Testduration,TestEndtime,FacultyId,SemesterId")] Test test)
        {
            if (!facultyExist() || !IsFaculty())
                return RedirectToAction("Logout", "Account");
            test.TestEndtime = test.Testduration;
            test.FacultyId = _context.Faculties.Where(f => f.rollno == User.Identity.Name.ToString()).Select(f => f.Id).FirstOrDefault();
            if (ModelState.IsValid)
            {
                _context.Add(test);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["FacultyId"] = new SelectList(_context.Faculties, "Id", "Id", test.FacultyId);
            ViewData["SemesterId"] = new SelectList(_context.semesters, "Id", "Id", test.SemesterId);
            return View(test);
        }

        // GET: Tests/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (!facultyExist() || !IsFaculty())
                return RedirectToAction("Logout", "Account");
            if (id == null)
            {
                return NotFound();
            }

            var test = await _context.Tests.FindAsync(id);
            if (test == null)
            {
                return NotFound();
            }
            ViewData["FacultyId"] = new SelectList(_context.Faculties, "Id", "Id", test.FacultyId);
            ViewData["SemesterId"] = new SelectList(_context.semesters, "Id", "Id", test.SemesterId);
            return View(test);
        }

        // POST: Tests/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Test test)
        {
            if (!facultyExist() || !IsFaculty())
                return RedirectToAction("Logout", "Account");
            if (id != test.Id)
            {
                return NotFound();
            }
            test.TestEndtime = test.Testduration;
            test.FacultyId = _context.Faculties.Where(f => f.rollno == User.Identity.Name.ToString()).Select(f => f.Id).FirstOrDefault();
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(test);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TestExists(test.Id))
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
            ViewData["FacultyId"] = new SelectList(_context.Faculties, "Id", "Id", test.FacultyId);
            ViewData["SemesterId"] = new SelectList(_context.semesters, "Id", "Id", test.SemesterId);
            return View(test);
        }

        // GET: Tests/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (!facultyExist() || !IsFaculty())
                return RedirectToAction("Logout", "Account");
            if (id == null)
            {
                return NotFound();
            }

            var test = await _context.Tests
                .Include(t => t.Faculty)
                .Include(t => t.semester)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (test == null)
            {
                return NotFound();
            }

            return View(test);
        }

        // POST: Tests/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (!facultyExist() || !IsFaculty())
                return RedirectToAction("Logout", "Account");
            var test = await _context.Tests.FindAsync(id);
            _context.Tests.Remove(test);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Result(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index", "Faculties");
            }
            List<TestResults> testResults = new List<TestResults>();
            var Test = _context.Tests.Where(t => t.Id == id).FirstOrDefault();
            var records = _context.StudentTests.Where(s => s.TestId == id).OrderBy(s => s.StudentId);
            int fdid = _context.Faculties.Where(f => f.Id == Test.FacultyId).Select(f => f.DepartmentId).FirstOrDefault();

            var students = _context.Students.Where(s => s.DepartmentId == fdid).Where(s=>s.SemesterId == Test.SemesterId).OrderBy(s=>s.rollno);

            foreach (var item in students)
            {
                TestResults tr = new TestResults();
                tr.rollno = item.rollno;
                tr.sname = item.Sname;

                var record = _context.StudentTests.Where(s => s.StudentId == item.Id).Where(s=>s.TestId == id).FirstOrDefault();
                if (record == null)
                    tr.marks = 0;
                else
                    tr.marks = record.results;
                testResults.Add(tr);
            }
            /*foreach (var item in records)
            {
                TestResults tr = new TestResults();
                tr.rollno = _context.Students.Where(s => s.Id == item.StudentId).Select(s => s.rollno).FirstOrDefault();
                tr.sname = _context.Students.Where(s => s.Id == item.StudentId).Select(s => s.Sname).FirstOrDefault();
                tr.marks = item.results;
                testResults.Add(tr);
            }*/


            ViewData["TestName"] = Test.Name;
            ViewData["TestDescription"] = Test.Description;
            ViewData["TestDate"] = Test.Testdate;
            ViewData["TestEndDate"] = Test.TestEndtime;
            ViewData["OutFrom"] = Test.Totalmarks;
            ViewData["totQUes"] = Test.TotalQuestions;
            ViewData["Fname"] = _context.Faculties.Where(f => f.rollno == User.Identity.Name.ToString()).Select(f => f.name).FirstOrDefault();
            return View(testResults);
        }
        private bool TestExists(int id)
        {
            return _context.Tests.Any(e => e.Id == id);
        }
    }
}
