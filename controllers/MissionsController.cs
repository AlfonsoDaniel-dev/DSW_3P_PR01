namespace dsw_3p_pr01.Controllers;

using dsw_3p_pr01.Models;
using dsw_3p_pr01.Services;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class MissionsController : ControllerBase {

    private readonly IMissionService _service;

    public MissionsController(IMissionService service) {
        _service = service;
    }

    [HttpGet]
    public ActionResult<List<Mission>> GetAll() {
        return Ok(_service.GetAll());
    }

    [HttpGet("{id:int}")]
    public ActionResult<Mission> GetById(int id) {
        var mission = _service.GetById(id);
        if (mission == null) {
            return NotFound();
        }

        return Ok(mission);
    }

    [HttpPost]
    public ActionResult<Mission> Create([FromBody] Mission mission) {
        var created = _service.Create(mission);
        if (created == null) {
            return BadRequest();
        }

        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpPut("{id:int}")]
    public IActionResult Update(int id, [FromBody] Mission mission) {
        if (!_service.Update(id, mission)) {
            return NotFound();
        }

        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public IActionResult Delete(int id) {
        if (!_service.Delete(id)) {
            return NotFound();
        }

        return NoContent();
    }
}
