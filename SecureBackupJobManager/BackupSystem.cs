namespace SecureBackupJobManager
{
    public class BackupSystem
    { 
        public int SystemId { get; set; }

        public string SystemName { get; set; }

        public string SystemType { get; set; }

        public string OwnerDepartment { get; set; }

        public BackupSystem() 
        {
        }

        public BackupSystem(int systemId, string systemName, string systemType, string ownerDepartment)
        { 
            SystemId = systemId;
            SystemName = systemName;
            SystemType = systemType;
            OwnerDepartment = ownerDepartment;
        }
    }
}