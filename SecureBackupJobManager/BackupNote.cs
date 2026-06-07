using System;

namespace SecureBackupJobManager
{
    public class BackupNote
    { 
        public string NoteText { get; set; }

        public DateTime DateAdded { get; set; }

        public BackupNote()
        { 
        }

        public BackupNote(string noteText)
        { 
            NoteText = noteText;
            DateAdded = DateTime.Now;
        
        }
    }
}