using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using GitHubFileService.Core.BLL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Octokit;

namespace GitHubFileService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GitHubController: ControllerBase
    {
        private readonly GitHubManager _gitHubManager;

        public GitHubController(ILogger<GitHubController> logger, GitHubManager gitHubManager)
        {
            _gitHubManager = gitHubManager;
        }

        [HttpGet]
        public async Task<IActionResult> GetTreeRecursiveAsync()
        {
            try
            {
                /// Traitement
                var result = await _gitHubManager.GetTreeRecursive();

                return StatusCode((int)HttpStatusCode.OK);
            }
            catch
            {
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }
    }
}
