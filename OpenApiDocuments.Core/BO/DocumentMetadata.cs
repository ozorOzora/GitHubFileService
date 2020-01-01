using System.Collections.Generic;

namespace OpenApiDocuments.Core.BO
{
    /// <summary>
    /// Représente les métadonnées d'un document de spécifications OpenAPI
    /// </summary>

    public class DocumentMetadata
    {
        public DocumentMetadata() { }

        /// <summary>
        /// Nom de l'api
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Description de l'api
        /// </summary>
        public string Description { get; set; }

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
