using System;
using Calendar.WebAPI.Data;
using Calendar.WebAPI.Helpers;
using Calendar.WebAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace Calendar.WebAPI.Controllers
{
    [ApiController]
    [Route("calendar/[controller]")]
    public class AvailabilityController : ControllerBase
    {
        private readonly IRepository _repo;
        public AvailabilityController(IRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_repo.GetAllAvailabilities());
        }

        [HttpGet("{name}")]
        public IActionResult Get([FromQuery] PageParams pageParams, string name)
        {
            return Ok(_repo.GetInterviewerAvailabilitiesForCandidate(pageParams, name));
        }

        [HttpPost]
        public IActionResult Post(Availability availability)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if(availability.Start >= availability.End)
                return BadRequest("The start of the Availability have to be lower than the end.");

            try
            {
                _repo.AddAvailabilities(availability);
                _repo.SaveChanges();
                return Ok(availability);
            }
            catch (Exception)
            {
                return BadRequest("It was not possible to register the availability.");
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                var availability = _repo.GetAvailabilityById(id);
                if (availability == null)
                    BadRequest("availability not found.");
                _repo.Remove(availability);
                _repo.SaveChanges();
                return Ok(availability);
            }
            catch (Exception)
            {
                return BadRequest("It was not possible to delete the register.");
            }
        }
    }
}