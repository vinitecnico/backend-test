using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Api.Repository;
using Microsoft.AspNetCore.Mvc;
using Web.Api.Entities;
using Microsoft.AspNetCore.Http;

namespace Web.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExtractController : ControllerBase
    {
        private readonly IExtractRepository _extractRepository;

        public ExtractController(IExtractRepository extractRepository)
        {
            _extractRepository = extractRepository;
        }

        [HttpPost("UploadFileDbLog")]
        public async Task<bool> UploadFileDbLog(IFormFile file)
        {
            return await _extractRepository.UploadFileDbLog(file);
        }

        [HttpGet("GetAllMovements")]
        public async Task<List<Extract>> GetAllMovements()
        {
            var result = await _extractRepository.GetAllMovements();
            return result;
        }
    }
}
