using System.ComponentModel.DataAnnotations;

namespace LivrariaAPI.InputModels
{
    public class UpdateLivroInputModel
    {
        [Required]
        [MaxLength(150)]
        public string Titulo { get; set; }

        [Required]
        public decimal Preco { get; set; }
    }
}
