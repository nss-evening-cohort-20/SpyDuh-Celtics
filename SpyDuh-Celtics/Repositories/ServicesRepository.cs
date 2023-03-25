using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using SpyDuh_Celtics.Models;
using SpyDuh_Celtics.Utils;

namespace SpyDuh_Celtics.Repositories
{
    public class ServicesRepository : BaseRepository, IServicesRepository
    {
        /// <summary>
        ///  When new RoomRepository is instantiated, pass the connection string along to the BaseRepository
        /// </summary>
        public ServicesRepository(IConfiguration configuration) : base(configuration) { }

        public void Add(Services service)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        INSERT INTO service (service, userId)
                        OUTPUT INSERTED.ID
                        VALUES (@service, @userId)";

                    DbUtils.AddParameter(cmd, "@service", service.Service);
                    DbUtils.AddParameter(cmd, "@userId", service.UserId);

                    service.Id = (int)cmd.ExecuteScalar();
                }
            }
        }

        public IList<Services> GetAll()
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                          SELECT id, service, userId
                            FROM service";

                    var reader = cmd.ExecuteReader();

                    var services = new List<Services>();
                    while (reader.Read())
                    {
                        services.Add(new Services()
                        {
                            Id = DbUtils.GetInt(reader, "id"),
                            Service = DbUtils.GetString(reader, "service"),
                            UserId = DbUtils.GetInt(reader, "userId")
                        });
                    }

                    reader.Close();

                    return services;
                }
            }
        }

        public Services GetById(int id)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                          SELECT id, service, userId
                            FROM service
                            WHERE id = @Id";

                    DbUtils.AddParameter(cmd, "@Id", id);
                    var reader = cmd.ExecuteReader();

                    Services service = null;
                    if (reader.Read())
                    {
                        service = new Services()
                        {
                            Id = DbUtils.GetInt(reader, "id"),
                            Service = DbUtils.GetString(reader, "service"),
                            UserId = DbUtils.GetInt(reader, "userId")
                        };
                    }

                    reader.Close();

                    return service;
                }
            }
        }

        public void Remove(int id)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "DELETE FROM service WHERE Id = @Id";
                    DbUtils.AddParameter(cmd, "@id", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Update(Services service)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        UPDATE service
                           SET service = @service,
                               userId = @userId
                         WHERE Id = @Id";

                    DbUtils.AddParameter(cmd, "@service", service.Service);
                    DbUtils.AddParameter(cmd, "@userId", service.UserId);
                    DbUtils.AddParameter(cmd, "@Id", service.Id);

                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
