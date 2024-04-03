using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace TesteIATec.Models
{
    [Index("Cpf", IsUnique = true)]
    public class Pessoa
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(200, MinimumLength = 2, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(60, ErrorMessage = "O campo {0} deve ter no máximo {1} caracteres")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Formato inválido para o campo {0}")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public char Sexo { get; set; }

        public string? Nacionalidade { get; set; }


        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [DataType(DataType.Date, ErrorMessage = "Formato inválido para o campo {0}")]
        [Display(Name = "Data de Nascimento")]
        public DateOnly DataNascimento { get; set; }


        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [Display(Name = "CPF")]
        public string Cpf { get; set; }

        [DataType(DataType.Text, ErrorMessage = "Digite apenas números")]
        [Display(Name = "Telefone (somente números)")]
        public List<string> Telefone { get; set; }
    }
}
