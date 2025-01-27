using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace PSTDotNetTrainingBatch5.Database.Models;

public partial class AppDbContext : DbContext
{
    public AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<TblBlog> TblBlogs { get; set; }

    #region Connection ဖြတ်ရန်လိုမယ်
    // Connection ဖြတ်ရန်လိုမယ် မဟုတ်ရင် အသေဖြစ်နေမှာပါ
    // overrid onconfig လိုအပ်မှ ပြန်ထည့်လို့ရပါတယ်
    #endregion
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TblBlog>(entity =>
        {
            entity.HasKey(e => e.BlogId);

            entity.ToTable("Tbl_Blog");

            entity.Property(e => e.BlogAuthor).HasMaxLength(50);
            entity.Property(e => e.BlogTitle).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
