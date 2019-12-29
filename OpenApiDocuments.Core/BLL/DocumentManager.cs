using Microsoft.OpenApi.Readers;
using MongoDB.Driver;
using Octokit;
using OpenApiDocuments.Core.BO;
using OpenApiDocuments.Core.DAL;
using OpenApiDocuments.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenApiDocuments.Core.BLL
{
    public class DocumentManager
    {
        private readonly IDocumentRepository _documentRepository;
        private readonly IDocumentMetadataRepository _documentMetadataRepository;

        private readonly GitHubService _gitHubService;

        public DocumentManager(GitHubService gitHubService, IDocumentRepository documentRepository, IDocumentMetadataRepository documentMetadataRepository)
        {
            _documentRepository = documentRepository;
            _documentMetadataRepository = documentMetadataRepository;
            _gitHubService = gitHubService;
        }

        public List<Document> Find()
        {
            return _documentRepository.Find("test");
        }

        public async Task Collect()
        {
            var tree = await _gitHubService.GetTreeRecursive();
            var reader = new OpenApiStringReader();
            foreach (TreeItem treeItem in (List<TreeItem>)tree)
            {
                if (treeItem.Type != TreeType.Blob) continue; // ignore if not a file
                var fileContent = await _gitHubService.GetFileContent(treeItem.Sha);
                string decodedString = Encoding.UTF8.GetString(fileContent);
                try // TODO: find a better way to keep iterating after reader error
                {
                    var openApiDocument = reader.Read(decodedString, out var diagnostic);
                    var documentMetadata = new DocumentMetadata {
                        Title = openApiDocument.Info.Title,
                        Description = openApiDocument.Info.Description,
                        Servers = openApiDocument.Servers.Select(s => s.Url).ToList(),
                        Paths = new List<string>(openApiDocument.Paths.Keys)
                    };
                    _documentMetadataRepository.UploadFile(fileContent, documentMetadata);
                }
                catch(Exception ex)
                {
                    continue;
                }
            }
            return;
        }

        public void CreateTextIndex()
        {
            _documentRepository.CreateTextIndex("metadata.Servers");
        }
    }
}
