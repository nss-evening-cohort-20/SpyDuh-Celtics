using Microsoft.AspNetCore.Mvc;
using SpyDuh_Celtics.Models;
using SpyDuh_Celtics.Repositories;

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
            _relationshipRepository.AddFriend(relationship);
            return CreatedAtAction("Get", new { id = relationship.Id }, relationship);
        }

        [HttpPost("AddFoe")]
        public IActionResult PostFoe(NewRelationship relationship)
        {
            _relationshipRepository.AddFoe(relationship);
            return CreatedAtAction("Get", new { id = relationship.Id }, relationship);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _relationshipRepository.Delete(id);
            return NoContent();
        }
    }
}
