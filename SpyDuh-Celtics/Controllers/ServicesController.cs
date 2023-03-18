using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using SpyDuh_Celtics.Models;

namespace SpyDuh_Celtics.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServicesController : ControllerBase
    {

        public ServicesController() { }

        [HttpGet]
        public IActionResult Get()
        {
            //return Ok(_postRepository.GetAll());
            return Ok("getting service data");
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            //var post = _postRepository.GetById(id);
            //if (post == null)
            //{
            //    return NotFound();
            //}
            //return Ok(post);

            return Ok($"getting data for {id}");
        }

        [HttpPost]
        public IActionResult Post(Services service)
        {
            //_postRepository.Add(post);
            //return CreatedAtAction("Get", new { id = post.Id }, post);
            return Ok($"adding data for {service.Id} {service.Service}");
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, Services service)
        {
            //if (id != post.Id)
            //{
            //    return BadRequest();
            //}

            //_postRepository.Update(post);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            //_postRepository.Delete(id);
            return NoContent();
        }
    }
}
