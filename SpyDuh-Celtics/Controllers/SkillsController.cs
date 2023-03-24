using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using SpyDuh_Celtics.Models;
using SpyDuh_Celtics.Repositories;

namespace SpyDuh_Celtics.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SkillsController : ControllerBase
    {
        private readonly ISkillsRepository _skillsRepo;

        public SkillsController(ISkillsRepository skillsRepo)
        {
            _skillsRepo = skillsRepo;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_skillsRepo.GetAll());
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var found = _skillsRepo.GetById(id);

            if (found == null)
            {
                return NotFound();
            }
            return Ok(found);
        }

        [HttpPost]
        public IActionResult Post(SkillInsert skill)
        {
            try
            {
                if (!_skillsRepo.Insert(skill))
                {
                    return BadRequest(skill);
                }

                return CreatedAtAction("Get", new { id = skill.Id }, skill);
            }
            catch (SqlException ex)
            {
                if (ex.Message.Contains("FOREIGN KEY constraint"))
                {
                    return BadRequest("That skill id doesn't exist");
                }

                return StatusCode(500, ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, SkillInsert skill)
        {
            try
            {
                if (id != skill.Id || !_skillsRepo.Update(skill))
                {
                    return BadRequest();
                }

                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var found = _skillsRepo.GetById(id) is not null;
                if (!found)
                {
                    return NotFound();
                }

                if (!_skillsRepo.Delete(id))
                {
                    return BadRequest();
                }

                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
