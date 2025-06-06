﻿using Microsoft.EntityFrameworkCore;
using PSTDotNetTrainingBatch5.Database.Models;

namespace PSTDotNetTrainingBatch5.Domain.Features.Blog;

public  class BlogV2Service : IBlogServiceV2
{
    private readonly AppDbContext _db;

    public BlogV2Service(AppDbContext db)
    {
        _db = db;
    }

    public async Task<List<TblBlog>> GetBlogs()
    {
        var model = await _db.TblBlogs.AsNoTracking().ToListAsync();

        return model;
    }

    public async Task<TblBlog?> GetBlog(int id)
    {
        var item = await _db.TblBlogs
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.BlogId == id);
        return item;
    }

    public async Task<TblBlog> CreateBlog(TblBlog blog)
    {
        await _db.TblBlogs.AddAsync(blog);
        await _db.SaveChangesAsync();
        return blog;
    }

    public async Task<TblBlog?> UpdateBlog(int id, TblBlog blog)
    {
        var item = await _db.TblBlogs
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.BlogId == id);
        if (item is null) return null;
       
        item.BlogTitle = blog.BlogTitle;
        item.BlogAuthor = blog.BlogAuthor;
        item.BlogContent = blog.BlogContent;

        _db.Entry(item).State = EntityState.Modified;
        await _db.SaveChangesAsync();
        return blog;
    }

    public async Task<TblBlog?> PatchBlog(int id, TblBlog blog)
    {
        var item = await _db.TblBlogs
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.BlogId == id);
        if (item is null) return null;
        
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
        return blog;
    }

    public async Task<bool?> DeleteBlog(int id)
    {
        var item = await _db.TblBlogs
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.BlogId == id);

        if (item is null) return null;
        
        item.DeleteFlag = true;
        //_db.Entry(item).State = EntityState.Deleted;
        _db.Entry(item).State = EntityState.Modified;
        var result = await _db.SaveChangesAsync();

        return result > 0;
    }

}

