using Microsoft.AspNetCore.Mvc;
using Refit;
using RestSharp;
using System.Net.Http;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Injecting Register Stages as like calling URL
builder.Services.AddSingleton(n => new HttpClient()
{
    BaseAddress = new Uri(builder.Configuration.GetSection("ApiDomainUrl").Value!)
});

builder.Services.AddSingleton(n =>
    new RestClient(builder.Configuration.GetSection("ApiDomainUrl").Value!));

builder.Services
    .AddRefitClient<ISnakesApi>()
    .ConfigureHttpClient(c => c.BaseAddress = new Uri(
        builder.Configuration.GetSection("ApiDomainUrl").Value!));



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Injecting Stages
app.MapGet("/birds", async ([FromServices] HttpClient httpclient) =>
{
    var response = await httpclient.GetAsync("birds");
    return await response.Content.ReadAsStringAsync();
});

app.MapGet("/pick-a-pile", async ([FromServices] RestClient restClient) =>
{
    RestRequest request = new RestRequest("pick-a-pile", Method.Get);
    var response = await restClient.GetAsync(request);
    return response.Content;
});

// *** refit should use try catch because it will throw exception when it is not success ***
app.MapGet("/snakes", async ([FromServices] ISnakesApi snakesApi) =>
{
    var response = await snakesApi.GetSnakes();
    return response;
});


app.Run();


public interface ISnakesApi
{
    [Get("/snakes")]
    Task<List<SnakeModel>> GetSnakes();
}


public class SnakeModel 
{
    public int Id { get; set; }
    public string ImageUrl { get; set; }
    public string MMName { get; set; }
    public string EngName { get; set; }
    public string Detail { get; set; }
    public string IsPoison { get; set; }
    public string IsDanger { get; set; }
}
