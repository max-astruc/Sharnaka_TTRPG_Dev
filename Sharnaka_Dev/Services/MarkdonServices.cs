using Microsoft.EntityFrameworkCore;
using Sharnaka_Dev.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sharnaka_Dev.Services
{
    internal class MarkdownService
    {
        private const int MaxBytes = 256 * 1024;   // à ajuster
        private const int MaxFilesPerUser = 50;    // à ajuster
        private readonly IDbContextFactory<BotContext> _factory;

        public MarkdownService(IDbContextFactory<BotContext> factory) => _factory = factory;

        public async Task<(bool ok, string message)> SaveAsync(ulong ownerId, string name, string content)
        {
            name = Path.GetFileName(name).Trim();
            if (!name.EndsWith(".md", StringComparison.OrdinalIgnoreCase))
                return (false, "Seuls les fichiers .md sont acceptés.");

            var size = System.Text.Encoding.UTF8.GetByteCount(content);
            if (size > MaxBytes)
                return (false, $"Fichier trop gros (max {MaxBytes / 1024} Ko).");

            await using var db = await _factory.CreateDbContextAsync();
            var file = await db.Files.FirstOrDefaultAsync(f => f.OwnerId == ownerId && f.Name == name);

            if (file is null)
            {
                if (await db.Files.CountAsync(f => f.OwnerId == ownerId) >= MaxFilesPerUser)
                    return (false, "Limite de fichiers atteinte.");

                file = new MarkdownFile { OwnerId = ownerId, Name = name, CreatedAt = DateTime.UtcNow };
                db.Files.Add(file);
            }

            file.Content = content;
            file.SizeBytes = size;
            file.UpdatedAt = DateTime.UtcNow;

            await db.SaveChangesAsync();
            return (true, "Fichier enregistré.");
        }

        public async Task<List<MarkdownFile>> ListAsync(ulong ownerId)
        {
            await using var db = await _factory.CreateDbContextAsync();
            return await db.Files
                .Where(f => f.OwnerId == ownerId)
                .OrderBy(f => f.Name)
                .Select(f => new MarkdownFile { Name = f.Name, SizeBytes = f.SizeBytes, UpdatedAt = f.UpdatedAt })
                .ToListAsync();
        }

        public async Task<MarkdownFile?> GetAsync(ulong ownerId, string name)
        {
            await using var db = await _factory.CreateDbContextAsync();
            return await db.Files.FirstOrDefaultAsync(f => f.OwnerId == ownerId && f.Name == name);
        }

        public async Task<bool> DeleteAsync(ulong ownerId, string name)
        {
            await using var db = await _factory.CreateDbContextAsync();
            return await db.Files.Where(f => f.OwnerId == ownerId && f.Name == name)
                                 .ExecuteDeleteAsync() > 0;
        }
    }
}
