using System;
using System.ComponentModel.DataAnnotations;

namespace LivrariaAPI.InputModels
{
    public class UpdateAutorInputModel
    {
        [Required]
        [MaxLength(100)]
        public string Nome { get; set; }

        [Required]
        [MaxLength(15)]
        public string CPF { get; set; }

        [Required]
        public DateTime DataNascimento { get; set; }
    }
}