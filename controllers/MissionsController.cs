namespace dsw_3p_pr01.Controllers;

using dsw_3p_pr01.Models;
using dsw_3p_pr01.Services;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class MissionsController : ControllerBase {

    private readonly IMissionService _service;

    public MissionsController(IMissionService service) {
        _service = service;
    }

    [HttpGet]
    [ProducesResponseType(typeof(List<Mission>), StatusCodes.Status200OK)]
    public ActionResult<List<Mission>> GetAll() {
        return Ok(_service.GetAll());
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(Mission), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<Mission> GetById(int id) {
        var mission = _service.GetById(id);
        if (mission == null) {
            return NotFound();
        }

        return Ok(mission);
    }

    [HttpPost]
    [ProducesResponseType(typeof(Mission), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public ActionResult<Mission> Create([FromBody] Mission mission) {
        var created = _service.Create(mission);
        if (created == null) {
            return BadRequest();
        }

        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult Update(int id, [FromBody] Mission mission) {
        if (!_service.Update(id, mission)) {
            return NotFound();
        }

        return NoContent();
    }

    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult Delete(int id) {
        if (!_service.Delete(id)) {
            return NotFound();
        }

        return NoContent();
    }
}
