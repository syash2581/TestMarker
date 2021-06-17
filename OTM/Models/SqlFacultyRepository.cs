using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OTM.Data;

namespace OTM.Models
{
    public class SqlFacultyRepository : IFacultyRepository
    {
        private readonly OTMContext _context;
        public SqlFacultyRepository(OTMContext _context)
        {
            this._context = _context;
        }

        Faculty IFacultyRepository.AddFaculty(Faculty s)
        {
            _context.Faculties.AddAsync(s);
            _context.SaveChanges();
            return s;
        }

        bool IFacultyRepository.DeleteFaculty(string rollno)
        {
            var Faculty =_context.Faculties.Find(GetFacultyId(rollno));
            _context.Faculties.Remove(Faculty);
            _context.SaveChanges();
            return true;
        }

        bool IFacultyRepository.DeleteFaculty(int FacultyId)
        {
            var Faculty = _context.Faculties.Find(FacultyId);
            _context.Faculties.Remove(Faculty);
            _context.SaveChanges();
            return true;
        }

        Faculty IFacultyRepository.EditFaculty(Faculty s)
        {
            _context.Faculties.Update(s);
            _context.SaveChanges();
            return s;
        }

        IEnumerable<Faculty> IFacultyRepository.GetAllFaculties()
        {
            return _context.Faculties;
        }

        Faculty IFacultyRepository.GetFaculty(string rollno)
        {
            int id = _context.Faculties.Where(s => s.rollno == rollno).FirstOrDefault<Faculty>().Id;
            return _context.Faculties.Find(GetFacultyId(rollno));
        }

        Faculty IFacultyRepository.GetFaculty(int id)
        {
            return _context.Faculties.Find(id);
        }

        int GetFacultyId(string rollno)
        {
            return _context.Faculties.Where(s => s.rollno == rollno).FirstOrDefault<Faculty>().Id;
        }

        int IFacultyRepository.GetFacultyId(string rollno)
        {
            throw new NotImplementedException();
        }

        string GetFacultyRollNo(int id)
        {
            return _context.Faculties.Find(id).rollno;
        }

        
    }
}
