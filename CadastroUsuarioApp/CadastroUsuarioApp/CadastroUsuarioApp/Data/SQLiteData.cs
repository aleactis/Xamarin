using CadastroUsuarioApp.Model;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace CadastroUsuarioApp.Data
{
    public class SQLiteData
    {
        //Definir as tabelas no banco de dados adicionando o
        //mapeamento das classes do modelo de dados no método construtor
        
        readonly SQLiteAsyncConnection database;
        public UsuarioData UsuarioRepositorio { get; set; }
        public SQLiteData(string path)
        {
            database = new SQLiteAsyncConnection(path);
            database.CreateTableAsync<Usuario>().Wait();
            UsuarioRepositorio = new UsuarioData(database);
        }
    }
}
