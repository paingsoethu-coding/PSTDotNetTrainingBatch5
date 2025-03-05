using Microsoft.AspNetCore.Mvc;
using PSTDotNetTrainingBatch5.Database.Models;
using PSTDotNetTrainingBatch5.Domain.Features.Blog;

namespace PSTDotNetTrainingBatch5.RestApi4.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BlogServiceController : ControllerBase
{
    public readonly IBlogServiceV2 _service;

    public BlogServiceController(IBlogServiceV2 service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetBlogs()
    {
        var lst = await _service.GetBlogs();
        return Ok(lst);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetBlog(int id)
    {
        var item = await _service.GetBlog(id);
        if (item is null) { return NotFound(); }
        return Ok(item);
    }

    [HttpPost]
    public async Task<IActionResult> CreateBlog(TblBlog blog)
    {
        var model = await _service.CreateBlog(blog);

        return Ok(model);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateBlog(int id, TblBlog blog)
    {
        var item = await _service.UpdateBlog(id, blog);
        if (item is null) { return NotFound(); }

        return Ok(item);
    }

    [HttpPatch("{id}")]
    public async Task<IActionResult> PatchBlogs(int id, TblBlog blog)
    {
        var item = await _service.PatchBlog(id, blog);
        if (item is null) { return NotFound(); }

        return Ok(item);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteBlogs(int id)
    {
        var item = await _service.DeleteBlog(id);
        if (item is null) { return NotFound(); }

        return Ok("Delete Success");
    }
}
