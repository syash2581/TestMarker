using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OTM.Data;
using OTM.Models;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace OTM.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
        private readonly OTMContext _context;
        private readonly IStudentRepository studentRepository;
        private readonly IFacultyRepository facultyRepository;
        private readonly UserManager<CustomIdentity> userManager;
        private readonly SignInManager<CustomIdentity> signInManager;

        public AccountController(UserManager<CustomIdentity> userManager, SignInManager<CustomIdentity> signInManager, OTMContext _context, IStudentRepository studentRepository, IFacultyRepository facultyRepository)
        {
            this.facultyRepository = facultyRepository;
            this.studentRepository = studentRepository;
            this.userManager = userManager;
            this.signInManager = signInManager;
            this._context = _context;
        }
        [HttpGet]
        public IActionResult Login()
        {
            ViewData["title"] = "Login";
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginModel model, string returnUrl)
        {
            bool flag = false;
            if (ModelState.IsValid)
            {
                string usertype = model.usertype.ToLower();
                if (usertype.Equals("student"))
                {
                    if (_context.Students.Where(s => s.rollno == model.rollno).Select(s => s.regornot).FirstOrDefault() == 1)
                    {
                        flag = true;
                    }
                    else
                    {
                        flag = false;
                    }
                }
                else if (usertype.Equals("faculty"))
                {
                    if (_context.Faculties.Where(f => f.rollno == model.rollno).Select(f => f.regornot).FirstOrDefault() == 1)
                    {
                        flag = true;
                    }
                    else
                    {
                        flag = false;
                    }
                }
                if (flag)
                {
                    var result = await signInManager.PasswordSignInAsync(model.rollno, model.Password2, false, false);


                    if (result.Succeeded)
                    {
                        if (model.usertype.Equals("Faculty"))
                        {
                            if (!string.IsNullOrEmpty(returnUrl))
                            {
                                return LocalRedirect(returnUrl);
                            }
                            else
                            {
                                if (_context.Faculties.Where(f => f.rollno == model.rollno).Select(f => f.type).FirstOrDefault().ToString().ToLower().Equals("admin"))
                                {
                                    return RedirectToAction("Index", "Admin");
                                }
                                return RedirectToAction("Index", "Faculties");
                            }
                        }
                        else if (model.usertype.Equals("Student"))
                        {
                            if (!string.IsNullOrEmpty(returnUrl))
                            {
                                return LocalRedirect(returnUrl);
                            }
                            else
                            {
                                return RedirectToAction("Index", "Students");
                            }
                        }

                    }
                }
                ModelState.AddModelError(string.Empty, "Invalid Login Attempt.\nMake Sure you are registered and Approved.");
            }
            return View(model);
        }


        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register()
        {
            ViewData["Dept"] = new SelectList(_context.Departments.Select(d => d.DeptSname));
            ViewData["Sem"] = new SelectList(_context.semesters.Select(s => s.Id));
            return View();


        }
        [HttpPost]
        public async Task<ActionResult> Register(CustomIdentity customIdentity)
        {


            if (ModelState.IsValid)
            {
                CustomIdentity ci;
                ci = new CustomIdentity
                {
                    UserName = customIdentity.rollno,
                    EmailConfirmed = true,
                    TwoFactorEnabled = false,
                    rollno = customIdentity.rollno,
                    name = customIdentity.name,
                    type = customIdentity.type,
                    mno = customIdentity.mno,
                    Email = customIdentity.Email,
                    DeptSname = customIdentity.DeptSname,
                    DepartmentId = _context.Departments.Where(d => d.DeptSname == customIdentity.DeptSname).Select(d => d.Id).FirstOrDefault(),
                    Password = customIdentity.Password,
                    ConfirmPassword = customIdentity.ConfirmPassword,
                };
                if (customIdentity.type.ToLower().Equals("faculty"))
                {
                    ci.SemesterId = 8;
                    Faculty f = new Faculty
                    {
                        rollno = ci.rollno,
                        name = ci.name,
                        type = ci.type,
                        mno = ci.mno,
                        email = ci.Email,
                        password = ci.Password,
                        regornot = 0,
                        DepartmentId = ci.DepartmentId,
                        DeptSname = ci.DeptSname

                    };
                    var faculty = _context.Faculties.Where(d => d.rollno == f.rollno).FirstOrDefault();
                    if (faculty != null)
                    {
                        ModelState.AddModelError(string.Empty, "Faculty Is Already Registred");
                        ViewData["Errors"] = "Faculty Is Already Registered";
                        return RedirectToAction("Login");
                        

                    }
                    facultyRepository.AddFaculty(f);
                    _context.SaveChanges();
                }
                else if (customIdentity.type.ToLower().Equals("student"))
                {
                    ci.SemesterId = customIdentity.SemesterId;
                    Student s = new Student
                    {
                        rollno = ci.rollno,
                        Sname = ci.name,
                        Mno = ci.mno,
                        Email = ci.Email,
                        Password = ci.Password,
                        regornot = 0,
                        DepartmentId = ci.DepartmentId,
                        SemesterId = ci.SemesterId
                    };
                    var student = _context.Students.Where(st => st.rollno == s.rollno).FirstOrDefault();
                    if (student != null)
                    {
                        ModelState.AddModelError(string.Empty, "Student Is Already Registred");
                        ViewData["Errors"] = "Student Is Already Registered";
                        return RedirectToAction("Login");

                    }
                    studentRepository.AddStudent(s);
                    _context.SaveChanges();

                }
                var result = await userManager.CreateAsync(ci, customIdentity.Password);


                if (result.Succeeded)
                {
                    //await signInManager.SignInAsync(ci, isPersistent: false);
                    if (ci.type.ToLower().Equals("faculty"))
                    {
                        return RedirectToAction("Index", "Faculties");
                    }
                    else if (ci.type.ToLower().Equals("student"))
                    {
                        return RedirectToAction("Index", "Students");
                    }

                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Could not Register.");
                    ModelState.AddModelError(string.Empty, result.Errors.ToString());
                    ViewData["Errors"] = result.Errors.ToString();
                    return RedirectToAction("Register", "Account");
                }

            }
            //return RedirectToAction("/","");
            return View(customIdentity);
        }
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
