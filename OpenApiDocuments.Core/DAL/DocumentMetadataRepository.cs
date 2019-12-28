using OpenApiDocuments.Core.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace OpenApiDocuments.Core.DAL
{
    public class DocumentMetadataRepository: GenericRepository<DocumentMetadata>, IDocumentMetadataRepository
    {
        private readonly MongoDbContext _context;

        /// <summary>
        /// Initialise une nouvelle instance de la classe DocumentRepository.
        /// </summary>
        /// <param name="context"></param>
        public DocumentMetadataRepository(MongoDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
