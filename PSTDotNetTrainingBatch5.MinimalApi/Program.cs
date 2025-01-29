var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

#region Old Code
//app.MapGet("/blogs", () => 
//{
//    AppDbContext db = new AppDbContext();
//    var model = db.TblBlogs.AsNoTracking().ToList();
//    return Results.Ok(model);
//})
//.WithName("GetBlogs")
//.WithOpenApi();

//app.MapGet("/blogs/{id}", (int id) =>
//{
//    AppDbContext db = new AppDbContext();
//    var item = db.TblBlogs
//    .AsNoTracking()
//    .FirstOrDefault(x => x.BlogId == id);

//    if (item is null)
//    {
//        return Results.BadRequest("No data found.");
//    }

//    return Results.Ok(item);
//})
//.WithName("GetBlog")
//.WithOpenApi();

//app.MapPost("/blogs", (TblBlog blog) =>
//{
//    AppDbContext db = new AppDbContext();
//    db.TblBlogs.Add(blog);
//    db.SaveChanges();

//    return Results.Ok(blog);
//})
//.WithName("CreateBlog")
//.WithOpenApi();

//app.MapPut("/blogs/{id}", (int id,TblBlog blog) =>
//{
//    AppDbContext db = new AppDbContext();
//    var item = db.TblBlogs
//    .AsNoTracking()
//    .FirstOrDefault(x => x.BlogId == id);

//    if (item is null)
//    {
//        return Results.BadRequest("No data found.");
//    }

//    item.BlogTitle = blog.BlogTitle;
//    item.BlogAuthor = blog.BlogAuthor;
//    item.BlogContent = blog.BlogContent;

//    // Require to tell stage changes because of AsNoTracking
//    // AsNoTracking() is used to clone the object and not to track the changes
//    db.Entry(item).State = EntityState.Modified; 
//    db.SaveChanges();

//    return Results.Ok(blog);
//})
//.WithName("UpdateBlog")
//.WithOpenApi();

//app.MapPatch("/blogs/{id}", (int id, TblBlog blog) =>
//{
//    AppDbContext db = new AppDbContext();
//    var item = db.TblBlogs
//    .AsNoTracking()
//    .FirstOrDefault(x => x.BlogId == id);

//    if (item is null) { return Results.NotFound(); }

//    if (!string.IsNullOrEmpty(blog.BlogTitle))
//    {
//        item.BlogTitle = blog.BlogTitle;
//    }
//    if (!string.IsNullOrEmpty(blog.BlogAuthor))
//    {
//        item.BlogAuthor = blog.BlogAuthor;
//    }
//    if (!string.IsNullOrEmpty(blog.BlogContent))
//    {
//        item.BlogContent = blog.BlogContent;
//    }

//    db.Entry(item).State = EntityState.Modified;
//    db.SaveChanges();

//    return Results.Ok(blog);
//})
//.WithName("PatchBlog")
//.WithOpenApi();

//app.MapDelete("/blogs/{id}", (int id) =>
//{
//    AppDbContext db = new AppDbContext();
//    var item = db.TblBlogs
//    .AsNoTracking()
//    .FirstOrDefault(x => x.BlogId == id);

//    if (item is null)
//    {
//        return Results.BadRequest("No data found.");
//    }

//    db.Entry(item).State = EntityState.Deleted;
//    db.SaveChanges();

//    return Results.Ok();
//})
//.WithName("DeleteBlog")
//.WithOpenApi();
#endregion

#region "this" extendion method
//  if we use this, we can convert it to extension method.
//  we have to declare static current class and every class below it and use this keyword.
//  we can use 1.test and call extension method.
//  BlogEndpoint.Test("a");
//  "a".Test();
#endregion

app.UseBlogEndpoints();

app.Run();

