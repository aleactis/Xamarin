using SQLite;

namespace CadastroUsuarioApp.Model
{
    [Table("Usuarios")]
    public class Usuario
    {
        //Chave primária da tabela
        [PrimaryKey, AutoIncrement]
        public int IdUsuario { get; set; }

        private string nome;

        [MaxLength(50), NotNull]
        public string Nome
        {
            get { return nome; }
            set { nome = value; }
        }

        public int Idade { get; set; }
        [NotNull]
        public string Email { get; set; }

        //Métodos auxiliares
        public bool Validar()
        {
            bool retorno;
            //Verifica se foi preenchido o nome
            if (nome.Length > 0)
            {
                retorno = true;
            }
            else if (Email.Length > 0)
            {
                retorno = true;
            }
            else
            {
                retorno = false;
            }

            return retorno;
        }
    }
}
