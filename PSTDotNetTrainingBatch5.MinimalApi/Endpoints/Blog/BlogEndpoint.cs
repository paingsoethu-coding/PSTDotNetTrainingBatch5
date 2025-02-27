using Microsoft.AspNetCore.Mvc;
using PSTDotNetTrainingBatch5.Database.Models;

namespace PSTDotNetTrainingBatch5.MinimalApi.Endpoints.Blog;

public static class BlogEndpoint
{
    //public static string Test (this string i)
    //{
    //    return i.ToString();
    //}

    public static void UseBlogEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("/blogs", ([FromServices] AppDbContext db) =>
        {
            var model = db.TblBlogs.AsNoTracking().ToList();
            return Results.Ok(model);
        })
        .WithName("GetBlogs")
        .WithOpenApi();

        app.MapGet("/blogs/{id}", ([FromServices] AppDbContext db, int id) =>
        {
            var item = db.TblBlogs
            .AsNoTracking()
            .FirstOrDefault(x => x.BlogId == id);

            if (item is null)
            {
                return Results.BadRequest("No data found.");
            }

            return Results.Ok(item);
        })
        .WithName("GetBlog")
        .WithOpenApi();

        app.MapPost("/blogs", ([FromServices] AppDbContext db, TblBlog blog) =>
        {
            db.TblBlogs.Add(blog);
            db.SaveChanges();

            return Results.Ok(blog);
        })
        .WithName("CreateBlog")
        .WithOpenApi();

        app.MapPut("/blogs/{id}", ([FromServices] AppDbContext db, int id, TblBlog blog) =>
        {
            var item = db.TblBlogs
            .AsNoTracking()
            .FirstOrDefault(x => x.BlogId == id);

            if (item is null)
            {
                return Results.BadRequest("No data found.");
            }

            item.BlogTitle = blog.BlogTitle;
            item.BlogAuthor = blog.BlogAuthor;
            item.BlogContent = blog.BlogContent;

            // Require to tell stage changes because of AsNoTracking
            // AsNoTracking() is used to clone the object and not to track the changes
            db.Entry(item).State = EntityState.Modified;
            db.SaveChanges();

            return Results.Ok(blog);
        })
        .WithName("UpdateBlog")
        .WithOpenApi();

        app.MapPatch("/blogs/{id}", ([FromServices] AppDbContext db, int id, TblBlog blog) =>
        {
            var item = db.TblBlogs
            .AsNoTracking()
            .FirstOrDefault(x => x.BlogId == id);

            if (item is null) { return Results.NotFound(); }

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

            db.Entry(item).State = EntityState.Modified;
            db.SaveChanges();

            return Results.Ok(blog);
        })
        .WithName("PatchBlog")
        .WithOpenApi();

        app.MapDelete("/blogs/{id}", ([FromServices] AppDbContext db, int id) =>
        {
            var item = db.TblBlogs
            .AsNoTracking()
            .FirstOrDefault(x => x.BlogId == id);

            if (item is null)
            {
                return Results.BadRequest("No data found.");
            }

            db.Entry(item).State = EntityState.Deleted;
            db.SaveChanges();

            return Results.Ok();
        })
        .WithName("DeleteBlog")
        .WithOpenApi();
    }
        
}
