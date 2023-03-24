using Azure.Core;
using Microsoft.Data.SqlClient;
using SpyDuh_Celtics.Models;
using SpyDuh_Celtics.Utils;
using System.Data;

namespace SpyDuh_Celtics.Repositories
{

    public class RelationshipRepository : BaseRepository, IRelationshipRepository
    {
        public RelationshipRepository(IConfiguration configuration) : base(configuration) { }

        /* Create Get All Request for All Friends */

        public List<Relationship> GetAllFriendsById(int id)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                          SELECT r.userOne
                                ,r.userTwo
                                ,r.isEnemy
                                
                                ,u.id
                                ,u.[name]
                                ,u.[location]
                            FROM relationship r
                            JOIN [user] u
                              ON r.userTwo = u.id
                           WHERE r.userOne = @id
                             AND r.isEnemy = 0
                           UNION 
                          SELECT r.userTwo
                                ,r.userOne
                                ,r.isEnemy

                                ,u.id
                                ,u.[name]
                                ,u.[location]
                           FROM relationship r
                           JOIN [user] u
                             ON r.userOne = u.id
                          WHERE r.userTwo = @id
                            AND r.isEnemy = 0";

                    DbUtils.AddParameter(cmd, "id", id);

                    var reader = cmd.ExecuteReader();

                    var friends = new List<Relationship>();
                    while (reader.Read())
                    {
                        var friendId = DbUtils.GetInt(reader, "id");

                        var existingFriend = friends.FirstOrDefault(friends => friends.Id == friendId);
                        if (existingFriend == null)
                        {
                            existingFriend = new Relationship()
                            {
                                Id = id,
                                UserOne = new User()
                                {
                                    Id = DbUtils.GetInt(reader, "id"),
                                    Name = DbUtils.GetString(reader, "name"),
                                    Location = DbUtils.GetString(reader, "location"),
                                },
                                UserTwo = new User()
                                {
                                    Id = DbUtils.GetInt(reader, "id"),
                                    Name = DbUtils.GetString(reader, "name"),
                                    Location = DbUtils.GetString(reader, "location"),
                                },
                                IsEnemy = reader.GetBoolean("isEnemy"),
                            };

                        }

                        if (DbUtils.IsNotDbNull(reader, "id"))
                        {
                            existingFriend.Id = DbUtils.GetInt(reader, "id");
                        }

                        friends.Add(existingFriend);
                    }

                    reader.Close();

                    return friends;
                }
            }
        }

        /* Create Get All Request for All Foes */
        public List<Relationship> GetAllFoesById(int id)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                          SELECT r.userOne
                                ,r.userTwo
                                ,r.isEnemy
                                
                                ,u.id
                                ,u.[name]
                                ,u.[location]
                            FROM relationship r
                            JOIN [user] u
                              ON r.userTwo = u.id
                           WHERE r.userOne = @id
                             AND r.isEnemy = 1
                           UNION 
                          SELECT r.userTwo
                                ,r.userOne
                                ,r.isEnemy

                                ,u.id
                                ,u.[name]
                                ,u.[location]
                           FROM relationship r
                           JOIN [user] u
                             ON r.userOne = u.id
                          WHERE r.userTwo = @id
                            AND r.isEnemy = 1";

                    DbUtils.AddParameter(cmd, "id", id);

                    var reader = cmd.ExecuteReader();

                    var foes = new List<Relationship>();
                    while (reader.Read())
                    {
                        var foeId = DbUtils.GetInt(reader, "id");

                        var existingFoe = foes.FirstOrDefault(foes => foes.Id == foeId);
                        if (existingFoe == null)
                        {
                            existingFoe = new Relationship()
                            {
                                Id = id,
                                UserOne = new User()
                                {
                                    Id = DbUtils.GetInt(reader, "id"),
                                    Name = DbUtils.GetString(reader, "name"),
                                    Location = DbUtils.GetString(reader, "location"),
                                },
                                UserTwo = new User()
                                {
                                    Id = DbUtils.GetInt(reader, "id"),
                                    Name = DbUtils.GetString(reader, "name"),
                                    Location = DbUtils.GetString(reader, "location"),
                                },
                                IsEnemy = reader.GetBoolean("isEnemy"),
                            };

                        }

                        if (DbUtils.IsNotDbNull(reader, "id"))
                        {
                            existingFoe.Id = DbUtils.GetInt(reader, "id");
                        }

                        foes.Add(existingFoe);
                    }

                    reader.Close();

                    return foes;
                }
            }
        }

        /* Create Get Request for individual Friends */


        /* Create Post Request to add a new Friend to my user id */

        public void AddFriend(NewRelationship relationship)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        INSERT INTO relationship (userOne, userTwo, isEnemy)
                             OUTPUT INSERTED.id
                             VALUES (@userOne, @userTwo, 0)";
                    cmd.Parameters.AddWithValue("@userOne", relationship.UserOne);
                    cmd.Parameters.AddWithValue("@userTwo", relationship.UserTwo);

                    relationship.Id = (int)cmd.ExecuteScalar();
                }
            }
        }

        /* Create Post Request to add a new foe to my user id */
        public void AddFoe(NewRelationship relationship)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        INSERT INTO relationship (userOne, userTwo, isEnemy)
                             OUTPUT INSERTED.id
                             VALUES (@userOne, @userTwo, 1)";
                    cmd.Parameters.AddWithValue("@userOne", relationship.UserOne);
                    cmd.Parameters.AddWithValue("@userTwo", relationship.UserTwo);

                    relationship.Id = (int)cmd.ExecuteScalar();
                }
            }
        }

        /* Create Delete Request to delete Friend from selected user */
        public void Delete(int id)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "DELETE FROM relationship WHERE id = @id";
                    DbUtils.AddParameter(cmd, "@id", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Add(Relationship relationship)
        {
            throw new NotImplementedException();
        }
    }
}
