using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace SecureBackupJobManager
{
    public class BackupManager
    { 
        private List<BackupSystem> systems = new List<BackupSystem>();
        private List<BackupJob> jobs = new List<BackupJob>();

        private int nextSystemId = 1001;
        private int nextJobId = 5001;

        private string systemFilePath = "backupSystems.json";
        private string jobFilePath = "backupJobs.json";

        public BackupManager()
        {
            LoadDataFromFiles();
        }

        // UPDATED 06/07/2026
        // Improved user guidance so non-technical users understand
        // what a system is and why it should be added.
        public void AddSystem()
        {
            Console.WriteLine("\n======================================");
            Console.WriteLine("             ADD SYSTEM");
            Console.WriteLine("======================================");
            Console.WriteLine("A system is anything important to the");
            Console.WriteLine("organization that stores, processes,");
            Console.WriteLine("or manages business information.");
            Console.WriteLine();
            Console.WriteLine("Systems are added so backup jobs can");
            Console.WriteLine("later be created to protect important");
            Console.WriteLine("business data.");
            Console.WriteLine();
            Console.WriteLine("Examples of systems include:");
            Console.WriteLine("- Payroll Server");
            Console.WriteLine("- Customer Database");
            Console.WriteLine("- Company Website");
            Console.WriteLine("- Shared File Storage");
            Console.WriteLine("- Human Resources Application");
            Console.WriteLine("- Accounting Software");
            Console.WriteLine();
            Console.WriteLine("Ask yourself: If this system stopped working");
            Console.WriteLine("or its information disappeared tomorrow,");
            Console.WriteLine("would it affect the organization's ability");
            Console.WriteLine("to operate normally?");
            Console.WriteLine();
            Console.WriteLine("Enter 0 at any prompt to cancel without saving.");
            Console.WriteLine();

            Console.WriteLine();
            Console.WriteLine("Examples of systems:");
            Console.WriteLine("- Employee Payroll");
            Console.WriteLine("- Customer Records");
            Console.WriteLine("- Company Website");
            Console.WriteLine("- Shared Documents");
            Console.WriteLine("- Employee Time Tracking");
            Console.WriteLine("- Accounting Records");
            Console.WriteLine();

            string systemName = GetTextOrCancel(
                "What important system, information, or business process would you like to protect with backups? ");

            if (systemName == "0")
            {
                Console.WriteLine("Add system cancelled.");
                return;
            }

            string systemType = GetSystemTypeFromUser();

            if (systemType == "Cancel")
            {
                Console.WriteLine("Add system cancelled.");
                return;
            }

            string ownerDepartment = GetTextOrCancel(
                "Department responsible for this system, such as IT, Finance, HR, or Operations: ");

            if (ownerDepartment == "0")
            {
                Console.WriteLine("Add system cancelled.");
                return;
            }

            BackupSystem system = new BackupSystem(
                nextSystemId,
                systemName,
                systemType,
                ownerDepartment
            );

            systems.Add(system);

            SaveDataToFiles();

            Console.WriteLine("\nSystem added successfully.");
            Console.WriteLine($"System ID: {nextSystemId}");
            Console.WriteLine($"System Name: {systemName}");
            Console.WriteLine($"System Type: {systemType}");

            nextSystemId++;
        }

        public void ViewSystems()
        {
            Console.WriteLine("\n======================================");
            Console.WriteLine("             SYSTEM LIST");
            Console.WriteLine("======================================");

            if (systems.Count == 0)
            {
                Console.WriteLine("No systems have been added yet.");
                return;
            }

            foreach (BackupSystem system in systems)
            {
                Console.WriteLine($"\nSystem ID: {system.SystemId}");
                Console.WriteLine($"Name: {system.SystemName}");
                Console.WriteLine($"Type: {system.SystemType}");
                Console.WriteLine($"Owner Department: {system.OwnerDepartment}");
            }
        }

        // UPDATED 06/07/2026
        // Expanded user guidance so users understand
        // what a backup job is and why they are creating one.
        public void CreateBackupJob()
        {
            Console.WriteLine("\n======================================");
            Console.WriteLine("          CREATE BACKUP JOB");
            Console.WriteLine("======================================");
            Console.WriteLine("A backup job is a scheduled plan used");
            Console.WriteLine("to create copies of important business");
            Console.WriteLine("data in case something goes wrong.");
            Console.WriteLine();
            Console.WriteLine("Backups help recover information after:");
            Console.WriteLine("- Hardware failures");
            Console.WriteLine("- Accidental deletion");
            Console.WriteLine("- Software problems");
            Console.WriteLine("- Ransomware attacks");
            Console.WriteLine("- Natural disasters");
            Console.WriteLine();
            Console.WriteLine("In this section you will:");
            Console.WriteLine("1. Select the system being protected");
            Console.WriteLine("2. Select the backup type");
            Console.WriteLine("3. Select how often backups run");
            Console.WriteLine("4. Decide if backup data is encrypted");
            Console.WriteLine();
            Console.WriteLine("Enter 0 at any prompt to cancel without saving.");
            Console.WriteLine();

            if (systems.Count == 0)
            {
                Console.WriteLine("You must add a system before creating a backup job.");
                return;
            }

            DisplaySystemSummary();

            int systemId = GetIdOrCancel(
                "\nEnter the System ID you want to protect, or 0 to cancel: ");

            if (systemId == 0)
            {
                Console.WriteLine("Create backup job cancelled.");
                return;
            }

            BackupSystem selectedSystem = systems.Find(
                system => system.SystemId == systemId);

            if (selectedSystem == null)
            {
                Console.WriteLine("No system with that ID was found.");
                return;
            }

            Console.WriteLine();
            Console.WriteLine($"Selected System: {selectedSystem.SystemName}");
            Console.WriteLine();

            string backupType = GetBackupTypeFromUser();

            if (backupType == "Cancel")
            {
                Console.WriteLine("Create backup job cancelled.");
                return;
            }

            string schedule = GetBackupScheduleFromUser();

            if (schedule == "Cancel")
            {
                Console.WriteLine("Create backup job cancelled.");
                return;
            }

            bool isEncrypted = GetEncryptionChoiceFromUser();

            BackupJob job = new BackupJob(
                nextJobId,
                systemId,
                backupType,
                schedule,
                isEncrypted
            );

            jobs.Add(job);

            SaveDataToFiles();

            Console.WriteLine("\nBackup job created successfully.");
            Console.WriteLine($"Job ID: {nextJobId}");
            Console.WriteLine($"Protected System: {selectedSystem.SystemName}");
            Console.WriteLine($"Backup Type: {backupType}");
            Console.WriteLine($"Backup Schedule: {schedule}");
            Console.WriteLine($"Encryption Enabled: {(isEncrypted ? "Yes" : "No")}");

            nextJobId++;
        }

        public void ViewAllBackupJobs()
        {
            Console.WriteLine("\n======================================");
            Console.WriteLine("           ALL BACKUP JOBS");
            Console.WriteLine("======================================");

            if (jobs.Count == 0)
            {
                Console.WriteLine("No backup jobs have been created yet.");
                return;
            }

            foreach (BackupJob job in jobs)
            {
                DisplayBackupJob(job);
            }
        }

        public void UpdateBackupRunStatus()
        {
            Console.WriteLine("\n======================================");
            Console.WriteLine("       UPDATE BACKUP RUN STATUS");
            Console.WriteLine("======================================");
            Console.WriteLine("Use this after checking whether a backup");
            Console.WriteLine("completed successfully, failed, or needs review.");
            Console.WriteLine();
            Console.WriteLine("Enter 0 at any prompt to cancel without saving.");
            Console.WriteLine();

            if (jobs.Count == 0)
            {
                Console.WriteLine("No backup jobs are available.");
                return;
            }

            DisplayJobSummary();

            int jobId = GetIdOrCancel("\nEnter Job ID to update, or 0 to cancel: ");

            if (jobId == 0)
            {
                Console.WriteLine("Update cancelled.");
                return;
            }

            BackupJob job = jobs.Find(item => item.JobId == jobId);

            if (job == null)
            {
                Console.WriteLine("No backup job with that ID was found.");
                return;
            }

            string status = GetRunStatusFromUser();

            if (status == "Cancel")
            {
                Console.WriteLine("Update cancelled.");
                return;
            }

            job.LastRunStatus = status;
            job.LastRunDate = DateTime.Now;

            SaveDataToFiles();

            Console.WriteLine($"Backup job {job.JobId} updated to {job.LastRunStatus}.");
        }

        public void AddBackupNote()
        {
            Console.WriteLine("\n======================================");
            Console.WriteLine("            ADD BACKUP NOTE");
            Console.WriteLine("======================================");
            Console.WriteLine("Use notes to document backup failures,");
            Console.WriteLine("restore testing, storage concerns, or");
            Console.WriteLine("follow-up actions.");
            Console.WriteLine();
            Console.WriteLine("Enter 0 at any prompt to cancel without saving.");
            Console.WriteLine();

            if (jobs.Count == 0)
            {
                Console.WriteLine("No backup jobs are available.");
                return;
            }

            DisplayJobSummary();

            int jobId = GetIdOrCancel("\nEnter Job ID for this note, or 0 to cancel: ");

            if (jobId == 0)
            {
                Console.WriteLine("Add note cancelled.");
                return;
            }

            BackupJob job = jobs.Find(item => item.JobId == jobId);

            if (job == null)
            {
                Console.WriteLine("No backup job with that ID was found.");
                return;
            }

            if (job.Notes == null)
            {
                job.Notes = new List<BackupNote>();
            }

            string noteText = GetTextOrCancel("Note text, or 0 to cancel: ");

            if (noteText == "0")
            {
                Console.WriteLine("Add note cancelled.");
                return;
            }

            job.AddNote(new BackupNote(noteText));

            SaveDataToFiles();

            Console.WriteLine("Backup note added successfully.");
        }

        public void ViewBackupNotes()
        {
            Console.WriteLine("\n======================================");
            Console.WriteLine("            BACKUP NOTES");
            Console.WriteLine("======================================");

            if (jobs.Count == 0)
            {
                Console.WriteLine("No backup jobs are available.");
                return;
            }

            DisplayJobSummary();

            int jobId = GetIdOrCancel("\nEnter Job ID to view notes, or 0 to cancel: ");

            if (jobId == 0)
            {
                Console.WriteLine("View notes cancelled.");
                return;
            }

            BackupJob job = jobs.Find(item => item.JobId == jobId);

            if (job == null)
            {
                Console.WriteLine("No backup job with that ID was found.");
                return;
            }

            if (job.Notes == null || job.Notes.Count == 0)
            {
                Console.WriteLine("No notes have been added for this backup job.");
                return;
            }

            foreach (BackupNote note in job.Notes)
            {
                Console.WriteLine($"\n[{note.DateAdded}]");
                Console.WriteLine(note.NoteText);
            }
        }

        public void ViewBackupDashboard()
        {
            Console.WriteLine("\n======================================");
            Console.WriteLine("           BACKUP DASHBOARD");
            Console.WriteLine("======================================");

            int successful = 0;
            int failed = 0;
            int warning = 0;
            int notRun = 0;
            int encrypted = 0;
            int notEncrypted = 0;

            foreach (BackupJob job in jobs)
            {
                if (job.LastRunStatus == "Successful") successful++;
                else if (job.LastRunStatus == "Failed") failed++;
                else if (job.LastRunStatus == "Warning") warning++;
                else if (job.LastRunStatus == "Not Run Yet") notRun++;

                if (job.IsEncrypted) encrypted++;
                else notEncrypted++;
            }

            Console.WriteLine($"Systems in System: {systems.Count}");
            Console.WriteLine($"Total Backup Jobs: {jobs.Count}");
            Console.WriteLine();
            Console.WriteLine("--- Last Run Status ---");
            Console.WriteLine($"Successful: {successful}");
            Console.WriteLine($"Warning: {warning}");
            Console.WriteLine($"Failed: {failed}");
            Console.WriteLine($"Not Run Yet: {notRun}");
            Console.WriteLine();
            Console.WriteLine("--- Encryption Status ---");
            Console.WriteLine($"Encrypted Jobs: {encrypted}");
            Console.WriteLine($"Not Encrypted Jobs: {notEncrypted}");
        }

        private string GetSystemTypeFromUser()
        {
            while (true)
            {
                Console.WriteLine("\nChoose system type:");
                Console.WriteLine("1. Server - physical or virtual machine that runs services");
                Console.WriteLine("2. Database - stores business or application data");
                Console.WriteLine("3. Web Application - website or online application");
                Console.WriteLine("4. File Share - shared folders or document storage");
                Console.WriteLine("5. Cloud Service - cloud-hosted system or platform");
                Console.WriteLine("0. Cancel and return to main menu");
                Console.Write("Choose option 0 through 5: ");

                string choice = Console.ReadLine();

                if (choice == "1") return "Server";
                if (choice == "2") return "Database";
                if (choice == "3") return "Web Application";
                if (choice == "4") return "File Share";
                if (choice == "5") return "Cloud Service";
                if (choice == "0") return "Cancel";

                Console.WriteLine("Invalid option. Please choose 0 through 5.");
            }
        }

        private string GetBackupTypeFromUser()
        {
            while (true)
            {
                Console.WriteLine("\nChoose backup type:");
                Console.WriteLine("1. Full Backup - copies all selected data");
                Console.WriteLine("2. Incremental Backup - copies only changes since the last backup");
                Console.WriteLine("3. Differential Backup - copies changes since the last full backup");
                Console.WriteLine("4. Snapshot - captures system state at a point in time");
                Console.WriteLine("0. Cancel and return to main menu");
                Console.Write("Choose option 0 through 4: ");

                string choice = Console.ReadLine();

                if (choice == "1") return "Full Backup";
                if (choice == "2") return "Incremental Backup";
                if (choice == "3") return "Differential Backup";
                if (choice == "4") return "Snapshot";
                if (choice == "0") return "Cancel";

                Console.WriteLine("Invalid option. Please choose 0 through 4.");
            }
        }

        private string GetBackupScheduleFromUser()
        {
            while (true)
            {
                Console.WriteLine("\nChoose backup schedule:");
                Console.WriteLine("1. Daily");
                Console.WriteLine("2. Weekly");
                Console.WriteLine("3. Monthly");
                Console.WriteLine("4. Manual / On Demand");
                Console.WriteLine("0. Cancel and return to main menu");
                Console.Write("Choose option 0 through 4: ");

                string choice = Console.ReadLine();

                if (choice == "1") return "Daily";
                if (choice == "2") return "Weekly";
                if (choice == "3") return "Monthly";
                if (choice == "4") return "Manual / On Demand";
                if (choice == "0") return "Cancel";

                Console.WriteLine("Invalid option. Please choose 0 through 4.");
            }
        }

        private bool GetEncryptionChoiceFromUser()
        {
            while (true)
            {
                Console.WriteLine("\nShould this backup be encrypted?");
                Console.WriteLine("Encryption helps protect backed-up data if storage is lost, stolen, or accessed improperly.");
                Console.WriteLine("1. Yes, backup should be encrypted");
                Console.WriteLine("2. No, backup is not encrypted");
                Console.Write("Choose option 1 or 2: ");

                string choice = Console.ReadLine();

                if (choice == "1") return true;
                if (choice == "2") return false;

                Console.WriteLine("Invalid option. Please choose 1 or 2.");
            }
        }

        private string GetRunStatusFromUser()
        {
            while (true)
            {
                Console.WriteLine("\nChoose last run status:");
                Console.WriteLine("1. Successful - backup completed without known issues");
                Console.WriteLine("2. Warning - backup completed but needs review");
                Console.WriteLine("3. Failed - backup did not complete successfully");
                Console.WriteLine("0. Cancel and return to main menu");
                Console.Write("Choose option 0 through 3: ");

                string choice = Console.ReadLine();

                if (choice == "1") return "Successful";
                if (choice == "2") return "Warning";
                if (choice == "3") return "Failed";
                if (choice == "0") return "Cancel";

                Console.WriteLine("Invalid option. Please choose 0 through 3.");
            }
        }

        private string GetTextOrCancel(string prompt)
        {
            Console.Write(prompt);

            return Console.ReadLine().Trim();
        }

        private int GetIdOrCancel(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);

                string input = Console.ReadLine().Trim();

                bool isValidNumber = int.TryParse(input, out int id);

                if (isValidNumber)
                {
                    return id;
                }

                Console.WriteLine("Invalid input. Enter a number, or 0 to cancel.");
            }
        }

        private void DisplaySystemSummary()
        {
            Console.WriteLine("\n--- Current Systems ---");

            foreach (BackupSystem system in systems)
            {
                Console.WriteLine($"ID: {system.SystemId} | {system.SystemName} | Type: {system.SystemType}");
            }
        }

        private void DisplayJobSummary()
        {
            Console.WriteLine("\n--- Current Backup Jobs ---");

            foreach (BackupJob job in jobs)
            {
                Console.WriteLine($"Job ID: {job.JobId} | System ID: {job.SystemId} | Type: {job.BackupType} | Status: {job.LastRunStatus}");
            }
        }

        private void DisplayBackupJob(BackupJob job)
        {
            BackupSystem system = systems.Find(item => item.SystemId == job.SystemId);

            string systemName = system == null ? "Unknown System" : system.SystemName;

            int noteCount = job.Notes == null ? 0 : job.Notes.Count;

            Console.WriteLine("\n------------------------------");
            Console.WriteLine($"Job ID: {job.JobId}");
            Console.WriteLine($"System: {systemName}");
            Console.WriteLine($"Backup Type: {job.BackupType}");
            Console.WriteLine($"Schedule: {job.Schedule}");
            Console.WriteLine($"Encrypted: {(job.IsEncrypted ? "Yes" : "No")}");
            Console.WriteLine($"Last Run Status: {job.LastRunStatus}");
            Console.WriteLine($"Date Created: {job.DateCreated}");
            Console.WriteLine($"Notes: {noteCount}");

            if (job.LastRunDate.HasValue)
            {
                Console.WriteLine($"Last Run Date: {job.LastRunDate.Value}");
            }
        }

        private void SaveDataToFiles()
        {
            JsonSerializerOptions options = new JsonSerializerOptions
            {
                WriteIndented = true
            };

            string systemJson = JsonSerializer.Serialize(systems, options);
            string jobJson = JsonSerializer.Serialize(jobs, options);

            File.WriteAllText(systemFilePath, systemJson);
            File.WriteAllText(jobFilePath, jobJson);
        }

        private void LoadDataFromFiles()
        {
            if (File.Exists(systemFilePath))
            {
                string systemJson = File.ReadAllText(systemFilePath);

                if (!string.IsNullOrWhiteSpace(systemJson))
                {
                    systems = JsonSerializer.Deserialize<List<BackupSystem>>(systemJson);

                    if (systems == null)
                    {
                        systems = new List<BackupSystem>();
                    }
                }
            }

            if (File.Exists(jobFilePath))
            {
                string jobJson = File.ReadAllText(jobFilePath);

                if (!string.IsNullOrWhiteSpace(jobJson))
                {
                    jobs = JsonSerializer.Deserialize<List<BackupJob>>(jobJson);

                    if (jobs == null)
                    {
                        jobs = new List<BackupJob>();
                    }
                }
            }

            foreach (BackupSystem system in systems)
            {
                if (system.SystemId >= nextSystemId)
                {
                    nextSystemId = system.SystemId + 1;
                }
            }

            foreach (BackupJob job in jobs)
            {
                if (job.JobId >= nextJobId)
                {
                    nextJobId = job.JobId + 1;
                }

                if (job.Notes == null)
                {
                    job.Notes = new List<BackupNote>();
                }
            }
        }
    }
}