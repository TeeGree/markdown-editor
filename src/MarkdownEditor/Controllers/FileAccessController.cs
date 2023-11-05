using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace MarkdownEditor.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public sealed class FileAccessController : ControllerBase
    {
        public record UpdateMarkdownBody([Required] string markdown);

        private readonly ILogger<FileAccessController> _logger;
        private readonly IFileAccessService _fileAccessService;

        public FileAccessController(ILogger<FileAccessController> logger, IFileAccessService fileAccessService)
        {
            _logger = logger;
            _fileAccessService = fileAccessService;
        }

        [HttpGet(Name = "GetFileContent")]
        public ActionResult<string> Get()
        {
            try
            {
                return Ok(_fileAccessService.GetMarkdown());
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return Problem(e.Message);
            }
        }

        [HttpPut("UpdateMarkdown", Name = "UpdateMarkdown")]
        public ActionResult UpdateMarkdown(UpdateMarkdownBody markdownBody)
        {
            try
            {
                _fileAccessService.PutMarkdown(markdownBody.markdown);
                return Ok();
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return Problem(e.Message);
            }
        }
    }
}