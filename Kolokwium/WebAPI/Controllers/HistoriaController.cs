using BLL.DTO;
using BLL.Interface;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HistoriaController : ControllerBase
    {
        private readonly IHistoriaService _historiaService;

        public HistoriaController(IHistoriaService historiaService)
        {
            _historiaService = historiaService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<HistoriaResponseDTO>>> GetPaged(
            [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var entries = await _historiaService.GetPagedFromProcedureAsync(pageNumber, pageSize);

            var response = entries.Select(h => new HistoriaResponseDTO
            {
                ID = h.ID,
                Imie = h.Imie,
                Nazwisko = h.Nazwisko,
                GrupaID = h.GrupaID,
                TypAkcji = h.TypAkcji,
                Data = h.Data
            });

            return Ok(response);
        }
    }
}
