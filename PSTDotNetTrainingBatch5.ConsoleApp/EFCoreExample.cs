using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using PSTDotNetTrainingBatch5.ConsoleApp.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace PSTDotNetTrainingBatch5.ConsoleApp
{
    public class EFCoreExample
    {
        public void Read()
        {
            AppDbContext db = new AppDbContext();
            // Tolist နောက်မှရေးရင် DB ကဆွဲထုတ်ပြီးတော့မှ ပြန် filter ဖြစ်သွားမှာပါ။
            var lst = db.Blogs.Where(x=> x.DeleteFlag == false) .ToList();
            foreach (var item in lst)
            {
                Console.WriteLine(item.BlogId);
                Console.WriteLine(item.BlogTitle);
                Console.WriteLine(item.BlogAuthor);
                Console.WriteLine(item.BlogContent);
            }
        }

        public void Create(string title, string author, string content)
        {
            BlogDataModel blog = new BlogDataModel
            {
                BlogTitle = title,
                BlogAuthor = author,
                BlogContent = content,
            };

            AppDbContext db = new AppDbContext();
            db.Blogs.Add(blog); // Query အနေနဲအလုပ်လုပ်တာပါ
            var result = db.SaveChanges(); // Execute လုပ်တာပါ

            Console.WriteLine(result == 1 ? "Saving Successful." : "Saving Failed.");
        }

        public void Edit(int id)
        {
            AppDbContext db = new AppDbContext();
            //db.Blogs.Where(x => x.BlogId == id).FirstOrDefault();
            var item = db.Blogs.FirstOrDefault(x => x.BlogId == id);

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

        public void Update(int id, string title, string author, string content)
        {
            AppDbContext db = new AppDbContext();
            var item = db.Blogs
                .AsNoTracking() // db data ကို copy ယူပြီး အလုပ်လုပ်တာပါ
                .FirstOrDefault(x => x.BlogId == id);

            // db မှာဆိုရင် with (nolock) နဲတူပါတယ် (ဖတ်ထားရန်)

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

            db.Entry(item).State = EntityState.Modified; // AsNoTracking လုပ်လို့ထည့်ရတာပါ
            var result = db.SaveChanges(); // Execute လုပ်တာပါ

            Console.WriteLine(result == 1 ? "Updating Successful." : "Updating Failed.");
        }

        public void Delete(int id) // Actual Deleting
        {
            AppDbContext db = new AppDbContext();
            var item = db.Blogs
                .AsNoTracking() 
                .FirstOrDefault(x => x.BlogId == id);

            if (item is null)
            {
                Console.WriteLine("No data found.");
                return;
            }

            db.Entry(item).State = EntityState.Deleted; 
            var result = db.SaveChanges(); 
            Console.WriteLine(result == 1 ? "Deleting Successful." : "Deleting Failed.");
        }

        public void DeleteFlag(int id) // Not Actual Deleting
        {
            AppDbContext db = new AppDbContext();
            var item = db.Blogs
                .AsNoTracking()
                .FirstOrDefault(x => x.BlogId == id & x.DeleteFlag == false);

            if (item is null)
            {
                Console.WriteLine("No data found.");
                return;
            }

            item.DeleteFlag = true;

            db.Entry(item).State = EntityState.Modified;
            var result = db.SaveChanges();
            Console.WriteLine(result == 1 ? "Deleting Successful." : "Deleting Failed.");
        }

    }
}
