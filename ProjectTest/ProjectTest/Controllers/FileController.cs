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

        [HttpGet("downloadFile/{fileName}")]
        [ApiExplorerSettings(IgnoreApi = true)]
        [ProducesResponseType((200), Type = typeof(byte[]))]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetFileAsync(string fileName)
        {

            if (fileName == null)
                return BadRequest("No file for download.");

            byte[] buffer =  _fileBusiness.GetFile(fileName);

            if (buffer != null)
            {
                HttpContext.Response.ContentType = $"application/{Path.GetExtension(fileName).Replace(".", "")}";

                HttpContext.Response.Headers.Add("content-length", buffer.Length.ToString());

                await HttpContext.Response.Body.WriteAsync(buffer, 0, buffer.Length);
            }

            return new ContentResult();
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

        [HttpPost("uploadMultipleFile")]
        [ApiExplorerSettings(IgnoreApi = true)]
        [ProducesResponseType((200), Type = typeof(List<FileDetailVO>))]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public async Task<IActionResult> UploadManyFile([FromForm] List<IFormFile> files)
        {

            if (files == null || files.Count == 0)
                return BadRequest("No file uploaded.");

            List<FileDetailVO> details = await _fileBusiness.SaveFilesToDisk(files);

            return new OkObjectResult(details);
        }
    }
}
