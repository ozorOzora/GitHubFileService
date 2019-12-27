using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OpenApiDocuments.Core.BLL;

namespace OpenApiDocuments.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentController : ControllerBase
    {
        private readonly DocumentManager _documentManager;

        public DocumentController(ILogger<DocumentController> logger, DocumentManager documentManager)
        {
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

        [HttpGet("/create-index")]
        public IActionResult CreateIndex()
        {
            try
            {
                _documentManager.CreateSingleFieldIndex();
                return StatusCode((int)HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

        [HttpGet()]
        public IActionResult GetByUrl()
        {
            try
            {
                var results = _documentManager.FindAll();
                return StatusCode((int)HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

    }
}
