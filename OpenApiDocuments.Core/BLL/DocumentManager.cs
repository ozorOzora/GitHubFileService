using Microsoft.OpenApi.Models;
using Microsoft.OpenApi.Readers;
using MongoDB.Driver;
using Octokit;
using OpenApiDocuments.Core.BO;
using OpenApiDocuments.Core.DAL;
using OpenApiDocuments.Core.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OpenApiDocuments.Core.BLL
{
    public class DocumentManager
    {
        private readonly IDocumentRepository _documentRepository;
        private readonly GitHubService _gitHubService;

        public DocumentManager(GitHubService gitHubService, IDocumentRepository documentRepository)
        {
            _documentRepository = documentRepository;
            _gitHubService = gitHubService;
        }

        public void CreateDocument(OpenApiDocument openApiDocument)
        {
            try
            {
                var document = new Document
                {
                    Content = openApiDocument
                };
                _documentRepository.Add(document);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public List<Document> FindAll()
        {
            // see https://stackoverflow.com/questions/40164908/mongodb-and-c-sharp-find
            // or https://www.codementor.io/@pmbanugo/working-with-mongodb-in-net-2-retrieving-mrlbeanm5
            var filter = Builders<Document>.Filter.Eq("Content.Servers.Url", "https://cal-test.adyen.com/cal/services/Notification/v1");
            return _documentRepository.FindAll(filter);
        }

        public async Task Collect()
        {
            var tree = await _gitHubService.GetTreeRecursive();
            var reader = new OpenApiStringReader();
            foreach (TreeItem treeItem in (List<TreeItem>)tree)
            {
                if (treeItem.Type != TreeType.Blob) continue;
                var file = await _gitHubService.GetFileContent(treeItem.Sha);

                try // TODO: find a better way to keep iterating after reader error
                {
                    var openApiDocument = reader.Read(file, out var diagnostic);
                    CreateDocument(openApiDocument);
                }
                catch
                {
                    continue;
                }

            }
            return;
        }

        public void CreateSingleFieldIndex()
        {
            _documentRepository.CreateSingleFieldIndex("Content.Servers.Url");
        }

    }
}
