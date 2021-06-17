using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OTM.Data;

namespace OTM.Models
{
    public class SqlStudentRepository : IStudentRepository
    {
        private readonly OTMContext _context;
        public SqlStudentRepository(OTMContext _context)
        {
            this._context = _context;
        }

        Student IStudentRepository.AddStudent(Student s)
        {
            _context.Students.Add(s);
            _context.SaveChanges();
            return s;
        }

        bool IStudentRepository.DeleteStudent(string rollno)
        {
            var student =_context.Students.Find(GetStudentId(rollno));
            _context.Students.Remove(student);
            _context.SaveChanges();
            return true;
        }

        bool IStudentRepository.DeleteStudent(int StudentId)
        {
            var student = _context.Students.Find(StudentId);
            _context.Students.Remove(student);
            _context.SaveChanges();
            return true;
        }

        Student IStudentRepository.EditStudent(Student s)
        {
            _context.Students.Update(s);
            _context.SaveChanges();
            return s;
        }

        IEnumerable<Student> IStudentRepository.GetAllStudents()
        {
            return _context.Students;
        }

        Student IStudentRepository.GetStudent(string rollno)
        {
            int id = _context.Students.Where(s => s.rollno == rollno).FirstOrDefault<Student>().Id;
            return _context.Students.Find(GetStudentId(rollno));
        }

        Student IStudentRepository.GetStudent(int id)
        {
            return _context.Students.Find(id);
        }

        int GetStudentId(string rollno)
        {
            return _context.Students.Where(s => s.rollno == rollno).FirstOrDefault<Student>().Id;
        }

        string GetStudentRollNo(int id)
        {
            return _context.Students.Find(id).rollno;
        }
    }
}
