using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Web.Api.Entities;

namespace Web.Api.Repository
{
    public interface ILoadingFileRepository
    {
        Extract Handle(IFormFile file);        
    }
}