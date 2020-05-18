using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SQLite;
using CadastroUsuarioApp.Data;
using CadastroUsuarioApp.Views;

namespace CadastroUsuarioApp
{
    public partial class App : Application
    {
        static SQLiteData database;

        public static SQLiteData BancoDados
        {
            get
            {
                if (database == null)
                {
                    database = new SQLiteData(
                        DependencyService.Get<ISQLiteBD>().SQLiteLocalPath("dados.db3"));
                }
                return database;
            }
        }

        //Uma instância do CustomersPage não está atribuída ao MainPage diretamente; em vez disso, 
        //é encapsulado como o parâmetro para uma instância da classe NavigationPage. O motivo é que 
        //usar uma NavigationPage é a única maneira de mostrar uma barra de menus no Android, mas isso 
        //não afeta o comportamento da interface do usuário.
        public App()
        {
            InitializeComponent();

            //MainPage = new MainPage();
            MainPage = new NavigationPage(new UsuarioListaView());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
