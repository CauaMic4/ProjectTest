using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using ProjectTest.Business;
using ProjectTest.Model;

namespace ProjectTest.Controllers
{
    [ApiVersion("1")]
    [ApiController]
    [Route("api/[controller]/v{version:apiVersion}")]
    public class PersonController : ControllerBase
    {
        private readonly ILogger<PersonController> _logger;

        private IPersonBusiness _personBusiness;


        public PersonController(ILogger<PersonController> logger, IPersonBusiness personBusiness)
        {
            _logger = logger;
            _personBusiness = personBusiness;
        }

        #region GET

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_personBusiness.FindAll());
        }

        [HttpGet("{id}")]
        public IActionResult Get(long id)
        {
            var person = _personBusiness.FindById(id);

            if (person == null)
                return NotFound();

            return Ok(person);
        }
        #endregion

        #region POST
        [HttpPost]
        public IActionResult Post([FromBody] Person person)
        {

            if (person == null)
                return BadRequest();

            return Ok(_personBusiness.Create(person));
        }
        #endregion

        #region PUT
        [HttpPut]
        public IActionResult Put([FromBody] Person person)
        {

            if (person == null)
                return BadRequest();

            return Ok(_personBusiness.Update(person));
        }
        #endregion

        #region DELETE
        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            _personBusiness.Delete(id);

            return NoContent();
        }
        #endregion


    }
}

