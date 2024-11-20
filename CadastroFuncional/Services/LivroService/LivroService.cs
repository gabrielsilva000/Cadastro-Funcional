using CadastroFuncional.Models;
using Dapper;
using System.Data.SqlClient;

namespace CadastroFuncional.Services.LivroService
{
    public class LivroService : ILivroInterface
    {
        private readonly IConfiguration _configuration;
        private readonly string getConnection;
        public LivroService(IConfiguration configuration) 
        {
            _configuration = configuration;
            getConnection = _configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<IEnumerable<Livro>> CreateLivro(Livro livro)
        {
            using (var con = new SqlConnection(getConnection))
            {
                var sql = "insert into livros (titulo, autor) values (@titulo, @autor)";
                await con.ExecuteAsync(sql, livro);

                return await con.QueryAsync<Livro>("select * from Livros");
            }
            
        }

        public async Task<IEnumerable<Livro>> DeleteLivro(int livroId)
        {
            using (var con = new SqlConnection(getConnection)) {
                var sql = "delete from livros where id = @id";
                await con.ExecuteAsync(sql, new { Id = livroId });

                return await con.QueryAsync<Livro>("select * from Livros");
            }
        }

        public async Task<IEnumerable<Livro>> GetAllLivros()
        {
            using(var con = new SqlConnection(getConnection))
            {
                var sql = "select * from Livros";
                return await con.QueryAsync<Livro>(sql);
            }
        }

        public async Task<Livro> GetLivroById(int livroId)
        {
            using (var con = new SqlConnection(getConnection))
            {
                var sql = "select * from Livros where id = @Id";
                return await con.QueryFirstAsync<Livro>(sql, new { Id = livroId });
            }
        }

        public async Task<IEnumerable<Livro>> UpdateLivro(Livro livro)
        {
            using (var con = new SqlConnection(getConnection))
            {
                var sql = "update livros set titulo = @titulo, autor = @autor where id = @id";
                await con.ExecuteAsync(sql, livro);

                return await con.QueryAsync<Livro>("select * from Livros");
            }
        }
    }
}
