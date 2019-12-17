using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Octokit;

namespace GitHubFileService.Core.BLL
{
    public class GitHubManager
    {
        private readonly ILogger _logger;

        public GitHubManager(ILogger<GitHubManager> logger)
        {
            _logger = logger;
        }

        public async Task<TreeResponse> GetTreeRecursive()
        {
            var github = new GitHubClient(new ProductHeaderValue("GitHubFileService"));
            var tree = await github.Git.Tree.GetRecursive("APIs-guru", "openapi-directory", "6b1a14521a7994de56bf1f9d3d65034a70d2d249");
            return tree;
        }
    }
}
