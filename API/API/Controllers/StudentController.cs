using API.Data;
using API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly AppDbContext _context;

        public StudentController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetStudents()
        {
            List<Student> students = _context.Students.ToList();
            return Ok(students);
        }

        [HttpGet("{id}")]
        public IActionResult GetStudent(int? id)
        {
            if (id==null)
            {
                //return BadRequest();
            }

            Student student = _context.Students.Find(id);
            if (student==null)
            {
                ModelState.AddModelError("", "Error");
                return StatusCode(StatusCodes.Status404NotFound, ModelState);
            }
            return Ok(student);
        }


        [HttpPost]
        public IActionResult CreateStudent( Student model)
        {
            if (ModelState.IsValid)
            {
                model.CreatedDate = DateTime.Now;
                _context.Students.Add(model);
                _context.SaveChanges();
                return Ok();
            }

            return BadRequest();
        }

    }
}
