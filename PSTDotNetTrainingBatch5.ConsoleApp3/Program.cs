// See https://aka.ms/new-console-template for more information
using PSTDotNetTrainingBatch5.ConsoleApp3;

Console.WriteLine("Hello, World!");

//Get
//Put
//Post
//Patch
//Delete

//Resource
//Endpoint (same)


//HttpClientExample client = new HttpClientExample();
//await clinet.Read();
//await client.Get(1);
//await client.Get(101);
//await client.Create("test title", "test body", 1);
//await client.Update(1,"test title", "test body", 10);
Console.WriteLine("Waiting for Api...");
Console.ReadLine();

RefitExample refit = new RefitExample();
//await refit.RunGet();
//await refit.RunCreate();
//await refit.RunUpdate();
//await refit.RunPatch();
await refit.RunDelete();


Console.ReadLine();

