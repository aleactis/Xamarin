using CadastroUsuarioApp.Data;
using CadastroUsuarioApp.Model;
using SQLite;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CadastroUsuarioApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UsuarioListaView : ContentPage
    {
        private SQLiteAsyncConnection bancodados;

        public UsuarioData usuariodata;

        public UsuarioListaView()
        {
            InitializeComponent();

            IconImageSource = ImageSource.FromFile("round_face_black_24.png");

            // Toolbaritem
            //tbiEditar.Clicked += TbiEditar_Clicked;

            //Evento de seleção de item da lista
            //lvContatos.ItemSelected += LvContatos_ItemSelected;

            //Evento disparado pelo botão da pesquisa
            //txtPesquisa.SearchButtonPressed += TxtPesquisa_SearchButtonPressed;

            //Inicializa a lista de dados
            ListarUsuariosAsync();
        }

        private void vBusca_SearchButtonPressed(object sender, EventArgs e)
        {
            //Texto digitado
            string texto = vBusca.Text;
            Debug.WriteLine("valor digitado na pesquisa:" + texto);
            //Executa a pesquisa na lista
            ListarUsuariosAsync(texto);
        }

        private async void ListarUsuariosAsync(string textoPesquisa = null)
        {
            // Cria a lista de pessoas
            List<Usuario> listaUsuarios = new List<Usuario>();

            // Pesquisa no banco de dados a lista de pessoas a partir do texto pesquisado
            listaUsuarios = await App.BancoDados.UsuarioRepositorio.Listar(textoPesquisa);

            //Adiciona a lista de usuários na listagem de contatos em tela
            vLista.ItemsSource = listaUsuarios;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            // Inicializa a lista            
            ListarUsuariosAsync();
        }

        private async void vLista_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            //Pega o item selecionado e faz a conversao de dados
            var item = e.SelectedItem as Usuario;
            //Exibe uma mensagem em tela
            //DisplayAlert("Titulo", $"Mensagem: { item.Nome }", "Fechar");
            //Separando o fluxo de procesamento de dados
            var resposta = await
                DisplayAlert("Titulo", $"Mensagem \n { ExibirDados(item) }", "Editar", "Fechar");
            //Verfica a opcao escolhida editar = true e fechar = false
            if (resposta)
            {
                await Navigation.PushAsync(new UsuarioEdicaoView(item));
            }
        }

        private string ExibirDados(Usuario Usuario)
        {
            StringBuilder mensagem = new StringBuilder();
            mensagem.Append($"Nome: {Usuario.Nome} \n");
            mensagem.Append($"Telefone: {Usuario.Idade} \n");
            mensagem.Append($"E-mail: {Usuario.Email} \n");

            return mensagem.ToString();
        }

        private void btnAdicionar_Clicked(object sender, EventArgs e)
        {
            //this.Navigation.PushAsync(new UsuarioEdicaoView(new Usuario()));
            this.Navigation.PushAsync(new UsuarioEdicaoView(null));
        }
    }
}