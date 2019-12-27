using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Octokit;

namespace OpenApiDocuments.Core.Services
{
    public class GitHubService
    {
        private readonly ILogger _logger;
        private readonly Credentials _credentials;

        public GitHubService(ILogger<GitHubService> logger, IConfiguration config)
        {
            _logger = logger;
            string username = config.GetSection("GitHubLogin")["Username"];
            string password = config.GetSection("GitHubLogin")["Password"];
            _credentials = new Credentials(username, password);
        }

        public async Task<IReadOnlyList<TreeItem>> GetTreeRecursive()
        {
            var github = new GitHubClient(new ProductHeaderValue("GitHubFileService"))
            {
                Credentials = _credentials
            };

            var tree = await github.Git.Tree.GetRecursive("APIs-guru", "openapi-directory", "6b1a14521a7994de56bf1f9d3d65034a70d2d249");
            return tree.Tree;
        }

        public async Task<byte[]> GetFileContent(string sha)
        {
            var github = new GitHubClient(new ProductHeaderValue("GitHubFileService"))
            {
                Credentials = _credentials
            };
            var blob = await github.Git.Blob.Get("APIs-guru", "openapi-directory", sha);
            if (blob.Encoding == EncodingType.Base64)
            {
                byte[] data = System.Convert.FromBase64String(blob.Content);
                return data;
            }
            else
            {
                throw new Exception("Unhandled encoding");
            }
        }

    }
}
