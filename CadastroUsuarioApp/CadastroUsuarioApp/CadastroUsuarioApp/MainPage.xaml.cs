using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using CadastroUsuarioApp.Model;

namespace CadastroUsuarioApp
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            Usuario Usuario = new Usuario();

            //Título da página
            this.Title = "Inserir Usuários";

            //Faz o carregamento dos dados na inicializacao
            //ExibirDados(Usuario);
        }

        void OnSaveButtonClicked(object sender, EventArgs e)
        {
            
        }

        private void ExibirDados(Usuario Usuario)
        {
            //Prenchimento do formulario não pode estar vazio
            if (Usuario != null)
            {
                vNome.Text = Usuario.Nome;
                vIdade.Text = Usuario.Idade.ToString();
                vEmail.Text = Usuario.Email;
            }
        }
    }
}
