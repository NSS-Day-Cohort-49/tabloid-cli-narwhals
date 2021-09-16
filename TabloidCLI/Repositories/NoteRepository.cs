using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using TabloidCLI.Models;
using TabloidCLI.Repositories;

namespace TabloidCLI
{
    class NoteRepository : DatabaseConnector, IRepository<Note>
    {
        private int _postId;
        public NoteRepository(string connectionString, int postId) : base(connectionString) 
        {
            _postId = postId;
        }

        public List<Note> GetAll()
        {
            throw new NotImplementedException();
        }

        public Note Get(int id)
        {
            throw new NotImplementedException();
        }

        public void Insert(Note note)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"INSERT INTO Note (Title, Content, CreateDateTime, PostId)
                                        VALUES (@title, @content, @createDateTime, @postId)";
                    cmd.Parameters.AddWithValue("@title", note.Title);
                    cmd.Parameters.AddWithValue(@"content", note.Content);
                    cmd.Parameters.AddWithValue("@createDateTime", note.CreateDateTime);
                    cmd.Parameters.AddWithValue("@postId", _postId);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Update(Note note)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }


    }
}
