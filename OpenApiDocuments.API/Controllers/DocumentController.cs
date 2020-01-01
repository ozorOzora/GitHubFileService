using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OpenApiDocuments.API.ViewModels;
using OpenApiDocuments.Core.BLL;

namespace OpenApiDocuments.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentController : ControllerBase
    {
        private readonly DocumentManager _documentManager;
        private readonly IMapper _mapper;

        public DocumentController(ILogger<DocumentController> logger, DocumentManager documentManager, IMapper mapper)
        {
            _mapper = mapper;
            _documentManager = documentManager;
        }

        [HttpGet("/collect")]
        public async Task<IActionResult> Collect()
        {
            try
            {
                /// Traitement
                await _documentManager.Collect();
                return StatusCode((int)HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

        [HttpGet("/create-text-index")]
        public IActionResult CreateTextIndex()
        {
            try
            {
                _documentManager.CreateTextIndex();
                return StatusCode((int)HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

        [HttpGet("/search")]
        public IActionResult Search([FromQuery] string query)
        {
            try
            {
                /// Traitement
                var documents = _documentManager.Find(query);

                /// Création du modèle de vue
                var resultViewModel = documents.Select(d => _mapper.Map<DocumentViewModel>(d));

                /// Restitution
                return StatusCode((int)HttpStatusCode.OK, resultViewModel);
            }
            catch(Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }
    }
}
