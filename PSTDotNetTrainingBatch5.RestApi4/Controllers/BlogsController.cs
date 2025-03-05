using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PSTDotNetTrainingBatch5.Database.Models;

namespace PSTDotNetTrainingBatch5.RestApi4.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BlogsController : ControllerBase
{
    private readonly AppDbContext _db;

    public BlogsController(AppDbContext db)
    {
        _db = db;
    }

    [HttpGet]
    public async Task<IActionResult> GetBlogs()
    {
        var lst = await _db.TblBlogs
            .AsNoTracking()
            .Where(x => x.DeleteFlag == false)
            .ToListAsync();

        return Ok(lst);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetBlog(int id)
    {
        var item = await _db.TblBlogs
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.BlogId == id);
        if (item is null) { return NotFound(); }

        return Ok(item);
    }

    [HttpPost]
    public async Task<IActionResult> CreateBlog(TblBlog blog)
    {
        await _db.TblBlogs.AddAsync(blog);
        await _db.SaveChangesAsync();

        return Ok(blog);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateBlog(int id, TblBlog blog)
    {
        var item = await _db.TblBlogs
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.BlogId == id);
        if (item is null) { return NotFound(); }

        item.BlogTitle = blog.BlogTitle;
        item.BlogAuthor = blog.BlogAuthor;
        item.BlogContent = blog.BlogContent;

        _db.Entry(item).State = EntityState.Modified;
        await _db.SaveChangesAsync();

        return Ok(item);
    }

    [HttpPatch("{id}")]
    public async Task<IActionResult> PatchBlogs(int id, TblBlog blog)
    {
        var item =  await _db.TblBlogs
            .AsNoTracking()
            .Where(x => x.DeleteFlag == false)
            .FirstOrDefaultAsync(x => x.BlogId == id);

        if (item is null) { return NotFound(); }

        if (!string.IsNullOrEmpty(blog.BlogTitle))
        {
            item.BlogTitle = blog.BlogTitle;
        }
        if (!string.IsNullOrEmpty(blog.BlogAuthor))
        {
            item.BlogAuthor = blog.BlogAuthor;
        }
        if (!string.IsNullOrEmpty(blog.BlogContent))
        {
            item.BlogContent = blog.BlogContent;
        }

        _db.Entry(item).State = EntityState.Modified;
        await _db.SaveChangesAsync();

        return Ok(item);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteBlogs(int id)
    {
        var item = await _db.TblBlogs
            .AsNoTracking()
            .Where(x => x.DeleteFlag == false)
            .FirstOrDefaultAsync(x => x.BlogId == id);

        if (item is null) { return NotFound(); }

        item.DeleteFlag = true;
        _db.Entry(item).State = EntityState.Modified;
        await _db.SaveChangesAsync();

        return Ok(item);
    }
}
