using System;
using System.Linq;
using Calendar.WebAPI.Data;
using Calendar.WebAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Calendar.WebAPI.Controllers
{
    [ApiController]
    [Route("calendar/[controller]")]
    public class PersonController : ControllerBase
    {
        private readonly IRepository _repo;
        public PersonController(IRepository repo)
        {
            _repo = repo;
        }

        [HttpPost]
        public IActionResult Post(Person person)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                _repo.Add(person);
                _repo.SaveChanges();
                return Ok(person);
            }
            catch (Exception)
            {
                return BadRequest("It was not possible to register the person.");
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                var person = _repo.GetPersonById(id); ;
                if (person == null)
                    BadRequest("Person not found.");
                _repo.Remove(person);
                _repo.SaveChanges();
                return Ok(person);
            }
            catch (Exception)
            {
                return BadRequest("It was not possible to delete the register.");
            }
        }        
    }
}