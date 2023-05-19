using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using WebApplication1.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public UsuarioController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        [HttpGet]
        public JsonResult Get()
        {
            string query = @"
                            select cpf_user, name_user,login_user,password_user,datebirth_user,adm_user from
                            dbo.Usuarios
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
        public JsonResult Post(User user)
        {
            string query = @"
                           insert into dbo.Usuarios
                           values (@cpf_user,@name_user,@login_user,@password_user,@datebirth_user,@adm_user)
                            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("WinAuth");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@cpf_user", user.cpf_user); 
                    myCommand.Parameters.AddWithValue("@name_user", user.name_user); 
                    myCommand.Parameters.AddWithValue("@login_user", user.login_user);
                    myCommand.Parameters.AddWithValue("@password_user", user.password_user);
                    myCommand.Parameters.AddWithValue("@datebirth_user", user.datebirth_user);
                    myCommand.Parameters.AddWithValue("@adm_user", user.adm_user);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult("Added Successfully");
        }
        [HttpPut]
        public JsonResult Put(User user)
        {
            string query = @"
                           update dbo.Usuarios
                           set name_user= @name_user, login_user= @login_user, password_user= @password_user, datebirth_user= @datebirth_user, adm_user=@adm_user
                            where cpf_user=@cpf_user
                            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("WinAuth");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@cpf_user", user.cpf_user);
                    myCommand.Parameters.AddWithValue("@name_user", user.name_user);
                    myCommand.Parameters.AddWithValue("@login_user", user.login_user);
                    myCommand.Parameters.AddWithValue("@password_user", user.password_user);
                    myCommand.Parameters.AddWithValue("@datebirth_user", user.datebirth_user);
                    myCommand.Parameters.AddWithValue("@adm_user", user.adm_user);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult("Updated Successfully");
        }
        [HttpDelete("{cpf_user}")]
        public JsonResult Delete(string cpf_user)
        {
            string query = @"
                           delete from dbo.Usuarios
                            where cpf_user = @cpf_user
                            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("WinAuth");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@cpf_user", cpf_user);

                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult("Deleted Successfully");
        }

        [HttpPost("login")]
        public IActionResult Login(Login login)
        {
            string query = @"
                SELECT *  FROM dbo.Usuarios
                    WHERE login_user = @login_user Collate SQL_Latin1_General_CP1_CS_AS AND password_user = @password_user Collate SQL_Latin1_General_CP1_CS_AS";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("WinAuth");
            SqlDataReader myReader;

            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();

                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@login_user", login.login_user);
                    myCommand.Parameters.AddWithValue("@password_user", login.password_user);

                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }

            if (table.Rows.Count > 0)
            {

                // Retorne os dados do usuário e o token
                return new JsonResult(table);
            }
            else
            {
                // Credenciais inválidas
                return Unauthorized();
            }
        }
    }
}
