using CadastroUsuarioApp.Model;
using System;
using System.Diagnostics;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CadastroUsuarioApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UsuarioEdicaoView : ContentPage
    {
        public Usuario usuarioContext { get;  set; }

        public UsuarioEdicaoView(Usuario usuario)
        {
            InitializeComponent();

            //Titulo da pagina
            this.Title = "Editar contato";
            //Faz o carregamento dos dados na inicializacao
            ExibirDados(usuario);
            IniciarUsuario(usuario);
        }

        private void IniciarUsuario(Usuario usuario)
        {
            // Verificação do tipo de operação
            if (usuario == null)
            {
                //Inicializa a pessoa                
                this.Title = "Inclusão do usuário";
                //Instanciar o objeto Binding vazio
                usuarioContext = new Usuario();
                //Dados inicais
                //usuarioContext.Foto = "icavatarapp48x48.png";
                //usuarioContext.Nome = "SP";
                //Habilita o BindingContext
                BindingContext = usuarioContext;

            }
            else
            {
                this.Title = "Alteração do contato";
                //Preencher o objeto Binding com os dados recebidos
                usuarioContext = usuario;
                //Dados iniciais
                //Habilita o BindingContext
                BindingContext = usuarioContext;
            }
        }

        private void ExibirDados(Usuario usuario)
        {
            //Prenchimento do formulario não pode estar vazio
            if (usuario != null)
            {
                vNome.Text = usuario.Nome;
                vIdade.Text = usuario.Idade.ToString();
                vEmail.Text = usuario.Email;
            }
        }

        private void BtnSalvar_Clicked(object sender, EventArgs e)
        {
            //Verifica se o cadastro está preenchido
            if (usuarioContext != null)
            {
                // Verifica se foi salvo com sucesso
                if (SalvarPessoa(usuarioContext))
                {
                    //Volta para a pagina anterior
                    Navigation.PopAsync(true);
                }
            }
            else
            {
                //Mensagem de aviso para o usuario
                this.DisplayAlert("Atenção", "Por favor prencha corretamente o cadastro!", "Fechar");
            }
        }

        private bool SalvarPessoa(Usuario usuario)
        {
            bool retorno = false;
            //Verifica se existe uma pessoa
            if (ValidarUsuario(usuario.Nome, usuario.Email))
            {
                //Faz o cadastramento da pessoa no banco de dados
                var salvo = App.BancoDados.UsuarioRepositorio.SalvarAsync(usuario);
                //Verifica o retono da execução do comando
                Debug.WriteLine("Valor de retorno " + int.Parse(salvo.Result.ToString()));
                if (salvo.Result > 0)
                {
                    this.DisplayAlert("Informação", "Cadastro salvo com sucesso!", "Fechar");
                    //Inicia novo cadastro
                    //pessoaContext = new Pessoa();                    
                    //this.BindingContext = pessoaContext;
                    retorno = true;
                }
            }
            else
            {
                //Mensagem de aviso para o usuario
                this.DisplayAlert("Atenção", "Por favor prencha corretamente o cadastro!", "Fechar");
            }
            return retorno;
        }

        private bool ValidarUsuario(string nome, string email)
        {
            bool retorno = true;
            // Inicializa background padrão dos campos
            vNome.BackgroundColor = Color.White;
            vEmail.BackgroundColor = Color.White;

            //Campos obrigatórios do cadastro
            //Verifica o nome
            if (string.IsNullOrWhiteSpace(nome))
            {
                //Formata o background do campo com erro
                vNome.BackgroundColor = Color.Red;
                retorno = false;
            }
            //Verifica o email
            else if (string.IsNullOrWhiteSpace(email))
            {
                //Formata o background do campo com erro
                vEmail.BackgroundColor = Color.Red;
                retorno = false;
            }
            return retorno;
        }

        private void BtnExcluir_Clicked(object sender, EventArgs e)
        {
            if (usuarioContext != null)
            {
                // Verifica se excluiu o usuário no bd
                if (ValidarExclusao(usuarioContext))
                {
                    //Volta para a pagina anterior
                    Navigation.PopAsync(true);
                }
            }
        }

        private bool ValidarExclusao(Usuario usuario)
        {
            bool retorno = false;
            var excluido = App.BancoDados.UsuarioRepositorio
                                .ExcluirAsync(usuario);
            if (excluido.Result > 0)
            {
                this.DisplayAlert("Informação",
                "Usuário excluído com sucesso!", "Fechar");
                retorno = true;
            }
            return retorno;
        }
    }
}