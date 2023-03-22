using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using SpyDuh_Celtics.Models;

namespace SpyDuh_Celtics.Repository
{
    public class UserRepository : BaseRepository, IUserRepository
    {
        private readonly string _connectionString;
        public UserRepository(IConfiguration configuration) : base(configuration) { }
         /*{
             _connectionString = configuration.GetConnectionString("DefaultConnection");
         }

    private SqlConnection Connection
    {
        get { return new SqlConnection(_connectionString); }
    }*/

    /*Gets All the Users Data*/
    public List<User> GetAll()
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "SELECT id, name, location FROM [user];";
                    using (var reader = cmd.ExecuteReader())
                    {
                        var users = new List<User>();
                        while (reader.Read())
                        {
                            var user = new User()
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                Name = reader.GetString(reader.GetOrdinal("Name")),
                                /*Location = reader.GetString(reader.GetOrdinal("Location")),*/
                            };
                            if (!reader.IsDBNull(reader.GetOrdinal("Location")))
                            {
                                user.Location = reader.GetString(reader.GetOrdinal("Location"));
                            }
                            users.Add(user);
                        }

                        reader.Close();

                        return users;
                    }
                }
            }
        }

        /*Get the User by ID*/

        public User GetById(int id)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT id, name, location
                                        FROM [user]
                                        WHERE id = @id;";
                    cmd.Parameters.AddWithValue("@id", id);

                    using (var reader = cmd.ExecuteReader())
                    {
                        User user = null;
                        if (reader.Read())
                        {
                            user = new User()
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                Name = reader.GetString(reader.GetOrdinal("Name")),
                                /*Location = reader.GetString(reader.GetOrdinal("Location")),*/
                            };
                            if (!reader.IsDBNull(reader.GetOrdinal("Location")))
                            {
                                user.Location = reader.GetString(reader.GetOrdinal("Location"));
                            }
                        }

                        reader.Close();

                        return user;
                    }

                }
            }
        }

        /*Get User by Name*/

        public User GetName(string Name)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT id, name, location
                                        FROM [user]
                                        WHERE name = @name;";
                    cmd.Parameters.AddWithValue("@name", Name);

                    var reader = cmd.ExecuteReader();

                    User users = null;
                    if (reader.Read())
                    {
                        users = new User()
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            Name = reader.GetString(reader.GetOrdinal("Name")),
                            /*Location = reader.GetString(reader.GetOrdinal("Location")),*/
                        };
                        if (!reader.IsDBNull(reader.GetOrdinal("Location")))
                        {
                            users.Location = reader.GetString(reader.GetOrdinal("Location"));
                        }
                    }

                    reader.Close();

                    return users;
                }
            }
        }

        /*Adds New User*/

        public User Add(User user)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"INSERT INTO [user] ([name], location)
                                        OUTPUT INSERTED.id
                                        VALUES (@name, @location);";
                    cmd.Parameters.AddWithValue("@Name", user.Name);
                    if (user.Location == null)
                    {
                        cmd.Parameters.AddWithValue("@Location", DBNull.Value);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@Location", user.Location);
                    }

                    user.Id = (int)cmd.ExecuteScalar();
                    return user;
                }
            }
        }

        /*Updates User Information*/

        public void Update(User user)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"UPDATE [user] 
                                        SET [name] = @name,
	                                        location = @location
                                        WHERE id = @id";
                    cmd.Parameters.AddWithValue("@id", user.Id);
                    cmd.Parameters.AddWithValue("@name", user.Name);
                    if (user.Location == null)
                    {
                        cmd.Parameters.AddWithValue("@location", DBNull.Value);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@location", user.Location);
                    }

                    cmd.ExecuteNonQuery();
                }
            }
        }

        /*Deletes user*/

        public void Delete(int id)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "DELETE FROM [user] WHERE Id = @id";
                    cmd.Parameters.AddWithValue("@id", id);

                    cmd.ExecuteNonQuery();
                }
            }
        }

    }
}
