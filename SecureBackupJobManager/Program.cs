/* Jeff O'Hara
 * 6/7/2026
 * 
 * Helps organizations identify important systems, create and track backup jobs, monitor backup success or failure, 
 * and document backup-related notes and activities. It provides dashboard reporting, encryption tracking, 
 * backup scheduling, and JSON data persistence to support data protection and disaster recovery planning.
 */

using System;

namespace SecureBackupJobManager
{
    class Program
    {
        static void Main(string[] args)
        {
            BackupManager manager = new BackupManager();

            bool running = true;

            while (running)
            {
                Console.WriteLine("\n==========================================");
                Console.WriteLine("        SECURE BACKUP JOB MANAGER");
                Console.WriteLine("==========================================");
                Console.WriteLine("Track systems, backup jobs, encryption,");
                Console.WriteLine("backup status, notes, and recovery readiness.");
                Console.WriteLine();
                Console.WriteLine("1. Add system");
                Console.WriteLine("2. View systems");
                Console.WriteLine("3. Create backup job");
                Console.WriteLine("4. View all backup jobs");
                Console.WriteLine("5. Update backup run status");
                Console.WriteLine("6. Add backup note");
                Console.WriteLine("7. View backup notes");
                Console.WriteLine("8. View backup dashboard");
                Console.WriteLine("9. Exit");
                Console.Write("\nChoose an option 1 through 9: ");

                string choice = Console.ReadLine();

                if (choice == "1")
                {
                    manager.AddSystem();
                }
                else if (choice == "2")
                {
                    manager.ViewSystems();
                }
                else if (choice == "3")
                {
                    manager.CreateBackupJob();
                }
                else if (choice == "4")
                {
                    manager.ViewAllBackupJobs();
                }
                else if (choice == "5")
                {
                    manager.UpdateBackupRunStatus();
                }
                else if (choice == "6")
                {
                    manager.AddBackupNote();
                }
                else if (choice == "7")
                {
                    manager.ViewBackupNotes();
                }
                else if (choice == "8")
                {
                    manager.ViewBackupDashboard();
                }
                else if (choice == "9")
                {
                    running = false;
                    Console.WriteLine("Exiting Secure Backup Job Manager.");
                }
                else
                {
                    Console.WriteLine("Invalid option. Please choose 1 through 9.");
                }
            }
        }
    }
}