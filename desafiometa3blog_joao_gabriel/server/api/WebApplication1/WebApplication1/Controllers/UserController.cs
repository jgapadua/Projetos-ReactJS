using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using WebApplication1.Models;

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
                SELECT cpf_user, name_user, datebirth_user,adm_user
                    FROM dbo.Usuarios
                    WHERE login_user = @login_user AND password_user = @password_user";

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
                // Login bem-sucedido
                var user = new User
                {
                    cpf_user = table.Rows[0]["cpf_user"].ToString(),
                    name_user = table.Rows[0]["name_user"].ToString(),
                    datebirth_user = Convert.ToDateTime(table.Rows[0]["datebirth_user"]),
                    adm_user = Convert.ToBoolean(table.Rows[0]["adm_user"])
                };

                // Gere um token de autenticação (pode ser usando JWT, por exemplo)
                var token = GenerateToken(user);

                // Retorne os dados do usuário e o token
                return new JsonResult(new
                {
                    user,
                    token
                });
            }
            else
            {
                // Credenciais inválidas
                return Unauthorized();
            }
        }

        private string GenerateToken(User user)
        {
            // Aqui você pode implementar a geração do token de autenticação,
            // como por exemplo usando JWT (JSON Web Tokens).
            // O token deve conter as informações do usuário autenticado,
            // como o CPF, nome e qualquer outra informação necessária.
            // Retorne o token gerado.

            return "exemplo-token";
        }
    }
}
