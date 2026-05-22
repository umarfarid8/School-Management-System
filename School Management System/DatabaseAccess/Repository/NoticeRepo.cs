using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Microsoft.Data.SqlClient;
using School_Management_System.DatabaseAccess.EntityFramework.Entities;

namespace School_Management_System.DatabaseAccess.Repository
{
    public class NoticeRepo
    {
        private readonly string _connectionString = @"Server=DESKTOP-279I7PS;Database=SchoolManagementDB;Trusted_Connection=True;TrustServerCertificate=True;";

        public void AddNotice(Notice notice)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string sql = "INSERT INTO Notices (Title, Content, DatePosted) VALUES (@title, @content, @date)";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@title", notice.Title);
                    cmd.Parameters.AddWithValue("@content", notice.Content);
                    cmd.Parameters.AddWithValue("@date", DateTime.Now);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }
        public List<Notice> GetAllNotices()
        {
            List<Notice> notices = new List<Notice>();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string sql = "SELECT *FROM Notices ORDER BY DatePosted DESC";
                SqlCommand cmd = new SqlCommand(sql, conn);
                conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        notices.Add(new Notice
                        {
                            Id = (int)reader["Id"],
                            Title = reader["Title"].ToString(),
                            Content = reader["Content"].ToString(),
                            DatePosted = (DateTime)reader["DatePosted"]
                        });
                    }
                }
            }
            return notices;
        }
        public void UpdateNotice(Notice notice)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string sql = "Update Notices SET Title=@title, Content=@content WHERE Id = @id";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@id", notice.Id);
                    cmd.Parameters.AddWithValue("@title", notice.Title);

                    cmd.Parameters.AddWithValue("@content", notice.Content);
                    conn.Open();
                    cmd.ExecuteNonQuery();

                }
            }
        }
        public void DeleteNotice(int noticeId)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string sql = "DELETE FROM Notices WHERE Id= @id";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@id", noticeId);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
