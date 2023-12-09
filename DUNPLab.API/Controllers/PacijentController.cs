using DUNPLab.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace DUNPLab.API.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class PacijentController : Controller
    {
        private readonly IArhivirajPacijenteService _arhivirajPacijenteService;
        public PacijentController(IArhivirajPacijenteService arhivirajPacijenteService)
        {
            _arhivirajPacijenteService = arhivirajPacijenteService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            await _arhivirajPacijenteService.ArhivirajPacijente();
            return Ok();
        }
    }
}
