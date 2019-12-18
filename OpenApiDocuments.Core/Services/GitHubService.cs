using System;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Octokit;

namespace OpenApiDocuments.Core.Services
{
    public class GitHubService
    {
        private readonly ILogger _logger;

        public GitHubService(ILogger<GitHubService> logger)
        {
            _logger = logger;
        }

        public async Task<TreeResponse> GetTreeRecursive()
        {
            var github = new GitHubClient(new ProductHeaderValue("GitHubFileService"));
            var tree = await github.Git.Tree.GetRecursive("APIs-guru", "openapi-directory", "6b1a14521a7994de56bf1f9d3d65034a70d2d249");

            return tree;
        }

        public async Task<String> GetFileContent()
        {
            var github = new GitHubClient(new ProductHeaderValue("GitHubFileService"));
            var blob = await github.Git.Blob.Get("APIs-guru", "openapi-directory", "fa3eb2888761f7219d9d88c4ea4eff3fc11530ae");
            if (blob.Encoding == EncodingType.Base64)
            {
                byte[] data = System.Convert.FromBase64String(blob.Content);
                string decodedString = Encoding.UTF8.GetString(data);
                return decodedString;
            }
            else
            {
                throw new Exception("Unhandled encoding");
            }
        }

    }
}
