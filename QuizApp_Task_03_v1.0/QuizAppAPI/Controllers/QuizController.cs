
using AutoMapper;
using Braintree;
using Microsoft.AspNetCore.Mvc;
using NWEC.P.L001_Task3.DataAccessLayer.Models;


[Route("api/[controller]")]
[ApiController]
public class QuizController : ControllerBase
{
    private readonly IQuizService _quizService;
    private readonly IMapper _mapper;

    public QuizController(IQuizService quizService, IMapper mapper)
    {
        _quizService = quizService;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<PaginatedResult<Dto>>> GetQuizzes(int pageIndex = 1, int pageSize = 10)
    {
        var paginatedResult = await _quizService.GetQuizzesAsync(pageIndex, pageSize);
        var quizzesDto = _mapper.Map<IEnumerable<Dto>>(paginatedResult.Items);

        var result = new PaginatedResult<Dto>(
            quizzesDto.ToList(),
            paginatedResult.Items.Length,
            paginatedResult.PageIndex,
            pageSize
        );

        result.TotalPages = paginatedResult.TotalPages;
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var quiz = await _quizService.GetQuizByIdAsync(id);
        if (quiz == null)
        {
            return NotFound();
        }
        return Ok(quiz);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] Quiz quiz)
    {
        if (quiz == null)
        {
            return BadRequest();
        }

        await _quizService.AddAsync(quiz);
        return CreatedAtAction(nameof(GetById), new { id = quiz.Id }, quiz);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] Quiz quiz)
    {
        if (quiz == null || quiz.Id != id)
        {
            return BadRequest();
        }

        var updated = await _quizService.UpdateAsync(quiz);
        if (!updated)
        {
            return NotFound();
        }

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var deleted = await _quizService.DeleteAsync(id);
        if (!deleted)
        {
            return NotFound();
        }

        return NoContent();
    }
}
