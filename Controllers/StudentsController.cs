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

    }
}
