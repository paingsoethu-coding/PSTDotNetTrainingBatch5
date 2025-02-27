using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestSharp;

namespace PSTDotNetTrainingBatch5.RestApi3.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BurmaProjectIdeaController : ControllerBase
{
    private readonly HttpClient _httpClient;
    private readonly RestClient _restClient;
    private readonly ISnakesApi _snakesApi;

    public BurmaProjectIdeaController(HttpClient httpClient, RestClient restClient, ISnakesApi snakesApi)
    {
        _httpClient = httpClient;
        _restClient = restClient;
        _snakesApi = snakesApi;
    }

    [HttpGet("birds")]
    public async Task<IActionResult> BirdsAsync()
    {
        var response = await _httpClient.GetAsync("birds");
        var str = await response.Content.ReadAsStringAsync();
        return Ok(str);
    }

    [HttpGet("pick-a-pile")]
    public async Task<IActionResult> PickAPileAsync()
    {
        RestRequest request = new RestRequest("pick-a-pile", Method.Get);
        var response = await _restClient.GetAsync(request);
        return Ok(response.Content);
    }

    [HttpGet("snakes")]
    public async Task<IActionResult> SnakesAsync()
    {
        var response = await _snakesApi.GetSnakes();
        return Ok(response);
    }
}
