using System;
using System.Collections.Generic;
using System.Text;

namespace Sharnaka_Dev.Data
{
    public class MarkdownFile
    {
        public int Id { get; set; }
        public ulong OwnerId { get; set; }          // ID Discord (snowflake)
        public string Name { get; set; } = "";      // ex. "notes-session-1.md"
        public string Content { get; set; } = "";   // le texte Markdown
        public int SizeBytes { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
