﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using PSTDotNetTrainingBatch5.Domain.Features.Blog;

namespace PSTDotNetTrainingBatch5.MinimalApi.Endpoints.Blog;

public static class BlogServiceEndpoint
{
    public static void UseBlogServiceEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapGet("/blogs", ([FromServices] IBlogService _blogService) =>
        {
            var model = _blogService.GetBlogs();

            return Results.Ok(model);
        })
        .WithName("GetServiceBlogs")
        .WithOpenApi();

        app.MapGet("/blogs/{id}", ([FromServices] IBlogService _blogService, int id) =>
        {
            var item = _blogService.GetBlog(id);
            if (item is null){return Results.BadRequest("No data found.");}

            return Results.Ok(item);
        })
        .WithName("GetServiceBlog")
        .WithOpenApi();

        app.MapPost("/blogs", ([FromServices] IBlogService _blogService, TblBlog blog) =>
        {
            var model = _blogService.CreateBlog(blog);

            return Results.Ok(model);
        })
        .WithName("CreateServiceBlog")
        .WithOpenApi();

        app.MapPut("/blogs/{id}", ([FromServices] IBlogService _blogService, int id, TblBlog blog) =>
        {
            var item = _blogService.UpdateBlog(id, blog);

            return Results.Ok(item);
        })
        .WithName("UpdateServiceBlog")
        .WithOpenApi();

        app.MapPatch("/blogs/{id}", ([FromServices] IBlogService _blogService, int id, TblBlog blog) =>
        {
            var item = _blogService.PatchBlog(id, blog);
            
            return Results.Ok(item);
        })
        .WithName("PatchServiceBlog")
        .WithOpenApi();

        app.MapDelete("/blogs/{id}", ([FromServices] IBlogService _blogService, int id) =>
        {
            var item = _blogService.DeleteBlog(id);
            
            return Results.Ok();
        })
        .WithName("DeleteServiceBlog")
        .WithOpenApi();
    }
}
