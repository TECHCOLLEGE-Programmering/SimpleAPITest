using DomainModel;
using System.Data.SqlClient;

namespace WebApplication1
{
    public class DatabaseContext
    {
        public DatabaseContext()
        {
            SqlConnectionStringBuilder stringBuilder = new SqlConnectionStringBuilder();
            stringBuilder.Clear();
            stringBuilder.DataSource = "TC190915";
            stringBuilder.InitialCatalog = "SQL Intro";
            stringBuilder.IntegratedSecurity = true;
            connectionString = stringBuilder.ConnectionString;
            conn = new SqlConnection(connectionString);
        }
        private static string connectionString;
        private readonly SqlConnection conn;

        public async Task<IEnumerable<User>> GetUsers()
        {
            string sql = "SELECT ID, Name, Email FROM Users";
            List<User> users = new List<User>();
            conn.Open();
            using (conn)
            {
                SqlCommand command = new SqlCommand(sql, conn);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (await reader.ReadAsync())
                    {
                        Console.WriteLine(String.Format("{0}, {1}, {2}", reader[0], reader[1], reader[2]));
                        users.Add(new User { Id = Convert.ToInt32(reader[0]), Name = reader[1].ToString(), Email = reader[2].ToString() });
                    }
                }
            }
            return users;
        }
    }
}
