using Newtonsoft.Json;
using static System.Net.Mime.MediaTypeNames;
using System.Net;
using System.Text;
using RestSharp;

namespace PSTDotNetTrainingBatch5.ConsoleApp3;

public class RestClientExample
{
    // Back-end
    private readonly RestClient _client;

    private const string _postEndpoint = "https://jsonplaceholder.typicode.com/posts";

    public RestClientExample()
    {
        _client = new RestClient();
    }

    public async Task Read()
    {
        //*** Only Changes Here *** HttpClient to RestClient
        RestRequest request = new RestRequest(_postEndpoint, Method.Get);

        //var response = await _client.GetAsync(request);
        var response = await _client.ExecuteAsync(request);

        if (response.IsSuccessStatusCode)
        {
            string jsonStr = response.Content!;
            Console.WriteLine(jsonStr);
        }
    }

    public async Task Get(int id)
    {
        RestRequest request = new RestRequest($"{_postEndpoint}/{id}", Method.Get);

        //var response = await _client.GetAsync(request);
        var response = await _client.ExecuteAsync(request);

        if (response.StatusCode == HttpStatusCode.NotFound)
        {
            Console.WriteLine("No Data Found");
            return;
        }

        if (response.IsSuccessStatusCode)
        {
            string jsonStr = response.Content!;
            Console.WriteLine(jsonStr);
        }
    }

    // POST, PUT, PATCH are support for body
    public async Task Create(string title, string body, int userId)
    {
        PostModel requestModel = new PostModel()
        {
            title = title,
            body = body,
            userId = userId
        }; // C# Object || .Net Object

        RestRequest request = new RestRequest(_postEndpoint, Method.Post);
        request.AddJsonBody(requestModel);

        //var response = await _client.PostAsync(request);
        var response = await _client.ExecuteAsync(request);

        if (response.IsSuccessStatusCode)
        {
            Console.WriteLine(response.Content!);
        }
    }

    public async Task Update(int id, string title, string body, int userId)
    {
        PostModel requestModel = new PostModel()
        {
            id = id,
            title = title,
            body = body,
            userId = userId
        }; // C# Object || .Net Object

        RestRequest request = new RestRequest(_postEndpoint, Method.Put);
        request.AddJsonBody(requestModel);

        //var response = await _client.PutAsync(request);
        var response = await _client.ExecuteAsync(request);

        if (response.IsSuccessStatusCode)
        {
            Console.WriteLine(response.Content!);
        }
    }

    public async Task Patch(int id, string title, string body, int userId)
    {
        PostModel requestModel = new PostModel()
        {
            id = id,
            title = title,
            body = body,
            userId = userId
        }; // C# Object || .Net Object

        RestRequest request = new RestRequest(_postEndpoint, Method.Patch);
        request.AddJsonBody(requestModel);

        //var response = await _client.PatchAsync(request);
        var response = await _client.ExecuteAsync(request);

        if (response.IsSuccessStatusCode)
        {
            Console.WriteLine(response.Content!);
        }
    }

    #region Http နဲ Rest Client နဲက ဘာကွာသွားလဲဆိုရင် ---

    // 1. Http မှာက Execute ဆိုတာမျိုးမရှီဘူး 
    // 2. Rest Client မှာ Execute ဆိုတာမျိုးရှိတယ်ပြီးတော့
    //    အကုန်လုံးမှာအစားထိုးပြီးသုံးနိုင်ပါတယ်
    // 3. အဓိကက လက်ရှိဝင်လာတဲ့ Method.--- အနောက်မှာကပ်ပြီးထည့်တဲ့ကောင် မှန်နေဖို့ပဲလိုပါတယ်
    // 4. code ချုံချင်တယ်ဆိုရင် အကုန်လုံးမှာ Execute ကိုသုံးအဓိကဝင်လာတဲ့ နေရာကို သေချာထည့်ပေးရင်ရပါတယ်။

    #endregion

    public async Task Delete(int id)
    {
        RestRequest request = new RestRequest($"{_postEndpoint}/{id}", Method.Delete);

        //var response = await _client.DeleteAsync(request);
        var response = await _client.ExecuteAsync(request);

        if (response.StatusCode == HttpStatusCode.NotFound)
        {
            Console.WriteLine("No Data Found");
            return;
        }

        if (response.IsSuccessStatusCode)
        {
            string jsonStr = response.Content!;
            Console.WriteLine(jsonStr);
        }
    }

}
