using LearningSystem.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningSystem.Data.Configuration
{
    public class ArticleConfig : IEntityTypeConfiguration<Article>
    {
        public void Configure(EntityTypeBuilder<Article> entity)
        {
            entity.HasKey(a => a.Id);

            entity.HasOne(a => a.Author)
                .WithMany(ar => ar.Articles)
                .HasForeignKey(a => a.AuthorId);

            entity.HasMany(c => c.Comments)
                .WithOne(a => a.Article)
                .HasForeignKey(a => a.ArticleId);
        }
    }
}