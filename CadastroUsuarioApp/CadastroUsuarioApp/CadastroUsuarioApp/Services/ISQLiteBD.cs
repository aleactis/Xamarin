using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace CadastroUsuarioApp
{
    public interface ISQLiteBD
    {
        //Essa interface expõe um método chamado DbConnection, que será implementado em cada
        //projeto específico da plataforma e retornará a seqüência de conexão apropriada.
        string SQLiteLocalPath(string arquivo);
    }
}
