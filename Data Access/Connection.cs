using Microsoft.Extensions.Configuration;
using MySqlConnector;
using Newtonsoft.Json;
using Renci.SshNet;
using System.Data;
using System.Text;
using ConnectionInfo = Renci.SshNet.ConnectionInfo;

namespace Data_Access
{
    public class Connection : IDisposable
    {
        private IConfiguration Configuration;

        private MySqlConnection connection;
        private string connectionType;
        private string mysqlHost;
        private string mysqlUsername;
        private string mysqlPassword;
        private string mysqlDatabase;
        private bool disposed = false;

        public Connection(IConfiguration _configuration)
        {
            this.Configuration = _configuration;
            this.mysqlHost = Configuration["Production:ConnectionMySql:mysqlHost"];
            this.mysqlUsername = Configuration["Production:ConnectionMySql:mysqlUsername"];
            this.mysqlPassword = Configuration["Production:ConnectionMySql:mysqlPassword"];
            this.mysqlDatabase = Configuration["Production:ConnectionMySql:mysqlDatabase"];
        }

        public string ExecuteQuery(string query)
        {
            try
            {
                string connectionString = $"Server={mysqlHost};Database={mysqlDatabase};Uid={mysqlUsername};Pwd={mysqlPassword};";
                using MySqlConnection connection = new MySqlConnection(connectionString);

                connection.Open();

                using MySqlCommand command = new MySqlCommand(query, connection);

                using MySqlDataReader reader = command.ExecuteReader();
                var dataTable = new DataTable();
                dataTable.Load(reader);

                string jsonData = JsonConvert.SerializeObject(dataTable);
                return jsonData;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (Convert.ToInt32(connectionType) == 2)
            {
                if (!disposed)
                {
                    if (disposing)
                    {
                        if (connection.State == ConnectionState.Open)
                        {
                            connection.Close();
                        }
                    }
                    disposed = true;
                }
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
