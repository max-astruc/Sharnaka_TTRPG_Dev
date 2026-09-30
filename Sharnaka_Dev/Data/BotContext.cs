using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sharnaka_Dev.Data
{
    public class BotContext : DbContext
    {
        public BotContext(DbContextOptions<BotContext> options) : base(options) { }
        public DbSet<MarkdownFile> Files => Set<MarkdownFile>();

        protected override void OnModelCreating(ModelBuilder mb)
        {
            var e = mb.Entity<MarkdownFile>();
            e.Property(f => f.OwnerId).HasConversion<long>();
            e.Property(f => f.Name).HasMaxLength(100).IsRequired();

            // Un utilisateur ne peut pas avoir deux fichiers du même nom
            e.HasIndex(f => new { f.OwnerId, f.Name }).IsUnique();
        }
    }
}