using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OTM.Data;
using OTM.Models;

namespace OTM.Controllers
{

    public class StudentsController : Controller
    {
        private readonly OTMContext _context;
        static int testid;
        static int totalmarks;

        public StudentsController(OTMContext context)
        {
            _context = context;
        }
        public int StudentExist()
        {
            return _context.Students.Where(s => s.rollno == User.Identity.Name).Select(s => s.Id).FirstOrDefault();
        }

        // GET: Students
        public async Task<IActionResult> Index()
        {
            if (StudentExist() <= 0)
                return RedirectToAction("Logout", "Account");
            Student f = _context.Students.Where(f => f.rollno == User.Identity.Name).FirstOrDefault();
            ViewData["welcome"] = "Welcome, " + f.Sname +" !!!";
            var oTMContext = _context.Students.Include(s => s.department).Include(s => s.semester);
            return View(await oTMContext.ToListAsync());
        }
        // GET: Students/Details/5
        public async Task<IActionResult> Details()
        {
            if (StudentExist() <= 0)
                return RedirectToAction("Logout", "Account");
            /*if(rno == null)
            {
                return NotFound();
            }*/
            string rno = User.Identity.Name.ToString();
            int id = _context.Students.Where(s => s.rollno == rno).Select(S => S.Id).FirstOrDefault();
            var student = await _context.Students
                .Include(s => s.department)
                .Include(s => s.semester)
                .FirstOrDefaultAsync(m => m.Id == id);
            ViewData["DeptSname"] = _context.Departments.Where(d => d.Id == student.DepartmentId).Select(d => d.DeptSname).FirstOrDefault();

            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        public IActionResult Tests()
        {
            int ssem = _context.Students.Where(s => s.rollno == User.Identity.Name.ToString()).Select(s => s.SemesterId).FirstOrDefault();
            int sdid = _context.Students.Where(s => s.rollno == User.Identity.Name.ToString()).Select(s => s.DepartmentId).FirstOrDefault();
            string sdept = _context.Departments.Where(d => d.Id == sdid).Select(s => s.DeptSname).FirstOrDefault();
            
            List<Faculty> faculties;
            faculties = _context.Faculties.Where(f => f.DepartmentId == sdid).ToList();
            List<Test> tests=new List<Test>();
            tests.Clear();
            foreach (var item in _context.Tests.Where(t=>t.SemesterId == ssem).ToList())
            {
                var faculty = _context.Faculties.Where(f => f.Id == item.FacultyId).FirstOrDefault();
                if(faculties.Contains(faculty))
                {
                    tests.Add(item);
                }             
            }
            
            return View(tests.ToList());
        }
        public async Task<IActionResult> TestDetails(int? tid)
        {
            if (StudentExist() <= 0)
                return RedirectToAction("Logout", "Account");
            if (tid == null)
            {
                return NotFound();
            }
            testid = (int)tid;
            //student
            int ssem = _context.Students.Where(s => s.rollno == User.Identity.Name.ToString()).Select(s => s.SemesterId).FirstOrDefault();
            int id = _context.Students.Where(s => s.rollno == User.Identity.Name.ToString()).Select(S => S.Id).FirstOrDefault();
            int sdid = _context.Students.Where(s => s.Id == id).Select(s => s.DepartmentId).FirstOrDefault();
            string sdeptsname = _context.Departments.Where(S => S.Id == sdid).Select(S => S.DeptSname).FirstOrDefault();

            //Faculty
            int fid = _context.Tests.Where(t => t.Id == tid).Select(t => t.FacultyId).FirstOrDefault();
            string fname = _context.Faculties.Where(f => f.Id == fid).Select(f => f.name).FirstOrDefault();
            int fdid = _context.Faculties.Where(s => s.Id == fid).Select(s => s.DepartmentId).FirstOrDefault();
            string fdeptsname = _context.Departments.Where(f => f.Id == fdid).Select(f => f.DeptSname).FirstOrDefault();


            if (fdeptsname != null && sdeptsname != null && fdeptsname.Equals(sdeptsname))
            {
                var test = _context.Tests.Where(t => t.SemesterId == ssem).Where(f => f.FacultyId == fid).FirstOrDefault();

                if (test == null)
                {
                    return NotFound();
                }
                ViewData["Faculty"] = fname;
                return View(test);
            }
            return NotFound();
        }
        public async Task<IActionResult> Attend(int? tid)
        {
            if (StudentExist() <= 0)
                return RedirectToAction("Logout", "Account");
            if (tid == null)
            {
                return NotFound();
            }
            testid = (int)tid;
            Test t = _context.Tests.Where(t => t.Id == tid).FirstOrDefault();
            DateTime curDate = DateTime.Now;

            int hh;
            int mm;
            int ss;

            //constraint that bounds the test time.
            if(t.Testdate > curDate)
            {
                string error = t.Name + " " + t.Description + " has not started yet.";
                ViewData["Error"] = error;

                int ssem = _context.Students.Where(s => s.rollno == User.Identity.Name.ToString()).Select(s => s.SemesterId).FirstOrDefault();
                int sdid = _context.Students.Where(s => s.rollno == User.Identity.Name.ToString()).Select(s => s.DepartmentId).FirstOrDefault();
                string sdept = _context.Departments.Where(d => d.Id == sdid).Select(s => s.DeptSname).FirstOrDefault();

                List<Faculty> faculties;
                faculties = _context.Faculties.Where(f => f.DepartmentId == sdid).ToList();
                List<Test> tests = new List<Test>();
                tests.Clear();
                foreach (var item in _context.Tests.Where(t => t.SemesterId == ssem).ToList())
                {
                    var faculty = _context.Faculties.Where(f => f.Id == item.FacultyId).FirstOrDefault();
                    if (faculties.Contains(faculty))
                    {
                        tests.Add(item);
                    }
                }






                return View("Tests", tests);
            }
            else if(t.TestEndtime < curDate)
            {
                string error = t.Name + " " + t.Description + " has finished.";
                ViewData["Error"] = error;



                int ssem = _context.Students.Where(s => s.rollno == User.Identity.Name.ToString()).Select(s => s.SemesterId).FirstOrDefault();
                int sdid = _context.Students.Where(s => s.rollno == User.Identity.Name.ToString()).Select(s => s.DepartmentId).FirstOrDefault();
                string sdept = _context.Departments.Where(d => d.Id == sdid).Select(s => s.DeptSname).FirstOrDefault();

                List<Faculty> faculties;
                faculties = _context.Faculties.Where(f => f.DepartmentId == sdid).ToList();
                List<Test> tests = new List<Test>();
                tests.Clear();
                foreach (var item in _context.Tests.Where(t => t.SemesterId == ssem).ToList())
                {
                    var faculty = _context.Faculties.Where(f => f.Id == item.FacultyId).FirstOrDefault();
                    if (faculties.Contains(faculty))
                    {
                        tests.Add(item);
                    }
                }

                return View("Tests",tests);
            }

            
            int sid = _context.Students.Where(s => s.rollno == User.Identity.Name.ToString()).Select(s => s.Id).FirstOrDefault();
            StudentTest records = _context.StudentTests.Where(s => s.StudentId == sid && s.TestId == tid).FirstOrDefault();
            if (records != null)
            {
                return Result(tid);
            }
            TimeSpan sp = t.Testduration.Subtract(curDate);
           if(sp.TotalSeconds<=0)
            {
                //string error;
                ViewData["Error"] = "Test has overed.";
                return View("Tests", _context.Tests.ToList());
            }


            hh = sp.Hours;
            mm = sp.Minutes;
    

            ViewData["hh"] = hh.ToString();
            ViewData["mm"] = mm;
            ViewData["ss"] = "00";


            testid = (int)tid;
            string testdescription = _context.Tests.Where(t => t.Id == tid).Select(t => t.Description).FirstOrDefault();
            string testName = _context.Tests.Where(t => t.Id == tid).Select(t => t.Name).FirstOrDefault();
            ViewBag.TestDetails = testName + " " + testdescription;
            var questions = _context.Questions.Where(q => q.TestId == tid).Include(q => q.Options);
            if (questions != null)
                return View(questions);
            return NotFound();
        }
        public IActionResult FinalSubmit(IFormCollection formCollection)
        {
            totalmarks = 0;
            if (testid == 0)
                return RedirectToAction("Tests", "Students");
            var questions = _context.Questions.Where(q => q.TestId == testid).Include(q => q.Options);
            foreach (var item in questions)
            {
                string selected = formCollection["" + item.Id + ""];
                string correctvalue = _context.Options.Where(O => O.QuestionId == item.Id).Where(O => O.Correct == true).Select(O => O.Option).FirstOrDefault();

                if (correctvalue != null && selected != null && selected.Equals(correctvalue))
                {
                    totalmarks++;
                }
            }
            int sid = _context.Students.Where(s => s.rollno == User.Identity.Name.ToString()).Select(s => s.Id).FirstOrDefault();
            string testdescription = _context.Tests.Where(t => t.Id == testid).Select(t => t.Description).FirstOrDefault();
            string testName = _context.Tests.Where(t => t.Id == testid).Select(t => t.Name).FirstOrDefault();
            ViewBag.TestDetails = testName + " " + testdescription;
            ViewBag.testtotalmarks = _context.Tests.Where(t => t.Id == testid).Select(t => t.Totalmarks).FirstOrDefault();
            ViewBag.totalmarks = totalmarks;
            DateTime d = DateTime.Now;
            StudentTest st = new StudentTest
            {
                StudentId = sid,
                TestId = testid,
                attendedDate = d,
                results = totalmarks
            };
            _context.StudentTests.Add(st);
            _context.SaveChanges();
            return View("Result", questions);
        }
        public IActionResult Result(int? tid)
        {
            if (tid == null)
            {

                return RedirectToAction("Index", "Students");
            }
            int sid = _context.Students.Where(S => S.rollno == User.Identity.Name.ToString()).Select(s => s.Id).FirstOrDefault();

            StudentTest records = _context.StudentTests.Where(s => s.StudentId == sid && s.TestId == testid).FirstOrDefault();
            if (records == null)
            {
                return RedirectToAction("Tests", "Students");
            }



            var questions = _context.Questions.Where(q => q.TestId == testid).Include(q => q.Options);
            testid = (int)tid;
            string testdescription = _context.Tests.Where(t => t.Id == testid).Select(t => t.Description).FirstOrDefault();
            string testName = _context.Tests.Where(t => t.Id == testid).Select(t => t.Name).FirstOrDefault();
            ViewBag.TestDetails = testName + " " + testdescription;
            ViewBag.testtotalmarks = _context.Tests.Where(t => t.Id == testid).Select(t => t.Totalmarks).FirstOrDefault();
            ViewBag.totalmarks = _context.StudentTests.Where(t => t.StudentId == sid && t.TestId == testid).Select(t => t.results).FirstOrDefault();
            return View("result", questions);

        }
        /*[Authorize]
        // GET: Students/Create
        public IActionResult Create()
        {
            ViewData["DepartmentId"] = new SelectList(_context.Departments, "Id", "Id");
            ViewData["SemesterId"] = new SelectList(_context.Set<Semester>(), "Id", "Id");
            return View();
        }

        // POST: Students/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,rollno,Sname,Mno,Email,Password,DepartmentId,SemesterId")] Student student)
        {
            if (ModelState.IsValid)
            {
                _context.Add(student);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DepartmentId"] = new SelectList(_context.Departments, "Id", "Id", student.DepartmentId);
            ViewData["SemesterId"] = new SelectList(_context.Set<Semester>(), "Id", "Id", student.SemesterId);
            return View(student);
        }*/

        // GET: Students/Edit/5
        /*public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Students.FindAsync(id);
            if (student == null)
            {
                return NotFound();
            }
            ViewData["DeptSname"] = new SelectList(_context.Departments.Select(d => d.DeptSname));
            ViewData["DepartmentId"] = new SelectList(_context.Departments, "Id", "Id", student.DepartmentId);
            ViewData["SemesterId"] = new SelectList(_context.Set<Semester>(), "Id", "Id", student.SemesterId);
            return View(student);
        }*/

        // POST: Students/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        /*[HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,rollno,Sname,Mno,Email,Password,DepartmentId,SemesterId")] Student student)
        {
            if (id != student.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(student);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StudentExists(student.Id))
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
            ViewData["DepartmentId"] = new SelectList(_context.Departments, "Id", "Id", student.DepartmentId);
            ViewData["SemesterId"] = new SelectList(_context.Set<Semester>(), "Id", "Id", student.SemesterId);
            return View(student);
        }*/

        // GET: Students/Delete/5
        /*public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Students
                .Include(s => s.department)
                .Include(s => s.semester)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // POST: Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var student = await _context.Students.FindAsync(id);
            _context.Students.Remove(student);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StudentExists(int id)
        {
            return _context.Students.Any(e => e.Id == id);
        }*/
    }
}
