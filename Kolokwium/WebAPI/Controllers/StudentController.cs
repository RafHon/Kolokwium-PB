using BLL.DTO;
using BLL.Interface;
using Microsoft.AspNetCore.Mvc;
using Model;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentController : ControllerBase
    {
        private readonly IStudentService _studentService;

        public StudentController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        // GET: api/Student
        [HttpGet]
        public async Task<ActionResult<IEnumerable<StudentResponseDTO>>> GetAll()
        {
            var students = await _studentService.GetAllAsync();

            var response = students.Select(s => new StudentResponseDTO
            {
                ID = s.ID,
                Imie = s.Imie,
                Nazwisko = s.Nazwisko,
                GrupaID = s.GrupaID
            });

            return Ok(response);
        }

        // GET: api/Student/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<StudentResponseDTO>> Get(int id)
        {
            var student = await _studentService.GetByIdAsync(id);
            if (student == null) return NotFound();

            var response = new StudentResponseDTO
            {
                ID = student.ID,
                Imie = student.Imie,
                Nazwisko = student.Nazwisko,
                GrupaID = student.GrupaID
            };

            return Ok(response);
        }

        // POST: api/Student
        [HttpPost]
        public async Task<ActionResult<StudentResponseDTO>> Create([FromBody] StudentRequestDTO dto)
        {
            var student = new Student
            {
                Imie = dto.Imie,
                Nazwisko = dto.Nazwisko,
                GrupaID = dto.GrupaID
            };

            var created = await _studentService.CreateAsync(student);

            var response = new StudentResponseDTO
            {
                ID = created.ID,
                Imie = created.Imie,
                Nazwisko = created.Nazwisko,
                GrupaID = created.GrupaID
            };

            return CreatedAtAction(nameof(Get), new { id = created.ID }, response);
        }

        // PUT: api/Student/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] StudentRequestDTO dto)
        {
            var existing = await _studentService.GetByIdAsync(id);
            if (existing == null) return NotFound();

            existing.Imie = dto.Imie;
            existing.Nazwisko = dto.Nazwisko;
            existing.GrupaID = dto.GrupaID;

            var updated = await _studentService.UpdateAsync(existing);
            return updated ? NoContent() : NotFound();
        }

        // DELETE: api/Student/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _studentService.DeleteAsync(id);
            return deleted ? NoContent() : NotFound();
        }
    }
}

