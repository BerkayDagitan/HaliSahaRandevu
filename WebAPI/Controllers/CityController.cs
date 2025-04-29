using BusinessLayer.Interfaces;
using DataAccessLayer.Context;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CityController : ControllerBase
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ICityApiServices _services;
        private readonly ProjectContext _db;

        public CityController(ProjectContext db, ICityApiServices services, HttpClient httpClient, IHttpContextAccessor httpContextAccessor)
        {
            if (httpClient.BaseAddress == null)
            {
                httpClient.BaseAddress = new Uri("http://localhost:5138/api/");
            }
            _db = db;
            _services = services;
            _httpClient = httpClient;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpGet("List")]
        public async Task<IActionResult> ListCity()
        {
            var result = await Task.FromResult(_db.Citys.ToList());
            if (result != null && result.Any())
            {
                return Ok(result);
            }
            return NotFound("Şehir bulunamadı.");
        }
    }
}