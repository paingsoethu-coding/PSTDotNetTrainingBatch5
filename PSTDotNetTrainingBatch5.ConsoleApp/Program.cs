// See https://aka.ms/new-console-template for more information


using Microsoft.Extensions.DependencyInjection;
using PSTDotNetTrainingBatch5.ConsoleApp;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.SqlClient;


//Console.WriteLine("Hello, World!");
//Console.Read();
//Console.ReadKey();

#region
// value: data-type အမျိုးအစားတွေကို ဖော်ပြနေတာကိုမေးရန်။

//Console.ReadLine();

//markdown

//C# <=> Database

// ADO.Net
// Dapper (ORM) Object Relational Mapper [C# က Obj နဲ Databsee က Table နဲ ညီမျှပါတယ်ဆိုတာမျိုးကိုလုပ်ချင်တာပါ ]
// EFCore / Entity Framework

// C# => Sql query

// Nuget

// Ctrl + .

// max connection = 100
// 100 = 99

//101

// ExecuteReaderAsnyc အကြောင်းကိုမေးရန် ျ await သိရန်လိုတယ် 
// dr နေရာမှာ reader ကိုပဲရွေးပြီး select မှတ်တာကိုမေးရန် ?1 ိdone (Alt + left click drag)
// မှားပြီး commit all လုပ်လိုက်တာကို push မလုပ်ခင် message ပြန်ပြင်ချင်ရင် ဘယ်လိုပြန်လုပ်လို့ရလဲ နာမည်ပြန်ခွဲပြီးပေးချင်တာမျိုးပါ ?2


//Array ကြော်ငြာတာပါ 
//string[] apple1 = new string[3];

//string[] apple = { "1", "2" };
//Console.WriteLine(apple[0]);
//Console.WriteLine(apple[1]);

//နောက်ဆုံး အခန်းကို C# မှာ စစ်လို့မရတော့ပါ C မှာပဲ ရပါတော့တယ်



//abstract class Fruit
//{
//    public abstract void Apple();
//    public abstract void Orange();
//}

//class Type : Fruit
//{
//    public override void Apple()
//    {
//        Console.WriteLine("Apple");
//    }
//    public override void Orange()
//    {
//        Console.WriteLine("Orange");
//    }
//}

//class Program
//{
//    static void Main(string[] args)
//    {
//        Type type = new Type();
//        type.Apple();
//        type.Orange();
//    }
//}

#endregion

//AdoDotNetExample adoDotNetExample = new AdoDotNetExample();
//adoDotNetExample.Read();
//adoDotNetExample.Create();
//adoDotNetExample.Edit();
//adoDotNetExample.Update();
//adoDotNetExample.Delete();

//DapperExample dapperExample = new DapperExample();
//dapperExample.Read();
//dapperExample.Create("Test-2", "PST-2", "Omen-2");
//dapperExample.Edit(1);
//dapperExample.Edit(2);
//dapperExample.Update();
//dapperExample.Delete();

//EFCoreExample eFCoreExample = new EFCoreExample();
//eFCoreExample.Read();
//eFCoreExample.Create("Test-3", "PST-3", "Omen-3");
//eFCoreExample.Edit(11);
//eFCoreExample.Update(11, "Test-11", "PST-11", "Omen-11");
//eFCoreExample.Delete(12);
//eFCoreExample.DeleteFlag(13);

//DapperExample2 dapperExample2 = new DapperExample2();
//dapperExample2.Read();
//dapperExample2.Edit(1);

//string query = " [BlogTitle] = @BlogTitle, ";
//Console.WriteLine(query.Substring(0, query.Length - 2)); // Substring Delete လုပ်တာပါ


// Ado.Net Dependency Injection
var services = new ServiceCollection().AddSingleton<AdoDotNetExample>()
    .BuildServiceProvider();

// ံေိ်္ိ္HomeWork (Ado, Dapper, EFcore, and RestApi) Using DI

var adoDotNetExample = services.GetRequiredService<AdoDotNetExample>();
adoDotNetExample.Read();


Console.ReadKey();
