using Microsoft.Data.SqlClient;

namespace mvstwo.Controllers
{
    public class Backup
    {
        private readonly string _connectionString;

        public Backup(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void BackupDatabase(string backupPath)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string backupQuery = $"BACKUP DATABASE [{connection.Database}] TO DISK = '{backupPath}'";
                using (SqlCommand command = new SqlCommand(backupQuery, connection))
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }

        public void RestoreDatabase(string backupPath)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string restoreQuery = $"USE master RESTORE DATABASE [{connection.Database}] FROM DISK = '{backupPath}' WITH REPLACE";
                using (SqlCommand command = new SqlCommand(restoreQuery, connection))
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
