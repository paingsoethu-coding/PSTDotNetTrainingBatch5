using Newtonsoft.Json;
using System.Reflection.Metadata;

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

//var summaries = new[]
//{
//    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
//};


//app.MapGet("/weatherforecast", () =>
//{
//    var forecast = Enumerable.Range(1, 5).Select(index =>
//        new WeatherForecast
//        (
//            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
//            Random.Shared.Next(-20, 55),
//            summaries[Random.Shared.Next(summaries.Length)]
//        ))
//        .ToArray();
//    return forecast;
//})
//.WithName("GetWeatherForecast")
//.WithOpenApi();

//internal record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
//{
//    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
//}


#endregion

app.MapGet("/birds", () =>
{
    string folderPath = "Data/Birds.json"!;
    var jsonStr = File.ReadAllText(folderPath);
    var result = JsonConvert.DeserializeObject<BirdResponseModel>(jsonStr)!;

    return Results.Ok(result.Tbl_Bird);
})
.WithName("GetBirds")
.WithOpenApi();


app.MapGet("/birds/{id}", (int id) =>
{
    string folderPath = "Data/Birds.json"!;
    var jsonStr = File.ReadAllText(folderPath);
    var result = JsonConvert.DeserializeObject<BirdResponseModel>(jsonStr)!;

    var item = result.Tbl_Bird.FirstOrDefault(x => x.Id == id);
    if (item is null) return Results.BadRequest("No data found.");
    
    return Results.Ok(item);
})
.WithName("GetBird")
.WithOpenApi();


app.MapPost("/birds", (BirdModel requestModel) =>
{
    string folderPath = "Data/Birds.json"!;
    var jsonStr = File.ReadAllText(folderPath);
    var result = JsonConvert.DeserializeObject<BirdResponseModel>(jsonStr)!;

    // Add id to request model id (To generate count)
    requestModel.Id = result.Tbl_Bird.Count == 0 ? 1: result.Tbl_Bird.Max(x => x.Id) + 1;
    // Insert the request model to the list
    result.Tbl_Bird.Add(requestModel);

    var jsonStrToWrite = JsonConvert.SerializeObject(result, Formatting.Indented);
    File.WriteAllText(folderPath, jsonStrToWrite);

    return Results.Ok(requestModel);
})
.WithName("PostBird")
.WithOpenApi();

app.MapPut("/birds/{id}", (int id, BirdModel requestModel) =>
{
    // File Read (like getting data from database)
    string folderPath = "Data/Birds.json"!;
    var jsonStr = File.ReadAllText(folderPath);
    var result = JsonConvert.DeserializeObject<BirdResponseModel>(jsonStr)!;

    // Find the bird by id and update its properties
    var item = result.Tbl_Bird.FirstOrDefault(x => x.Id == id);
    if (item is null) { return Results.NotFound(); }

    // Updating Process
    item.BirdMyanmarName = requestModel.BirdMyanmarName;
    item.BirdEnglishName = requestModel.BirdEnglishName;
    item.Description = requestModel.Description;
    item.ImagePath = requestModel.ImagePath;

    // File Write (like saving to database)
    var jsonStrToWrite = JsonConvert.SerializeObject(result, Formatting.Indented);
    File.WriteAllText(folderPath, jsonStrToWrite);

    return Results.Ok(item);
})
.WithName("PutBird")
.WithOpenApi();

app.MapPatch("/birds/{id}", (int id,BirdModel requestModel) =>
{
    // File Read (like getting data from database)
    string folderPath = "Data/Birds.json"!;
    var jsonStr = File.ReadAllText(folderPath);
    var result = JsonConvert.DeserializeObject<BirdResponseModel>(jsonStr)!;

    // Find the bird by id and update its properties
    var item = result.Tbl_Bird.FirstOrDefault(x => x.Id == id);

    if (item is null) { return Results.NotFound(); }

    // Patching Process
    if (!string.IsNullOrEmpty(requestModel.BirdMyanmarName))
    {
        item.BirdMyanmarName = requestModel.BirdMyanmarName;
    }
    if (!string.IsNullOrEmpty(requestModel.BirdEnglishName))
    {
        item.BirdEnglishName = requestModel.BirdEnglishName;
    }
    if (!string.IsNullOrEmpty(requestModel.Description))
    {
        item.Description = requestModel.Description;
    }
    if (!string.IsNullOrEmpty(requestModel.ImagePath))
    {
        item.ImagePath = requestModel.ImagePath;
    }

    // File Write (like saving to database)
    var jsonStrToWrite = JsonConvert.SerializeObject(result, Formatting.Indented);
    File.WriteAllText(folderPath, jsonStrToWrite);

    return Results.Ok(item);
})
.WithName("PatchBird")
.WithOpenApi();

app.MapDelete("/birds/{id}", (int id) =>
{
    string folderPath = "Data/Birds.json"!;
    var jsonStr = File.ReadAllText(folderPath);
    var result = JsonConvert.DeserializeObject<BirdResponseModel>(jsonStr)!;

    // Find the bird by id and delete it
    var item = result.Tbl_Bird.FirstOrDefault(x => x.Id == id);
    if (item is null) { return Results.BadRequest("No data found."); }
    result.Tbl_Bird.Remove(item);

    var jsonStrToWrite = JsonConvert.SerializeObject(result, Formatting.Indented);
    File.WriteAllText(folderPath, jsonStrToWrite);

    return Results.Ok(result);
})
.WithName("DeleteBird")
.WithOpenApi();


app.Run();



public class BirdResponseModel
{
    public List<BirdModel> Tbl_Bird { get; set; }
}

public class BirdModel
{
    public int Id { get; set; }
    public string BirdMyanmarName { get; set; }
    public string BirdEnglishName { get; set; }
    public string Description { get; set; }
    public string ImagePath { get; set; }
}

