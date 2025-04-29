using BusinessLayer.Interfaces;
using DataAccessLayer.Context;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PitchController : ControllerBase
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly HttpClient _httpClient;
        private readonly ProjectContext _db;
        private readonly IPitchApiServices _services;

        public PitchController(ProjectContext db, IPitchApiServices services, HttpClient httpClient, IHttpContextAccessor httpContextAccessor)
        {
            if (httpClient.BaseAddress == null)
            {
                httpClient.BaseAddress = new Uri("http://localhost:5138/api/");
                Console.WriteLine("set edildi: " + httpClient.BaseAddress);
            }
            _db = db;
            _services = services;
            _httpClient = httpClient;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpGet("List")]
        public async Task<IActionResult> ListPitch()
        {
            var result = await Task.FromResult(_db.Pitches.ToList());
            if (result != null && result.Any())
            {
                return Ok(result);
            }
            return NotFound("Saha bulunamadı.");
        }
    }
}
