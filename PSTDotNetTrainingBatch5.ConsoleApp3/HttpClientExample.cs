using Newtonsoft.Json;
using System.Net;
using System.Text;
using static System.Net.Mime.MediaTypeNames;

namespace PSTDotNetTrainingBatch5.ConsoleApp3;

public class HttpClientExample
{
    private readonly HttpClient _client;

    private const string _postEndpoint = "https://jsonplaceholder.typicode.com/posts";

    public HttpClientExample()
    {
        _client = new HttpClient();
    }

    public async Task Read()
    {
        var response = await _client.GetAsync(_postEndpoint);
        if (response.IsSuccessStatusCode)
        {
            string jsonStr = await response.Content.ReadAsStringAsync();
            Console.WriteLine(jsonStr);
        }
    }

    public async Task Get(int id)
    {
        var response = await _client.GetAsync($"{_postEndpoint}/{id}");
        if (response.StatusCode == HttpStatusCode.NotFound)
        {
            Console.WriteLine("No Data Found");
            return;
        }

        if (response.IsSuccessStatusCode)
        {
            string jsonStr = await response.Content.ReadAsStringAsync();
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

        var jsonRequest = JsonConvert.SerializeObject(requestModel);
        var content = new StringContent(jsonRequest,
            Encoding.UTF8, Application.Json); //"application/json" same with last one
        var response = await _client.PostAsync(_postEndpoint, content); // .Result can also be used

        if (response.IsSuccessStatusCode)
        {
            Console.WriteLine(await response.Content.ReadAsStringAsync());
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

        var jsonRequest = JsonConvert.SerializeObject(requestModel);
        var content = new StringContent(jsonRequest,
            Encoding.UTF8, Application.Json); //"application/json" same with last one
        var response = await _client.PutAsync($"{_postEndpoint}/{id}", content); // .Result can also be used

        if (response.IsSuccessStatusCode)
        {
            Console.WriteLine(await response.Content.ReadAsStringAsync());
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

        var jsonRequest = JsonConvert.SerializeObject(requestModel);
        var content = new StringContent(jsonRequest,
            Encoding.UTF8, Application.Json); //"application/json" same with last one
        var response = await _client.PatchAsync($"{_postEndpoint}/{id}", content); // .Result can also be used

        if (response.IsSuccessStatusCode)
        {
            Console.WriteLine(await response.Content.ReadAsStringAsync());
        }
    }


    #region Real world projects တွေမှာ Http-POST method ပဲ ဘာလိုရှိနေရတာလဲဆိုရင် ---

    // *** 1. Aware: Get and Delete are not support for body. ***
    // 2. တချို့ Real world projects တွေမှာ body ထည့်ပေးရတာမျိုးဆိုရင် Listing or GetByID
    //    ဘာကိုပဲဆွဲဆွဲ Http-POST method နဲပဲလုပ်ထားတာမျိုးတွေရှိပါတယ်။
    // ာ3. ဘာလို့အခုလိုလုပ်တာလဲဆိုရင် System တစ်ခုလုံးကို စနစ်ကျစေချင်လို့ပါ။
    // 4. ဒါက Http Standard တော့မဖြစ်တော့ဘူး ဒါပေမယ့်
    // 5. ဘာလို့အခုလို သုံးမရတာလဲဆိုရင် Get and Delete က သူသည် body ကိုမသယ်ပေးနိုင်ဘူး
    // 6. သူသည် Url ကနေပဲ Parameter ကနေပဲ သယ်လိုရတာကြောင့်ဖြစ်ပါတယ်။

    // --- ပြီးတော့ body encryption လုပ်ချင်တယ်ဆိုရင် ---
    // 1. Get မှာဆိုရင် encryption လုပ်ရင် Data တွေက မလိုအပ်ပဲနဲ ပွထပြီး အရှည်ကြီးဖြစ်တဲ့ ကိစ္စတွေရှိတက်တယ်။
    // 2. Url မှာက သတ်မှတ်ထားတဲ့ ပမာဏရှိပြီး အဲ့ကောင်က
    //    ***Url maximun length is 2048 characters.***
    // 3. I will delete more than 2048 characters in url.
    // 4. Browser တွေ အပေါ်မူတည်တယ် တစ်ခုနဲ တစ်ခုက မတူဘူး။ သေချာတာက အရမ်းရှည်ရင် ဖြတ်ချပါတယ်။
    // *** Url မှာ Data တွေ အများကြီးသယ်နေဖို့ဆိုတာ လုံးဝကို အဆင်မပြေပါဘူး။ ***
    // 5. System တစ်ခုမှာ စနစ်ကျဖို့ဆိုရင် Http-POST ကိုတစ်ခုပဲ သုံးပြီး အဲ့အတိုင်းကိုပဲ
    //    Data တွေ အသယ်အပြုလိုတာလုပ်ရတာပါ။ ဲဒါမှ အဆင်ပြေတာမလို့ပါ။
    // 6. ံHttp Standard မရှိဘူးဆိုပြီး Blame တာမျိုးရှိတယ် (System Requirement for work-flow ပါ)
    // 7. ဒါပေမယ့် ဒါက အလုပ်ရဲ့ သဘာဝအရပါ။ (ဘာကြောင့်ဒါမျိုးသုံးရတာလဲ? ဆိုတာရဲ့ အဖြေပါ။)

    #endregion

    public async Task Delete(int id)
    {
        var response = await _client.DeleteAsync($"{_postEndpoint}/{id}");
        if (response.StatusCode == HttpStatusCode.NotFound)
        {
            Console.WriteLine("No Data Found");
            return;
        }

        if (response.IsSuccessStatusCode)
        {
            string jsonStr = await response.Content.ReadAsStringAsync();
            Console.WriteLine(jsonStr);
        }
    }
}


public class PostModel
{
    public int userId { get; set; }
    public int id { get; set; } // auto increment
    public string title { get; set; }
    public string body { get; set; }
}
