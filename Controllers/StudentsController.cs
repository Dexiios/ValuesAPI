using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ValuesAPI.Models;
using ValuesAPI.Data;
using Microsoft.EntityFrameworkCore;
using ValuesAPI.DTO;

namespace ValuesAPI.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class StudentsController : Controller
    {
        private readonly Context _context;

        public StudentsController(Context context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<StudentDTO>>> GetStudents()
        {
            var student = from students in _context.Students
                          join student_descriptions in _context.Students_Description on students.Id equals student_descriptions.Student_Id
                          select new StudentDTO
                          {
                              Student_id = students.Id,
                              Student_grade = students.Grade,
                              Student_age = student_descriptions.Age,
                              Student_first_name = student_descriptions.First_name,
                              Student_last_name = student_descriptions.Last_name,
                              Student_adress = student_descriptions.Adress,
                              Student_country = student_descriptions.Country

                          };
            return await student.ToListAsync();
        }

        [HttpGet("{id}")]
        public ActionResult<StudentDTO> GetStuednts_byId(int id)
        {
            var student = from students in _context.Students
                          join student_descriptions in _context.Students_Description on students.Id equals student_descriptions.Student_Id
                          select new StudentDTO
                          {
                              Student_id = students.Id,
                              Student_grade = students.Grade,
                              Student_age = student_descriptions.Age,
                              Student_first_name = student_descriptions.First_name,
                              Student_last_name = student_descriptions.Last_name,
                              Student_adress = student_descriptions.Adress,
                              Student_country = student_descriptions.Country

                          };
            var student_by_id = student.ToList().Find(x => x.Student_id == id);

            if(student_by_id == null)
            {
                return NotFound();
            }
            return student_by_id;
        }

        [HttpPost]
        public async Task<ActionResult<AddStudent>> Add_Students(AddStudent addstudent)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var student = new Students()
            {
                Grade = addstudent.Student_grade,
            };

            await _context.Students.AddAsync(student);
            await _context.SaveChangesAsync();

            var student_description = new Students_Description()
            {
                Student_Id = student.Id,
                Age = addstudent.Student_age,
                First_name = addstudent.Student_first_name,
                Last_name = addstudent.Student_last_name,
                Adress = addstudent.Student_adress,
                Country = addstudent.Student_country
            };
            await _context.AddAsync(student_description);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetStudents", new { id = student}, addstudent);
        }

        [HttpDelete]
        public async Task<ActionResult<Students>> Delete_Student(int id)
        {
            var student = _context.Students.Find(id);
            var student_description = _context.Students_Description.SingleOrDefault(x => x.Student_Id == id);

            if(student == null)
            {
                return NotFound();
            }
            else
            {
                _context.Remove(student);
                _context.Remove(student_description);
                await _context.SaveChangesAsync();
                return student;
            }
        }

        [HttpPut]
        public async Task<ActionResult> Update_Student(int id, StudentDTO studentdto)
        {
            if(id != studentdto.Student_id || !StudentExists(id))
            {
                return BadRequest();
            }
            else
            {
                var students = _context.Students.SingleOrDefault(x => x.Id == id);
                var students_description = _context.Students_Description.SingleOrDefault(x => x.Student_Id == id);

                students.Grade = studentdto.Student_grade;
                students_description.Age = studentdto.Student_age;
                students_description.Country = studentdto.Student_country;
                students_description.Adress = studentdto.Student_adress;
                students_description.First_name = studentdto.Student_first_name;
                students_description.Last_name = studentdto.Student_last_name;

                await _context.SaveChangesAsync();
                return NoContent();
            }
        }

        private bool StudentExists(int id)
        {
            return _context.Students.Any(x => x.Id == id);
        }
    }
}
