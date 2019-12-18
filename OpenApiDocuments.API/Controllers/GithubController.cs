using System.Net;
using System.Threading.Tasks;
using OpenApiDocuments.Core.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace OpenApiDocuments.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GitHubController: ControllerBase
    {
        private readonly GitHubService _gitHubService;

        public GitHubController(ILogger<GitHubController> logger, GitHubService gitHubService)
        {
            _gitHubService = gitHubService;
        }

        [HttpGet]
        public async Task<IActionResult> GetTreeRecursiveAsync()
        {
            try
            {
                /// Traitement
                var result = await _gitHubService.GetTreeRecursive();

                return StatusCode((int)HttpStatusCode.OK);
            }
            catch
            {
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

        [HttpGet("/file")]
        public async Task<IActionResult> GetFileContent()
        {
            try
            {
                var result = await _gitHubService.GetFileContent();

                return StatusCode((int)HttpStatusCode.OK);
            }
            catch
            {
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

    }
}
