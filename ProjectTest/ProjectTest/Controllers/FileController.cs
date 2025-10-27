using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectTest.Business;
using ProjectTest.Data.VO;

namespace ProjectTest.Controllers
{
    [ApiVersion("1")]
    [ApiController]
    //[Authorize("Bearer")]
    [Route("api/[controller]/v{version:apiVersion}")]
    public class FileController : ControllerBase
    {
        private readonly IFileBusiness _fileBusiness;

        public FileController(IFileBusiness fileBusiness)
        {
            _fileBusiness = fileBusiness;
        }

        [HttpPost("uploadFile")]
        [ApiExplorerSettings(IgnoreApi = true)]
        [ProducesResponseType((200), Type = typeof(FileDetailVO))]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public async Task<IActionResult> UploadOneFile([FromForm] IFormFile file)
        {
            
            if (file == null || file.Length == 0)
                return BadRequest("No file uploaded.");

            FileDetailVO detail = await _fileBusiness.SaveFileToDisk(file);

            return new OkObjectResult(detail);
        }
    }
}
