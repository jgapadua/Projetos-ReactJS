using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using System.Data;
using System.Data.SqlClient;
using WebApplication1.DTO;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public PostController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        [HttpGet]
        public JsonResult Get()
        {
            string query = @"
                            select id_post, title_post, description_post,image_post,date_post,likes_post,cpf_user from
                            dbo.Posts
                            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("WinAuth");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }
            return new JsonResult(table);

        }
        [HttpPost]
        public JsonResult Post(Post p)
        {
            string query = @"
                           insert into dbo.Posts
                           VALUES (@title_post,@description_post,@image_post,@date_post,@likes_post,@cpf_user)
                            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("WinAuth");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@title_post", p.title_post);
                    myCommand.Parameters.AddWithValue("@description_post", p.description_post);
                    myCommand.Parameters.AddWithValue("@image_post", p.image_post);
                    myCommand.Parameters.AddWithValue("@date_post", p.date_post);
                    myCommand.Parameters.AddWithValue("@likes_post", p.likes_post);
                    myCommand.Parameters.AddWithValue("@cpf_user", p.cpf_user);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult("Added Successfully");
        }
        [HttpPut("{id_post}")]
        public JsonResult Put(int id_post,PostDTO p)
        {
            string query = @"
                           update dbo.Posts
                           set title_post= @title_post, description_post= @description_post, image_post= @image_post, date_post= @date_post, likes_post=@likes_post
                            where id_post=@id_post
                            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("WinAuth");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@id_post", id_post);
                    myCommand.Parameters.AddWithValue("@title_post", p.title_post);
                    myCommand.Parameters.AddWithValue("@description_post", p.description_post);
                    myCommand.Parameters.AddWithValue("@image_post", p.image_post);
                    myCommand.Parameters.AddWithValue("@date_post", p.date_post);
                    myCommand.Parameters.AddWithValue("@likes_post", p.likes_post);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult("Updated Successfully");
        }
        [HttpDelete("{id_post}")]
        public JsonResult Delete(int id_post)
        {
            string query = @"
                           delete from dbo.Posts
                            where id_post = @id_post
                            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("WinAuth");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@id_post", id_post);

                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult("Deleted Successfully");
        }

        [HttpPost("{id_post}/like")]
        public JsonResult Like(int id_post)
        {
            string query = @"
                   UPDATE dbo.Posts
                   SET likes_post = likes_post + 1
                   WHERE id_post = @id_post
                   ";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("WinAuth");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@id_post", id_post);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult("Post Liked Successfully");
        }
        [HttpPost("{id_post}/unlike")]
        public JsonResult UnLike(int id_post)
        {
            string query = @"
                   UPDATE dbo.Posts
                   SET likes_post = likes_post - 1
                   WHERE id_post = @id_post AND likes_post > 0
                   ";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("WinAuth");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@id_post", id_post);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult("Post Unliked Successfully");
        }
    }
}

