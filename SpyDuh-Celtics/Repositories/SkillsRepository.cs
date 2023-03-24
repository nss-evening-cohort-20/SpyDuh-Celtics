using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;
using SpyDuh_Celtics.Models;

namespace SpyDuh_Celtics.Repositories
{
    public class SkillsRepository : BaseRepository, ISkillsRepository
    {

        private const string _skillSelect = @"SELECT s.[id]
                                                  ,s.[skill]
	                                              ,s.userId
	                                              ,u.[name]
	                                              ,u.[location]
	                                              FROM [SpyDuh].[dbo].[skill] s
                                              JOIN [user] u on s.userId = u.id";

        private const string _skillInsert = @"INSERT INTO [SpyDuh].[dbo].[skill] (skill, userId)
                                           OUTPUT INSERTED.Id
                                           VALUES (@skill, @userId)";

        private const string _skillUpdate = @"UPDATE [SpyDuh].[dbo].[skill]
                                           SET skill = @skill,
                                               userId = @userId
                                           WHERE Id = @id";

        private const string _skillDelete = @"DELETE FROM [SpyDuh].[dbo].[skill]
                                           WHERE Id = @id";


        public SkillsRepository(IConfiguration configuration) : base(configuration)
        { 
        }

        public List<Skills> GetAll()
        {
            using var conn = Connection;
            conn.Open();

            using var cmd = conn.CreateCommand();
            cmd.CommandText = _skillSelect;

            using var reader = cmd.ExecuteReader();
            List<Skills> results = new();

            while (reader.Read())
            {
                results.Add(SkillsFromReader(reader));
            }

            return results;
        }

        public List<Skills> GetById(int id)
        {
            using var conn = Connection;
            conn.Open();

            using var cmd = conn.CreateCommand();
            cmd.CommandText = $"{_skillSelect} WHERE u.id = @id";
            cmd.Parameters.AddWithValue("@id", id);

            using var reader = cmd.ExecuteReader();
            List<Skills>? result = new();

            while (reader.Read())
            {
                result.Add(SkillsFromReader(reader));
            }

            return result;
        }

        public bool Insert(SkillInsert skill)
        {
            using var conn = Connection;
            conn.Open();

            using var cmd = conn.CreateCommand();
            cmd.CommandText = _skillInsert;
            cmd.Parameters.AddWithValue("@skill", skill.Skill);
            cmd.Parameters.AddWithValue("@userId", skill.UserId);

            skill.Id = (int)cmd.ExecuteScalar();
            return skill.Id != 0;
        }

        public bool Update(SkillInsert skill)
        {
            using var conn = Connection;
            conn.Open();

            using var cmd = conn.CreateCommand();
            cmd.CommandText = _skillUpdate;
            cmd.Parameters.AddWithValue("@id", skill.Id);
            cmd.Parameters.AddWithValue("@skill", skill.Skill);
            cmd.Parameters.AddWithValue("@userId", skill.UserId);

            int rowsAffected = cmd.ExecuteNonQuery();

            return rowsAffected > 0;
        }

        public bool Delete(int id)
        {
            using var conn = Connection;
            conn.Open();

            using var cmd = conn.CreateCommand();
            cmd.CommandText = _skillDelete;
            cmd.Parameters.AddWithValue("@id", id);

            return cmd.ExecuteNonQuery() > 0;
        }


        private Skills SkillsFromReader(SqlDataReader reader)
        {
            return new Skills()
            {
                Id = reader.GetInt32(reader.GetOrdinal("id")),
                Skill = reader.GetString(reader.GetOrdinal("skill")),
                UserId = reader.GetInt32(reader.GetOrdinal("userId")),

                User = new()
                {
                    Id = reader.GetInt32(reader.GetOrdinal("id")),
                    Name = reader.GetString(reader.GetOrdinal("name")),
                    Location = reader.GetString(reader.GetOrdinal("location")),
                }
            };
        }

    }
}
