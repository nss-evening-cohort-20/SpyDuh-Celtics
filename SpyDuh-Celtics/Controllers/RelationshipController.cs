using Microsoft.AspNetCore.Mvc;
using SpyDuh_Celtics.Models;
using SpyDuh_Celtics.Repositories;
using Microsoft.Data.SqlClient;
using Microsoft.AspNetCore.Http;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SpyDuh_Celtics.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RelationshipController : ControllerBase
    {
        private readonly IRelationshipRepository _relationshipRepository;

        public RelationshipController(IRelationshipRepository relationshipRepository)
        {
            _relationshipRepository = relationshipRepository;
        }

        [HttpGet("GetAllFriendsById")]
        public IActionResult GetAllFriendsById(int id)
        {
            var friends = _relationshipRepository.GetAllFriendsById(id);
            return Ok(friends);
        }

        [HttpGet("GetAllFoesById")]
        public IActionResult GetAllFoesById(int id)
        {
            var foes = _relationshipRepository.GetAllFoesById(id);
            return Ok(foes);
        }

        [HttpPost("AddFriend")]
        public IActionResult PostFriend(NewRelationship relationship)
        {
            try
            {
                if (!_relationshipRepository.AddFriend(relationship))
                {
                    return BadRequest(relationship);
                }

                return CreatedAtAction("Get", new { id = relationship.Id }, relationship);
            }
            catch (SqlException ex)
            {
                if (ex.Message.Contains("FOREIGN KEY constraint"))
                {
                    return BadRequest("That relationship id doesn't exist");
                }

                return StatusCode(500, ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }


        }


    //    _relationshipRepository.AddFriend(relationship);
    //        return CreatedAtAction("Get", new { id = relationship.Id
    //}, relationship);

        [HttpPost("AddFoe")]
        public IActionResult PostFoe(NewRelationship relationship)
        {
            try
            {
                if (!_relationshipRepository.AddFoe(relationship))
                {
                    return BadRequest(relationship);
                }

                return CreatedAtAction("Get", new { id = relationship.Id }, relationship);
            }
            catch (SqlException ex)
            {
                if (ex.Message.Contains("FOREIGN KEY constraint"))
                {
                    return BadRequest("That relationship id doesn't exist");
                }

                return StatusCode(500, ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

    //    _relationshipRepository.AddFoe(relationship);
    //        return CreatedAtAction("Get", new { id = relationship.Id
    //}, relationship);

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _relationshipRepository.Delete(id);
            return NoContent();
        }
    }
}
