using OpenApiDocuments.Core.Attributes;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Microsoft.OpenApi.Models;
using MongoDB.Bson.Serialization.Options;
using System.Collections.Generic;

namespace OpenApiDocuments.Core.BO
{
    /// <summary>
    /// Représente un document de spécifications OpenAPI
    /// </summary>
    public class DocumentMetadata
    {
        public DocumentMetadata() { }

        /// <summary>
        /// Urls des serveurs de l'api
        /// </summary>
        public List<string> Servers { get; set; }

        /// <summary>
        /// Liste des endpoints de l'api
        /// </summary>
        public List<string> Paths { get; set; }

    }

}
