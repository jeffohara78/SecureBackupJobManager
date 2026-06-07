using System;
using System.Collections.Generic;

namespace SecureBackupJobManager
{
    public class BackupJob
    { 
        public int JobId { get; set; }

        public int SystemId { get; set; }

        public string BackupType { get; set; }

        public string Schedule {  get; set; }

        public bool IsEncrypted { get; set; }

        public string LastRunStatus { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime? LastRunDate { get; set; }

        public List<BackupNote> Notes { get; set; }

        public BackupJob()
        { 
            Notes = new List<BackupNote>();
        }

        public BackupJob(int jobId, int sytemId, string backupType, string schedule, bool isEncrypted)
        { 
            JobId = jobId;
            SystemId = sytemId;
            BackupType = backupType;    
            Schedule = schedule;
            IsEncrypted = isEncrypted;
            LastRunStatus = "Not Run Yet";
            DateCreated = DateTime.Now;
            LastRunDate = null;
            Notes = new List<BackupNote>();
        }

        public void AddNote(BackupNote note)
        {
            Notes.Add(note);
        }
    }
}