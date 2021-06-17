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
    public class FacultiesController : Controller
    {
        private readonly OTMContext _context;

        public FacultiesController(OTMContext context)
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
            int id= _context.Faculties.Where(f => f.type == "Faculty").Select(f => f.Id).FirstOrDefault();
            if (id <= 0)
                return false;
            return true;
        }
        public IActionResult Students() 
        {

            if (!facultyExist() || !IsFaculty())
                return RedirectToAction("Logout", "Account");
            int id = _context.Faculties.Where(f => f.rollno == User.Identity.Name).Select(f => f.DepartmentId).FirstOrDefault();
            var oTMContext = _context.Students.Include(s => s.department).Where(s=>s.DepartmentId == id).Include(s => s.semester);
            return View(oTMContext.ToList());
        }
        public IActionResult Approve(string rollno)
        {
            if (!facultyExist() || !IsFaculty())
                return RedirectToAction("Logout", "Account");
            Student s = _context.Students.Where(s=>s.rollno == rollno).Select(s=>s).FirstOrDefault();
            s.regornot = 1;
            _context.Students.Update(s);
            _context.SaveChanges();

            var oTMContext = _context.Students.Include(s => s.department).Include(s => s.semester);
            
            return View("Students",oTMContext.ToList());
        }
        // GET: Faculties
        public IActionResult Index()
        {
            if (!facultyExist() || !IsFaculty())
                return RedirectToAction("Logout", "Account");
            //var oTMContext = _context.Faculties.Include(f => f.department);
            //return View(await oTMContext.ToListAsync());
            Faculty f = _context.Faculties.Where(f => f.rollno == User.Identity.Name).FirstOrDefault();
            ViewData["welcome"] = "Welcome, " + f.name + " !!!";
            return View();
        }
        public async Task<IActionResult> Details()
        {

            if (!facultyExist() || !IsFaculty())
                return RedirectToAction("Logout", "Account");
            int id = _context.Faculties.Where(f => f.rollno == User.Identity.Name.ToString()).Where(f => f.type.ToLower() == "faculty").Select(f => f.Id).FirstOrDefault();
            var faculty = await _context.Faculties
                .Include(f => f.department)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (faculty == null)
            {
                return NotFound();
            }
            ViewData["DeptSname"] = _context.Departments.Where(d => d.Id == faculty.DepartmentId).Select(d => d.DeptSname).FirstOrDefault();
            return View(faculty);
        }

        // GET: Faculties/Create
       /* public IActionResult Create()
        {
            ViewData["DepartmentId"] = new SelectList(_context.Departments, "Id", "Id");
            return View();
        }

        // POST: Faculties/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,rollno,name,type,mno,email,password,regornot,DepartmentId")] Faculty faculty)
        {
            if (ModelState.IsValid)
            {
                _context.Add(faculty);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DepartmentId"] = new SelectList(_context.Departments, "Id", "Id", faculty.DepartmentId);
            return View(faculty);
        }

        // GET: Faculties/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var faculty = await _context.Faculties.FindAsync(id);
            if (faculty == null)
            {
                return NotFound();
            }
            ViewData["DepartmentId"] = new SelectList(_context.Departments, "Id", "Id", faculty.DepartmentId);
            return View(faculty);
        }

        // POST: Faculties/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,rollno,name,type,mno,email,password,regornot,DepartmentId")] Faculty faculty)
        {
            if (id != faculty.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(faculty);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FacultyExists(faculty.Id))
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
            ViewData["DepartmentId"] = new SelectList(_context.Departments, "Id", "Id", faculty.DepartmentId);
            return View(faculty);
        }

        // GET: Faculties/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var faculty = await _context.Faculties
                .Include(f => f.department)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (faculty == null)
            {
                return NotFound();
            }

            return View(faculty);
        }

        // POST: Faculties/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var faculty = await _context.Faculties.FindAsync(id);
            _context.Faculties.Remove(faculty);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }*/

        private bool FacultyExists(int id)
        {
            return _context.Faculties.Any(e => e.Id == id);
        }
    }
}
