using CadastroUsuarioApp.Model;
using SQLite;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;

namespace CadastroUsuarioApp.Data
{
    public class UsuarioData
    {
        //Atributo
        private List<Usuario> listaUsuarios;

        //Acesso ao BD
        private SQLiteAsyncConnection bancoDados;

        //Construtor
        public UsuarioData(SQLiteAsyncConnection bancodados)
        {
            //Instancia a lista em memória
            listaUsuarios = new List<Usuario>();

            bancoDados = bancodados;
        }

        //Metodos auxiliares
        public Task<List<Usuario>> Listar(string textoPesquisa)
        {
            var tabelaUsuario = bancoDados.Table<Usuario>();
            // Verifica se o texto de pesquisa esta vazio
            if (string.IsNullOrWhiteSpace(textoPesquisa))
            {
                // Retorna todos os registro da tabela
                return tabelaUsuario.ToListAsync();
            }
            else
            {
                //  Retorna a tabela a partir do resultado de pesquisa
                return tabelaUsuario.Where(
                   p => p.Nome.Contains(textoPesquisa)
                   || p.Email.Contains(textoPesquisa)).ToListAsync();
            }

            ////Adicionar Usuarios na lista
            //listaUsuarios.Add(
            //    new Usuario()
            //    {
            //        IdUsuario = 1,
            //        Nome = "Usuario 1",
            //        Idade = 22,
            //        Email = "Usuario@teste.com.br"
            //    });
            //listaUsuarios.Add(
            //    new Usuario()
            //    {
            //        IdUsuario = 2,
            //        Nome = "Usuario 2",
            //        Idade = 27,
            //        Email = "Usuario@teste2.com.br"
            //    });
            //listaUsuarios.Add(
            //    new Usuario()
            //    {
            //        Nome = "Usuario 3",
            //        Idade = 29,
            //        Email = "Usuario@teste3.com.br"
            //    });
            //return listaUsuarios;
        }

        public Task ListarAsync()
        {
            //Tratamento de erros
            try
            {
                //Comando de consulta e retorna a lista de dados
                var select = bancoDados.Table<Usuario>().ToListAsync();
                return select;
            }
            catch (Exception erro)
            {
                throw new Exception(erro.Message);
            }
        }

        public Task InserirAsync(Usuario usuario)
        {
            try
            {
                //Comando de inclusão de dados
                var insert = bancoDados.InsertAsync(usuario);
                return insert;
            }
            catch (Exception erro)
            {
                throw new Exception(erro.Message);
            }
        }

        public Task BuscarAsync(int id, string nome)
        {
            try
            {
                //Comando de consulta e retorna a um registro de dados
                var select = bancoDados
                    .Table<Usuario>() //Tabela 
                    .Where(o => o.IdUsuario == id || o.Nome.Contains(nome)) //Codicao de filtro ||ou &&e
                    .FirstOrDefaultAsync(); //Tipo de retorno
                return select;
            }
            catch (Exception erro)
            {
                //throw new Exception(erro.Message);
                //Faz o log do erro e nao trava a execucao do aplicativo
                Debug.WriteLine($"Erro: {erro.Message}");
                return null;
            }
        }

        public Task AlterarAsync(Usuario usuario)
        {
            try
            {
                //Comando de alteração de dados
                var update = bancoDados.UpdateAsync(usuario);
                return update;
            }
            catch (Exception erro)
            {
                throw new Exception(erro.Message);
            }

        }
        public Task<int> ExcluirAsync(Usuario usuario)
        {
            try
            {
                //Comando de exclusão de dados
                var delete = bancoDados.DeleteAsync(usuario);
                return delete;
            }
            catch (Exception erro)
            {
                throw new Exception(erro.Message);
            }
        }

        public Task<int> SalvarAsync(Usuario usuario)
        {
            if (usuario.IdUsuario != 0)
            {
                return bancoDados.UpdateAsync(usuario);
            }
            else
            {
                return bancoDados.InsertAsync(usuario);
            }
        }
    }
}
