using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using SpyDuh_Celtics.Models;
using SpyDuh_Celtics.Repositories;

namespace SpyDuh_Celtics.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServicesController : ControllerBase
    {
        private IServicesRepository _servicesRepository;

        public ServicesController(IServicesRepository servicesRepository) {
            _servicesRepository = servicesRepository;
        }

        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            return Ok(_servicesRepository.GetAll());
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var service = _servicesRepository.GetById(id);
            if (service == null)
            {
                return NotFound();
            }
            return Ok(service);

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
