using OpenApiDocuments.Core.Attributes;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Microsoft.OpenApi.Models;

namespace OpenApiDocuments.Core.BO
{
    /// <summary>
    /// Représente un document de spécifications OpenAPI
    /// </summary>
    [BsonDiscriminator(RootClass = true)]
    [BsonIgnoreExtraElements]
    [BsonCollection("documents")]
    public class Document
    {
        public Document() { }

        /// <summary>
        /// Identifiant du document
        /// </summary>
        [BsonId]
        public ObjectId Id { get; set; }

        /// <summary>
        /// Document de spécifications
        /// </summary>
        [BsonElement("openapidocument")]
        public OpenApiDocument Info { get; set; }

    }
}
