using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectTest.Business;
using ProjectTest.Data.VO;
using ProjectTest.Hypermedia.FIlters;

namespace ProjectTest.Controllers
{
    [ApiVersion("1")]
    [ApiController]
    [Authorize("Bearer")]
    [Route("api/[controller]/v{version:apiVersion}")]
    public class BookController : ControllerBase
    {
        private readonly ILogger<BookController> _logger;

        private IBookBusiness _bookBusiness;


        public BookController(ILogger<BookController> logger, IBookBusiness bookBusiness)
        {
            _logger = logger;
            _bookBusiness = bookBusiness;
        }

        #region GET

        [HttpGet]
        [TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult Get()
        {
            return Ok(_bookBusiness.FindAll());
        }

        [HttpGet("{id}")]
        [TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult Get(long id)
        {
            var person = _bookBusiness.FindById(id);

            if (person == null)
                return NotFound();

            return Ok(person);
        }
        #endregion

        #region POST
        [HttpPost]
        [TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult Post([FromBody] BookVO book)
        {

            if (book == null)
                return BadRequest();

            return Ok(_bookBusiness.Create(book));
        }
        #endregion

        #region PUT
        [HttpPut]
        [TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult Put([FromBody] BookVO book)
        {

            if (book == null)
                return BadRequest();

            return Ok(_bookBusiness.Update(book));
        }
        #endregion

        #region DELETE
        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            _bookBusiness.Delete(id);

            return NoContent();
        }
        #endregion


    }
}

