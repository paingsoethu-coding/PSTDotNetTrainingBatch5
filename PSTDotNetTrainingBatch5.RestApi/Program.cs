using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using PSTDotNetTrainingBatch5.Database.Models;
using PSTDotNetTrainingBatch5.Domain.Features.Blog;

var builder = WebApplication.CreateBuilder(args);

#region DI
//    DI ကို ဘ‌ယ်လိုသုံးရမလဲ
//    DI ဆိုတာက builder.Services ထဲကို လာထည့်တာမျိုးကို ခေါ်တာပါ။
// 1. DI ကိုသုံးတော့မယ်ဆိုရင် လက်ရှိကို သုံးချင်တဲ့ Method ကိုအရင်ဆုံး Register လုပ်ရပါမယ်။
// 2. builder.Services.AddDbContext<AppDbContext> က ီDA လုပ်နေတာပါ
//    အဲ့တော့သူက Register ဖြစ်သွားပါတယ်။
// 3. builder.Services.AddScoped<IBlogService, BlogService>(); က ီBL လုပ်နေတာပါ
//    အဲ့တော့သူကလည်း Register ဖြစ်သွားပါတယ်။
// 4. သူကို ဘာလို့ Interface ထည့်ပေးလဲဆိုရင် Service ကနောက်တစ်ခုပြောင်းတာနဲ Method တူနေရင်
//    အဲ့ကောင်ကိုပြန်ထည့်းပေးလိုရင် အကုန်လိုက်ပြင်စရာမလို့တော့အောင်ပါ။ DI ရဲ့ အားသာချက်ပါပဲ။
// 5. *** Registerတွေကို အစဉ်လိုက်လုပ်ပေးသင့်တယ် Project ကြီးလာရင် Errors တက်လာနိုင်ချေများပါတယ်။ ***
// 6. UI BL DA တွေကို ပြောင်းပြန်လှန်ပြီး Injection ထိုးပေးဖို့လိုပါတယ်။ 
//    *** အဓိကက ကိုယ်အရင်သုံးချင်တဲ့ Service ကိုအရင်ထိုးပေး အရင် Register လုပ်ပေးဖို့လိုပါတယ်။ ***

//    DI ဆိုတာ ဘာလဲ
// 1. Class တွေမှာ const or instance ထည့်တဲ့အခါမှာ ဆောက်စရာမလိုဘဲနဲ ကိုလိုချင်တဲ့ Services ကို
//    Register လုပ်လိုက်ရုံနဲပဲ Class တိုင်းကနေ ပြီးတော့ ယူသုံးလိုရတယ်။
//    *** ပြီးတော့သူက Memory ပေါ်မှာသွားတဲ့ကိစ္စတွေကိုလည်း မလိုအပ်ရင် တစ်ခါတည်းရှင်းထုတ်ပေးတယ်။ ***
//    *** အဓိကတစ်ခုအနေနဲ ပြင်ရပြုရလွယ်ကူအောင်လည်း တစ်ခါတည်းလုပ်ပေးတယ်။ ***
//    အရင်ကသုံးခဲ့တဲ့ Hard-Code cases တွေဆိုရင်လည်း ဒီကောင်တွေသုံးလိုက်တာနဲ သက်သာစေပါတယ်။

// => DI ၃ခု ရှိတယ်။ 1. Constructor Injection 2. Method Injection 3. Property Injection
// 1. Constructor Injection ကတော့ Class တစ်ခုဆောက်တယ် instance ဆောက်ပြီးစစချင်မှာ
//    ဝင်လာမယ့်နေရာမျို့မှာ ထိုးတဲ့ Injection ကိုခေါ်တာပါ။
// 2. Property Injection ကတော့ Get SET ခေါ်တဲ့နေရာမှာ အပေါ်ကနေ [Injection] ဆိုပြီးရေးတာမျိုးကိုခေါ်တာပါ။
// 3. Method Injection ကတော့ ရေးလိုက်တဲ့ Method တစ်ခုရဲ့ အနောက်က Parameter မှာဝင်တဲ့နေရာမှာ
//    ဝင်လာတဲ့ကောင်ရဲ့ Injection ကို Method Injection လိုခေါ်ပါတယ်။ အလုပ်ရှုပ်မယ့် ကိစ္စတွေကို သက်သာစေပါတယ်။

//    DI ရဲ့ SingleTon life cycle တွေဆိုတာဘာလဲ 
// 1. SingleTon ဆိုတာက သူက Service တစ်ခုကို instance တစ်ခုပဲဆောက်တယ်ပြီးရင် အကုန်လုံးက Sharing သုံးတယ်။ 
// 2. အဲ့ချိန်နောက်တစ်ခုက ဝင်လာမယ်ဆိုရင်‌ နောက်ဆုံးလူကထားခဲ့မယ်ဆိုတဲ့ values ကိုပဲ သူကပြန်ထုတ်ပေးမယ်။
// 3. အလုအယှက် ကို ဝိုင်းပြင်ခဲ့တဲ့ အခါမျိုးတွေဆိုရင်တော့ ပြသာနာတက်နိုင်ပါတယ်။ SingleTon သုံးတာကတော့ရှားပါတယ်။
// 4. ှSingleTon ဆိုတာက ရေတစ်ဘူးကို အကုန် Sharing သုံးနေတာနဲတူတူပါပဲ။
// 5. SingleTon ကို မပြောင်းလဲနိုင်တဲ့ ဟာမျိုးကိုသုံးတယ်။
// 6. ံHelper Method/Class တို့ Utility Class/ static method တို့ ကိုယ်ရေးတဲ့ Code တွေကိုလွယ်ကူအောင်
//    ကူညီပေးတဲ့ Class အတွက်ဆိုရင် အဲ့ကောင်  ှSingleTon ကို သုံးပါတယ်။
// 7. အဓိကက Changes များများမရှိတာမျိုးအတွက်ကြောင့် သုံးတာပါ။ 
// 8. Services လိုကောင်မျိုးတွေကိုဆိုရင်တော့  ှSingleTon ကိုမသုံးတာကောင်းပါတယ်။ ပြန်ပေးလာမယ့် values တွေက
//    တစ်ခုနဲတစ်ခုမတူနိုင်တာကြောင့်ပါ။
// 9. ဒီကောင်တွေမှာကိုဆီ Life-time တွေရှိကျပါတယ်။
// 10.*** API request အရေအတွက်ကတော့ limit လုပ်မထားပါဘူး ***

//    DI ရဲ့ Scoped life cycle တွေဆိုတာဘာလဲ
// 1. သူကကျတော့ Request တစ်ခုဆီအတွက်ကို instance တစ်ခုဆီပေးတယ်။ သူက Request တစ်ခုကနေပြီးတော့
//    Response မပြန်မချင်း၊ သူအလုပ်ကမပြီးမချင်းကိုတော့ ပထမ instance ကိုပဲ ပြန်ထုတ်ပေးတယ်။
// -  ဒါပေမယ့် အဲ့ထဲမှာ Request တစ်ခုချင်းဆီအတွက်ကို instance တွေဆောက်ပြီးပေးမယ်၊ မလိုတဲ့အကောင်တွေကိုရှင်းထုတ်ပေးတယ်။
// -  နောက်တစ်ခု Request လာရင်းလည်း Service တစ်ခုကို instance ဆောက်ပေးထားလားပြန်မေးတယ်။
//    ပြီးတော့အဲ့လို ပြန်အလုပ်လုပ်ပေးတယ်။ 
// -  အဲ့ဒါကြောင့် instance တွေက နစ်ခုကွာသွားပြီးတော့ Request တစ်ခုဆီအလိုက်ကိုအလုပ်လုပ်နေတယ် Response မပြန်မချင်းအထိပါ။
// 2. ***ဒီကောင်အခန်းတစ်ခုနဲတူတယ် အခန်းထဲကိုဝင်လာပြီး အခန်းထဲကဟာတွေအကုန်ကြိုက်တာခွဲရတယ်။
//    ဒါပေမယ့်အခန်းထဲကနေ ထွက်သွားပြီး ြပြန်ဝင်လာရင်တော့ အခန်းကအရင်အတိုင်းပြန်ဖြစ်သွားပါတယ်။***
// -  ဒီကောင်တွေမှာကိုဆီ Life - time တွေရှိကျပါတယ်။
// -  Project ကြီးလာတဲ့ အချိန်တွေမှာဆိုရင် Scoped ကိုသုံးနေတယ်ဆိုပေမယ် Life time တွေအတွက်ကို အသစ်ပြန်
//    ‌ေဆောင်စေချင်တယ်ဆိုရင် Transient တွေကို ထည့်ပေးရတာတွေလည်းရှိပါတယ်။
// -  Eg. AddDbContext<AppDbContext> ကို အဲ့ Add တဲ့နေရာမှာဆိုရင် GetConnectionString ရယ်ပြီးတော့
//    [Context ရဲ့ Lifetime အချိန်ပါ] ServiceLifetime.Transient, [options ရဲ့ Lifetime အချိန်ပါ]
//    ServiceLifetime.Transient ); အဲ့ပုံစံနဲက နေရာတိုင်းက AppDbContext ကိုယူသုံးတိုင်းအသစ်ဆောက်ပေးစေချင်လို့ပါ။
//    မဟုတ်ရင် တစ်ခုကယူသုံးပြီ နောက်တစ်ခုထပ်ယူသုံးနေတုန်း အရှေ့တစ်ခုပြီးလို့ ဖြတ်သွားတဲ့အခါ တက်လာနိုင်တဲ့ ပြသာနာတွေကို
//    ဖြေရှင်းပြီးသား ဖြစ်သွားချင်လို့ပါ။

//    DI ရဲ့ Transient life cycle တွေဆိုတာဘာလဲ
// 1. ဒီကောင်ကျတော့ Request တစ်ခုလာတိုင်း instance တစ်ခုဆောက်တယ်ပြီးရင် သုံးနေတာပါ။
// 2. သူက ီDA မှာ instance တစ်ခု BL မှာ instance တစ်ခု UI မှာ instance တစ်ခု အဲ့လိုမျိုးကို Response အထိကို
//    သူက instance တစ်ခုလိုတိုင်း အသစ်ဆောက်ဆောက်ပြီးကိုသုံးတာမျိုးပါ။
// 3. နောက် Request တစ်ခုလာမယ်ဆိုရင်လည်း အဲလိုမျိုးပဲ သုံးတာပါ။
// 4. အဲ့ဒါကြောင့် သူကကျတော့ တစ်ခန်းဝင်ရတဲ့ ရေတစ်ဘူးရတယ် ရတဲ့ တစ်ဘူးကိုအကုန်သုံးတယ်၊ နောက်တစ်ခန်းဝင်တယ်
//    နောက် ရေတစ်ဘူးအသစ်ရတယ် ရတာကို အကုန်သောက်ဆက်သွားတယ် Response အထိအဲ့လိုမျိုးပါပဲ။
// 5. *** အားသာချက်ကကျတော့ Multi-Threading လုပ်လိုက်တာမျိုးကိုဆိုရင် အသုံးများဆုံးနဲ safest optionဖြစ်တယ်။ ***
//    - ပုံမှန်ကတော့ ဲဒီကောင်ကို DbContext တွေမှာအသုံးများဆုံးဖြစ်တယ်။
// 6. ဒီကောင်တွေမှာကိုဆီ Life-time တွေရှိကျပါတယ်။ middleware မှာ Transient ကိုသုံးကျပါတယ်။

#endregion


builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DbConnection"));
}, ServiceLifetime.Transient, ServiceLifetime.Transient );


//builder.Services.AddScoped<IBlogService, BlogService>();
builder.Services.AddScoped<IBlogServiceV2, BlogV2Service>();



// Add services to the container.
builder.Services.AddControllers();
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

app.UseAuthorization();

app.MapControllers();

app.Run();
