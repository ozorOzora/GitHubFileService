using OpenApiDocuments.Core.Attributes;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace OpenApiDocuments.Core.BO
{
    /// <summary>
    /// Représente un document de spécifications OpenAPI
    /// </summary>
    [BsonDiscriminator(RootClass = true)]
    [BsonIgnoreExtraElements]
    [BsonCollection("documents.files")]
    public class Document
    {
        public Document() { }

        /// <summary>
        /// Identifiant du document
        /// </summary>
        [BsonId]
        public ObjectId Id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [BsonElement("metadata")]
        public DocumentMetadata Metadata { get; set; }
    }

}
