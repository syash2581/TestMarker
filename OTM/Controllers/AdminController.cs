using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OTM.Data;
using OTM.Models;

namespace OTM.Controllers
{
    public class AdminController : Controller
    {
        OTMContext _context;
        public AdminController(OTMContext _context)
        {
            this._context = _context;
        }
        public bool AdminExist()
        {
            int id =_context.Faculties.Where(f => f.rollno == User.Identity.Name.ToString()).Select(f => f.Id).FirstOrDefault();
            if (id <= 0)
                return false;
            return true;
        }
        public bool IsAdmin()
        {
            int id =_context.Faculties.Where(f => f.type == "Admin").Select(f => f.Id).FirstOrDefault();
            return (id <= 0) ? false : true;
        }
        public IActionResult Index()
        {
            if (!AdminExist() || !IsAdmin())
                return RedirectToAction("Logout", "Account");
            Faculty f = _context.Faculties.Where(f => f.rollno == User.Identity.Name).FirstOrDefault();
            ViewData["welcome"] = "Welcome, " + f.name + " !!!";
            return View();
        }

        public IActionResult Faculties()
        {

            if (!AdminExist() || !IsAdmin())
                return RedirectToAction("Logout", "Account");
            int did = _context.Faculties.Where(f => f.rollno == User.Identity.Name).Select(f => f.DepartmentId).FirstOrDefault();
            var oTMContext = _context.Faculties.Where(f => f.type != "Admin").Include(f => f.department).Where(f=>f.DepartmentId == did);
            return View(oTMContext.ToList());
        }
        public IActionResult Approve(string rollno)
        {
            if (!AdminExist() || !IsAdmin())
                return RedirectToAction("Logout", "Account");
            Faculty s = _context.Faculties.Where(s => s.rollno == rollno).Select(s => s).FirstOrDefault();
            s.regornot = 1;
            _context.Faculties.Update(s);
            _context.SaveChanges();

            int did = _context.Faculties.Where(f => f.rollno == User.Identity.Name).Select(f => f.DepartmentId).FirstOrDefault();
            var oTMContext = _context.Faculties.Include(f => f.department).Where(f => f.DepartmentId == did && f.rollno != User.Identity.Name.ToString());

            return View("Faculties", oTMContext.ToList());
        }
    }
}