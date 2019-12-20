using OpenApiDocuments.Core.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace OpenApiDocuments.Core.DAL
{
    public class DocumentRepository: GenericRepository<Document>, IDocumentRepository
    {
        private readonly MongoDbContext _context;

        /// <summary>
        /// Initialise une nouvelle instance de la classe DocumentRepository.
        /// </summary>
        /// <param name="context"></param>
        public DocumentRepository(MongoDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
