using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PSTDotNetTrainingBatch5.ConsoleApp4.Models;

namespace PSTDotNetTrainingBatch5.ConsoleApp4;

// Crt + R and Crt + I for getting interface
public class EFCoreExample : IEFCoreExample
{
    private readonly AppDbContext _db;

    public EFCoreExample(AppDbContext db)
    {
        _db = db;
    }

    public async void Read()
    {
        // Tolist နောက်မှရေးရင် DB ကဆွဲထုတ်ပြီးတော့မှ ပြန် filter ဖြစ်သွားမှာပါ။
        var lst = await _db.Blogs
            .Where(x => x.DeleteFlag == false).ToListAsync();

        foreach (var item in lst)
        {
            Console.WriteLine(item.BlogId);
            Console.WriteLine(item.BlogTitle);
            Console.WriteLine(item.BlogAuthor);
            Console.WriteLine(item.BlogContent);
        }
    }

    public async void Create(string title, string author, string content)
    {
        var blog = new BlogDataModel
        {
            BlogTitle = title,
            BlogAuthor = author,
            BlogContent = content,
        };

        await _db.Blogs.AddAsync(blog);
        var result = await _db.SaveChangesAsync(); // Execute လုပ်တာပါ

        Console.WriteLine(result == 1 ? "Saving Successful." : "Saving Failed.");
    }

    public async void Edit(int id)
    {
        var item = await _db.Blogs
            .FirstOrDefaultAsync(x => x.BlogId == id);

        if (item is null)
        {
            Console.WriteLine("No data found.");
            return;
        }

        Console.WriteLine(item.BlogId);
        Console.WriteLine(item.BlogTitle);
        Console.WriteLine(item.BlogAuthor);
        Console.WriteLine(item.BlogContent);
    }

    public async void Update(int id, string title, string author, string content)
    {
        var item = await _db.Blogs
            .AsNoTracking() // db data ကို copy ယူပြီး အလုပ်လုပ်တာပါ
            .FirstOrDefaultAsync(x => x.BlogId == id);

        #region With (NOLOCK)
        // .AsNoTracking() က db data ကို copy ယူပြီး အလုပ်လုပ်တာပါ
        // db မှာဆိုရင် With (NOLOCK) နဲတူပါတယ် (ဖတ်ထားရန်)
        // With (NOLOCK) က query data ကို other transactions တွေကိုပြီးအောင် စောင့်နေစရာမလိုပဲ အလုပ်လုပ်တာပါ
        // Performance ကောင်းဖို့နဲ Reporting or Analytics တို့မှာဆိုရင် 100% မှန်နေဖို့မလိုတဲ့နေရာမှာဆို သုံးလို့ကောင်းတယ်
        // မကောင်းတာက Real time data ကိုမရတော့ပါဘူး

        #endregion

        if (item is null)
        {
            Console.WriteLine("No data found.");
            return;
        }

        if (!string.IsNullOrEmpty(title))
        {
            item.BlogTitle = title;
        }

        if (!string.IsNullOrEmpty(author))
        {
            item.BlogAuthor = author;
        }

        if (!string.IsNullOrEmpty(content))
        {
            item.BlogContent = content;
        }

        _db.Entry(item).State = EntityState.Modified; // AsNoTracking လုပ်လို့ထည့်ရတာပါ
        var result = await _db.SaveChangesAsync(); // Execute လုပ်တာပါ

        Console.WriteLine(result == 1 ? "Updating Successful." : "Updating Failed.");
    }

    public async void Delete(int id) // Actual Deleting
    {
        var item = await _db.Blogs
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.BlogId == id);

        if (item is null)
        {
            Console.WriteLine("No data found.");
            return;
        }

        item.DeleteFlag = true;
        //_db.Entry(item).State = EntityState.Deleted; // Actual Deleting
        _db.Entry(item).State = EntityState.Modified; // Soft Deleting
        var result = await _db.SaveChangesAsync();
        Console.WriteLine(result == 1 ? "Deleting Successful." : "Deleting Failed.");
    }

    public async void DeleteFlag(int id) // Not Actual Deleting
    {
        var item = await _db.Blogs
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.BlogId == id & x.DeleteFlag == false);

        if (item is null)
        {
            Console.WriteLine("No data found.");
            return;
        }

        item.DeleteFlag = true;

        _db.Entry(item).State = EntityState.Modified;
        var result = await _db.SaveChangesAsync();
        Console.WriteLine(result == 1 ? "Deleting Successful." : "Deleting Failed.");
    }

}
