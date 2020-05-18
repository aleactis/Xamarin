using SQLite;
using System.IO;
using CadastroUsuarioApp.Droid;
[assembly: Xamarin.Forms.Dependency(typeof(SQLiteBD))]

namespace CadastroUsuarioApp.Droid
{
    public class SQLiteBD : ISQLiteBD
    {
        public string SQLiteLocalPath(string arquivo)
        {
            string path = System.Environment.GetFolderPath(
                System.Environment.SpecialFolder.Personal);
            return Path.Combine(path, arquivo);
        }
    }
}
