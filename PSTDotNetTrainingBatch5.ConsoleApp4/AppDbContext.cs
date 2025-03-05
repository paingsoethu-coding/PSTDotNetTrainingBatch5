using Microsoft.EntityFrameworkCore;
using PSTDotNetTrainingBatch5.ConsoleApp4.Models;

namespace PSTDotNetTrainingBatch5.ConsoleApp4
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                string connectionString = "Data Source= MSI\\SQLEXPRESS2022; Initial Catalog=DotNetTrainingBatch5; User ID=sa; Password=sasa; TrustServerCertificate=True;";
                optionsBuilder.UseSqlServer(connectionString);
            }
        }

        public DbSet<BlogDataModel> Blogs { get; set; }

        #region CRUD ရေးဖို့အတွက် Configure ချပေးခြင်း

        // ၁။ override onconf ကို ‌ေခေါ်ရေးရမယ်။ optionsBuilder ရလာမယ်။
        // ၂။ သူရေးပေးထားတာကို ဘာလို့မသုံးတာလဲဆိုတော့ ကိုယ် connectionနဲ ကိုသုံးလို့ရအောင်ပါ။
        // ၃။ optionsBuilder က IsConfigured မဖြစ်ဘူးဆိုရင်ဆိုပြီး စစ်မယ်။
        // ၄။ optionsBuilder ကို သုံးတဲ့ Db ပေါ်မူတည်ပြီ UseSqlServer ခေါ်ရမယ်။
        // connectionStringထည့်ပေးရမယ်။ TrustServerCertificate ကို True ပေးခဲ့ရမယ်။
        // ၅။ ိDb ကရှိတယ် Table နဲ C# က BlogDataModel နဲ mapping လုပ်တဲ့ကိစ္စကို ရေးပေးရမယ်။
        //      ၅.၁။ Columns တွေက ညီခဲ့ရင် ပြသာနာမရှိပေမယ် မညီနေခဲ့ဘူးဆိုရင် Column ဘာနဲတူတယ်ဆိုပြီ ကြော်ငြာပေးရမယ်။
        //      ၅.၂။ Primary Key ကို ရှိရင် တစ်ခါါတည်း ရေးပေးရမယ်။ DataAnnotations နစ်ခုကိုလည်းထည့်ပေးရမယ်။

        // *** CRUD ရေးဖို့အတွက် configure ချပေးရတာပါ။ 

        #endregion
    }
}
